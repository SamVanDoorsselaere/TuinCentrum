using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TuinCentrum.BL.Interfaces;
using TuinCentrum.BL.Model;
using TuinCentrum.DL.Exceptions;

public class OfferteRepository : IOfferteRepository
{
    private string connectionString;

    public OfferteRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public Offertes GeefOfferteOpId(int id)
    {
        Offertes offerte = null;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = @"
                SELECT 
                    o.Id,
                    o.Datum,
                    k.Id AS KlantId,
                    k.Naam AS KlantNaam,
                    k.Adres AS KlantAdres,
                    o.Afhalen,
                    o.Aanleg,
                    op.ProductId AS ProductId,
                    p.NederlandseNaam,
                    p.WetenschappelijkeNaam,
                    p.Beschrijving,
                    p.Prijs,
                    op.Aantal
                FROM Offertes o
                INNER JOIN Klanten k ON o.KlantId = k.Id
                LEFT JOIN OfferteProducten op ON o.Id = op.OfferteID
                LEFT JOIN Producten p ON op.ProductId = p.Id
                WHERE o.Id = @Id";

            using (SqlCommand command = new SqlCommand(query, con))
            {
                command.Parameters.AddWithValue("@Id", id);

                try
                {
                    con.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (offerte == null)
                            {
                                Klanten klant = new Klanten(
                                    reader.GetInt32(reader.GetOrdinal("KlantId")),
                                    reader.GetString(reader.GetOrdinal("KlantNaam")),
                                    reader.GetString(reader.GetOrdinal("KlantAdres"))
                                );

                                offerte = new Offertes(
                                    reader.GetInt32(reader.GetOrdinal("Id")),
                                    reader.GetDateTime(reader.GetOrdinal("Datum")),
                                    klant,
                                    !reader.GetBoolean(reader.GetOrdinal("Afhalen")),
                                    reader.GetBoolean(reader.GetOrdinal("Aanleg")),
                                    0 // AantalProducten moet apart worden behandeld
                                );
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("ProductId")))
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

                                // VoegProductToe-methode aanroepen met beide argumenten
                                offerte.VoegProductToe(product, reader.GetInt32(reader.GetOrdinal("Aantal")));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new DataException("Fout bij het ophalen van offerte.", ex);
                }
            }
        }

        return offerte;
    }

    // Aanvullende methoden voor offerte CRUD-operaties
}
