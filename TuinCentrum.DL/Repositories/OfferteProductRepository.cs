using System;
using Microsoft.Data.SqlClient;

namespace TuinCentrum.DL.Repositories
{
    public class OfferteProductRepository
    {
        private string connectionString;

        public OfferteProductRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void SchrijfOfferteProduct(int offerteID, int productID, int aantal)
        {
            string SQL = "INSERT INTO OfferteProducten (OfferteID, ProductID, Aantal) VALUES (@offerteID, @productID, @aantal)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(SQL, conn))
            {
                try
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@offerteID", offerteID);
                    cmd.Parameters.AddWithValue("@productID", productID);
                    cmd.Parameters.AddWithValue("@aantal", aantal);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Fout bij het schrijven van offerteproduct.", ex);
                }
            }
        }
    }
}
