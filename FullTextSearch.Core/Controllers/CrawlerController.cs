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
        private const string MotorkariData1 = @"CrawlerIR_Html.xml";
        private const string MotorkariData2 = @"CrawlerIR_Html64.xml";
        private const string MotorkariData3 = @"CrawlerIR_Html128.xml";
        private const string MotorkariData4 = @"CrawlerIR_Html192.xml";

        private const string MotorkariData11 = @"CrawlerIR_TidyText.xml";
        private const string MotorkariData22 = @"CrawlerIR_TidyText64.xml";
        private const string MotorkariData33 = @"CrawlerIR_TidyText128.xml";
        private const string MotorkariData44 = @"CrawlerIR_TidyText192.xml";

        public List<Article> GetDataFromFiles(IEnumerable<Article> articles, Controllers.DatabaseController _databaseController)
        {
            List<Article> list = new List<Article>();
            if (articles == null || articles.Count() == 0)
            {
                DataReader.Instance.ReadData(Path.Combine(Environment.CurrentDirectory, DataFolder, MotorkariData11), ref list, _databaseController);
                //DataReader.Instance.ReadData(Path.Combine(Environment.CurrentDirectory, DataFolder, MotorkariData22), ref list, _databaseController);
                //DataReader.Instance.ReadData(Path.Combine(Environment.CurrentDirectory, DataFolder, MotorkariData33), ref list, _databaseController);
                //DataReader.Instance.ReadData(Path.Combine(Environment.CurrentDirectory, DataFolder, MotorkariData44), ref list, _databaseController);

                //DataReader.Instance.ReadData(Path.Combine(Environment.CurrentDirectory, DataFolder, MotorkariData1), ref list, _databaseController);
                //DataReader.Instance.ReadData(Path.Combine(Environment.CurrentDirectory, DataFolder, MotorkariData2), ref list, _databaseController);
                //DataReader.Instance.ReadData(Path.Combine(Environment.CurrentDirectory, DataFolder, MotorkariData3), ref list, _databaseController);
                //DataReader.Instance.ReadData(Path.Combine(Environment.CurrentDirectory, DataFolder, MotorkariData4), ref list, _databaseController);
            }
            else
            {
                list = articles.ToList();
            }

            return list;
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
