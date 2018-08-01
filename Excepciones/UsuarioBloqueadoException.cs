using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrbaHotel.Excepciones
{
    public class UsuarioBloqueadoException : Exception
    {
        public UsuarioBloqueadoException()
        {
        }

        public UsuarioBloqueadoException(String message) : base(message)
        { 
        }
    }
}
