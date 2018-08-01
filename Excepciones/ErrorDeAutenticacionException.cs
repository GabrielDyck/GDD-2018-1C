using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrbaHotel.Excepciones
{
    public class ErrorDeAutenticacionException : Exception
    {
        public ErrorDeAutenticacionException()
        {
        }

        public ErrorDeAutenticacionException(String message) : base(message)
        { 
        }
    }
}
