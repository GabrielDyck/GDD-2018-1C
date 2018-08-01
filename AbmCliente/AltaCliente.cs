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

namespace FrbaHotel.AbmCliente
{
    public partial class AltaCliente : Form
    {
        public AltaCliente()
        {
            InitializeComponent();
        }

        private void limpiarPantalla()
        {
            //vacio todos los campos porque es el limpiar
            textBoxNacionalidad.Text = "";
            textBoxNombre.Text = "";
            textBoxApellido.Text = "";
            textBoxNroDoc.Text = "";
            textBoxMail.Text = "";
            textBoxTelefono.Text = "";
            textBoxCalle.Text = "";
            textBoxLocalidad.Text = "";
            textBoxPaisOrigen.Text = "";
            textBoxNroCalle.Text = "";
            textBoxPiso.Text = "";
            textBoxDepto.Text = "";

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
        
        private void button1_Click(object sender, EventArgs e)
        {
            this.limpiarPantalla();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //GENERAR ALTA (no pido el estado ya que va como activo al momento de crearlo)

            //traigo los valores
            String nacionalidad = textBoxNacionalidad.Text.Trim();
            String nombre = textBoxNombre.Text.Trim();
            String apellido = textBoxApellido.Text.Trim();
            String nroDoc = textBoxNroDoc.Text.Trim();
            String mail = textBoxMail.Text.Trim();
            String telefono = textBoxTelefono.Text.Trim();
            String calle = textBoxCalle.Text.Trim();
            String localidad = textBoxLocalidad.Text.Trim();
            String paisOrigen = textBoxPaisOrigen.Text.Trim();
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
            String tipoIdentidad = "Cliente";
            int idDir = 0;
            int idIdentidad = 0;
            int idCliente = 0;
            Boolean activo = true;
            List<Reserva> reservas = new List<Reserva>();
            if (comboBoxTipoDoc.SelectedItem != null)
            {
                tipoDoc = (String)comboBoxTipoDoc.SelectedItem;
            }
            //armo direccion (id en 0)
            Direccion adress = new Direccion(idDir, paisOrigen, localidad,
            calle, nroCalle, nroPiso, depto);
            //armo la identidad con la direccion(id en 0)
            Identidad identidad = new Identidad(idIdentidad, tipoIdentidad, nombre, apellido, tipoDoc, nroDoc,
            mail, fechaNacimiento, nacionalidad, telefono, adress);
            // armo el cliente con la identidad (id en 0)
            Cliente cliente = new Cliente(idCliente,identidad, activo,reservas, false);
            //ahora si ya lo puedo crear
            RepositorioCliente repoCliente = new RepositorioCliente();

            if (this.validoInput(this))
            {
                try
                {
                    repoCliente.create(cliente);
                    MessageBox.Show("Cliente creado con éxito.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Verifique haber ingresado todos los datos necesarios para crear el Cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Boolean validoInput(AltaCliente form)
        {
            return !form.textBoxNombre.Text.Equals("") &&
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
                   form.comboBoxTipoDoc.SelectedValue != null;
        } 

        private void AltaCliente_Load(object sender, EventArgs e)
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

        private void onlyNumeric(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}

