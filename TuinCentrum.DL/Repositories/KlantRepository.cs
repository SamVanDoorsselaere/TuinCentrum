using System.Collections.Generic;
using TuinCentrum.BL.Model;
using System.Data.SqlClient;

public class KlantRepository
{
    private readonly DatabaseContext _context;

    public KlantRepository(DatabaseContext context)
    {
        _context = context;
    }

    public List<Klanten> GeefAlleKlanten()
    {
        var klanten = new List<Klanten>();
        var commando = _context.MaakCommando();
        commando.CommandText = "SELECT * FROM Klanten";

        using (var reader = commando.ExecuteReader())
        {
            while (reader.Read())
            {
                klanten.Add(new Klanten(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetString(2)
                ));
            }
        }

        return klanten;
    }
}
