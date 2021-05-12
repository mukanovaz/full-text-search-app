using CrawlerIR2.Models;
using System.Collections.Generic;

namespace FullTextSearch.Indexer
{
    public interface IPreprocessing
    {
        string GetProcessedForm(string text);
        void ParseTokens(string text, Article article, Index index);
        string[] ParseTokens(string text);
    }
}
