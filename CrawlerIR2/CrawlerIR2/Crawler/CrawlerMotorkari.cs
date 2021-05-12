using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using CrawlerIR2.Models;
using CrawlerIR2.Utils;

namespace CrawlerIR2.Crawler
{
    public class CrawlerMotorkari : ICrawler
    {
        const string Base_url = "https://www.motorkari.cz/clanky/";
        const string page_url = "https://www.motorkari.cz/clanky/?pgr=";

        const int First_page = 1;
        const int Last_page = 256;

        private string Type = "_TidyText";


        public List<Article> GetArticles()
        {
            List<Article> articles = new List<Article>();
            HtmlWeb web = new HtmlWeb();

            for (int i = First_page; i < Last_page; i++)
            {
                if (i % 64 == 0) Type = "_TidyText" + i.ToString();

                Console.WriteLine("=================== " + i.ToString() + " ===================");
                HtmlDocument document = web.Load(page_url + i.ToString());
                HtmlNode[] nodes = document.DocumentNode.SelectNodes("//ul[@class='list']/li").ToArray();

                foreach (HtmlNode item in nodes)
                {
                    if (item.GetAttributeValue("class", string.Empty) == "flash-new")
                    {
                        continue;
                    }

                    if (item.SelectSingleNode(".//div[@class='parameters']//tr//a") == null)
                    {
                        continue;
                    }

                    try
                    {
                        articles.Add(ProcessOneArticle(item));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ERROR: " + ex.ToString());
                    }
                }
            }

            return articles;
        }

        public List<Comment> GetComments(string url)
        {
            List<Comment> comments = new List<Comment>();

            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load(url + "?diskuze=1");
            HtmlNode div = document.DocumentNode.SelectSingleNode("//div[@class='comments-list']");

            if (div == null)
            {
                return null;
            }

            HtmlNode[] com = div.SelectNodes(".//div[@class='comment']").ToArray();
            foreach (HtmlNode item in com)
            {
                comments.Add(ProcessOneComment(item, url + "?diskuze=1"));
            }

            return comments;
        }

        public Article ProcessOneArticle(HtmlNode node)
        {
            string category = FileManager.Instance.GetString(node.InnerText.Replace("\t", string.Empty).Replace("\r", string.Empty), @"Kategorie:(?<string>[\s]*[^\n]*)");
            string articleUrl = "https:" + node.SelectSingleNode(".//h3/a/@href").GetAttributeValue("href", string.Empty);
            string articleTitle = node.SelectSingleNode(".//h3/a/@href").InnerText;
            Article res;

            // Load article page
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load(articleUrl);
            HtmlNode article = document.DocumentNode.SelectSingleNode("//article");

            string page = document.DocumentNode.InnerText.Replace("\t", string.Empty).Replace("\r", string.Empty);
            string views = FileManager.Instance.GetString(page, @"Zobrazeno: (?<string>[^x]*)");
            string author = FileManager.Instance.GetString(page, @"Text:[\s]*(?<string>[^\|]*) ");
            string date = FileManager.Instance.GetDateString(page, @"Zveřejněno: (?<date>\d{1,2}.\d{1,2}.\d{4})");

            // Extract text
            HtmlNode anotace = article.SelectSingleNode("//article/div[@class='anotace']");
            HtmlNode content = article.SelectSingleNode("//article/div[@class='content']");

            Console.WriteLine("\tINFO: " + date + "\t" + articleUrl);

            // Get comments
            List<Comment> comments = GetComments(articleUrl);

            // Build text
            string text = BuildText(articleTitle, category, author, date, views, anotace.InnerText, content.InnerText, comments);

            try
            {
                res = FileManager.Instance.SaveArticle(new Article()
                {
                    Author = author,
                    Date = date,
                    Title = articleTitle,
                    Views = views,
                    Text = text
                   .Replace("\r", string.Empty)
                   .Replace("\t", string.Empty)
                   .Replace("\n\n", "\n")
                   .Replace("&nbsp;", string.Empty)
                   .Replace("0x0B", string.Empty).Trim(),
                    Comments = comments,
                    Category = category,
                    Url = articleUrl
                }, Type);
            }
            catch
            {
                res = null;
            }
            return res;
        }

        public string BuildText(string title, string category, string author, string date, string views, string anotace, string content, List<Comment> comments)
        {
            string newText = $"<h1>{title}</h1>\n";
            newText += $"<b>Text:</b> {author}\n";
            newText += $"<b>Zveřejněno:</b> {date}\n";
            newText += $"<b>Zobrazeno:</b> {views}\n";
            newText += $"<b>Kategorie:</b> {category}\n";
            newText += $"<blockquote>{anotace}</blockquote>";
            newText += $"<br/><br/>";
            newText += content;
            newText += $"<br/><br/>";

            newText += "<h2>Komentáře</h2>" + "\n";
            // Add comments
            if (comments == null) return newText;

            foreach (Comment comment in comments)
            {
                newText += $"<strong>{comment.Author}</strong> napsal {comment.DateCreated}\n";
                newText += $"<blockquote>{comment.Text}</blockquote>";
            }
            return newText;
        }

        public string BuildText(string title, string category, string author, string date, string views, string content)
        {
            string newText = $"<h1>{title}</h1>\n";
            newText += $"<b>Text:</b> {author}\n";
            newText += $"<b>Zveřejněno:</b> {date}\n";
            newText += $"<b>Zobrazeno:</b> {views}\n";
            newText += $"<b>Kategorie:</b> {category}\n";
            //newText += $"<blockquote>{anotace}</blockquote>";
            //newText += $"<br/><br/>";
            newText += content;

            return newText;
        }

        public Comment ProcessOneComment(HtmlNode item, string url)
        {
            string author = item.SelectSingleNode(".//a[@class='user']").InnerText;
            DateTime date = FileManager.Instance.GetDateFromString(item.InnerText, @"napsal (?<date>\d{1,2}.\d{1,2}.\d{4})"); ;
            string id = item.GetAttributeValue("id", string.Empty);
            string text = item.SelectSingleNode(".//p").InnerText;
            return new Comment()
            {
                Author = author,
                DateCreated = date,
                Text = text.Replace("\r", string.Empty).Replace("\t", string.Empty).Replace("\n\n", "\n").Replace("&nbsp;", "").Trim(),
                ArticleId = Int32.Parse(id),
                Url = url
            };
        }
    }
}
