using TuinCentrum.BL.Interfaces;
using TuinCentrum.BL.Model;
using Microsoft.Data.SqlClient;

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
        using (SqlCommand cmd = conn.CreateCommand())
        {
            try
            {
                conn.Open();
                cmd.CommandText = SQL;
                // Hier geen waarde toewijzen aan OfferteID (identiteitskolom), de database zal deze automatisch genereren
                cmd.Parameters.AddWithValue("@datum", offerte.Datum);
                cmd.Parameters.AddWithValue("@klantid", offerte.KlantID);
                cmd.Parameters.AddWithValue("@afhalen", offerte.Afhalen);
                cmd.Parameters.AddWithValue("@aanleg", offerte.Aanleg);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("SchrijfOfferte", ex);
            }
        }
    }


}
