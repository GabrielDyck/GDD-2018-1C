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
using FrbaHotel.Excepciones;

namespace FrbaHotel.AbmUsuario
{
    public partial class AltaUsuario : Form
    {
        public AltaUsuario()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.limpiarPantalla();
        }

        private void limpiarPantalla()
        {
            //vacio todos los campos porque es el limpiar
            textBoxUsername.Text = "";
            textBoxPassword.Text = "";
            textBoxNombre.Text = "";
            textBoxApellido.Text = "";
            textBoxNroDoc.Text = "";
            textBoxMail.Text = "";
            textBoxTelefono.Text = "";
            textBoxCalle.Text = "";
            textBoxNroCalle.Text = "";
            textBoxPiso.Text = "";
            textBoxDepto.Text = "";
            textBoxLocalidad.Text = "";
            textBoxPaisOrigen.Text = "";
            textBoxNacionalidad.Text = "";

            //cargo rol
            RepositorioRol repositorioRol = new RepositorioRol();
            dataGridRoles.DataSource = repositorioRol.getAll().OrderBy(r => r.getNombre()).ToList();
            dataGridRoles.AutoResizeColumns();
            dataGridRoles.CurrentCell = null;
            dataGridRoles.ClearSelection();

            //cargo hotel
            RepositorioHotel repositorioHotel = new RepositorioHotel();
            dataGridHoteles.DataSource = repositorioHotel.getAll().OrderBy(h => h.getNombre()).ToList();
            dataGridHoteles.AutoResizeColumns();
            dataGridHoteles.CurrentCell = null;
            dataGridHoteles.ClearSelection();

            comboBoxTipoDoc.SelectedValue = "";
            dateTime.ResetText();

            List<String> tipoDoc = new List<String>();
            tipoDoc.Add("DNI");
            tipoDoc.Add("CUIT");
            tipoDoc.Add("LE");
            tipoDoc.Add("LC");
            tipoDoc.Add("Pasaporte");

            comboBoxTipoDoc.ValueMember = "Value";
            comboBoxTipoDoc.DisplayMember = "Key";
            comboBoxTipoDoc.DataSource = tipoDoc;
            comboBoxTipoDoc.SelectedValue = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //GENERAR ALTA (no pido el estado ya que va como activo al momento de crearlo)

            //traigo los valores
            String username = textBoxUsername.Text.Trim();
            String password = textBoxPassword.Text.Trim();
            String nombre = textBoxNombre.Text.Trim();
            String apellido = textBoxApellido.Text.Trim();
            String nroDoc = textBoxNroDoc.Text.Trim();
            String mail = textBoxMail.Text.Trim();
            String telefono = textBoxTelefono.Text.Trim();
            String calle = textBoxCalle.Text.Trim();
            String localidad = textBoxLocalidad.Text.Trim();
            String pais = textBoxPaisOrigen.Text.Trim();
            String nacionalidad = textBoxNacionalidad.Text.Trim();
            DateTime fechaNacimiento = dateTime.Value;
            int nroCalle = 0;
            if (textBoxNroCalle.Text != "")
            {
                 nroCalle = int.Parse(textBoxNroCalle.Text);
            }
            int nroPiso = 0;
            if (textBoxPiso.Text != "")
            {
                nroPiso = int.Parse(textBoxPiso.Text);
            }

            String depto = textBoxDepto.Text.Trim();
            String tipoDoc = "";
            String tipoIdentidad = "Usuario";
            int idDir = 0;
            int idIdentidad = 0;
            int idUsuario = 0;
            int intentosFallidosLogin = 0;
            Boolean activo = true;
            if (comboBoxTipoDoc.SelectedItem != null)
            {
                tipoDoc = (String)comboBoxTipoDoc.SelectedItem;
            }
            //traigo los roles elegidos
            List<Rol> roles = new List<Rol>();
            foreach (DataGridViewRow item in this.dataGridRoles.SelectedRows)
                {
                    roles.Add(item.DataBoundItem as Rol);
                }
            //traigo los hoteles elegidos
            List<Hotel> hoteles = new List<Hotel>();
            foreach (DataGridViewRow item in this.dataGridHoteles.SelectedRows)
            {
                hoteles.Add(item.DataBoundItem as Hotel);
            }

            //armo direccion (id en 0)
            Direccion adress = new Direccion(idDir,pais, localidad,
            calle, nroCalle, nroPiso, depto);
            //armo la identidad con la direccion(id en 0)
            Identidad identidad =new Identidad(idIdentidad,tipoIdentidad,nombre, apellido,tipoDoc,nroDoc,
            mail,fechaNacimiento,nacionalidad,telefono, adress);
            // armo el usuario con la identidad (id en 0)
            Usuario user = new Usuario(idUsuario, identidad, username,password,intentosFallidosLogin, activo,roles,hoteles);
            //ahora si ya lo puedo crear
            RepositorioUsuario repoUser = new RepositorioUsuario();

            if (this.validoInput(this))
            {
                try
                {
                    repoUser.create(user);
                    MessageBox.Show("Usuario creado con éxito.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Verifique haber ingresado todos los datos necesarios para crear el usuario, incluyendo los roles que el usuario desempeña y los hoteles donde el usuario trabaja.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Boolean validoInput(AltaUsuario form)
        {
            return !form.textBoxUsername.Text.Equals("") &&
                   !form.textBoxPassword.Text.Equals("") &&
                   !form.textBoxNombre.Text.Equals("") &&
                   !form.textBoxApellido.Text.Equals("") &&
                   !form.textBoxNroDoc.Text.Equals("") &&
                   !form.textBoxMail.Text.Equals("") &&
                   !form.textBoxTelefono.Text.Equals("") &&
                   !form.textBoxCalle.Text.Equals("") &&
                   !form.textBoxNroCalle.Text.Equals("") &&
                   //!form.textBoxPiso.Text.Equals("") && //PISO PUEDE ESTAR VACIO DEFAULT 0
                   //!form.textBoxDepto.Text.Equals("") && //DEPTO PUEDE ESTAR VACIO DEFAULT ''
                   !form.textBoxLocalidad.Text.Equals("") &&
                   !form.textBoxPaisOrigen.Text.Equals("") &&
                   !form.textBoxNacionalidad.Text.Equals("") &&
                   !form.dataGridRoles.SelectedRows.Count.Equals(0) &&
                   !form.dataGridHoteles.SelectedRows.Count.Equals(0) &&
                   form.comboBoxTipoDoc.SelectedValue != null;
        } 

        private void AltaUsuario_Load(object sender, EventArgs e)
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

        //ESTO LO PONGO PARA QUE EL NUMERO DE CALLE SOLO PUEDA SER UN NUMERO
        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        //ESTO LO PONGO PARA QUE EL PISO DEL DEPTO SOLO PUEDA SER UN NUMERO
        private void textBox13_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        //ESTO LO PONGO PARA QUE EL DOCUMENTO SOLO PUEDA TENER NUMEROS
        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        //ESTO LO PONGO PARA QUE EL TELEFONO SOLO PUEDA TENER NUMEROS
        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

    }
}
