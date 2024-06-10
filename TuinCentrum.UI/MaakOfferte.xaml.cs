using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;
using TuinCentrum.BL.Interfaces;
using TuinCentrum.BL.Model;
using System.Windows.Media;

namespace TuinCentrum.UI
{
    public partial class MaakOfferte : Window
    {
        private string connectionString = @"Data Source=LAPTOP-6EGCK7EE\SQLEXPRESS;Initial Catalog=TuinCentrum;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
        private Klanten geselecteerdeKlant;
        private ObservableCollection<Klanten> gevondenKlanten = new ObservableCollection<Klanten>();
        private IProductRepository productRepository;
        private ObservableCollection<Producten> beschikbareProducten = new ObservableCollection<Producten>();
        private IKlantRepository klantRepository;

        public MaakOfferte()
        {
            InitializeComponent();
            lvKlanten.ItemsSource = gevondenKlanten;
            productRepository = new ProductRepository(connectionString);
            klantRepository = new KlantRepository(connectionString);
            LaadProducten();
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

        private void LaadProducten()
        {
            var producten = productRepository.GeefAlleProducten();
            foreach (var product in producten)
            {
                beschikbareProducten.Add(product);
            }
            lvProducten.ItemsSource = beschikbareProducten;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTotalePrijs();
        }

        private void UpdateTotalePrijs()
        {
            double totalePrijs = 0;
            foreach (var item in lvProducten.Items)
            {
                var container = lvProducten.ItemContainerGenerator.ContainerFromItem(item) as ListViewItem;
                if (container != null)
                {
                    var textBox = FindVisualChild<TextBox>(container);
                    if (textBox != null && int.TryParse(textBox.Text, out int aantal) && aantal > 0)
                    {
                        var product = item as Producten;
                        if (product != null)
                        {
                            totalePrijs += product.Prijs * aantal;
                        }
                    }
                }
            }
            txtTotalePrijs.Text = $"Totale Prijs: €{totalePrijs:F2}";
        }

        private static T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            if (parent == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T typedChild)
                {
                    return typedChild;
                }

                var childOfChild = FindVisualChild<T>(child);
                if (childOfChild != null)
                {
                    return childOfChild;
                }
            }
            return null;
        }
    }
}
