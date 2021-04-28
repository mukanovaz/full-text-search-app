using CrawlerIR2.Indexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullTextSearch.Indexer.Query
{
    class EvaluateTerms
    {
        private Index _index;

        p

        private List<IResult> AndOperation(string term1 = null, string term2 = null, List<IResult> docs1 = null, List<IResult> docs2 = null)
        {
            if (term1 != null && term2 != null && docs1 == null && docs2 == null)
            {
                docs1 = _index.GetPostingsFor(term1).ToList();
                docs2 = _index.GetPostingsFor(term2).ToList();
            }

            return term1 == null && term2 == null && docs1 == null && docs2 == null ? null : docs1.Intersect(docs2).ToList();
        }

        private List<IResult> OrOperation(string term1 = null, string term2 = null, List<IResult> docs1 = null, List<IResult> docs2 = null)
        {
            if (term1 != null && term2 != null && docs1 == null && docs2 == null)
            {
                docs1 = _index.GetPostingsFor(term1).ToList();
                docs2 = _index.GetPostingsFor(term2).ToList();
            }

            return term1 == null && term2 == null && docs1 == null && docs2 == null ? null : docs1.Union(docs2).ToList();
        }

        private List<IResult> NotOperation(string term1 = null, List<IResult> docs1 = null)
        {
            if (term1 != null && docs1 == null)
            {
                docs1 = _index.GetPostingsFor(term1).ToList();
            }

            List<IResult> results = new List<IResult>();

            for (int i = 1; i < _index.IndexedDocuments.Count(); ++i)
            {
                if (!docs1.Contains(new Result() { DocumentID = i.ToString() }))
                {
                    results.Add(new Result() { DocumentID = i.ToString() });
                }
            }

            return term1 == null && docs1 == null ? null : results;
        }
    }
}
