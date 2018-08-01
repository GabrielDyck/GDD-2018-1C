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

namespace FrbaHotel.FacturarEstadia
{
    public partial class FacturarEstadias : Form
    {
        private Sesion sesion = null;
        List<Estadia> estadias = new List<Estadia>();
        List<ConsumibleParaMostrar> consumiblesXEstadia = new List<ConsumibleParaMostrar>();
        private RepositorioConsumibles repoConsumibles = new RepositorioConsumibles();

        public FacturarEstadias(Sesion sesion)
        {
            InitializeComponent();
            this.sesion = sesion;
        }
        private void ListadoFacturarEstadia_Load(object sender, EventArgs e)
        {
            textIdEstadia.Text = "";
            textTitularTarjeta.Text = "";
            textNumeroTarjeta.Text = "";
            textCodigoSeguridad.Text = "";
            textBox5.Text = "";
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;

            comboBoxTipoPago.SelectedValue = "";

            List<String> tipoPago = new List<String>();
            tipoPago.Add("Efectivo");
            tipoPago.Add("VISA Credito");
            tipoPago.Add("American Express Credito");
            tipoPago.Add("Mastercard Credito");

            comboBoxTipoPago.ValueMember = "Value";
            comboBoxTipoPago.DisplayMember = "Key";
            comboBoxTipoPago.DataSource = tipoPago;
            comboBoxTipoPago.SelectedValue = "";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.ListadoFacturarEstadia_Load(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //traigo la estadia con sus items de factura si la estadia ya tiene checkout hecho.
            int idEstadia = 0;
            RepositorioEstadia repositorioEstadia = new RepositorioEstadia();
            RepositorioEstadoReserva repoEstadoReserva =new RepositorioEstadoReserva();

            Estadia estadia = null;
            EstadoReserva estadoReserva=null;

            if (textIdEstadia.Text != "")
            {
                idEstadia = int.Parse(textIdEstadia.Text);

                estadia = repositorioEstadia.getById(idEstadia);
                RepositorioReserva repoReserva = new RepositorioReserva();
                Reserva reserva = repoReserva.getIdByIdEstadia(idEstadia);
                //buscar por estado reserva que este con check out ya realizado
                estadoReserva = repoEstadoReserva.getByIdEstadia(idEstadia);
                if(estadoReserva==null){
                    MessageBox.Show("La estadia ingresada no existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if ( (!estadoReserva.getTipoEstado().Equals("RCE") && !estadoReserva.getTipoEstado().Equals("RCCR")) | estadia.getFacturada() == true | reserva.getHotel().getIdHotel() != this.sesion.getHotel().getIdHotel())
             {
                 if(estadoReserva.getTipoEstado().Equals("RCI"))
                     MessageBox.Show("Todavia no se realizo el checkout de la estadia ingresada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 else if (estadoReserva.getTipoEstado().Equals("RF"))
                     MessageBox.Show("La estadia ya fue facturada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 else if (estadia.getFacturada() == true)
                     MessageBox.Show("La estadia ingresada ya fue facturada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 else if (reserva.getHotel().getIdHotel() != this.sesion.getHotel().getIdHotel())
                     MessageBox.Show("La estadia ingresada no corresponde al hotel al cual el usuario esta logueado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 else
                     MessageBox.Show("La estadia ingresada no esta en estado para facturarse", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 
             }else
             {
                //lleno los datos de la estadia aca se puede ver la cantidad de noches que de verdad se alojo.
                 //List<Estadia> estadias = new List<Estadia>();
                estadias.Add(estadia);
                dataGridView1.DataSource = estadias;
                dataGridView1.AutoResizeColumns();
                dataGridView1.ClearSelection();
                //lleno los consumibles por estadia en el datagrid2
                //List<Consumible> consumiblesXEstadia = new List<Consumible>();
                //repositorioEstadia.getConsumiblesXIdEstadia(estadia.getIdEstadia()).ForEach(i => consumiblesXEstadia.Add(new ConsumibleParaMostrar(i, );
                consumiblesXEstadia = repoConsumibles.getByQuery(estadia.getIdEstadia());
                dataGridView2.DataSource = consumiblesXEstadia;
                dataGridView2.AutoResizeColumns();
                dataGridView2.ClearSelection();                
             }
            }else
            {
                MessageBox.Show("Por favor ingresar ID de estadia.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //FACTURAR ESTADIA
            RepositorioFactura repoFact = new RepositorioFactura();
            int idFactura = 0;
            String tipoPago = "";
            String nombreTarjeta = "";
            int vencTarjeta = 0;
            Decimal nroTarjeta = 0;
            int codSegTarjeta = 0;

            if (comboBoxTipoPago.SelectedItem != null)
            {
                tipoPago = (String)comboBoxTipoPago.SelectedItem;
            }
            if(tipoPago.Equals("Efectivo"))
            {
                //es efectivo llamo aca no necesito los datos de tarjeta
                //tomo la informacion de la estadia, consumibles por estadia y datos de pago
                idFactura = repoFact.facturar(estadias, consumiblesXEstadia, tipoPago,nombreTarjeta,nroTarjeta,codSegTarjeta,vencTarjeta);
                if (idFactura == 0)
                    MessageBox.Show("Error cargando item de factura.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (idFactura == 2)
                    MessageBox.Show("Error cargando la factura.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (idFactura == 1)
                    MessageBox.Show("Estadia facturada correctamente. Factura Numero: " + repoFact.getLastNumeroFactura() + ".", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ListadoFacturarEstadia_Load(sender, e);    
                
            }else
            {
                //traigo los datos de la tarjeta
                nombreTarjeta = textTitularTarjeta.Text.Trim();
                if (textNumeroTarjeta.Text != "" )
                {
                    nroTarjeta = Decimal.Parse(textNumeroTarjeta.Text.Trim());
                }
                if (textCodigoSeguridad.Text != "")
                {
                    codSegTarjeta = int.Parse(textCodigoSeguridad.Text.Trim());
                }
                if (textBox5.Text != "")
                {
                    vencTarjeta = int.Parse(textBox5.Text.Trim());
                }
                if (vencTarjeta==0 |codSegTarjeta==0| nroTarjeta==0 | nombreTarjeta=="")
                {
                    MessageBox.Show("Por favor completar todos los campos de informacion de tarjeta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //this.ListadoFacturarEstadia_Load(sender, e);    
                }else
                {
                    //tomo la informacion de la estadia, consumibles por estadia y datos de pago
                    idFactura= repoFact.facturar(estadias, consumiblesXEstadia, tipoPago,nombreTarjeta,nroTarjeta,codSegTarjeta,vencTarjeta);
                    if (idFactura==0)
                        MessageBox.Show("Error cargando item de factura.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (idFactura == 2)
                        MessageBox.Show("Error cargando la factura.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (idFactura == 1)
                    {
                        MessageBox.Show("Estadia facturada correctamente. Factura Numero: " + repoFact.getLastNumeroFactura() + ".", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    this.ListadoFacturarEstadia_Load(sender, e);    
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

