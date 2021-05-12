
using System.Collections.Generic;

namespace CrawlerIR2.Indexer
{
    public interface IResult
    {

        /**
         * Vrátí id dokumentu
         * @return id dokumentu
         */
        string GetDocumentID();

        /**
         * Vrátí skóre podobnosti mezi dokumentem a dotazem
         * např. kosinova podobnost
         *
         * @return skóre podobnosti mezi dokumentem a dotazem
         */
        double GetScore();

        /**
         * Pořadí mezi ostatními vrácenými dokumenty
         * Výsledek s rank 1 je nejrelevantnější dokument k zadanému dotazu
         *
         * @return pořadí mezi ostatními vrácenými dokumenty
         */
        int GetRank();

        /**
         * Metoda používaná pro generování výstupu pro vyhodnocovací skript.
         * Metodu nepřepisujte (v potomcích) ani neupravujte
         */
        string ToString(string topic);
    }
}
