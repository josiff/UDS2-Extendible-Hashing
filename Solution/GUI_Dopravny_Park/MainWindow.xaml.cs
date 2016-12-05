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

        private void b_a_pridanie_auta_Click(object sender, RoutedEventArgs e)
        {
            vystup.Text = app.PridanieAuto(t_a_evidencne_cislo_vozidla.Text, t_a_vin.Text, t_a_pocet_naprav.Text, t_a_prevadzkova_hmotnost.Text,
                t_a_v_patrani.IsChecked.Value, t_a_datum_konca_STK.SelectedDate.Value,
                t_a_datum_konca_EK.SelectedDate.Value);
        }
    }
}
