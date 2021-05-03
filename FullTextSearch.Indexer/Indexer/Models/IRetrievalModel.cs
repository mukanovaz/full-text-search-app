
using CrawlerIR2.Indexer;
using FullTextSearch.Indexer.Query;
using System.Collections.Generic;

namespace FullTextSearch.Indexer.Indexer.Models
{
    public interface IRetrievalModel
    {
        List<IResult> EvaluateResults(string query, Index index);
    }
}
