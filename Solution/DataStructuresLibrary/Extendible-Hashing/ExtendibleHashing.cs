using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresLibrary.Extendible_Hashing
{
    public class ExtendibleHashing<T> where T : Record
    {
        #region Property

        /// <summary>
        /// Adresar - dynamicke pole celych cisel
        /// Obsahuju Blockn 
        /// </summary>
        public List<int> Adresar { get; set; }

        /// <summary>
        /// Hlbka Hesovacieho suboru  - D
        /// </summary>
        public int HlbkaSuboru { get; set; }

        public int PocetBlokov { get; set; }
        public ExFile Subor { get; set; }

        public int VelkostZaznamu { get; set; }
        public int MaxPocetZaznamovVBloku { get; private set; }
        private Record _tempRecord;
        #endregion

        public ExtendibleHashing(string filename, int maxPocetZaznamovBloku, Record record, bool createNew = false)
        {
            VelkostZaznamu = record.GetSize();
            MaxPocetZaznamovVBloku = maxPocetZaznamovBloku;
            _tempRecord = record;
            Subor = new ExFile(filename, maxPocetZaznamovBloku, record, createNew);
            Adresar = new List<int>(maxPocetZaznamovBloku);
            Adresar.Add(0);
            Adresar.Add(1);
            PocetBlokov++;
            PocetBlokov++;
            HlbkaSuboru =1;
            Subor.AlokujNovyBlock();
            Subor.AlokujNovyBlock();

            Block b = new Block(maxPocetZaznamovBloku, record);
            Subor.WriteBlok(0, b);
            Subor.WriteBlok(1, b);
        }

        /// <summary>
        /// Metoda zobrazi informacie hashovatelneho suboru. 
        /// </summary>
        /// <returns></returns>


        #region Methods
        /// <summary>
        /// Operacia Vloz.  
        /// Efektivnost: 1 prenos, v pripade preplenia 2 prenosy. 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Insert(Record data)
        {
            bool vlozene = false;
            while (!vlozene)
            {
                //
                //        00   01  10   11      hlbka suboru 2 = hlbka heshovacieho suboru => D
                //      | 0 | 0 |  1  | 2 |     Adresar - dynamicke pole celych cisiel 
                //        \  /     |     \
                //       block1   b2      b3
                //hlbka:    1      2       2           Hlbky blokov => d
                //        0 bla    10 mb  11 st        Bloky dat     
                //        0 hee    10 na  11 wt         

                //vypocitam hash vkladaneho recordu. 
               //vypocitam index v adresari 
                int index = IndexSubAdresara(data.GetHash(), HlbkaSuboru);
                int adresaBloku = Adresar[index];
                //nacitam blok zo suboru - na zadanej adrese. 
                Block block = Subor.ReadBlok(adresaBloku);
                
               // Console.WriteLine("Index v adresari: " + index + ", z hash funkcie: "+ data.GetHash() + ", hlbka suboru: " + HlbkaSuboru + ", hlbka bloku: " + block.Hlbka);

                // 1. Ak sa blok preplni
                //     => nebuduj skupinu preplnujucich blokov, namiesto toho 
                //     => zvacsi adresovy priestor tak, aby sa adresovali bloky
                //         a nie skupiny blokov. 
                if (block.JePlny())
                {

                    // 2. Pri preplneni alokuj novy blok 
                    //     => prirad mu adresu v adresari. 

                    // 3. Adresovy priestor zvacsi, tak ze ho
                    //     =>  zdvojnasobis (D = D + 1) 
                    //             => alokujes nove pole vacsie o 100%
                    //             => kazda honota z povodneho pola sa nakopiruje dvakrat
                    //     => zaktualizujes adresy v adresari.  

                    //ak hlbka bloku je rovnaka ako hlbka suboru
                    //d == D
                    if (block.Hlbka == HlbkaSuboru)
                    {
                       //zdvojnasob adresar
                        ZdvojnasobAdresar();
                        HlbkaSuboru++;
                   }
                 
                    int hlbkanova = block.Hlbka + 1;

                    int adresaNovehoBloku = Subor.AlokujNovyBlock();
                    Block novyBlock = new Block(MaxPocetZaznamovVBloku, hlbkanova, _tempRecord);
                   
                    //podla bajtu na danej adrese v recorde prerozdelim - 0 povodny, 1 - novy blok. 
                    PrerozdelenieBlokov( block, hlbkanova, novyBlock);
                    //zaktualizujem adresy v adresari
                    ZaktualizujAdresyAdresara(data, block, hlbkanova, adresaNovehoBloku);
                    block.Hlbka = hlbkanova;

                    //zapisem prerozdelenie blokov do suboru. 
                    Subor.WriteBlok(adresaNovehoBloku, novyBlock);
                    Subor.WriteBlok(adresaBloku, block);
                }
                else
                {
                    //vloz zaznam
                    block.PridajRecord(data);
                    Subor.WriteBlok(adresaBloku, block);
                    vlozene = true;
                }
            }
            return vlozene;
        }

        private void PrerozdelenieBlokov(Block block, int hlbkanova, Block novyBlock)
        {
            for (int i = 0; i < block.PoleRecordov.Count; i++)
            {
                if (GetBitArrayFromHash(block.PoleRecordov[i].GetHash())[hlbkanova])
                {
                    novyBlock.PridajRecord(block.PoleRecordov[i]);
                    block.VymazRecord(block.PoleRecordov[i]);
                }
            }
        }

        private void ZaktualizujAdresyAdresara(Record data, Block block, int hlbkanova, int adresaNovehoBloku)
        {
            int maxAdresa = MaxIndexPrerozdelenia(block.Hlbka, hlbkanova, data);
            int minAdresa = MinIndexPrerozdelenia(block.Hlbka, hlbkanova, data);

            //prerozdelenie adries v adresari, aby ukazovali na nove bloky spravne. 
            int rozdielBlokov = maxAdresa - minAdresa;
            double vysledok = (double) rozdielBlokov/2;
            int vysled = (int) Math.Ceiling(vysledok);
            for (int i = vysled + minAdresa; i <= maxAdresa; i++)
            {
                Adresar[i] = adresaNovehoBloku;
            }
        }

        /// <summary>
        /// Zdvojnasobenie adresaru (D = D + 1) 
        //   => alokujes nove pole vacsie o 100%
        //   => kazda honota z povodneho pola sa nakopiruje dvakrat
        /// </summary>
        private void ZdvojnasobAdresar()
        {
            List<int> ZdvojnasobAdresar = new List<int>(Adresar.Capacity*2);
            for (int i = 0; i < Adresar.Count; i++)
            {
                ZdvojnasobAdresar.Add(Adresar[i]);
                ZdvojnasobAdresar.Add(Adresar[i]);
            }
            Adresar = ZdvojnasobAdresar;
        }

        //vrati mi minimalny index prerozdelenia
        private int MinIndexPrerozdelenia(int aktualnaHlbkaSubor, int novaHlbka, Record data)
        {
            int min = 0;

            BitArray hassBitArray = new BitArray(BitConverter.GetBytes(data.GetHash()));
            //potrebujem urobit dekadicky tvar cisla 
           
            int exponent = novaHlbka ;
            for (int i = 0; i< novaHlbka; i++)
            {
                if (i < aktualnaHlbkaSubor)
                {
                    min += hassBitArray[i] ? 2 ^ (exponent) *1  : 0;
                }
              exponent--;
            }
            
            return min;
        }
        private int MaxIndexPrerozdelenia(int aktualnaHlbkaSubor, int novaHlbka, Record data)
        {
            int max = 0;

            BitArray hassBitArray = new BitArray(BitConverter.GetBytes(data.GetHash()));
            //potrebujem urobit dekadicky tvar cisla 
            int exponent = novaHlbka+1;
            for (int i = 0; i < novaHlbka; i++)
            {
                if (i < aktualnaHlbkaSubor)
                {
                    max += hassBitArray[i] ? 2 ^ (exponent) * 1 : 0;
                }
                else
                {
                    max += 2 ^ (exponent) * 1;
                }

                exponent--;
            }

            return max;
        }

        private int IndexSubAdresara(int hash, int hlbka)
        {
            //32 bitove cislo
            //napr. 1010 0101 0101 0010 10....
            BitArray hassBitArray = new BitArray(BitConverter.GetBytes(hash));
           // Console.WriteLine();
            //Console.Write("Hlbka: " + hlbka + ", prevod hash do binarneho:\t");
            //potrebujem previest prvych par bitov na dekadicke cislo
            //napr. ak je hlbka - 4
            //1010 => 0 + 2 + 0 + 8 => 10 
            int cislo2 = 0;
            int ind = hlbka-1;
            for (int i = 0; i < hlbka ; i++)
            {
                if (hassBitArray[i])
                {
                    cislo2 += Convert.ToInt32(Math.Pow(2, (ind-i))); //http://stackoverflow.com/questions/5283180/how-can-i-convert-bitarray-to-single-int
                }
           //     Console.Write(((hassBitArray[i]) ? 1 : 0) + "");
            }
          
          //  Console.Write(", bolo prevedene na cislo : \t" + cislo2);
           // Console.WriteLine();
            return cislo2;
        }
        private BitArray GetBitArrayFromHash(int hash)
        {
            return new BitArray(BitConverter.GetBytes(hash));
        }
        public override string ToString()
        {
            string s = $"{nameof(HlbkaSuboru)}: {HlbkaSuboru}, {nameof(PocetBlokov)}: {PocetBlokov}, {nameof(VelkostZaznamu)}: {VelkostZaznamu}, {nameof(MaxPocetZaznamovVBloku)}: {MaxPocetZaznamovVBloku}";
            StringBuilder sb = new StringBuilder();
            //vypisem jednotlive bloky
            //prechadzam vsetky bloky a postupne ich citam zo suboru. 
            s += "\nAdresar: ";
            //for (int i = 0; i < PocetBlokov; i++)
            //{
            //   //Block _prvyBlock = Subor.ReadBlok(i);
            //   // sb.AppendLine("Blok " + i + "\t" + _prvyBlock.ToString());
            //    s += Adresar[i] + ", ";
            //}
            foreach (var a in Adresar)
            {
                s += a + ", ";
            }
            int i = 1;
            foreach (var adresa in Adresar)
            {
                if (adresa != -1)
                {
                    Block b = Subor.ReadBlok(adresa);
                    sb.AppendLine("Block c: " + (i++));
                    sb.AppendLine(b.ToString());

                }

            }

            return s + sb.ToString();

        }

        /// <summary>
        /// Operacia Vymaz
        /// Efektivnost: 1-3 prenosy. 
        /// // vacsinou 2 prenosy, pretoze sa skuma i sused. (v oboch pripadoch pripadny 
        ///     zapis buffra pred prenosom naviac) 
        /// //
        /// Zakladna idea:
        /// //
        /// 1. Ak po zruseni zostane v susednych blokov iba tolko zaznamov
        ///    ze sa zmestia do jedneho bloku
        ///     => presunu sa tam
        ///     => volny blok sa dealokuje
        ///     => bloku, ktory tam zostal, 
        ///         => sa znizi hlbka
        ///         => ak to bol posledny blok s touto hlbkou 
        ///             => znizi sa hlbka celeho suboru
        ///             => adresar sa zmensi na polovicu 
        /// //
        /// 2. Operacia prebieha cyklicky podobne ako vkladanie. 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Delete(Record data)
        {
            return false;
        }

        /// <summary>
        /// Operácia nájdi záznam s kľúčom K. 
        /// Efektivnost: 1 prenos. 
        /// //
        /// Adresar je kratky => 
        ///     pocas spracovania v operacnej pamati
        ///     => vyhladanie zaznmau vyzaduje standartne jeden blokovy prenos
        ///         (plus pripadny vyvovalny prenos z buffera do suboru). 
        /// Adresar sa nachadza v operacnej pamati. 
        ///     => je to jednorozmenre pole adries (celociselnych honot). 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Record Search(Record hladanyZaznam)
        {
            Record findRecord = null;
            //vypocitaj prvych D bitov hodnoty hesovacej funkcie 
            //D bit -> Hlbka adresara 
            //vypocitaj I=hD(K)
            int hash = ((Record)hladanyZaznam).GetHash();
            int indexvAdresari = IndexSubAdresara(hash, HlbkaSuboru);
            //V bloku P[i] najdi zaznam s klucom K. 
            int adresaBlokuVSubore = Adresar[indexvAdresari];
            Block block = Subor.ReadBlok(adresaBlokuVSubore);

            //pomocou adresara spristupni blok P[i] 
            // spristupni prvych D bitov 
            //I preved na cele cislo
            //toot je index v adresari na tkorom sa nachadza adresa 
            //miesta v subore, kde sa nachadza blok,ktory by mal 
            //obsahovat hladany zaznam

            foreach (var x in block.PoleRecordov)
            {
                if (x.Equals(hladanyZaznam))
                {
                    return x;
                }
            }

            return findRecord;
        }
        #endregion

        public Block VyhladajBlock()
        {
            return default(Block);
        }
    }
}
