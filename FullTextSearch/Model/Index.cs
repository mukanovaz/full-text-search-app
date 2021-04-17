using CrawlerIR2.Interface;
using CrawlerIR2.Models;
using FullTextSearch.Interface;
using FullTextSearch.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullTextSearch.Model
{
    class Index : IIndexer, ISearcher
    {
        Dictionary<string, List<int>> InvertedIndex;

        public Index ()
        {
            InvertedIndex = new Dictionary<string, List<int>>();
        }

        public List<IResult> Search(string query)
        {
            if (InvertedIndex.ContainsKey(query))
            {
                var res = InvertedIndex[query];
            }
            return null;
        }

        void IIndexer.Index(List<Article> documents)
        {
            foreach (Article article in documents)
            {
                List<string> tokens = LucenePreprocessing.Instance.GetTokens(article.GetText(), article, ref InvertedIndex);

            }
        }
    }
}
