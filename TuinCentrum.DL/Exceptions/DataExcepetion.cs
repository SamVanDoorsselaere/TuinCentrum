﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuinCentrum.DL.Exceptions
{
    public class DataExcepetion : Exception
    {
        public DataExcepetion(string? message) : base(message)
        {
        }

        public DataExcepetion(string? message, Exception? innerException) : base(message, innerException)
        {
        }

    }
}
