using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using System.Threading.Tasks;
using CrawlerIR2.Crawler;
using CrawlerIR2.Models;
using CrawlerIR2.Utils;

namespace CrawlerIR2
{
    class CrawlerMotorkari : ICrawler
    {
        const string Base_url = "https://www.motorkari.cz/clanky/";
        const string page_url = "https://www.motorkari.cz/clanky/?pgr=";

        const int First_page = 1;
        const int Last_page = 256;

        private string Type = "_Html";


        public List<Article> GetArticles()
        {
            List<Article> articles = new List<Article>();
            HtmlWeb web = new HtmlWeb();

            for (int i = First_page; i < Last_page; i++)
            {
                if (i % 64 == 0) Type = "_Html" + i.ToString();

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
            string page;
            string author;
            string date;
            string views;
            string category = FileManager.Instance.GetString(node.InnerText.Replace("\t", string.Empty).Replace("\r", string.Empty), @"Kategorie:(?<string>[\s]*[a-zA-Z]*)"); // node.SelectSingleNode(".//div[@class='parameters']//tr//a").InnerText 
            string articleUrl = "https:" + node.SelectSingleNode(".//h3/a/@href").GetAttributeValue("href", string.Empty);
            string articleTitle = node.SelectSingleNode(".//h3/a/@href").InnerText;
            Article res;

            // Load article page
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load(articleUrl);
            HtmlNode article = document.DocumentNode.SelectSingleNode("//article");

            page = document.DocumentNode.InnerText.Replace("\t", string.Empty).Replace("\r", string.Empty);
            views = FileManager.Instance.GetString(page, @"Zobrazeno: (?<string>[^x]*)");
            author = FileManager.Instance.GetString(page, @"Text:[\s]*(?<string>[^\|]*) ");
            date = FileManager.Instance.GetDateString(page, @"Zveřejněno: (?<date>\d{1,2}.\d{1,2}.\d{4})");

            Console.WriteLine("\tINFO: " + date + "\t" + articleUrl);

            // Get comments
            List<Comment> comments = GetComments(articleUrl);
            try
            {
                res = FileManager.Instance.SaveArticle(new Article()
                {
                    Author = author,
                    Date = date,
                    Title = articleTitle,
                    Views = views,
                    Text = article.InnerText
                   .Replace("\r", string.Empty)
                   .Replace("\t", string.Empty)
                   .Replace("\n\n", "\n")
                   .Replace("&nbsp;", string.Empty)
                   .Replace("0x0B", string.Empty).Trim(),
                    HtmlText = article.InnerHtml
                   .Replace("&nbsp;", string.Empty)
                   .Replace("0x0B", string.Empty).Trim(),
                    TidyText = article.InnerText
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

        public Comment ProcessOneComment(HtmlNode item, string url)
        {
            string author = item.SelectSingleNode(".//a[@class='user']").InnerText;
            string date = FileManager.Instance.GetDateString(item.InnerText, @"napsal (?<date>\d{1,2}.\d{1,2}.\d{4})"); ;

            return new Comment()
            {
                Author = author,
                Date = date,
                Text = item.InnerText.Replace("\r", string.Empty).Replace("\t", string.Empty).Replace("\n\n", "\n").Replace("&nbsp;", "").Trim(),
                HtmlText = item.InnerHtml,
                TidyText = item.InnerText,
                Id = item.GetAttributeValue("id", string.Empty),
                Url = url
            };
        }
    }
}
