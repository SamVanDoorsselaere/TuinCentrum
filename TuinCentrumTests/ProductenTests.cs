using System;
using TuinCentrum.BL.Exceptions;
using TuinCentrum.BL.Model;
using Xunit;

namespace TuinCentrum.Tests
{
    public class ProductenTests
    {
        [Fact]
        public void Constructor_ValidParameters_ShouldCreateProduct()
        {
            string nederlandseNaam = "Roos";
            string wetenschappelijkeNaam = "Rosa";
            string beschrijving = "Mooie rode bloem";
            double prijs = 10.5;

            var product = new Producten(nederlandseNaam, wetenschappelijkeNaam, beschrijving, prijs);

            Assert.Equal(nederlandseNaam, product.NederlandseNaam);
            Assert.Equal(wetenschappelijkeNaam, product.WetenschappelijkeNaam);
            Assert.Equal(beschrijving, product.Beschrijving);
            Assert.Equal(prijs, product.Prijs);
        }

        [Fact]
        public void NederlandseNaam_SetToNull_ShouldThrowDomeinException()
        {
            var product = new Producten("Roos", "Rosa", "Mooie rode bloem", 10.5);

            var exception = Assert.Throws<DomeinException>(() => product.NederlandseNaam = null);
            Assert.Equal("Nederlandse naam is verplicht.", exception.Message);
        }

        [Fact]
        public void WetenschappelijkeNaam_SetToNull_ShouldThrowDomeinException()
        {
            var product = new Producten("Roos", "Rosa", "Mooie rode bloem", 10.5);

            var exception = Assert.Throws<DomeinException>(() => product.WetenschappelijkeNaam = null);
            Assert.Equal("Wetenschappelijke naam is verplicht.", exception.Message);
        }

        [Fact]
        public void Beschrijving_SetToNull_ShouldThrowDomeinException()
        {
            var product = new Producten("Roos", "Rosa", "Mooie rode bloem", 10.5);

            var exception = Assert.Throws<DomeinException>(() => product.Beschrijving = null);
            Assert.Equal("Beschrijving is verplicht.", exception.Message);
        }

        [Fact]
        public void Prijs_SetToZero_ShouldThrowDomeinException()
        {
            var product = new Producten("Roos", "Rosa", "Mooie rode bloem", 10.5);

            var exception = Assert.Throws<DomeinException>(() => product.Prijs = 0);
            Assert.Equal("Prijs moet groter zijn dan 0.", exception.Message);
        }

        [Fact]
        public void Prijs_SetToNegative_ShouldThrowDomeinException()
        {
            var product = new Producten("Roos", "Rosa", "Mooie rode bloem", 10.5);

            var exception = Assert.Throws<DomeinException>(() => product.Prijs = -1);
            Assert.Equal("Prijs moet groter zijn dan 0.", exception.Message);
        }
    }
}
