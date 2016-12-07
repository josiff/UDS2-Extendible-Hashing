using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresLibrary.Extendible_Hashing
{
    class ExFile
    {
        private string _filename;
        private FileStream _fileStream;
        private int _pocetBlockov;
        private Record _record;
        private int _maxCount; //maximalny pocet recordov v bloku

        public ExFile(string filename, int maxCount,  Record record)
        {
            _filename = filename;
            _maxCount = maxCount;
            _record = record;
            _fileStream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
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
            _fileStream.Seek(adresa * temp, SeekOrigin.Begin);
            //precitam dane byty zo suboru
            _fileStream.Read(poleBytov, 0, temp);
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
            _fileStream.WriteAsync(poleBytov, odKial, block.GetSize());
        }

        public int AlokujNovyBlock()
        {
            return _pocetBlockov++;
        }
        /// <summary>
        /// Metoda zobrazi informacie neutriedeneho suboru. 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < _pocetBlockov; i++)
            {
                Block b = ReadBlok(i);
                sb.AppendLine(b.ToString());
            }
            return sb.ToString();
        }
    }
}
