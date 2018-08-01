using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrbaHotel.Excepciones
{
    class RequestInvalidoException : Exception
    {
        public RequestInvalidoException(String message) : base(message)
        {
        }
    }
}
