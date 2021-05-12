using System;
using System.Collections.Generic;
using System.Text;

namespace FullTextSearch.Indexer
{
    public class CzechStemmerAggressive : IStemmer
    {
        private StringBuilder sb = new StringBuilder();

        public CzechStemmerAggressive() { }

        public string Stem(string input)
        {
            input = input.ToLower();

            // Reset string buffer
            sb.Clear();
            sb.Insert(0, input);

            // Stemming...
            // Removes case endings from nouns and adjectives
            CzechStemmerUtility.Instance.RemoveCase(sb);

            // Removes possesive endings from names -ov- and -in-
            CzechStemmerUtility.Instance.RemovePossessives(sb);

            // Removes comparative endings
            CzechStemmerUtility.Instance.RemoveComparative(sb);

            // Removes diminutive endings
            CzechStemmerUtility.Instance.RemoveDiminutive(sb);

            // Removes augmentatives endings
            CzechStemmerUtility.Instance.RemoveAugmentative(sb);

            // Removes derivational sufixes from nouns
            CzechStemmerUtility.Instance.RemoveDerivational(sb);

            return sb.ToString();
        }
    }
}
