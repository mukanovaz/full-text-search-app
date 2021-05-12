using CrawlerIR2.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CrawlerIR2.Crawler
{
    public class BasicCrawler : ICrawler
    {
        public string TableName { get; set; }
        public string Base_url { get; set; }
        public string Page_url { get; set; }
        public int First_page { get; set; }
        public int Last_page { get; set; }
        public string XPathToElements { get; set; }
        public string XPathToArticleUrl { get; set; }
        public string XPathToTitle { get; set; }
        public string XPathToText { get; set; }
        public bool IsComments { get; set; }
        public string XPathToCommentsUrl { get; set; }
        public string XPathToComment { get; set; }
        public string Type { get; set; }

        public List<Article> GetArticles()
        {
            List<Article> articles = new List<Article>();
            HtmlWeb web = new HtmlWeb();

            for (int i = First_page; i < Last_page; i++)
            {
                HtmlDocument document = web.Load(Page_url + i.ToString());
                HtmlNode[] nodes = document.DocumentNode.SelectNodes(XPathToElements).ToArray();

                foreach (HtmlNode item in nodes)
                {
                    if (item.SelectSingleNode(XPathToArticleUrl) == null)
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
            if (IsComments)
            {
                List<Comment> comments = new List<Comment>();

                HtmlWeb web = new HtmlWeb();
                HtmlDocument document = web.Load(url);
                HtmlNode[] com = document.DocumentNode.SelectNodes(XPathToComment).ToArray();

                if (com == null)
                {
                    return null;
                }
                foreach (HtmlNode item in com)
                {
                    comments.Add(ProcessOneComment(item, url));
                }

                return comments;
            }
            return null;
        }

        public Article ProcessOneArticle(HtmlNode node)
        {
            string text;
            string articleTitle;
            string commentsUrl;
            string articleUrl = CheckUrl(node.SelectSingleNode(XPathToArticleUrl).GetAttributeValue("href", string.Empty));

            // Load article page
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load(articleUrl);

            text = GetTxtByType(document.DocumentNode.SelectSingleNode(XPathToText));
            articleTitle = document.DocumentNode.SelectSingleNode(XPathToTitle).InnerText;
            commentsUrl = CheckUrl(document.DocumentNode.SelectSingleNode(XPathToCommentsUrl).GetAttributeValue("href", string.Empty), articleUrl); 
            
            // Get comments
            List<Comment> comments = GetComments(commentsUrl);
            return new Article()
            {
                Title = articleTitle,
                Text = text,
                Comments = comments,
                Url = articleUrl
            };
        }

        private string GetTxtByType(HtmlNode htmlNode)
        {
            string result;
            if (htmlNode == null) return null;

            result = Type == "HTML"
                ? htmlNode.InnerHtml
                : htmlNode.InnerText
                        .Replace("\r", string.Empty)
                        .Replace("\t", string.Empty)
                        .Replace("\n\n", "\n")
                        .Replace("&nbsp;", string.Empty)
                        .Replace("0x0B", string.Empty).Trim();
            return result;
        }

        public Comment ProcessOneComment(HtmlNode item, string url)
        {
            string text = GetTxtByType(item);

            return new Comment()
            {
                Text = text,
                Url = url
            };
        }

        private string CheckUrl(string url, string article_url = null)
        {
            if (url.Contains(Base_url))
            {
                return "https://" + url.Replace("//", string.Empty);
            } else
            {
                url = url.Replace("https://", string.Empty).Replace("//", string.Empty);
                url = article_url != null ? article_url + url : Path.Combine(Base_url, url);
            }
            return url;
        }
    }
}
