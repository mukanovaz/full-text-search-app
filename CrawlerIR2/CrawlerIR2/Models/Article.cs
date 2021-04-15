using CrawlerIR2.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace CrawlerIR2.Models
{
    public class Article : IDocument
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public DateTime DateTime { get; set; }
        public string Category { get; set; }
        public string Author { get; set; }
        public string Views { get; set; }
        public string HtmlText { get; set; }
        public string TidyText { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
        public List<Comment> Comments { get; set; }

        public DateTime GetDate()
        {
            return DateTime;
        }

        public int GetId()
        {
            return Id;
        }

        public string GetText()
        {
            return Text;
        }

        public string GetTitle()
        {
            return Title;
        }
    }
}
