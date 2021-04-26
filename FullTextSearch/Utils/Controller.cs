using CrawlerIR2.Crawler;
using CrawlerIR2.Interface;
using CrawlerIR2.Models;
using FullTextSearch.Interface;
using FullTextSearch.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullTextSearch.Utils
{
    public class Controller
    {
        private static Controller _instance = null;
        public Index Index { get; set; }

        public string TableName { get; set; }

        private Controller() {}

        public static Controller Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Controller();
                }
                return _instance;
            }
        }

        public List<Article> RunCrawler(ICrawler crawler)
        {
            return crawler.GetArticles();
        }

        public List<Article> GetDataFromFilesAndIndex(string db_name, ref System.ComponentModel.BackgroundWorker backgroundWorker)
        {
            backgroundWorker.ReportProgress(1);

            TableName = db_name;
            List<Article> list = new List<Article>();
            Context context = new Context(TableName);
            if (context.Articles == null || context.Articles.Count() == 0)
            {
                DataReader.Instance.ReadData(Path.Combine(Environment.CurrentDirectory, @"Data\", @"CrawlerIR_Html.xml"), ref list, TableName);
                //DataReader.Instance.ReadData(Path.Combine(Environment.CurrentDirectory, @"Data\", @"CrawlerIR_Html64.xml"), ref list, TableName);
                //DataReader.Instance.ReadData(Path.Combine(Environment.CurrentDirectory, @"Data\", @"CrawlerIR_Html128.xml"), ref list, TableName);
                //DataReader.Instance.ReadData(Path.Combine(Environment.CurrentDirectory, @"Data\", @"CrawlerIR_Html192.xml"), ref list, TableName);
            }
            else
            {
                list = context.Articles.ToList();
            }

            backgroundWorker.ReportProgress(2);
            Index = new Index();
            (Index as IIndexer).Index(list);

            return list;
        }

        public List<Article> BooleanModelSearch(string query, decimal top)
        {
            List<Article> articles = new List<Article>();
            List<IResult> results = Controller.Instance.Index.Search(query);

            for (int i = 0; i < results.Count; i++)
            {
                if (articles.Count == top) break;
                Result res = (Result)results[i];
                string id = res.GetDocumentID();
                using (var context = new Context(Controller.Instance.TableName))
                {
                    Article article = context.Articles
                                       .Where(s => s.ArticleId.ToString() == id)
                                       .FirstOrDefault<Article>();

                    Article obj = articles.FirstOrDefault(x => x.ArticleId == article.ArticleId);
                    if (obj == null)
                    {
                        article.Text = DataReader.Instance.AddHighlightToText(article.Text, res.StartPosition, res.EndPositionPosition);
                        articles.Add(article);
                    }
                }
            }
            return articles;
        }

        public string GetValidDBName (string name)
        {
            return RemoveDiacritics(name)
                .Replace(".", string.Empty)
                .Replace(",", string.Empty)
                .Replace(@"\", string.Empty)
                .Replace("/", string.Empty);
        }

        private string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
