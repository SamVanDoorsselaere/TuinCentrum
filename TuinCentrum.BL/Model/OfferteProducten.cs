namespace TuinCentrum.BL.Model
{
    public class OfferteProduct
    {
        public int OfferteId { get; set; }
        public Offertes Offerte { get; set; }
        public int ProductId { get; set; }
        public Producten Product { get; set; }
        public int Aantal { get; set; }
    }
}
