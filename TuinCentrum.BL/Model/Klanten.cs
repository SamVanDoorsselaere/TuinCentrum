using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuinCentrum.BL.Model
{
    public class Klanten
    {
        public int? Id { get; set; }
        public string Naam { get; set; }
        public string Adres { get; set; }

        
        public Klanten(int? id, string naam, string adres)
        {
            Id = id;
            Naam = naam;
            Adres = adres;
        }

    }
}
