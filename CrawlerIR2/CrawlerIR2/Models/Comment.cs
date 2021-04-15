using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace CrawlerIR2.Models
{
    public class Comment
    {
        public string Author { get; set; }
        public string Date { get; set; }
        public string HtmlText { get; set; }
        public string TidyText { get; set; }
        public string Text { get; set; }
        [MaxLength(300)]
        public string Url { get; set; }
        public string Id { get; set; }

    }
}
