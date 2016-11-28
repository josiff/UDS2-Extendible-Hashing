using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructuresLibrary.Extendible_Hashing;

namespace DataStructureLogic
{
   public class Auto : Record
    {
        #region Properties
        ///Záznam o aute má uložené nasledujúce informácie:
        ///Evidenčné číslo vozidla(unikátny reťazec s maximálnou dĺžkou 7 znakov)
        public int EvidencneCisloVozidla { get; set; }
        public const int EvidencneCisloVozidlaMaxLength = 7; 
        ///VIN číslo(unikátny reťazec s maximálnou dĺžkou 17 znakov)
        public string VinCislo { get; set; }
        public const int VinCisloMaxLength = 17;
        ///Počet náprav(celé číslo)
        public int PocetNaprav { get; set; }
        ///Prevádzková hmotnosť(celé číslo)
        public int PrevadzkovaHmotnost { get; set; }
        ///v pátraní(boolean hodnota)
        public bool VPatrani { get; set; }
        ///dátum konca platnosti STK
        public DateTime KoniecPlatnostiSTK { get; set; }
        ///dátum konca platnosti EK
        public DateTime KoniecPlatnostiEK { get; set; }
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
        public Auto(int evidencneCisloVozidla, string vinCislo, int pocetNaprav, int prevadzkovaHmotnost, bool vPatrani, DateTime koniecPlatnostiStk, DateTime koniecPlatnostiEk)
        {
            EvidencneCisloVozidla = evidencneCisloVozidla;
            VinCislo = vinCislo;
            PocetNaprav = pocetNaprav;
            PrevadzkovaHmotnost = prevadzkovaHmotnost;
            VPatrani = vPatrani;
            KoniecPlatnostiSTK = koniecPlatnostiStk;
            KoniecPlatnostiEK = koniecPlatnostiEk;
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
        }
        #endregion


        #region Override methods from Block

          public override int GetHash()
        {
            throw new NotImplementedException();
        }

        public override BitArray GetBitSet()
        {
            throw new NotImplementedException();
        }

        public override int GetSize()
        {
            throw new NotImplementedException();
        }

        public override bool Equals()
        {
            throw new NotImplementedException();
        }

        public override BitArray ToByteArray(Record data)
        {
            throw new NotImplementedException();
        }

        public override Record FromByteArray(BitArray bitArray)
        {
            throw new NotImplementedException();
        }

        #endregion

      
    }
}
