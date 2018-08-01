using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrbaHotel.Excepciones
{
    public class ElementoYaExisteException : Exception
    {
        public ElementoYaExisteException()
        {
        }

        public ElementoYaExisteException(String message) : base(message)
        { 
        }
    }
}
