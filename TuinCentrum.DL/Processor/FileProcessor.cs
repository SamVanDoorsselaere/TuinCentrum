using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuinCentrum.BL.Interfaces;

namespace TuinCentrum.DL.Processor
{
    public class FileProcessor : IFileProcessor
    {
        public List<string> LeesKlanten(string fileName)
        {
            try
            {
                List<string> klanten = new List<string>();
                using (StreamReader sr = new StreamReader(fileName))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        klanten.Add(line.Trim());
                    }
                }
                return klanten;
            }
            catch (Exception ex) { throw new Exception($"FileProcessor.leesKlanten - {fileName}", ex); }
        }

        public List<string> LeesProducten(string fileName)
        {
            try
            {
                List<string> klanten = new List<string>();
                using (StreamReader sr = new StreamReader(fileName))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        klanten.Add(line.Trim());
                    }
                }
                return klanten;
            }
            catch (Exception ex) { throw new Exception($"FileProcessor.leesProducten - {fileName}", ex); }
        }
        public List<string> LeesOffertes(string fileName)
        {
            try
            {
                List<string> klanten = new List<string>();
                using (StreamReader sr = new StreamReader(fileName))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        klanten.Add(line.Trim());
                    }
                }
                return klanten;
            }
            catch (Exception ex) { throw new Exception($"FileProcessor.leesOffertes - {fileName}", ex); }
        }

        public List<string> LeesOfferteProducten(string fileName)
        {
            try
            {
                List<string> klanten = new List<string>();
                using (StreamReader sr = new StreamReader(fileName))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        klanten.Add(line.Trim());
                    }
                }
                return klanten;
            }
            catch (Exception ex) { throw new Exception($"FileProcessor.leesOffertes - {fileName}", ex); }
        }
    }
}
