using CrawlerIR2.Indexer;
using CrawlerIR2.Models;
using FullTextSearch.Core.Controllers;
using FullTextSearch.Indexer;
using FullTextSearch.Indexer.Indexer.Models;
using FullTextSearch.SimpleLogger;
using FullTextSearch.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace FullTextSearch.Core
{
    class IndexerController
    {
        public Index Index;
        private IPreprocessing _preprocessing;
        private VectorRetrievalModel _vectorRetrievalModel;

        public List<Article> LastArticles { get; private set; }

        public Index IndexArticles(List<Article> articles, IPreprocessing preprocessing, VectorRetrievalModel vectorRetrievalModel)
        {
            _preprocessing = preprocessing; 
            _vectorRetrievalModel = vectorRetrievalModel;
            Logger.Info("IndexerController: start indexing..");
            Index = new Index(_preprocessing);

            (Index as IIndexer).Index(articles);

            Logger.Info("IndexerController: index is ready.");

            var thread = new Thread(CalculateIDF);
            thread.IsBackground = true;
            thread.Start();

            return Index;
        }

        private void IndexDocuments(List<Article> articles)
        {
            (Index as IIndexer).Index(articles);
        }

        private void CalculateIDF()
        {
            _vectorRetrievalModel.Index = Index;
            _vectorRetrievalModel.CalculateIDF();
        }

        internal List<Article> Search(string query,
            IPreprocessing _preprocessing, IRetrievalModel retrievalModel, DatabaseController databaseController)
        {
            Index.RetrievalModel = retrievalModel;
            return GetResults(query, _preprocessing, databaseController);
        }

        private List<Article> GetResults(string query, IPreprocessing _preprocessing, DatabaseController databaseController)
        {
            LastArticles = new List<Article>();
            List<IResult> results = Index.Search(query);

            for (int i = 0; i < results.Count; i++)
            {
                Result res = (Result)results[i];
                string id = res.GetDocumentID();

                // Get article from database
                Article article = databaseController.GetArticleById(Int32.Parse(id));
                if (article == null) return null;
                // Higtlight query text
                article.Text = DataReader.Instance.AddHighlightToText(query, Index, article, _preprocessing);

                LastArticles.Add(article);
            }
            return LastArticles;
        }   
    }
}
