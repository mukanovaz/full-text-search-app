using System.Collections.Generic;
using CrawlerIR2.Interface;

namespace FullTextSearch.Interface
{
    public interface ISearcher
    {
        List<IResult> Search(string query);
    }
}
