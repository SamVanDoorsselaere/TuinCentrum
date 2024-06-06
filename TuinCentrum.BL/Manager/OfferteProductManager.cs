using System;
using System.IO;
using System.Linq;
using TuinCentrum.BL.Interfaces;
using TuinCentrum.BL.Model;
using TuinCentrum.DL.Repositories;

namespace TuinCentrum.BL.Manager
{
    public class OfferteProductManager
    {
        private IOfferteProductenRepository offerteProductRepository;
        private IFileProcessor fileProcessor;

        public OfferteProductManager(IFileProcessor fileProcessor, IOfferteProductenRepository offerteProductRepository)
        {
            this.fileProcessor = fileProcessor;
            this.offerteProductRepository = offerteProductRepository;
        }
        public void UploadOfferteProducten(string fileName)
        {
            try
            {
                string[] lines = File.ReadAllLines(fileName);

                foreach (string line in lines)
                {
                    string[] values = line.Split('|');
                    if (values.Length == 3)
                    {
                        int offerteID = int.Parse(values[0]);
                        int productID = int.Parse(values[1]);
                        int aantal = int.Parse(values[2]);

                        OfferteProduct offerteProduct = new OfferteProduct(offerteID, productID, aantal);
                        offerteProductRepository.SchrijfOfferteProduct(offerteProduct);
                    }
                    else
                    {
                        Console.WriteLine($"Ongeldige invoer: {line}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fout bij het uploaden van offerteproducten: {ex.Message}");
            }
        }
    }

}
