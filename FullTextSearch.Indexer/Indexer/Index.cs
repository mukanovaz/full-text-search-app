using CrawlerIR2.Indexer;
using CrawlerIR2.Models;
using FullTextSearch.Indexer.Indexer.Models;
using FullTextSearch.Indexer.Query;
using System.Collections.Generic;
using System.Linq;

namespace FullTextSearch.Indexer
{
    public class Index : IIndexer, ISearcher
    {
        private LucenePreprocessing _preprocessing;
        private readonly SortedSet<int> _indexedDocuments = new SortedSet<int>();
        public IEnumerable<int> IndexedDocuments => _indexedDocuments;

        private Dictionary<string, List<IResult>> _index = new Dictionary<string, List<IResult>>();

        public void Append(string term, int doc_id)
        {
            IResult result = new Result(doc_id.ToString());
            if (_index.ContainsKey(term))
            {
                _index[term].Add(result);
            }
            else
            {
                _index.Add(term, new List<IResult> { result });
            }

            _indexedDocuments.Add(doc_id);
        }

        public void Append(string term, int doc_id, int positionStart, int positionEnd)
        {
            IResult result = new Result(doc_id.ToString()) { StartPosition = positionStart, EndPosition = positionEnd };
            if (_index.ContainsKey(term))
            {
                _index[term].Add(result);
            }
            else
            {
                _index.Add(term, new List<IResult> { result });
            }

            _indexedDocuments.Add(doc_id);
        }

        public IEnumerable<IResult> GetPostingsFor(string term)
        {
            return !_index.ContainsKey(term)
              ? Enumerable.Empty<IResult>()
              : _index[term];
        }

        public List<IResult> Search(string query)
        {
            if (_preprocessing == null) return null;
            IRetrievalModel retrievalModel = new BooleanRetrievalModel(_preprocessing);
            //IRetrievalModel retrievalModel = query.Contains("AND") || query.Contains("OR") || query.Contains("NOT")
            //    ? new BooleanRetrievalModel(_preprocessing)
            //    : (IRetrievalModel)new VectorRetrievalModel(_preprocessing);

            return retrievalModel.EvaluateResults(query, this);
        }

        void IIndexer.Index(List<Article> documents)
        {
            _preprocessing = new LucenePreprocessing();

            for (int i = 0; i < documents.Count; i++)
            {
                _preprocessing.ParseTokens(documents[i].GetText(), documents[i], this);
            }
        }

    }
}
