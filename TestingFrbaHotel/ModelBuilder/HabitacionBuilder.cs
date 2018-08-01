using FrbaHotel.Modelo;
using FrbaHotel.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingFrbaHotel.ModelBuilder
{
    public class HabitacionBuilder
    {
        public static Habitacion buildHabitacion(TipoHabitacion tipoHabitacion, int idHotel)
        {
            RepositorioHotel repositorioHotel = new RepositorioHotel();
            Hotel hotel = HotelBuilder.buildHotel();
            int id = repositorioHotel.create(hotel);
            hotel.setIdHotel(id);
            Habitacion hab = new Habitacion(0, true, 1, 1, "TEST: ZONA 1", "Desc");
            hab.setHotel(hotel);
            return hab;
        }
    }
}
