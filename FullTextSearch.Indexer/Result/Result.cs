
using System.Collections.Generic;

namespace FullTextSearch.Indexer
{
    public class Result : AbstractResult
    {
        public int StartPosition { get; set; }
        public int EndPosition { get; set; }

        public Result(string documentId)
        {
            DocumentID = documentId;
        }

        public override int GetHashCode()
        {
            return this.DocumentID.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is Result other && this.DocumentID == other.DocumentID;
        }
    }
}
