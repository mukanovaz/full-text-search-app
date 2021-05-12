using System;
using System.Collections.Generic;
using System.Text;

namespace FullTextSearch.Indexer
{
    public class CzechStemmerLight : IStemmer
    {
        private StringBuilder sb = new StringBuilder();

        public CzechStemmerLight () { }

        public string Stem(string input)
        {
            input = input.ToLower();

            // reset string buffer
            sb.Clear();
            sb.Insert(0, input);

            // removes case endings from nouns and adjectives
            CzechStemmerUtility.Instance.RemoveCase(sb);

            //removes possesive endings from names -ov- and -in-
            CzechStemmerUtility.Instance.RemovePossessives(sb);

            return sb.ToString();
        }
        
    }
}
