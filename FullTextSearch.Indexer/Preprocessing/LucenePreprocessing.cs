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

namespace FullTextSearch.Indexer
{
    public class LucenePreprocessing
    {
        private CzechAnalyzer Analyzer;

        public LucenePreprocessing()
        {
            Analyzer = new CzechAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            Analyzer.LoadStopWords(File.Open(@"Data\stopwords.txt", FileMode.Open), Encoding.UTF8);
        }

        public void ParseTokens(string text, Article article, Index index)
        {
            TokenStream ts = Analyzer.TokenStream("text", new System.IO.StringReader(text));
            var offsetAtt = (OffsetAttribute)ts.AddAttribute<IOffsetAttribute>();

            while (ts.IncrementToken())
            {
                string token = ts.GetAttribute<ITermAttribute>().Term;
                int start = offsetAtt.StartOffset;
                int end = offsetAtt.EndOffset;
                index.Append(token, article.GetId(), start, end);
            }
        }

        public string[] ParseTokens(string text)
        {
            List<string> tokens = new List<string>();
            TokenStream ts = Analyzer.TokenStream("text", new System.IO.StringReader(text));

            while (ts.IncrementToken())
            {
                string token = ts.GetAttribute<ITermAttribute>().Term;
                tokens.Add(token);
            }
            return tokens.ToArray();
        }

    }
}
