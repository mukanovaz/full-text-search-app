using CrawlerIR2.Models;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Cz;
using Lucene.Net.Analysis.Tokenattributes;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FullTextSearch.Model
{
    class LucenePreprocessing
    {
        private static LucenePreprocessing instance = null;
        private CzechAnalyzer Analyzer;

        public static LucenePreprocessing Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LucenePreprocessing();
                }
                return instance;
            }
        }

        public LucenePreprocessing()
        {
            Analyzer = new CzechAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            Analyzer.LoadStopWords(File.Open(Path.Combine(Environment.CurrentDirectory, @"Data\stopwords.txt"), FileMode.Open), Encoding.UTF8);
        }

        public List<string> GetTokens(string text, Article article, ref Dictionary<string, List<int>> invertedIndex)
        {
            List<string> tokens = new List<string>();
            TokenStream ts = Analyzer.TokenStream("text", new System.IO.StringReader(text));
            while (ts.IncrementToken())
            {
                string token = ts.GetAttribute<ITermAttribute>().Term;
                // Check if work exist
                if (!invertedIndex.ContainsKey(token))
                {
                    List<int> tmp = new List<int>();
                    tmp.Add(article.ArticleId);
                    invertedIndex.Add(token, tmp);
                    continue;
                }

                // Check if document exist and add
                if (!invertedIndex[token].Contains(article.ArticleId))
                {
                    invertedIndex[token].Add(article.ArticleId);
                    continue;
                }
            }
            return tokens;
        }

        public void ParseQuery(string searchTerm, string searchField)
        {
            QueryParser parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, searchField, Analyzer);
            Query query = parser.Parse(searchTerm);
        }

    }
}
