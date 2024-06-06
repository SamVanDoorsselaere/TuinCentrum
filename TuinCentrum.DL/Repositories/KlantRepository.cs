using System.Collections.Generic;
using TuinCentrum.BL.Model;
using System.Data.SqlClient;
using TuinCentrum.DL.Exceptions;
using TuinCentrum.BL.Interfaces;

public class KlantRepository : IKlantRepository
{
    private string connectionString;

    public KlantRepository(string connectionString)
    {
        this.connectionString = connectionString;
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
