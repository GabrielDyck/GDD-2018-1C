using FrbaHotel.Modelo;
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
    public partial class ConfirmarReservaWindow : Form
    {

        private List<HabitacionDisponible> habitaciones;
        private DateTime fechaInicio;
        private  DateTime fechaFin;
        private int diasDeEstadia;
        private Usuario usuario;
        public ConfirmarReservaWindow(List<HabitacionDisponible> habitaciones, DateTime fechaInicio, DateTime fechaFin,Usuario  usuario)
        {
            this.habitaciones = habitaciones;
            this.fechaInicio = fechaInicio;
            this.fechaFin = fechaFin;
            this.diasDeEstadia = (fechaFin - fechaInicio).Days;
            this.usuario=usuario;
            InitializeComponent();
            init();
        }


        private void init() {

            decimal precioTotal = 0;

            this.labelTipoHabitacion.Text += "Reserva desde el dia: " + fechaInicio + " hasta " + fechaFin +".\n";
            foreach (HabitacionDisponible habitacion in habitaciones)
            {
                decimal precioHabitacion = (diasDeEstadia * habitacion.PrecioPorNoche);
                precioTotal += precioHabitacion;
                this.labelTipoHabitacion.Text += "Habitacion numero " + habitacion.Numero + " de tipo " + habitacion.getHabitacion().getTipoHabitacion().getDescripcion() + " con el regimen " + habitacion.Regimen + ". Cantidad de dias: " + diasDeEstadia + " por la suma de " + precioHabitacion + "\n";

            }
            this.labelTipoHabitacion.Text += " en el hotel " + habitaciones[0].Hotel + " por la suma de " + precioTotal + "USD\n";
            this.labelTipoHabitacion.Text += "¿Desea realizar la reserva?";
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

        //CIERRO LA VENTANA CON ESCAPE
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
