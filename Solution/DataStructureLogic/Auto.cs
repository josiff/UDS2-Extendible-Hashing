using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DataStructuresLibrary.Extendible_Hashing;
using DateStructureGeneral;

namespace DataStructureLogic
{
    public class Auto : Record
    {
        #region Properties

        ///Záznam o aute má uložené nasledujúce informácie:
        ///Evidenčné číslo vozidla(unikátny reťazec s maximálnou dĺžkou 7 znakov)
        public string EvidencneCisloVozidla { get; set; }

        public const int EvidencneCisloVozidlaMaxLength = 7;
        private int _pocet_bytov_evc = 7;

        ///VIN číslo(unikátny reťazec s maximálnou dĺžkou 17 znakov)
        public string VinCislo { get; set; }

        public const int VinCisloMaxLength = 17;
        private int _pocet_bytov_vin = 17;

        /// <summary>
        /// Počet náprav(celé číslo)
        /// </summary>
        public int PocetNaprav { get; set; }

        private int _pocet_bytov_napravy = 4;

        /// <summary>
        /// Prevádzková hmotnosť(celé číslo)
        /// </summary>
        public int PrevadzkovaHmotnost { get; set; }

        private int _pocet_bytov_hmotnost = 4;

        /// <summary>
        /// v pátraní(boolean hodnota)
        /// </summary>
        public bool VPatrani { get; set; }

        private int _pocet_bytov_vpatrani = 1;

        /// <summary>
        /// dátum konca platnosti STK
        /// </summary>
        public DateTime KoniecPlatnostiSTK { get; set; }

        private int _pocet_bytov_datum_stk = 10;

        /// <summary>
        /// dátum konca platnosti EK
        /// </summary>
        public DateTime KoniecPlatnostiEK { get; set; }

        private int _pocet_bytov_datum_ek = 10;

        public bool JePotrebnyPlnyVypis = true;

        #endregion

        #region Constructors

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="evidencneCisloVozidla"></param>
        /// <param name="vinCislo"></param>
        /// <param name="pocetNaprav"></param>
        /// <param name="prevadzkovaHmotnost"></param>
        /// <param name="vPatrani"></param>
        /// <param name="koniecPlatnostiStk"></param>
        /// <param name="koniecPlatnostiEk"></param>
        public Auto(string evidencneCisloVozidla, string vinCislo, int pocetNaprav,
            int prevadzkovaHmotnost, bool vPatrani,
            DateTime koniecPlatnostiStk, DateTime koniecPlatnostiEk)
        {
            EvidencneCisloVozidla = evidencneCisloVozidla;
            VinCislo = vinCislo;
            PocetNaprav = pocetNaprav;
            PrevadzkovaHmotnost = prevadzkovaHmotnost;
            VPatrani = vPatrani;
            KoniecPlatnostiSTK = koniecPlatnostiStk;
            KoniecPlatnostiEK = koniecPlatnostiEk;
            IsValid = true;
            Address = -1;
        }

        /// <summary>
        /// Copy konstruktor. 
        /// </summary>
        /// <param name="auto"></param>
        public Auto(Auto auto)
        {
            EvidencneCisloVozidla = auto.EvidencneCisloVozidla;
            VinCislo = auto.VinCislo;
            PocetNaprav = auto.PocetNaprav;
            PrevadzkovaHmotnost = auto.PrevadzkovaHmotnost;
            VPatrani = auto.VPatrani;
            KoniecPlatnostiSTK = auto.KoniecPlatnostiSTK;
            KoniecPlatnostiEK = auto.KoniecPlatnostiEK;
            Address = auto.Address;
            Key = auto.Key;
            IsValid = auto.IsValid;


        }

        public Auto(string key)
        {
            Key = key;
            IsValid = true;
        }

        public Auto()
        {
            IsValid = false;
        }

        #endregion


        #region Override methods from Block




        public override int GetSize()
        {
            return _pocet_bytov_vin + _pocet_bytov_datum_ek + _pocet_bytov_datum_stk +
                     _pocet_bytov_evc + _pocet_bytov_hmotnost + _pocet_bytov_napravy + _pocet_bytov_vpatrani;
        }

