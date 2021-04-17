using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlerIR2.Interface
{
    public interface IDocument
    {

        /**
         * Text dokumentu
         * @return text
         */
        string GetText();

        /**
         * Unikátní id dokumentu
         * @return id dokumentu
         */
        int GetId();

        /**
         * Titulek dokumentu
         * @return titulek dokumentu
         */
        string GetTitle();

        /**
         * Datum přidání dokumentu (tj. např. indexace nebo stažení nebo publikování
         *
         * @return datum vztažené k dokumentu
         */
        DateTime GetDate();

    }
}
