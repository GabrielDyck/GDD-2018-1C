using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FrbaHotel.Repositorios;
using FrbaHotel.Modelo;

namespace FrbaHotel.AbmRol
{
    public partial class ABMRoles : Form
    {
        public ABMRoles()
        {
            InitializeComponent();
        }

        private void ListadoRoles_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            List<KeyValuePair<String, Boolean>> estados = new List<KeyValuePair<String, Boolean>>();
            estados.Add(new KeyValuePair<String, Boolean>("Habilitado", true));
            estados.Add(new KeyValuePair<String, Boolean>("Inhabilitado", false));
            comboBoxEstados.ValueMember = "Value";
            comboBoxEstados.DisplayMember = "Key";
            comboBoxEstados.DataSource = estados;
            comboBoxEstados.SelectedValue = "";

            RepositorioFuncionalidad repositorioFuncionalidad = new RepositorioFuncionalidad();
            comboBoxFuncionalidades.ValueMember = "IdFuncionalidad";
            comboBoxFuncionalidades.DisplayMember = "Descripcion";
            comboBoxFuncionalidades.DataSource = repositorioFuncionalidad.getAll();
            comboBoxFuncionalidades.SelectedValue = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.limpiarBusquedaYResultados();
        }

        private void limpiarBusquedaYResultados()
        {
            dataGridView1.DataSource = null;
            textBox1.Text = "";
            comboBoxEstados.SelectedValue = "";
            comboBoxFuncionalidades.SelectedValue = "";
            this.button4.Enabled = false;
            this.button5.Enabled = false;
        }

        private void buscar_Click(object sender, EventArgs e)
        {
            String nombreRol = textBox1.Text.Trim();
            KeyValuePair<String, Boolean> estado = new KeyValuePair<String, Boolean>();
            Funcionalidad funcionalidad = null;
            RepositorioRol repositorioRoles = new RepositorioRol();

            if (comboBoxEstados.SelectedItem != null)
            {
                estado = (KeyValuePair<String, Boolean>)comboBoxEstados.SelectedItem;
            }

            if (comboBoxFuncionalidades.SelectedItem != null)
            {
                funcionalidad = (Funcionalidad)comboBoxFuncionalidades.SelectedItem;
            }

            dataGridView1.DataSource = repositorioRoles.getByQuery(nombreRol, estado, funcionalidad).OrderBy(r => r.getNombre()).ToList();
            dataGridView1.AutoResizeColumns();
            //ESTO LO TENGO QUE HACER PARA QUE NO APAREZCA SIEMPRE SELECCIONADO EL PRIMER ITEM
            dataGridView1.CurrentCell = null;
            dataGridView1.ClearSelection();

            //PONGO ESTO ACA PARA QUE DESPUES DE DAR DE ALTA, MODIFICAR O DAR DE BAJA
            //Y SE VUELVA A CARGAR LA LISTA, NO SE PUEDA MODIFICAR O DAR DE BAJA
            //UN ROL NULL...
            this.button4.Enabled = false;
            this.button5.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (AltaRol form = new AltaRol())
            {
                var result = form.ShowDialog();

                //AL CERRAR LA VENTANA DESPUES DE DAR DE ALTA UN NUEVO ROL VUELVO A CARGAR LA LISTA
                this.buscar_Click(sender, e);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Rol rol = (Rol) dataGridView1.CurrentRow.DataBoundItem;

            using (ModificacionRol form = new ModificacionRol(rol))
            {
                var result = form.ShowDialog();

                //AL CERRAR LA VENTANA DESPUES DE MODIFICAR EL ROL VUELVO A CARGAR LA LISTA
                this.buscar_Click(sender, e);
            }
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

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro que desea dar de baja el Rol?", "Baja Logica", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                RepositorioRol repositorioRol = new RepositorioRol();
                Rol rol = (Rol)dataGridView1.CurrentRow.DataBoundItem;

                repositorioRol.bajaLogica(rol);

                //CUANDO DOY DE BAJA EL ROL VUELVO A CARGAR LA LISTA
                this.buscar_Click(sender, e);
            }
        }
    }
}
