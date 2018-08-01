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

namespace FrbaHotel.Login
{
    public partial class SeleccionRolYHotel : Form
    {
        private Sesion sesion = null;
        private Usuario usuario = null;

        public SeleccionRolYHotel(Usuario usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
        }

        public Sesion getSesion()
        {
            return sesion;
        }

        private void SeleccionRolYHotel_Load(object sender, EventArgs e)
        {
            //cargo únicamente los roles del usuario que están habilitados 
            List<Rol> rolesHabilitados = usuario.getRoles().FindAll(rol => rol.getActivo()).OrderBy(r => r.getNombre()).ToList();
            dataGridRoles.DataSource = rolesHabilitados;
            dataGridRoles.AutoResizeColumns();
            dataGridRoles.CurrentCell = null;
            dataGridRoles.ClearSelection();

            //cargo los hoteles del usuario
            dataGridHoteles.DataSource = usuario.getHoteles().OrderBy(h => h.getNombre()).ToList();
            dataGridHoteles.AutoResizeColumns();
            dataGridHoteles.CurrentCell = null;
            dataGridHoteles.ClearSelection();

            //CON ESTO ME ASEGURO PASAR DIRECTO COMO PIDE EL ENUNCIADO
            if (rolesHabilitados.Count == 1 && 
                usuario.getHoteles().Count == 1)
            {
                dataGridRoles.Rows[0].Selected = true;
                dataGridHoteles.Rows[0].Selected = true;

                this.seleccionoRolHotelYCierro();
            }
            else
            {
                //SI ES UN SOLO ROL ELIJO EL PRIMERO
                if (rolesHabilitados.Count == 1)
                {
                    dataGridRoles.Rows[0].Selected = true;
                }

                //SI ES UN SOLO HOTEL ELIJO EL PRIMERO
                if (usuario.getHoteles().Count == 1)
                {
                    dataGridHoteles.Rows[0].Selected = true;
                }
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

        private void buttonOK_Click(object sender, EventArgs e)
        {
            //SI NO ELIGE UN ROL Y UN HOTEL
            if (!this.dataGridRoles.SelectedRows.Count.Equals(1) || 
                !this.dataGridHoteles.SelectedRows.Count.Equals(1))
            {
                MessageBox.Show("Debe elegir un rol y un hotel antes de continuar.", "Error Seleccionando Hotel y Rol", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //SI ELIGE OK
            else 
            {
                this.seleccionoRolHotelYCierro();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void seleccionoRolHotelYCierro()
        {
            Hotel hotelSeleccionado = null;
            Rol rolSeleccionado = null;

            foreach (DataGridViewRow item in this.dataGridHoteles.SelectedRows)
            {
                hotelSeleccionado = item.DataBoundItem as Hotel;
            }

            foreach (DataGridViewRow item in this.dataGridRoles.SelectedRows)
            {
                rolSeleccionado = item.DataBoundItem as Rol;
            }

            this.sesion = new Sesion(this.usuario, hotelSeleccionado, rolSeleccionado);

            this.DialogResult = DialogResult.OK;
        }
    }
}
