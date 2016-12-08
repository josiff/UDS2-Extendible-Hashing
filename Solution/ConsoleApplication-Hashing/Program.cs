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
            //  Auto auto = new Auto("1234567", "1234567", 4, 544, false, DateTime.Today.AddDays(433),
            //     DateTime.Today.AddDays(99));
            //  ExtendibleHashing<Auto> ex = new ExtendibleHashing<Auto>("auto.txt", 444, auto);
            //  Console.WriteLine(ex.ToString());
            //  ex.Adresar[0] = 34455435;
            //  ex.Adresar[1] = 8888435;

            //  Console.WriteLine(ex.ToString());


            //  Auto auto2 = new Auto("23", "32", 4, 544, false, DateTime.Today.AddDays(433),
            //      DateTime.Today.AddDays(99));
            //  Console.WriteLine(auto.ToString());
            //  Block blok = new Block(2, 4, auto);
            //  Console.WriteLine(blok.ToString());

            // blok.PridajRecord(auto);
            ////  blok.PridajRecord(auto2);
            //  Console.WriteLine(blok.ToString());


            //  Block bloktemp = new Block(60, 2,auto);
            //  bloktemp.PridajRecord((Record) auto2);
            //  bloktemp.PridajRecord((Record) auto);

            // Record r = blok.NajdiRecord((Record)auto);
            Test auto = new Test("test ");
            Random rand = new Random();

            ExtendibleHashing<Test> hashing2 = new ExtendibleHashing<Test>("test"+rand.Next(999)+"_file.txt",10, auto, true);
           
            Block block = new Block(40, 0, auto);
            for (int i = 0; i < 20; i++)
            {
                auto = new Test("test " + i);
                block.PridajRecord(auto);
                hashing2.Insert(auto);
            }

            Console.WriteLine(block.ToString());
            byte[] array = block.ToByteArray();
            Block b = new Block(40, 0, auto);
            Block fromArray = b.FromByteArray(array);
            Console.WriteLine(hashing2.ToString());

            Console.WriteLine("Najdi recordy: ");
            Test kluc;
            
            for (int i = 0; i < 20; i++)
            {
                kluc = new Test("test " + i);
                Console.WriteLine(hashing2.Search(kluc)?.ToString());
            }




            //Record r = new Test("prvy rec");
            //Record r1 = new Test("druhy1322");
            //Record r2 = new Test("treti2142");
            //Record r3 = new Test("str24322");
            //Record r4 = new Test("piaty32e");

            //string filename = "test_" + rand.Next(3, 5454545)*rand.Next(7)+".txt";
            //ExtendibleHashing<Test> hashing = new ExtendibleHashing<Test>(filename, 1, r, true);
            //Console.WriteLine("Pred Vkladanim");
            //Console.WriteLine();
            //Console.WriteLine(hashing.ToString());

            
            //Console.WriteLine(hashing.ToString());

            //hashing.Insert(r);
            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine("Vlozenie jedneho zaznamu. ");
            //Console.WriteLine(hashing.ToString());
            
          
            //hashing.Insert(r1);
            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine("Vlozenie dvoch zaznamov. ");
            //Console.WriteLine(hashing.ToString());

            //hashing.Insert(r2);
            //Console.WriteLine();
            //Console.WriteLine("Vlozenie troch zaznamov. ");

            //Console.WriteLine(hashing.ToString());
           
            //hashing.Insert(r3);
            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine("Vlozenie styroch zaznamov. ");
            //Console.WriteLine(hashing.ToString());
            //hashing.Insert(r4);
            //Console.WriteLine();
            //Console.WriteLine("Po vlozeni piatych: ");
            //Console.WriteLine(hashing.ToString());
            
            Console.ReadLine();
            Console.ReadKey();
        }
    }
}
