using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace FullTextSearch.Indexer
{
    public class CzechStemmerUtility
    {
        private static CzechStemmerUtility instance = null;

        private CzechStemmerUtility()
        {
        }

        public static CzechStemmerUtility Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CzechStemmerUtility();
                }
                return instance;
            }
        }

        public void RemovePossessives(StringBuilder buffer)
        {
            int len = buffer.Length;

            if (len > 5)
            {
                if (buffer.ToString(len - 2, 2).Equals("ov"))
                {

                    buffer.Remove(len - 2, 2);
                    return;
                }
                if (buffer.ToString(len - 2, 2).Equals("\u016fv"))
                { //-ův

                    buffer.Remove(len - 2, 2);
                    return;
                }
                if (buffer.ToString(len - 2, 2).Equals("in"))
                {

                    buffer.Remove(len - 1, 1);
                    Palatalise(buffer);
                    return;
                }
            }
        }

        public void RemoveCase(StringBuilder buffer)
        {
            int len = buffer.Length;

            if ((len > 7) &&
                buffer.ToString(len - 5, 5).Equals("atech"))
            {

                buffer.Remove(len - 5, 5);
                return;
            }//len>7
            if (len > 6)
            {
                if (buffer.ToString(len - 4, 4).Equals("\u011btem"))
                { //-ětem

                    buffer.Remove(len - 3, 3);
                    Palatalise(buffer);
                    return;
                }
                if (buffer.ToString(len - 4, 4).Equals("at\u016fm"))
                {  //-atům
                    buffer.Remove(len - 4, 4);
                    return;
                }

            }
            if (len > 5)
            {
                if (buffer.ToString(len - 3, 3).Equals("ech") ||
                        buffer.ToString(len - 3, 3).Equals("ich") ||
                        buffer.ToString(len - 3, 3).Equals("\u00edch"))
                { //-ích

                    buffer.Remove(len - 2, 2);
                    Palatalise(buffer);
                    return;
                }
                if (buffer.ToString(len - 3, 3).Equals("\u00e9ho") || //-ého
                        buffer.ToString(len - 3, 3).Equals("\u011bmi") ||  //-ěmi
                        buffer.ToString(len - 3, 3).Equals("emi") ||
                        buffer.ToString(len - 3, 3).Equals("\u00e9mu") ||  //ému
                        buffer.ToString(len - 3, 3).Equals("\u011bte") ||  //-ěte
                        buffer.ToString(len - 3, 3).Equals("\u011bti") ||  //-ěti
                        buffer.ToString(len - 3, 3).Equals("iho") ||
                        buffer.ToString(len - 3, 3).Equals("\u00edho") ||  //-ího
                        buffer.ToString(len - 3, 3).Equals("\u00edmi") ||  //-ími
                        buffer.ToString(len - 3, 3).Equals("imu"))
                {

                    buffer.Remove(len - 2, 2);
                    Palatalise(buffer);
                    return;
                }
                if (buffer.ToString(len - 3, 3).Equals("\u00e1ch") || //-ách
                        buffer.ToString(len - 3, 3).Equals("ata") ||
                        buffer.ToString(len - 3, 3).Equals("aty") ||
                        buffer.ToString(len - 3, 3).Equals("\u00fdch") ||   //-ých
                        buffer.ToString(len - 3, 3).Equals("ama") ||
                        buffer.ToString(len - 3, 3).Equals("ami") ||
                        buffer.ToString(len - 3, 3).Equals("ov\u00e9") ||   //-ové
                        buffer.ToString(len - 3, 3).Equals("ovi") ||
                        buffer.ToString(len - 3, 3).Equals("\u00fdmi"))
                {  //-ými

                    buffer.Remove(len - 3, 3);
                    return;
                }
            }
            if (len > 4)
            {
                if (buffer.ToString(len - 2, 2).Equals("em"))
                {

                    buffer.Remove(len - 1, 1);
                    Palatalise(buffer);
                    return;

                }
                if (buffer.ToString(len - 2, 2).Equals("es") ||
                        buffer.ToString(len - 2, 2).Equals("\u00e9m") ||    //-ém
                        buffer.ToString(len - 2, 2).Equals("\u00edm"))
                {   //-ím

                    buffer.Remove(len - 2, 2);
                    Palatalise(buffer);
                    return;
                }
                if (buffer.ToString(len - 2, 2).Equals("\u016fm"))
                {  //-ům

                    buffer.Remove(len - 2, 2);
                    return;

                }
                if (buffer.ToString(len - 2, 2).Equals("at") ||
                        buffer.ToString(len - 2, 2).Equals("\u00e1m") ||    //-ám
                        buffer.ToString(len - 2, 2).Equals("os") ||
                        buffer.ToString(len - 2, 2).Equals("us") ||
                        buffer.ToString(len - 2, 2).Equals("\u00fdm") ||     //-ým
                        buffer.ToString(len - 2, 2).Equals("mi") ||
                        buffer.ToString(len - 2, 2).Equals("ou"))
                {

                    buffer.Remove(len - 2, 2);
                    return;
                }
            }//len>4
            if (len > 3)
            {
                if (buffer.ToString(len - 1, 1).Equals("e") ||
                        buffer.ToString(len - 1, 1).Equals("i"))
                {

                    Palatalise(buffer);
                    return;

                }
                if (buffer.ToString(len - 1, 1).Equals("\u00ed") ||  //-í
                        buffer.ToString(len - 1, 1).Equals("\u011b"))
                { //-ě

                    Palatalise(buffer);
                    return;
                }
                if (buffer.ToString(len - 1, 1).Equals("u") ||
                        buffer.ToString(len - 1, 1).Equals("y") ||
                        buffer.ToString(len - 1, 1).Equals("\u016f"))
                {  //-ů

                    buffer.Remove(len - 1, 1);
                    return;

                }
                if (buffer.ToString(len - 1, 1).Equals("a") ||
                        buffer.ToString(len - 1, 1).Equals("o") ||
                        buffer.ToString(len - 1, 1).Equals("\u00e1") ||  // -á
                        buffer.ToString(len - 1, 1).Equals("\u00e9") ||  //-é
                        buffer.ToString(len - 1, 1).Equals("\u00fd"))
                {   //-ý

                    buffer.Remove(len - 1, 1);
                    return;
                }
            }//len>3
        }

        private void Palatalise(StringBuilder buffer)
        {
            int len = buffer.Length;

            if (buffer.ToString(len - 2, 2).Equals("ci") ||
                    buffer.ToString(len - 2, 2).Equals("ce") ||
                    buffer.ToString(len - 2, 2).Equals("\u010di") ||  //-či
                    buffer.ToString(len - 2, 2).Equals("\u010de"))
            {  //-č

                ReplaceAt(buffer.ToString(), len - 2, 2, "k");
                return;
            }
            if (buffer.ToString(len - 2, 2).Equals("zi") ||
                    buffer.ToString(len - 2, 2).Equals("ze") ||
                    buffer.ToString(len - 2, 2).Equals("\u017ei") ||  //-ži
                    buffer.ToString(len - 2, 2).Equals("\u017ee"))
            {  //-že

                ReplaceAt(buffer.ToString(), len - 2, 2, "h");
                return;
            }
            if (buffer.ToString(len - 3, 3).Equals("\u010dt\u011b") ||  //-čtě
                    buffer.ToString(len - 3, 3).Equals("\u010dti") ||       //-čti
                    buffer.ToString(len - 3, 3).Equals("\u010dt\u00ed"))
            {  //-čté

                ReplaceAt(buffer.ToString(), len - 3, 3, "ck");
                return;
            }
            if (buffer.ToString(len - 2, 2).Equals("\u0161t\u011b") ||  //-ště
                    buffer.ToString(len - 2, 2).Equals("\u0161ti") ||       //-šti
                    buffer.ToString(len - 2, 2).Equals("\u0161t\u00ed"))
            {  //-šté

                ReplaceAt(buffer.ToString(), len - 2, 2, "sk");
                return;
            }
            buffer.Remove(len - 1, 1);
            return;
        }

        public void RemoveComparative(StringBuilder buffer)
        {
            int len = buffer.Length;
            
            if ((len > 5) &&
                    (buffer.ToString(len - 3, 3).Equals("ej\u0161") ||  //-ejš
                            buffer.ToString(len - 3, 3).Equals("\u011bj\u0161")))
            {
                buffer.Remove(len - 2, 2);
                Palatalise(buffer);
                return;
            }
        }

        public void RemoveDiminutive(StringBuilder buffer)
        {
            int len = buffer.Length;
            //
            if ((len > 7) &&
                    buffer.ToString(len - 5, 5).Equals("ou\u0161ek"))
            {  //-oušek
                buffer.Remove(len - 5, 5);
                return;
            }
            if (len > 6)
            {
                if (buffer.ToString(len - 4, 4).Equals("e\u010dek") ||            //-eček
                        buffer.ToString(len - 4, 4).Equals("\u00e9\u010dek") ||   //-éček
                        buffer.ToString(len - 4, 4).Equals("i\u010dek") ||        //-iček
                        buffer.ToString(len - 4, 4).Equals("\u00ed\u010dek") ||   //íček
                        buffer.ToString(len - 4, 4).Equals("enek") ||
                        buffer.ToString(len - 4, 4).Equals("\u00e9nek") ||        //-ének
                        buffer.ToString(len - 4, 4).Equals("inek") ||
                        buffer.ToString(len - 4, 4).Equals("\u00ednek"))
                {      //-ínek

                    buffer.Remove(len - 3, 3);
                    Palatalise(buffer);
                    return;
                }
                if (buffer.ToString(len - 4, 4).Equals("\u00e1\u010dek") ||  //áček
                        buffer.ToString(len - 4, 4).Equals("a\u010dek") ||   //aček
                        buffer.ToString(len - 4, 4).Equals("o\u010dek") ||   //oček
                        buffer.ToString(len - 4, 4).Equals("u\u010dek") ||   //uček
                        buffer.ToString(len - 4, 4).Equals("anek") ||
                        buffer.ToString(len - 4, 4).Equals("onek") ||
                        buffer.ToString(len - 4, 4).Equals("unek") ||
                        buffer.ToString(len - 4, 4).Equals("\u00e1nek"))
                {   //-ánek

                    buffer.Remove(len - 4, 4);
                    return;
                }
            }//len>6
            if (len > 5)
            {
                if (buffer.ToString(len - 3, 3).Equals("e\u010dk") ||   //-ečk
                        buffer.ToString(len - 3, 3).Equals("\u00e9\u010dk") ||  //-éčk
                        buffer.ToString(len - 3, 3).Equals("i\u010dk") ||   //-ičk
                        buffer.ToString(len - 3, 3).Equals("\u00ed\u010dk") ||    //-íčk
                        buffer.ToString(len - 3, 3).Equals("enk") ||   //-enk
                        buffer.ToString(len - 3, 3).Equals("\u00e9nk") ||  //-énk
                        buffer.ToString(len - 3, 3).Equals("ink") ||   //-ink
                        buffer.ToString(len - 3, 3).Equals("\u00ednk"))
                {   //-ínk

                    buffer.Remove(len - 3, 3);
                    Palatalise(buffer);
                    return;
                }
                if (buffer.ToString(len - 3, 3).Equals("\u00e1\u010dk") ||  //-áčk
                        buffer.ToString(len - 3, 3).Equals("au010dk") || //-ačk
                        buffer.ToString(len - 3, 3).Equals("o\u010dk") ||  //-očk
                        buffer.ToString(len - 3, 3).Equals("u\u010dk") ||   //-učk
                        buffer.ToString(len - 3, 3).Equals("ank") ||
                        buffer.ToString(len - 3, 3).Equals("onk") ||
                        buffer.ToString(len - 3, 3).Equals("unk"))
                {

                    buffer.Remove(len - 3, 3);
                    return;

                }
                if (buffer.ToString(len - 3, 3).Equals("\u00e1tk") || //-átk
                        buffer.ToString(len - 3, 3).Equals("\u00e1nk") ||  //-ánk
                        buffer.ToString(len - 3, 3).Equals("u\u0161k"))
                {   //-ušk

                    buffer.Remove(len - 3, 3);
                    return;
                }
            }//len>5
            if (len > 4)
            {
                if (buffer.ToString(len - 2, 2).Equals("ek") ||
                        buffer.ToString(len - 2, 2).Equals("\u00e9k") ||  //-ék
                        buffer.ToString(len - 2, 2).Equals("\u00edk") ||  //-ík
                        buffer.ToString(len - 2, 2).Equals("ik"))
                {

                    buffer.Remove(len - 1, 1);
                    Palatalise(buffer);
                    return;
                }
                if (buffer.ToString(len - 2, 2).Equals("\u00e1k") ||  //-ák
                        buffer.ToString(len - 2, 2).Equals("ak") ||
                        buffer.ToString(len - 2, 2).Equals("ok") ||
                        buffer.ToString(len - 2, 2).Equals("uk"))
                {

                    buffer.Remove(len - 1, 1);
                    return;
                }
            }
            if ((len > 3) &&
                    buffer.ToString(len - 1, 1).Equals("k"))
            {

                buffer.Remove(len - 1, 1);
                return;
            }
        }

        public void RemoveAugmentative(StringBuilder buffer)
        {
            int len = buffer.Length;
            //
            if ((len > 6) &&
                    buffer.ToString(len - 4, 4).Equals("ajzn"))
            {

                buffer.Remove(len - 4, 4);
                return;
            }
            if ((len > 5) &&
                    (buffer.ToString(len - 3, 3).Equals("izn") ||
                            buffer.ToString(len - 3, 3).Equals("isk")))
            {

                buffer.Remove(len - 2, 2);
                Palatalise(buffer);
                return;
            }
            if ((len > 4) &&
                    buffer.ToString(len - 2, 2).Equals("\00e1k"))
            { //-ák

                buffer.Remove(len - 2, 2);
                return;
            }
        }

        public void RemoveDerivational(StringBuilder buffer)
        {
            int len = buffer.Length;
            //
            if ((len > 8) &&
                    buffer.ToString(len - 6, 6).Equals("obinec"))
            {

                buffer.Remove(len - 6, 6);
                return;
            }//len >8
            if (len > 7)
            {
                if (buffer.ToString(len - 5, 5).Equals("ion\u00e1\u0159"))
                { // -ionář

                    buffer.Remove(len - 4, 4);
                    Palatalise(buffer);
                    return;
                }
                if (buffer.ToString(len - 5, 5).Equals("ovisk") ||
                        buffer.ToString(len - 5, 5).Equals("ovstv") ||
                        buffer.ToString(len - 5, 5).Equals("ovi\u0161t") ||  //-ovišt
                        buffer.ToString(len - 5, 5).Equals("ovn\u00edk"))
                { //-ovník

                    buffer.Remove(len - 5, 5);
                    return;
                }
            }//len>7
            if (len > 6)
            {
                if (buffer.ToString(len - 4, 4).Equals("\u00e1sek") || // -ásek
                        buffer.ToString(len - 4, 4).Equals("loun") ||
                        buffer.ToString(len - 4, 4).Equals("nost") ||
                        buffer.ToString(len - 4, 4).Equals("teln") ||
                        buffer.ToString(len - 4, 4).Equals("ovec") ||
                        buffer.ToString(len - 5, 5).Equals("ov\u00edk") || //-ovík
                        buffer.ToString(len - 4, 4).Equals("ovtv") ||
                        buffer.ToString(len - 4, 4).Equals("ovin") ||
                        buffer.ToString(len - 4, 4).Equals("\u0161tin"))
                { //-štin

                    buffer.Remove(len - 4, 4);
                    return;
                }
                if (buffer.ToString(len - 4, 4).Equals("enic") ||
                        buffer.ToString(len - 4, 4).Equals("inec") ||
                        buffer.ToString(len - 4, 4).Equals("itel"))
                {

                    buffer.Remove(len - 3, 3);
                    Palatalise(buffer);
                    return;
                }
            }//len>6
            if (len > 5)
            {
                if (buffer.ToString(len - 3, 3).Equals("\u00e1rn"))
                { //-árn

                    buffer.Remove(len - 3, 3);
                    return;
                }
                if (buffer.ToString(len - 3, 3).Equals("\u011bnk"))
                { //-ěnk

                    buffer.Remove(len - 2, 2);
                    Palatalise(buffer);
                    return;
                }
                if (buffer.ToString(len - 3, 3).Equals("i\u00e1n") || //-ián
                        buffer.ToString(len - 3, 3).Equals("ist") ||
                        buffer.ToString(len - 3, 3).Equals("isk") ||
                        buffer.ToString(len - 3, 3).Equals("i\u0161t") || //-išt
                        buffer.ToString(len - 3, 3).Equals("itb") ||
                        buffer.ToString(len - 3, 3).Equals("\u00edrn"))
                {  //-írn

                    buffer.Remove(len - 2, 2);
                    Palatalise(buffer);
                    return;
                }
                if (buffer.ToString(len - 3, 3).Equals("och") ||
                        buffer.ToString(len - 3, 3).Equals("ost") ||
                        buffer.ToString(len - 3, 3).Equals("ovn") ||
                        buffer.ToString(len - 3, 3).Equals("oun") ||
                        buffer.ToString(len - 3, 3).Equals("out") ||
                        buffer.ToString(len - 3, 3).Equals("ou\u0161"))
                {  //-ouš

                    buffer.Remove(len - 3, 3);
                    return;
                }
                if (buffer.ToString(len - 3, 3).Equals("u\u0161k"))
                { //-ušk

                    buffer.Remove(len - 3, 3);
                    return;
                }
                if (buffer.ToString(len - 3, 3).Equals("kyn") ||
                        buffer.ToString(len - 3, 3).Equals("\u010dan") ||    //-čan
                        buffer.ToString(len - 3, 3).Equals("k\u00e1\u0159") || //kář
                        buffer.ToString(len - 3, 3).Equals("n\u00e9\u0159") || //néř
                        buffer.ToString(len - 3, 3).Equals("n\u00edk") ||      //-ník
                        buffer.ToString(len - 3, 3).Equals("ctv") ||
                        buffer.ToString(len - 3, 3).Equals("stv"))
                {

                    buffer.Remove(len - 3, 3);
                    return;
                }
            }//len>5
            if (len > 4)
            {
                if (buffer.ToString(len - 2, 2).Equals("\u00e1\u010d") || // -áč
                        buffer.ToString(len - 2, 2).Equals("a\u010d") ||      //-ač
                        buffer.ToString(len - 2, 2).Equals("\u00e1n") ||      //-án
                        buffer.ToString(len - 2, 2).Equals("an") ||
                        buffer.ToString(len - 2, 2).Equals("\u00e1\u0159") || //-ář
                        buffer.ToString(len - 2, 2).Equals("as"))
                {

                    buffer.Remove(len - 2, 2);
                    return;
                }
                if (buffer.ToString(len - 2, 2).Equals("ec") ||
                        buffer.ToString(len - 2, 2).Equals("en") ||
                        buffer.ToString(len - 2, 2).Equals("\u011bn") ||   //-ěn
                        buffer.ToString(len - 2, 2).Equals("\u00e9\u0159"))
                {  //-éř

                    buffer.Remove(len - 1, 1);
                    Palatalise(buffer);
                    return;
                }
                if (buffer.ToString(len - 2, 2).Equals("\u00ed\u0159") || //-íř
                        buffer.ToString(len - 2, 2).Equals("ic") ||
                        buffer.ToString(len - 2, 2).Equals("in") ||
                        buffer.ToString(len - 2, 2).Equals("\u00edn") ||  //-ín
                        buffer.ToString(len - 2, 2).Equals("it") ||
                        buffer.ToString(len - 2, 2).Equals("iv"))
                {

                    buffer.Remove(len - 1, 1);
                    Palatalise(buffer);
                    return;
                }

                if (buffer.ToString(len - 2, 2).Equals("ob") ||
                        buffer.ToString(len - 2, 2).Equals("ot") ||
                        buffer.ToString(len - 2, 2).Equals("ov") ||
                        buffer.ToString(len - 2, 2).Equals("o\u0148"))
                { //-oň

                    buffer.Remove(len - 2, 2);
                    return;
                }
                if (buffer.ToString(len - 2, 2).Equals("ul"))
                {

                    buffer.Remove(len - 2, 2);
                    return;
                }
                if (buffer.ToString(len - 2, 2).Equals("yn"))
                {

                    buffer.Remove(len - 2, 2);
                    return;
                }
                if (buffer.ToString(len - 2, 2).Equals("\u010dk") ||              //-čk
                        buffer.ToString(len - 2, 2).Equals("\u010dn") ||  //-čn
                        buffer.ToString(len - 2, 2).Equals("dl") ||
                        buffer.ToString(len - 2, 2).Equals("nk") ||
                        buffer.ToString(len - 2, 2).Equals("tv") ||
                        buffer.ToString(len - 2, 2).Equals("tk") ||
                        buffer.ToString(len - 2, 2).Equals("vk"))
                {

                    buffer.Remove(len - 2, 2);
                    return;
                }
            }//len>4
            if (len > 3)
            {
                if (buffer.ToString()[buffer.Length - 1] == 'c' ||
                        buffer.ToString()[buffer.Length - 1] == '\u010d' || //-č
                        buffer.ToString()[buffer.Length - 1] == 'k' ||
                        buffer.ToString()[buffer.Length - 1] == 'l' ||
                        buffer.ToString()[buffer.Length - 1] == 'n' ||
                        buffer.ToString()[buffer.Length - 1] == 't')
                {

                    buffer.Remove(len - 1, 1);
                }
            }//len>3
        }

        public string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
        public string ReplaceAt(string str, int first, int last, string replace)
        {
            return str.Remove(first, last)
                    .Insert(first, replace);
        }



    }
}
