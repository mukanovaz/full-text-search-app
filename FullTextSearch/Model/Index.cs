using CrawlerIR2.Interface;
using CrawlerIR2.Models;
using FullTextSearch.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullTextSearch.Model
{
    class Index : IIndexer, ISearcher
    {
        Dictionary<string, List<Article>> InvertedIndex;

        public Index ()
        {
            InvertedIndex = new Dictionary<string, List<Article>>();
        }

        public List<IResult> Search(string query)
        {
            throw new NotImplementedException();
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
