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
            ExtendibleHashing ex = new ExtendibleHashing();
          
            
            Auto auto = new Auto("1234567", "1234567", 4, 544, false, DateTime.Today.AddDays(433),
                DateTime.Today.AddDays(99));
            Auto auto2 = new Auto("1234567", "1234567", 4, 544, false, DateTime.Today.AddDays(433),
                DateTime.Today.AddDays(99));
            Console.WriteLine(auto.ToString());
            Block blok = new Block(40, 1 );
            Console.WriteLine(blok.ToString());

            blok.PridajRecord(auto);
            blok.PridajRecord(auto2);
            Console.WriteLine(blok.ToString());
         
            
            Block bloktemp = new Block(60, 2);
            NeusporiadanySubor ns = new NeusporiadanySubor(bloktemp, "auta.txt");
            ns.ZapisBlok(1, blok.ToByteArray());
            Console.WriteLine(ns.ToString());
            ns.ZapisZaznam((Record)auto);
            Console.WriteLine(ns.ToString());
            ns.ZapisZaznam((Record)auto2);
            Console.WriteLine("subor");
            Console.WriteLine(ns.ToString());
            Record r = blok.NajdiRecord((Record)auto);
          r =   ns.PrecitajZaznam(1, auto2, false);

            Console.WriteLine(r?.ToString());

            Block bl = ns.PrecitajBlok(blok.AdresaPrvehoRecordu);
            Console.WriteLine(bl.ToString());
            
            Console.ReadLine();
            Console.ReadKey();
        }
    }
}
