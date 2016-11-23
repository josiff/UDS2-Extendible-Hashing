using System;
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

        public int[] Adresar { get; set; }
        public int HlbkaAdresara { get; set; }
        
        public FileStream Seek { get; set; }

        #endregion
        
        #region Methods

        public bool Insert(Record data)
        {

            return false;
        }

        public bool Delete(Record data)
        {

            return false;
        }


        public Record Search(Record data)
        {


            return default(Record);
        }

        
        #endregion
    }
}
