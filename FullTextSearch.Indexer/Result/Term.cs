using System;

namespace FullTextSearch.Indexer.Result
{
    class Term
    {
        /// <summary>
        /// Word
        /// </summary>
        public string Word { get; set; }
        /// <summary>
        /// Number of word in documets
        /// </summary>
        public int DF { get; set; }
        /// <summary>
        /// log(Num of documents/DF) 
        /// </summary>
        public double IDF { get; set; }

        /// <summary>
        /// Set IDF value
        /// </summary>
        /// <param name="docs_count">Number of documents</param>
        public void SetIDF(int docs_count)
        {
            double tmp = (double)docs_count / (double)DF;
            IDF = Math.Log10(tmp);
        }
    }
}
