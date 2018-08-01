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

namespace FrbaHotel.AbmHabitacion
{
    public partial class ModificarHabitacion : Form
    {
        private Habitacion habitacion;

        public ModificarHabitacion(Habitacion habitacion)
        {
            this.habitacion = habitacion;
            InitializeComponent();

            RepositorioHabitacion repoHabitacion = new RepositorioHabitacion();
            comboBoxUbicacion.DataSource = repoHabitacion.getAllUbicaciones();
            initFields();
        }

        private void initFields()
        {
            RepositorioTipoHabitacion repositorioTipoHab = new RepositorioTipoHabitacion();
            comboBoxTipoHabitacion.DataSource = repositorioTipoHab.getAll();
            comboBoxTipoHabitacion.DisplayMember = "Descripcion";
            comboBoxTipoHabitacion.SelectedIndex = comboBoxTipoHabitacion.FindStringExact(habitacion.getTipoHabitacion().getDescripcion());
            
            this.textNumero.Text = habitacion.getNumero().ToString();
            this.checkBoxActiva.Checked = habitacion.getActiva();
            this.textDescripcion.Text = habitacion.getDescripcion();
            this.textPiso.Text = habitacion.getPiso().ToString();
            this.comboBoxUbicacion.SelectedItem = habitacion.getUbicacion();
            this.Text = "Modificar Habitación: " + habitacion.getNumero().ToString() + ", Hotel: " + habitacion.getHotel().getNombre();
        }

        private void buttonModificarHabitacion_Click(object sender, EventArgs e)
        {
            RepositorioHabitacion repoHabitacion = new RepositorioHabitacion();

            TipoHabitacion tipoHabitacion = (TipoHabitacion)Utils.validateFields(comboBoxTipoHabitacion.SelectedItem, "Tipo");
            bool activa = this.checkBoxActiva.Checked;
            int numero = Utils.validateIntField(textNumero.Text.Trim(), "Numero");
            int piso = Utils.validateIntField(textPiso.Text.Trim(), "Piso");
            String ubicacion = Utils.validateStringFields((String)comboBoxUbicacion.SelectedItem, "Ubicacion");
            String descripcion = textDescripcion.Text.Trim();
            Habitacion habitacionAModificar = new Habitacion(this.habitacion.getIdHabitacion(), activa, numero, piso, ubicacion, descripcion);
            habitacionAModificar.setHotel(this.habitacion.getHotel());
            habitacionAModificar.setTipoHabitacion(tipoHabitacion);

            try
            {
                if (repoHabitacion.yaExisteHabitacionMismoNumero(habitacionAModificar))
                {
                    MessageBox.Show("Ya existe una habitacion con ese numero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                repoHabitacion.update(habitacionAModificar);

                MessageBox.Show("Habitacion modificada.", "Gestion de Datos TP 2018 1C - LOS_BORBOTONES", MessageBoxButtons.OK, MessageBoxIcon.Information);
                habitacion = habitacionAModificar;

                this.initFields();
            }
            //catch (RequestInvalidoException exception1)
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "Gestion de Datos TP 2018 1C - LOS_BORBOTONES", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
