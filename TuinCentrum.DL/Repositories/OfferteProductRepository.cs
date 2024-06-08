using System;
using Microsoft.Data.SqlClient;
using TuinCentrum.BL.Interfaces;
using TuinCentrum.BL.Model;

namespace TuinCentrum.DL.Repositories
{
    public class OfferteProductRepository : IOfferteProductenRepository
    {
        private string connectionString;

        public OfferteProductRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }


        public bool HeeftOfferteProducten(OfferteProduct offerteProduct)
        {
            string SQL = "SELECT Count(*) FROM OfferteProducten WHERE OfferteID=@offerteId AND ProductID=@productId AND Aantal=@aantal";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(SQL, conn))
            {
                try
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@offerteId", offerteProduct.OfferteId);
                    cmd.Parameters.AddWithValue("@productId", offerteProduct.ProductId);
                    cmd.Parameters.AddWithValue("@aantal", offerteProduct.Aantal);
                    int n = (int)cmd.ExecuteScalar();
                    return n > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("HeeftOfferteProducten", ex);
                }
            }
        }

        public void SchrijfOfferteProducten(OfferteProduct offerteProduct)
        {
            string SQL = "INSERT INTO OfferteProducten (OfferteID, ProductID, Aantal) VALUES (@offerteId, @productId, @aantal)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(SQL, conn))
            {
                try
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@offerteId", offerteProduct.OfferteId);
                    cmd.Parameters.AddWithValue("@productId", offerteProduct.ProductId);
                    cmd.Parameters.AddWithValue("@aantal", offerteProduct.Aantal);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Fout bij het schrijven van offerte product.", ex);
                }
            }
        }


    }
}
