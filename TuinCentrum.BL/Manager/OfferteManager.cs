using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuinCentrum.BL.Exceptions;
using TuinCentrum.BL.Interfaces;
using TuinCentrum.BL.Model;

namespace TuinCentrum.BL.Manager
{
    public class OfferteManager
    {
        private IFileProcessor fileProcessor;
        private IOfferteRepository offerteRepository;

        public OfferteManager(IFileProcessor fileProcessor, IOfferteRepository offerteRepository)
        {
            this.fileProcessor = fileProcessor;
            this.offerteRepository = offerteRepository;
        }

        public void UploadOfferte(string fileName)
        {
            List<string> soorten = fileProcessor.LeesOffertes(fileName);
            List<Offertes> offertes = MaakOfferte(soorten);
            foreach (Offertes offerte in offertes)
            {
                if (!offerteRepository.HeeftOfferte(offerte))
                    offerteRepository.SchrijfOfferte(offerte);
            }
        }


        private List<Offertes> MaakOfferte(List<string> offertes)
        {
            List<Offertes> offerteList = new List<Offertes>();
            foreach (string offerteString in offertes)
            {
                string[] offerteData = offerteString.Split('|');
                if (offerteData.Length == 6) // Controleren of er 6 delen zijn gescheiden door '|'
                {
                    try
                    {
                        int offerteId = int.Parse(offerteData[0]); // Parsing naar int
                        DateTime offerteDatum = DateTime.ParseExact(offerteData[1], "d/M/yyyy", CultureInfo.InvariantCulture); // Parsing naar DateTime
                        int offerteKlantnummer = int.Parse(offerteData[2]); // Parsing naar int
                        bool offerteAfhaal = bool.Parse(offerteData[3]); // Parsing naar bool
                        bool offerteAanleg = bool.Parse(offerteData[4]); // Parsing naar bool
                        int aantalProducten = int.Parse(offerteData[5]); // Parsing naar int

                        Offertes offerte = new Offertes(offerteId, offerteDatum, offerteKlantnummer, offerteAfhaal, offerteAanleg, aantalProducten);
                        offerteList.Add(offerte);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Fout bij verwerken van offerte: {offerteString}, fout: {ex.Message}");
                        throw new DomeinException("Ongeldige offertegegevens", ex);
                    }
                }
                else
                {
                    Console.WriteLine($"Ongeldige offertegegevens: {offerteString}");
                    throw new DomeinException($"Ongeldige offertegegevens: {offerteString}");
                }
            }
            return offerteList;
        }
    }
}
