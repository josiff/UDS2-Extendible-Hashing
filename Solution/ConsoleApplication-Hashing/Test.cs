using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructuresLibrary.Extendible_Hashing;

namespace ConsoleApplication_Hashing
{
    public class Test : Record
    {
        public int Number { get; set; }
       
        public Test(int number)
        {
            Number = number;
        }

     public override void FromByteArray(byte[] byteArray, bool hasAdress = true)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "" + Number;
        }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }

        public override byte[] ToByteArray(bool allData = true)
        {
            throw new NotImplementedException();
        }

        public override int GetHash()
        {
            throw new NotImplementedException();
        }

        public override byte[] GetBitSet()
        {
            throw new NotImplementedException();
        }


        public override int GetSize()
        {
            throw new NotImplementedException();
        }

        public override int GetAddressSize()
        {
            throw new NotImplementedException();
        }

      }

}
