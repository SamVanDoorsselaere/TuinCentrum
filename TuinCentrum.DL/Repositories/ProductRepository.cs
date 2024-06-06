using System.Collections.Generic;
using TuinCentrum.BL.Model;
using System.Data.SqlClient;
using TuinCentrum.BL.Interfaces;
using System.Collections;
using TuinCentrum.DL.Exceptions;

public class ProductRepository : IProductRepository
{
    private string connectionString;

    public ProductRepository(string connectionString)
    {
        this.connectionString = connectionString;
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
                con.Open(); using (SqlDataReader reader = cmd.ExecuteReader())
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
