using CrawlerIR2.Crawler;
using CrawlerIR2.Models;
using FullTextSearch.Indexer;
using System.Collections.Generic;
using System.ComponentModel;

namespace FullTextSearch.Core
{
    public class MainController
    {
        private static MainController _instance = null;

        public List<Article> Articles { get; private set; }
        private Index Index;
        private static IndexerController IndexerController;
        private static CrawlerController CrawlerController;

        private MainController() { }

        public static MainController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MainController();
                    IndexerController = new IndexerController();
                    CrawlerController = new CrawlerController();
                }
                return _instance;
            }
        }

        public void RunSearcher(bool is_vector, string query)
        {

        }

        public List<Article> RunCrawler(bool is_exist, ICrawler crawler = null, string db_name = "", BackgroundWorker backgroundWorker = null)
        {
            if (is_exist && db_name == "" && crawler != null && backgroundWorker == null) return null;
            if (!is_exist && db_name != "" && crawler == null && backgroundWorker != null) return null;

            if (is_exist)
            {
                backgroundWorker.ReportProgress(1);
                // Get data from files and save to database if not exist
                Articles = CrawlerController.GetDataFromFiles(db_name);
                backgroundWorker.ReportProgress(2);
            }
            else
            {
                // Run crawler
                Articles = CrawlerController.GetDataFromWeb(crawler);
            }

            if (Articles == null) return null;

            // Index data
            return RunIndexer(Articles);
        }

        public List<Article> RunIndexer(List<Article> articles)
        {
            Index = IndexerController.IndexArticles(articles);

            return articles;
        }
    }
}
