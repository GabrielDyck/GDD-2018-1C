using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrbaHotel.Excepciones
{
    public class NoExisteIDException : Exception
    {
        public NoExisteIDException()
        {
        }

        public NoExisteIDException(String message) : base(message)
        { 
        }
    }
}
