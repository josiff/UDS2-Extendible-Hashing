using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureLogic
{
    class VodickyPreukaz
    {
        #region Properties 
        //        Záznam o vodičskom preukaze má uložené nasledujúce informácie:
        ///Meno vodiča(reťazec s maximálnou dĺžkou 35 znakov)
        public string MenoVodica { get; set; }
        ///Priezvisko vodiča(reťazec s maximálnou dĺžkou 35 znakov)
        public string PriezviskoVodica { get; set; }
        ///Evidenčné číslo preukazu(unikátne celé číslo)
        public int EvidencneCisloPreukazu { get; set; }
        ///Dátum ukončenia platnosti vodičského preukazu
        public DateTime UkonceniePlatnosti { get; set; }
        ///zákaz viesť motorové vozidlo(boolean hodnota)
        public bool ZakazViestVozidlo { get; set; }
        ///počet dopravných priestupkov v karte vodiča za posledných 12 mesiacov(celé číslo)
        public int DopravnePriestupky { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="menoVodica"></param>
        /// <param name="priezviskoVodica"></param>
        /// <param name="evidencneCisloPreukazu"></param>
        /// <param name="ukonceniePlatnosti"></param>
        /// <param name="zakazViestVozidlo"></param>
        /// <param name="dopravnePriestupky"></param>
        public VodickyPreukaz(string menoVodica, string priezviskoVodica, int evidencneCisloPreukazu, DateTime ukonceniePlatnosti, bool zakazViestVozidlo, int dopravnePriestupky)
        {
            MenoVodica = menoVodica;
            PriezviskoVodica = priezviskoVodica;
            EvidencneCisloPreukazu = evidencneCisloPreukazu;
            UkonceniePlatnosti = ukonceniePlatnosti;
            ZakazViestVozidlo = zakazViestVozidlo;
            DopravnePriestupky = dopravnePriestupky;
        }
        /// <summary>
        /// Copy Konstruktor
        /// </summary>
        /// <param name="vodickyPreukaz"></param>
        public VodickyPreukaz(VodickyPreukaz vodickyPreukaz)
        {
            MenoVodica = vodickyPreukaz.MenoVodica;
            PriezviskoVodica = vodickyPreukaz.PriezviskoVodica;
            EvidencneCisloPreukazu = vodickyPreukaz.EvidencneCisloPreukazu;
            UkonceniePlatnosti = vodickyPreukaz.UkonceniePlatnosti;
            ZakazViestVozidlo = vodickyPreukaz.ZakazViestVozidlo;
            DopravnePriestupky = vodickyPreukaz.DopravnePriestupky;
        }

        #endregion


    }
}
