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

        public Block NacitajBlok(int adresa)
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

        

    }
}
