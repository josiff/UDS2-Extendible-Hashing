using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresLibrary.Extendible_Hashing
{
    public abstract class Record 
    {
        public bool IsValid { get; set; }


        public abstract int GetHash();
        //Bitove pole
        public abstract BitArray GetBitSet();

        public abstract int GetSize();

        public abstract bool Equals();


        public abstract BitArray ToByteArray(Record data);
        public abstract Record FromByteArray(BitArray bitArray);
    }
}
