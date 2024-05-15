using System;

namespace TuinCentrum.BL.Model
{
    internal class OfferteProducten
    {
        public int OfferteId { get; set; }
        public int ProductId { get; set; }
        public int AantalExemplaren { get; set; }

        public OfferteProducten(int offerteId, int productId, int aantalExemplaren)
        {
            OfferteId = offerteId;
            ProductId = productId;
            AantalExemplaren = aantalExemplaren;
        }
    }
}
