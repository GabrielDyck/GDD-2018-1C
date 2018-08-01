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
using FrbaHotel.AbmRol;
using FrbaHotel.AbmUsuario;
using FrbaHotel.AbmHotel;
using FrbaHotel.AbmCliente;
using FrbaHotel.AbmHabitacion;
using FrbaHotel.RegistrarEstadia;
using FrbaHotel.FacturarEstadia;
using FrbaHotel.RegistrarConsumible;
using FrbaHotel.AbmReserva;
using FrbaHotel.ListadoEstadistico;
using FrbaHotel.CambiarPassword;

namespace FrbaHotel
{
    public partial class FuncionesAdicionales : Form
    {
        Sesion sesion = null;

        public FuncionesAdicionales(Sesion sesion)
        {
            InitializeComponent();
            this.sesion = sesion;
        }

        private Sesion getSesion()
        {
            return this.sesion;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FuncionesAdicionales_Load(object sender, EventArgs e)
        {
            ABMRol.Enabled = false;
            ABMUsuario.Enabled = false;
            ABMCliente.Enabled = false;
            ABMHotel.Enabled = false;
            ABMHabitacion.Enabled = false;
            buttonGenerarReserva.Enabled = false;
            buttonModificarReserva.Enabled = false;

            RegistrarEstadia.Enabled = false;
            RegistrarConsumible.Enabled = false;
            FacturarEstadia.Enabled = false;
            GenerarListadoEstadistico.Enabled = false;
            RegistrarConsumible.Enabled = false;

            labelHotel.Text = "Hotel: " + this.getSesion().getHotel().getNombre();
            labelRol.Text = "Rol: " + this.getSesion().getRol().getNombre();

            this.HabilitarFuncionalidades(this.getSesion().getUsuario());
        }

        private void HabilitarFuncionalidades(Usuario usuarioLogueado)
        {
            List<Funcionalidad> funcionalidades = this.getSesion().getRol().getFuncionalidades();

            ABMRol.Enabled = funcionalidades.Exists(f => f.getDescripcion().Equals(ABMRol.Name));
            ABMUsuario.Enabled = funcionalidades.Exists(f => f.getDescripcion().Equals(ABMUsuario.Name));
            ABMCliente.Enabled = funcionalidades.Exists(f => f.getDescripcion().Equals(ABMCliente.Name));
            ABMHotel.Enabled = funcionalidades.Exists(f => f.getDescripcion().Equals(ABMHotel.Name));
            ABMHabitacion.Enabled = funcionalidades.Exists(f => f.getDescripcion().Equals(ABMHabitacion.Name));
            buttonGenerarReserva.Enabled = funcionalidades.Exists(f => f.getDescripcion().Equals("ABMReserva"));
            buttonModificarReserva.Enabled = funcionalidades.Exists(f => f.getDescripcion().Equals("ABMReserva"));
            RegistrarEstadia.Enabled = funcionalidades.Exists(f => f.getDescripcion().Equals(RegistrarEstadia.Name));
            RegistrarConsumible.Enabled = funcionalidades.Exists(f => f.getDescripcion().Equals(RegistrarConsumible.Name));
            FacturarEstadia.Enabled = funcionalidades.Exists(f => f.getDescripcion().Equals(FacturarEstadia.Name));
            GenerarListadoEstadistico.Enabled = funcionalidades.Exists(f => f.getDescripcion().Equals(GenerarListadoEstadistico.Name));
            RegistrarConsumible.Enabled = funcionalidades.Exists(f => f.getDescripcion().Equals(RegistrarConsumible.Name));
        }

        private void ABMRol_Click(object sender, EventArgs e)
        {
            using (ABMRoles formularioABMRoles = new ABMRoles())
            {
                var resultFormABMRoles = formularioABMRoles.ShowDialog();

                if (resultFormABMRoles == DialogResult.OK)
                {
                    //Hago algo con el return value
                }
            }
        }

        private void ABMUsuario_Click(object sender, EventArgs e)
        {
            using (ABMUsuarios formularioABMUsuarios = new ABMUsuarios(this.getSesion()))
            {
                var resultFormABMUsuarios = formularioABMUsuarios.ShowDialog();

                if (resultFormABMUsuarios == DialogResult.OK)
                {
                    //Hago algo con el return value
                }
            }
        }

        private void ABMCliente_Click(object sender, EventArgs e)
        {
            using (ABMClientes formularioABMClientes = new ABMClientes())
            {
                var resultFormABMClientes = formularioABMClientes.ShowDialog();

                if (resultFormABMClientes == DialogResult.OK)
                {
                    //Hago algo con el return value
                }
            }
        }

        private void ABMHotel_Click(object sender, EventArgs e)
        {
            using (SearchHotel formularioABMHotel = new SearchHotel(this.getSesion()))
            {
                var resultFormABMHotel = formularioABMHotel.ShowDialog();

                if (resultFormABMHotel == DialogResult.OK)
                {
                    //Hago algo con el return value
                }
            }
        }

        private void ABMHabitacion_Click(object sender, EventArgs e)
        {
            using (ABMHabitacion formularioABMHabitacion = new ABMHabitacion(this.getSesion()))
            {
                var resultFormABMHabitacion = formularioABMHabitacion.ShowDialog();

                if (resultFormABMHabitacion == DialogResult.OK)
                {
                    //Hago algo con el return value
                }
            }
        }
        private void RegistrarEstadia_Click(object sender, EventArgs e)
        {
            using (RegistrarEstadias formularioRegistrarEstadia = new RegistrarEstadias(this.getSesion()))
            {
                var resultFormRegistrarEstadia = formularioRegistrarEstadia.ShowDialog();

                if (resultFormRegistrarEstadia == DialogResult.OK)
                {
                    //Hago algo con el return value
                }
            }
        }
        private void FacturarEstadia_Click(object sender, EventArgs e)
        {
            using (FacturarEstadias formularioFacturarEstadia = new FacturarEstadias(sesion))
            {
                var resultFormFacturarEstadia = formularioFacturarEstadia.ShowDialog();

                if (resultFormFacturarEstadia == DialogResult.OK)
                {
                    //Hago algo con el return value
                }
            }
        }
        private void RegistrarConsumible_Click(object sender, EventArgs e)
        {
            using (Consumibles formularioRegistrarConsumible = new Consumibles(this.getSesion()))
            {
                var resultFormRegistrarConsumible = formularioRegistrarConsumible.ShowDialog();

                if (resultFormRegistrarConsumible == DialogResult.OK)
                {
                    //Hago algo con el return value
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

        private void buttonGenerarReserva_Click(object sender, EventArgs e)
        {
            using (GenerarReserva generarReserva = new GenerarReserva(sesion))
            {
                var resultFormLogin = generarReserva.ShowDialog();


            }
        }

        private void buttonModificarReserva_Click(object sender, EventArgs e)
        {
            using (EditarReserva modificarReserva = new EditarReserva(sesion))
            {
                var resultFormLogin = modificarReserva.ShowDialog();


            }
        }

        private void GenerarListadoEstadistico_Click(object sender, EventArgs e)
        {
            using (Listado listado= new Listado())
            {
                var resultFormLogin = listado.ShowDialog();


            }
        }

        private void cambiarPasswordButton_click(object sender, EventArgs e)
        {
            using (CambiarPasswordForm form= new CambiarPasswordForm(sesion.getUsuario()))
            {
                var resultFormLogin = form.ShowDialog();


            }
        }
    }
}
