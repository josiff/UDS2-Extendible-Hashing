using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresLibrary.Extendible_Hashing
{
   public class ExFile
    {
        private string _filename;
        private FileStream _fileStream;
        public  int PocetBlockov { get; set; }
        private Record _record;
        private int _maxCount; //maximalny pocet recordov v bloku
        public ExFile(string filename, int maxCount,  Record record, bool createNew = false)
        {
            _filename = filename;
            _maxCount = maxCount;
            _record = record;
            _fileStream = new FileStream(filename, (createNew ? FileMode.OpenOrCreate : FileMode.Open) , FileAccess.ReadWrite, FileShare.ReadWrite);
        }

        public Block ReadBlok(int adresa)
        {
            //copy konstruktorom vytvorim novy block
            Block _tempBlock = new Block(_maxCount, _record);
            //vytvorim pole bytov, ktore idem citat
            byte[] poleBytov = new byte[_tempBlock.GetSize()];
            //vycistim blok - nastavim dane recordy na nevalidne
            //nastavim pomocnu premenu na velkost poctu bytov v bloku
            //je to index od ktoreho budem seekovat.
            int temp = _tempBlock.GetSize();
            //seeknem na dany index
            int offset = (adresa == 0 || adresa == -1)?0: temp*adresa-temp;
           _fileStream.Seek(offset, SeekOrigin.Begin);
            //precitam dane byty zo suboru
            _fileStream.Read(poleBytov, offset, temp);
            //nastavim blok z danych dat. 
            _tempBlock.FromByteArray(poleBytov);
            //vratim blok ktory som precitala.
            return _tempBlock;
        }

        /// <summary>
        /// Metoda zapise blok do suboru. 
        /// </summary>
        /// <param name="adresaBloku"></param>
        /// <param name="poleBytov"></param>
        public void WriteBlok(int adresaBloku, Block block)
        {
            int odKial = adresaBloku*block.GetSize();
            //zapisem dane byty na dany index
            byte[] poleBytov = new byte[block.GetSize()];
            poleBytov = block.ToByteArray();
         ///   _fileStream.Seek(odKial, SeekOrigin.Begin);
            _fileStream.Write(poleBytov, odKial, block.GetSize());
        }

        public int AlokujNovyBlock()
        {
            return PocetBlockov++;
        }
        /// <summary>
        /// Metoda zobrazi informacie neutriedeneho suboru. 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < PocetBlockov; i++)
            {
                Block b = ReadBlok(i);
                sb.AppendLine("Block c: " + (i + 1));
                sb.AppendLine(b.ToString());
            }
            return sb.ToString();
        }
    }
}
