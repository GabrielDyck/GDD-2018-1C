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

namespace FrbaHotel.CambiarPassword
{
    public partial class CambiarPasswordForm : Form
    {

        private Usuario usuario;
        public CambiarPasswordForm(Usuario usuario)
        {
            this.usuario = usuario;
            InitializeComponent();
        }

        private void cambiarPassword_click(object sender, EventArgs e)
        {
            RepositorioUsuario repoUsuario = new RepositorioUsuario();
            try
            {
                String nuevaContraseña = Utils.validateStringFields(textNuevaContraseña.Text, "Contraseña");
                usuario.setPassword(nuevaContraseña);
                repoUsuario.changePassword(usuario);
                MessageBox.Show("Contraseña cambiada con exito", "Cambiar Password");


            }
            catch (RequestInvalidoException exception) {
                MessageBox.Show(exception.Message, "Cambiar Password");

            }
        }
    }
}
