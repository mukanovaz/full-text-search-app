using CrawlerIR2.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CrawlerIR2.Models
{
    public class Article : IDocument
    {
        private DateTime? dateCreated = null;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
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
        public string Category { get; set; }
        public string Author { get; set; }
        public string Views { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
        public virtual List<Comment> Comments { get; set; }

        public DateTime GetDate()
        {
            return DateCreated;
        }

        public int GetId()
        {
            return ArticleId;
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
