using CrawlerIR2.Models;
using FullTextSearch.Indexer;
using System;
using System.Collections.Generic;

namespace FullTextSearch.Core
{
    class IndexerController
    {
        private Index Index;

        public Index IndexArticles(List<Article> articles)
        {
            Index = new Index();
            (Index as IIndexer).Index(articles);

            return Index;
        }

        internal void Search(string query)
        {
            Index.Search(query);
        }
    }
}
