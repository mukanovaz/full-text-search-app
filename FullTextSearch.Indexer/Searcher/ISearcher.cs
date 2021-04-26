using CrawlerIR2.Indexer;
using System.Collections.Generic;

namespace FullTextSearch.Indexer
{
    public interface ISearcher
    {
        List<IResult> Search(string query);
    }
}
