using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuinCentrum.BL.Model
{
    public class Klanten
    {
        public int? KlantID { get; set; }
        private string naam;
        public string Naam
        {
            get { return naam; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Naam cannot be null or empty.", nameof(value));
                naam = value;
            }
        }
        private string adres;
        public string Adres
        {
            get { return adres; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Adres cannot be null or empty.", nameof(value));
                adres = value;
            }
        }
        public Klanten(string naam)
        {
            Naam = naam;
        }
        public Klanten(string naam, string adres)
        {
            Naam = naam;
            Adres = adres;
        }

        public Klanten(int? id, string naam, string adres)
        {
            KlantID = id;
            Naam = naam;
            Adres = adres;
        }

    }
}
