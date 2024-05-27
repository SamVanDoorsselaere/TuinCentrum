using System.Collections.Generic;
using TuinCentrum.BL.Model;
using System.Data.SqlClient;

public class ProductRepository
{
    private readonly DatabaseContext _context;

    public ProductRepository(DatabaseContext context)
    {
        _context = context;
    }

    public List<Producten> GeefAlleProducten()
    {
        var producten = new List<Producten>();
        var commando = _context.MaakCommando();
        commando.CommandText = "SELECT * FROM Producten";

        using (var reader = commando.ExecuteReader())
        {
            while (reader.Read())
            {
                producten.Add(new Producten(
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetDouble(4)
                )
                {
                    Id = reader.GetInt32(0)
                });
            }
        }

        return producten;
    }
}
