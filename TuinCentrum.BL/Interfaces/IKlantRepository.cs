using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuinCentrum.BL.Model;

namespace TuinCentrum.BL.Interfaces
{
    public interface IKlantRepository
    {
        List<Klanten> GeefAlleKlanten();
        bool HeeftKlant(Klanten klant);
        void SchrijfKlant(Klanten klant);
        List<Klanten> ZoekKlantenOpNaam(string naam);
    }
}
