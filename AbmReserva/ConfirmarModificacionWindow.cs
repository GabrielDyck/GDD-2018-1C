using FrbaHotel.Modelo;
using FrbaHotel.Repositorios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrbaHotel.AbmReserva
{
    public partial class ConfirmarModificacionWindow : Form
    {

        private List<HabitacionDisponible> habitaciones;
        private DateTime fechaInicio;
        private DateTime fechaFin;
        private int diasDeEstadia;
        private Usuario usuario;
        private Reserva reserva;

        public ConfirmarModificacionWindow(List<HabitacionDisponible> habitaciones, DateTime fechaInicio, DateTime fechaFin, Usuario usuario,Reserva reserva)
        {

            this.habitaciones = habitaciones;
            this.fechaInicio = fechaInicio;
            this.fechaFin = fechaFin;
            this.diasDeEstadia = (fechaFin - fechaInicio).Days;
            this.usuario = usuario;
            this.reserva = reserva;
            InitializeComponent();
            init();
        }

        private void init() {

            

            this.labelInformacionDeModificacion.Text = "Reserva actual desde el dia: " + reserva.getFechaDesde() + " hasta " + reserva.getFechaHasta() + ".\n";
            decimal precioTotalReservaActual = 0;
            foreach (Habitacion habitacion in reserva.getHabitaciones())
            {

                decimal precioTipoHabitacion=habitacion.getTipoHabitacion().getPorcentual();
                decimal precioRegimen= reserva.getRegimen().getPrecio();
                decimal precioCategoria= reserva.getHotel().getCategoria().getRecargaEstrellas();
                decimal precioNocheHab=  (precioRegimen*precioTipoHabitacion) + precioCategoria;

                decimal precioHabitacion = (reserva.getDiasAlojados() * precioNocheHab);
                precioTotalReservaActual += precioHabitacion;
                this.labelInformacionDeModificacion.Text += "Habitacion numero " + habitacion.getNumero() + " de tipo " + habitacion.getTipoHabitacion().getDescripcion() + " con el regimen " + reserva.getRegimen().getDescripcion() + ". Cantidad de dias: " + reserva.getDiasAlojados() + " por la suma de " + precioHabitacion + "\n";

            }
            this.labelInformacionDeModificacion.Text += " en el hotel " + reserva.getHotel().getNombre() + " por la suma de " + precioTotalReservaActual + "USD\n";



            this.labelInformacionDeModificacion.Text += "\n \n \n";
            this.labelInformacionDeModificacion.Text+= "Nueva Reserva desde el dia: " + fechaInicio + " hasta " + fechaFin +".\n";
            decimal precioTotal = 0;
            foreach (HabitacionDisponible habitacion in habitaciones)
            {
                decimal precioHabitacion = (diasDeEstadia * habitacion.PrecioPorNoche);
                precioTotal += precioHabitacion;
                this.labelInformacionDeModificacion.Text += "Habitacion numero " + habitacion.Numero + " de tipo " + habitacion.getHabitacion().getTipoHabitacion().getDescripcion() + " con el regimen " + habitacion.Regimen + ". Cantidad de dias: " + diasDeEstadia + " por la suma de " + precioHabitacion + "\n";

            }
            this.labelInformacionDeModificacion.Text += " en el hotel " + habitaciones[0].Hotel + " por la suma de " + precioTotal + "USD\n";
            this.labelInformacionDeModificacion.Text += "\n \n \n";

            this.labelInformacionDeModificacion.Text += " ¿Desea modificar la reserva?";

        }

        private void rechazarReservaButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void confirmarReservaButton_Click(object sender, EventArgs e)
        {
            using (VincularCliente form = new VincularCliente(habitaciones,fechaInicio,fechaFin,diasDeEstadia,usuario))
            {
                var result = form.ShowDialog();
                this.Close();
            }
        }

        private void modificarReservaButton_Click(object sender, EventArgs e)
        {
            List<Habitacion> habitacionesParaReservar = new List<Habitacion>();
            Regimen regimen=null;
            Hotel hotel=null;
            foreach (HabitacionDisponible dto in habitaciones) {
                habitacionesParaReservar.Add(dto.getHabitacion());
                regimen = dto.getRegimen();
                hotel=dto.getHabitacion().getHotel();
            }


            RepositorioReserva repoReserva = new RepositorioReserva();

            reserva.setHotel(hotel);
            reserva.setFechaDesde(fechaInicio);
            reserva.setFechaHasta(fechaFin);
            reserva.setHabitaciones(habitacionesParaReservar);
            reserva.setRegimen(regimen);
            reserva.setDiasAlojados(diasDeEstadia);
            reserva.setUsuarioGenerador(usuario);
            //Reserva reserva = new Reserva(habitacionesParaReservar, regimen, cliente, fechaInicio, fechaFin, diasDeEstadia,usuario);
            try
            {
                repoReserva.modificarReserva(reserva);
                MessageBox.Show("Reserva modificada exitosamente \n Codigo de reserva: " + reserva.getCodigoReserva(), "Gestion de Datos TP 2018 1C - LOS_BORBOTONES", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Gestion de Datos TP 2018 1C - LOS_BORBOTONES", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        
        }

    }
    
}
