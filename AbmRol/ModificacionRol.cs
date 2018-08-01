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
    public partial class ModificacionRol : Form
    {
        //ROL A MODIFICAR
        Rol rol = null;

        private void button1_Click(object sender, EventArgs e)
        {
            this.inicializarResetear();
        }

        public ModificacionRol(Rol rol)
        {
            InitializeComponent();
            this.rol = rol;
        }

        private void inicializarResetear()
        {
            //BUSCO TODAS LAS FUNCIONALIDADES
            RepositorioFuncionalidad repositorioFuncionalidad = new RepositorioFuncionalidad();
            dataGridFuncionalidades.DataSource = repositorioFuncionalidad.getAll().OrderBy(f => f.getDescripcion()).ToList();
            dataGridFuncionalidades.AutoResizeColumns();

            //ESTO LO TENGO QUE HACER PARA QUE NO APAREZCA SIEMPRE SELECCIONADO EL PRIMER ITEM
            dataGridFuncionalidades.CurrentCell = null;
            dataGridFuncionalidades.ClearSelection();

            //MARCO LAS FUNCIONALIDADES QUE PERTENECEN AL ROL
            foreach (DataGridViewRow row in dataGridFuncionalidades.Rows)
            {
                Funcionalidad funcionalidad = (Funcionalidad)row.DataBoundItem;
                if (rol.getFuncionalidades().Exists(f => f.getDescripcion().Equals(funcionalidad.getDescripcion())))
                {
                    dataGridFuncionalidades.Rows[row.Index].Selected = true;
                    dataGridFuncionalidades.Rows[row.Index].Cells[0].Selected = true;
                }
            }

            //MOSTRAR EL NOMBRE DEL ROL
            textBoxNombreRol.Text = rol.getNombre();

            //MOSTRAR SI EL ROL ESTA ACTIVO
            checkBoxActivo.Checked = rol.getActivo();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ModificacionRol_Load(object sender, EventArgs e)
        {
            this.inicializarResetear();
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

        private void buttonGuardar_Click(object sender, EventArgs e){
            String nombreRol = textBoxNombreRol.Text.Trim();
            Boolean activo = checkBoxActivo.Checked;
            List<Funcionalidad> funcionalidades = new List<Funcionalidad>();
            RepositorioRol repositorioRol = new RepositorioRol();

            foreach (DataGridViewRow row in dataGridFuncionalidades.SelectedRows)
            {
                funcionalidades.Add((Funcionalidad)row.DataBoundItem);
            }

            //CAMBIO LOS ATRIBUTOS DEL ROL
            rol.setNombre(nombreRol);
            rol.setActivo(activo);
            rol.setFuncionalidades(funcionalidades);

            //LA RESPONSABILIDAD DE VALIDAR EL INPUT LA DEJO EN LA INTERFAZ???
            //O MEJOR EN EL REPOSITORIO Y LAS MANEJO CON EXCEPCIONES???
            if (this.validoInput(this))
            {
                try
                {
                    repositorioRol.update(rol);
                    MessageBox.Show("Rol actualizado con éxito.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //ME TRAIGO EL ROL ACTUALIZADO
                    this.rol = repositorioRol.getById(rol.getIdRol());
                    this.inicializarResetear();
                }
                //catch (NoExisteIDException exc)
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Ingrese nombre del Rol y seleccione sus Funcionalidades", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Boolean validoInput(ModificacionRol form)
        {
            return !form.textBoxNombreRol.Text.Equals("") &&
                   !form.dataGridFuncionalidades.SelectedRows.Count.Equals(0);
        }
    }
}
