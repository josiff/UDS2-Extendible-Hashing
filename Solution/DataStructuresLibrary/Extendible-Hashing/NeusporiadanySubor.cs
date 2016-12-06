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
        /// Je to prvy blok v subore - obsahuje nasledovne informacie:
        /// Prazdny blok - adresa
        /// Volny blok   - adresa
        /// Pocet Blokov - adresa
        /// </summary>
        private Block _prvyBlock;

        /// <summary>
        /// Pomocny blok temporarny. 
        /// </summary>
        private Block _tempBlock;
        /// <summary>
        /// Object FileStream - pouzime ho na citanie a zapisovanie do suboru. 
        /// </summary>
        private FileStream _fileStream;

        #endregion

        #region Konstruktor

        public NeusporiadanySubor(Block blok, string nazovSuboru)
        {
            _prvyBlock = blok;
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

        
        #region Metody - nacitanie, zapisanie - Blocku
        /// <summary>
        /// Metoda zapise blok do suboru. 
        /// </summary>
        /// <param name="adresaBloku"></param>
        /// <param name="poleBytov"></param>
        private void ZapisBlok(int adresaBloku, byte[] poleBytov)
        {
            //vypocitam kolko zabera prvy block
            int temp = adresaBloku*_prvyBlock.GetSize();

            //seeknem na dany index
            //a odtial zacnem potom zapisovat. 
            _fileStream.Seek(temp, SeekOrigin.Begin);
            //zapisem dane byty na dany index
            _fileStream.WriteAsync(poleBytov, 0, _prvyBlock.GetSize());
        }

        /// <summary>
        /// Metoda zapise prvy - nulty blok do suboru. 
        /// Prvy blok obsahuje informacie o prazdnom, volnom bloku, a poctu blokov. 
        /// </summary>
        private void ZapisPrvyBlok()
        {
            var poleBytov = new byte[_prvyBlock.GetSize()];
            int temp_index = 0;
            //prazdny blok 
            int temp = BitConverter.GetBytes(PrazdnyBlok).Length;
            Array.Copy(BitConverter.GetBytes(PrazdnyBlok), 0, poleBytov, temp_index, temp );
            temp_index += temp;
            //volny blok 
             temp = BitConverter.GetBytes(VolnyBlok).Length;
            Array.Copy(BitConverter.GetBytes(VolnyBlok), 0, poleBytov, temp_index, temp);
            temp_index += temp;
            //pocet blokov. 
            temp = BitConverter.GetBytes(PocetBlokov).Length;
            Array.Copy(BitConverter.GetBytes(PocetBlokov), 0, poleBytov, temp_index, temp);
          //Seeknem na zaciatok
            _fileStream.Seek(0, SeekOrigin.Begin);
            //zapisem dany blok na nulty index. 
            ZapisBlok(0, poleBytov);
        }
        /// <summary>
        /// Metoda precita prvy blok zo suboru. 
        /// Prvy blok obsahu informacie o tom, ktory blok je volny, do ktoreho sa da zapisat a kolko je tam bloko. 
        /// </summary>
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

        public Block PrecitajBlok(int adresaBloku)
        {
            //copy konstruktorom vytvorim novy block
            _tempBlock = new Block(_prvyBlock);
            //vytvorim pole bytov, ktore idem citat
            byte [] poleBytov = new byte[_tempBlock.GetSize()];
            //vycistim blok - nastavim dane recordy na nevalidne
            _tempBlock.VycistiBlok();
            //nastavim pomocnu premenu na velkost poctu bytov v bloku
            //je to index od ktoreho budem seekovat.
            int temp = _tempBlock.GetSize();
            //seeknem na dany index
            _fileStream.Seek(adresaBloku*temp, SeekOrigin.Begin);
            //precitam dane byty zo suboru
            _fileStream.Read(poleBytov, 0, temp);
            //nastavim blok z danych dat. 
            _tempBlock.FromByteArray(poleBytov, false);
            //vratim blok ktory som precitala.
            return _tempBlock;
        }

        #endregion

        #region MyRegion

        


        #endregion

    }
}
