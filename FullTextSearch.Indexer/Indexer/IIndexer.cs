using CrawlerIR2.Models;
using System.Collections.Generic;

namespace FullTextSearch.Indexer
{
    public interface IIndexer
    {
        void Index(List<Article> documents);
    }
}
