using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;


namespace FullTextSearch.Indexer
{
    public class AdvancedTokenizer : ITokenizer
    {
        private const string DefaultRegex = "<[^>]*?>|[^ ^.^,^!^?]+";
        private static readonly Regex _isNumericRegex =
        new Regex("^(" +
                    /*Hex*/ @"0x[0-9a-f]+" + "|" +
                    /*Bin*/ @"0b[01]+" + "|" +
                    /*Oct*/ @"0[0-7]*" + "|" +
                    /*Dec*/ @"((?!0)|[-+]|(?=0+\.))(\d*\.)?\d+(e\d+)?" +
                    ")$");

        private static readonly string[] _formats = {"M/d/yyyy h:mm:ss tt", "M/d/yyyy h:mm tt",
                   "MM/dd/yyyy hh:mm:ss", "M/d/yyyy h:mm:ss",
                   "M/d/yyyy hh:mm tt", "M/d/yyyy hh tt",
                   "M/d/yyyy h:mm", "M/d/yyyy h:mm",
                   "MM/dd/yyyy hh:mm", "M/dd/yyyy hh:mm"};

        public static string[] Tokenize(string text, string regex)
        {
            List<string> result = new List<string>();
            string[] words = text.Split(' ');

            foreach (string word in words)
            {
                string wordtmp = word.Replace("\n", string.Empty)
                          .Replace("\r", string.Empty).Trim();
                if (IsValidURL(wordtmp))       // URL
                {
                    result.Add(wordtmp);
                } 
                else if (word.Length == 1 && Char.IsPunctuation(word[0]))
                {
                    continue;
                }
                else if (IsNumeric(wordtmp))   // Numeric
                {
                    result.Add(wordtmp);       
                } else if (IsDate(wordtmp))    // Date
                {
                    result.Add(wordtmp);
                } else                         // Punctuation
                {
                    MatchCollection matcher = Regex.Matches(wordtmp, regex);
                    foreach (Match match in matcher)
                    {
                        result.Add(match.Value
                           .Replace("&quot;", string.Empty)
                           .Replace("&amp;", string.Empty)
                          .Replace(",", string.Empty)
                          .Replace(".", string.Empty)
                          .Replace("!", string.Empty)
                          .Replace("?", string.Empty)
                          .Replace(")", string.Empty)
                          .Replace("(", string.Empty)
                          .Replace("}", string.Empty)
                          .Replace("{", string.Empty)
                          .Replace(":", string.Empty)
                          .Replace(";", string.Empty)
                          .Replace("\"", string.Empty)
                          .Replace("“", string.Empty)
                          .Replace("„", string.Empty)
                      );
                    }
                }
            }
            return result.ToArray();
        }

        private static bool IsValidURL(string URL)
        {
            string Pattern = @"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$";
            Regex Rgx = new Regex(Pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return Rgx.IsMatch(URL);
        }
        private static bool IsNumeric(string value)
        {
            return _isNumericRegex.IsMatch(value);
        }

        private static bool IsDate (string text)
        {
            DateTime dateValue;
            return DateTime.TryParseExact(text, _formats,
                              new CultureInfo("en-US"),
                              DateTimeStyles.None,
                              out dateValue);
        }

        public static string RemoveAccents(String text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory == UnicodeCategory.NonSpacingMark)
                {
                    continue;
                }
                stringBuilder.Append(c);
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public string[] Tokenize(string text)
        {
            return Tokenize(text, DefaultRegex);
        }
    }
}
