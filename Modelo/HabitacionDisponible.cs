using FrbaHotel.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrbaHotel.AbmReserva
{
    public class HabitacionDisponible
    {

        private Habitacion habitacion;
        private Regimen regimen;

        public HabitacionDisponible(Habitacion habitacion, Regimen regimen)
        {
            this.habitacion = habitacion;
            this.regimen = regimen;
        }

        public Habitacion getHabitacion()
        {
            return this.habitacion;
        }


        public Regimen getRegimen()
        {
            return this.regimen;
        }

        public int getNumeroHabitacion()
        {
            return this.getHabitacion().getNumero();
        }

        public String Regimen { get { return this.regimen.getDescripcion(); } }
        public decimal PrecioPorNoche
        {
            get
            {
                decimal precioRegimen = regimen.getPrecio();
                decimal precioTipoHabitacion = habitacion.getTipoHabitacion().getPorcentual();
                decimal categoriaPrecio = habitacion.getHotel().getCategoria().getRecargaEstrellas();
                return ((precioRegimen * precioTipoHabitacion) + categoriaPrecio);
            }
        }
        public String Hotel { get { return this.habitacion.getHotel().getNombre(); } }
        public int Numero { get { return this.habitacion.getNumero(); } }

        public String TipoHabitacion { get { return this.habitacion.getTipoHabitacion().getDescripcion(); } }
        public String Ubicacion { get { return this.habitacion.getUbicacion(); } }
        public int Piso { get { return this.habitacion.getPiso(); } }
       
    }
}
