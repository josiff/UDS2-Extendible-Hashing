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

      

        public override string ToString()
        {
            return "" + Number;
        }

        public override byte[] ToByteArray()
        {
            throw new NotImplementedException();
        }

        public override void FromByteArray(byte[] byteArray)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }

      

        public override int GetSize()
        {
            throw new NotImplementedException();
        }

     
      }

}
