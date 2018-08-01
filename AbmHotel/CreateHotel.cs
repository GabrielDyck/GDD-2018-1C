using FrbaHotel.Commons;
using FrbaHotel.Modelo;
using FrbaHotel.Repositorios;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FrbaHotel.Excepciones;
using System.Linq;

namespace FrbaHotel.AbmHotel
{
    public partial class CreateHotel
      : Form

    {
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
        private DataGridView regimenesDataGrid;
        private Label creacionLabel;
        private DateTimePicker creacionTime;
        private GroupBox groupBox1;
        private Label emailLabel;
        private TextBox emailText;
        private Button buttonLimpiar;
        private Button crearHotel;


        public CreateHotel()
        {
            InitializeComponent();
        }

        private void initModificacionHotel() {

            RepositorioCategoria repoCategoria = new RepositorioCategoria();
            RepositorioRegimen repoRegimen = new RepositorioRegimen();

            this.estrellasComboBox.DataSource = repoCategoria.getAll().OrderBy(c => c.getEstrellas()).ToList();
            this.regimenesDataGrid.DataSource = repoRegimen.getAll().OrderBy(r => r.getDescripcion()).ToList();
            regimenesDataGrid.AutoResizeColumns();
            this.regimenesDataGrid.CurrentCell = null;
            this.regimenesDataGrid.ClearSelection();
            this.regimenesDataGrid.Rows[0].Cells[0].Selected = false;
            this.regimenesDataGrid.Rows[0].Selected = false;

            this.nombreText.Text = "";
            this.paisText.Text = "";
            this.ciudadText.Text = "";
            this.telefonoText.Text = "";
            this.calleText.Text = "";
            this.numeroCalleText.Text = "";
            this.emailText.Text = "";

            this.creacionTime.ResetText();
            this.estrellasComboBox.SelectedItem = null;

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
            this.buttonLimpiar = new System.Windows.Forms.Button();
            this.emailLabel = new System.Windows.Forms.Label();
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
            this.regimenesLabel = new System.Windows.Forms.Label();
            this.regimenesDataGrid = new System.Windows.Forms.DataGridView();
            this.creacionLabel = new System.Windows.Forms.Label();
            this.creacionTime = new System.Windows.Forms.DateTimePicker();
            this.crearHotel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.regimenesDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // nombreText
            // 
            this.nombreText.Location = new System.Drawing.Point(86, 38);
            this.nombreText.MaxLength = 255;
            this.nombreText.Name = "nombreText";
            this.nombreText.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.nombreText.Size = new System.Drawing.Size(100, 20);
            this.nombreText.TabIndex = 1;
            // 
            // nombreLabel
            // 
            this.nombreLabel.AutoSize = true;
            this.nombreLabel.Location = new System.Drawing.Point(22, 41);
            this.nombreLabel.Name = "nombreLabel";
            this.nombreLabel.Size = new System.Drawing.Size(44, 13);
            this.nombreLabel.TabIndex = 1;
            this.nombreLabel.Text = "Nombre";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonLimpiar);
            this.groupBox1.Controls.Add(this.emailLabel);
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
            this.groupBox1.Controls.Add(this.regimenesLabel);
            this.groupBox1.Controls.Add(this.regimenesDataGrid);
            this.groupBox1.Controls.Add(this.creacionLabel);
            this.groupBox1.Controls.Add(this.creacionTime);
            this.groupBox1.Controls.Add(this.nombreLabel);
            this.groupBox1.Controls.Add(this.nombreText);
            this.groupBox1.Controls.Add(this.crearHotel);
            this.groupBox1.Location = new System.Drawing.Point(13, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(700, 371);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Crear Hotel";
            // 
            // buttonLimpiar
            // 
            this.buttonLimpiar.Location = new System.Drawing.Point(155, 332);
            this.buttonLimpiar.Name = "buttonLimpiar";
            this.buttonLimpiar.Size = new System.Drawing.Size(161, 23);
            this.buttonLimpiar.TabIndex = 18;
            this.buttonLimpiar.Text = "Limpiar";
            this.buttonLimpiar.Click += new System.EventHandler(this.buttonLimpiar_Click);
            // 
            // emailLabel
            // 
            this.emailLabel.AutoSize = true;
            this.emailLabel.Location = new System.Drawing.Point(485, 100);
            this.emailLabel.Name = "emailLabel";
            this.emailLabel.Size = new System.Drawing.Size(35, 13);
            this.emailLabel.TabIndex = 17;
            this.emailLabel.Text = "Email:";
            // 
            // emailText
            // 
            this.emailText.Location = new System.Drawing.Point(548, 97);
            this.emailText.MaxLength = 255;
            this.emailText.Name = "emailText";
            this.emailText.Size = new System.Drawing.Size(117, 20);
            this.emailText.TabIndex = 6;
            // 
            // telefonoLabel
            // 
            this.telefonoLabel.AutoSize = true;
            this.telefonoLabel.Location = new System.Drawing.Point(485, 164);
            this.telefonoLabel.Name = "telefonoLabel";
            this.telefonoLabel.Size = new System.Drawing.Size(52, 13);
            this.telefonoLabel.TabIndex = 15;
            this.telefonoLabel.Text = "Telefono:";
            // 
            // telefonoText
            // 
            this.telefonoText.Location = new System.Drawing.Point(548, 161);
            this.telefonoText.MaxLength = 45;
            this.telefonoText.Name = "telefonoText";
            this.telefonoText.Size = new System.Drawing.Size(117, 20);
            this.telefonoText.TabIndex = 9;
            this.telefonoText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.onlyNumeric);
            // 
            // calleLabel
            // 
            this.calleLabel.AutoSize = true;
            this.calleLabel.Location = new System.Drawing.Point(23, 161);
            this.calleLabel.Name = "calleLabel";
            this.calleLabel.Size = new System.Drawing.Size(33, 13);
            this.calleLabel.TabIndex = 13;
            this.calleLabel.Text = "Calle:";
            // 
            // calleText
            // 
            this.calleText.Location = new System.Drawing.Point(86, 158);
            this.calleText.MaxLength = 255;
            this.calleText.Name = "calleText";
            this.calleText.Size = new System.Drawing.Size(117, 20);
            this.calleText.TabIndex = 7;
            // 
            // estrellasLabel
            // 
            this.estrellasLabel.AutoSize = true;
            this.estrellasLabel.Location = new System.Drawing.Point(485, 44);
            this.estrellasLabel.Name = "estrellasLabel";
            this.estrellasLabel.Size = new System.Drawing.Size(49, 13);
            this.estrellasLabel.TabIndex = 11;
            this.estrellasLabel.Text = "Estrellas:";
            // 
            // estrellasComboBox
            // 
            this.estrellasComboBox.BackColor = System.Drawing.SystemColors.Window;
            this.estrellasComboBox.DisplayMember = "Estrellas";
            this.estrellasComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.estrellasComboBox.Location = new System.Drawing.Point(548, 41);
            this.estrellasComboBox.Name = "estrellasComboBox";
            this.estrellasComboBox.Size = new System.Drawing.Size(117, 21);
            this.estrellasComboBox.TabIndex = 3;
            // 
            // ciudadLabel
            // 
            this.ciudadLabel.AutoSize = true;
            this.ciudadLabel.Location = new System.Drawing.Point(244, 97);
            this.ciudadLabel.Name = "ciudadLabel";
            this.ciudadLabel.Size = new System.Drawing.Size(43, 13);
            this.ciudadLabel.TabIndex = 9;
            this.ciudadLabel.Text = "Ciudad:";
            // 
            // ciudadText
            // 
            this.ciudadText.Location = new System.Drawing.Point(307, 94);
            this.ciudadText.MaxLength = 255;
            this.ciudadText.Name = "ciudadText";
            this.ciudadText.Size = new System.Drawing.Size(117, 20);
            this.ciudadText.TabIndex = 5;
            // 
            // numeroCalleLabel
            // 
            this.numeroCalleLabel.AutoSize = true;
            this.numeroCalleLabel.Location = new System.Drawing.Point(244, 157);
            this.numeroCalleLabel.Name = "numeroCalleLabel";
            this.numeroCalleLabel.Size = new System.Drawing.Size(72, 13);
            this.numeroCalleLabel.TabIndex = 13;
            this.numeroCalleLabel.Text = "Numero calle:";
            // 
            // numeroCalleText
            // 
            this.numeroCalleText.Location = new System.Drawing.Point(322, 154);
            this.numeroCalleText.MaxLength = 9;
            this.numeroCalleText.Name = "numeroCalleText";
            this.numeroCalleText.Size = new System.Drawing.Size(117, 20);
            this.numeroCalleText.TabIndex = 8;
            this.numeroCalleText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.onlyNumeric);
            // 
            // paisLabel
            // 
            this.paisLabel.AutoSize = true;
            this.paisLabel.Location = new System.Drawing.Point(23, 97);
            this.paisLabel.Name = "paisLabel";
            this.paisLabel.Size = new System.Drawing.Size(30, 13);
            this.paisLabel.TabIndex = 7;
            this.paisLabel.Text = "Pais:";
            // 
            // paisText
            // 
            this.paisText.Location = new System.Drawing.Point(86, 94);
            this.paisText.MaxLength = 45;
            this.paisText.Name = "paisText";
            this.paisText.Size = new System.Drawing.Size(117, 20);
            this.paisText.TabIndex = 4;
            // 
            // regimenesLabel
            // 
            this.regimenesLabel.AutoSize = true;
            this.regimenesLabel.Location = new System.Drawing.Point(23, 216);
            this.regimenesLabel.Name = "regimenesLabel";
            this.regimenesLabel.Size = new System.Drawing.Size(55, 13);
            this.regimenesLabel.TabIndex = 5;
            this.regimenesLabel.Text = "Regimen: ";
            // 
            // regimenesDataGrid
            // 
            this.regimenesDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.regimenesDataGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.regimenesDataGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.regimenesDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.regimenesDataGrid.Location = new System.Drawing.Point(86, 213);
            this.regimenesDataGrid.Name = "regimenesDataGrid";
            this.regimenesDataGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.regimenesDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.regimenesDataGrid.Size = new System.Drawing.Size(416, 97);
            this.regimenesDataGrid.TabIndex = 10;
            // 
            // creacionLabel
            // 
            this.creacionLabel.AutoSize = true;
            this.creacionLabel.Location = new System.Drawing.Point(244, 44);
            this.creacionLabel.Name = "creacionLabel";
            this.creacionLabel.Size = new System.Drawing.Size(60, 13);
            this.creacionLabel.TabIndex = 3;
            this.creacionLabel.Text = "F. creacion";
            // 
            // creacionTime
            // 
            this.creacionTime.Location = new System.Drawing.Point(307, 41);
            this.creacionTime.Name = "creacionTime";
            this.creacionTime.Size = new System.Drawing.Size(117, 20);
            this.creacionTime.TabIndex = 2;
            // 
            // crearHotel
            // 
            this.crearHotel.Location = new System.Drawing.Point(341, 332);
            this.crearHotel.Name = "crearHotel";
            this.crearHotel.Size = new System.Drawing.Size(161, 23);
            this.crearHotel.TabIndex = 11;
            this.crearHotel.Text = "Crear";
            this.crearHotel.Click += new System.EventHandler(this.altaHotel_Click);
            // 
            // CreateHotel
            // 
            this.AcceptButton = this.crearHotel;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 395);
            this.Controls.Add(this.groupBox1);
            this.Name = "CreateHotel";
            this.Text = "Creacion de Hoteles";
            this.Load += new System.EventHandler(this.load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.regimenesDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private void altaHotel_Click(object sender, EventArgs e)
        {
            try
            {
                RepositorioHotel repoHotel = new RepositorioHotel();

                List<Regimen> regimenes = new List<Regimen>();
                foreach (DataGridViewRow item in this.regimenesDataGrid.SelectedRows)
                {
                    regimenes.Add(item.DataBoundItem as Regimen);
                }

                Utils.validateListField(this.regimenesDataGrid.SelectedRows, "Regimen");

                String pais = Utils.validateStringFields((String)paisText.Text.Trim(), "Pais");
                String ciudad = Utils.validateStringFields((String)ciudadText.Text.Trim(), "Ciudad");
                String calle = Utils.validateStringFields((String)calleText.Text.Trim(), "Calle");
                int numeroCalle = Utils.validateIntField((String)numeroCalleText.Text.Trim(), "NumeroCalle");
                Direccion direccion = new Direccion(0, pais, ciudad, calle, numeroCalle, 0, "");

                Categoria categoria = (Categoria)Utils.validateFields(estrellasComboBox.SelectedItem, "Categoria");
                String email = Utils.validateStringFields(emailText.Text.Trim(), "Email");
                String telefono = Utils.validateStringFields(telefonoText.Text.Trim(), "Telefono");
                DateTime fechaInicioActividades = (DateTime)Utils.validateFields(creacionTime.Value, "Fecha Inicio de Actividades");
                String nombre = Utils.validateStringFields(nombreText.Text.Trim(), "Nombre");
                Hotel hotelToUpdateSave = new Hotel(0, categoria, direccion, nombre, email, telefono, fechaInicioActividades, regimenes);

                //VALIDAMOS QUE NO EXISTA UN HOTEL CON EL MISMO NOMBRE
                if (repoHotel.yaExisteHotelMismoNombre(hotelToUpdateSave))
                {
                    MessageBox.Show("Ya existe un hotel registrado con el mismo nombre.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                repoHotel.create(hotelToUpdateSave);
                MessageBox.Show("Hotel creado exitosamente.", "Gestion de Datos TP 2018 1C - LOS_BORBOTONES", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.initModificacionHotel();
            }
            //catch (RequestInvalidoException exception)
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Verifique los datos ingresados.", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void load(object sender, EventArgs e) {
            this.initModificacionHotel();
        }

        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
            this.initModificacionHotel();
        }

    }

}
