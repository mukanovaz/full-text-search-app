using CrawlerIR2.Indexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullTextSearch.Indexer.Query
{
    class EvaluateTerms
    {
        private Index _index;
        private IPreprocessing _preprocessing;
        private string _documentID;

        public EvaluateTerms(Index index, IPreprocessing preprocessing, string documentID)
        {
            _index = index; 
            _preprocessing = preprocessing;
            _documentID = documentID;
        }

        public bool Evaluate(string query)
        {
            string[] tokens = _preprocessing.ParseTokens(query);

            if (tokens.Length > 1)
            {
                return false;
            }

            Dictionary<int, Document> documents = _index.GetPostingsFor(tokens[0]);
            return documents == null ? false : documents.ContainsKey(Int32.Parse(_documentID));
        }
    }
}
