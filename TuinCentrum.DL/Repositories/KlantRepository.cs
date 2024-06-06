using System.Collections.Generic;
using TuinCentrum.BL.Model;
using Microsoft.Data.SqlClient;
using TuinCentrum.DL.Exceptions;
using TuinCentrum.BL.Interfaces;

public class KlantRepository : IKlantRepository
{
    private string connectionString;

    public KlantRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public bool HeeftKlant(Klanten klant)
    {
        string SQL = "SELECT Count(*) FROM Klanten WHERE naam=@naam"; // Update de tabelnaam naar Klanten
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = conn.CreateCommand())
        {
            try
            {
                conn.Open();
                cmd.CommandText = SQL;
                cmd.Parameters.Add(new SqlParameter("@naam", System.Data.SqlDbType.NVarChar));
                cmd.Parameters["@naam"].Value = klant.Naam;
                int n = (int)cmd.ExecuteScalar();
                if (n > 0) return true; else return false;
            }
            catch (Exception ex)
            {
                throw new Exception("HeeftKlant", ex);
            }
        }
    }


    public void SchrijfKlant(Klanten klant)
    {
        string SQL = "INSERT INTO Klanten (naam, adres) VALUES (@naam, @adres)";
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = conn.CreateCommand())
        {
            try
            {
                conn.Open();
                cmd.CommandText = SQL;
                cmd.Parameters.AddWithValue("@naam", klant.Naam);

                // Controleer of klant.Adres niet null is voordat je de parameter toevoegt
                if (!string.IsNullOrEmpty(klant.Adres))
                {
                    cmd.Parameters.AddWithValue("@adres", klant.Adres);
                }
                else
                {
                    // Als klant.Adres null is, voeg een lege string toe aan de parameter
                    cmd.Parameters.AddWithValue("@adres", "");
                }

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("SchrijfKlant", ex);
            }
        }
    }



    public List<Klanten> GeefAlleKlanten()
    {
        List<Klanten> klanten = new List<Klanten>();

        string query = "SELECT * FROM Klanten";
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
                        Klanten klant = new Klanten(
                            reader.GetInt32(reader.GetOrdinal("Id")),
                            reader.GetString(reader.GetOrdinal("Naam")),
                            reader.GetString(reader.GetOrdinal("Adres"))
                        );
                        klanten.Add(klant);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DataException("Fout bij het ophalen van klanten.", ex);
            }
        }
        return klanten;
    }

}
