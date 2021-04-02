using FullTextSearch.Model;
using Nest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullTextSearch.Utils
{
    class ElasticSearchInstance
    {
        private List<string> DataPaths = new List<string>() {
            @"Data\", @"CrawlerIR_TidyText.xml",
            @"Data\", @"CrawlerIR_TidyText2.xml",
            @"Data\", @"CrawlerIR_TidyText3.xml"
        };
        private List<Article> Documents;
        public ElasticClient Client;
        private string IndexName;

        public ElasticSearchInstance (string indexName)
        {
            IndexName = indexName;
            Documents = new List<Article>();
            ConnectionSettings settings = new ConnectionSettings(new Uri("http://localhost:9200"));
            settings.ThrowExceptions(alwaysThrow: true);
            settings.PrettyJson();

            Client = new ElasticClient(settings);
        }

        public bool Ping()
        {
            // Check client working
            PingResponse ping = Client.Ping();
            return ping.IsValid;
        }

        public void FillData()
        {
            // Read data 
            foreach (string path in DataPaths)
            {
                DataReader.Instance.ReadData(Path.Combine(Environment.CurrentDirectory, @"Data\", path), ref Documents);
            }
            AddManyDocument(Documents);
        }

        public bool CreateIndexWithMapping(string indexName)
        {
            // Create index with mappings
            var createIndexResponse = Client.Indices.Create(IndexName, c => c
                .Map<Article>(m => m.AutoMap())
            );
            return createIndexResponse.IsValid;
        }

        public string AddOneDocument(Article article)
        {
            var indexResponse = Client.Index(article, i => i.Index(IndexName));
            return indexResponse == null ? "Error" : indexResponse.Id;
        }

        public bool AddManyDocument(List<Article> list)
        {
            var indexManyResponse = Client.IndexMany(list.ToArray(), IndexName);
            return indexManyResponse.IsValid;
        }

        public bool DeleteDocument(string id)
        {
            var deleteResponse = Client.Delete<Article>(id, i => i.Index(IndexName));
            return deleteResponse.IsValid;
        }

        public Article GetDocumentById(string id)
        {
            var getResponse = Client.Get<Article>(id, g => g.Index(IndexName));
            return getResponse.Source;
        }

        public async void UpdateDocumentById(string id)
        {
            dynamic updateDoc = new Article();
            updateDoc.Title = "My new title";

            await Client.UpdateAsync<Article, dynamic>(new DocumentPath<Article>(id), u => u.Index(IndexName).Doc(updateDoc));
        }

        public List<Article> Search(string word)
        {
            var searchResponse = Client.Search<Article>(s => s.Index(IndexName)
                .Query(q => q
                    .MatchAll()
                )
            );
            return searchResponse.Documents.ToList();
        }
    }
}
