using System;
using System.Windows;
using TuinCentrum.BL.Interfaces;
using TuinCentrum.BL.Model;
using TuinCentrum.DL.Exceptions;
using TuinCentrum.DL.Repositories;

namespace TuinCentrum.UI
{
    public partial class ZoekOffertes : Window
    {
        private IOfferteRepository offerteRepository;

        public ZoekOffertes(string connectionString)
        {
            InitializeComponent();
            offerteRepository = new OfferteRepository(connectionString);
        }

        private void Button_Click_ZoekOfferte(object sender, RoutedEventArgs e)
        {
            int offerteId;
            if (int.TryParse(offerteIdTextBox.Text, out offerteId))
            {
                try
                {
                    var offerte = offerteRepository.GeefOfferte(offerteId);
                    if (offerte != null)
                    {
                        offerteDetailsTextBlock.Text = $"Offerte ID: {offerte.OfferteID}\nDatum: {offerte.Datum}\nKlant ID: {offerte.KlantID}\nAfhalen: {offerte.Afhalen}\nAanleg: {offerte.Aanleg}";
                    }
                    else
                    {
                        offerteDetailsTextBlock.Text = "Geen offerte gevonden.";
                    }
                }
                catch (DataException ex)
                {
                    MessageBox.Show($"Fout bij het ophalen van offertegegevens: {ex.Message}\n\nDetails: {ex.InnerException?.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Onverwachte fout: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Voer een geldig offerte ID in.");
            }
        }
    }
}
