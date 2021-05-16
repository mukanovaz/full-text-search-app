using CrawlerIR2.Crawler;
using CrawlerIR2.Models;
using FullTextSearch.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FullTextSearch.Core
{
    class CrawlerController
    {
        private const string DataFolder = @"Data\";

        public void ReadDataFromDbOrFromFile(Controllers.DatabaseController _databaseController, string[] pathes)
        {
            IEnumerable<Article> articles = _databaseController.GetAllArticles();
            if (articles == null || articles.Count() == 0)
            {
                foreach (string path in pathes)
                {
                    DataReader.Instance.ReadArticlesFromXml(Path.Combine(Environment.CurrentDirectory, DataFolder, path), _databaseController);
                }
            }
        }

        public List<Article> GetDataFromWeb(ICrawler crawler, Controllers.DatabaseController _databaseController)
        {
            List<Article> articles = crawler?.GetArticles();
            foreach (Article article in articles)
            {
                _databaseController.AddArticle(article);
            }
            return articles;
        }

    }
}
