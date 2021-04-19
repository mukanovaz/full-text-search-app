using CrawlerIR2.Models;
using FullTextSearch.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullTextSearch.Interface
{
    public class IPreprocessing
    {
        void ParseTokens(string text, Article article, ref Dictionary<string, List<Result>> invertedIndex) { }
        void ParseQuery(string searchTerm, string searchField) { }
    }
}