        public override string ToString()
        {
            string s = "\nEVC: " + EvidencneCisloVozidla + "\tVIN cislo: " + VinCislo +
                       "\nPocet naprav: " + PocetNaprav + "\tPrevadzkova hmotnost: " + PrevadzkovaHmotnost
                       + "\tV patrani: " + ((VPatrani) ? "ANO" : "NIE")
                       + "\tKoniec platnosti STK: " + KoniecPlatnostiSTK.ToShortDateString()
                       + "\tKoniec platnosti EK: " + KoniecPlatnostiEK.ToShortDateString();
            string s2 = "\nHash code: " + Key + "\tSize: " + GetSize();
            if (JePotrebnyPlnyVypis)
            {
                return s + s2;
            }
            else
            {
                return s2;
            }
        }

        public override bool Equals(object obj)
        {
            var temp = (Auto)obj;
            if (temp != null)
            {
                //ak je hash kluc rozny od null a rovnaju sa - vrati true
                if ((temp.Key != null) && temp.Key.Equals(Key))
                {
                    return true;
                }
                //porovnam ci sa rovnaku evidencne cisla
                if ((temp.EvidencneCisloVozidla != null) && (temp.EvidencneCisloVozidla.Equals(EvidencneCisloVozidla)))
                {
                    return true;
                }
                //porovnam ci su rovnake vin cisla
                if (temp.VinCislo != null && temp.VinCislo.Equals(VinCislo))
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// Metoda zkonvetuje data do pola bytov. 
        /// </summary>
        /// <returns></returns>
        public override byte[] ToByteArray()
        {
            //array bytov ktore vratim
            byte[] poleBytov = new byte[GetSize()];
            int index = 0;
            //evidencne cislo
            Helper_Bytes._get_pom_pole(_pocet_bytov_evc, Encoding.UTF8.GetBytes(EvidencneCisloVozidla)).CopyTo(poleBytov, index);
            index += _pocet_bytov_evc;
            Helper_Bytes._get_pom_pole(_pocet_bytov_vin, Encoding.UTF8.GetBytes(VinCislo)).CopyTo(poleBytov, index);
            index += _pocet_bytov_vin;
            BitConverter.GetBytes(PocetNaprav).CopyTo(poleBytov, index);
            index += _pocet_bytov_napravy;
            BitConverter.GetBytes(PrevadzkovaHmotnost).CopyTo(poleBytov, index);
            index += _pocet_bytov_napravy;
            BitConverter.GetBytes(VPatrani).CopyTo(poleBytov, index);
            Encoding.UTF8.GetBytes(KoniecPlatnostiSTK.ToString("dd.MM.yyyy")).CopyTo(poleBytov, index);
            index += _pocet_bytov_datum_stk;
            Encoding.UTF8.GetBytes(KoniecPlatnostiEK.ToString("dd.MM.yyyy")).CopyTo(poleBytov, index);       
            return poleBytov;

        }
        public override void FromByteArray(byte[] byteArray)
        {
            int temp_index = 0;
                //evidencne cislo 
                EvidencneCisloVozidla = Encoding.UTF8.GetString(byteArray, temp_index, _pocet_bytov_evc).Trim('\0');
                temp_index += _pocet_bytov_evc;
                //vin cislo
                VinCislo = Encoding.UTF8.GetString(byteArray, temp_index, _pocet_bytov_vin).Trim('\0');
                temp_index += _pocet_bytov_vin;
                //pocet naprav
                PocetNaprav = BitConverter.ToInt32(byteArray, temp_index);
                temp_index += _pocet_bytov_napravy;
                //prevadzkova hmotnost
                PrevadzkovaHmotnost = BitConverter.ToInt32(byteArray, temp_index);
                temp_index += _pocet_bytov_napravy;
                // je v patrani
                VPatrani = BitConverter.ToBoolean(byteArray, temp_index);
                temp_index += _pocet_bytov_vpatrani;
                //koniec platnosti stk
                KoniecPlatnostiSTK =
                DateTime.ParseExact(Encoding.UTF8.GetString(byteArray, temp_index, _pocet_bytov_datum_stk), "dd.MM.yyyy", CultureInfo.InvariantCulture);
        }
        #endregion
    }
}
