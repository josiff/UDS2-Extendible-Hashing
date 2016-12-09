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
                //case "Žilina": return 0;
                //case "Košice": return 51200 ;
                //case "Martin": return 38144 ;
                //case "Levice": return 47872;
                //case "Trnava": return 42240;
                //case "Snina": return 46592;
                //case "Senica": return 40960;
                //case "Nitra": return 27648;
                //case "Poprad": return 0;

                //case "Lučenec": return 25600;
                //case "Zvolen": return 59648;
                //case "Prešov": return 61440;
                //case "Púchov": return 46848;
                //case "Ilava": return 3840;
                //case "Brezno": return 15360;
                case "Žilina": return 0;
                case "Košice": return 38;
                case "Martin": return 169;
                case "Levice": return 221;
                case "Trnava": return 165;
                case "Snina": return 109;
                case "Senica": return 5;
                case "Nitra": return 54;
                case "Poprad": return 0;
                case "Lučenec": return 38;
                case "Zvolen": return 151;
                case "Prešov": return 15;
                case "Púchov": return 237;
                case "Ilava": return 31;
                case "Brezno": return 60;


            }
            return Math.Abs(Number.GetHashCode());
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
