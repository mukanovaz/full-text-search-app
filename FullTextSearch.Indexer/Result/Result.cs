using System;

namespace FullTextSearch.Indexer
{
    public class Result : AbstractResult
    {
        public int StartPosition { get; set; }
        public int EndPositionPosition { get; set; }

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
