using FrbaHotel.AbmReserva;
using FrbaHotel.Commons;
using FrbaHotel.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrbaHotel.Modelo
{
    public class Reserva
    {
        private List<Habitacion> habitaciones;
        private Usuario usuarioGenerador;
        private int idReserva = 0;
        private Hotel hotel = null;
        private Estadia estadia = null;
        private Regimen regimen = null;
        private Cliente cliente = null;
        private decimal codigoReserva = 0;
        private decimal diasAlojados = 0;
        private DateTime fechaCreacion = Utils.getSystemDatetimeNow();
        private DateTime fechaDesde = Utils.getSystemDatetimeNow();
        private DateTime fechaHasta = Utils.getSystemDatetimeNow();
        private EstadoReserva estado = null;

        public Reserva(int idReserva, Hotel hotel, Estadia estadia, Regimen regimen, Cliente cliente, decimal codigoReserva,
            decimal diasAlojados, DateTime fechaCreacion, DateTime fechaDesde, DateTime fechaHasta, EstadoReserva estado)
        {
            this.idReserva = idReserva;
            this.hotel = hotel;
            this.estadia = estadia;
            this.regimen = regimen;
            this.cliente = cliente;
            this.codigoReserva = codigoReserva;
            this.diasAlojados = diasAlojados;
            this.fechaCreacion = fechaCreacion;
            this.fechaDesde = fechaDesde;
            this.fechaHasta = fechaHasta;
            this.estado = estado;
        }

        public Reserva(List<Habitacion> habitacionesParaReservar,Regimen regimen, Cliente clienteDueñoDeLaReserva, DateTime fechaInicio, DateTime fechaFin, int diasAlojados,Usuario usuario)
        {
            Habitacion habitacionDeUnHotel = habitacionesParaReservar[0];
            this.habitaciones = habitacionesParaReservar;
            this.hotel = habitacionDeUnHotel.getHotel();
            this.regimen = regimen;
            this.cliente = clienteDueñoDeLaReserva;
            this.fechaCreacion = Utils.getSystemDatetimeNow();
            this.fechaDesde = fechaInicio;
            this.fechaHasta = fechaFin;
            this.diasAlojados = diasAlojados;
            this.usuarioGenerador = usuario;
        }

        public int getIdReserva()
        {
            return this.idReserva;
        }

        public Hotel getHotel()
        {
            if (this.hotel == null) {
                RepositorioReserva repoReserva = new RepositorioReserva();
                this.hotel = repoReserva.getHotelByIdReserva(this);
            }
            return this.hotel;
        }

        public Estadia getEstadia()
        {
            return this.estadia;
        }

        public Regimen getRegimen()
        {
            if (this.regimen == null) {
                RepositorioReserva repoReserva = new RepositorioReserva();
                this.regimen=repoReserva.getRegimenByIdReserva(this);
            }

            return this.regimen;
        }

        public Cliente getCliente()
        {
            if (this.cliente == null) {
                RepositorioReserva repoReserva = new RepositorioReserva();
                this.cliente = repoReserva.getClienteByIdReserva(this);
            }
            return this.cliente;
        }

        public decimal getCodigoReserva()
        {
            return this.codigoReserva;
        }

        public decimal getDiasAlojados()
        {
            return this.diasAlojados;
        }

        public DateTime getFechaCreacion()
        {
            return this.fechaCreacion;
        }

        public DateTime getFechaDesde()
        {
            return this.fechaDesde;
        }

        public DateTime getFechaHasta()
        {
            return this.fechaHasta;
        }

        public EstadoReserva getEstadoReserva()
        {

            if (this.estado == null) {
                RepositorioEstadoReserva repoEstados = new RepositorioEstadoReserva();
                this.estado = repoEstados.getByIdReserva(this.idReserva);
            }
            return this.estado;
        }

        public Boolean esNuevo()
        {
            return idReserva.Equals(0);
        }

        public List<Habitacion> getHabitaciones() {

            if (this.habitaciones == null) {
                RepositorioHabitacion repoHab = new RepositorioHabitacion();
                this.habitaciones = repoHab.getHabitacionesByReservaId(this);
            }
            return this.habitaciones;
        }

        public void setIdReserva(int idReserva) {
            this.idReserva = idReserva;
        }

        public void setCodigoReserva(decimal codigoReserva)
        {
            this.codigoReserva = codigoReserva;
        }

        public void setFechaDesde(DateTime fechaDesde)
        {
            this.fechaDesde = fechaDesde;
        }

        public void setFechaHasta(DateTime fechaHasta)
        {
            this.fechaHasta = fechaHasta;
        }

        public void setHotel(Hotel hotel)
        {
            this.hotel = hotel;
        }

        public void setHabitaciones(List<Habitacion> habitaciones) {
            this.habitaciones = habitaciones;
        }

        public void setRegimen(Regimen regimen)
        {
            this.regimen = regimen;
        }

        public void setDiasAlojados(int diasAlojados)
        {
            this.diasAlojados = diasAlojados;
        }


        public void setUsuarioGenerador(Usuario usuarioGenerador)
        {
            this.usuarioGenerador = usuarioGenerador;
        }

        public Usuario getUsuarioGenerador()
        {
            if(this.usuarioGenerador ==null){
                RepositorioEstadoReserva repoEstado = new RepositorioEstadoReserva();
                this.usuarioGenerador=repoEstado.getUsuarioByIdReservaAndTipoEstado(idReserva, "RC");
            }
            return this.usuarioGenerador;
        }
        //Estos metodos extra los necesito para popular los combo box y data grid view
        public decimal CodigoReserva { get { return this.getCodigoReserva(); } }
        public String Regimen { get { return this.getRegimen().getDescripcion(); } }
        public String FechaCreacion { get { return this.getFechaCreacion().ToString(); } }
        public String FechaDesde { get { return this.getFechaDesde().ToString(); } }
        public String FechaHasta { get { return this.getFechaHasta().ToString(); } }
        public String Cliente { get { return this.getCliente().getIdentidad().getNombreCompleto(); } }       

    }
}
