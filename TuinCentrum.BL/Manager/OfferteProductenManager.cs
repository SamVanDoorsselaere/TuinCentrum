using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuinCentrum.BL.Exceptions;
using TuinCentrum.BL.Interfaces;
using TuinCentrum.BL.Model;

namespace TuinCentrum.BL.Manager
{
    public class OfferteProductenManager
    {
        private IFileProcessor fileProcessor;
        private IOfferteProductenRepository offerteProductenRepository;

        public OfferteProductenManager(IFileProcessor fileProcessor, IOfferteProductenRepository offerteProductenRepository)
        {
            this.fileProcessor = fileProcessor;
            this.offerteProductenRepository = offerteProductenRepository;
        }

        public void UploadOfferteProducten(string fileName)
        {
            if (offerteProductenRepository == null)
            {
                throw new ArgumentNullException(nameof(offerteProductenRepository), "OfferteProductenRepository is niet geïnitialiseerd.");
            }

            List<string> soorten = fileProcessor.LeesOfferteProducten(fileName);
            List<OfferteProduct> offerteProduct = MaakOfferteProducten(soorten);
            foreach (OfferteProduct offerteproducten in offerteProduct)
            {
                if (!offerteProductenRepository.HeeftOfferteProducten(offerteproducten))
                    offerteProductenRepository.SchrijfOfferteProducten(offerteproducten);
            }
        }


        private List<OfferteProduct> MaakOfferteProducten(List<string> soorten)
        {
            List<OfferteProduct> offerteProductenList = new List<OfferteProduct>();
            foreach (string offerteProductenString in soorten)
            {
                string[] offerteProductenData = offerteProductenString.Split('|');
                if (offerteProductenData.Length == 3) // Controleren of er 3 delen zijn gescheiden door '|'
                {
                    try
                    {
                        string offerteProductenOfferteId = offerteProductenData[0]; // Gebruik index 0 voor het OfferteId
                        string offerteProductenProductenId = offerteProductenData[1]; // Gebruik index 1 voor het ProductenId
                        string offerteProductenAantal = offerteProductenData[2]; // Gebruik index 2 voor het Aantal
                        OfferteProduct offerteProduct = new OfferteProduct(
                            int.Parse(offerteProductenOfferteId),
                            int.Parse(offerteProductenProductenId),
                            int.Parse(offerteProductenAantal)
                        );
                        offerteProductenList.Add(offerteProduct);
                    }
                    catch (FormatException ex)
                    {
                        throw new DomeinException("Ongeldige offerte productgegevens", ex);
                    }
                }
                else
                {
                    throw new DomeinException($"Ongeldige offerte productgegevens: {offerteProductenString}");
                }
            }
            return offerteProductenList;
        }

    }
}
