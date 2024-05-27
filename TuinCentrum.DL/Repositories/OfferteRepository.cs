using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TuinCentrum.BL.Model;

public class OfferteRepository
{
    private readonly DatabaseContext _context;

    public OfferteRepository(DatabaseContext context)
    {
        _context = context;
    }

    public Offertes GeefOfferteOpId(int id)
    {
        Offertes offerte = null;
        var commando = _context.MaakCommando();
        commando.CommandText = @"
            SELECT o.*, k.Id as KlantId, k.Naam, k.Adres 
            FROM Offertes o
            JOIN Klanten k ON o.KlantId = k.Id
            WHERE o.Id = @Id";
        commando.Parameters.AddWithValue("@Id", id);

        using (var reader = commando.ExecuteReader())
        {
            if (reader.Read())
            {
                var klant = new Klanten(
                    reader.GetInt32(reader.GetOrdinal("KlantId")),
                    reader.GetString(reader.GetOrdinal("Naam")),
                    reader.GetString(reader.GetOrdinal("Adres"))
                );

                offerte = new Offertes(
                    reader.GetInt32(reader.GetOrdinal("Id")),
                    reader.GetDateTime(reader.GetOrdinal("Datum")),
                    klant,
                    !reader.GetBoolean(reader.GetOrdinal("Afhalen")),  // Omgekeerde waarde zoals aangegeven
                    reader.GetBoolean(reader.GetOrdinal("Aanleg")),
                    0 // AantalProducten moet apart worden behandeld
                );
            }
        }

        if (offerte != null)
        {
            commando = _context.MaakCommando();
            commando.CommandText = "SELECT * FROM OfferteProducten WHERE OfferteID = @OfferteID";
            commando.Parameters.AddWithValue("@OfferteID", id);

            using (var reader = commando.ExecuteReader())
            {
                while (reader.Read())
                {
                    var product = new Producten(
                        reader.GetString(reader.GetOrdinal("NederlandseNaam")),
                        reader.GetString(reader.GetOrdinal("WetenschappelijkeNaam")),
                        reader.GetString(reader.GetOrdinal("Beschrijving")),
                        reader.GetDouble(reader.GetOrdinal("Prijs"))
                    )
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("ProductId"))
                    };

                    offerte.VoegProductToe(product);
                    offerte.AantalProducten = reader.GetInt32(reader.GetOrdinal("Aantal"));
                }
            }
        }

        return offerte;
    }

    // Aanvullende methoden voor offerte CRUD-operaties
}
