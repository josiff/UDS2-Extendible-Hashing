using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataStructureLogic;

namespace GUI_Dopravny_Park
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public AppCore app;
        public MainWindow()
        {
            InitializeComponent();
            app = new AppCore();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void t_a_evidencne_cislo_vozidla_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        #region Auta
        
        private void b_a_pridanie_auta_Click(object sender, RoutedEventArgs e)
        {
            vystup.Text = app.PridanieAuto(t_a_evidencne_cislo_vozidla.Text, t_a_vin.Text, t_a_pocet_naprav.Text, t_a_prevadzkova_hmotnost.Text,
                t_a_v_patrani.IsChecked.Value, t_a_datum_konca_STK.SelectedDate.Value,
                t_a_datum_konca_EK.SelectedDate.Value);
        }

        private void b_a_zmena_auta_Click(object sender, RoutedEventArgs e)
        {
            vystup.Text = app.ZmenaUdajovAute(t_a_evidencne_cislo_vozidla.Text, t_a_vin.Text, t_a_pocet_naprav.Text, t_a_prevadzkova_hmotnost.Text,
                t_a_v_patrani.IsChecked.Value, t_a_datum_konca_STK.SelectedDate.Value,
                t_a_datum_konca_EK.SelectedDate.Value);
        }

        private void b_a_vyhladaj_auto_evidencne_cislo_Click(object sender, RoutedEventArgs e)
        {
            vystup.Text = app.VyhladajVypisAutoEvidencneCislo(t_a_udaj_pre_operacie_auta.Text);
        }

        private void b_a_vyhladaj_auto_vin_cislo_Click(object sender, RoutedEventArgs e)
        {

            vystup.Text = app.VyhladajVypisAutoVin(t_a_udaj_pre_operacie_auta.Text);

        }

        private void b_a_vyrad_auto_evidencne_cislo_Click(object sender, RoutedEventArgs e)
        {
            vystup.Text = app.VyradenieAutoEvidencneCislo(t_a_udaj_pre_operacie_auta.Text);

        }

        private void b_a_vyrad_auto_vin_cislo_Click(object sender, RoutedEventArgs e)
        {
            vystup.Text = app.VyradenieAutoVinCislo(t_a_udaj_pre_operacie_auta.Text);

        }

        private void b_a_vygeneruj_auta_Click(object sender, RoutedEventArgs e)
        {
            vystup.Text = app.VygenerujAuta(t_a_udaj_pre_operacie_auta.Text);
        }
        #endregion

        #region Vodicky preukaz
        private void b_v_pridanie_vodicak_Click(object sender, RoutedEventArgs e)
        {
            vystup.Text = app.PridanieVodickeho(t_v_meno.Text, t_v_priezvisko.Text, t_v_evidencneCisloPreukazu.Text,
                t_v_datum_ukoncenia_platnosti.SelectedDate.Value, t_v_zakaz_viest_vozidlo.IsChecked.Value,
                t_v_pocet_dopravnych_priestupkov.Text);
        }

        private void b_v_zmena_vodicak_Click(object sender, RoutedEventArgs e)
        {
            vystup.Text = app.ZmenaUdajovVodicky(t_v_meno.Text, t_v_priezvisko.Text, t_v_evidencneCisloPreukazu.Text,
         t_v_datum_ukoncenia_platnosti.SelectedDate.Value, t_v_zakaz_viest_vozidlo.IsChecked.Value,
         t_v_pocet_dopravnych_priestupkov.Text);
        }

        private void b_v_vyhladaj_vodicak_Click(object sender, RoutedEventArgs e)
        {
            vystup.Text = app.VyhladajVypisVodicky(t_v_vstupny_udaj_vodicky.Text);
        }

        private void b_a_vyrad_vodicak_Click(object sender, RoutedEventArgs e)
        {
            vystup.Text = app.VyradenieVodickeho(t_v_vstupny_udaj_vodicky.Text);
        }

        private void b_a_vygeneruj_vodicak_Click(object sender, RoutedEventArgs e)
        {
            vystup.Text = app.VygenerujVodickePreukazy(t_v_pocet_vygeneruj.Text);
        }
        #endregion
    }
}
