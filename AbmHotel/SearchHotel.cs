using System;
using System.Windows.Forms;
using System.Drawing;
using FrbaHotel.Repositorios;
using System.Collections.Generic;
using FrbaHotel.Modelo;
using System.Linq;

namespace FrbaHotel.AbmHotel
{
    public partial class SearchHotel : Form

    {
        private Sesion sesion = null;

        public SearchHotel(Sesion sesion)
        {
            InitializeComponent();

            this.sesion = sesion;
            RepositorioCategoria repoCategoria = new RepositorioCategoria();
            this.estrellasComboBox.DataSource = repoCategoria.getAll().OrderBy(c => c.getEstrellas()).ToList();
            this.estrellasComboBox.ValueMember = "Estrellas";
            limpiarBusquedaYResultados();
        }

        private void button_buscarHoteles(object sender, EventArgs e)
        {
            buscarHoteles();
        }

        private void buscarHoteles() {
            String nombre = validateStringFields(nombreText.Text.Trim());
            int? estrellas = null;
            if (estrellasComboBox.SelectedItem != null) { estrellas = ((Categoria)estrellasComboBox.SelectedItem).getEstrellas(); };
            String pais = validateStringFields(paisText.Text.Trim());
            String ciudad = validateStringFields(ciudadText.Text.Trim());
            RepositorioHotel repositorioHotel = new RepositorioHotel();

            this.modificarButton.Enabled = false;

            registroHoteles.DataSource = repositorioHotel.getByQuery(nombre, estrellas, ciudad, pais).OrderBy(h => h.getNombre()).ToList();
            registroHoteles.AutoResizeColumns();
            //ESTO LO TENGO QUE HACER PARA QUE NO APAREZCA SIEMPRE SELECCIONADO EL PRIMER ITEM
            registroHoteles.CurrentCell = null;
            registroHoteles.ClearSelection();
        }

        private String validateStringFields(String field)
        {
            return field == "" ? null : field;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.limpiarBusquedaYResultados();
        }

        private void limpiarBusquedaYResultados()
        {
            registroHoteles.DataSource = null;
            nombreText.Text = "";
            paisText.Text = "";
            ciudadText.Text = "";
            estrellasComboBox.SelectedValue = "";
            estrellasComboBox.SelectedIndex = -1;
            this.modificarButton.Enabled = false;
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

        private void registroHoteles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (dgv == null) return;
            if (dgv.CurrentRow.Selected)
            {
                this.modificarButton.Enabled = true;
            }
        }

        private void modificarButton_Click(object sender, EventArgs e)
        {
            Hotel hotelAModificar = (Hotel) registroHoteles.CurrentRow.DataBoundItem;

            //EL ENUNCIADO DICE QUE SI QUIERO REALIZAR ACCIONES SOBRE UN HOTEL TENGO QUE ELEGIRLO AL INICIAR SESION
            //ENTONCES SI EL HOTEL QUE QUIERO MODIFICAR ES EL MISMO QUE ELEGI AL INICIAR SESION PUEDO EDITARLO SINO NO
            //
            //el criterio anterior era que se puede editar un hotel, si el usuario logueado trabaja en ese hotel
            //if (this.sesion.getUsuario().getHoteles().Exists(hotel => hotel.getIdHotel().Equals(hotelAModificar.getIdHotel())))
            //
            if (this.sesion.getHotel().getIdHotel().Equals(hotelAModificar.getIdHotel()))
            {
                using (ModificacionHotel form = new ModificacionHotel(hotelAModificar))
                {
                    var result = form.ShowDialog();
                    this.buscarHoteles();
                }
            }
            else
            {
                //MessageBox.Show("Usuario sin permisos para modificar el Hotel seleccionado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Si desea editar este hotel debe elegirlo al iniciar sesión.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void altaButton_Click(object sender, EventArgs e)
        {
            using (CreateHotel form = new CreateHotel())
            {
                var result = form.ShowDialog();
                this.buscarHoteles();
            }
        }

    }

}
