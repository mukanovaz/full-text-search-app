using CrawlerIR2.Crawler;
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
    class Controller
    {
        private static Controller _instance = null;
        public Index Index { get; set; }

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

        public List<Article> GetDataFromFilesAndIndex(string db_name)
        {
            List<Article> list = new List<Article>();
            Context context = new Context(db_name);
            if (context.Articles == null || context.Articles.Count() == 0)
            {
                DataReader.Instance.ReadData(Path.Combine(Environment.CurrentDirectory, @"Data\", @"CrawlerIR_Html.xml"), ref list, db_name);
                //DataReader.Instance.ReadData(Path.Combine(Environment.CurrentDirectory, @"Data\", @"CrawlerIR_Html64.xml"), ref list, table_name);
                //DataReader.Instance.ReadData(Path.Combine(Environment.CurrentDirectory, @"Data\", @"CrawlerIR_Html128.xml"), ref list, table_name);
                //DataReader.Instance.ReadData(Path.Combine(Environment.CurrentDirectory, @"Data\", @"CrawlerIR_Html192.xml"), ref list, table_name);
            }
            else
            {
                list = context.Articles.ToList();
            }

            Index = new Index();
            (Index as IIndexer).Index(list);

            return list;
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
