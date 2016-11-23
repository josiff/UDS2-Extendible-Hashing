using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresLibrary.Extendible_Hashing
{
    class Block
    {
        public Record[] PoleRecordov { get; set; }

        public int PocetZaznamov { get; set; }

        public int PocetPlatnych { get; set; }

        public Block DalsiVolny { get; set; }

        public BitArray ToByteArray(object data)
        {
            // return BitConverter.GetBytes(data);          
            throw new NotImplementedException();

        }

        public object FromByteArray(BitArray bitArray)
        {
            throw new NotImplementedException();
        }
    }
}
