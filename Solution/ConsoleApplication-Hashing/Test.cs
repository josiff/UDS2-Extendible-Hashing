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
            return BitConverter.GetBytes(Number);
        }

        public override void FromByteArray(byte[] byteArray)
        {
            Number = BitConverter.ToInt32(byteArray, 0);
        }

        public override bool Equals(object obj)
        {
            var test = (Test) obj;
            return test != null && Number == test.Number;
        }
        
        public override int GetSize()
        {
            return 4;
        }
      }

}
