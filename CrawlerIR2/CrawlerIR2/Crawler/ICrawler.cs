using CrawlerIR2.Models;
using HtmlAgilityPack;
using System.Collections.Generic;

namespace CrawlerIR2.Crawler
{
    public interface ICrawler
    {
        List<Article> GetArticles();
        List<Comment> GetComments(string url);
        Article ProcessOneArticle(HtmlNode node);
        Comment ProcessOneComment(HtmlNode item, string url);
    }
}
