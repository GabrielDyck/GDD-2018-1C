using FrbaHotel.AbmCliente;
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

namespace FrbaHotel.RegistrarEstadia
{
    public partial class VincularHuespedes : Form
    {
        List<Cliente> clientesElegidos =new List<Cliente>() ;
        private int codReserva = 0;

        public VincularHuespedes(int reserva)
        {
            InitializeComponent();
            this.codReserva = reserva;
        }

        private void ListadoClientes_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;

            RepositorioIdentidad repoIdentidad = new RepositorioIdentidad();
            comboBoxTipoDoc.ValueMember = "Value";
            comboBoxTipoDoc.DisplayMember = "Key";
            comboBoxTipoDoc.DataSource = repoIdentidad.getAllTiposDocsClientes();
            comboBoxTipoDoc.SelectedValue = "";
        }

        private void limpiarBusquedaYResultados()
        {
            textBox3.Text = "";
            textBox4.Text = "";
            comboBoxTipoDoc.SelectedValue = "";
            dataGridView1.DataSource = null;
            //this.botonReservar.Enabled = false;


            //ESTO ES PARA QUE ME ACTUALICE LA LISTA DE DOCUMENTOS POSIBLES
            RepositorioIdentidad repoIdentidad = new RepositorioIdentidad();
            comboBoxTipoDoc.DataSource = repoIdentidad.getAllTiposDocsClientes();
            comboBoxTipoDoc.SelectedValue = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.limpiarBusquedaYResultados();
        }

        private void buttonBuscar_click(object sender, EventArgs e)
        {

            String mail = textBox3.Text.Trim();
            String nroDoc = textBox4.Text.Trim();
            KeyValuePair<String, Boolean> estado = new KeyValuePair<String, Boolean>();
            estado = new KeyValuePair<String, Boolean>("Habilitado", true);
            String tipoDoc = "";
            RepositorioCliente repositorioClientes = new RepositorioCliente();


            if (comboBoxTipoDoc.SelectedItem != null)
            {
                tipoDoc = (String)comboBoxTipoDoc.SelectedItem;
            }

            List<Cliente> clientes = repositorioClientes.getByQuery("", "", tipoDoc, nroDoc, estado, mail);

            //MEJORA DE PERFORMANCE DEL DGV
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.DataSource = clientes;
            dataGridView1.RowHeadersVisible = true;
            //ESTO LO TENGO QUE HACER PARA QUE NO APAREZCA SIEMPRE SELECCIONADO EL PRIMER ITEM
            dataGridView1.CurrentCell = null;
            dataGridView1.ClearSelection();
            
            //ESTO ES PARA QUE ME ACTUALICE LA LISTA DE DOCUMENTOS POSIBLES
            RepositorioIdentidad repoIdentidad = new RepositorioIdentidad();
            comboBoxTipoDoc.DataSource = repoIdentidad.getAllTiposDocsClientes();
            comboBoxTipoDoc.SelectedValue = "";
        }

        private void buttonAlta_click(object sender, EventArgs e)
        {
            //aca va el alta
            using (AltaCliente form = new AltaCliente())
            {
                var result = form.ShowDialog();

                //AL CERRAR LA VENTANA DESPUES DE DAR DE ALTA UN NUEVO ROL VUELVO A CARGAR LA LISTA
                this.buttonBuscar_click(sender, e);
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (dgv == null) return;
            if (dgv.CurrentRow.Selected)
            {
                this.botonReservar.Enabled = true;
                dataGridView2.DataSource = null;
                //la agrego al datagrid 2
                foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
                {
                    Cliente clientSeleccionado = item.DataBoundItem as Cliente;

                    if (clientSeleccionado.getInconsistente())
                    {
                        MessageBox.Show("El cliente aparece como inconsistente en la base, por favor verifique el numero de documento y mail del cliente y actualice la informacion. No podrá vincular un cliente inconsistente a una estadia. Por favor edite el cliente desde el ABM indicado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (!clientSeleccionado.getActivo())
                    {
                        MessageBox.Show("El cliente aparece como inhabilitado en la base. No podrá vincular un cliente inhabilitado a una estadia.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    //PARA NO AGREGAR DUPLICADOS
                    if (!clientesElegidos.Exists(clienteYaAgregado => clienteYaAgregado.getIdCliente().Equals(clientSeleccionado.getIdCliente())))
                    {
                        clientesElegidos.Add(item.DataBoundItem as Cliente);
                    }
                }
                //MEJORA DE PERFORMANCE DEL DGV
                dataGridView2.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
                dataGridView2.RowHeadersVisible = false;
                dataGridView2.DataSource = clientesElegidos;
                dataGridView2.RowHeadersVisible = true;
                //ESTO LO TENGO QUE HACER PARA QUE NO APAREZCA SIEMPRE SELECCIONADO EL PRIMER ITEM
                dataGridView2.CurrentCell = null;
                dataGridView2.ClearSelection();
                //vuelvo a cargar la lista
                this.buttonBuscar_click(sender, e);
            }
        }

        private void botonLimpiar_Click(object sender, EventArgs e)
        {
            this.limpiarBusquedaYResultados();
        }

        private void botonReservar_Click(object sender, EventArgs e)
        {

            RepositorioEstadia repoEstadia = new RepositorioEstadia();
            
            try
            {
                repoEstadia.vincularHuespedes(codReserva, clientesElegidos);
                //MessageBox.Show("Check in realizado exitosamente \n Codigo de reserva: " + codReserva, "Gestion de Datos TP 2018 1C - LOS_BORBOTONES");
                MessageBox.Show("Huespedes vinculados correctamente a la reserva. ", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Gestion de Datos TP 2018 1C - LOS_BORBOTONES", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Cliente cliente = null;
            try{
            cliente = (Cliente)dataGridView2.CurrentRow.DataBoundItem;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error al obtener el cliente de la base", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (cliente==null)
            {
                MessageBox.Show("No se selecciono ningun cliente para eliminar. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {    DialogResult result = MessageBox.Show("¿Está seguro que desea quitar este cliente?", "Baja", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    
                    
                    foreach (Cliente item in clientesElegidos.ToList())
                    {
                        if (item.getIdCliente() == cliente.getIdCliente())
                        {
                            clientesElegidos.Remove(item);

                        }
                    }
                    
                    //CUANDO DOY DE BAJA EL Cliente VUELVO A CARGAR LA LISTA
                    dataGridView2.DataSource = null;
                    dataGridView2.DataSource = clientesElegidos;
                    dataGridView2.ClearSelection();
                }
            }
        }

        private void onlyNumeric(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
