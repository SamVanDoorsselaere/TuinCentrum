using Microsoft.Win32;
using System.Diagnostics;
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
using TuinCentrum.DL.Processor;

namespace TuinCentrumUI_DataUpload
{
    public partial class MainWindow : Window
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        string connectionString = @"Data Source=LAPTOP-6EGCK7EE\SQLEXPRESS;Initial Catalog=TuinCentrum;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
        IFileProcessor processor;
        IKlantRepository klantRepository;
        IProductRepository productRepository;
        IOfferteRepository offerteRepository;
        KlantManager km;
        ProductenManager pm;
        OfferteManager om;

        public MainWindow()
        {
            InitializeComponent();
            openFileDialog.DefaultExt = ".txt";
            openFileDialog.Filter = "Text documents (.txt)|*.txt";
            openFileDialog.InitialDirectory = @"C:\data\tuin";
            openFileDialog.Multiselect = true;
            processor = new FileProcessor();
            productRepository = new ProductRepository(connectionString);
            klantRepository = new KlantRepository(connectionString);
            offerteRepository = new OfferteRepository(connectionString);
            km = new KlantManager(processor, klantRepository);
            pm = new ProductenManager(processor, productRepository);
            om = new OfferteManager(processor, offerteRepository);
        }

        private void Button_Click_Klanten(object sender, RoutedEventArgs e)
        {
            bool? result = openFileDialog.ShowDialog();
            if (result == true)
            {
                var fileNames = openFileDialog.FileNames;
                KlantenFileListBox.ItemsSource = fileNames;
                openFileDialog.FileName = null;
            }
        }

        private void Button_Click_UploadKlanten(object sender, RoutedEventArgs e)
        {
            foreach (string fileName in KlantenFileListBox.ItemsSource)
            {
                km.UploadKlanten(fileName); 
            }
            MessageBox.Show("Upload klaar", "Klanten");
        }

        private void Button_Click_Producten(object sender, RoutedEventArgs e)
        {
            bool? result = openFileDialog.ShowDialog();
            if (result == true)
            {
                var fileNames = openFileDialog.FileNames;
                ProductenFileListBox.ItemsSource = fileNames;
                openFileDialog.FileName = null;
            }
        }

        private void Button_Click_UploadProducten(object sender, RoutedEventArgs e)
        {
            foreach (string fileName in ProductenFileListBox.ItemsSource)
            {
                pm.UploadProducten(fileName);
            }
            MessageBox.Show("Upload klaar", "Producten");
        }

        private void Button_Click_Offertes(object sender, RoutedEventArgs e)
        {
            bool? result = openFileDialog.ShowDialog();
            if (result == true)
            {
                var fileNames = openFileDialog.FileNames;
                OffertesFileListBox.ItemsSource = fileNames;
                openFileDialog.FileName = null;
            }
        }

        private void Button_Click_UploadOffertes(object sender, RoutedEventArgs e)
        {
            foreach (string fileName in OffertesFileListBox.ItemsSource)
            {
                om.UploadOfferte(fileName);
            }
            MessageBox.Show("Upload klaar", "Offertes");
        }

        private void Button_Click_OfferteProducten(object sender, RoutedEventArgs e)
        {
            bool? result = openFileDialog.ShowDialog();
            if (result == true)
            {
                var fileNames = openFileDialog.FileNames;
                OfferteProductenFileListBox.ItemsSource = fileNames;
                openFileDialog.FileName = null;
            }
        }

        private void Button_Click_UploadOfferteProducten(object sender, RoutedEventArgs e)
        {
            foreach (string fileName in OfferteProductenFileListBox.ItemsSource)
            {
                om.UploadOfferte(fileName);
            }
            MessageBox.Show("Upload klaar", "OfferteProducten");
        }

    }
}