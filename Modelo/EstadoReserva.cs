using FrbaHotel.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrbaHotel.Modelo
{
    public class EstadoReserva
    {
        private int idEstadoReserva = 0;
        private Usuario usuario = null;
        private Reserva reserva = null;
        private String tipoEstado = "";
        private DateTime fecha = Utils.getSystemDatetimeNow();
        private String descripcion = "";

        public EstadoReserva(int idEstadoReserva, Usuario usuario, Reserva reserva, String tipoEstado, DateTime fecha, String descripcion)
        {
            this.idEstadoReserva = idEstadoReserva;
            this.usuario = usuario;
            this.reserva = reserva;
            this.tipoEstado = tipoEstado;
            this.fecha = fecha;
            this.descripcion = descripcion;
        }

        public int getIdEstadoReserva()
        {
            return idEstadoReserva;
        }

        public Usuario getUsuario()
        {
            return usuario;
        }

        public Reserva getReserva()
        {
            return reserva;
        }

        public String getTipoEstado()
        {
            return tipoEstado;
        }

        public DateTime getFecha()
        {
            return fecha;
        }

        public String getDescripcion()
        {
            return descripcion;
        }

        public Boolean esNuevo()
        {
            return idEstadoReserva.Equals(0);
        }

        //Estos metodos extra los necesito para popular los combo box y data grid view
        //public int IdEstadoReserva { get { return this.getIdEstadoReserva(); } }
        public Usuario Usuario { get { return this.getUsuario(); } }
        public Reserva Reserva { get { return this.getReserva(); } }
        public String TipoEstado { get { return this.getTipoEstado(); } }
        public DateTime Fecha { get { return this.getFecha(); } }
        public String Descripcion { get { return this.getDescripcion(); } }
    }
}
