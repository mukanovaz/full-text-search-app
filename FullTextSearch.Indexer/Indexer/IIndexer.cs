using CrawlerIR2.Interface;
using CrawlerIR2.Models;
using System.Collections.Generic;

namespace FullTextSearch.Indexer
{
    public interface IIndexer
    {
        void Index(List<Article> documents);
        void Append(string term, int doc_id, int positionStart, int positionEnd);
    }
}
