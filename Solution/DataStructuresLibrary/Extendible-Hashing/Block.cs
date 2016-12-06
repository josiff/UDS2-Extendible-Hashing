using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresLibrary.Extendible_Hashing
{
    /// <summary>
    /// Trieda Blok
    /// </summary>
    public class Block
    {
        #region Properties
        /// <summary>
        /// Adresa prveho zaznamu v bloku. 
        /// </summary>
        public int AdresaPrvehoRecordu { get; set; }

        /// <summary>
        /// Pole zaznamov - Record
        /// </summary>
        public Record[] PoleRecordov { get; set; }
        /// <summary>
        /// Hlbka bloku - znamena kolko bitov beriem z hash kluca
        /// </summary>
        public int Hlbka { get; set; }
        /// <summary>
        /// Pocet zaznamov, ktore obsahuje blok. 
        /// </summary>
        public int PocetZaznamov { get; set; }
        /// <summary>
        /// Pocet Platnych zaznamov v bloku
        /// </summary>
        public int PocetPlatnych { get; set; }
        /// <summary>
        /// Smernik na Dalsi volny Blok. 
        /// </summary>
        public Block DalsiVolny { get; set; }
        /// <summary>
        /// Velkost bloku v bytoch. 
        /// </summary>
        #endregion

        #region Konstruktor

        public Block(int pocetZaznamov)
        {
            PocetZaznamov = pocetZaznamov;
            PocetPlatnych = 0;
            PoleRecordov = new Record[pocetZaznamov];
            AdresaPrvehoRecordu = -1;
        }

        #endregion

        #region Overriden methods
     /*   public bool Equals(object obj)
        {
            return false;
        }

    */
        /// <summary>
        /// METODA vypise vsetky informacie o bloku
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            //poradove cislo zaznamu
            int poradovneCisloZaznamu = 0;
            foreach (var x in PoleRecordov)
            {
                poradovneCisloZaznamu++;
                sb.AppendLine("Record: " + poradovneCisloZaznamu + "\t"
                              + x.ToString());
            }

            return sb.ToString();
        }
        #endregion

        #region Metody - ToByteArray, FromByteArray
        /// <summary>
        /// Vrati velkost bloku v bytoch
        /// Vypocitam ako - velkost jedneho zaznamu a ten vynasobim poctom zaznamov. 
        /// </summary>
        /// <returns></returns>
        public int GetSize()
        {
            return  BitConverter.GetBytes(AdresaPrvehoRecordu).Length +
          +PoleRecordov[0].GetSize()*PocetZaznamov;
        } 
        /// <summary>
        /// Vrati velkost bloku v bytoch - adresy
        /// Vypocitam ako - velkost jedneho zaznamu (iba adresa a blok) a ten vynasobim poctom zaznamov. 
        /// </summary>
        /// <returns></returns>
        public int GetAddressSize()
        {
            return  BitConverter.GetBytes(AdresaPrvehoRecordu).Length + 
           + PoleRecordov[0].GetAddressSize()*PocetZaznamov;
        }
        
    
        /// <summary>
        /// Metoda ktora mi skonvertuje blok dat do array bytov.
        /// Data (pole bytov) budu pouzite na ulozenie daneho bloku do suboru.
        /// </summary>
        /// <param name="hasAddress">True - ak ma adresu a kluc - pouzi GetAddressSize, inak GetSize.</param>
        /// <returns>>Blok skonvertovany do pole bytov</returns>
        public byte[] ToByteArray(bool hasAddress = false)
        {
            //v tejto premenej bude blok skonvertovany na byty
            byte[] poleBytov = new byte[hasAddress? GetAddressSize() : GetSize()];
            //skonvertujem Kazdy prvok v poli zaznamov na byty a pridam ich do pola bytov. 
          
            int temp_index = 0;//pomocna premena na zistenie na ktorom indexe mam zapisat zaznam.
            //velkost adresy 
            int temp = BitConverter.GetBytes(AdresaPrvehoRecordu).Length;
            Array.Copy(BitConverter.GetBytes(AdresaPrvehoRecordu), 0, poleBytov, temp_index, temp);
            temp_index += temp;

            foreach (var x in PoleRecordov)
            {
                if (hasAddress)
                {
                    temp_index += x.GetAddressSize();
                    Array.Copy(x.ToByteArray(true), 0, poleBytov, temp_index, x.GetAddressSize());
                }
                else
                {
                    temp_index +=  x.GetSize();
                    Array.Copy(x.ToByteArray(false), 0, poleBytov, temp_index, x.GetSize());
               }
            }
            return poleBytov;
        }

        /// <summary>
        /// Metoda naplni tento blok datami z array of bytov
        /// Zo suboru co mi prislo, tak naplnim record. 
        /// </summary>
        /// <param name="byteArray">pole bytov<param>
        /// <param name="hasAdress"></param>
        public void FromByteArray(byte[] byteArray, bool hasAdress = true)
        {
            //pomocne pole bytov
            byte[] temp = hasAdress
                ? new byte[PoleRecordov[0].GetAddressSize()]
                : new byte[PoleRecordov[0].GetSize()];
            //premena na pomoc s indexom pri kopirovani arrayov bytov. 
            int temp_index = 0;
            //adresa prveho
            AdresaPrvehoRecordu = BitConverter.ToInt32(byteArray, 0);
            temp_index = BitConverter.GetBytes(AdresaPrvehoRecordu).Length;
            int i = 0;
            foreach (var x in PoleRecordov)
            {
                 if (hasAdress)
                 {
                     temp_index += x.GetAddressSize();
                    Array.Copy(byteArray, temp_index, temp, 0, x.GetAddressSize());
                }
                else
                {
                    temp_index += x.GetAddressSize();
                    Array.Copy(byteArray, temp_index, temp, 0, x.GetSize());
                }
                //pridam novy record z pomocnych bytov
                x.FromByteArray(temp,hasAdress);
            }
          }

        #endregion


        #region Metody - na pracu s polom zaznamov - pridaj, najdi, vymaz. 
        /// <summary>
        /// Metoda prida zaznam do bloku. 
        /// </summary>
        /// <param name="record">Zaznam ktory chcem pridat. </param>
        /// <returns>Hodnotu adresy, kde bol pridany zaznam. </returns>
        public int PridajRecord(Record record)
        {
            //ak pridavam prvy record, tak tam nastavim aj adresu prveho recordu. 
            if (AdresaPrvehoRecordu == -1 || PocetPlatnych == 0)
            {
                PoleRecordov[0] = record;
                AdresaPrvehoRecordu = record.Address;
                PocetPlatnych++;
                return 0;
            }
            for (int i = 0; i < PocetZaznamov; i++)
            {
                //ak record nie je validny, tak tam pridam dany record. 
                if (!PoleRecordov[i].IsValid)
                {
                    PoleRecordov[i] = record;
                    PocetPlatnych++;
                    return i; 
                }
            }
            //nebol pridany, pretoze uz tam nie su ziadne volne bloky. 
            return -1;
        }
        /// <summary>
        /// Metoda najde dany record v bloku. 
        /// Ak je validny a rovnaky, tak ho vrati. inak null object. 
        /// </summary>
        /// <param name="zaznam">Record, ktory hladam</param>
        /// <returns>Record, ktory hladam. </returns>
        public Record NajdiRecord(Record zaznam)
        {
            foreach (var x in PoleRecordov)
            {
                //ak je record validny 
                //a je rovnaky s danym recordom,
                //tak ho vratim, inak pokracujem dalej. 
                if (x.IsValid && x.Equals(zaznam))
                {
                    return x;
                }
            }
            //record sa nenasiel :( 
            //vratim null object, resp. defaultny
            return default(Record);
        }
        /// <summary>
        /// Metoda vymaze zadany record z bloku. 
        /// Prakticky iba zmeni to, ze ho oznaci za neplatny. 
        /// </summary>
        /// <param name="record">Record ktory chcem vymazat. </param>
        public void VymazRecord(Record record)
        {
            foreach (var x in PoleRecordov)
            {
                if (x.Equals(record))
                {
                    x.IsValid = false;
                    PocetPlatnych--;
                    break;
                }
            }
        }

        #endregion

        #region Dalsie metody
        /// <summary>
        /// Metoda vycisti blok. 
        /// Oznaci vsetky zaznamy ako nevalidne. 
        /// </summary>
        public void VycistiBlok()
        {
            foreach (var x in PoleRecordov)
            {
                x.IsValid = false;
                PocetPlatnych--;
            }
        }
        /// <summary>
        /// Metoda zisti ci je blok plny. 
        /// </summary>
        /// <returns>Hodnota vyjadrujuca ci je blok plny alebo nie. </returns>
        public bool JePlny()
        {
            //vypocitam ho ako rozdiel medzi poctom zaznamou a poctom platnych, ak su tieto dva cisla rovnake, 
            //tak to znamena, ze blok je plny. 
            return (PocetZaznamov == PocetPlatnych);
        }

        public bool JePlnyCezCyklus()
        {
            foreach (var x in PoleRecordov)
            {
                if (!x.IsValid)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Metoda zisti ci je nejake volne miesto pre nejaky zaznam v bloku. 
        /// </summary>
        /// <returns></returns>
        public int VolneMiesto()
        {
            //zistim ho odpocitanim poctom zaznamov od poctom platnych
            return PocetZaznamov - PocetPlatnych;
        }

        public int VolneMiestoCezCyklus()
        {
            int pocetVolnych = 0;
            foreach (var x in PoleRecordov)
            {
                if (!x.IsValid)
                {
                    pocetVolnych++;
                }
            }
            return pocetVolnych;
        }
        /// <summary>
        /// Metoda zisti ci blok je prazdny. 
        /// </summary>
        /// <returns></returns>
        public bool JePrazdny()
        {
            return PocetPlatnych == 0;
        }

        public bool JePrazdnyCezCyklus()
        {
            foreach (var x in PoleRecordov)
            {
                if (x.IsValid)
                {
                    return false;
                }
            }
            return true;
        }

        #endregion




    }
}
