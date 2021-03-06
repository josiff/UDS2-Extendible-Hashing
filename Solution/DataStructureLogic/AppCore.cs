﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureLogic
{
 public   class AppCore
    {
        private Zariadenie zariadenie;
        private int maxEvidenceCislo = 7;
        private int maxVinCislo = 17;
        private int maxMeno = 35;
        private int maxPriezvisko = 35;

        public AppCore()
        {
            zariadenie = new Zariadenie();
        }
        #region Auto
        ///Pre záznamy o aute: 
        /// <summary>
        ///Vyhľadanie 
        /// – podľa  evidenčného čísla vozidla nájde príslušný záznam 
        /// a zobrazí všetky údaje
        /// </summary>
        /// <param name="evidencneCislo"></param>
        /// <returns></returns>
        public string VyhladajVypisAutoEvidencneCislo(string evidencneCislo)
        {
            if (evidencneCislo.Length > maxEvidenceCislo)
            {
                return "Evidencne cislo musi obsahovat menej ako"+maxEvidenceCislo+" znakov.";
            }
            else if (evidencneCislo == "")
            {
                return "Neplatne evidencne cislo";
            }
            else
            {
                return zariadenie.VyhladajVypisAutoEvidencneCislo(evidencneCislo);
            }
          }
        /// <summary>
        ///  Vyhľadanie
        /// – podľa  VIN čísla vozidla nájde príslušný záznam 
        /// a zobrazí všetky údaje
        /// </summary>
        /// <param name="vinCislo"></param>
        /// <returns></returns>
        public string VyhladajVypisAutoVin(string vinCislo)
        {
            if (vinCislo.Length > maxVinCislo)
            {
                return "VIN musi obsahovat menej ako"+maxVinCislo+" znakov.";
            }
            else if (vinCislo == "")
            {
                return "Neplatne VIN";
            }
            else
            {
                return zariadenie.VyhladajVypisAutoVin(vinCislo);
            }

        }
        /// <summary>
        ///  Pridanie 
        /// – na základe vstupných údajov pridá automobil do evidencie  
        /// </summary>
        /// <param name="evidencneCisloVozidla"></param>
        /// <param name="vinCislo"></param>
        /// <param name="pocetNaprav"></param>
        /// <param name="prevadzkovaHmotnost"></param>
        /// <param name="vPatrani"></param>
        /// <param name="koniecPlatnostiStk"></param>
        /// <param name="koniecPlatnostiEk"></param>
        /// <returns></returns>
        public string PridanieAuto(string evidencneCisloVozidla, string vinCislo, string pocetNapravs, string prevadzkovaHmotnosts, bool vPatrani, DateTime koniecPlatnostiStk, DateTime koniecPlatnostiEk)
        {
            string errors = "";
            int pocetNaprav;
            int prevadzkovaHmotnost;
            errors += (evidencneCisloVozidla == "") ? "Nespravne evidencne cislo..\n" : "";
            errors += (!Int32.TryParse(pocetNapravs, out pocetNaprav)) ?"Nebolo zadany spravny vstup pre: " + "Pocet naprav.\n" : "";
            errors += (!Int32.TryParse(prevadzkovaHmotnosts, out prevadzkovaHmotnost)) ? "Nebolo zadany spravny vstup pre: " + "Prevadzkova hmotnost.\n" : "";
            errors += (evidencneCisloVozidla.Length > maxEvidenceCislo)
                ? "Evidencne cislo musi obsahovat najviac "+ maxEvidenceCislo+ " znakov.\n" : "";
            errors += (vinCislo.Length > maxVinCislo)
                ? "VIN cislo musi obsahovat najviac "+ maxVinCislo+ " znakov.\n" : "";

            if (errors.Equals(""))
            {
                bool pridaj = zariadenie.PridanieAuto(evidencneCisloVozidla, vinCislo, pocetNaprav, prevadzkovaHmotnost,
                    vPatrani,
                    koniecPlatnostiStk, koniecPlatnostiEk);
                Auto auto = new Auto(evidencneCisloVozidla, vinCislo, pocetNaprav, prevadzkovaHmotnost, vPatrani,
                    koniecPlatnostiStk, koniecPlatnostiEk);
                return pridaj
                    ? "Do zariadenia bolo uspesne pridane auto. "
                    : "Pridanie auta bolo neuspesne." + auto.ToString();
            }
            else
            {
                return errors;
            }

          
        }
      
        /// Vyradenie 
        /// – na základe evidenčného čísla vozidla vyradí záznam z evidencie 
        public string VyradenieAutoEvidencneCislo(string evidencneCislo)
        {
            if (evidencneCislo == "")
            {
                return "Neplatne evidencne cislo";
            }
            else
            {
                return zariadenie.VyradenieAutoEvidencneCislo(evidencneCislo) ? "Auto bolo vyradene. " : "Auto nebolo vyradene.";
            }
        }

        /// <summary>
        /// Vyradenie 
        /// – na základe VIN čísla vozidla vyradí záznam z evidencie
        /// </summary>
        /// <param name="vin"></param>
        /// <returns></returns>
        public string VyradenieAutoVinCislo(string vin)
        {
            if (vin == "")
            {
                return "Neplatne VIN. ";
            }
            else
            {
                return zariadenie.VyradenieAutoVinCislo(vin) ? "Auto bolo vyradene. " : "Auto nebolo vyradene.";
            }
        }

        /// <summary>
        /// Zmena 
        /// – umožní meniť akékoľvek údaje v zázname o aute (záznam je identifikovaný EČV) 
        /// </summary>
        /// <param name="evidencneCislo"></param>
        /// <param name="vinCislo"></param>
        /// <param name="pocetNaprav"></param>
        /// <param name="prevadzkovaHmotnost"></param>
        /// <param name="vPatrani"></param>
        /// <param name="koniecPlatnostiStk"></param>
        /// <param name="koniecPlatnostiEk"></param>
        /// <returns></returns>
        public string ZmenaUdajovAute(string evidencneCislo, string vinCislo, string pocetNapravs, string prevadzkovaHmotnosts, bool vPatrani = false, DateTime koniecPlatnostiStk = default(DateTime), DateTime koniecPlatnostiEk = default(DateTime))
        {
            int pocetNaprav;
            int prevadzkovaHmotnost;
            string errors = "";
            errors += (evidencneCislo == "") ? "Nespravne evidencne cislo.\n" : "";
            errors += (!Int32.TryParse(pocetNapravs, out pocetNaprav)) ? "Nebolo zadany spravny vstup pre: " + "Pocet naprav.\n" : "";
            errors += (!Int32.TryParse(prevadzkovaHmotnosts, out prevadzkovaHmotnost)) ? "Nebolo zadany spravny vstup pre: " + "Prevadzkova hmotnost\n" : "";
            errors += (evidencneCislo.Length > maxEvidenceCislo)
                ? "Evidencne cislo musi obsahovat najviac " + maxEvidenceCislo + " znakov.\n" : "";
            errors += (vinCislo.Length > maxVinCislo)
                ? "VIN cislo musi obsahovat najviac " + maxVinCislo + " znakov.\n" : "";

            if (!errors.Equals(""))
            {
                return errors;
            }
            else
            {
                return zariadenie.ZmenaUdajovAute(evidencneCislo, vinCislo, pocetNaprav, prevadzkovaHmotnost, vPatrani,
                    koniecPlatnostiStk, koniecPlatnostiEk) ? "Zmena udajov prebehla uspesne." : "Zmena udajov bola neuspesna.";
            }
        }
        #endregion

        #region Vodicky Preukaz


        /// <summary>
        /// Pre záznamy o vodičskom preukaze: 
        /// Vyhľadanie 
        /// – podľa  čísla vodičského preukazu nájde príslušný záznam 
        /// a zobrazí všetky údaje
        /// </summary>
        /// <param name="cisloVodickeho"></param>
        /// <returns></returns>
        public string VyhladajVypisVodicky(string cisloVodickehos)
        {
            int cisloVodickeho;
            if (!Int32.TryParse(cisloVodickehos, out cisloVodickeho))
            {
                return "Nebolo zadany spravny vstup pre: " + "Cislo vodickeho.";
            }
            else
            {
                return zariadenie.VyhladajVypisVodicky(cisloVodickeho);
            }
        }
     
        /// <summary>
        /// Pridanie 
        /// – na základe vstupných údajov pridá záznam do evidencie
        /// </summary>
        /// <param name="menoVodica"></param>
        /// <param name="priezviskoVodica"></param>
        /// <param name="evidencneCisloPreukazu"></param>
        /// <param name="ukonceniePlatnosti"></param>
        /// <param name="zakazViestVozidlo"></param>
        /// <param name="dopravnePriestupky"></param>
        /// <returns></returns>
        public string PridanieVodickeho(string menoVodica, string priezviskoVodica, string evidencneCisloPreukazus,
            DateTime ukonceniePlatnosti, bool zakazViestVozidlo, string dopravnePriestupkys)
        {
            string errorHlasky = "";
            int evidencneCisloPreukazu;
            int dopravnePriestupky;
            errorHlasky += (!Int32.TryParse(evidencneCisloPreukazus, out evidencneCisloPreukazu)) ? "Nebol zadany spravny vstup pre: " + "Cislo vodickeho.\n" : "";
            errorHlasky += (!Int32.TryParse(dopravnePriestupkys, out dopravnePriestupky)) ?"Nebol zadany spravny vstup pre: " + "Dopravne priestupky.\n" : "";
            errorHlasky += (menoVodica == "") ? "Nebol zadany spravny vstup pre: " + "Meno vodica.\n" : "";
            errorHlasky += (priezviskoVodica == "") ? "Nebol zadany spravny vstup pre: " + "Priezvisko vodica.\n" : "";
            errorHlasky += (menoVodica.Length > maxMeno) ? "Meno vodica musi obsahovat najviac 35 znakov.\n" : "";
            errorHlasky += (priezviskoVodica.Length > maxPriezvisko) ?  "Priezvisko vodica musi obsahovat najviac 35 znakov.\n" : "";
            VodickyPreukaz vodicky = new VodickyPreukaz(menoVodica, priezviskoVodica, evidencneCisloPreukazu, ukonceniePlatnosti, zakazViestVozidlo, dopravnePriestupky);

            if (errorHlasky == "")
            {
                return zariadenie.PridanieVodickeho(menoVodica, priezviskoVodica, evidencneCisloPreukazu,
                    ukonceniePlatnosti,
                    zakazViestVozidlo, dopravnePriestupky)
                    ? "Vodicky bol uspesne pridany. " + vodicky.ToString()
                    : "Pridanie vodickeho preukazu bolo neuspesne. "+ vodicky.ToString();
            }
            else
            {
                return errorHlasky;
            }
            
        }
        /// <summary>
        ///  Vyradenie 
        /// – na základe čísla vodičského preukazu vyradí záznam z evidencie 
        /// </summary>
        /// <param name="cisloVodickeho"></param>
        /// <returns></returns>
        public string VyradenieVodickeho(string evidencneCisloPreukazus)
        {
            int evidencneCisloPreukazu;

            if (!Int32.TryParse(evidencneCisloPreukazus, out evidencneCisloPreukazu))
            {
                return "Nebol zadany spravny vstup pre: " + "Cislo vodickeho.\n";
            }

            return zariadenie.VyradenieVodickeho(evidencneCisloPreukazu)
                ? "Uspesne vyradeny."
                : "Neuspesne. Vodicky nebol vyradeny.";
        }

        /// <summary>
        /// Zmena 
        /// – umožní meniť akékoľvek údaje o preukaze
        /// (záznam je identifikovaný pomocou čísla vodičského preukazu)
        /// </summary>
        /// <param name="evidencneCisloPreukazu"></param>
        /// <param name="menoVodica"></param>
        /// <param name="priezviskoVodica"></param>
        /// <param name="ukonceniePlatnosti"></param>
        /// <param name="zakazViestVozidlo"></param>
        /// <param name="dopravnePriestupky"></param>
        /// <returns></returns>
        public string ZmenaUdajovVodicky( string menoVodica,
            string priezviskoVodica, string evidencneCisloPreukazus,
            DateTime ukonceniePlatnosti, bool zakazViestVozidlo, string dopravnePriestupkys)
        {
            string errorHlasky = "";
            int evidencneCisloPreukazu;
            int dopravnePriestupky;
            errorHlasky += (!Int32.TryParse(evidencneCisloPreukazus, out evidencneCisloPreukazu)) ? "Nebol zadany spravny vstup pre: " + "Cislo vodickeho.\n" : "";
            errorHlasky += (!Int32.TryParse(dopravnePriestupkys, out dopravnePriestupky)) ? "Nebol zadany spravny vstup pre: " + "Dopravne priestupky.\n" : "";
            errorHlasky += (menoVodica == "") ? "Nebol zadany spravny vstup pre: " + "Meno vodica.\n" : "";
            errorHlasky += (priezviskoVodica == "") ? "Nebol zadany spravny vstup pre: " + "Priezvisko vodica.\n" : "";
            errorHlasky += (menoVodica.Length > maxMeno) ? "Meno vodica musi obsahovat najviac 35 znakov.\n" : "";
            errorHlasky += (priezviskoVodica.Length > maxPriezvisko) ? "Priezvisko vodica musi obsahovat najviac 35 znakov.\n" : "";

            if (errorHlasky == "")
            {
                return zariadenie.ZmenaUdajovVodicky(evidencneCisloPreukazu, menoVodica, priezviskoVodica, ukonceniePlatnosti, zakazViestVozidlo, dopravnePriestupky)
                    ? "Vodicky bol uspesne zmeneny. "
                    : "Zmena vodickeho preukazu bola neuspesna. ";
            }
            else
            {
                return errorHlasky;
            }
        }

        #endregion



        #region Generovanie udajov

        public string VygenerujAuta(string pocets)
        {
            int pocet;
            if (!Int32.TryParse(pocets, out pocet))
            {
                return "Nebol zadany spravny pocet na generovanie.";
            }
            return "todo urobit";
        }

public string VygenerujVodickePreukazy(string pocets)
        {
            int pocet;
            if (!Int32.TryParse(pocets, out pocet))
            {
                return "Nebol zadany spravny pocet na generovanie.";
            }
            return "todo urobit";
        }


        #endregion

    }
}
