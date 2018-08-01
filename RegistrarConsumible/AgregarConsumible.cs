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

namespace FrbaHotel.RegistrarConsumible
{
    public partial class AgregarConsumible : Form
    {
        int idEstadia = 0;

        public AgregarConsumible(int estadia)
        {
            InitializeComponent();
            this.idEstadia = estadia;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.AltaConsumible_Load(sender, e);
        }

        private void AltaConsumible_Load(object sender, EventArgs e)
        {
            RepositorioConsumibles repositorioConsumible = new RepositorioConsumibles();
            //FILTRO LOS CONSUMIBLES QUE USO INTERNAMENTE
            dataGridView1.DataSource = repositorioConsumible.getAll().FindAll(cm => !cm.getCodigo().Equals(1) && !cm.getCodigo().Equals(2)).OrderBy(c => c.getDescripcion()).ToList();
            dataGridView1.AutoResizeColumns();
            dataGridView1.ClearSelection();
            textBox1.Text = "";
            botonGuardar.Enabled = false;
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Consumible consumible = null;
            //int idConsumible = 0;

            //traigo el consumible elegido (solo dejo traer uno)
            foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
            {
               consumible = item.DataBoundItem as Consumible;
            }

            int cantidad;

            if (textBox1.Text.Trim().Equals(""))
            {
                cantidad = 1;
            }
            else
            {
                cantidad = int.Parse(textBox1.Text.Trim());
            }

            RepositorioConsumibles repoConsumible = new RepositorioConsumibles();

            try
            {
                repoConsumible.asociarConsumibleConEstadia(consumible, cantidad, idEstadia);
                MessageBox.Show("Consumible(s) registrado(s) correctamente en la estadia.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.AltaConsumible_Load(sender, e);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }           

            /*
            RepositorioConsumibles repoConsumible = new RepositorioConsumibles();

                 //sumar el consumible elegido a la estadia marcada
                 //consumible.setIdEstadia(idEstadia);
                 //metodo registrar que no crea el consumible solo lo suma
                 //idConsumible = repoConsumible.registrar(consumible);
                    if (idConsumible == consumible.getIdConsumible())
                    {
                        MessageBox.Show("Consumible Registrado correctamente en la estadia.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.AltaConsumible_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Error al registrar el consumible.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.AltaConsumible_Load(sender, e);
                    }
            */
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (dgv == null) return;
            if (dgv.CurrentRow.Selected)
            {
                this.botonGuardar.Enabled = true;
            }
        }
    }
}

