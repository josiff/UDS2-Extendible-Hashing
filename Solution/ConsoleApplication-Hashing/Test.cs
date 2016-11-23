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

        public override int GetHash()
        {
            throw new NotImplementedException();
        }

        public override BitArray GetBitSet()
        {
            throw new NotImplementedException();
        }

        public override int GetSize()
        {
            throw new NotImplementedException();
        }

        public override bool Equals()
        {
            throw new NotImplementedException();
        }

        public override BitArray ToByteArray(Record data)
        {
            throw new NotImplementedException();
        }

        public override Record FromByteArray(BitArray bitArray)
        {
            throw new NotImplementedException();
        }
    }

}
