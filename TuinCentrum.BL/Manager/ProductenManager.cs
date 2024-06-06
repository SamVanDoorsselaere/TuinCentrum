using System;
using System.Collections.Generic;
using TuinCentrum.BL.Exceptions;
using TuinCentrum.BL.Interfaces;
using TuinCentrum.BL.Model;

namespace TuinCentrum.BL.Manager
{
    public class ProductenManager
    {
        private IFileProcessor fileProcessor;
        private IProductRepository productRepository;

        public ProductenManager(IFileProcessor fileProcessor, IProductRepository productRepository)
        {
            this.fileProcessor = fileProcessor;
            this.productRepository = productRepository;
        }

        public void UploadProducten(string fileName)
        {
            List<string> soorten = fileProcessor.LeesProducten(fileName);
            List<Producten> producten = MaakProducten(soorten);
            foreach (Producten product in producten)
            {
                if (!productRepository.HeeftProduct(product))
                    productRepository.SchrijfProduct(product);
            }
        }

        private List<Producten> MaakProducten(List<string> producten)
        {
            List<Producten> productList = new List<Producten>();
            foreach (string productString in producten)
            {
                string[] productData = productString.Split('|');
                if (productData.Length == 5) // Controleer of er 5 delen zijn gescheiden door '|'
                {
                    try
                    {
                        string productId = productData[0]; // ID
                        string productNederlandsNaam = productData[1]; // Nederlandse naam
                        string productWettenschappelijkeNaam = productData[2]; // Wetenschappelijke naam
                        string productPrijsString = productData[3]; // Prijs als string
                        string productBeschrijving = productData[4]; // Beschrijving

                        if (string.IsNullOrWhiteSpace(productNederlandsNaam))
                            throw new DomeinException($"Nederlandse naam is verplicht voor product: {productString}");

                        if (string.IsNullOrWhiteSpace(productWettenschappelijkeNaam))
                            throw new DomeinException($"Wetenschappelijke naam is verplicht voor product: {productString}");

                        if (string.IsNullOrWhiteSpace(productBeschrijving))
                            throw new DomeinException($"Beschrijving is verplicht voor product: {productString}");

                        if (!double.TryParse(productPrijsString, out double productPrijs))
                            throw new DomeinException($"Ongeldige prijsgegevens: {productPrijsString} voor product: {productString}");

                        Producten product = new Producten(productNederlandsNaam, productWettenschappelijkeNaam, productBeschrijving, productPrijs)
                        {
                            Id = string.IsNullOrWhiteSpace(productId) ? (int?)null : int.Parse(productId)
                        };
                        productList.Add(product);
                    }
                    catch (DomeinException ex)
                    {
                        // Log de fout en ga verder met het volgende product
                        Console.WriteLine($"Fout bij het verwerken van product: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine($"Ongeldige productgegevens: {productString}");
                }
            }
            return productList;
        }
    }
}
