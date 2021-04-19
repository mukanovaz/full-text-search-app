using CrawlerIR2.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullTextSearch.Interface
{
    public abstract class AbstractResult : IResult
    {
        /**
         * Id dokumentu
         */
        public string DocumentID { get; set; }

        /**
         * Rank (pořadí) mezi ostatními vrácenými dokumenty
         */
        public int Rank { get; set; } = -1;

        /**
         * Skóre podobnosti mezi tímto výsledkem (dokumentem) a dotazem
         */
        public float Score { get; set; } = -1;

        public string GetDocumentID()
        {
            return DocumentID;
        }

        public int GetRank()
        {
            return Rank;
        }

        public float GetScore()
        {
            return Score;
        }

        override public string ToString()
        {
            return "Result{" +
              "documentID='" + DocumentID + '\'' +
              ", rank=" + Rank +
              ", score=" + Score +
              '}';
        }

        /**
        * Metoda používaná pro generování výstupu pro vyhodnocovací skript.
        * Metodu nepřepisujte (v potomcích) ani neupravujte
        */
        public string ToString(String topic)
            {
                return topic + " Q0 " + DocumentID + " " + Rank + " " + Score + " runindex1";
            }
        }
}
