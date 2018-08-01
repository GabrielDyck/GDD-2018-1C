
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using FrbaHotel.Repositorios;

namespace FrbaHotel.ListadoEstadistico
{
    public partial class Listado : Form
    {
        public Listado()
        {
            InitializeComponent();
            trimestre.Items.Add("1");
            trimestre.Items.Add("2");
            trimestre.Items.Add("3");
            trimestre.Items.Add("4");

            tipoListado.Items.Insert(0, "Hotel con mas reservas canceladas");
            tipoListado.Items.Insert(1, "Hotel con mas consumibles facturados");
            tipoListado.Items.Insert(2, "Hotel con mas dias fuera de servicio");
            tipoListado.Items.Insert(3, "Cliente con mas puntos");
            tipoListado.Items.Insert(4, "Habitaciones mas ocupadas");

        }

        private void button_buscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxAnio.Text))
            {
                MessageBox.Show("Debe ingresar un año.", "Listado estadistico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(trimestre.Text))
            {
                MessageBox.Show("Debe seleccionar un trimestre.", "Listado estadistico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(tipoListado.Text))
            {
                MessageBox.Show("Debe seleccionar el tipo de listado.","Listado estadistico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(tipoListado.Text))
            {
                MessageBox.Show("Debe seleccionar el tipo de listado.", "Listado estadistico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Int32.Parse(textBoxAnio.Text) < 1900)
            {
                MessageBox.Show("Debe elegir una fecha válida.", "Listado estadistico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            RepositorioListadoEstadistico repoListado = new RepositorioListadoEstadistico();
            string anioString = textBoxAnio.Text.PadLeft(4, '0');

            switch (tipoListado.SelectedIndex)
            {
                case 0:
                    dataGridView1.DataSource = repoListado.getHotelesMayorCantidadReservasCanceladas(trimestre.Text.Trim(), anioString.Trim());
                    break;
                case 1:
                    dataGridView1.DataSource = repoListado.hotelesMayorCantidadConsumiblesFacturados(trimestre.Text.Trim(), anioString.Trim());
                    break;
                case 2:
                    dataGridView1.DataSource = repoListado.hotelesMayorCantidadDiasFueraServicio(trimestre.Text.Trim(), anioString.Trim());
                    break;
                case 3:
                    dataGridView1.DataSource = repoListado.clientesConMasPuntos(trimestre.Text.Trim(), anioString.Trim());
                    break;
                case 4:
                    dataGridView1.DataSource = repoListado.habitacionesMasOcupadas(trimestre.Text.Trim(), anioString.Trim());
                    break;
            }

            dataGridView1.AutoResizeColumns();
        }

        private void button_cerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            this.trimestre.Text = "";
            this.textBoxAnio.Text = "";
            this.tipoListado.SelectedValue = "";
            this.tipoListado.SelectedIndex = -1;
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