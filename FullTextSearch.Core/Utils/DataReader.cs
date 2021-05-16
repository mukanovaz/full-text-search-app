using CrawlerIR2.Models;
using FullTextSearch.Core.Controllers;
using FullTextSearch.Indexer;
using FullTextSearch.SimpleLogger;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace FullTextSearch.Utils
{
    public class DataReader
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

        public void ReadArticlesFromXml(string path, DatabaseController databaseController)
        {
            if (!File.Exists(path)) return;
            List<Article> articles = new List<Article>();
            int ind = 0;
            XmlDocument doc = new XmlDocument();
            doc.Load(path);

            foreach (XmlElement article in doc.DocumentElement)
            {
                List<Comment> comments = new List<Comment>();
                // Get comments node
                XmlNode com = article.SelectSingleNode("Comments");
                if (com != null)
                {
                    foreach (XmlElement comment in com.ChildNodes)
                    {
                        string text_comment = comment.SelectSingleNode("Text").InnerText.Replace("\r", string.Empty).Replace("\t", string.Empty);
                        DateTime date = DateTime.ParseExact(comment.SelectSingleNode("Date").InnerText, "dd.MM.yyyy", null); 
                        comments.Add(new Comment()
                        {
                            Author = comment.SelectSingleNode("Author").InnerText,
                            Text = text_comment,
                            DateCreated = date
                        });
                    }
                }
                XmlNode textNode = article.SelectSingleNode("Text");
                XmlNode authorNode = article.SelectSingleNode("Author");
                XmlNode categoryNode = article.SelectSingleNode("Category");
                XmlNode viewsNode = article.SelectSingleNode("Views");
                XmlNode titleNode = article.SelectSingleNode("Title");
                XmlNode urlNode = article.SelectSingleNode("Url");
                XmlNode dateStrNode = article.SelectSingleNode("Date");

                string text = textNode != null ? textNode.InnerText.Replace("\r", string.Empty)
                                                                   .Replace("\t", string.Empty) : "";
                string author = authorNode != null ? authorNode.InnerText : "";
                string category = categoryNode != null ? categoryNode.InnerText : "";
                string views = viewsNode != null ? viewsNode.InnerText : "";
                string title = titleNode != null ? titleNode.InnerText.Replace("\r", string.Empty)
                                                                      .Replace("\t", string.Empty) : "";
                string url = urlNode != null ? urlNode.InnerText.Replace("\r", string.Empty)
                                                                .Replace("\t", string.Empty) : "";
                string dateStr = dateStrNode != null ? dateStrNode.InnerText : "";

                Article a = new Article()
                {
                    Author = author,
                    Category = category,
                    Views = views,
                    Title = title,
                    Url = url,
                    Text = text,
                    Date = dateStr,
                    Comments = comments
                };
                articles.Add(a);
                ind++;
            }
            try
            {
                databaseController.AddArticles(articles);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
            }
        }

        internal void ReadArticlesFromFile(string path, ref List<Article> list, DatabaseController databaseController)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            List<Article> objnew = (List<Article>)formatter.Deserialize(stream);

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
            var startDescendingOrder = start.AsParallel().OrderByDescending(i => i).ToArray();
            var endDescendingOrder = end.AsParallel().OrderByDescending(i => i).ToArray();

            // Highlight
            Parallel.ForEach(startDescendingOrder, (line, state, idx) =>
            {
                article.Text = AddHighlightToText(article.Text, (int)startDescendingOrder[idx], (int)endDescendingOrder[idx]);
            });
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
    }
}
