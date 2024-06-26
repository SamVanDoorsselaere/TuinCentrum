﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuinCentrum.BL.Interfaces
{
    public interface IFileProcessor
    {
        List<string> LeesKlanten(string fileName);
        List<string> LeesProducten(string fileName);
        List<string> LeesOffertes(string fileName);
        List<string> LeesOfferteProducten(string fileName);
    }
}
