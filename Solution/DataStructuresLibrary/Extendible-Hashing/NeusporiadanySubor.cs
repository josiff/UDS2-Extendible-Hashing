using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresLibrary.Extendible_Hashing
{
    /// <summary>
    /// Trieda - Neusporiadany subor. 
    /// </summary>
    public class NeusporiadanySubor
    {
        #region Vlastnosti
        /// <summary>
        /// Adresa prveho prazdneho bloku. 
        /// </summary>
        public int PrazdnyBlok { get;  set; }
        /// <summary>
        /// Adresa prveho volneho bloku s volnym miestom. 
        /// Blok ktory nie je plny. 
        /// </summary>
        public int VolnyBlok { get; set; }
        /// <summary>
        /// Pocet Blokov v subore. 
        /// </summary>
        public int PocetBlokov { get; set; }
        /// <summary>
        /// Adresa, bloku, ktory prave editujem. 
        /// </summary>
        public int AddressEditedBlock { get; set; }
        #endregion

        #region Pomocne property

        /// <summary>
        /// Pomocny block. Temporarny. 
        /// </summary>
        private Block _tempBlock;

        /// <summary>
        /// Pomocny blok temporarny 2. 
        /// </summary>
        private Block _tempBlock2;
        /// <summary>
        /// Object FileStream - pouzime ho na citanie a zapisovanie do suboru. 
        /// </summary>
        private FileStream _fileStream;

        #endregion

        #region Konstruktor

        public NeusporiadanySubor(Block blok, string nazovSuboru)
        {
            _tempBlock = blok;
            //zistim ci subor nahodou neexistuje
            if (File.Exists(nazovSuboru))
                using (_fileStream = new FileStream(nazovSuboru, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    PrecitajPrvyBlok();
                }
            else
                using (_fileStream = new FileStream(nazovSuboru, FileMode.Create, FileAccess.ReadWrite))
                {
                    //nastavim defaultne hodnoty
                    PrazdnyBlok = -1;
                    VolnyBlok = -1;
                    PocetBlokov = 0;
                    //Zapisem blok do suboru
                    ZapisPrvyBlok();
                }
        }

        #endregion



        #region Metody

        private void ZapisBlok(int adresaBloku, byte[] poleBytov)
        {
            //vypocitam kolko zabera prvy block
            int temp = adresaBloku*_tempBlock.GetSize();

            //seeknem na dany index
            //a odtial zacnem potom zapisovat. 
            _fileStream.Seek(temp, SeekOrigin.Begin);
            //zapisem dane byty na dany index
            _fileStream.WriteAsync(poleBytov, 0, _tempBlock.GetSize());
        }

        /// <summary>
        /// Metoda zapise prvy - nulty blok do suboru. 
        /// Prvy blok obsahuje informacie o prazdnom, volnom bloku, a poctu blokov. 
        /// </summary>
        private void ZapisPrvyBlok()
        {
            var poleBytov = new byte[_tempBlock.GetSize()];
            int temp_index = 0;
            int temp = BitConverter.GetBytes(PrazdnyBlok).Length;
            Array.Copy(BitConverter.GetBytes(PrazdnyBlok), 0, poleBytov, temp_index, temp );
            temp_index += temp;
             temp = BitConverter.GetBytes(VolnyBlok).Length;
            Array.Copy(BitConverter.GetBytes(VolnyBlok), 0, poleBytov, temp_index, temp);
            temp_index += temp;
            temp = BitConverter.GetBytes(PocetBlokov).Length;
            Array.Copy(BitConverter.GetBytes(PocetBlokov), 0, poleBytov, temp_index, temp);
          //Seeknem na zaciatok
            _fileStream.Seek(0, SeekOrigin.Begin);
            //zapisem dany blok na nulty index. 
            ZapisBlok(0, poleBytov);
        }

        private void PrecitajPrvyBlok()
        {
            //pocet vsetkych bytov troch premennych 
            int temp = BitConverter.GetBytes(PocetBlokov).Length + BitConverter.GetBytes(VolnyBlok).Length + BitConverter.GetBytes(PocetBlokov).Length;
            var poleBytov = new byte[temp];
            //precimam vsetke byty
            _fileStream.Read(poleBytov, 0, temp);
            //nastavim atributy podla toho co som precitala a indexu
            temp = 0; 
            //prazdny blok
            PrazdnyBlok = BitConverter.ToInt32(poleBytov, temp);
            temp = BitConverter.GetBytes(PrazdnyBlok).Length;
            //volny blok
            VolnyBlok = BitConverter.ToInt32(poleBytov, temp);
            temp += BitConverter.GetBytes(VolnyBlok).Length;
            //pocet blokov
            PocetBlokov = BitConverter.ToInt32(poleBytov, temp);temp = 0; 
         }

        #endregion

    }
}
