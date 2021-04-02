using FullTextSearch.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace FullTextSearch.Utils
{
    class DataReader
    {
        private static DataReader instance = null;
        private static string _path;

        private DataReader() { }

        public static DataReader Instance
        {
            get
            {
                if (instance == null)
                {
                    _path = Path.Combine(Environment.CurrentDirectory, @"Data\", @"CrawlerIR_TidyText.xml");
                    instance = new DataReader();
                }
                return instance;
            }
        }

        public List<Article> ReadData(string path, ref List<Article> articles)
        {
            if (!File.Exists(path)) return null;

            XmlDocument doc = new XmlDocument();
            doc.Load(path);

            foreach (XmlElement article in doc.DocumentElement)
            {
                List<Comment> comments = new List<Comment>();
                foreach (XmlElement comment in article.SelectSingleNode("Comments").ChildNodes)
                {
                    string text_comment = comment.SelectSingleNode("Text").InnerText.Replace("\r", string.Empty).Replace("\t", string.Empty);

                    comments.Add(new Comment()
                    {
                        Author = comment.SelectSingleNode("Author").InnerText,
                        Id = comment.SelectSingleNode("Id").InnerText.Replace("\r", string.Empty).Replace("\t", string.Empty),
                        Text = text_comment,
                        Date = comment.SelectSingleNode("Date").InnerText
                    }); 
                }

                string text = article.SelectSingleNode("Text").InnerText.Replace("\r", string.Empty).Replace("\t", string.Empty);
              
                articles.Add(new Article()
                {
                    Author = article.SelectSingleNode("Author").InnerText,
                    Category = article.SelectSingleNode("Category").InnerText,
                    Views = article.SelectSingleNode("Views").InnerText,
                    Title = article.SelectSingleNode("Title").InnerText.Replace("\r", string.Empty).Replace("\t", string.Empty),
                    Url = article.SelectSingleNode("Url").InnerText.Replace("\r", string.Empty).Replace("\t", string.Empty),
                    Text = text,
                    Date = article.SelectSingleNode("Date").InnerText,
                    Comments = comments
                }); ;
            }

            return articles;
        }

        public Article GetArticle(string url, string title, string info, string text)
        {
            return new Article() { Title = title, Url = url, Date = info, Text = text, Comments = null };
        }

        private string GetDateString(string text, string pattern)
        {
            Match date = Regex.Match(text, pattern);
            string result = date.Length == 0 ? "28.04.2021" : date.Groups["date"].Value;

            result = DateTime.ParseExact(result, "d.M.yyyy",
                CultureInfo.InvariantCulture
            ).ToString("dd.MM.yyyy");

            return result;
        }

        public List<Article> Test()
        {
            List<Article> articles = new List<Article>();

            articles.Add(GetArticle("http1", "Title 1", "Info1", "Věřte nevěřte, první číslo časopisu Motocykl přišlo do českých trafik přesně před třiceti lety."));
            articles.Add(GetArticle("http2", "Title 2", "Info2", "Veletrh Motosalon letos v jeho obvyklé podobě bohužel navštívit nelze, ale všechny novinky 2021 najdete pěkně pohromadě na 28 "));
            articles.Add(GetArticle("http3", "Title 3", "Info3", "Málokdo se v současné době vrací ke kořenům takovým způsobem jako Triumph s novým Speed Triplem 1200 RS."));

            return articles;
        }
    }
}
