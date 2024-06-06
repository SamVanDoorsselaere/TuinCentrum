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
    public class KlantManager
    {
        private IFileProcessor fileProcessor;
        private IKlantRepository klantRepository;

        public KlantManager(IFileProcessor fileProcessor, IKlantRepository klantRepository)
        {
            this.fileProcessor = fileProcessor;
            this.klantRepository = klantRepository;
        }

        public void UploadKlanten(string fileName)
        {
            List<string> soorten = fileProcessor.LeesKlanten(fileName);
            List<Klanten> klanten = MaakKlanten(soorten);
            foreach (Klanten klant in klanten)
            {
                if (!klantRepository.HeeftKlant(klant))
                    klantRepository.SchrijfKlant(klant);
            }
        }

        private List<Klanten> MaakKlanten(List<string> klanten)
        {
            List<Klanten> klantList = new List<Klanten>();
            foreach (string klantString in klanten)
            {
                string[] klantData = klantString.Split('|');
                if (klantData.Length == 3) // Controleren of er 3 delen zijn gescheiden door '|'
                {
                    try
                    {
                        string klantNaam = klantData[1];
                        string klantAdres = klantData[2];
                        Klanten klant = new Klanten(klantNaam, klantAdres);
                        klantList.Add(klant);
                    }
                    catch (DomeinException ex)
                    {
                        throw new DomeinException("Ongeldige klantgegevens", ex);
                    }
                }
                else
                {
                    throw new DomeinException($"Ongeldige klantgegevens: {klantString}");
                }
            }
            return klantList;
        }



    }
}
