using System;

namespace TuinCentrum.BL.Model
{
    public class Offertes
    {
        public int? Id { get; private set; }
        public DateTime Datum { get; set; }
        public Klanten Klant { get; set; }
        List<Producten> ProductenList = new List<Producten>();
        public bool Afhalen { get; set; } 

        public Offertes(int id, DateTime datum, Klanten klant, bool afhalen)
        {
            Id = id;
            Datum = datum;
            Klant = klant;
            Afhalen = afhalen;
        }

        // Methode om totaalprijs te berekenen
        public double CalculateTotalPrice()
        {
            double totalPrice = ProductenList.Sum(product => product.Prijs);

            // Reken totaalprijs uit
            if (totalPrice > 2000)
            {
                totalPrice *= 0.95; //  5% 
            }
            else if (totalPrice > 5000)
            {
                totalPrice *= 0.90; //  10% 
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

            return totalPrice;
        }

        // Methode voor landscaping prijs te berekenen
        public double CalculateLandscapingPrice()
        {
            double totalPrice = ProductenList.Sum(product => product.Prijs);

            if (totalPrice < 2000)
            {
                totalPrice *= 1.15; // 15% 
            }
            else if (totalPrice > 2000 && totalPrice <= 5000)
            {
                totalPrice *= 1.10; // 10% 
            }
            else if (totalPrice > 5000)
            {
                totalPrice *= 1.05; // 5% 
            }

            return totalPrice;
        }

    }
}
