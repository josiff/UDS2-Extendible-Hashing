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
            Random rand = new Random();

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


           // Console.WriteLine("Priklad z prednasky:");
           // string[] mesta = new[]
           // {
           //     "Žilina", "Košice", "Martin", "Levice", "Trnava", "Snina", "Senica", "Nitra", "Poprad", "Lučenec",
           //     "Zvolen", "Prešov", "Púchov", "Ilava", "Brezno","Namestovo","Klin","Luka","Teplicka","Bratislava","Pole","Za Kopcom"
           // };
           // Test mesto = new Test("");
           // ExtendibleHashing<Test> hashovanie = new ExtendibleHashing<Test>("mesta_test" + rand.Next(999) + "_file.txt", 5, mesto, true);

           // for (int i = 0; i < 10; i++)
           // {
           //     mesto = new Test(mesta[i]);
           //     hashovanie.Insert(mesto);
           // }
           // Console.WriteLine();
           // Console.WriteLine("Stav suboru po vlozeni 10 zaznamov.");
           // Console.WriteLine(hashovanie.ToString());

           // mesto = new Test(mesta[10]);
           // hashovanie.Insert(mesto);

           // mesto = new Test(mesta[11]);
           // hashovanie.Insert(mesto);
           // Console.WriteLine();
           // Console.WriteLine("Stav suboru po vlozeni 2 zaznamov. Zvolen a Presov");
           // Console.WriteLine(hashovanie.ToString());

           // mesto = new Test(mesta[12]);
           // hashovanie.Insert(mesto);
           // Console.WriteLine();
           // Console.WriteLine("Stav suboru po vlozeni Puchov. ");
           // Console.WriteLine(hashovanie.ToString());
           // mesto = new Test(mesta[13]);
           // hashovanie.Insert(mesto);
           // mesto = new Test(mesta[14]);
           // hashovanie.Insert(mesto);
           // Console.WriteLine();
           // Console.WriteLine("Stav suboru po vlozeni Ilava a Brezno. ");
           // Console.WriteLine(hashovanie.ToString());
           // for (int i = 15; i < mesta.Length; i++)
           // {
           //     mesto = new Test(mesta[i]);
           //     hashovanie.Insert(mesto);
           // }
            
           //Console.WriteLine("Stav suboru po vlozeni Dalsich x zaznamov. ");
           // Console.WriteLine(hashovanie.ToString());

           // int pocetNenajdenych = 0;

           // for (int i = 0; i < mesta.Length; i++)
           // {
           //     mesto = new Test(mesta[i]);
           //     Record h = hashovanie.Search(mesto);
           //     if (h != null)
           //     {
           //         Console.WriteLine(h?.ToString());
           //     }
           //     else
           //     {
           //         pocetNenajdenych++;
           //     }
           // }
           // Console.WriteLine("Nenaslo sa : " + pocetNenajdenych);

            Test auto = new Test("test ");

            ExtendibleHashing<Test> hashing2 = new ExtendibleHashing<Test>("test" + rand.Next(999) + "_file.txt", 1000, auto, true);
            List<Test> aa =new List<Test>();
            Block block = new Block(40, 0, auto);
            for (int i = 0; i < 10000; i++)
            {
                auto = new Test(""+rand.Next(0,33333));
                block.PridajRecord(auto);
                hashing2.Insert(auto);
                aa.Add(auto);
            }

            //Console.WriteLine(block.ToString());
            //byte[] array = block.ToByteArray();
            //Block b = new Block(40, 0, auto);
            //Block fromArray = b.FromByteArray(array);
            //Console.WriteLine(hashing2.ToString());

            //Console.WriteLine("Najdi recordy: ");
            //Test kluc;

            for (int i = rand.Next(40, 200); i < rand.Next(200, 300); i++)
            {
                Record h = hashing2.Search(aa[i]);
                if (h != null)
                {
                    Console.WriteLine(h?.ToString());
                }
                else
                {
                    Console.WriteLine("ZAZNAM SA NENASIEL!!!");
                }
            }
            //}


                //Record r = new Test("prvy rec");
                //Record r1 = new Test("druhy1322");
                //Record r2 = new Test("treti2142");
                //Record r3 = new Test("str24322");
                //Record r4 = new Test("piaty32e");

                //string filename = "test_" + rand.Next(3, 5454545) * rand.Next(7) + ".txt";
                //ExtendibleHashing<Test> hashing = new ExtendibleHashing<Test>(filename, 1, r, true);
                //Console.WriteLine("Pred Vkladanim");
                //Console.WriteLine(hashing.ToString());

                //hashing.Insert(r);
                //Console.WriteLine("Vlozenie jedneho zaznamu. ");
                //Console.WriteLine(hashing.ToString());


                //hashing.Insert(r1);
                //Console.WriteLine("Vlozenie dvoch zaznamov. ");
                //Console.WriteLine(hashing.ToString());

                //hashing.Insert(r2);
                //Console.WriteLine("Vlozenie troch zaznamov. ");
                //Console.WriteLine(hashing.ToString());

                //hashing.Insert(r3);
                //Console.WriteLine("Vlozenie styroch zaznamov. ");
                //Console.WriteLine(hashing.ToString());

                //hashing.Insert(r4);
                //Console.WriteLine("Po vlozeni piatych: ");
                //Console.WriteLine(hashing.ToString());

                Console.ReadLine();
            Console.ReadKey();
        }
    }
}
