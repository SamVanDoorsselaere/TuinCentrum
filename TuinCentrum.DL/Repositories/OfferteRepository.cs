using TuinCentrum.BL.Interfaces;
using TuinCentrum.BL.Model;
using Microsoft.Data.SqlClient;
using TuinCentrum.DL.Exceptions;

public class OfferteRepository : IOfferteRepository
{
    private string connectionString;

    public OfferteRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public bool HeeftOfferte(Offertes offerte)
    {
        string SQL = "SELECT Count(*) FROM Offertes WHERE Datum=@datum"; // Gebruik de juiste kolomnaam (Datum)
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = conn.CreateCommand())
        {
            try
            {
                conn.Open();
                cmd.CommandText = SQL;
                cmd.Parameters.Add(new SqlParameter("@datum", System.Data.SqlDbType.DateTime)); // Gebruik SqlDbType.DateTime
                cmd.Parameters["@datum"].Value = offerte.Datum;
                int n = (int)cmd.ExecuteScalar();
                return n > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("HeeftOfferte", ex); // Update de foutmelding
            }
        }
    }

    public void SchrijfOfferte(Offertes offerte)
    {
        string SQL = "INSERT INTO Offertes (Datum, KlantID, Afhalen, Aanleg) VALUES (@datum, @klantid, @afhalen, @aanleg)";

        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(SQL, conn))
        {
            try
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@datum", offerte.Datum);
                cmd.Parameters.AddWithValue("@klantid", offerte.KlantID);
                cmd.Parameters.AddWithValue("@afhalen", offerte.Afhalen);
                cmd.Parameters.AddWithValue("@aanleg", offerte.Aanleg);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Insert query executed");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred: " + ex.Message);
                throw new Exception("SchrijfOfferte", ex);
            }
        }
    }


    public Offertes GeefOfferte(int offerteId)
    {
        string query = "SELECT * FROM Offertes WHERE OfferteID = @offerteId";
        using (SqlConnection con = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(query, con))
        {
            try
            {
                con.Open();
                cmd.Parameters.AddWithValue("@offerteId", offerteId);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int offerteID = reader.GetInt32(reader.GetOrdinal("OfferteID"));
                        DateTime datum = reader.GetDateTime(reader.GetOrdinal("Datum"));
                        int klantID = reader.GetInt32(reader.GetOrdinal("KlantID"));
                        bool afhalen = reader.GetBoolean(reader.GetOrdinal("Afhalen"));
                        bool aanleg = reader.GetBoolean(reader.GetOrdinal("Aanleg"));

                        return new Offertes(offerteID, datum, klantID, afhalen, aanleg);
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new DataException("Fout bij het ophalen van offerte (SQL-fout).", sqlEx);
            }
            catch (Exception ex)
            {
                throw new DataException("Fout bij het ophalen van offerte (algemene fout).", ex);
            }
        }
        return null;
    }





}
