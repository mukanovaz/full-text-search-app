using CrawlerIR2.Interface;
using CrawlerIR2.Models;
using FullTextSearch.Interface;
using FullTextSearch.View;
using System.Collections.Generic;
using System.Linq;

namespace FullTextSearch.Model
{
    public class Index : IIndexer, ISearcher
    {
        private readonly SortedSet<int> _indexedDocuments = new SortedSet<int>();
        public IEnumerable<int> IndexedDocuments => _indexedDocuments;

        private Dictionary<string, List<IResult>> _index = new Dictionary<string, List<IResult>>();

        public void Append(string term, int doc_id)
        {
            IResult result = new Result() { DocumentID = doc_id.ToString() };
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

        public void Append(string term, int doc_id, int start, int end)
        {
            IResult result = new Result() { DocumentID = doc_id.ToString(), StartPosition = start, EndPositionPosition = end };
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

        private IEnumerable<IResult> GetPostingsFor(string term)
        {
            return !_index.ContainsKey(term)
              ? Enumerable.Empty<IResult>()
              : _index[term];
        }

        public List<IResult> Search(string query)
        {
            return GetPostingsFor(query).ToList();
        }

        void IIndexer.Index(List<Article> documents)
        {
            LucenePreprocessing preprocessing = new LucenePreprocessing();

            for (int i = 0; i < documents.Count; i++)
            {
                preprocessing.ParseTokens(documents[i].GetText(), documents[i], this);
            }
        }

    }
}
