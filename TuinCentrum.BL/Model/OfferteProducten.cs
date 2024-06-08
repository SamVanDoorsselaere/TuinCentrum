namespace TuinCentrum.BL.Model
{
    public class OfferteProduct
    {
        public int OfferteId { get; set; }
        public int ProductId { get; set; }
        public int Aantal { get; set; }


        public OfferteProduct(int offerteId, int productId, int aantal)
        {
            OfferteId = offerteId;
            ProductId = productId;
            Aantal = aantal;
        }
    }
}
