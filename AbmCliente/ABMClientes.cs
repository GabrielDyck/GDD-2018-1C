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

namespace FrbaHotel.AbmCliente
{
    public partial class ABMClientes : Form
    {
        public ABMClientes()
        {
            InitializeComponent();
        }

        private void ListadoClientes_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            List<KeyValuePair<String, Boolean>> estados = new List<KeyValuePair<String, Boolean>>();
            estados.Add(new KeyValuePair<String, Boolean>("Habilitado", true));
            estados.Add(new KeyValuePair<String, Boolean>("Inhabilitado", false));
            comboBoxEstados.ValueMember = "Value";
            comboBoxEstados.DisplayMember = "Key";
            comboBoxEstados.DataSource = estados;
            comboBoxEstados.SelectedValue = "";

            RepositorioIdentidad repoIdentidad = new RepositorioIdentidad();
            comboBoxTipoDoc.ValueMember = "Value";
            comboBoxTipoDoc.DisplayMember = "Key";
            comboBoxTipoDoc.DataSource = repoIdentidad.getAllTiposDocsClientes();
            comboBoxTipoDoc.SelectedValue = "";
        }

        private void limpiarBusquedaYResultados()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBoxEstados.SelectedValue = "";
            comboBoxTipoDoc.SelectedValue = "";
            dataGridView1.DataSource = null;
            this.button4.Enabled = false;
            this.button5.Enabled = false;

            //ESTO ES PARA QUE ME ACTUALICE LA LISTA DE DOCUMENTOS POSIBLES
            RepositorioIdentidad repoIdentidad = new RepositorioIdentidad();
            comboBoxTipoDoc.DataSource = repoIdentidad.getAllTiposDocsClientes();
            comboBoxTipoDoc.SelectedValue = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.limpiarBusquedaYResultados();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            botonBuscar.Enabled = false;
            String nombre = textBox1.Text.Trim();
            String apellido = textBox2.Text.Trim();
            String mail = textBox3.Text.Trim();
            String nroDoc = textBox4.Text.Trim();
            KeyValuePair<String, Boolean> estado = new KeyValuePair<String, Boolean>();
            String tipoDoc = "";
            RepositorioCliente repositorioClientes = new RepositorioCliente();

            if (comboBoxEstados.SelectedItem != null)
            {
                estado = (KeyValuePair<String, Boolean>)comboBoxEstados.SelectedItem;
            }

            if (comboBoxTipoDoc.SelectedItem != null)
            {
                tipoDoc = (String)comboBoxTipoDoc.SelectedItem;
            }

            List<Cliente> clientes = repositorioClientes.getByQuery(nombre,apellido,tipoDoc,nroDoc,estado,mail);

            //MEJORA DE PERFORMANCE DEL DGV
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.DataSource = clientes;
            dataGridView1.RowHeadersVisible = true;
            //ESTO LO TENGO QUE HACER PARA QUE NO APAREZCA SIEMPRE SELECCIONADO EL PRIMER ITEM
            dataGridView1.CurrentCell = null;
            dataGridView1.ClearSelection();

            //PONGO ESTO ACA PARA QUE DESPUES DE DAR DE ALTA, MODIFICAR O DAR DE BAJA
            //Y SE VUELVA A CARGAR LA LISTA, NO SE PUEDA MODIFICAR O DAR DE BAJA
            //UN CLIENTE NULL...
            this.button4.Enabled = false;
            this.button5.Enabled = false;

            //ESTO ES PARA QUE ME ACTUALICE LA LISTA DE DOCUMENTOS POSIBLES
            RepositorioIdentidad repoIdentidad = new RepositorioIdentidad();
            comboBoxTipoDoc.DataSource = repoIdentidad.getAllTiposDocsClientes();
            comboBoxTipoDoc.SelectedValue = "";

            botonBuscar.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //aca va el alta
            using (AltaCliente form = new AltaCliente())
            {
                var result = form.ShowDialog();

                //AL CERRAR LA VENTANA DESPUES DE DAR DE ALTA UN NUEVO ROL VUELVO A CARGAR LA LISTA
                //this.button2_Click(sender, e);
                //NO LO HAGO MAS PORQUE TRAE PROBLEMAS DE PERFORMANCE
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Cliente cliente = (Cliente)dataGridView1.CurrentRow.DataBoundItem;

            using (ModificacionCliente form = new ModificacionCliente(cliente))
            {
                var result = form.ShowDialog();

                //AL CERRAR LA VENTANA DESPUES DE DAR DE ALTA UN NUEVO CLIENTE VUELVO A CARGAR LA LISTA
                this.button2_Click(sender, e);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro que desea dar de baja el Cliente?", "Baja Logica", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                RepositorioCliente repoCliente = new RepositorioCliente();
                Cliente cliente = (Cliente)dataGridView1.CurrentRow.DataBoundItem;

                repoCliente.bajaLogica(cliente);

                //CUANDO DOY DE BAJA EL CLIENTE VUELVO A CARGAR LA LISTA
                //this.button2_Click(sender, e);
                //NO LO HAGO MAS PORQUE TRAE PROBLEMAS DE PERFORMANCE
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
                this.button4.Enabled = true;
                this.button5.Enabled = true;
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
