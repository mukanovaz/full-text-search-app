using CrawlerIR2.Models;
using FullTextSearch.Indexer;
using System.Collections.Generic;

namespace FullTextSearch.Core
{
    class IndexerController
    {
        public Index IndexArticles(List<Article> articles)
        {
            Index index = new Index();
            (index as IIndexer).Index(articles);

            return index;
        }
    }
}
