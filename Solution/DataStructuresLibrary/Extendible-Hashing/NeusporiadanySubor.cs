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
        public int PrazdnyBlok { get; set; }
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
        /// Je to lubovolny blok, pouzivam ho takmer vo vsetkych metodach 
        /// alebo 
        /// Je to prvy blok v subore - obsahuje nasledovne informacie:
        /// Prazdny blok - adresa
        /// Volny blok   - adresa
        /// Pocet Blokov - adresa
        /// </summary>
        private Block _prvyBlock;

        /// <summary>
        /// Pomocny blok temporarny. 
        /// pouzite - pri citani bloku 
        /// pri mazani bloku 
        /// </summary>
        private Block _tempBlock;
        /// <summary>
        /// Object FileStream - pouzime ho na citanie a zapisovanie do suboru. 
        /// </summary>
        private FileStream _fileStream;

        #endregion

        #region Konstruktor a toString
        /// <summary>
        /// Konstruktor neusporiadaneho suboru
        /// Blok 
        /// nazov suboru
        /// </summary>
        /// <param name="blok"></param>
        /// <param name="nazovSuboru"></param>
        public NeusporiadanySubor(Block blok, string nazovSuboru)
        {
            _prvyBlock = blok;
            //zistim ci subor nahodou neexistuje
            if (File.Exists(nazovSuboru))
            {
                _fileStream = new FileStream(nazovSuboru, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                PrecitajPrvyBlok();
            }
            else

            {
                _fileStream = new FileStream(nazovSuboru, FileMode.Create, FileAccess.ReadWrite);
                //nastavim defaultne hodnoty
                PrazdnyBlok = -1;
                VolnyBlok = -1;
                PocetBlokov = 0;
                //Zapisem blok do suboru
                ZapisPrvyBlok();
            }
        }
        /// <summary>
        /// Metoda zobrazi informacie neutriedeneho suboru. 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Prvy blok:");
            sb.AppendLine("Prazdny blok: " + PrazdnyBlok
                         + "\tVolny blok: " + VolnyBlok
                         + "\tPocet blokov: " + PocetBlokov);
            //vypisem jednotlive bloky
            //prechadzam vsetky bloky a postupne ich citam zo suboru. 
            for (int i = 0; i < PocetBlokov; i++)
            {
                _prvyBlock = PrecitajBlok(i);
                sb.AppendLine("Blok " + i + "\t" + _prvyBlock);
            }
            return sb.ToString();
        }

        #endregion


        #region Metody - nacitanie, zapisanie - Blocku
        /// <summary>
        /// Metoda zapise blok do suboru. 
        /// </summary>
        /// <param name="adresaBloku"></param>
        /// <param name="poleBytov"></param>
        public void ZapisBlok(int adresaBloku, byte[] poleBytov)
        {
            //vypocitam kolko zabera prvy block
            int temp;
            if (_prvyBlock.PoleRecordov[0] == null)
            {
                temp = 12;
            }
            else
            {
               temp = adresaBloku * _prvyBlock.GetSize();
            }
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
            var poleBytov = new byte[_prvyBlock.GetSize()+ 12];
            int temp_index = 0;
            //prazdny blok 
            int temp = BitConverter.GetBytes(PrazdnyBlok).Length;
            Array.Copy(BitConverter.GetBytes(PrazdnyBlok), 0, poleBytov, temp_index, temp);
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
            PocetBlokov = BitConverter.ToInt32(poleBytov, temp); temp = 0;
        }

        public Block PrecitajBlok(int adresaBloku)
        {
            //copy konstruktorom vytvorim novy block
            _tempBlock = new Block(_prvyBlock);
            //vytvorim pole bytov, ktore idem citat
            byte[] poleBytov = new byte[_tempBlock.GetSize()];
            //vycistim blok - nastavim dane recordy na nevalidne
            _tempBlock.VycistiBlok();
            //nastavim pomocnu premenu na velkost poctu bytov v bloku
            //je to index od ktoreho budem seekovat.
            int temp = _tempBlock.GetSize();
            if (adresaBloku < 0) adresaBloku = 0;
            //seeknem na dany index
            _fileStream.Seek(adresaBloku * temp, SeekOrigin.Begin);
            //precitam dane byty zo suboru
            _fileStream.Read(poleBytov, 0, temp);
            //nastavim blok z danych dat. 
            _tempBlock.FromByteArray(poleBytov, false);
            //vratim blok ktory som precitala.
            return _tempBlock;
        }

        public void VymazBlok()
        {
            _fileStream.SetLength(_fileStream.Length - _prvyBlock.GetSize());
            PocetBlokov--;
        }

        #endregion

        #region Zapis a Nacitanie Recordu (zaznamu)
        /// <summary>
        /// Metoda precita zaznam zo suboru na zadanej adrese v bloku. 
        /// </summary>
        /// <param name="adresaBloku"></param>
        /// <param name="record"></param>
        /// <param name="editovanie"></param>
        /// <returns></returns>
        public Record PrecitajZaznam(int adresaBloku, Record record, bool editovanie)
        {
            _prvyBlock = PrecitajBlok(adresaBloku);
            if (editovanie)
            {
                AddressEditedBlock = adresaBloku;
            }
            return _prvyBlock.NajdiRecord(record);
        }

        /// <summary>
        /// Metoda zapise dany zaznam resp. record do suboru, resp. aj bloku.
        /// </summary>
        /// <param name="record">Record, ktory chcem zapisat do suboru. </param>
        /// <returns>Adresa na ktoru zapisalo do suboru. </returns>
        public int ZapisZaznam(Record record)
        {
            //vycistim prvy blok.aby som do neho mohla potom nacitat blok.  
            _prvyBlock.VycistiBlok();
            //nastavim adresu bolu na najblizsiu volnu adresu. 
            int adresaBloku = VolnyBlok;
            //ak je adresa plna, tak zapisem do najblizsieho volneho bloku. 
            if (adresaBloku < 0)
            {
                adresaBloku = PrazdnyBlok;
                //ak je adresa bloku - nenastavena
                //zapisem novy blok do suboru
                if (adresaBloku < 0)
                {
                    //nastavim sa na dany index od ktoreho idem citat 
                    //resp. na koniec
                    _fileStream.Seek(0, SeekOrigin.End);
                    //zvacsim adresu bolou o jedna
                    adresaBloku = PocetBlokov + 1;
                    //pridam zaznam do bloku
                    _prvyBlock.PridajRecord(record);
                    //ak je pocet zaznamov v bloku vacsi ako jeden, 
                    //tak to pridam do volneho bloku. 
                    if (_prvyBlock.PocetPlatnych > 1)
                    {
                        _pridajDoVolnehoBloku(adresaBloku);
                    }
                    //zapis blok do suboru
                    _fileStream.WriteAsync(_prvyBlock.ToByteArray(), 0, _prvyBlock.GetSize());
                    //zvysim pocet blokov
                    PocetBlokov++;
                    //vratim adresu bloku, na ktoru som to zapisala
                    return adresaBloku;
                }
                // ak je adresa bloku definovana
                //precitam blok na danej adrese
                _prvyBlock = PrecitajBlok(adresaBloku);
                //pridam zaznam do bloku
                _prvyBlock.PridajRecord(record);
                //vymazem blok z prazdneho 
                _vymazBlokZPrazdnychBlokov(adresaBloku);
                //zistim ci prvy blok obsahuje nejaky rekord
                if (_prvyBlock.PocetPlatnych > 1)
                {
                    //tak to pridam do volneho bloku. 
                    _pridajDoVolnehoBloku(adresaBloku);
                }

            }
            //ak mam platnu adresu bloku
            //ak adresa ukazuje na nejaky existujuci blok. 
            else
            {
                //precitam blok z danej adresy
                _prvyBlock = PrecitajBlok(adresaBloku);
                //pridam zaznam do bloku 
                _prvyBlock.PridajRecord(record);
                //ak je blok plny 
                if (_prvyBlock.JePlny())
                {
                    //ak je pocet platnych platny
                    //resp. budem mat z coho vymazat
                    if (_prvyBlock.PocetPlatnych > 1)
                    {
                        _vymazBlokZVolnychBlokov(adresaBloku);
                    }
                }
            }
            ZapisBlok(adresaBloku, _prvyBlock.ToByteArray());
            //vrat adresu bloku do ktoreho sa to zapisalo
            return adresaBloku;
        }

        public void UpravZaznam(Record staryRecord, Record novyRecord)
        {
            //vymazem stary record
            _prvyBlock.VymazRecord(staryRecord);
            //pridam novy record
            _prvyBlock.PridajRecord(novyRecord);
            //zapisem do suboru
            ZapisBlok(AddressEditedBlock, _prvyBlock.ToByteArray());
        }

        public Record VymazZaznam(int adresaBloku, Record record)
        {
            record = PrecitajZaznam(adresaBloku, record, false);
            //zistim ci je prvy blok plny
            bool plny = _prvyBlock.JePlny();
            //nastavim record na neplatny
            record.IsValid = false;
            //ak je prazdny
            if (_prvyBlock.JePrazdny())
            {
                //ak je adresa bloku rovnaka ako pocet blokov
                if (adresaBloku == PocetBlokov)
                {
                    //Vymaz blok
                    VymazBlok();
                    //ak bosahuej nejake platne data
                    //tak to vymazem z bloku s volnymi blokmi. 
                    if (_prvyBlock.PocetPlatnych > 1)
                    {
                        _vymazBlokZVolnychBlokov(adresaBloku);
                    }
                    adresaBloku--;
                    //precitam blok z nasledujucej adresy a ak je tam nejaky
                    //volny blok tak ho vymazem 
                    while (adresaBloku != 0)
                    {
                        _prvyBlock = PrecitajBlok(adresaBloku);
                        //ak je blok prazdny - tak ho vymazem 
                        if (_prvyBlock.JePrazdny())
                        {
                            VymazBlok();
                            _vymazBlokZPrazdnychBlokov(adresaBloku);
                            adresaBloku--;
                        }
                        else
                        {
                            //ak nie je prazdny,tak vrat dany record, data sa uz vymazali, 
                            //a ako aj prazdne bloky, tak je cas sa vratit a skoncit. 
                            return record;
                        }
                    }

                }
                else
                {
                    //vymaz z volnych blokov
                    if (_prvyBlock.PocetPlatnych > 1)
                    {
                        _vymazBlokZVolnychBlokov(adresaBloku);
                    }
                    //pridaj do prazdneho bloku
                    _pridajDoPrazdneho(adresaBloku);
                }

            }
            else if (plny)
            {
                //pripadne ak je plny, tak ho pridam do volnych blokov 
                if (_prvyBlock.PocetPlatnych > 1)
                {
                    _pridajDoVolnehoBloku(adresaBloku);
                }
                ZapisBlok(adresaBloku, _prvyBlock.ToByteArray());
            }
            else
            {
                //inak 
                //zapis do bloku 
                ZapisBlok(adresaBloku, _prvyBlock.ToByteArray());
            }
            return record;
        }

        #endregion

        #region Pomocne metody pre Zapis, citanie Zaznamov
        /// <summary>
        /// Privatna metoda, ktora sa pouziva iba v danej triede. 
        /// Metoda prida do temporarnej prementej _prvyBlock adresu
        /// a nastavy volny blok suboru na danu adresu. 
        /// </summary>
        /// <param name="adresaBloku"></param>
        private void _pridajDoVolnehoBloku(int adresaBloku)
        {
            _prvyBlock.AdresaPrvehoRecordu = adresaBloku;
            VolnyBlok = adresaBloku;
        }

        private void _pridajDoPrazdneho(int adresaBloku)
        {
            //adresu prveho bloku nastavim na prazdny blok 
            _prvyBlock.AdresaPrvehoRecordu = PrazdnyBlok;
            //zapisem blok s danou adresou
            ZapisBlok(adresaBloku, _prvyBlock.ToByteArray());
            //ak je nastavena adresa volneho bloku, tak tam zapisem prazdny blok. 
            if (VolnyBlok != -1)
            {
                _prvyBlock = PrecitajBlok(PrazdnyBlok);
                ZapisBlok(PrazdnyBlok, _prvyBlock.ToByteArray());
            }
            VolnyBlok = adresaBloku;
        }


        private void _vymazBlokZPrazdnychBlokov(int adresaMazaneho)
        {
            int adrsesaPrvehoRecordu = _tempBlock.AdresaPrvehoRecordu;
            //ak mazane sa rovna praznemu bloku 
            if (adresaMazaneho == PrazdnyBlok)
            {
                PrazdnyBlok = adrsesaPrvehoRecordu;
                _prvyBlock.AdresaPrvehoRecordu = -1;
            }
            //ak adresa je rozna -1, resp. musi byt nastavena
            if (adrsesaPrvehoRecordu != -1)
            {
                //precitam prazdny blok 
                PrecitajBlok(adrsesaPrvehoRecordu);
                //zapisem druhu premenu blok do suboru
                ZapisBlok(adrsesaPrvehoRecordu, _tempBlock.ToByteArray());
            }
        }

        private void _vymazBlokZVolnychBlokov(int adresaMazaneho)
        {
            int rodic = VolnyBlok;
            //adresa prveho bloku - temporarneho
            int vymazPotomka = _prvyBlock.AdresaPrvehoRecordu;
            if (VolnyBlok == adresaMazaneho)
            {
                //nastavim adresu prveho pomocneho bloku na -1
                _prvyBlock.AdresaPrvehoRecordu = -1;
                //nastavim adresu volneho bloku na danu adresu
                VolnyBlok = vymazPotomka;
                //vratim sa spat
                return;
            }
            //ak nie je to volny blok, tak hladam z danych blokov
            while (rodic != -1)
            {
                //precitam blok z druheho temp bloku
                _tempBlock = PrecitajBlok(rodic);

                if (_tempBlock.AdresaPrvehoRecordu == adresaMazaneho)
                {
                    ZapisBlok(rodic, _tempBlock.ToByteArray());
                    return;
                }
                //prechadzam dovtedy, potkym sa nenastavi rodic na nejaku inu hodnotu ako -1
                rodic = _tempBlock.AdresaPrvehoRecordu;

            }
        }


        #endregion

    }
}
