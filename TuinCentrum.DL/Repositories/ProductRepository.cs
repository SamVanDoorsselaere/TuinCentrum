using System;
using System.Collections.Generic;
using TuinCentrum.BL.Model;
using Microsoft.Data.SqlClient;
using TuinCentrum.BL.Interfaces;
using TuinCentrum.DL.Exceptions;

public class ProductRepository : IProductRepository
{
    private string connectionString;

    public ProductRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public bool HeeftProduct(Producten product)
    {
        string SQL = "SELECT Count(*) FROM Producten WHERE NederlandseNaam=@NederlandseNaam";
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = conn.CreateCommand())
        {
            try
            {
                conn.Open();
                cmd.CommandText = SQL;
                cmd.Parameters.Add(new SqlParameter("@NederlandseNaam", System.Data.SqlDbType.NVarChar));
                cmd.Parameters["@NederlandseNaam"].Value = product.NederlandseNaam;
                int n = (int)cmd.ExecuteScalar();
                if (n > 0) return true; else return false;
            }
            catch (Exception ex)
            {
                throw new Exception("HeeftProduct", ex);
            }
        }
    }

    public void SchrijfProduct(Producten product)
    {
        string SQL = "INSERT INTO Producten (NederlandseNaam, WetenschappelijkeNaam, Beschrijving, Prijs) VALUES (@NederlandseNaam, @WetenschappelijkeNaam, @Beschrijving, @Prijs)";
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = conn.CreateCommand())
        {
            try
            {
                conn.Open();
                cmd.CommandText = SQL;
                cmd.Parameters.AddWithValue("@NederlandseNaam", product.NederlandseNaam);
                cmd.Parameters.AddWithValue("@WetenschappelijkeNaam", product.WetenschappelijkeNaam);
                cmd.Parameters.AddWithValue("@Beschrijving", product.Beschrijving);
                cmd.Parameters.AddWithValue("@Prijs", product.Prijs);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("SchrijfProduct", ex);
            }
        }
    }

    public List<Producten> GeefAlleProducten()
    {
        var producten = new List<Producten>();
        string query = "SELECT * FROM Producten";

        using (SqlConnection con = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(query, con))
        {
            try
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        producten.Add(new Producten(
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetString(3),
                            reader.GetDouble(4))
                        {
                            Id = reader.GetInt32(0)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DataException("Fout bij het ophalen van producten.", ex);
            }
        }

        return producten;
    }
}
