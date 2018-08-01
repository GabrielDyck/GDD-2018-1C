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
    public partial class ModificacionCliente : Form
    {
        Cliente cliente = null;

        public ModificacionCliente(Cliente cliente)
        {
            InitializeComponent();
            this.cliente = cliente;
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
                        
            //obtengo todos los campos a mostrar
            textBoxNacionalidad.Text = cliente.getIdentidad().getNacionalidad();
            textBoxNombre.Text = cliente.getIdentidad().getNombre();
            textBoxApellido.Text = cliente.getIdentidad().getApellido();
            textBoxNroDoc.Text = cliente.getIdentidad().getNumeroDocumento();
            textBoxMail.Text = cliente.getIdentidad().getMail();
            textBoxTelefono.Text = cliente.getIdentidad().getTelefono();
            textBoxCalle.Text = cliente.getIdentidad().getDireccion().getCalle();
            textBoxLocalidad.Text = cliente.getIdentidad().getDireccion().getCiudad();
            textBoxPaisOrigen.Text = cliente.getIdentidad().getDireccion().getPais();
            textBoxNroCalle.Text = cliente.getIdentidad().getDireccion().getNumeroCalle().ToString(); ;
            textBoxPiso.Text = cliente.getIdentidad().getDireccion().getPiso().ToString();
            textBoxDepto.Text = cliente.getIdentidad().getDireccion().getDepartamento();

            comboBoxTipoDoc.SelectedIndex = comboBoxTipoDoc.FindStringExact(cliente.getIdentidad().getTipoDocumento());
            dateTime.Value = cliente.getIdentidad().getFechaNacimiento();
            checkBoxActivo.Checked = cliente.getActivo();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            this.resetearDatos();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBoxMail.Text.Trim().Equals("INCONSISTENTE"))
            {
                MessageBox.Show("Corrija el tipo y numero de documento y el Mail.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            //ACTUALIZAR EL CLIENTE
            RepositorioCliente repoCliente = new RepositorioCliente();

            //ESTOS VALORES SON LOS QUE CAMBIAN
            //STRINGS
            String nombre = textBoxNombre.Text.Trim();
            String apellido = textBoxApellido.Text.Trim();
            String nroDoc = textBoxNroDoc.Text.Trim();
            String mail = textBoxMail.Text.Trim();
            String telefono = textBoxTelefono.Text.Trim();
            String calle = textBoxCalle.Text.Trim();
            String localidad = textBoxLocalidad.Text.Trim();
            String pais = textBoxPaisOrigen.Text.Trim();
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

            //VALORES QUE NO CAMBIAN
            String tipoIdentidad = "Cliente";
            int idDir = this.cliente.getIdentidad().getDireccion().getIdDireccion();
            int idIdentidad = this.cliente.getIdentidad().getIdIdentidad();
            int idCliente = this.cliente.getIdCliente();
            List<Reserva> reservas = this.cliente.getReservas();
            Boolean inconsistente = this.cliente.getInconsistente();

            Direccion adress = new Direccion(idDir, pais, localidad, calle, nroCalle, nroPiso, depto);
            Identidad identidad = new Identidad(idIdentidad, tipoIdentidad, nombre, apellido, tipoDoc, nroDoc, mail, fechaNacimiento, nacionalidad, telefono, adress);
            Cliente updatedClient = new Cliente(idCliente, identidad, activo, reservas, inconsistente);            

            if (this.validoInput(this))
            {
                try
                {
                   //SI EL CLIENTE ORIGINAL ESTABA INCONSISTENTE
                    if (cliente.getInconsistente())
                    {
                        repoCliente.limpioInconsistencia(updatedClient);
                        //LUEGO ACTUALIZO EL CLIENTE
                        repoCliente.updateAutoritativo(updatedClient);
                    }
                    else
                    {
                            //SI ES UN UPDATE NORMAL
                            repoCliente.update(updatedClient);
                    }

                    MessageBox.Show("Cliente actualizado con éxito", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //ME TRAIGO EL USUARIO ACTUALIZADO
                    this.cliente = repoCliente.getById(cliente.getIdCliente());
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
                MessageBox.Show("Verifique haber ingresado todos los datos necesarios para actualizar el Cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Boolean validoInput(ModificacionCliente form)
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

        private void ModificacionCliente_Load(object sender, EventArgs e)
        {
            this.resetearDatos();

            if (this.cliente.getInconsistente())
            {
                MessageBox.Show("El cliente aparece como inconsistente en la base, por favor verifique el numero de documento y mail del cliente y actualice la informacion. El usuario dejará de estar marcado como inconsistente si lo actualiza ya que la información que se introduce se considera como autoritativa.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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

