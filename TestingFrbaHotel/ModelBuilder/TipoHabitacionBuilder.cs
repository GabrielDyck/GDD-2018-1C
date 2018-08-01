using FrbaHotel.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingFrbaHotel.ModelBuilder
{
    public class TipoHabitacionBuilder
    {

        public static TipoHabitacion build() {
            return new TipoHabitacion(0, "TEST-1", 10, "TIPO HABITACION BUILDER");
        }
    }
}
