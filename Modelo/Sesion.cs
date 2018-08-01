using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrbaHotel.Modelo
{
    public class Sesion
    {
        private Usuario user = null;
        private Rol rol = null;
        private Hotel hotel = null;

        public Sesion(Usuario user, Hotel hotel, Rol rol)
        {
            this.user = user;
            this.hotel = hotel;
            this.rol = rol;
        }

        public Usuario getUsuario()
        {
            return this.user;
        }

        public Hotel getHotel()
        {
            return this.hotel;
        }

        public Rol getRol()
        {
            return this.rol;
        }
    }
}
