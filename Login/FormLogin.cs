using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using FrbaHotel.Repositorios;
using FrbaHotel.Excepciones;
using FrbaHotel.Modelo;

namespace FrbaHotel.Login
{
    public partial class FormLogin : Form
    {
        private Usuario usuarioLogueado = null;

        public FormLogin()
        {
            InitializeComponent();
        }

        private void BotonLoginClick(object sender, EventArgs e)
        {
            RepositorioUsuario repoUsuario = new RepositorioUsuario();

            try
            {
                //CREDENCIALES CORRECTAS
                usuarioLogueado = repoUsuario.AutenticarUsuario(txtUsername.Text, txtPassword.Text);
                this.DialogResult = DialogResult.OK;
            }
            catch (ErrorDeAutenticacionException exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (UsuarioBloqueadoException exc1)
            {
                MessageBox.Show(exc1.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exc2)
            {
                MessageBox.Show(exc2.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public Usuario getUsuarioLogueado()
        {
            return this.usuarioLogueado;
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

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            //GUEST NO LOGIN
            loginButton.Enabled = !txtUsername.Text.ToUpper().Equals("GUEST");
        }
    }
}
