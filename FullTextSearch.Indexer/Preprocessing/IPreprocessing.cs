using CrawlerIR2.Models;
using System.Collections.Generic;

namespace FullTextSearch.Indexer
{
    public interface IPreprocessing
    {
        void ParseTokens(string text, Article article, ref Dictionary<string, List<Result>> invertedIndex);
        void ParseQuery(string searchTerm, string searchField);
    }
}
