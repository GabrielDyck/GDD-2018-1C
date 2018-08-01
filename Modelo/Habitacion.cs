using FrbaHotel.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrbaHotel.Modelo
{
    public class Habitacion
    {
        private int idHabitacion = 0;
        private TipoHabitacion tipoHabitacion = null;
        private Boolean activa = false;
        private int numero = 0;
        private int piso = 0;
        private String ubicacion = "";
        private Hotel hotel=null;
        private String descripcion;

        public Habitacion(int idHabitacion,
            Boolean activa, int numero, int piso, String ubicacion,String descripcion)
        {
            this.idHabitacion = idHabitacion;
            this.activa = activa;
            this.numero = numero;
            this.piso = piso;
            this.ubicacion = ubicacion;
            this.descripcion = descripcion;
        }


        public TipoHabitacion getTipoHabitacion()
        {
            if (this.tipoHabitacion == null)
            {
                RepositorioHabitacion repoHabitacion = new RepositorioHabitacion();
                this.tipoHabitacion = repoHabitacion.getTipoHabitacionByIdHabitacion(this.idHabitacion);
            }
            return tipoHabitacion;
        }

        public Boolean getActiva()
        {
            return activa;
        }

        public int getNumero()
        {
            return numero;
        }

        public int getPiso()
        {
            return piso;
        }

        public String getUbicacion()
        {
            return ubicacion;
        }

        public Hotel getHotel()
        {
            if (this.hotel == null)
            {
                RepositorioHabitacion repoHabitacion = new RepositorioHabitacion();
                this.hotel = repoHabitacion.getHotelByIdHabitacion(this.idHabitacion);
            }
            return this.hotel;
        }

        public int getIdHabitacion()
        {
            return this.idHabitacion;
        }

        public void setIdHabitacion(int idHabitacion)
        {
            this.idHabitacion = idHabitacion;
        }
        public void setHotel(Hotel hotel)
        {
            this.hotel = hotel;
        }

        public void setActiva(bool activa)
        {
            this.activa = activa;
        }


        public void setTipoHabitacion(TipoHabitacion tipo)
        {
            this.tipoHabitacion = tipo;
        }

        public String getDescripcion(){
            return this.descripcion;
        }

        //Estos metodos extra los necesito para popular los combo box y data grid view
        public int NumeroHabitacion { get { return this.getNumero(); } }
        public int Piso { get { return this.getPiso(); } }
        public String Ubicacion { get { return this.getUbicacion(); } }
        public String TipoHabitacion { get { return this.getTipoHabitacion().getDescripcion(); } }
        public Boolean Activa { get { return this.getActiva(); } }
        public String Descripcion { get { return this.getDescripcion(); } }
        //public String Hotel { get { return this.getHotel().getNombre(); } }
    }
}
