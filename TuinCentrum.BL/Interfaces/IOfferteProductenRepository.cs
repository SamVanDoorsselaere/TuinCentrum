using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuinCentrum.BL.Model;

namespace TuinCentrum.BL.Interfaces
{
    public interface IOfferteProductenRepository
    {
        bool HeeftOfferteProducten(OfferteProduct offerteProduct);
        void SchrijfOfferteProducten(OfferteProduct offerteProduct);
    }
}
