using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructuresLibrary.Extendible_Hashing;
using DateStructureGeneral;

namespace ConsoleApplication_Hashing
{
    public class Test : Record
    {
        public string Number { get; set; }
       
        public Test(string number)
        {
            Number = number;
        }

        public override string ToString()
        {
            return "" + Number;
        }

        public override int GetHash()
        {
            switch (Number)
            {
                case "Žilina": return 0;
                case "Košice": return 100;
                case "Martin": return 149;
                case "Levice": return 187;
                case "Trnava": return 165;
                case "Snina": return 182;
                case "Senica": return 160;
                case "Nitra": return 108;
                case "Poprad": return 0;
                case "Lučenec": return 100;
                case "Zvolen": return 233;
                case "Prešov": return 240;
                case "Púchov": return 183;
                case "Ilava": return 15;
                case "Brezno": return 60;
            }

            return (Number.GetHashCode());
        }

        public override byte[] ToByteArray()
        {
            return Helper_Bytes._get_pom_pole(10, Encoding.UTF8.GetBytes(Number));
            ;
        }

        public override Record FromByteArray(byte[] byteArray)
        {
             Number = Encoding.UTF8.GetString(byteArray, 0, 10).Trim('\0');
       return new Test(Number);
        }

        public override bool Equals(object obj)
        {
            var test = (Test) obj;
            return test != null && Number == test.Number;
        }
        
        public override int GetSize()
        {
            return 10;
        }
      }

}
