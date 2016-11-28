using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresLibrary.Extendible_Hashing
{
    public class ExtendibleHashing
    {
        #region Property
        /// <summary>
        /// Adresar - dynamicke pole celych cisel
        /// </summary>
        public int[] Adresar { get; set; }
        /// <summary>
        /// Hlbka Hesovacieho suboru  - D
        /// </summary>
        public int HlbkaAdresara { get; set; }
        
        public FileStream Seek { get; set; }

        #endregion
        
        #region Methods
        /// <summary>
        /// Operacia Vloz.  
        /// Efektivnost: 1 prenos, v pripade preplenia 2 prenosy. 
        /// //
        /// Zakladna idea: 
        /// 1. Ak sa blok preplni
        ///     => nebuduj skupinu preplnujucich blokov, namiesto toho 
        ///     => zvacsi adresovy priestor tak, aby sa adresovali bloky
        ///         a nie skupiny blokov. 
        /// //
        /// 2. Pri preplneni alokuj novy blok 
        ///     => prirad mu adresu v adresari. 
        /// // 
        /// 3. Adresovy priestor zvacsi, tak ze ho
        ///     =>  zdvojnasobis (D = D + 1) 
        ///             => alokujes nove pole vacsie o 100%
        ///             => kazda honota z povodneho pola sa nakopiruje dvakrat
        ///     => zaktualizujes adresy v adresari.  
        /// //
        /// 4. V pripade, ze dojde k 
        ///     => vyuzitiu vsetkych bitov z vysledku hashovacej fukcie a
        ///     => zaznam nebolo mozne vlozit
        ///         => dojde ku kolizii
        ///    Riesenie kolizii
        ///     => pouzi oblast preplenia blokov alebo
        ///     => oblast preplnenia suboru. 
        /// // 
        /// 5. Volne bloky uprostred suboru je nutne vyuzivat prednostne
        ///     => rovnaky postup ako prazdne bloky v prepnujucom subore (staticke heshovanie).
        ///         => adresy volnych blokov sa udrziavaju v operacnej pamati
        ///              - pri ukonceni programu sa musia tieto indexy ulozit 
        ///         => adresy volnych blokov sa zretazuju 
        ///             => kazdy blok ma svoju adresu na nasledujuci blok 
        ///             => novy uvolneny blok je pridany na zaciatok zretazenia
        ///             => pri poziadavke na novy blok je prideleny prvy blok so zretaxenia
        ///                 max jeden blokovy prenos
        ///             => v operacnej pamati je ptorebne uchovavat adresu prveho volneho bloku 
        ///                so zretazenia, ktora sa pri ukonceni programu musi niekde ulozit . 
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Insert(Record data)
        {
            //Vkladanie do bloku b - Princip
            //Nedoslo k preplneniu => vlozi sa priamo
            //Doslo k preplneniu, ale d<D 
            //      => rozdel blok
            //      => vloz zaznam
            //      => uprav adresy (lokalne) bez zvacsenia adresara
            //Doslo k prepleniu a d = D
            //      => zdvojnasob adresar
            //      => rozdel blok
            //      => vloz zaznam 
            //      => reorganizuj cely adresar

           



            //PSEOUDO
            //
            //Algoritmus vkladania
            // while niejevlozene do
            // begin
                //vypocitajHash               - ziskame adresu bloku
                //if blokJePlny then 
                    //begin 
                        //if hlbkaBloku = HlbkaSuboru then 
                            //begin 
                                //Zdvojnasob Adresar
                            //end
                        //rozdelenieBloku    -split - vytvorenie noveho bloku
                    //end
               //else
                    //begin 
                        //VlozZaznam         - ukoncenie cyklu uwhile
                    //end
            //end
            return false;
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
        public Record Search(int kluc)
        {
            Record findRecord = null;
            //vypocitaj prvych D bitov hodnoty hesovacej funkcie 
            //D bit -> Hlbka adresara 
            //vypocitaj I=hD(K)
            byte[] I = BitConverter.GetBytes(kluc); //todo

            //pomocou adresara spristupni blok P[i] 
            // spristupni prvych D bitov 
            //I preved na cele cislo
            //toot je index v adresari na tkorom sa nachadza adresa 
            //miesta v subore, kde sa nachadza blok,ktory by mal 
            //obsahovat hladany zaznam

            int index = BitConverter.ToInt32(I, HlbkaAdresara);

            Block Pi = null;
            
            //V bloku P[i] najdi zaznam s klucom K. 
            bool found =false;
            foreach (var x in Adresar)
            {
                if (x == index)
                {
                    found = true;
                    break;
                }
            }

            if (!found) return null;

            foreach (var x in Pi.PoleRecordov)
            {
                if (x.Key.Equals(index))
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
