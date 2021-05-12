using System;
using System.Collections.Generic;
using System.Text;

namespace FullTextSearch.Indexer
{
    public interface ITokenizer
    {
        string[] Tokenize(string text);
    }
}
