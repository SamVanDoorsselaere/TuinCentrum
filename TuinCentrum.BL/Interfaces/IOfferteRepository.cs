using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuinCentrum.BL.Model;

namespace TuinCentrum.BL.Interfaces
{
    public interface IOfferteRepository
    {
        bool HeeftOfferte(Offertes offerte);
        void SchrijfOfferte(Offertes offerte);
        Offertes GeefOfferteOpKlantID(int klantId);
    }
}
