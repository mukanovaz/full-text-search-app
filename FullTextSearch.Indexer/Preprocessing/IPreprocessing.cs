using CrawlerIR2.Models;
using FullTextSearch.Model;
using System.Collections.Generic;

namespace FullTextSearch.Indexer
{
    public class IPreprocessing
    {
        void ParseTokens(string text, Article article, ref Dictionary<string, List<Result>> invertedIndex) { }
        void ParseQuery(string searchTerm, string searchField) { }
    }
}
