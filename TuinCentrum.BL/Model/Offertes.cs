using System;
using TuinCentrum.BL.Exceptions;

namespace TuinCentrum.BL.Model
{
    public class Offertes
    {
        public int? OfferteID { get; private set; }
        public DateTime Datum { get; set; }
        public int? KlantID {get; set;}
        public Dictionary<Producten, int> ProductenList { get; set; } = new Dictionary<Producten, int>();
        public bool Afhalen { get; set; }
        public bool Aanleg { get; set; }
        public int AantalProducten { get; set; }

        public Offertes(int id, DateTime datum, int klantID, bool afhalen, bool aanleg, int aantalProducten)
        {
            OfferteID = id;
            Datum = datum;
            KlantID = klantID;
            Afhalen = afhalen;
            Aanleg = aanleg;
            AantalProducten = aantalProducten;
        }
        public Offertes(DateTime datum, int? klantID, bool afhalen, bool aanleg, int aantalProducten)
        {
            Datum = datum;
            KlantID = klantID;
            Afhalen = afhalen;
            Aanleg = aanleg;
            AantalProducten = aantalProducten;
        }

        public Offertes(int offerteID, DateTime datum, int klantID, bool afhalen, bool aanleg)
        {
            OfferteID = offerteID;
            Datum = datum;
            KlantID = klantID;
            Afhalen = afhalen;
            Aanleg = aanleg;
        }


        public Offertes(int id, int aantalProducten)
        {
            OfferteID = id;
            AantalProducten = aantalProducten;
        }

        public void VoegProductToe(Producten product, int aantal)
        {
            if (product == null)
                throw new DomeinException("Product kan niet null zijn.");

            if (aantal <= 0)
                throw new DomeinException("Aantal moet groter zijn dan 0.");

            if (ProductenList.ContainsKey(product))
            {
                ProductenList[product] += aantal;
            }
            else
            {
                ProductenList.Add(product, aantal);
            }
        }

        public double CalculateTotalPrice()
        {
            double totalPrice = ProductenList.Sum(item => item.Key.Prijs * item.Value);

            if (totalPrice > 2000)
            {
                totalPrice *= 0.95; // 5% 
            }
            else if (totalPrice > 5000)
            {
                totalPrice *= 0.90; // 10% 
            }

            if (!Afhalen)
            {
                double deliveryCost = 0;
                if (totalPrice < 500)
                {
                    deliveryCost = 100;
                }
                else if (totalPrice >= 500 && totalPrice < 1000)
                {
                    deliveryCost = 50;
                }
                totalPrice += deliveryCost;
            }

            if (Aanleg)
            {
                double landscapingCost = 0;
                if (totalPrice < 2000)
                {
                    landscapingCost = totalPrice * 0.15; // 15% 
                }
                else if (totalPrice > 2000 && totalPrice <= 5000)
                {
                    landscapingCost = totalPrice * 0.10; // 10% 
                }
                else if (totalPrice > 5000)
                {
                    landscapingCost = totalPrice * 0.05; // 5% 
                }
                totalPrice += landscapingCost;
            }

            return totalPrice;
        }

    }
}
