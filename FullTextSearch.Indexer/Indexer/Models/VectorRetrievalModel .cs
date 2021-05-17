using CrawlerIR2.Indexer;
using FullTextSearch.SimpleLogger;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FullTextSearch.Indexer.Indexer.Models
{
    public class VectorRetrievalModel : IRetrievalModel
    {
        private Index _index;
        /// <summary>
        /// IDF
        /// </summary>
        private ConcurrentDictionary<string, double> _termsVectors; 
        private Dictionary<long, Vector> _queryVector;
        private ConcurrentDictionary<long, Dictionary<long, Vector>> _documentVectors;
        private Stopwatch _stopwatch = new Stopwatch();
        private HashSet<long> _documents;
        private int _documentsCount;

        /// <summary>
        /// Preprocessing instance
        /// </summary>
        public IPreprocessing Preprocessing { get; }
        public Index Index { get => _index; set { _index = value; } }

        public VectorRetrievalModel(IPreprocessing preprocessing)
        {
            Preprocessing = preprocessing; 
        }

        public List<IResult> EvaluateResults(string query, Index index)
        {
            _index = index;

            Logger.Info("VectorRetrievalModel: Calculating cosine similarity");
            // Learn model if not exist
            if (_documentVectors == null)
            {
                LearningModel();
            }

            // Get results
            List<IResult> results = QueryingModel(query);

            // Sort results
            var orderByDescendingResult = from s in results
                                          orderby s.GetScore() descending
                                          select s;
            return orderByDescendingResult.ToList();
        }

        /// <summary>
        /// Learn the weights of document index terms.
        /// </summary>
        private void LearningModel()
        {
            _termsVectors = new ConcurrentDictionary<string, double>();
            _documentVectors = new ConcurrentDictionary<long, Dictionary<long, Vector>>();

            // Getting Documents list
            _documents = GetDocuments();

            // Calculating IDF
            Logger.Info("VectorRetrievalModel: Calculating IDF");
            _stopwatch.Reset();
            _stopwatch.Start();
            CalculateIDF();
            _stopwatch.Stop();
            Logger.Info("VectorRetrievalModel: Calculating IDF execution time is " + _stopwatch.ElapsedMilliseconds + " ms");

            // Transforming docs to vectors
            Logger.Info("VectorRetrievalModel: Transforming documents into vectors");
            _stopwatch.Reset();
            _stopwatch.Start();
            GetDocumentVectors();
            Logger.Info("VectorRetrievalModel: Transforming documents into vectors execution time is " + _stopwatch.ElapsedMilliseconds + " ms");
        }

        private HashSet<long> GetDocuments()
        {
            var result = new HashSet<long>();
            _documentsCount = 0;
            foreach (var entry in _index.InvertedIndex)
            {
                foreach (var docEntry in entry.Value)
                {
                    if (!result.Contains(docEntry.Key))
                    {
                        result.Add(docEntry.Key);
                        _documentsCount++;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Use the similarity function to find the relevant documents.
        /// </summary>
        private List<IResult> QueryingModel(string query)
        {
            if (_documentVectors == null || _documentVectors.Count() != _documentsCount)
            {
                throw new ArgumentNullException("Documents vectors cannot be null");
            }

            _queryVector = new Dictionary<long, Vector>();

            Logger.Info("VectorRetrievalModel: Tranforming query to vector");
            Dictionary<string, int> queryDictionary = CreateQueryDictionary(Preprocessing.ParseTokens(query));
            _queryVector = TransformToQueryVector(queryDictionary, _index.TermsCount);

            // Using the similarity function to find the relevant documents
            Logger.Info("VectorRetrievalModel: Using the similarity function to find the relevant documents");
            _stopwatch.Reset();
            _stopwatch.Start();
            List<IResult> result = CalculateSimilarityFunction();
            Logger.Info("VectorRetrievalModel: Using the similarity function to find the relevant documents execution time is " + _stopwatch.ElapsedMilliseconds + " ms");
            return result;
        }

        private List<IResult> GetDocumentVectors()
        {
            var results = new List<IResult>();

            //Parallel.For(1, _documentsCount + 1, document => 
            //{
            //    Dictionary<long, Vector> docVector;
            //    docVector = TransformToDocumentVectors(document);
            //    // Save whole vector to dictionary
            //    _documentVectors.TryAdd(document, docVector);
            //});

            for (int i = 1; i < _documentsCount + 1; i++)
            {
                Dictionary<long, Vector> docVector;
                docVector = TransformToDocumentVectors(i);
                // Save whole vector to dictionary
                _documentVectors.TryAdd(i, docVector);
            }
            return results;
        }

        private List<IResult> CalculateSimilarityFunction()
        {
            var results = new ConcurrentBag<IResult>();

            Parallel.For(1, _documentsCount + 1, document =>
            {
                // Find cos similarity
                double cosSim = GetCos(_documentVectors[document], _queryVector);
                results.Add(new Result(document.ToString()) { Score = cosSim });
            });
            return results.ToList();
        }

        /// <summary>
        /// Find query token freq
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns></returns>
        private Dictionary<string, int> CreateQueryDictionary(string[] tokens)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            foreach (string token in tokens)
            {
                if (result.ContainsKey(token))
                {
                    result[token]++;
                } else
                {
                    result.Add(token, 1);
                }
            }
            return result;
        }

        /// <summary>
        /// Compute query vektor
        /// </summary>
        /// <param name="tokens">Tokens and their freq</param>
        /// <param name="terms_count"></param>
        private Dictionary<long, Vector> TransformToQueryVector(Dictionary<string, int> tokens, int terms_count)
        {
            int word_index = 0;
            var vector = new Dictionary<long, Vector>();
            foreach (KeyValuePair<string, Dictionary<int, Document>> entry in _index.InvertedIndex)
            {
                if (tokens.ContainsKey(entry.Key))
                {
                    double tf_wght = 1 + Math.Log10(tokens[entry.Key]);
                    double tf_idf = tf_wght * _termsVectors[entry.Key];

                    // Save vector value
                    vector.Add(word_index, new Vector(word_index, tf_idf));
                } 
                word_index++;
            }
            return vector;
        }

        /// <summary>
        /// Traverse list of documents and create documents vectors
        /// </summary>
        private Dictionary<long, Vector> TransformToDocumentVectors(int document)
        {
            ConcurrentDictionary<long, Vector> vectors = new ConcurrentDictionary<long, Vector>();

            Parallel.ForEach(_index.InvertedIndex, (entry, state, word_index) =>
            {
                Document value = null;
                if (entry.Value.TryGetValue(document, out value))
                {
                    // Check if TF is not 0
                    double tf_wght = value.TF > 0 ? 1 + Math.Log10(value.TF) : 0;
                    double tf_idf = tf_wght * _termsVectors[entry.Key];

                    // Save vector value
                    vectors.TryAdd(word_index, new Vector(word_index, tf_idf));
                }
            });

            return vectors.ToDictionary(entry => entry.Key, 
                entry => entry.Value);
        }

        /// <summary>
        /// Calculate IDF value
        /// </summary>
        /// <param name="documentCount">Documents count</param>
        public void CalculateIDF()
        {
            if (_termsVectors == null)
            {
                return;
            }

            _index.InvertedIndex.AsParallel().ForAll(entry => 
            {
                double tmp = (double)_documentsCount / (double)entry.Value.Count;
                double idf = Math.Log10(tmp);
                _termsVectors.TryAdd(entry.Key, idf);
            });

            //foreach (KeyValuePair<string, Dictionary<int, Document>> entry in _index.InvertedIndex)
            //{
            //    double tmp = (double)_index.DocCount / (double)entry.Value.Count;
            //    double idf = Math.Log10(tmp);
            //    _termsVectors.Add(entry.Key, idf);
            //}

        }

        /// <summary>
        /// Calculate cosine similarity
        /// </summary>
        /// <param name="docname"></param>
        /// <param name="queryname"></param>
        /// <returns></returns>
        internal double GetCos(Dictionary<long, Vector> docVectror, Dictionary<long, Vector> queryVector)
        {
            if (docVectror == null || queryVector == null)
            {
                throw new Exception("Cosine Distance: vectors cannot be null");
            }

            double scalar = 0;
            double docVecNorm = 0;
            double queryVecNorm = 0;
            int position = 0;

            while (_index.TermsCount >= position)
            {
                bool isDoc      = docVectror.ContainsKey(position);
                bool isQuery    = queryVector.ContainsKey(position);
               
                // Compute scalar
                if (isDoc && isQuery)       // not 0
                {
                    scalar += docVectror[position].Value * queryVector[position].Value;
                }

                // Compute document vector norm
                if (isDoc)
                {
                    docVecNorm += Math.Pow(docVectror[position].Value, 2);
                }

                // Compute query vector norm
                if (isQuery)
                {
                    queryVecNorm += Math.Pow(queryVector[position].Value, 2);
                }

                position++;
            }

            // Cosine similarity
            return scalar / (Math.Sqrt(docVecNorm) * Math.Sqrt(queryVecNorm));
        }
    }

    sealed class Vector
    {
        public long Position { get; set; }
        public double Value { get; set; }

        public Vector (long position, double value)
        {
            Position = position;
            Value = value;
        }

        public Vector(long position)
        {
            Position = position;
        }

        public override int GetHashCode()
        {
            return Position.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is Vector other && this.Position == other.Position;
        }
    }
}
