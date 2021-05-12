using CrawlerIR2.Models;
using FullTextSearch.Core.Controllers;
using FullTextSearch.Indexer;
using FullTextSearch.SimpleLogger;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace FullTextSearch.Utils
{
    public class DataReader
    {
        private const string SettingFile = @"crawlers.txt";
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

        public List<Article> ReadData(string path, ref List<Article> articles, DatabaseController databaseController)
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
                    DateTime date = DateTime.ParseExact(comment.SelectSingleNode("Date").InnerText, "dd.MM.yyyy", null); ;
                    comments.Add(new Comment()
                    {
                        Author = comment.SelectSingleNode("Author").InnerText,
                        Text = text_comment,
                        DateCreated = date
                    }); 
                }

                string text = article.SelectSingleNode("Text").InnerText.Replace("\r", string.Empty).Replace("\t", string.Empty);
              
                Article a = new Article()
                {
                    Author = article.SelectSingleNode("Author").InnerText,
                    Category = article.SelectSingleNode("Category").InnerText,
                    Views = article.SelectSingleNode("Views").InnerText,
                    Title = article.SelectSingleNode("Title").InnerText.Replace("\r", string.Empty).Replace("\t", string.Empty),
                    Url = article.SelectSingleNode("Url").InnerText.Replace("\r", string.Empty).Replace("\t", string.Empty),
                    Text = text,
                    Date = article.SelectSingleNode("Date").InnerText,
                    Comments = comments
                };
                try
                {
                    databaseController.AddArticle(a);
                    articles.Add(a);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex.ToString());
                    continue;
                }
            }

            return articles;
        }

        internal string AddHighlightToText(string query, Index index, Article article, IPreprocessing _preprocessing)
        {
            string[] tokens = _preprocessing.ParseTokens(query);
            List<long> start = new List<long>();
            List<long> end = new List<long>();

            foreach (string token in tokens)
            {
                if (token == "and" || token == "or" || token == "or") continue;
                var postings = index.GetPostingsFor(token);
                if (postings == null || !postings.ContainsKey(article.ArticleId))
                {
                    continue;
                }
                Document res = postings[article.ArticleId];
                if (res == null) continue;

                start.AddRange(res.GetPositionStart());
                end.AddRange(res.GetPositionEnd());
            }

            // Sort
            start.Sort();
            end.Sort();

            // Highlight
            for (int i = start.Count - 1; i >= 0; i--)
            {
                article.Text = AddHighlightToText(article.Text, (int) start[i], (int) end[i]);
            }
            return article.Text;
        }

        internal string AddHighlightToText(string text, int start, int end)
        {
            string newText;
            newText = text.Insert(end, "</span>");
            newText = newText.Insert(start, "<span id=\"search\" style=\"background-color: #FFFF00\">");

            return newText;
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

        public string GetValidDBName(string name)
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

        public string[] GetDataSources()
        {
            return File.ReadAllLines(SettingFile);
        }

        public void AddDataSourceToFile(string table_name)
        {
            File.AppendAllLines(SettingFile, new string[] { table_name });
        }

    }
}
