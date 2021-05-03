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

        public Index IndexArticles(List<Article> articles)
        {
            Index = new Index();
            (Index as IIndexer).Index(articles);

            return Index;
        }

        internal List<Article> Search(string query, string dbName)
        {
            return GetResults(query, dbName);
        }

        private List<Article> GetResults(string query, string dbName, decimal top = 10)
        {
            List<Article> articles = new List<Article>();
            List<IResult> results = Index.Search(query);

            for (int i = 0; i < results.Count; i++)
            {
                if (articles.Count == top) break;
                Result res = (Result)results[i];
                string id = res.GetDocumentID();
                using (var context = new Context(dbName))
                {
                    Article article = context.Articles
                                       .Where(s => s.ArticleId.ToString() == id)
                                       .FirstOrDefault<Article>();

                    Article obj = articles.FirstOrDefault(x => x.ArticleId == article.ArticleId);
                    if (obj == null)
                    {
                        article.Text = DataReader.Instance.AddHighlightToText(article.Text, res.StartPosition, res.EndPosition);
                        articles.Add(article);
                    }
                }
            }
            return articles;
        }   
    }
}
