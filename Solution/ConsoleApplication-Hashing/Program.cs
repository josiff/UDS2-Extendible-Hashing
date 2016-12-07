using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using DataStructureLogic;
using DataStructuresLibrary.Extendible_Hashing;

namespace ConsoleApplication_Hashing
{
    class Program
    {
        static void Main(string[] args)
        {
            Auto auto = new Auto("1234567", "1234567", 4, 544, false, DateTime.Today.AddDays(433),
               DateTime.Today.AddDays(99));
            ExtendibleHashing<Auto> ex = new ExtendibleHashing<Auto>("auto.txt", 444, auto);
            Console.WriteLine(ex.ToString());
            ex.Adresar[0] = 34455435;
            ex.Adresar[1] = 8888435;

            Console.WriteLine(ex.ToString());
            
           
            Auto auto2 = new Auto("23", "32", 4, 544, false, DateTime.Today.AddDays(433),
                DateTime.Today.AddDays(99));
            Console.WriteLine(auto.ToString());
            Block blok = new Block(2, 4, auto);
            Console.WriteLine(blok.ToString());

           blok.PridajRecord(auto);
          //  blok.PridajRecord(auto2);
            Console.WriteLine(blok.ToString());
         
            
            Block bloktemp = new Block(60, 2,auto);
            bloktemp.PridajRecord((Record) auto2);
            bloktemp.PridajRecord((Record) auto);
        
            Record r = blok.NajdiRecord((Record)auto);
         
            Console.ReadLine();
            Console.ReadKey();
        }
    }
}
