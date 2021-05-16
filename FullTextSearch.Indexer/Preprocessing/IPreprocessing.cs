using CrawlerIR2.Models;
using System.Collections.Generic;

namespace FullTextSearch.Indexer
{
    public interface IPreprocessing
    {
        bool IsStemerSetting { get; set; }

        string GetProcessedForm(string text);
        void ParseTokens(string text, Article article, Index index);
        string[] ParseTokens(string text);
    }
}
