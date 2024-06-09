using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using TuinCentrum.BL.Interfaces;
using TuinCentrum.BL.Model;

namespace TuinCentrum.UI
{
    public partial class MaakOfferte : Window
    {
        private string connectionString = @"Data Source=LAPTOP-6EGCK7EE\SQLEXPRESS;Initial Catalog=TuinCentrum;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
        private Klanten geselecteerdeKlant;
        private ObservableCollection<Klanten> gevondenKlanten = new ObservableCollection<Klanten>();
        private IKlantRepository klantRepository;

        public MaakOfferte()
        {
            InitializeComponent();
            lvKlanten.ItemsSource = gevondenKlanten;
            klantRepository = new KlantRepository(connectionString);

        }

        private void Button_ZoekKlant_Click(object sender, RoutedEventArgs e)
        {
            string klantNaam = txtKlantNaam.Text.Trim();
            gevondenKlanten.Clear();
            var gevonden = klantRepository.ZoekKlantenOpNaam(klantNaam);
            foreach (var klant in gevonden)
            {
                gevondenKlanten.Add(klant);
            }
        }

        private void lvKlanten_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            geselecteerdeKlant = lvKlanten.SelectedItem as Klanten;
        }
    }
}
