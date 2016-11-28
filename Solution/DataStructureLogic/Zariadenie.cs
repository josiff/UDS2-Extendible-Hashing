using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureLogic
{
    /// <summary>
    /// Dopravná polícia uvažuje nad zakúpením špecializovaných zariadení, 
    /// ktoré budú namontované do nových vozidiel. 
    /// 
    /// Zariadenie bude automaticky 
    /// zaznamenávať obraz z kamery umiestnenej vo vozidle, 
    /// analyzovať EČV okoloidúcich vozidiel 
    /// a vyhľadávať ich v databáze vozidiel celej EÚ. 
    /// 
    /// Ak zariadenie zaznamená kradnuté auto, 
    /// alebo auto bez platnej STK (prípadne EK), 
    /// automaticky upozorní na túto skutočnosť posádku vozidla. 
    /// 
    /// Zariadenie bude taktiež umožňovať kontrolu platnosti vodičských preukazov 
    /// a náhľad do karty vodiča.
    /// Je potrebné zabezpečiť, aby zariadenie pracovalo aj v offline režime 
    /// a kontrola vozidla podľa EČV bola čo najrýchlejšia. 
    /// </summary>
    class Zariadenie
    {
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
            Auto auto = VyhladajAutoEvidencne(evidencneCislo);
            return "todo";
        }

        public Auto VyhladajAutoEvidencne(string evidencneCislo)
        {
            return default(Auto);
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
            Auto auto = VyhladajAutoVin(vinCislo);
            return "todo";
        }
        public Auto VyhladajAutoVin(string vinCislo)
        {
            return default(Auto);
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
        public bool PridanieAuto(int evidencneCisloVozidla, string vinCislo, int pocetNaprav, int prevadzkovaHmotnost, bool vPatrani, DateTime koniecPlatnostiStk, DateTime koniecPlatnostiEk)
        {
            Auto auto = new Auto(evidencneCisloVozidla, vinCislo, pocetNaprav, prevadzkovaHmotnost, vPatrani, koniecPlatnostiStk, koniecPlatnostiEk);
            return false;
        }
        public bool PridanieAuto(Auto auto)
        {
           
            return false;
        }

        /// Vyradenie 
        /// – na základe evidenčného čísla vozidla vyradí záznam z evidencie 
        public bool VyradenieAutoEvidencneCislo(string evidencneCislo)
        {
            Auto auto = VyhladajAutoEvidencne(evidencneCislo);
            return false;
        }

        /// <summary>
        /// Vyradenie 
        /// – na základe VIN čísla vozidla vyradí záznam z evidencie
        /// </summary>
        /// <param name="vin"></param>
        /// <returns></returns>
        public bool VyradenieAutoVinCislo(string vin)
        {
            Auto auto = VyhladajAutoVin(vin);
            return false;
        }

        /// <summary>
        /// Zmena 
        /// – umožní meniť akékoľvek údaje v zázname o aute (záznam je identifikovaný EČV) 
        /// </summary>
        /// <param name="evidencneCislo"></param>
        /// <param name="auto"></param>
        /// <returns></returns>
        public bool ZmenaUdajovAute(int evidencneCislo, Auto auto)
        {
            return false;
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
        public bool ZmenaUdajovAute(int evidencneCislo, string vinCislo = null, int pocetNaprav=1, int prevadzkovaHmotnost=550, bool vPatrani = false, DateTime koniecPlatnostiStk = default(DateTime), DateTime koniecPlatnostiEk =default(DateTime))
        {
            return false;
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
        public string VyhladajVypisVodicky(int cisloVodickeho)
        {
            VodickyPreukaz vodicky = VyhladajVodickyPreukaz(cisloVodickeho);
            return "todo";
        }
        public VodickyPreukaz VyhladajVodickyPreukaz(int cisloVodickeho)
        {
            return default(VodickyPreukaz);
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
        public bool PridanieVodickeho(string menoVodica, string priezviskoVodica, int evidencneCisloPreukazu,
            DateTime ukonceniePlatnosti, bool zakazViestVozidlo, int dopravnePriestupky)
        {
            VodickyPreukaz vodicky = new VodickyPreukaz( menoVodica,  priezviskoVodica,  evidencneCisloPreukazu,  ukonceniePlatnosti,  zakazViestVozidlo,  dopravnePriestupky);
            return PridanieVodickeho(vodicky);
        }

        public bool PridanieVodickeho(VodickyPreukaz vodicky)
        {
            return false;
        }
        /// <summary>
        ///  Vyradenie 
        /// – na základe čísla vodičského preukazu vyradí záznam z evidencie 
        /// </summary>
        /// <param name="cisloVodickeho"></param>
        /// <returns></returns>
        public bool VyradenieVodickeho(int cisloVodickeho)
        {
            VodickyPreukaz vodicky = VyhladajVodickyPreukaz(cisloVodickeho);

            return false;
        }

        public bool VyradenieVodickeho(VodickyPreukaz vodickyPreukaz)
        {
            return false;
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
        public bool ZmenaUdajovVodicky(int evidencneCisloPreukazu, string menoVodica = null,
            string priezviskoVodica = null,
            DateTime ukonceniePlatnosti = default(DateTime), bool zakazViestVozidlo = false, int dopravnePriestupky = 0)
        {
            return false;
        }

        public bool ZmenaUdajovVodicky(int evidencneCisloPreukazu, VodickyPreukaz vodicky)
        {
            return false;
        }
        #endregion
    }
}
