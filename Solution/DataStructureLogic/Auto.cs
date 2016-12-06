﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
            return  hashcode;
        }


        public override int GetSize()
        {
          return _pocet_bytov_vin + _pocet_bytov_datum_ek + _pocet_bytov_datum_stk +
                   _pocet_bytov_evc + _pocet_bytov_hmotnost + _pocet_bytov_napravy + _pocet_bytov_vpatrani;
           
        }
        /// <summary>
        /// Velkost dat v bytov - adresa a kluc. 
        /// </summary>
        public override int GetAddressSize()
        {
            return _pocet_bytov_address + _pocet_bytov_isvalid + _pocet_bytov_key;

        }

        public override string ToString()
        {
            string s = "\nEVC: " + EvidencneCisloVozidla + "\tVIN cislo: " + VinCislo +
                       "\nPocet naprav: " + PocetNaprav + "\tPrevadzkova hmotnost: " + PrevadzkovaHmotnost
                       + "\tV patrani: " + ((VPatrani) ? "ANO" : "NIE")
                       + "\tKoniec platnosti STK: " + KoniecPlatnostiSTK.ToShortDateString()
                       + "\tKoniec platnosti EK: " + KoniecPlatnostiEK.ToShortDateString();
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

        public override bool Equals(object obj)
        {
            var temp = (Auto) obj;
            if (temp != null)
            {
                //ak je hash kluc rozny od null a rovnaju sa - vrati true
                if ((temp.Key != null) && temp.Key.Equals(Key))
                {
                    return true;
                }
                //porovnam ci sa rovnaku evidencne cisla
                if ((temp.EvidencneCisloVozidla !=null) && (temp.EvidencneCisloVozidla.Equals(EvidencneCisloVozidla)))
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
        /// <param name="allData"></param>
        /// <returns></returns>
        public override byte[] ToByteArray(bool allData)
        {
            //array bytov ktore vratim
            byte[] poleBytov;
            int temp_index = 0;
            if (allData)
            {
                //je validny
                poleBytov = new byte[GetSize()];
                Array.Copy(BitConverter.GetBytes(IsValid), 0, poleBytov, temp_index, _pocet_bytov_isvalid);
                temp_index += _pocet_bytov_isvalid;
                //adresa
                Array.Copy(BitConverter.GetBytes(Address), 0, poleBytov, temp_index, _pocet_bytov_address);
                temp_index += _pocet_bytov_address;

                if (IsValid)
                {
                    //evidencne cislo
                    Array.Copy(Encoding.UTF8.GetBytes(EvidencneCisloVozidla), 0, poleBytov, temp_index, _pocet_bytov_evc);
                    temp_index += _pocet_bytov_evc;
                    //vin cislo
                    Array.Copy(Encoding.UTF8.GetBytes(VinCislo), 0, poleBytov, temp_index, _pocet_bytov_vin);
                    temp_index += _pocet_bytov_vin;
                    //pocet naprav
                    Array.Copy(BitConverter.GetBytes(PocetNaprav), 0, poleBytov, temp_index, _pocet_bytov_napravy);
                    temp_index += _pocet_bytov_napravy;
                    //prevadzkova hmotnost
                    Array.Copy(BitConverter.GetBytes(PrevadzkovaHmotnost), 0, poleBytov, temp_index,
                        _pocet_bytov_hmotnost);
                    temp_index += _pocet_bytov_napravy;
                    // je v patrani
                    Array.Copy(BitConverter.GetBytes(VPatrani), 0, poleBytov, temp_index, _pocet_bytov_vpatrani);
                    temp_index += _pocet_bytov_vpatrani;
                    //koniec platnosti stk
                    Array.Copy(Encoding.UTF8.GetBytes(KoniecPlatnostiSTK.ToString("dd.MM.yyyy")), 0, poleBytov,
                        temp_index, _pocet_bytov_datum_stk);
                    temp_index += _pocet_bytov_datum_stk;
                    //koniec platnosti ek
                    Array.Copy(Encoding.UTF8.GetBytes(KoniecPlatnostiEK.ToString("dd.MM.yyyy")), 0, poleBytov,
                        temp_index, _pocet_bytov_datum_ek);
                    // temp_index += _pocet_bytov_datum_ek;
                    return poleBytov;
                }
            }

            //ak chcem vratit iba adresu, a hashovaciu hodnotu
            poleBytov = new byte[GetAddressSize()];
            temp_index = 0;
            //is valid
            Array.Copy(BitConverter.GetBytes(IsValid), 0, poleBytov, temp_index, _pocet_bytov_isvalid);
            temp_index += _pocet_bytov_isvalid;
            //adresa
            Array.Copy(BitConverter.GetBytes(Address), 0, poleBytov, temp_index, _pocet_bytov_address);
            temp_index += _pocet_bytov_address;
            if (IsValid)
            {
                //zapisem hashovaci klud
                Array.Copy(Encoding.UTF8.GetBytes(Key), 0, poleBytov, temp_index, Encoding.UTF8.GetBytes(Key).Length);
            }
            return poleBytov;
            
        }
        public override void FromByteArray(byte[] byteArray, bool hasAdress = true)
        {
            int temp_index = 0;
            //precitam prvu hodnotu na prvom bite - ci je validne alebo nie
            IsValid = BitConverter.ToBoolean(byteArray, 0);
            temp_index += _pocet_bytov_isvalid;
            //ak ma adresu fa je validne
          //ak je validne tak priradim 
                if (!hasAdress &&IsValid)
                {
                    //adresa
                    Address = BitConverter.ToInt32(byteArray, temp_index);
                    temp_index += _pocet_bytov_address;
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
                    temp_index += _pocet_bytov_datum_stk;
                    //koniec platnosti ek
                    // temp_index += _pocet_bytov_datum_ek;
            }
            else if (hasAdress && IsValid)
            {
                //adresa
                Address = BitConverter.ToInt32(byteArray, temp_index);
                temp_index += _pocet_bytov_address;
                //hash key
                Key = Encoding.UTF8.GetString(byteArray, temp_index, Encoding.UTF8.GetBytes(Key).Length).Trim('0');
            }
        }


       

        #endregion
    }
}
