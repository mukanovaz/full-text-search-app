using CrawlerIR2.Indexer;
using CrawlerIR2.Models;
using FullTextSearch.Indexer.Indexer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FullTextSearch.Indexer
{
    public class Index : IIndexer, ISearcher
    {
        #region PRIVATE_VARS
        /// <summary>
        /// Preprocessing instance
        /// </summary>
        private readonly IPreprocessing _preprocessing;
        /// <summary>
        /// Indexed documents
        /// </summary>
        private readonly HashSet<int> _indexedDocuments = new HashSet<int>();
        /// <summary>
        /// Index
        /// <Term, <doc_id, Result>> 
        /// </summary>
        private readonly SortedDictionary<string, Dictionary<int, Document>> _index = new SortedDictionary<string, Dictionary<int, Document>>();

        #endregion

        #region PUBLIC_VARS
        public HashSet<int> IndexedDocuments => _indexedDocuments;
        public SortedDictionary<string, Dictionary<int, Document>> InvertedIndex { get => _index; }
        public int TermsCount { get; private set; } = 0;
        public int DocCount { get; private set; } = 0;
        public IRetrievalModel RetrievalModel { get; set; }
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="preprocessing">Preprocessing instance</param>
        public Index(IPreprocessing preprocessing)
        {
            _preprocessing = preprocessing;
        }

        /// <summary>
        /// Append term to index
        /// </summary>
        /// <param name="term">word</param>
        /// <param name="doc_id">document id</param>
        /// <param name="positionStart">start position in text</param>
        /// <param name="positionEnd">end position in text</param>
        public void Append(string term, int doc_id, int positionStart, int positionEnd)
        {
            // Check if term already exist in Index
            if (_index.ContainsKey(term))
            {
                // Check if word in Index already contains document
                if (_index[term].ContainsKey(doc_id))
                {
                    // Update document
                    Document res = _index[term][doc_id];
                    res.SetPositions(positionStart, positionEnd);
                    res.TF += 1;
                }
                else
                {
                    // Add new document
                    _index[term].Add(doc_id, new Document(doc_id, positionStart, positionEnd));
                }
            }
            else
            {
                // Add new term to Index
                _index.Add(
                    term, new Dictionary<int, Document>() {
                        { doc_id, new Document(doc_id, positionStart, positionEnd) }
                    }
                );
                TermsCount++;
            }
            if (!_indexedDocuments.Contains(doc_id))
            {
                _indexedDocuments.Add(doc_id);
                DocCount++;
            }
        }

        /// <summary>
        /// Update document in index
        /// </summary>
        /// <param name="article"></param>
        public void UpdateDocument(Article article)
        {
            if (article == null) return;

            // Remove old document
            foreach (var entry in _index)
            {
                if (_index[entry.Key].ContainsKey(article.ArticleId))
                {
                    _index[entry.Key].Remove(article.ArticleId);
                    _indexedDocuments.Remove(article.ArticleId);
                }
            }

            // Add new
            IndexOneDocument(article);
        }

        /// <summary>
        /// Get list of documents by term
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public Dictionary<int, Document> GetPostingsFor(string term)
        {
            return !_index.ContainsKey(term)
              ? null
              : _index[term];
        }

        /// <summary>
        /// Search query in index
        /// </summary>
        /// <param name="query">query</param>
        /// <returns></returns>
        public List<IResult> Search(string query)
        {
            if (_preprocessing == null)
            {
                throw new System.ArgumentNullException("Index: preprocessing instance cannot be null");
            }
            if (RetrievalModel == null)
            {
                throw new System.ArgumentNullException("Index: Retrieval Model cannot be null");
            }

            return RetrievalModel.EvaluateResults(query, this);
        }

        /// <summary>
        /// Create index from documents
        /// </summary>
        /// <param name="documents">List of documents</param>
        void IIndexer.Index(List<Article> documents)
        {
            for (int i = 0; i < documents.Count; i++)
            {
                _preprocessing.ParseTokens(documents[i].GetText(), documents[i], this);
            }
        }

        /// <summary>
        /// Add new document to Index
        /// </summary>
        /// <param name="document"></param>
        public void IndexOneDocument(Article document)
        {
            _preprocessing.ParseTokens(document.GetText(), document, this);
        }

        public void Remove(int articleId)
        {
            foreach (var entry in _index)
            {
                if (_index[entry.Key].ContainsKey(articleId))
                {
                    _index[entry.Key].Remove(articleId);
                    _indexedDocuments.Remove(articleId);
                }
            }
        }
    }
}
