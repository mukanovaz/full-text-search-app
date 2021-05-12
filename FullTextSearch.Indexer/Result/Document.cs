using System;
using System.Collections.Generic;

namespace FullTextSearch.Indexer
{
    public class Document
    {
        /// <summary>
        /// Document id
        /// </summary>
        public long DocumentID { get; set; }

        /// <summary>
        /// Start position in text
        /// </summary>
        public List<long> StartPosition { get; set; }

        /// <summary>
        /// End position in text
        /// </summary>
        public List<long> EndPosition { get; set; }

        /// <summary>
        /// Word count in document
        /// </summary>
        public double TF { get; set; }

        public Document(int doc_id)
        {
            DocumentID = doc_id;
            TF = 1;
            StartPosition = new List<long>();
            EndPosition = new List<long>();
        }

        public Document(int doc_id, int startPosition, int endPosition)
        {
            DocumentID = doc_id;
            TF = 1;
            SetPositions(startPosition, endPosition);
            StartPosition = new List<long>();
            EndPosition = new List<long>();
        }

        public List<long> GetPositionEnd()
        {
            return EndPosition;
        }

        public List<long> GetPositionStart()
        {
            return StartPosition;
        }

        public void SetPositions(int startPosition, int endPosition)
        {
            if (StartPosition == null || EndPosition == null)
            {
                return;
            }
            StartPosition.Add(startPosition);
            EndPosition.Add(endPosition);
        }

        public override int GetHashCode()
        {
            return this.DocumentID.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is Document other && this.DocumentID == other.DocumentID;
        }
    }
}
