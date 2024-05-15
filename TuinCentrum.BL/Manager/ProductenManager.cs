using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuinCentrum.BL.Exceptions;
using TuinCentrum.BL.Model;

namespace TuinCentrum.BL.Manager
{
    public class ProductenManager
    {
        private List<Producten> productenLijst = new List<Producten>();

        public void VoegProductToe(Producten product)
        {
            productenLijst.Add(product);
        }

        public List<Producten> GetProducten()
        {
            return new List<Producten>(productenLijst);
        }

        public Producten GetProductById(int id)
        {
            return productenLijst.FirstOrDefault(p => p.Id == id);
        }

        public void VerwijderProduct(int id)
        {
            var product = GetProductById(id);
            if (product != null)
            {
                productenLijst.Remove(product);
            }
            else
            {
                throw new DomeinException("Product niet gevonden.");
            }
        }

        public void UpdateProduct(int id, string nederlandseNaam, string wetenschappelijkeNaam, string beschrijving, double prijs)
        {
            var product = GetProductById(id);
            if (product != null)
            {
                product.NederlandseNaam = nederlandseNaam;
                product.WetenschappelijkeNaam = wetenschappelijkeNaam;
                product.Beschrijving = beschrijving;
                product.Prijs = prijs;
            }
            else
            {
                throw new DomeinException("Product niet gevonden.");
            }
        }
    }
}
