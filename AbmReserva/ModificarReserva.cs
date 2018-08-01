using FrbaHotel.Commons;
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

namespace FrbaHotel.AbmReserva
{
    public partial class ModificarReserva : Form
    {

        private Reserva reserva;
        private Usuario usuario;
        private Sesion sesion;

        private ModificarReserva(Reserva reserva, Sesion sesion) : this(reserva, sesion.getUsuario())
        {
            this.sesion = sesion;
        }
        public ModificarReserva(Reserva reserva, Usuario usuario)
        {
            this.reserva = reserva;
            this.usuario = usuario;
            InitializeComponent();

            buildReservaActual(reserva);
         
            init();
        }

        private void buildReservaActual(Reserva reserva)
        {
            List<Habitacion> habitaciones = reserva.getHabitaciones();
            Regimen regimen = reserva.getRegimen();
            this.labelHotelActual.Text = "";
            this.labelRegimenActual.Text = "";
            this.labelFechaDesde.Text = "";
            this.labelFechaHasta.Text = "";

            this.labelHotelActual.Text += " " + reserva.getHotel().getNombre();
            this.labelRegimenActual.Text += " " + regimen.getDescripcion();
            this.labelFechaDesde.Text += " " + reserva.getFechaDesde();
            this.labelFechaHasta.Text += " " + reserva.getFechaHasta();
            this.buttonModificarReserva.Enabled = false;
            this.dataGridView1.DataSource = buildHabitacionesReservadas(habitaciones, regimen).OrderBy(hd => hd.getNumeroHabitacion()).ToList();
            this.dataGridView1.AutoResizeColumns();
        }


        private List<HabitacionDisponible> buildHabitacionesReservadas(List<Habitacion> habitaciones,Regimen regimen){
            List<HabitacionDisponible> habitacionesReservadas = new List<HabitacionDisponible>();
            foreach(Habitacion hab in habitaciones){
            habitacionesReservadas.Add(new HabitacionDisponible(hab,regimen));
            }
            return habitacionesReservadas;
        }



        private void limpiarFiltros()
        {
            calendarioDesde.Value = Utils.getSystemDatetimeNow().Date;
            calendarioHasta.Value = Utils.getSystemDatetimeNow().Date.AddDays(1);

            init();
            limpiarGrids();


            this.buttonModificarReserva.Enabled = false;

        }

        private void limpiarGrids()
        {
            this.regimenesDisponiblesGrid.DataSource = null;
            this.regimenesDisponiblesGrid.Rows.Clear();

            this.habitacionesDisponiblesGrid.DataSource = null;
            this.habitacionesDisponiblesGrid.Rows.Clear();
        }

        private void init()
        {
            calendarioDesde.Value = Utils.getSystemDatetimeNow().Date;
            calendarioHasta.Value = Utils.getSystemDatetimeNow().Date.AddDays(1);
            RepositorioTipoHabitacion repoTipoHabitacion = new RepositorioTipoHabitacion();
            RepositorioHotel repoHotel = new RepositorioHotel();

            comboBoxTipoHabitacion.DisplayMember = "Descripcion";
            comboBoxTipoHabitacion.ValueMember = "Descripcion";
            comboBoxTipoHabitacion.DataSource = repoTipoHabitacion.getAll();

            comboBoxHotel.DisplayMember = "Nombre";
            comboBoxHotel.ValueMember = "Nombre";
            comboBoxHotel.DataSource = repoHotel.getAll();

            comboBoxTipoHabitacion.SelectedValue = "";
            comboBoxTipoHabitacion.SelectedIndex = -1;

            if (sesion != null && sesion.getHotel() != null)
            {
                this.comboBoxHotel.Visible = false;
                this.labelHotel.Visible = false;
            }
        }

