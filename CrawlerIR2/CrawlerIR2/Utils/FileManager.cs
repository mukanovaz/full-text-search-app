using CrawlerIR2.Models;
using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace CrawlerIR2.Utils
{
    class FileManager
    {
        const string Starage_url = @"..\..\Storage\";
        const string EXTENSION = ".xml";
        private string Last_file_name = "CrawlerIR";

        private static FileManager instance = null;

        private FileManager() {}

        public static FileManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FileManager();
                }
                return instance;
            }
        }

        public string GetDateString(string text, string pattern)
        {
            Match date = Regex.Match(text, pattern);
            string result = date.Length == 0 ? "28.04.2021" : date.Groups["date"].Value;

            result = DateTime.ParseExact(result, "d.M.yyyy",
                CultureInfo.InvariantCulture
            ).ToString("dd.MM.yyyy");

            return result;
        }

        public DateTime GetDateFromString(string text, string pattern)
        {
            Match date = Regex.Match(text, pattern);
            string d = date.Length == 0 ? "28.04.2021" : date.Groups["date"].Value;

            DateTime result = DateTime.ParseExact(d, "d.M.yyyy",
                CultureInfo.InvariantCulture
            );

            return result;
        }

        public string GetString(string text, string pattern)
        {
            Match result = Regex.Match(text, pattern);
            return result.Groups["string"].Value.Trim();
        }

        public Article SaveArticle(Article article, string type)
        {
            SaveArticleTidy(article, type);
            return article;
        }

        public string CleanInvalidXmlChars(string text)
        {
            string re = @"[^\x09\x0A\x0D\x20-\xD7FF\xE000-\xFFFD\x10000-x10FFFF]";
            return Regex.Replace(text, re, "");
        }

        public void SaveArticleTidy(Article article, string key)
        {
            string path = Path.Combine(Environment.CurrentDirectory, Starage_url, Last_file_name + key + EXTENSION);
            XmlDocument doc = new XmlDocument();
            XmlNode articlesNode;

            if (File.Exists(path))
            {
                // Append
                doc.Load(path);
                articlesNode = doc.DocumentElement;
            }
            else
            {
                // Add new
                XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                doc.AppendChild(docNode);

                articlesNode = doc.CreateElement("Articles");
                doc.AppendChild(articlesNode);
            }

            XmlNode articleNode = doc.CreateElement("Article");
            articlesNode.AppendChild(articleNode);

            articleNode.AppendChild(CreateNode(doc, "Title", article.Title));
            articleNode.AppendChild(CreateNode(doc, "Url", article.Url));
            articleNode.AppendChild(CreateNode(doc, "Views", article.Views));
            articleNode.AppendChild(CreateNode(doc, "Category", article.Category));
            articleNode.AppendChild(CreateNode(doc, "Author", article.Author));
            articleNode.AppendChild(CreateNode(doc, "Date", article.Date));

            XmlNode textNode = doc.CreateElement("Text");
            XmlCDataSection CData = doc.CreateCDataSection(article.Text);
            articleNode.AppendChild(textNode);
            textNode.AppendChild(CData);

            XmlNode commentsNode = doc.CreateElement("Comments");
            articleNode.AppendChild(commentsNode);

            if (article.Comments != null)
            {
                foreach (Comment comment in article.Comments)
                {
                    XmlNode commentNode = doc.CreateElement("Comment");
                    commentsNode.AppendChild(commentNode);

                    commentNode.AppendChild(CreateNode(doc, "Id", comment.CommentId.ToString()));
                    commentNode.AppendChild(CreateNode(doc, "Date", comment.DateCreated.ToString()));
                    commentNode.AppendChild(CreateNode(doc, "Author", comment.Author));

                    XmlNode textNode2 = doc.CreateElement("Text");
                    XmlCDataSection CData2 = doc.CreateCDataSection(CleanInvalidXmlChars(comment.Text));
                    commentNode.AppendChild(textNode2);
                    textNode2.AppendChild(CData2);
                }
            }

            doc.Save(path);
        }

        private XmlNode CreateNode(XmlDocument doc, string name, string text)
        {
            XmlNode node = doc.CreateElement(name);
            node.InnerText = text;

            return node;
        }

        private string GetCurrentTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss");
        }
    }
}
