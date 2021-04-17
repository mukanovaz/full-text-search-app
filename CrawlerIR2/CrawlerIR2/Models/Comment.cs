using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CrawlerIR2.Models
{
    public class Comment
    {
        private DateTime? dateCreated = null;
        public string Author { get; set; }
        public DateTime DateCreated
        {
            get
            {
                return this.dateCreated.HasValue
                   ? this.dateCreated.Value
                   : DateTime.Now;
            }

            set
            {
                this.dateCreated = value;
            }
        }
        public string HtmlText { get; set; }
        public string TidyText { get; set; }
        public string Text { get; set; }
        [MaxLength(300)]
        public string Url { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }
        [ForeignKey("Article")]
        public int ArticleId { get; set; }
        public Article Article { get; set; }

    }
}
