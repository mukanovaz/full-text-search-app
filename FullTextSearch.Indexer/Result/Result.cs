
using System.Collections.Generic;

namespace FullTextSearch.Indexer
{
    public class Result : AbstractResult
    {
        public Result(string documentId)
        {
            DocumentID = documentId;
            StartPosition = new List<long>();
            EndPosition = new List<long>();
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
