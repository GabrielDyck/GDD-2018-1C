using FrbaHotel.Commons;
using FrbaHotel.Excepciones;
using FrbaHotel.Modelo;
using FrbaHotel.Repositorios;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace FrbaHotel.AbmHotel
{
    public partial class ModificacionHotel
      : Form

    {
        private Hotel hotel;
        private TextBox nombreText;
        private Label nombreLabel;
        private Label telefonoLabel;
        private TextBox telefonoText;
        private Label calleLabel;
        private TextBox calleText;
        private Label estrellasLabel;
        private ComboBox estrellasComboBox;
        private Label ciudadLabel;
        private TextBox ciudadText;
        private Label numeroCalleLabel;
        private TextBox numeroCalleText;
        private Label paisLabel;
        private TextBox paisText;
        private Label regimenesLabel;
        private DataGridView regimenesGrid;
        private Label creacionLabel;
        private DateTimePicker creacionTime;
        private GroupBox groupBox1;
        private Label emailLabel;
        private TextBox emailText;
        private Button buttonResetear;
        private DataGridView dataGridCierres;
        private Label label1;
        private Button buttonNuevoCierre;
        private Button buttonSalir;
        private Label labelReservas;
        private DataGridView dataGridReservas;
        private Button modificarHotel;


        public ModificacionHotel(Hotel hotel)
        {
            this.hotel = hotel;
            InitializeComponent();
        }

        private void initModificacionHotel()
        {
            RepositorioCategoria repoCategoria = new RepositorioCategoria();
            this.estrellasComboBox.DataSource = repoCategoria.getAll().OrderBy(c => c.getEstrellas()).ToList();
            this.estrellasComboBox.ValueMember = "Estrellas";

            RepositorioRegimen repoRegimen = new RepositorioRegimen();
            this.regimenesGrid.DataSource = repoRegimen.getAll().OrderBy(r => r.getDescripcion()).ToList();
            regimenesGrid.AutoResizeColumns();
            regimenesGrid.CurrentCell = null;
            regimenesGrid.ClearSelection();
            this.regimenesGrid.Rows[0].Cells[0].Selected = false;
            this.regimenesGrid.Rows[0].Selected = false;

            this.nombreText.Text = hotel.getNombre();
            this.paisText.Text = hotel.getDireccion().getPais();
            this.ciudadText.Text = hotel.getDireccion().getCiudad();
            this.telefonoText.Text = hotel.getTelefono();
            this.estrellasComboBox.SelectedValue = hotel.getCategoria().getEstrellas();

            this.calleText.Text = hotel.getDireccion().getCalle();
            this.numeroCalleText.Text = hotel.getDireccion().getNumeroCalle().ToString();
            this.creacionTime.Value = hotel.getFechaInicioActividades();
            this.emailText.Text = hotel.getMail();


            foreach (DataGridViewRow row in regimenesGrid.Rows)
            {
                Regimen regimen = (Regimen)row.DataBoundItem;
                if (hotel.getRegimenes().Exists(r => r.getIdRegimen().Equals(regimen.getIdRegimen())))
                {
                    this.regimenesGrid.Rows[row.Index].Cells[0].Selected = true;
                    this.regimenesGrid.Rows[row.Index].Selected = true;
                }
            }

            this.dataGridCierres.DataSource = this.hotel.getCierresTemporales().OrderBy(c => c.getFechaInicio()).ToList();
            //ESTO LO TENGO QUE HACER PARA QUE NO APAREZCA SIEMPRE SELECCIONADO EL PRIMER ITEM
            dataGridCierres.CurrentCell = null;
            dataGridCierres.ClearSelection();

            //MEJORA DE PERFORMANCE DEL DGV
            this.dataGridReservas.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            this.dataGridReservas.RowHeadersVisible = false;
            this.dataGridReservas.DataSource = this.hotel.getReservas().OrderBy(r => r.getFechaDesde()).ToList();
            this.dataGridReservas.RowHeadersVisible = true;

            //ESTO LO TENGO QUE HACER PARA QUE NO APAREZCA SIEMPRE SELECCIONADO EL PRIMER ITEM
            dataGridReservas.CurrentCell = null;
            dataGridReservas.ClearSelection();

        }
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.nombreText = new System.Windows.Forms.TextBox();
            this.nombreLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelReservas = new System.Windows.Forms.Label();
            this.dataGridReservas = new System.Windows.Forms.DataGridView();
            this.buttonSalir = new System.Windows.Forms.Button();
            this.buttonNuevoCierre = new System.Windows.Forms.Button();
            this.dataGridCierres = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonResetear = new System.Windows.Forms.Button();
            this.emailLabel = new System.Windows.Forms.Label();
            this.regimenesGrid = new System.Windows.Forms.DataGridView();
            this.regimenesLabel = new System.Windows.Forms.Label();
            this.emailText = new System.Windows.Forms.TextBox();
            this.telefonoLabel = new System.Windows.Forms.Label();
            this.telefonoText = new System.Windows.Forms.TextBox();
            this.calleLabel = new System.Windows.Forms.Label();
            this.calleText = new System.Windows.Forms.TextBox();
            this.estrellasLabel = new System.Windows.Forms.Label();
            this.estrellasComboBox = new System.Windows.Forms.ComboBox();
            this.ciudadLabel = new System.Windows.Forms.Label();
            this.ciudadText = new System.Windows.Forms.TextBox();
            this.numeroCalleLabel = new System.Windows.Forms.Label();
            this.numeroCalleText = new System.Windows.Forms.TextBox();
            this.paisLabel = new System.Windows.Forms.Label();
            this.paisText = new System.Windows.Forms.TextBox();
            this.creacionLabel = new System.Windows.Forms.Label();
            this.creacionTime = new System.Windows.Forms.DateTimePicker();
            this.modificarHotel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridReservas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCierres)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.regimenesGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // nombreText
            // 
            this.nombreText.Location = new System.Drawing.Point(86, 38);
            this.nombreText.MaxLength = 255;
            this.nombreText.Name = "nombreText";
            this.nombreText.Size = new System.Drawing.Size(117, 20);
            this.nombreText.TabIndex = 1;
            // 
            // nombreLabel
            // 
            this.nombreLabel.AutoSize = true;
            this.nombreLabel.Location = new System.Drawing.Point(23, 41);
            this.nombreLabel.Name = "nombreLabel";
            this.nombreLabel.Size = new System.Drawing.Size(44, 13);
            this.nombreLabel.TabIndex = 1;
            this.nombreLabel.Text = "Nombre";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelReservas);
            this.groupBox1.Controls.Add(this.dataGridReservas);
            this.groupBox1.Controls.Add(this.buttonSalir);
            this.groupBox1.Controls.Add(this.buttonNuevoCierre);
            this.groupBox1.Controls.Add(this.dataGridCierres);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.buttonResetear);
            this.groupBox1.Controls.Add(this.emailLabel);
            this.groupBox1.Controls.Add(this.regimenesGrid);
            this.groupBox1.Controls.Add(this.regimenesLabel);
            this.groupBox1.Controls.Add(this.emailText);
            this.groupBox1.Controls.Add(this.telefonoLabel);
            this.groupBox1.Controls.Add(this.telefonoText);
            this.groupBox1.Controls.Add(this.calleLabel);
            this.groupBox1.Controls.Add(this.calleText);
            this.groupBox1.Controls.Add(this.estrellasLabel);
            this.groupBox1.Controls.Add(this.estrellasComboBox);
            this.groupBox1.Controls.Add(this.ciudadLabel);
            this.groupBox1.Controls.Add(this.ciudadText);
            this.groupBox1.Controls.Add(this.numeroCalleLabel);
            this.groupBox1.Controls.Add(this.numeroCalleText);
            this.groupBox1.Controls.Add(this.paisLabel);
            this.groupBox1.Controls.Add(this.paisText);
            this.groupBox1.Controls.Add(this.creacionLabel);
            this.groupBox1.Controls.Add(this.creacionTime);
            this.groupBox1.Controls.Add(this.nombreLabel);
            this.groupBox1.Controls.Add(this.nombreText);
            this.groupBox1.Controls.Add(this.modificarHotel);
            this.groupBox1.Location = new System.Drawing.Point(13, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(720, 508);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Modificar Hotel";
            // 
            // labelReservas
            // 
            this.labelReservas.AutoSize = true;
            this.labelReservas.Location = new System.Drawing.Point(23, 369);
            this.labelReservas.Name = "labelReservas";
            this.labelReservas.Size = new System.Drawing.Size(55, 13);
            this.labelReservas.TabIndex = 23;
            this.labelReservas.Text = "Reservas:";
            // 
            // dataGridReservas
            // 
            this.dataGridReservas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridReservas.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dataGridReservas.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dataGridReservas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridReservas.Location = new System.Drawing.Point(137, 369);
            this.dataGridReservas.MultiSelect = false;
            this.dataGridReservas.Name = "dataGridReservas";
            this.dataGridReservas.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridReservas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridReservas.Size = new System.Drawing.Size(528, 104);
            this.dataGridReservas.TabIndex = 22;
            // 
            // buttonSalir
            // 
            this.buttonSalir.Location = new System.Drawing.Point(504, 479);
            this.buttonSalir.Name = "buttonSalir";
            this.buttonSalir.Size = new System.Drawing.Size(161, 23);
            this.buttonSalir.TabIndex = 21;
            this.buttonSalir.Text = "Salir";
            this.buttonSalir.Click += new System.EventHandler(this.buttonSalir_Click);
            // 
            // buttonNuevoCierre
            // 
            this.buttonNuevoCierre.Location = new System.Drawing.Point(26, 277);
            this.buttonNuevoCierre.Name = "buttonNuevoCierre";
            this.buttonNuevoCierre.Size = new System.Drawing.Size(97, 23);
            this.buttonNuevoCierre.TabIndex = 20;
            this.buttonNuevoCierre.Text = "Nuevo Cierre...";
            this.buttonNuevoCierre.Click += new System.EventHandler(this.buttonNuevoCierre_Click);
            // 
            // dataGridCierres
            // 
            this.dataGridCierres.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridCierres.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dataGridCierres.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dataGridCierres.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridCierres.Location = new System.Drawing.Point(137, 261);
            this.dataGridCierres.MultiSelect = false;
            this.dataGridCierres.Name = "dataGridCierres";
            this.dataGridCierres.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridCierres.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridCierres.Size = new System.Drawing.Size(528, 102);
            this.dataGridCierres.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 261);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Cierres Temporales:";
            // 
            // buttonResetear
            // 
            this.buttonResetear.Location = new System.Drawing.Point(140, 479);
            this.buttonResetear.Name = "buttonResetear";
            this.buttonResetear.Size = new System.Drawing.Size(161, 23);
            this.buttonResetear.TabIndex = 12;
            this.buttonResetear.Text = "Resetear";
            this.buttonResetear.Click += new System.EventHandler(this.button1_Click);
            // 
            // emailLabel
            // 
            this.emailLabel.AutoSize = true;
            this.emailLabel.Location = new System.Drawing.Point(482, 67);
            this.emailLabel.Name = "emailLabel";
            this.emailLabel.Size = new System.Drawing.Size(35, 13);
            this.emailLabel.TabIndex = 17;
            this.emailLabel.Text = "Email:";
            // 
            // regimenesGrid
            // 
            this.regimenesGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.regimenesGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.regimenesGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.regimenesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.regimenesGrid.Location = new System.Drawing.Point(137, 116);
            this.regimenesGrid.Name = "regimenesGrid";
            this.regimenesGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.regimenesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.regimenesGrid.Size = new System.Drawing.Size(528, 139);
            this.regimenesGrid.TabIndex = 10;
            // 
            // regimenesLabel
            // 
            this.regimenesLabel.AutoSize = true;
            this.regimenesLabel.Location = new System.Drawing.Point(23, 116);
            this.regimenesLabel.Name = "regimenesLabel";
            this.regimenesLabel.Size = new System.Drawing.Size(55, 13);
            this.regimenesLabel.TabIndex = 5;
            this.regimenesLabel.Text = "Regimen: ";
            // 
            // emailText
            // 
            this.emailText.Location = new System.Drawing.Point(548, 64);
            this.emailText.MaxLength = 255;
            this.emailText.Name = "emailText";
            this.emailText.Size = new System.Drawing.Size(117, 20);
            this.emailText.TabIndex = 6;
            // 
            // telefonoLabel
            // 
            this.telefonoLabel.AutoSize = true;
            this.telefonoLabel.Location = new System.Drawing.Point(482, 93);
            this.telefonoLabel.Name = "telefonoLabel";
            this.telefonoLabel.Size = new System.Drawing.Size(52, 13);
            this.telefonoLabel.TabIndex = 15;
            this.telefonoLabel.Text = "Telefono:";
            // 
            // telefonoText
            // 
            this.telefonoText.Location = new System.Drawing.Point(548, 90);
            this.telefonoText.MaxLength = 45;
            this.telefonoText.Name = "telefonoText";
            this.telefonoText.Size = new System.Drawing.Size(117, 20);
            this.telefonoText.TabIndex = 9;
            this.telefonoText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.onlyNumeric);
            // 
            // calleLabel
            // 
            this.calleLabel.AutoSize = true;
            this.calleLabel.Location = new System.Drawing.Point(23, 93);
            this.calleLabel.Name = "calleLabel";
            this.calleLabel.Size = new System.Drawing.Size(33, 13);
            this.calleLabel.TabIndex = 13;
            this.calleLabel.Text = "Calle:";
            // 
            // calleText
            // 
            this.calleText.Location = new System.Drawing.Point(86, 90);
            this.calleText.MaxLength = 255;
            this.calleText.Name = "calleText";
            this.calleText.Size = new System.Drawing.Size(117, 20);
            this.calleText.TabIndex = 7;
            // 
            // estrellasLabel
            // 
            this.estrellasLabel.AutoSize = true;
            this.estrellasLabel.Location = new System.Drawing.Point(482, 44);
            this.estrellasLabel.Name = "estrellasLabel";
            this.estrellasLabel.Size = new System.Drawing.Size(49, 13);
            this.estrellasLabel.TabIndex = 11;
            this.estrellasLabel.Text = "Estrellas:";
            // 
            // estrellasComboBox
            // 
            this.estrellasComboBox.DisplayMember = "Estrellas";
            this.estrellasComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.estrellasComboBox.Location = new System.Drawing.Point(548, 38);
            this.estrellasComboBox.Name = "estrellasComboBox";
            this.estrellasComboBox.Size = new System.Drawing.Size(117, 21);
            this.estrellasComboBox.TabIndex = 3;
            // 
            // ciudadLabel
            // 
            this.ciudadLabel.AutoSize = true;
            this.ciudadLabel.Location = new System.Drawing.Point(229, 67);
            this.ciudadLabel.Name = "ciudadLabel";
            this.ciudadLabel.Size = new System.Drawing.Size(43, 13);
            this.ciudadLabel.TabIndex = 9;
            this.ciudadLabel.Text = "Ciudad:";
            // 
            // ciudadText
            // 
            this.ciudadText.Location = new System.Drawing.Point(307, 64);
            this.ciudadText.MaxLength = 255;
            this.ciudadText.Name = "ciudadText";
            this.ciudadText.Size = new System.Drawing.Size(117, 20);
            this.ciudadText.TabIndex = 5;
            // 
            // numeroCalleLabel
            // 
            this.numeroCalleLabel.AutoSize = true;
            this.numeroCalleLabel.Location = new System.Drawing.Point(229, 93);
            this.numeroCalleLabel.Name = "numeroCalleLabel";
            this.numeroCalleLabel.Size = new System.Drawing.Size(72, 13);
            this.numeroCalleLabel.TabIndex = 13;
            this.numeroCalleLabel.Text = "Numero calle:";
            // 
            // numeroCalleText
            // 
            this.numeroCalleText.Location = new System.Drawing.Point(307, 90);
            this.numeroCalleText.MaxLength = 9;
            this.numeroCalleText.Name = "numeroCalleText";
            this.numeroCalleText.Size = new System.Drawing.Size(117, 20);
            this.numeroCalleText.TabIndex = 8;
            this.numeroCalleText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.onlyNumeric);
            // 
            // paisLabel
            // 
            this.paisLabel.AutoSize = true;
            this.paisLabel.Location = new System.Drawing.Point(23, 67);
            this.paisLabel.Name = "paisLabel";
            this.paisLabel.Size = new System.Drawing.Size(30, 13);
            this.paisLabel.TabIndex = 7;
            this.paisLabel.Text = "Pais:";
            // 
            // paisText
            // 
            this.paisText.Location = new System.Drawing.Point(86, 64);
            this.paisText.MaxLength = 45;
            this.paisText.Name = "paisText";
            this.paisText.Size = new System.Drawing.Size(117, 20);
            this.paisText.TabIndex = 4;
            // 
            // creacionLabel
            // 
            this.creacionLabel.AutoSize = true;
            this.creacionLabel.Location = new System.Drawing.Point(229, 41);
            this.creacionLabel.Name = "creacionLabel";
            this.creacionLabel.Size = new System.Drawing.Size(60, 13);
            this.creacionLabel.TabIndex = 3;
            this.creacionLabel.Text = "F. creacion";
            // 
            // creacionTime
            // 
            this.creacionTime.Location = new System.Drawing.Point(307, 38);
            this.creacionTime.Name = "creacionTime";
            this.creacionTime.Size = new System.Drawing.Size(117, 20);
            this.creacionTime.TabIndex = 2;
            // 
            // modificarHotel
            // 
            this.modificarHotel.Location = new System.Drawing.Point(326, 479);
            this.modificarHotel.Name = "modificarHotel";
            this.modificarHotel.Size = new System.Drawing.Size(161, 23);
            this.modificarHotel.TabIndex = 11;
            this.modificarHotel.Text = "Modificar";
            this.modificarHotel.Click += new System.EventHandler(this.modificarHotel_Click);
            // 
            // ModificacionHotel
            // 
            this.AcceptButton = this.modificarHotel;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 532);
            this.Controls.Add(this.groupBox1);
            this.Name = "ModificacionHotel";
            this.Text = "Modificacion de Hoteles";
            this.Load += new System.EventHandler(this.load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridReservas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCierres)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.regimenesGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private void modificarHotel_Click(object sender, EventArgs e)
        {
            try{
                RepositorioHotel repoHotel = new RepositorioHotel();

                List<Regimen> regimenes = new List<Regimen>();
                foreach (DataGridViewRow item in this.regimenesGrid.SelectedRows)
                {
                    regimenes.Add(item.DataBoundItem as Regimen);
                }

                validarQuitaRegimen(hotel.getRegimenes(), regimenes);

                String nombre = Utils.validateStringFields(nombreText.Text.Trim(), "Nombre");
                String pais = Utils.validateStringFields((String)paisText.Text.Trim(), "Pais");
                String ciudad = Utils.validateStringFields((String)ciudadText.Text.Trim(), "Ciudad");
                String calle = Utils.validateStringFields((String)calleText.Text.Trim(), "Calle");
                int numeroCalle = Utils.validateIntField((String)numeroCalleText.Text.Trim(), "NumeroCalle");
                Categoria categoria = (Categoria)Utils.validateFields(estrellasComboBox.SelectedItem, "Categoria");
                String email = Utils.validateStringFields(emailText.Text.Trim(), "Email");
                String telefono = Utils.validateStringFields(telefonoText.Text.Trim(), "Telefono");
                DateTime fechaInicioActividades = (DateTime)Utils.validateFields(creacionTime.Value, "Fecha Inicio de Actividades");
                Direccion direccion = new Direccion(hotel.getDireccion().getIdDireccion(), pais, ciudad, calle, numeroCalle, 0, "");
                Hotel hotelToUpdateSave = new Hotel(hotel.getIdHotel(), categoria, direccion, nombre, email, telefono, fechaInicioActividades, regimenes);

                if (repoHotel.yaExisteHotelMismoNombre(hotelToUpdateSave))
                {
                    MessageBox.Show("Ya existe un hotel registrado con el mismo nombre.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                repoHotel.update(hotelToUpdateSave);
                MessageBox.Show("Hotel modificado correctamente.", "Gestion de Datos TP 2018 1C - LOS_BORBOTONES", MessageBoxButtons.OK, MessageBoxIcon.Information);

                hotel = repoHotel.getById(hotel.getIdHotel());
                this.initModificacionHotel();
            }
            /*
            catch (NoExisteIDException exceptionUpdateHotel)
            {
                MessageBox.Show(exceptionUpdateHotel.Message, "Gestion de Datos TP 2018 1C - LOS_BORBOTONES", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            */
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Gestion de Datos TP 2018 1C - LOS_BORBOTONES", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void load(object sender, EventArgs e)
        {
            this.initModificacionHotel();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.initModificacionHotel();
        }

        private void validarQuitaRegimen(List<Regimen> regimenesActuales, List<Regimen> regimenesPorActualizar)
        {
            foreach (Regimen regimenActual in regimenesActuales)
            {
                if (!regimenesPorActualizar.Exists(regimen => regimen.getIdRegimen().Equals(regimenActual.getIdRegimen())))
                {
                    RepositorioReserva repoReserva = new RepositorioReserva();
                    bool exists= repoReserva.existsReservasConRegimen(regimenActual, hotel);
                    if (exists)
                    {
                        throw new RequestInvalidoException("No puede quitarse el regimen " + regimenActual.getDescripcion() + " porque existen reservas tomadas para ese regimen");
                    }
                }

            }

        }

        private void onlyNumeric(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void buttonNuevoCierre_Click(object sender, EventArgs e)
        {
            using (DropHotel cierreTemporalForm = new DropHotel(this.hotel))
            {
                var result = cierreTemporalForm.ShowDialog();

                //ME TRAIGO EL HOTEL MODIFICADO
                this.hotel.getCierresTemporales();

                RepositorioHotel repoHotel = new RepositorioHotel();
                hotel = repoHotel.getById(hotel.getIdHotel());
                //AL CERRAR LA VENTANA DESPUES DE DAR DE ALTA UN NUEVO CIERRE TEMPORAL VUELVO A CARGAR LA LISTA
                this.initModificacionHotel();
            }
        }

        private void buttonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }

}
