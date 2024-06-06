using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuinCentrum.BL.Model;

namespace TuinCentrum.BL.Interfaces
{
    public interface IProductRepository
    {
        List<Producten> GeefAlleProducten();
        bool HeeftProduct(Producten product);
        void SchrijfProduct(Producten product);
    }
}
