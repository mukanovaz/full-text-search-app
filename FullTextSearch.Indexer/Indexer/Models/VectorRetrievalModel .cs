using CrawlerIR2.Indexer;
using System;
using System.Collections.Generic;

namespace FullTextSearch.Indexer.Indexer.Models
{
    class VectorRetrievalModel : IRetrievalModel
    {
        public VectorRetrievalModel(LucenePreprocessing preprocessing)
        {
            Preprocessing = preprocessing;
        }

        public LucenePreprocessing Preprocessing { get; }

        public List<IResult> EvaluateResults(string query, Index index)
        {
            throw new NotImplementedException();
        }
    }
}
