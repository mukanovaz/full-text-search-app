using CrawlerIR2.Indexer;
using CrawlerIR2.Models;
using FullTextSearch.Indexer;
using FullTextSearch.Utils;
using System.Collections.Generic;
using System.Linq;

namespace FullTextSearch.Core
{
    class IndexerController
    {
        private Index Index;

        public Index IndexArticles(List<Article> articles, LucenePreprocessing _preprocessing)
        {
            Index = new Index(_preprocessing);
            (Index as IIndexer).Index(articles);

            return Index;
        }

        internal List<Article> Search(string query, string dbName, LucenePreprocessing _preprocessing)
        {
            return GetResults(query, dbName, _preprocessing);
        }

        private List<Article> GetResults(string query, string dbName, LucenePreprocessing _preprocessing,  decimal top = 10)
        {
            List<Article> articles = new List<Article>();
            List<IResult> results = Index.Search(query);

            for (int i = 0; i < results.Count; i++)
            {
                // Break if top 
                if (articles.Count == top) break;

                Result res = (Result)results[i];
                string id = res.GetDocumentID();

                // Get article from database
                using (var context = new Context(dbName))
                {
                    Article article = context.Articles
                                       .Where(s => s.ArticleId.ToString() == id)
                                       .FirstOrDefault<Article>();

                    // Higtlight query text
                    article.Text = DataReader.Instance.AddHighlightToText(query, Index, article, _preprocessing);

                    articles.Add(article);
                }
            }
            return articles;
        }   
    }
}
