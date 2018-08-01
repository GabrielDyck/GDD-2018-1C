using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrbaHotel.Excepciones
{
    public class NoExisteNombreException : Exception
    {
        public NoExisteNombreException()
        {
        }

        public NoExisteNombreException(String message) : base(message)
        { 
        }
    }
}
