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
        /// Kazdy zaznam obsahuje Hash Kluc. 
        /// </summary>
        public string Key { get; set; }
        #endregion

        #region Pomocne konstanty
        public const int _pocet_bytov_isvalid = 1;
        public const int _pocet_bytov_key = 4;
        public const int _pocet_bytov_address = 4;
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
        /// Metoda, ktora vrati Hash code daneho zaznamu. 
        /// </summary>
        /// <returns>Hash Code</returns>
        public int GetHash(string key)
        {
            int hashcode = 0;
            int i = 0;
            double j;
            //prejdem kazdy chareakter v kluci
            foreach (var c in key)
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
        //metody specificke pre record. 
        /// <summary>
        /// Abstraktna metoda, ktora spracuje/skonvertuje dany zaznam a vrati ho ako pole bytov. 
        /// </summary>
        /// <returns>Pole bytov - zaznam konvertovany do bytov.</returns>
        public abstract byte[] ToByteArray();
        /// <summary>
        ///Abstraktna Metoda naplni dany record datami z array bytov
        /// </summary>
        /// <param name="byteArray">Pole bytov<param>
        /// <param name="hasAdress">Ak ma platnu adresu.</param>
        public abstract void FromByteArray(byte[] byteArray);

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
