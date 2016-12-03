using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        public string EvidencneCisloVozidla { get; set; }
        public const int EvidencneCisloVozidlaMaxLength = 7;
        private int _pocet_bitov_evc = 7;
        ///VIN číslo(unikátny reťazec s maximálnou dĺžkou 17 znakov)
        public string VinCislo { get; set; }
        public const int VinCisloMaxLength = 17;
        private int _pocet_bitov_vin = 17;
        ///Počet náprav(celé číslo)
        public int PocetNaprav { get; set; }

        private int _pocet_bitov_napravy = 4;
        ///Prevádzková hmotnosť(celé číslo)
        public int PrevadzkovaHmotnost { get; set; }

        private int _pocet_bitov_hmotnost = 4;
        ///v pátraní(boolean hodnota)
        public bool VPatrani { get; set; }

        private int _pocet_bitov_vpatrani = 1;
        ///dátum konca platnosti STK
        public DateTime KoniecPlatnostiSTK { get; set; }

        private int _pocet_bitov_datum_stk = 10;
        ///dátum konca platnosti EK
        public DateTime KoniecPlatnostiEK { get; set; }

        private int _pocet_bitov_datum_ek = 10;
        public int Size { get; private set; }
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
        public Auto(string evidencneCisloVozidla, string vinCislo, int pocetNaprav, int prevadzkovaHmotnost, bool vPatrani, DateTime koniecPlatnostiStk, DateTime koniecPlatnostiEk)
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

        public override byte[] GetBitSet()
        {
            throw new NotImplementedException();
        }


        public override int GetSize()
        {
            throw new NotImplementedException();
        }

        public override int GetAddressSize()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Metoda zkonvetuje data do pola bytov. 
        /// </summary>
        /// <param name="allData"></param>
        /// <returns></returns>
        public override byte[] ToByteArray(bool allData)
        {
            //array bytov ktore vratim
            byte[] poleBytov;
            int temp_index = 0;
            if (allData)
            {
                poleBytov = new byte[GetSize()];
                Array.Copy(BitConverter.GetBytes(IsValid), 0, poleBytov, temp_index, _pocet_bitov_isvalid);
                temp_index += _pocet_bitov_isvalid;
                Array.Copy(BitConverter.GetBytes(Address), 0, poleBytov, temp_index, _pocet_bitov_address);
                temp_index += _pocet_bitov_address;

                if (IsValid)
                {
                    Array.Copy(Encoding.UTF8.GetBytes(EvidencneCisloVozidla), 0, poleBytov, temp_index, _pocet_bitov_evc);
                    temp_index += _pocet_bitov_evc;
                    Array.Copy(Encoding.UTF8.GetBytes(VinCislo), 0, poleBytov, temp_index, _pocet_bitov_vin);
                    temp_index += _pocet_bitov_vin;
                    Array.Copy(BitConverter.GetBytes(PocetNaprav), 0, poleBytov, temp_index, _pocet_bitov_napravy);
                    temp_index += _pocet_bitov_napravy;
                    Array.Copy(BitConverter.GetBytes(PrevadzkovaHmotnost), 0, poleBytov, temp_index,
                        _pocet_bitov_hmotnost);
                    temp_index += _pocet_bitov_napravy;
                    Array.Copy(BitConverter.GetBytes(VPatrani), 0, poleBytov, temp_index, _pocet_bitov_vpatrani);
                    temp_index += _pocet_bitov_vpatrani;
                    Array.Copy(Encoding.UTF8.GetBytes(KoniecPlatnostiSTK.ToString("dd.MM.yyyy")), 0, poleBytov,
                        temp_index, _pocet_bitov_datum_stk);
                    temp_index += _pocet_bitov_datum_stk;
                    Array.Copy(Encoding.UTF8.GetBytes(KoniecPlatnostiEK.ToString("dd.MM.yyyy")), 0, poleBytov,
                        temp_index, _pocet_bitov_datum_ek);
                    // temp_index += _pocet_bitov_datum_ek;
                    return poleBytov;
                }
            }

            //ak chcem vratit iba adresu, a hashovaciu hodnotu
            poleBytov = new byte[GetAddressSize()];
            temp_index = 0;
            Array.Copy(BitConverter.GetBytes(IsValid), 0, poleBytov, temp_index, _pocet_bitov_isvalid);
            temp_index += _pocet_bitov_isvalid;
            Array.Copy(BitConverter.GetBytes(Address), 0, poleBytov, temp_index, _pocet_bitov_address);
            temp_index += _pocet_bitov_address;
            if (IsValid)
            {
                Array.Copy(Encoding.UTF8.GetBytes(Key), 0, poleBytov, temp_index, Encoding.UTF8.GetBytes(Key).Length);
            }
            return poleBytov;
            
        }

        // EvidencneCisloVozidla = evidencneCisloVozidla;
            //VinCislo = vinCislo;
            //PocetNaprav = pocetNaprav;
            //PrevadzkovaHmotnost = prevadzkovaHmotnost;
            //VPatrani = vPatrani;
            //KoniecPlatnostiSTK = koniecPlatnostiStk;
            //KoniecPlatnostiEK = koniecPlatnostiEk;


            /*
             public override byte[] ToBytes(bool address)
              {
                  byte[] bytes;
                  byte[] pomBytes;
                  if (!address)
                  {
                      bytes = new byte[Size];
                      Array.Copy(BitConverter.GetBytes(Valid), 0, bytes, 0, 1);
                      Array.Copy(BitConverter.GetBytes(Address), 0, bytes, 1, 4);
                      if (Valid)
                      {
                          pomBytes = Encoding.UTF8.GetBytes(IdCar);
                          Array.Copy(pomBytes, 0, bytes, 5, pomBytes.Length);
                          var pomBytesVin = Encoding.UTF8.GetBytes(Vin);
                          Array.Copy(pomBytesVin, 0, bytes, 12, pomBytesVin.Length);
                          Array.Copy(BitConverter.GetBytes(Axles), 0, bytes, 29, 4);
                          Array.Copy(BitConverter.GetBytes(Weight), 0, bytes, 33, 4);
                          Array.Copy(BitConverter.GetBytes(Wanted), 0, bytes, 37, 1);
                          Array.Copy(Encoding.UTF8.GetBytes(IsEnd.ToString("dd.MM.yyyy")), 0, bytes, 38, 10);
                          Array.Copy(Encoding.UTF8.GetBytes(EcEnd.ToString("dd.MM.yyyy")), 0, bytes, 48, 10);
                      }
                      return bytes;
                  }
                  bytes = new byte[AddressSize];

                  Array.Copy(BitConverter.GetBytes(Valid), 0, bytes, 0, 1);
                  Array.Copy(BitConverter.GetBytes(Address), 0, bytes, 1, 4);
                  if (Valid)
                  {
                      pomBytes = Encoding.UTF8.GetBytes(Key);
                      Array.Copy(pomBytes, 0, bytes, 5, pomBytes.Length);
                  }
                  return bytes;
              }
              */



        public override Record FromByteArray(byte[] bitArray)
        {
            throw new NotImplementedException();
        }



        #endregion


    }
}
