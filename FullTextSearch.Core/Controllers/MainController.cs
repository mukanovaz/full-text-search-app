using CrawlerIR2.Crawler;
using CrawlerIR2.Indexer;
using CrawlerIR2.Models;
using FullTextSearch.Core.Controllers;
using FullTextSearch.Indexer;
using FullTextSearch.Indexer.Indexer.Models;
using FullTextSearch.SimpleLogger;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace FullTextSearch.Core
{
    public class MainController
    {
        private static MainController _instance = null;

        public List<Article> Articles { get; private set; }
        public static Index Index { get; private set; }

        private static IPreprocessing _preprocessing;

        private VectorRetrievalModel _vectorRetrievalModel;
        private bool _isUsingStemmer = true;

        private static IndexerController _indexerController;
        private static CrawlerController _crawlerController;
        private static DatabaseController _databaseController;

        private MainController() { }

        public static MainController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MainController();
                    _indexerController = new IndexerController();
                    _crawlerController = new CrawlerController();
                    _preprocessing = new LucenePreprocessing();
                }
                return _instance;
            }
        }

        public List<Article> RunSearcher(bool is_boolean, string query)
        {
            if (is_boolean) 
            {
                Logger.Info("MainController: start searching using Boolean Model");
                return _indexerController.Search(query, _preprocessing, new BooleanRetrievalModel(_preprocessing), _databaseController);
            } else
            {
                Logger.Info("MainController: start searching using Vector Model");
                return _indexerController.Search(query, _preprocessing, _vectorRetrievalModel, _databaseController);
            }
        }

        public List<Article> GetResults()
        {
            return _indexerController.LastArticles;
        }

        public List<Article> RunCrawler(bool is_exist, string datasource, ICrawler crawler = null, string db_name = "", BackgroundWorker backgroundWorker = null)
        {
            if (is_exist && db_name == "" && crawler != null && backgroundWorker == null) return null;
            if (!is_exist && db_name != "" && crawler == null && backgroundWorker != null) return null;

            _databaseController = new DatabaseController(db_name);

            if (is_exist)
            {
                Logger.Info("MainController: reading data");
                backgroundWorker.ReportProgress(1);

                // Get data from files and save to database if not exist
                if (datasource == "Motorkaricz")
                {
                    _crawlerController.ReadDataFromDbOrFromFile(_databaseController, 
                        new string[] {
                            @"CrawlerIR_TidyText.xml",
                            @"CrawlerIR_TidyText64.xml",
                            @"CrawlerIR_TidyText128.xml",
                            @"CrawlerIR_TidyText192.xml"
                        });
                } else if (datasource == "Czech data")
                {
                    _crawlerController.ReadDataFromDbOrFromFile(_databaseController,
                        new string[] {
                            @"czechData.xml"
                        });
                }
               
                backgroundWorker.ReportProgress(2);
            }

            Articles = _databaseController.GetAllArticles().ToList();
            if (Articles == null) return null;

            // Index data
            return RunIndexer(Articles);
        }

        /// <summary>
        /// CRUD operations in database
        /// </summary>
        /// <param name="mode">
        ///     0 - create
        ///     1 - read
        ///     2 - update
        ///     3 - delete
        /// </param>
        public Article RunDatabaseUtility(int mode, int articleId, string title = null, string category = null, string author = null, DateTime date = default(DateTime), string newText = null, string views = null)
        {
            if (_databaseController == null)
            {
                return null;
            }

            switch (mode)
            {
                case 0:
                    Logger.Info("MainController: Creating new document in database");
                    Article article = new Article()
                    {
                        Title = title,
                        Category = category,
                        Author = author,
                        DateCreated = date,
                        Date = date.ToString(),
                        Text = newText,
                        Views = views
                    };
                    _databaseController.AddArticle(article);
                    Logger.Info("MainController: Done");
                    return article;
                case 1:
                    Logger.Info("MainController: Get document from database");
                    Logger.Info("MainController: Done");
                    break;
                case 2:
                    Logger.Info("MainController: Updating document in database");
                   
                    Article oldArticle = _databaseController.GetArticleById(articleId);
                    if (oldArticle == null)
                    {
                        return null;
                    }
                    oldArticle.Title = title;
                    oldArticle.Category = category;
                    oldArticle.Author = author;
                    oldArticle.DateCreated = date;
                    oldArticle.Date = date.ToString("dd.MM.yyyy");
                    oldArticle.Text = newText;
                    oldArticle.Views = views;

                    _databaseController.UpdateArticle(oldArticle);

                    Logger.Info("MainController: Done");
                    break;
                case 3:
                    Logger.Info("MainController: Deleting document in database");
                    _databaseController.DeleteArticle(articleId);
                    Logger.Info("MainController: Done");
                    break;
                default:
                    break;
            }
            return null;
        }

        public List<Article> RunIndexer(List<Article> articles)
        {
            Logger.Info("MainController: Indexing data");
            _vectorRetrievalModel = new VectorRetrievalModel(_preprocessing);
            Index = _indexerController.IndexArticles(articles, _preprocessing, _vectorRetrievalModel);

            Logger.Info("MainController: Done");
            return articles;
        }

        /// <summary>
        /// CRUD operations in Index
        /// </summary>
        /// <param name="mode">
        ///     0 - create
        ///     1 - read
        ///     2 - update
        ///     3 - delete
        /// </param>
        /// <param name="articleId"></param>
        public void RunIndexer(int mode, int articleId = 0)
        {
            if (Index == null)
            {
                return;
            }

            switch (mode)
            {
                case 0:
                    Logger.Info("MainController: Creating new document");

                    Logger.Info("MainController: Done");
                    break;
                case 1:
                    break;
                case 2:
                    Logger.Info("MainController: Updating index");
                    Index.IndexOneDocument(_databaseController.GetArticleById(articleId));
                    Logger.Info("MainController: Done");
                    break;
                case 3:
                    Logger.Info("MainController: Deleting document from index");
                    if (articleId == 0 || Index == null)
                    {
                        break;
                    }

                    Index.Remove(articleId);
                    Logger.Info("MainController: Done");
                    break;
                default:
                    break;
            }
        }

        public Index GetIndex()
        {
            return _indexerController.Index;
        }

        public void SetStemmerSetting(bool isStemmer)
        {
            _preprocessing.IsStemerSetting = isStemmer;
        }
    }
}
