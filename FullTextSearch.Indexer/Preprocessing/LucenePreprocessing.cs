using CrawlerIR2.Models;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Cz;
using Lucene.Net.Analysis.Tokenattributes;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FullTextSearch.Indexer
{
    public class LucenePreprocessing : IPreprocessing
    {
        private CzechAnalyzer Analyzer;

        private bool _removeAccentsBeforeStemming;
        private bool _removeAccentsAfterStemming;
        private bool _toLowercase;

        private readonly IStemmer _stemmer;

        public bool IsStemerSetting { get; set; } = true;

        public LucenePreprocessing(bool removeAccentsBeforeStemming = false, bool removeAccentsAfterStemming = true, bool toLower = true)
        {
            Analyzer = new CzechAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            Analyzer.LoadStopWords(File.Open(@"Data\cz.txt", FileMode.Open), Encoding.UTF8);

            _removeAccentsBeforeStemming = removeAccentsBeforeStemming;
            _removeAccentsAfterStemming = removeAccentsAfterStemming;
            _toLowercase = toLower;

            _stemmer = new CzechStemmerAggressive();
        }

        public string GetProcessedForm(string text)
        {
            if (_toLowercase)
            {
                text = text.ToLower();
            }
            if (_removeAccentsBeforeStemming)
            {
                text = RemoveAccents(text);
            }
            if (_stemmer != null)
            {
                text = _stemmer.Stem(text);
            }
            if (_removeAccentsAfterStemming)
            {
                text = RemoveAccents(text);
            }
            return text;
        }

        public void ParseTokens(string text, Article article, Index index)
        {
            if (index == null)
            {
                throw new System.ArgumentNullException("Lucene preprocessing: index cannot be null");
            }

            if (article == null)
            {
                throw new System.ArgumentNullException("Lucene preprocessing: article cannot be null");
            }

            if (_toLowercase)
            {
                text = text.ToLower();
            }

            if (_removeAccentsBeforeStemming)
            {
                text = RemoveAccents(text);
            }

            TokenStream ts = Analyzer.TokenStream("text", new System.IO.StringReader(text));
            var offsetAtt = (OffsetAttribute)ts.AddAttribute<IOffsetAttribute>();

            while (ts.IncrementToken())
            {
                string token = ts.GetAttribute<ITermAttribute>().Term;
                int start = offsetAtt.StartOffset;
                int end = offsetAtt.EndOffset;

                if (IsStemerSetting)
                {
                    token = _stemmer.Stem(token);
                }
                
                // Remove html tags
                if (token == "h1" || token == "b" || token == "blockquote")
                    continue;
                token.Replace("&quot;", string.Empty).Replace("&amp;", string.Empty);
                index.Append(token, article.GetId(), start, end);
            }
        }

        private string RemoveAccents(string text)
        {
            return CzechStemmerUtility.Instance.RemoveDiacritics(text);
        }

        public string[] ParseTokens(string text)
        {
            List<string> tokens = new List<string>();
            TokenStream ts = Analyzer.TokenStream("text", new System.IO.StringReader(text));

            while (ts.IncrementToken())
            {
                string token = ts.GetAttribute<ITermAttribute>().Term;

                token = _stemmer.Stem(token);

                // Remove html tags
                if (token == "<h1>" || token == "</h1>" || token == "<b>" || token == "</b>" || token == "<blockquote>" || token == "</blockquote>")
                    continue;
                token.Replace("&quot;", string.Empty).Replace("&amp;", string.Empty);

                tokens.Add(token);
            }
            return tokens.ToArray();
        }
    }
}
