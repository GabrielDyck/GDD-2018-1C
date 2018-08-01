using FrbaHotel.Excepciones;
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
    public partial class CancelarReserva : Form
    {

        private Reserva reserva;
        private Usuario usuario;

        public CancelarReserva(Reserva reserva, Usuario usuario)
        {
            this.reserva = reserva;
            this.usuario = usuario;
            InitializeComponent();
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                //no se valida el string que ingreso como motivo
                //el repositorio reserva no tira excepciones si falla al actualizar la reserva
                //y este codigo no las agarra esas excepciones
                String motivo = textMotivo.Text.Trim();
                RepositorioReserva repoReserva = new RepositorioReserva();
                repoReserva.cancelarReserva(reserva, usuario, motivo);
                MessageBox.Show("Reserva cancelada exitosamente.", "Gestion de Datos TP 2018 1C - LOS_BORBOTONES", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            //catch (RequestInvalidoException exception)
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Verifique los datos ingresados", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
