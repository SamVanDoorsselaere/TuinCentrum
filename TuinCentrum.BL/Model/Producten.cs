using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TuinCentrum.BL.Exceptions;

namespace TuinCentrum.BL.Model
{
    public class Producten
    {
        public int Id { get; private set; }

        private string nederlandseNaam;
        public string NederlandseNaam
        {
            get { return nederlandseNaam; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new DomeinException("Nederlandse naam is verplicht.");
                nederlandseNaam = value;
            }
        }

        private string wetenschappelijkeNaam;
        public string WetenschappelijkeNaam
        {
            get { return wetenschappelijkeNaam; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new DomeinException("Wetenschappelijke naam is verplicht.");
                wetenschappelijkeNaam = value;
            }
        }

        private string beschrijving;
        public string Beschrijving
        {
            get { return beschrijving; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new DomeinException("Beschrijving is verplicht.");
                beschrijving = value;
            }
        }

        private double prijs;
        public double Prijs
        {
            get { return prijs; }
            set
            {
                if (value <= 0)
                    throw new DomeinException("Prijs moet groter zijn dan 0.");
                prijs = value;
            }
        }

        public Producten(string nederlandseNaam, string wetenschappelijkeNaam, string beschrijving, double prijs)
        {
            NederlandseNaam = nederlandseNaam;
            WetenschappelijkeNaam = wetenschappelijkeNaam;
            Beschrijving = beschrijving;
            Prijs = prijs;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Nederlandse Naam: {NederlandseNaam}, Wetenschappelijke Naam: {WetenschappelijkeNaam}, Beschrijving: {Beschrijving}, Prijs: {Prijs:C}";
        }
    }
}
