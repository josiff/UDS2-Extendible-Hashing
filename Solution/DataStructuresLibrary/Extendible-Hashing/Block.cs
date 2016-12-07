using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DateStructureGeneral;

namespace DataStructuresLibrary.Extendible_Hashing
{
    /// <summary>
    /// Trieda Blok
    /// </summary>
    public class Block
    {
        #region Properties
        /// <summary>
        /// Pole zaznamov - Record
        /// </summary>
        public List<Record> PoleRecordov { get; set; }
        /// <summary>
        /// Hlbka bloku - znamena kolko bitov beriem z hash kluca
        /// </summary>
        public int Hlbka { get; set; }
        /// <summary>
        /// Pocet zaznamov, ktore obsahuje blok. 
        /// </summary>
        public int MaximalnyPocetZaznamov { get; set; }
        /// <summary>
        /// Velkost bloku v bytoch. 
        /// </summary>
        public static int VelkostZaznamu { get; set; }

        private Record _tempRecord;
        #endregion

        #region Konstruktor
        ///<summary>
        /// Konstruktor, ktory vytvori prazdny blok. 
        /// </summary>
        public Block(int maximalnyPocetZaznamov, int hlbka, int velkostZaznamu, Record r)
        {
            MaximalnyPocetZaznamov = maximalnyPocetZaznamov;
            PoleRecordov = new List<Record>(maximalnyPocetZaznamov);
            VelkostZaznamu = velkostZaznamu;
            Hlbka = hlbka;
            _tempRecord = r;
        }
        #endregion

        #region Overriden methods
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
                if (x != null)
                {
                    sb.AppendLine("Record: " + poradovneCisloZaznamu + "\t"
                             + x.ToString());
                }
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
            return VelkostZaznamu * MaximalnyPocetZaznamov + 4 + 4;
        }


        /// <summary>
        /// Metoda ktora mi skonvertuje blok dat do array bytov.
        /// Data (pole bytov) budu pouzite na ulozenie daneho bloku do suboru.
        /// </summary>
        /// <param name="hasAddress">True - ak ma adresu a kluc - pouzi GetAddressSize, inak GetSize.</param>
        /// <returns>>Blok skonvertovany do pole bytov</returns>
        public byte[] ToByteArray()
        {
            //v tejto premenej bude blok skonvertovany na byty
            byte[] poleBytov = new byte[GetSize()];
            //skonvertujem Kazdy prvok v poli zaznamov na byty a pridam ich do pola bytov. 

            int temp_index = 0;//pomocna premena na zistenie na ktorom indexe mam zapisat zaznam.
            //velkost adresy 
            //aktualny pocet zaznamov
            BitConverter.GetBytes(PoleRecordov.Count).CopyTo(poleBytov, temp_index);
            //cely 
            foreach (var x in PoleRecordov)
            {
                if (x != null)
                {
                    var array = x.ToByteArray();
                    Array.Copy(array, 0, poleBytov, temp_index, VelkostZaznamu);
                    temp_index += VelkostZaznamu;
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
        public void FromByteArray(byte[] byteArray)
        {
            if (PoleRecordov[0] == null)
            {
                return;
            }
            int temp_index = 0;
            //adresa prveho
            //pomocne pole bytov
            byte[] temp = new byte[GetSize()];
            //pocet zaznamov, tkore nacitavam
            int pocetZaznamov = BitConverter.ToInt32(byteArray, temp_index);
            temp_index = 4;
            int i = 0;

            for (int j = 0; j < pocetZaznamov; j++)
            {
                byte[] pomBytes = new byte[VelkostZaznamu];
                byteArray.CopyTo(pomBytes, temp_index);
                temp_index += VelkostZaznamu;
                //pridam novy record vytvorenych z danych bytov. 
                PoleRecordov.Add(_tempRecord.FromByteArray(pomBytes));
            }
        }
        #endregion

        #region Metody - na pracu s polom zaznamov - pridaj, najdi, vymaz. 
        /// <summary>
        /// Metoda prida zaznam do bloku. 
        /// </summary>
        /// <param name="record">Zaznam ktory chcem pridat. </param>
        /// <returns>Hodnotu adresy, kde bol pridany zaznam. </returns>
        public bool PridajRecord(Record record)
        {
            if (JePlny()) return false;
            else PoleRecordov.Add(record);
            return true;
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
                if (x != null)
                {
                    //a je rovnaky s danym recordom,
                    //tak ho vratim, inak pokracujem dalej. 
                    if (x.Equals(zaznam))
                    {
                        return x;
                    }
                }
            }
            //record sa nenasiel 
            //vratim null object, resp. defaultny
            return null;
        }
        /// <summary>
        /// Metoda vymaze zadany record z bloku. 
        /// Prakticky iba zmeni to, ze ho oznaci za neplatny. 
        /// </summary>
        /// <param name="record">Record ktory chcem vymazat. </param>
        public void VymazRecord(Record record)
        {
            PoleRecordov.Remove(record);
        }
        #endregion

        #region Dalsie metody
    /// <summary>
        /// Metoda zisti ci je blok plny. 
        /// </summary>
        /// <returns>Hodnota vyjadrujuca ci je blok plny alebo nie. </returns>
        public bool JePlny()
        {
            //vypocitam ho ako rozdiel medzi poctom zaznamou a poctom platnych, ak su tieto dva cisla rovnake, 
            //tak to znamena, ze blok je plny. 
            return (MaximalnyPocetZaznamov == PoleRecordov.Count);
        }


        /// <summary>
        /// Metoda zisti ci je nejake volne miesto pre nejaky zaznam v bloku. 
        /// </summary>
        /// <returns></returns>
        public int VolneMiesto()
        {
            //zistim ho odpocitanim poctom zaznamov od poctom platnych
            return MaximalnyPocetZaznamov - PoleRecordov.Count;
        }
        /// <summary>
        /// Metoda zisti ci blok je prazdny. 
        /// </summary>
        /// <returns></returns>
        public bool JePrazdny()
        {
            return PoleRecordov.Count == 0;
        }
        #endregion
        
    }
}
