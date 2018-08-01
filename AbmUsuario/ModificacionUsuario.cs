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
    public partial class ModificacionUsuario : Form
    {
        //USUARIO A MODIFICAR
        Usuario usuario = null;
        String password = "";
        Boolean passwordChanged = false;

        public ModificacionUsuario(Usuario usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.resetearDatos();
        }

        private void resetearDatos()
        {
            List<String> tipoDoc = new List<String>();
            tipoDoc.Add("DNI");
            tipoDoc.Add("CUIT");
            tipoDoc.Add("LE");
            tipoDoc.Add("LC");
            tipoDoc.Add("Pasaporte");

            comboBoxTipoDoc.ValueMember = "Value";
            comboBoxTipoDoc.DisplayMember = "Key";
            comboBoxTipoDoc.DataSource = tipoDoc;

            dateTime.ResetText();
            passwordChanged = false;

            //seteo la info
            textBoxUsername.Text = usuario.getUsername();
            textBoxNombre.Text = usuario.getIdentidad().getNombre();
            textBoxApellido.Text = usuario.getIdentidad().getApellido();
            textBoxNroDoc.Text = usuario.getIdentidad().getNumeroDocumento();
            textBoxMail.Text = usuario.getIdentidad().getMail();
            textBoxTelefono.Text = usuario.getIdentidad().getTelefono();
            textBoxCalle.Text = usuario.getIdentidad().getDireccion().getCalle();
            textBoxNroCalle.Text = usuario.getIdentidad().getDireccion().getNumeroCalle().ToString();
            textBoxPiso.Text = usuario.getIdentidad().getDireccion().getPiso().ToString();
            textBoxDepto.Text = usuario.getIdentidad().getDireccion().getDepartamento();
            textBoxLocalidad.Text = usuario.getIdentidad().getDireccion().getCiudad();
            textBoxPais.Text = usuario.getIdentidad().getDireccion().getPais();
            textBoxNacionalidad.Text = usuario.getIdentidad().getNacionalidad();
            dateTime.Value = usuario.getIdentidad().getFechaNacimiento();
            comboBoxTipoDoc.SelectedIndex = comboBoxTipoDoc.FindStringExact(usuario.getIdentidad().getTipoDocumento());
            checkBoxActivo.Checked = usuario.getActivo();

            //cargo roles
            RepositorioRol repositorioRol = new RepositorioRol();
            dataGridRoles.DataSource = repositorioRol.getAll().OrderBy(r => r.getNombre()).ToList();
            dataGridRoles.CurrentCell = null;
            dataGridRoles.AutoResizeColumns();
            dataGridRoles.ClearSelection();

            //cargo hoteles
            RepositorioHotel repositorioHotel = new RepositorioHotel();
            dataGridHoteles.DataSource = repositorioHotel.getAll().OrderBy(h => h.getNombre()).ToList();
            dataGridHoteles.CurrentCell = null;
            dataGridHoteles.AutoResizeColumns();
            dataGridHoteles.ClearSelection();

            //MARCO LOS ROLES QUE TIENE EL USUARIO
            foreach (DataGridViewRow row in dataGridRoles.Rows)
            {
                Rol rol = (Rol)row.DataBoundItem;
                if (usuario.getRoles().Exists(r => r.getIdRol().Equals(rol.getIdRol())))
                {
                    dataGridRoles.Rows[row.Index].Selected = true;
                    dataGridRoles.Rows[row.Index].Cells[0].Selected = true;
                }
            }

            //MARCO LOS HOTELES EN LOS QUE TRABAJA EL USUARIO
            foreach (DataGridViewRow row in dataGridHoteles.Rows)
            {
                Hotel hotel = (Hotel)row.DataBoundItem;
                //if (rol.getFuncionalidades().Exists(f => f.getDescripcion().Equals(funcionalidad.getDescripcion())))
                if (usuario.getHoteles().Exists(h => h.getIdHotel().Equals(hotel.getIdHotel())))
                {
                    dataGridHoteles.Rows[row.Index].Selected = true;
                    dataGridHoteles.Rows[row.Index].Cells[0].Selected = true;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //ACTUALIZAR EL USUARIO
            RepositorioUsuario repoUser = new RepositorioUsuario();

            //ESTOS VALORES SON LOS QUE CAMBIAN
            //STRINGS
            String username = textBoxUsername.Text.Trim();          
            String nombre = textBoxNombre.Text.Trim();
            String apellido = textBoxApellido.Text.Trim();
            String nroDoc = textBoxNroDoc.Text.Trim();
            String mail = textBoxMail.Text.Trim();
            String telefono = textBoxTelefono.Text.Trim();
            String calle = textBoxCalle.Text.Trim();
            String localidad = textBoxLocalidad.Text.Trim();
            String pais = textBoxPais.Text.Trim();
            String nacionalidad = textBoxNacionalidad.Text.Trim();
            String depto = textBoxDepto.Text.Trim();            
            
            //NUMEROS
            int nroCalle = 0;
            if (textBoxNroCalle.Text != "") { nroCalle = int.Parse(textBoxNroCalle.Text); }
            int nroPiso = 0; 
            if (textBoxPiso.Text != "") { nroPiso = int.Parse(textBoxPiso.Text); }

            //OTROS
            String tipoDoc = "";
            if (comboBoxTipoDoc.SelectedItem != null) { tipoDoc = (String)comboBoxTipoDoc.SelectedItem; }
            Boolean activo = checkBoxActivo.Checked;
            DateTime fechaNacimiento = dateTime.Value;

            //SI SE CAMBIA LA PASSWORD SE VUELVE A GENERAR
            //SINO SE MANTIENE LA MISMA
            if (this.password != null && this.password !="" && passwordChanged) { password = repoUser.EncriptarSHA256(this.password); } else { password = usuario.getPassword(); }

            //VALORES QUE NO CAMBIAN
            String tipoIdentidad = "Usuario";
            int idDir = usuario.getIdentidad().getDireccion().getIdDireccion();
            int idIdentidad = usuario.getIdentidad().getIdIdentidad();
            int idUsuario = usuario.getIdUsuario();
            int intentosFallidosLogin = usuario.getIntentosFallidosLogin();            
            
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

            Direccion address = new Direccion(idDir,pais, localidad, calle, nroCalle, nroPiso, depto);
            Identidad identidad =new Identidad(idIdentidad,tipoIdentidad,nombre, apellido,tipoDoc,nroDoc,mail,fechaNacimiento,nacionalidad,telefono, address);
            Usuario user = new Usuario(idUsuario, identidad, username,password,intentosFallidosLogin, activo,roles,hoteles);

            if (this.validoInput(this))
            {
                try
                {
                    repoUser.update(user);
                    MessageBox.Show("Usuario actualizado con éxito.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //ME TRAIGO EL USUARIO ACTUALIZADO
                    this.usuario = repoUser.getById(usuario.getIdUsuario());
                    this.resetearDatos();
                }
                //catch (NoExisteIDException exc)
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Verifique haber ingresado todos los datos necesarios para actualizar el usuario, incluyendo los roles que el usuario desempeña y los hoteles donde el usuario trabaja.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Boolean validoInput(ModificacionUsuario form)
        {
            return !form.textBoxUsername.Text.Equals("") &&
                   !this.password.Equals("") &&
                   !form.textBoxNombre.Text.Equals("") &&
                   !form.textBoxApellido.Text.Equals("") &&
                   !form.textBoxNroDoc.Text.Equals("") &&
                   !form.textBoxMail.Text.Equals("") &&
                   !form.textBoxTelefono.Text.Equals("") &&
                   !form.textBoxCalle.Text.Equals("") &&
                   !form.textBoxNroCalle.Text.Equals("") &&
                 //!form.textBoxPiso.Text.Equals("") && //PISO PUEDE ESTAR VACIO DEFAULT 0
                 //!form.textBox14.Text.Equals("") && //DEPTO PUEDE ESTAR VACIO DEFAULT ''
                   !form.textBoxLocalidad.Text.Equals("") &&
                   !form.textBoxPais.Text.Equals("") &&
                   !form.textBoxNacionalidad.Text.Equals("") &&
                   !form.dataGridRoles.SelectedRows.Count.Equals(0) &&
                   !form.dataGridHoteles.SelectedRows.Count.Equals(0) &&
                   form.comboBoxTipoDoc.SelectedValue != null;
        }

        private void ModificacionUsuario_Load(object sender, EventArgs e)
        {
            this.resetearDatos();
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            passwordChanged = true;
            this.password = textBoxPassword.Text.Trim();
        }
    }
}
