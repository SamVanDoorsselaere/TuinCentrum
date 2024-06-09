using Microsoft.Win32;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TuinCentrum.BL.Interfaces;
using TuinCentrum.BL.Manager;
using TuinCentrum.DL.Exceptions;

namespace TuinCentrum.UI
{
    public partial class MainWindow : Window
    {
        private string connectionString = @"Data Source=LAPTOP-6EGCK7EE\SQLEXPRESS;Initial Catalog=TuinCentrum;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
        private IKlantRepository klantRepository;

        public MainWindow()
        {
            InitializeComponent();
            klantRepository = new KlantRepository(connectionString);
        }

        private void Button_Click_Klanten(object sender, RoutedEventArgs e)
        {
            try
            {
                var klanten = klantRepository.GeefAlleKlanten();
                if (klanten.Count > 0)
                {
                    var klant = klanten[0]; // Hier zou je wellicht een specifieke klant willen kiezen
                    InfoKlanten infoKlantenWindow = new InfoKlanten(klant);
                    infoKlantenWindow.Show();
                }
                else
                {
                    MessageBox.Show("Geen klanten gevonden.");
                }
            }
            catch (DataException ex)
            {
                MessageBox.Show($"Fout bij het ophalen van klantgegevens: {ex.Message}\n\nDetails: {ex.InnerException?.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Onverwachte fout: {ex.Message}");
            }
        }

        private void Button_Click_OfferteAanmaken(object sender, RoutedEventArgs e)
        {
            MaakOfferte maakOfferteWindow = new MaakOfferte();
            maakOfferteWindow.Show();
        }
        private void Button_Click_Offertes(object sender, RoutedEventArgs e)
        {
            ZoekOffertes zoekOffertesWindow = new ZoekOffertes(connectionString);
            zoekOffertesWindow.Show();
        }
    }
}