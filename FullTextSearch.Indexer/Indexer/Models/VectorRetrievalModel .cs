using CrawlerIR2.Indexer;
using FullTextSearch.SimpleLogger;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace FullTextSearch.Indexer.Indexer.Models
{
    public class VectorRetrievalModel : IRetrievalModel
    {
        private Index _index;
        /// <summary>
        /// IDF
        /// </summary>
        private Dictionary<string, double> _termsVectors; 
        private Dictionary<long, Vector> _queryVector;
        private ConcurrentDictionary<long, Dictionary<long, Vector>> _documentVectors;

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
            // Get query tokens
            int document_count = _index.DocCount;

            Logger.Info("VectorRetrievalModel: Calculating IDF");
            CalculateIDF();

            Logger.Info("VectorRetrievalModel: Tranforming query to vector");
            Dictionary<string, int> queryDictionary = CreateQueryDictionary(Preprocessing.ParseTokens(query));
            _queryVector = TransformToQueryVector(queryDictionary, _index.TermsCount);

            Logger.Info("VectorRetrievalModel: Calculating cosine similarity");

            // Get document vector and find cos similarity
            List<IResult> results = null;
            if (_documentVectors == null)
            {
                _documentVectors = new ConcurrentDictionary<long, Dictionary<long, Vector>>();
                results = GetDocumentVectorsAndCosSimilarity();
            } else
            {
                results = GetDocumentVectorsAndCosSimilarity(isNewVector: false);
            }

            var orderByDescendingResult = from s in results
                                          orderby s.GetScore() descending
                                          select s;
            Logger.Info("VectorRetrievalModel: Done");
            // Sort
            return orderByDescendingResult.ToList();
        }

        private List<IResult> GetDocumentVectorsAndCosSimilarity(bool isNewVector = true, bool isNewCos = true)
        {
            var results = new List<IResult>();
            foreach (var document in _index.IndexedDocuments)
            {
                Dictionary<long, Vector> docVector;
                if (isNewVector)
                {
                    docVector = TransformToDocumentVectors(document);
                    // Save whole vector to dictionary
                    _documentVectors.TryAdd(document, docVector);
                } else
                {
                    docVector = _documentVectors[document];
                }

                if (isNewCos)
                {
                    // Find cos similarity
                    double cosSim = GetCos(docVector, _queryVector);
                    results.Add(new Result(document.ToString()) { Score = cosSim });
                }
                
            }
            return results;
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
            int word_index = 0;
            var vector = new Dictionary<long, Vector>();
            foreach (KeyValuePair<string, Dictionary<int, Document>> entry in _index.InvertedIndex)
            {
                if (entry.Value.ContainsKey(document))
                {
                    // Check if TF is not 0
                    double tf_wght = entry.Value[document].TF > 0 ? 1 + Math.Log10(entry.Value[document].TF) : 0;
                    double tf_idf = tf_wght * _termsVectors[entry.Key];
                        
                    // Save vector value
                    vector.Add(word_index, new Vector(word_index, tf_idf));
                }
                word_index++;
            }
            return vector;
        }

        /// <summary>
        /// Calculate IDF value
        /// </summary>
        /// <param name="documentCount">Documents count</param>
        public void CalculateIDF()
        {
            if (_termsVectors == null)
            {
                _termsVectors = new Dictionary<string, double>();
            } else
            {
                return;
            }

            foreach (KeyValuePair<string, Dictionary<int, Document>> entry in _index.InvertedIndex)
            {
                double tmp = (double)_index.DocCount / (double)entry.Value.Count;
                double idf = Math.Log10(tmp);
                _termsVectors.Add(entry.Key, idf);
            }

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