        private void eventHandlerHotelComboBox(object sender, EventArgs e)
        {

            this.regimenesDisponiblesGrid.DataSource = null;
            this.regimenesDisponiblesGrid.Rows.Clear();

            this.habitacionesDisponiblesGrid.DataSource = null;
            this.habitacionesDisponiblesGrid.Rows.Clear();

            if (this.comboBoxHotel.SelectedItem == null)
            {
                this.comboBoxRegimen.Enabled = false;
                this.comboBoxRegimen.DataSource = new List<Hotel>();
            }
            else
            {
                Hotel hotel = (Hotel)this.comboBoxHotel.SelectedItem;
                this.comboBoxRegimen.DataSource = hotel.getRegimenes();
                this.comboBoxRegimen.Enabled = true;
                this.comboBoxRegimen.DisplayMember = "Descripcion";
                this.comboBoxRegimen.ValueMember = "Descripcion";
                this.comboBoxRegimen.SelectedIndex = -1;
                this.comboBoxRegimen.SelectedValue = "";


            }
        }


        private void buscarHabitaciones(Regimen regimenParam)
        {
            this.buttonModificarReserva.Enabled = false;

            DateTime fechaInicio = (DateTime)Utils.validateFields(calendarioDesde.Value, "Fecha Desde");
            DateTime fechaFin = (DateTime)Utils.validateFields(calendarioHasta.Value, "Fecah Hasta");

            if (fechaInicio < Utils.getSystemDatetimeNow())
            {
                MessageBox.Show("No puede realizar reservas en el pasado", "Error");
                return;
            }
            if (Utils.validateTimeRanges(fechaInicio, fechaFin))
            {

                Hotel hotelSeleccionado = (Hotel)Utils.validateFields(comboBoxHotel.SelectedItem, "Hotel");
                TipoHabitacion tipoHabitacionSeleccionada = null;
                if (comboBoxTipoHabitacion.SelectedItem != null)
                {
                    tipoHabitacionSeleccionada = (TipoHabitacion)comboBoxTipoHabitacion.SelectedItem;
                }
                Regimen regimenSeleccionado = null;

                regimenSeleccionado = (Regimen)comboBoxRegimen.SelectedItem;

                regimenSeleccionado = regimenParam;

                RepositorioHabitacion repoHabitacion = new RepositorioHabitacion();
                List<HabitacionDisponible> habitacionesDisponibles = repoHabitacion.getHabitacionesDisponibles(fechaInicio, fechaFin, hotelSeleccionado, tipoHabitacionSeleccionada, regimenSeleccionado,reserva).OrderBy(hd => hd.getNumeroHabitacion()).ToList();
                
                if (habitacionesDisponibles.Count == 0)
                {
                    limpiarRegimenesDataGrid();
                    this.regimenesDisponiblesGrid.DataSource = null;
                    this.habitacionesDisponiblesGrid.DataSource = null;
                    MessageBox.Show("No se encontraron habitaciones disponibles.", "Generar Reserva", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                this.habitacionesDisponiblesGrid.DataSource = habitacionesDisponibles;
                this.habitacionesDisponiblesGrid.AutoResizeColumns();
                this.habitacionesDisponiblesGrid.CurrentCell = null;
                this.habitacionesDisponiblesGrid.ClearSelection();
                if (this.habitacionesDisponiblesGrid.Rows.Count > 0)
                {
                    this.habitacionesDisponiblesGrid.Rows[0].Cells[0].Selected = false;
                    this.habitacionesDisponiblesGrid.Rows[0].Selected = false;
                }

                RepositorioRegimen repoRegimen = new RepositorioRegimen();

                this.regimenesDisponiblesGrid.DataSource = repoRegimen.getByIdHotel(hotelSeleccionado.getIdHotel());
                this.regimenesDisponiblesGrid.AutoResizeColumns();



                if (regimenSeleccionado == null)
                {
                    limpiarRegimenesDataGrid();

                }
                else
                {
                    limpiarRegimenesDataGrid();
                    foreach (DataGridViewRow item in this.regimenesDisponiblesGrid.Rows)
                    {
                        Regimen regimen = item.DataBoundItem as Regimen;
                        if (regimen.getIdRegimen() == regimenSeleccionado.getIdRegimen())
                        {
                            item.Selected = true;

                        }
                    }
                }
            }
        }

        private void limpiarRegimenesDataGrid()
        {
            this.regimenesDisponiblesGrid.Enabled = true;
            this.regimenesDisponiblesGrid.CurrentCell = null;
            this.regimenesDisponiblesGrid.ClearSelection();
            if (this.regimenesDisponiblesGrid.Rows.Count > 0)
            {
                this.regimenesDisponiblesGrid.Rows[0].Cells[0].Selected = false;
                this.regimenesDisponiblesGrid.Rows[0].Selected = false;
            }
        }
        private void buscarHabitaciones_Click(object sender, EventArgs e)
        {
            this.buscarHabitaciones((Regimen)comboBoxRegimen.SelectedItem);

        }

        private void habitaciones_cellClick(object sender, EventArgs e)
        {
            Regimen regimen = null;
            foreach (DataGridViewRow item in this.regimenesDisponiblesGrid.SelectedRows)
            {
                regimen = item.DataBoundItem as Regimen;
            }
            if (regimen != null)
            {
                this.buttonModificarReserva.Enabled = true;
            }
        }

        

        private void regimenesDisponiblesGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Regimen regimenSelected = null;
            foreach (DataGridViewRow item in this.regimenesDisponiblesGrid.SelectedRows)
            {
                regimenSelected = item.DataBoundItem as Regimen;
            }


            this.buscarHabitaciones(regimenSelected);
        }

        private void limpiarButton_Click(object sender, EventArgs e)
        {
            this.limpiarFiltros();
        }

        private void buttonModificarReserva_Click(object sender, EventArgs e)
        {
            DateTime fechaInicio = calendarioDesde.Value;
            DateTime fechaFin = calendarioHasta.Value;
            List<HabitacionDisponible> habitacionesAReservar = new List<HabitacionDisponible>();

            foreach (DataGridViewRow item in this.habitacionesDisponiblesGrid.SelectedRows)
            {
                habitacionesAReservar.Add(item.DataBoundItem as HabitacionDisponible);
            }

            if (!isReservaModificada(habitacionesAReservar, fechaInicio, fechaFin)){
                MessageBox.Show("La reserva es identica a la ya reservada. Por ende no se modifica", "Modificar Reserva");
                return;
            }
            using (ConfirmarModificacionWindow form = new ConfirmarModificacionWindow(habitacionesAReservar, fechaInicio, fechaFin, usuario,reserva))
            {
                var result = form.ShowDialog();
                this.buscarHabitaciones((Regimen)comboBoxRegimen.SelectedItem);
                RepositorioReserva repoReserva = new RepositorioReserva();
                reserva = repoReserva.getById(reserva.getIdReserva());
                buildReservaActual(reserva);
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

        private bool isReservaModificada(List<HabitacionDisponible> habitacionesAReservar, DateTime fechaInicio, DateTime fechaFin){
            bool distintoRegimen= habitacionesAReservar[0].getRegimen().getIdRegimen() != reserva.getRegimen().getIdRegimen();
            bool distintoHotel = habitacionesAReservar[0].getHabitacion().getHotel().getIdHotel() != reserva.getHotel().getIdHotel();
            bool mismaCantidadesHabitacionesEntreReservaYSeleccionadas=habitacionesAReservar.Count == reserva.getHabitaciones().Count;
            bool existenTodasLasHabitacionesDeLaReservaEnLasNuevasHabitaciones= reserva.getHabitaciones().All(habitacionReserva => habitacionesAReservar.Exists(habitacionAReservar => habitacionAReservar.getHabitacion().getIdHabitacion().Equals(habitacionReserva.getIdHabitacion())));
            bool distintaFechaInicio = fechaInicio != reserva.getFechaDesde();
            bool distintaFechaFin = fechaFin != reserva.getFechaHasta();

            return distintaFechaFin | distintaFechaInicio | distintoHotel |
                    distintoRegimen | !(mismaCantidadesHabitacionesEntreReservaYSeleccionadas & existenTodasLasHabitacionesDeLaReservaEnLasNuevasHabitaciones);
           
        }


    }
}
