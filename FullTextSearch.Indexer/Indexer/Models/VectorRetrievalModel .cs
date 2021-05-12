using CrawlerIR2.Indexer;
using FullTextSearch.SimpleLogger;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FullTextSearch.Indexer.Indexer.Models
{
    public class VectorRetrievalModel : IRetrievalModel
    {
        /// <summary>
        /// Preprocessing instance
        /// </summary>
        public IPreprocessing Preprocessing { get; }
        private Index _index;

        private Dictionary<string, double> _termsVectors;
        private double[] _queryVector;
        
        public Index Index { get;  set; }

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
            GetQueryVectors(queryDictionary, _index.TermsCount);

            Logger.Info("VectorRetrievalModel: Calculating cosine similarity");
            List<IResult> cos = TransformToDocumentVectors(_index.TermsCount);

            var orderByDescendingResult = from s in cos
                                          orderby s.GetScore() descending
                                          select s;
            Logger.Info("VectorRetrievalModel: Done");
            // Sort
            return orderByDescendingResult.ToList();
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
        private void GetQueryVectors(Dictionary<string, int> tokens, int terms_count)
        {
            _queryVector = new double[terms_count];
            int word_index = 0;
            foreach (KeyValuePair<string, Dictionary<int, Document>> entry in _index.InvertedIndex)
            {
                if (tokens.ContainsKey(entry.Key))
                {
                    double tf_wght = 1 + Math.Log10(tokens[entry.Key]);
                    double tf_idf = tf_wght * _termsVectors[entry.Key];
                    _queryVector[word_index] = tf_idf;
                } else
                {
                    _queryVector[word_index] = 0;
                }
                word_index++;
            }
        }

        /// <summary>
        /// Traverse list of documents and create documents vectors
        /// </summary>
        private List<IResult> TransformToDocumentVectors(int terms_count)
        {
            List<IResult> results = new List<IResult>();

            foreach (var document in _index.IndexedDocuments)
            {
                int word_index = 0;
                double[] vector = new double[terms_count];
                // TODO: parallel
                foreach (KeyValuePair<string, Dictionary<int, Document>> entry in _index.InvertedIndex)
                {
                    if (entry.Value.ContainsKey(document))
                    {
                        double tf_wght = 1 + Math.Log10(entry.Value[document].TF);
                        double tf_idf = tf_wght * _termsVectors[entry.Key];
                        vector[word_index] = tf_idf;
                    }
                    word_index++;
                }
                // Find cos similarity
                double cosSim = GetCos(vector, _queryVector);
                results.Add(new Result(document.ToString()) { Score = cosSim });
            }
            return results;
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
            
            lock (Index.InvertedIndex)
            {
                foreach (KeyValuePair<string, Dictionary<int, Document>> entry in Index.InvertedIndex)
                {
                    double tmp = (double)Index.DocCount / (double)entry.Value.Count;
                    double idf = Math.Log10(tmp);
                    _termsVectors.Add(entry.Key, idf);
                }
            }
            
        }

        /// <summary>
        /// Calculate cosine similarity
        /// </summary>
        /// <param name="docname"></param>
        /// <param name="queryname"></param>
        /// <returns></returns>
        internal double GetCos(double[] docVectror, double[] queryVector)
        {
            if (docVectror.Length == 0 || queryVector.Length == 0)
            {
                throw new Exception("Cosine Distance: arrays cannot be zero length");
            }

            if (docVectror.Length != queryVector.Length)
            {
                throw new Exception("Cosine Distance: Vectors must be same length");
            }

            // Compute scalar
            double scalar = 0;
            for (int i = 0; i < docVectror.Length; i++)
            {
                scalar += docVectror[i] * queryVector[i];
            }

            // Compute document vector norm
            double docVecNorm = 0;
            for (int i = 0; i < docVectror.Length; i++)
            {
                docVecNorm += Math.Pow(docVectror[i], 2);
            }
            docVecNorm = Math.Sqrt(docVecNorm);

            // Compute query vector norm
            double queryVecNorm = 0;
            for (int i = 0; i < queryVector.Length; i++)
            {
                queryVecNorm += Math.Pow(queryVector[i], 2);
            }
            queryVecNorm = Math.Sqrt(queryVecNorm);

            // Cosine similarity
            return scalar / (Math.Sqrt(docVecNorm) * Math.Sqrt(queryVecNorm)); ;
        }
    }
}
