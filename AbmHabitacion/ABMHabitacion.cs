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

namespace FrbaHotel.AbmHabitacion
{
    public partial class ABMHabitacion : Form
    {
        private Sesion sesion = null;

        public ABMHabitacion(Sesion sesion)
        {
            InitializeComponent();

            this.sesion = sesion;

            this.Text = "ABM Habitación: " + this.sesion.getHotel().getNombre();

            RepositorioTipoHabitacion repositorioTipoHab = new RepositorioTipoHabitacion();

            comboBoxTipoHabitacion.DataSource = repositorioTipoHab.getAll();
            comboBoxTipoHabitacion.ValueMember = "Descripcion";

            List<KeyValuePair<String, Boolean>> estados = new List<KeyValuePair<String, Boolean>>();
            estados.Add(new KeyValuePair<String, Boolean>("Habilitado", true));
            estados.Add(new KeyValuePair<String, Boolean>("Inhabilitado", false));
            comboBoxEstados.ValueMember = "Value";
            comboBoxEstados.DisplayMember = "Key";
            comboBoxEstados.DataSource = estados;
            comboBoxEstados.SelectedValue = "";

            limpiarBusquedaYResultados();
        }

        private void buttonCrearHabitacion_Click(object sender, EventArgs e)
        {
            using (CrearHabitacion crearHabitacion = new CrearHabitacion(this.sesion.getHotel()))
            {
                var resultFormCrearHabitacion = crearHabitacion.ShowDialog();

                //AL CERRAR LA VENTANA DESPUES DE DAR DE ALTA UNA NUEVA HABITACION VUELVO A CARGAR LA LISTA
                this.buttonBbuscarHoteles_Click(sender, e);
            }
        }

        private void buttonBajaHabitacion_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro que desea dar de baja la Habitación?", "Baja Logica", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                RepositorioHabitacion repoHabitacion = new RepositorioHabitacion();
                Habitacion habitacion = (Habitacion)registroHabitaciones.CurrentRow.DataBoundItem;

                repoHabitacion.bajaLogica(habitacion);

                //AL CERRAR LA VENTANA DESPUES DE MODIFICAR UNA HABITACION VUELVO A CARGAR LA LISTA
                this.buttonBbuscarHoteles_Click(sender, e);
            }
        }

        private void buttonBbuscarHoteles_Click(object sender, EventArgs e)
        {
            String numero = validateStringFields(textNumero.Text.Trim());
            String piso = validateStringFields(textPiso.Text.Trim());
            TipoHabitacion tipoHabitacion = (TipoHabitacion)comboBoxTipoHabitacion.SelectedItem;
            RepositorioHabitacion repositorioHabitacion = new RepositorioHabitacion();
            //bool activa = checkBoxActiva.Checked;

            KeyValuePair<String, Boolean> estado = new KeyValuePair<String, Boolean>();
            if (comboBoxEstados.SelectedItem != null)
            {
                estado = (KeyValuePair<String, Boolean>)comboBoxEstados.SelectedItem;
            }

            //MEJORA DE PERFORMANCE DEL DGV
            registroHabitaciones.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            registroHabitaciones.RowHeadersVisible = false;
            registroHabitaciones.DataSource = repositorioHabitacion.getByQuery(numero, piso, this.sesion.getHotel(), tipoHabitacion, estado).OrderBy(hab => hab.getNumero()).ToList();
            registroHabitaciones.AutoResizeColumns();
            registroHabitaciones.RowHeadersVisible = true;

            //ESTO LO TENGO QUE HACER PARA QUE NO APAREZCA SIEMPRE SELECCIONADO EL PRIMER ITEM
            registroHabitaciones.CurrentCell = null;
            registroHabitaciones.ClearSelection();

            //PONGO ESTO ACA PARA QUE DESPUES DE DAR DE ALTA, MODIFICAR O DAR DE BAJA
            //Y SE VUELVA A CARGAR LA LISTA, NO SE PUEDA MODIFICAR O DAR DE BAJA
            //UNA HABITACION NULL...
            this.buttonModificarHabitacion.Enabled = false;
            this.buttonBajaHabitacion.Enabled = false;
        }

        private String validateStringFields(String field)
        {
            return field == "" ? null : field;
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

        private void registroHabitacion_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            DataGridView dgv = sender as DataGridView;

            if (dgv == null) return;
            if (dgv.CurrentRow.Selected)
            {
                this.buttonModificarHabitacion.Enabled = true;
                this.buttonBajaHabitacion.Enabled = true;
            }
        }

        private void buttonModificarHabitacion_Click(object sender, EventArgs e)
        {
            Habitacion habitacion = (Habitacion)registroHabitaciones.CurrentRow.DataBoundItem;
            using (ModificarHabitacion modificarHabitacion = new ModificarHabitacion(habitacion))
            {
                var resultFormModificarHabitacion = modificarHabitacion.ShowDialog();

                //AL CERRAR LA VENTANA DESPUES DE MODIFICAR UNA HABITACION VUELVO A CARGAR LA LISTA
                this.buttonBbuscarHoteles_Click(sender, e);
            }
        }

        private void limpiarBusquedaYResultados(object sender, EventArgs e) {
            this.limpiarBusquedaYResultados();
        }
        private void limpiarBusquedaYResultados()
        {
            registroHabitaciones.DataSource = null;
            textNumero.Text = "";
            textPiso.Text = "";
            comboBoxTipoHabitacion.SelectedValue = "";
            comboBoxTipoHabitacion.SelectedIndex = -1;
            comboBoxEstados.SelectedValue = "";
            this.buttonModificarHabitacion.Enabled = false;
            this.buttonBajaHabitacion.Enabled = false;
        }        
    }

}
