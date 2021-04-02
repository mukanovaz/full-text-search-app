using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullTextSearch.Model
{
    [ElasticsearchType(RelationName = "article")]
    class Article
    {
        [Keyword(Name = "title")]
        public string Title { get; set; }
        [Text(Name = "url")]
        public string Url { get; set; }
        [Date(Name = "date", Format = "dd.MM.yyyy")]
        public string Date { get; set; }
        [Text(Name = "text")]
        public string Text { get; set; }
        [Keyword(Name = "category")]
        public string Category { get; set; }
        [Keyword(Name = "author")]
        public string Author { get; set; }
        [Keyword(Name = "views")]
        public string Views { get; set; }
        [Object]
        public List<Comment> Comments { get; set; }
    }
}
