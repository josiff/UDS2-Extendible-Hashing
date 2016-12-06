using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructuresLibrary.Extendible_Hashing;

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

        public bool JePotrebnyPlnyVypis = true;

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

        public override int GetHash()
        {
            int hashcode = 0;
            int i = 0;
            double j;
            //prejdem kazdy chareakter v kluci
            foreach (var c in Key)
            {
                //ak je dlzka kluca vacsia ako jedna - nastavim pomocnu prementu na 3 alebo 3.5 inak 1
                double pom1 = Key.Length > 7 ? 3 : 1;
                double pom2 = Key.Length > 7 ? 3.5 : 1;
                i++;
                //konvertujem char na int
                j = Convert.ToInt32(c) / 6.0;
                //vypocitam pomocou funkcie hash code. 
                hashcode += Convert.ToInt32(Math.Round(Math.Pow(j, i / pom1)) * i * 1.6 * pom1 * pom2);
            }

            return hashcode;
        }

        public override byte[] GetBitSet()
        {
            throw new NotImplementedException();
        }

        public override int GetSize()
        {
            return Size = _pocet_bytov_cislo_preukazu + _pocet_bytov_dopravne_prieskumnik + _pocet_bytov_meno_vodica
                          + _pocet_bytov_priezvisko + _pocet_bytov_ukoncenie_platnosti +
                          _pocet_bytov_zakaz_viest_vozidlo
                          + _pocet_bytov_address + _pocet_bytov_isvalid;
        }

        public override int GetAddressSize()
        {
            return _pocet_bytov_address + _pocet_bytov_isvalid + _pocet_bytov_key;
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

        public override byte[] ToByteArray(bool allData = true)
        {
            throw new NotImplementedException();
        }

        public override void FromByteArray(byte[] byteArray, bool hasAdress = true)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            string s = "\nEvidencne Cislo: " + EvidencneCisloPreukazu +"\nMeno a Priezvisko: " + MenoVodica + " " + PriezviskoVodica
                
                + "\tUkoncenie Platnosti: " + UkonceniePlatnosti.ToShortDateString()
                + "\t" + (ZakazViestVozidlo ? "POVOLENE" : "ZAKAZ") + " Viest Vozidlo \t " +
                "Pocet dopravnych priestupkov: " + DopravnePriestupky;

            string s2 = "\nAdresa: " + Address + "\tHash code: " + Key + "\tSize: " + GetSize() + "\tAddress Size: " +
                GetAddressSize();

            if (JePotrebnyPlnyVypis)
            {
                return s + s2;
            }
            else
            {
                return s2;
            }
        }



    }
}
