using CrawlerIR2.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FullTextSearch.Indexer
{
    public class BasicPreprocessing : IPreprocessing
    {
        const string WithDiacritics = "áÁčČďĎéÉěĚíÍňŇóÓřŘšŠťŤúÚůŮýÝžŽ";
        const string WithoutDiacritics = "aAcCdDeEeEiInNoOrRsStTuUuUyYzZ";
        private readonly IStemmer _stemmer;
        private ITokenizer _tokenizer;
        private List<string> _stopwords;
        private bool _removeAccentsBeforeStemming;
        private bool _removeAccentsAfterStemming;
        private bool _toLowercase;
        private Index _index;

        public BasicPreprocessing(IStemmer stemmer, ITokenizer tokenizer, List<string> stopwords, bool removeAccentsBeforeStemming, bool removeAccentsAfterStemming, bool toLowercase)
        {
            _stemmer = stemmer;
            _tokenizer = tokenizer;
            _stopwords = stopwords;
            _removeAccentsBeforeStemming = removeAccentsBeforeStemming;
            _removeAccentsAfterStemming = removeAccentsAfterStemming;
            _toLowercase = toLowercase;
        }

        public bool IsStemerSetting { get; set; }

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

        public List<string> Index(string document)
        {
            List<string> result = new List<string>();
            if (_toLowercase)
            {
                document = document.ToLower();
            }
            if (_removeAccentsBeforeStemming)
            {
                document = RemoveAccents(document);
            }

            foreach (string token in _tokenizer.Tokenize(document))
            {
                string tmpToken = "";
                if (_stemmer != null)
                {
                    tmpToken = _stemmer.Stem(token);
                }

                if (_removeAccentsAfterStemming)
                {
                    tmpToken = RemoveAccents(tmpToken);
                }
                result.Add(tmpToken);
            }
            return result;
        }

        public void Index(Article document)
        {
            if (document == null)
            {
                throw new ArgumentNullException("Document cannot be empty");
            }
            string text = document.Text;
            if (_toLowercase)
            {
                text = text.ToLower();
            }
            if (_removeAccentsBeforeStemming)
            {
                text = RemoveAccents(text);
            }

            foreach (string token in _tokenizer.Tokenize(text))
            {
                string tmpToken = "";
                if (_stemmer != null)
                {
                    tmpToken = _stemmer.Stem(token);
                }

                if (_removeAccentsAfterStemming)
                {
                    tmpToken = RemoveAccents(tmpToken);
                }
                _index.Append(tmpToken, document.GetId(), 0, 0);
            }
        }

        public void ParseTokens(string text, Article article, Index index)
        {
            _index = index;
            Index(article);
        }

        public string[] ParseTokens(string text)
        {
            return Index(text).ToArray();
        }

        private string RemoveAccents(string text)
        {
            return CzechStemmerUtility.Instance.RemoveDiacritics(text);
        }
    }
}
