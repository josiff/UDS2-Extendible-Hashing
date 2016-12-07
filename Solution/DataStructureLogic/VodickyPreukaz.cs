using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructuresLibrary.Extendible_Hashing;
using DateStructureGeneral;

namespace DataStructureLogic
{
    public class VodickyPreukaz : Record
    {
        #region Properties 
        //        Záznam o vodičskom preukaze má uložené nasledujúce informácie:

        /// <summary>
        /// Meno vodiča(reťazec s maximálnou dĺžkou 35 znakov)
        /// </summary>
        public string MenoVodica { get; set; }

        private const int _pocet_bytov_meno_vodica = 35;
        /// <summary>
        /// Priezvisko vodiča(reťazec s maximálnou dĺžkou 35 znakov)
        /// </summary>
        public string PriezviskoVodica { get; set; }

        private const int _pocet_bytov_priezvisko = 35;
        /// <summary>
        /// Evidenčné číslo preukazu(unikátne celé číslo)
        /// </summary>
        public int EvidencneCisloPreukazu { get; set; }

        private const int _pocet_bytov_cislo_preukazu = 4;
        /// <summary>
        /// Dátum ukončenia platnosti vodičského preukazu
        /// </summary>
        public DateTime UkonceniePlatnosti { get; set; }

        private const int _pocet_bytov_ukoncenie_platnosti = 10;//dd-mm-yyyy
        ///zákaz viesť motorové vozidlo(boolean hodnota)
        public bool ZakazViestVozidlo { get; set; }

        private const int _pocet_bytov_zakaz_viest_vozidlo = 1;
        ///počet dopravných priestupkov v karte vodiča za posledných 12 mesiacov(celé číslo)
        public int DopravnePriestupky { get; set; }

        private const int _pocet_bytov_dopravne_prieskumnik = 4;

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

        public override int GetSize()
        {
            return _pocet_bytov_cislo_preukazu + _pocet_bytov_dopravne_prieskumnik + _pocet_bytov_meno_vodica
                          + _pocet_bytov_priezvisko + _pocet_bytov_ukoncenie_platnosti +
                          _pocet_bytov_zakaz_viest_vozidlo;
        }

        public override bool Equals(object obj)
        {
            var temp = (VodickyPreukaz)obj;
            if (temp != null)
            {
                //ak je hash kluc rozny od null a rovnaju sa - vrati true
                if ((temp.Key != null) && temp.Key.Equals(Key))
                {
                    return true;
                }
                //porovnam ci sa rovnaku evidencne cisla
                if ((temp.EvidencneCisloPreukazu.Equals(EvidencneCisloPreukazu)))
                {
                    return true;
                }

            }
            return false;
        }

        public override byte[] ToByteArray()
        {
            //array bytov ktore vratim
            byte[] poleBytov = new byte[GetSize()];
            int index = 0;
            //evidencne cislo
            Helper_Bytes._get_pom_pole(_pocet_bytov_meno_vodica, Encoding.UTF8.GetBytes(MenoVodica)).CopyTo(poleBytov, index);
            index += _pocet_bytov_meno_vodica;
            Helper_Bytes._get_pom_pole(_pocet_bytov_priezvisko, Encoding.UTF8.GetBytes(PriezviskoVodica)).CopyTo(poleBytov, index);
            index += _pocet_bytov_priezvisko;
            BitConverter.GetBytes(EvidencneCisloPreukazu).CopyTo(poleBytov, index);
            index += _pocet_bytov_cislo_preukazu;
            Encoding.UTF8.GetBytes(UkonceniePlatnosti.ToString("dd.MM.yyyy")).CopyTo(poleBytov, index);
            index += _pocet_bytov_ukoncenie_platnosti;
            BitConverter.GetBytes(ZakazViestVozidlo).CopyTo(poleBytov, index);
            index += _pocet_bytov_zakaz_viest_vozidlo;
            BitConverter.GetBytes(DopravnePriestupky).CopyTo(poleBytov, index);
            return poleBytov;

        }
        public override void FromByteArray(byte[] byteArray)
        {
            int index = 0;
            MenoVodica = Encoding.UTF8.GetString(byteArray, index, _pocet_bytov_meno_vodica).Trim('\0');
            index += _pocet_bytov_meno_vodica;
            PriezviskoVodica = Encoding.UTF8.GetString(byteArray, index, _pocet_bytov_priezvisko).Trim('\0');
            index += _pocet_bytov_priezvisko;
            EvidencneCisloPreukazu = BitConverter.ToInt32(byteArray, index);
            index += _pocet_bytov_cislo_preukazu;
            UkonceniePlatnosti =
            DateTime.ParseExact(Encoding.UTF8.GetString(byteArray, index, _pocet_bytov_ukoncenie_platnosti), "dd.MM.yyyy", CultureInfo.InvariantCulture);
            index += _pocet_bytov_ukoncenie_platnosti;
            ZakazViestVozidlo = BitConverter.ToBoolean(byteArray, index);
            index += _pocet_bytov_zakaz_viest_vozidlo;
            DopravnePriestupky =  BitConverter.ToInt32(byteArray, index);
        }

        public override string ToString()
        {
            string s = "\nEvidencne Cislo: " + EvidencneCisloPreukazu +"\nMeno a Priezvisko: " + MenoVodica + " " + PriezviskoVodica
                
                + "\tUkoncenie Platnosti: " + UkonceniePlatnosti.ToShortDateString()
                + "\t" + (ZakazViestVozidlo ? "POVOLENE" : "ZAKAZ") + " Viest Vozidlo \t " +
                "Pocet dopravnych priestupkov: " + DopravnePriestupky;

            s += "\tHash code: " + Key + "\tSize: " + GetSize() ;

            return s;
        }



    }
}
