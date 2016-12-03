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
        public int _pocet_bitov_isvalid = 1;
        public string Key { get; set; }
        
        /// <summary>
        /// Adresa bloku s datami tohto recordu
        /// </summary>
        public int Address { get; set; }

        public int _pocet_bitov_address = 4; 
        public int Size { get; set; }

        public abstract int GetHash();
        //Bitove pole
        public abstract byte[] GetBitSet();

        public abstract int GetSize();
        public abstract int GetAddressSize();

        public abstract override bool Equals(object obj);

        public abstract byte[] ToByteArray(bool allData = true);

        public abstract Record FromByteArray(byte[] bitArray);

        public abstract override string ToString();

    }
}
