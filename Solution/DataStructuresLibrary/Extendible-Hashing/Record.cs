using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresLibrary.Extendible_Hashing
{
    /// <summary>
    /// Abstraktna trieda Record (Zaznam) 
    /// </summary>
    public abstract class Record
    {
        #region Properties
        /// <summary>
        /// Hodnota hovori o tom, ci je dany record validny alebo nie. 
        /// Validny je vtedy, ak je v bloku. 
        /// Nevalidny - ak je vymazany, alebo jednoducho nie je validny. 
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// Adresa bloku s datami zaznamu
        /// </summary>
        public int Address { get; set; }

        /// <summary>
        /// Kazdy zaznam obsahuje Hash Kluc. 
        /// </summary>
        public string Key { get; set; }
        #endregion

        #region Pomocne konstanty
        public const int _pocet_bytov_isvalid = 1;
        public const int _pocet_bytov_key = 4;
        public int _pocet_bytov_address = 4;
        #endregion

        #region Abstraktne metody
        //Abstraknte metody, ktore vratia hodnoty properties
        /// <summary>
        ///Abstraktna metoda vrati celu velkost zaznamu. 
        /// Vypocita sa ako sucet vsetkych bytov properties a adresy a kluca. 
        /// </summary>
        /// <returns>Velkost zazanmu.</returns>
        public abstract int GetSize();
        /// <summary>
        /// Abstraktna metoda, ktora vrati velkost Adressy. 
        /// Vypocitana z poctu bytov- jeValidne + kluc + adresa 
        /// </summary>
        /// <returns></returns>
        public abstract int GetAddressSize();

        /// <summary>
        /// Abstraktna metoda, ktora vrati Hash code daneho zaznamu. 
        /// </summary>
        /// <returns>Hash Code</returns>
        public abstract int GetHash();
        //metody specificke pre record. 
        /// <summary>
        /// Abstraktna metoda, ktora spracuje/skonvertuje dany zaznam a vrati ho ako pole bytov. 
        /// </summary>
        /// <returns>Pole bytov - zaznam konvertovany do bytov.</returns>
        public abstract byte[] ToByteArray(bool allData = true);
        /// <summary>
        ///Abstraktna Metoda naplni dany record datami z array bytov
        /// </summary>
        /// <param name="byteArray">Pole bytov<param>
        /// <param name="hasAdress">Ak ma platnu adresu.</param>
        public abstract void FromByteArray(byte[] byteArray, bool hasAdress = true);

        //override defaultnych hodnot z rodica Object
        /// <summary>
        /// Abstraktna metoda, ktora porovna dva objekty, a vrati true/false. 
        /// Pri implementovani porovnavam ci sa rovnaju specificke identifikacne property a key. 
        /// </summary>
        /// <param name="obj">Record</param>
        /// <returns>Hodnotu ci sa rovnaju. </returns>
        public abstract override bool Equals(object obj);
        /// <summary>
        /// Abstraktna metoda, ktora vypise dany zaznam do stringu. 
        /// </summary>
        /// <returns></returns>
        public abstract override string ToString();
        #endregion
     }
}
