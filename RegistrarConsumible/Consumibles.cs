using FrbaHotel.Commons;
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

namespace FrbaHotel.RegistrarConsumible
{
    public partial class Consumibles : Form
    {
        int idEstadia = 0;
        private Sesion sesion = null;
        public Consumibles(Sesion sesion)
        {
            InitializeComponent();
            this.sesion = sesion;
        }
        private void ListadoConsumibles_Load(object sender, EventArgs e)
        {
            textBox1.Text = "";
            dataGridView1.DataSource = null;
            idEstadia = 0;
            botonAgregar.Enabled = false;
            botonRegistrar.Enabled = false;
            botonBorrar.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.ListadoConsumibles_Load(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                RepositorioReserva repoReserva = new RepositorioReserva();
                RepositorioEstadia repoEstadia = new RepositorioEstadia();
                String estado = "";

                idEstadia = int.Parse(textBox1.Text.Trim());
                //hago el get estado para ver si no termino de ponerle el Reserva Con Consumibles (RCC)
                Reserva reserva= repoReserva.getIdByIdEstadia(idEstadia);

                if (reserva == null)
                {
                    MessageBox.Show("La estadia ingresada no existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (reserva.getHotel().getIdHotel()!=this.sesion.getHotel().getIdHotel()){
                        MessageBox.Show("La estadia ingresada no corresponde al hotel al cual el usuario esta logueado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        estado = repoEstadia.getEstado((int)reserva.getCodigoReserva());
                    if (estado.Equals("RCI") | estado.Equals("RCE"))
                    {
                        RepositorioConsumibles repositorioConsumibles = new RepositorioConsumibles();
                        List<ConsumibleParaMostrar> consumibles = repositorioConsumibles.getByQuery(idEstadia);

                        //PARA QUE NO PINCHE SI NO TRAE RESULTADOS
                        if (consumibles.Count.Equals(0))
                        {
                            dataGridView1.DataSource = null;
                            botonAgregar.Enabled = true;
                            botonRegistrar.Enabled = true;
                        }
                        else
                        {
                            dataGridView1.DataSource = consumibles;
                            dataGridView1.AutoResizeColumns();
                            dataGridView1.ClearSelection();

                            botonAgregar.Enabled = true;
                            botonRegistrar.Enabled = true;

                            //ESTO LO TENGO QUE HACER PARA QUE NO APAREZCA SIEMPRE SELECCIONADO EL PRIMER ITEM
                            dataGridView1.CurrentCell = null;
                            dataGridView1.ClearSelection();
                        }                        
                    }
                    else
                        MessageBox.Show("La estadia debe estar con ingreso o con egreso para registrar consumibles.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }else
                MessageBox.Show("Ingresar ID estadia por favor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //aca va el alta del consumible.
            RepositorioReserva repoReserva = new RepositorioReserva();
            if (textBox1.Text != "")
            {
                Reserva reserva= repoReserva.getIdByIdEstadia(idEstadia);
                if(reserva==null)
                    {
                        MessageBox.Show("La estadia ingresada no existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                else
                {
                    using (AgregarConsumible form = new AgregarConsumible(idEstadia))
                    {
                        var result = form.ShowDialog();

                        if (result == DialogResult.OK)
                        {
                            
                        }
                    }
                    this.button2_Click(sender, e);
                }
            }
            else
                MessageBox.Show("Ingresar ID estadia por favor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        private void button4_Click(object sender, EventArgs e)
        {
                    //pongo el estado en RCCR (reserva con consumibles registrados) ya queda sin chance de modificar.
                    RepositorioEstadoReserva repoEstadoReserva = new RepositorioEstadoReserva();
                    RepositorioReserva repoReserva = new RepositorioReserva();
                    DateTime date = Utils.getSystemDatetimeNow();

                    int idEstadoReserva = 0;
                    Reserva reserva = repoReserva.getIdByIdEstadia(idEstadia);

                    String desc = "Reserva Con Consumibles Registrados";
                    String tipoEstado = "RCCR";
                    EstadoReserva estadoReserva = new EstadoReserva(idEstadoReserva, this.sesion.getUsuario(), reserva, tipoEstado, date, desc);

                    try
                    {
                        repoEstadoReserva.update(estadoReserva);
                        MessageBox.Show("Consumibles registrados. Ya puede facturar la estadia.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.botonAgregar.Enabled = false;
                        this.botonBorrar.Enabled = false;
                        this.botonRegistrar.Enabled = false;
                        this.dataGridView1.DataSource = null;
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.Message, "Error al cerrar el registro.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

        }
        private void button5_Click(object sender, EventArgs e)
        {
            RepositorioReserva repoReserva = new RepositorioReserva();
            Reserva reserva= repoReserva.getIdByIdEstadia(idEstadia);

            DialogResult result = MessageBox.Show("¿Está seguro que desea quitar este consumible?", "Baja Logica", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                RepositorioConsumibles repoConsumible = new RepositorioConsumibles();
                ConsumibleParaMostrar consParaMostrar = (ConsumibleParaMostrar)dataGridView1.CurrentRow.DataBoundItem;

                try
                {
                    repoConsumible.baja(consParaMostrar.getConsumible(), idEstadia);
                    MessageBox.Show("Consumible(s) eliminado(s) correctamente de la estadia.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, "Error al dar de baja el consumible de la estadia.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //CUANDO DOY DE BAJA EL CONSUMIBLE VUELVO A CARGAR LA LISTA
                this.button2_Click(sender, e);

                this.botonBorrar.Enabled = false;
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

        private void onlyNumeric(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (dgv == null) return;
            if (dgv.CurrentRow.Selected)
            {
                botonBorrar.Enabled = true;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
