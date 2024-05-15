﻿using System;

namespace TuinCentrum.BL.Model
{
    public class Offertes
    {
        public int? Id { get; private set; }
        public DateTime Datum { get; set; }
        public Klanten Klant { get; set; }
        public List<Producten> ProductenList { get; set; } = new List<Producten>();
        public bool Afhalen { get; set; }
        public bool Aanleg { get; set; } 
        public int AantalProducten { get; set; }

        public Offertes(int id, DateTime datum, Klanten klant, bool afhalen, bool aanleg, int aantalProducten)
        {
            Id = id;
            Datum = datum;
            Klant = klant;
            Afhalen = afhalen;
            Aanleg = aanleg;
            AantalProducten = aantalProducten;
        }

        public void VoegProductToe(Producten product)
        {
            ProductenList.Add(product);
        }

        public double CalculateTotalPrice()
        {
            double totalPrice = ProductenList.Sum(product => product.Prijs) * AantalProducten;

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
