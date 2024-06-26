using System;
using Xunit;
using TuinCentrum.BL.Model;
using TuinCentrum.BL.Exceptions;

namespace TuinCentrumTests
{
    public class OffertesTests
    {
        [Fact]
        public void Test_BerekenTotalePrijs_ZonderKorting_EnZonderAfhalen()
        {
            var klant = new Klanten(id: 1, naam: "Test Klant", adres: "Test Adres");
            var offerte = new Offertes(1, DateTime.Now, klantID: 10, afhalen: true, aanleg: false, aantalProducten: 3);
            var product1 = new Producten("Product 1", "Wetenschappelijke naam 1", "Beschrijving 1", 100);
            var product2 = new Producten("Product 2", "Wetenschappelijke naam 2", "Beschrijving 2", 150);
            offerte.VoegProductToe(product2, 5);

            var totalePrijs = offerte.CalculateTotalPrice();

            Assert.Equal(3 * (100 + 150), totalePrijs);
        }


        [Fact]
        public void Test_BerekenTotalePrijs_MetKorting()
        {
            var klant = new Klanten(id: 1, naam: "Test Klant", adres: "Test Adres");
            var offerte = new Offertes(1, DateTime.Now, klantID: 15, afhalen: true, aanleg: false, aantalProducten: 3);
            var product = new Producten("Product", "Wetenschappelijke naam", "Beschrijving", 5000);
            offerte.ProductenList.Add(product, 1);

            var totalePrijs = offerte.CalculateTotalPrice();

            Assert.Equal(0.95 * 5000, totalePrijs); // 5% korting toegepast
        }

        [Fact]
        public void Test_BerekenTotalePrijs_MetLeveringskost()
        {
            // Arrange
            var klant = new Klanten(id: 1, naam: "Test Klant", adres: "Test Adres");
            var offerte = new Offertes(1, DateTime.Now, klantID: 25, afhalen: true, aanleg: false, aantalProducten: 3);
            var product = new Producten("Product", "Wetenschappelijke naam", "Beschrijving", 500);
            offerte.ProductenList.Add(product, 1);

            // Act
            var totalePrijs = offerte.CalculateTotalPrice();

            // Assert
            Assert.Equal(400 + 100, totalePrijs); // Leveringskost van 100 toegevoegd
        }
    }
}
