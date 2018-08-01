using FrbaHotel.Commons;
using FrbaHotel.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrbaHotel.Modelo
{
    public class Hotel
    {
        private int idHotel = 0;
        private Categoria categoria = null;
        private Direccion direccion = null;
        private String nombre = "";
        private String mail = "";
        private String telefono = "";
        private DateTime fechaInicioActividades = Utils.getSystemDatetimeNow();
        private List<Reserva> reservas = null;
        private List<Regimen> regimenes = null;
        private List<Habitacion> habitaciones = null;
        private List<CierreTemporal> cierresTemporales = null;

        public Hotel() { }
        public Hotel(int idHotel, Categoria categoria, Direccion direccion, String nombre, String mail, String telefono, DateTime fechaInicioActividades,
            List<Reserva> reservas, List<Regimen> regimenes, List<Habitacion> habitaciones, List<CierreTemporal> cierresTemporales)
        {
            this.idHotel = idHotel;
            this.categoria = categoria;
            this.direccion = direccion;
            this.nombre = nombre;
            this.mail = mail;
            this.telefono = telefono;
            this.fechaInicioActividades = fechaInicioActividades;
            this.reservas = reservas;
            this.regimenes = regimenes;
            this.habitaciones = habitaciones;
            this.cierresTemporales = cierresTemporales;
        }

        public Hotel(int idHotel, Categoria categoria, Direccion direccion, String nombre, String mail, String telefono, DateTime fechaInicioActividades,List<Regimen> regimenes)
        {
            this.idHotel = idHotel;
            this.categoria = categoria;
            this.direccion = direccion;
            this.nombre = nombre;
            this.mail = mail;
            this.telefono = telefono;
            this.fechaInicioActividades = fechaInicioActividades;
            this.regimenes = regimenes;
        }

        public Hotel(int idHotel, Categoria categoria, Direccion direccion, String nombre, String mail, String telefono, DateTime fechaInicioActividades)
        {
            this.idHotel = idHotel;
            this.categoria = categoria;
            this.direccion = direccion;
            this.nombre = nombre;
            this.mail = mail;
            this.telefono = telefono;
            this.fechaInicioActividades = fechaInicioActividades;
        }
        public int getIdHotel()
        {
            return this.idHotel;
        }

        public void setIdHotel(int idHotel)
        {
            this.idHotel = idHotel;
        }

        public Categoria getCategoria()
        {
            return this.categoria;
        }

        public Direccion getDireccion()
        {
            return this.direccion;
        }

        public String getNombre()
        {
            return this.nombre;
        }

        public String getMail()
        {
            return this.mail;
        }

        public String getTelefono()
        {
            return this.telefono;
        }

        public DateTime getFechaInicioActividades()
        {
            return this.fechaInicioActividades;
        }

        public List<Reserva> getReservas()
        {

            if (this.reservas == null) {
                RepositorioReserva repoReserva = new RepositorioReserva();
                this.reservas = repoReserva.getByIdHotel(this.idHotel);
            }

            return this.reservas;
                      
        }

        public List<Regimen> getRegimenes()
        {

            if (this.regimenes == null)
            {
                RepositorioRegimen repoRegimen = new RepositorioRegimen();
                this.regimenes = repoRegimen.getByIdHotel(this.idHotel);
            }
            return this.regimenes;
            
        }

        public List<Habitacion> getHabitaciones()
        {
  
            if (this.habitaciones == null)
            {
                RepositorioHabitacion repoHabitacion = new RepositorioHabitacion();
                this.habitaciones = repoHabitacion.getByHotelId(this.idHotel);
            }
            return this.habitaciones;
            
        }

        public List<CierreTemporal> getCierresTemporales()
        {

            if (this.cierresTemporales == null)
            {
                RepositorioCierreTemporal repoCierres = new RepositorioCierreTemporal();
                this.cierresTemporales = repoCierres.getByIdHotel(this);
            }
            return this.cierresTemporales;
            
        }

        public Boolean esNuevo()
        {
            return idHotel.Equals(0);
        }

        //Estos metodos extra los necesito para popular los combo box y data grid view
        //public int IdHotel { get { return this.getIdHotel(); } }
        public String Nombre { get { return this.getNombre(); } }
        public String Mail { get { return this.getMail(); } }
        public String Telefono { get { return this.getTelefono(); } }
        public String Direccion { get { return this.getDireccion().getDireccionCorta(); } }
        public String Ciudad { get { return this.getDireccion().getCiudad(); } }
        public String Pais { get { return this.getDireccion().getPais(); } }
        public int Estrellas { get { return this.getCategoria().Estrellas; } }
        public String Regimenes { get { return String.Join(",",this.getRegimenes().Select(regimen => regimen.Descripcion)); } }
        public DateTime FechaInicioActividades { get { return this.getFechaInicioActividades(); } }

    }
}