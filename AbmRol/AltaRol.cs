using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FrbaHotel.Modelo;
using FrbaHotel.Repositorios;
using System.Diagnostics;
using FrbaHotel.Excepciones;

namespace FrbaHotel.AbmRol
{
    public partial class AltaRol : Form
    {
        public AltaRol()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.limpiarPantalla();
        }

        private void limpiarPantalla()
        {
            RepositorioFuncionalidad repositorioFuncionalidad = new RepositorioFuncionalidad();
            dataGridFuncionalidades.DataSource = repositorioFuncionalidad.getAll().OrderBy(f => f.getDescripcion()).ToList();
            dataGridFuncionalidades.AutoResizeColumns();
            dataGridFuncionalidades.ClearSelection();
            textBoxNombreRol.Text = "";
            checkBoxActivo.Checked = false;

            //ESTO LO TENGO QUE HACER PARA QUE NO APAREZCA SIEMPRE SELECCIONADO EL PRIMER ITEM
            dataGridFuncionalidades.CurrentCell = null;
            dataGridFuncionalidades.ClearSelection();
        }

        private void AltaRol_Load(object sender, EventArgs e)
        {
            this.limpiarPantalla();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void button2_Click(object sender, EventArgs e)
        {
            String nombreRol = textBoxNombreRol.Text.Trim();
            Boolean activo = checkBoxActivo.Checked;
            List<Funcionalidad> funcionalidades = new List<Funcionalidad>();
            RepositorioRol repositorioRol = new RepositorioRol();
            Rol rol = null;

            foreach (DataGridViewRow row in dataGridFuncionalidades.SelectedRows)
            {
                funcionalidades.Add((Funcionalidad) row.DataBoundItem);
            }

            rol = new Rol(0, nombreRol, activo, funcionalidades);

            //LA RESPONSABILIDAD DE VALIDAR EL INPUT LA DEJO EN LA INTERFAZ???
            //O MEJOR EN EL REPOSITORIO Y LAS MANEJO CON EXCEPCIONES???
            if (this.validoInput(this))
            {
                try
                {
                    repositorioRol.create(rol);
                    MessageBox.Show("Rol creado con éxito.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.limpiarPantalla();
                }
                //catch (ElementoYaExisteException exc)
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Ingrese nombre del Rol y seleccione sus Funcionalidades.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Boolean validoInput(AltaRol form)
        {
            return !form.textBoxNombreRol.Text.Equals("") &&
                   !form.dataGridFuncionalidades.SelectedRows.Count.Equals(0);
        }
    }
}
