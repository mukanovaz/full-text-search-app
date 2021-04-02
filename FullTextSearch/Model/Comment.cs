using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullTextSearch.Model
{
    class Comment
    {
        [Keyword(Name = "id")]
        public string Id { get; set; }
        [Date(Name = "date", Format = "dd.MM.yyyy")]
        public string Date { get; set; }
        [Text(Name = "text")]
        public string Text { get; set; }
        [Keyword(Name = "author")]
        public string Author { get; set; }
    }
}
