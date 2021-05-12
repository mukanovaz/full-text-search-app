using CrawlerIR2.Indexer;
using CrawlerIR2.Models;
using FullTextSearch.Indexer;
using FullTextSearch.Indexer.Indexer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace FullTextSearch.Tests
{
    [TestClass]
    public class VectorRetrievalModelTest
    {
        private Index _index;
        private VectorRetrievalModel _vectorRetrievalModel;

        private void CreateNewInstance()
        {
            LucenePreprocessing _preprocessing = new LucenePreprocessing();
            _vectorRetrievalModel = new VectorRetrievalModel(_preprocessing);
            _index = new Index(_preprocessing);
        }

        [TestMethod]
        public void TestCosineSimilarity()
        {
            CreateNewInstance();
            string query = "krásné město";

            _index.RetrievalModel = _vectorRetrievalModel;

            (_index as IIndexer).Index(new List<Article>() {
                new Article() { ArticleId = 1, Text = "Plzeň je krásné město a je to krásné místo" },
                new Article() { ArticleId = 2, Text = "Ostrava je ošklivé místo" },
                new Article() { ArticleId = 3, Text = "Praha je také krásné město Plzeň je hezčí" }
            });

            List<IResult> results = _index.Search(query);

            Assert.AreEqual(0.751098433, results[0].GetScore(), 0.00000001);
            Assert.AreEqual(0.282705044, results[1].GetScore(), 0.00000001);
            Assert.AreEqual(0, results[2].GetScore());
        }

        [TestMethod]
        public void TestCosineSimilarity2()
        {
            CreateNewInstance();
            string query = "tropical fish sea";

            _index.RetrievalModel = _vectorRetrievalModel;

            (_index as IIndexer).Index(new List<Article>() {
                new Article() { ArticleId = 1, Text = "tropical fish include fish found in tropical enviroments" },
                new Article() { ArticleId = 2, Text = "fish live in a sea" },
                new Article() { ArticleId = 3, Text = "tropical fish are popular aquarium fish" },
                new Article() { ArticleId = 4, Text = "fish also live in Czechia" },
                new Article() { ArticleId = 5, Text = "Czechia is a country" }
            });

            List<IResult> results = _index.Search(query);

            Assert.AreEqual(0.20086059, results[1].GetScore(), 0.00000001);      // 1
            Assert.AreEqual(0.731774019, results[0].GetScore(), 0.00000001);     // 2
            Assert.AreEqual(0.164417883, results[2].GetScore(), 0.00000001);     // 3
            Assert.AreEqual(0.012472604, results[3].GetScore(), 0.00000001);     // 4
            Assert.AreEqual(0, results[4].GetScore());                           // 5
        }
    }
}
