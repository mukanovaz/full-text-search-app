using System;
using System.Collections.Generic;
using System.Text;

namespace FullTextSearch.Indexer
{
    public interface IStemmer
    {
        string Stem(string input);
    }
}
