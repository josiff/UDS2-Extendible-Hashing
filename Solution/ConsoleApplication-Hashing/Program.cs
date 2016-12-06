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
            Block blok = new Block(40, 1);
            Console.WriteLine(blok.ToString());
            Auto auto = new Auto("dkljkdsjl", "dkjsalkfjsdlkj", 4, 544, false, DateTime.Today.AddDays(433),
                DateTime.Today.AddDays(99));
            blok.PridajRecord(auto);
            Console.WriteLine(blok.ToString());
            NeusporiadanySubor ns = new NeusporiadanySubor(blok, "auta.txt");
            ns.ZapisBlok(blok.AdresaPrvehoRecordu, blok.ToByteArray());
            Console.WriteLine(ns.ToString());
            ns.ZapisZaznam((Record)auto);
            Console.WriteLine(ns.ToString());
            Record r = blok.NajdiRecord((Record) auto);
            Console.WriteLine(r.ToString());

            Block bl = ns.PrecitajBlok(blok.AdresaPrvehoRecordu);
            Console.WriteLine(bl.ToString());
            
            Console.ReadLine();
            Console.ReadKey();
        }
    }
}
