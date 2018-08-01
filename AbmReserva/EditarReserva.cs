using FrbaHotel.Commons;
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
    public partial class EditarReserva : Form
    {

        private Usuario usuario;
        private Sesion sesion;
        public EditarReserva(Sesion sesion)
        {
            this.sesion = sesion;
            this.usuario = sesion != null ? sesion.getUsuario() : null;
            InitializeComponent();
        }

        public EditarReserva(Usuario usuario)
        {
            this.usuario = usuario;
            InitializeComponent();
        }



        private void onlyNumeric(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void buttonBuscar_Click(object sender, EventArgs e)
        {
            buscarReservas();

        }



        private void buscarReservas() {

            dataGridReserva.DataSource = null;
            try
            {
                int codigoReserva = Utils.validateIntField(textCodigoReserva.Text, "Codigo de Reserva");
                RepositorioReserva repoReserva = new RepositorioReserva();
                Reserva reserva = repoReserva.getReservaByCodigoReserva(codigoReserva);

                if (reserva != null)
                {

                    if (sesion != null && reserva.getHotel().getIdHotel() != sesion.getHotel().getIdHotel())
                    {
                        MessageBox.Show("La reserva buscada no corresponde al hotel " + sesion.getHotel().getNombre() + ".", "Error al editar reserva", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    EstadoReserva estadoDeLaReserva = reserva.getEstadoReserva();
                    List<String> estadosNoModificables = new List<String>(new String[] { "RCC", "RCR", "RCNS", "RCE", "RCI","RCCR", "RF" });
                    bool noPuedeModificar = estadosNoModificables.Exists(estadoNoModificable => estadoNoModificable.Equals(estadoDeLaReserva.getTipoEstado()));

                    if (noPuedeModificar)
                    {
                        MessageBox.Show("No puede modificar la reserva por que la misma ha alcanzado un estado no modificable.", "Error al editar reserva", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }


                    DateTime fechaAhora = Utils.getSystemDatetimeNow();
                    DateTime fechaInicio = reserva.getFechaDesde();


                    if (((fechaInicio - fechaAhora).TotalDays > 1) && (fechaInicio > fechaAhora))
                    {
                        this.buttonModificar.Enabled = true;
                        this.buttonCancelar.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Las reservas pueden ser editadas hasta 24 horas antes de la fecha de inicio de la misma.", "Error al editar reserva", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }



                    List<Reserva> reservas = new List<Reserva>();
                    reservas.Add(reserva);
                    dataGridReserva.DataSource = reservas;
                    dataGridReserva.AutoResizeColumns();

                    this.AcceptButton = this.buttonModificar;

                }
                else
                {
                    MessageBox.Show("No se ha encontrado la reserva que intenta modificar.", "Error al editar reserva", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //catch (RequestInvalidoException exception)
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Verifique los datos ingresados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            Reserva reserva=null;
            foreach (DataGridViewRow item in this.dataGridReserva.SelectedRows)
            {
                reserva = item.DataBoundItem as Reserva;
            }
            using (CancelarReserva form = new CancelarReserva(reserva, usuario))
            {
                var result = form.ShowDialog();
                //this.Close();
            }
            buscarReservas();

        }

        private void buttonModificar_Click(object sender, EventArgs e)
        {
            Reserva reserva = null;
            foreach (DataGridViewRow item in this.dataGridReserva.SelectedRows)
            {
                reserva = item.DataBoundItem as Reserva;
            }

            using (ModificarReserva form = new ModificarReserva(reserva,usuario))
            {
                var result = form.ShowDialog();
                //this.Close();
            }
            buscarReservas();

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
