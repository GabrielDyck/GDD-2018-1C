using System.Windows.Forms;
namespace FrbaHotel.AbmHabitacion
{
    partial class CrearHabitacion
    {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelNumero = new System.Windows.Forms.Label();
            this.textNumero = new System.Windows.Forms.TextBox();
            this.labelPiso = new System.Windows.Forms.Label();
            this.textPiso = new System.Windows.Forms.TextBox();
            this.labelUbicacion = new System.Windows.Forms.Label();
            this.comboBoxUbicacion = new System.Windows.Forms.ComboBox();
            this.labelTipoHabitacion = new System.Windows.Forms.Label();
            this.comboBoxTipoHabitacion = new System.Windows.Forms.ComboBox();
            this.buttonCrearHabitacion = new System.Windows.Forms.Button();
            this.checkBoxActiva = new System.Windows.Forms.CheckBox();
            this.labelDescripcion = new System.Windows.Forms.Label();
            this.textDescripcion = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelNumero
            // 
            this.labelNumero.AutoSize = true;
            this.labelNumero.Location = new System.Drawing.Point(10, 37);
            this.labelNumero.Name = "labelNumero";
            this.labelNumero.Size = new System.Drawing.Size(44, 13);
            this.labelNumero.TabIndex = 3;
            this.labelNumero.Text = "Numero";
            // 
            // textNumero
            // 
            this.textNumero.Location = new System.Drawing.Point(61, 34);
            this.textNumero.MaxLength = 9;
            this.textNumero.Name = "textNumero";
            this.textNumero.Size = new System.Drawing.Size(100, 20);
            this.textNumero.TabIndex = 1;
            this.textNumero.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.onlyNumeric);
            // 
            // labelPiso
            // 
            this.labelPiso.AutoSize = true;
            this.labelPiso.Location = new System.Drawing.Point(217, 37);
            this.labelPiso.Name = "labelPiso";
            this.labelPiso.Size = new System.Drawing.Size(27, 13);
            this.labelPiso.TabIndex = 5;
            this.labelPiso.Text = "Piso";
            // 
            // textPiso
            // 
            this.textPiso.Location = new System.Drawing.Point(292, 34);
            this.textPiso.MaxLength = 9;
            this.textPiso.Name = "textPiso";
            this.textPiso.Size = new System.Drawing.Size(100, 20);
            this.textPiso.TabIndex = 2;
            this.textPiso.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.onlyNumeric);
            // 
            // labelUbicacion
            // 
            this.labelUbicacion.AutoSize = true;
            this.labelUbicacion.Location = new System.Drawing.Point(217, 100);
            this.labelUbicacion.Name = "labelUbicacion";
            this.labelUbicacion.Size = new System.Drawing.Size(58, 13);
            this.labelUbicacion.TabIndex = 15;
            this.labelUbicacion.Text = "Ubicacion:";
            // 
            // comboBoxUbicacion
            // 
            this.comboBoxUbicacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxUbicacion.Location = new System.Drawing.Point(292, 94);
            this.comboBoxUbicacion.Name = "comboBoxUbicacion";
            this.comboBoxUbicacion.Size = new System.Drawing.Size(100, 21);
            this.comboBoxUbicacion.TabIndex = 5;
            // 
            // labelTipoHabitacion
            // 
            this.labelTipoHabitacion.AutoSize = true;
            this.labelTipoHabitacion.Location = new System.Drawing.Point(10, 100);
            this.labelTipoHabitacion.Name = "labelTipoHabitacion";
            this.labelTipoHabitacion.Size = new System.Drawing.Size(31, 13);
            this.labelTipoHabitacion.TabIndex = 17;
            this.labelTipoHabitacion.Text = "Tipo:";
            // 
            // comboBoxTipoHabitacion
            // 
            this.comboBoxTipoHabitacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTipoHabitacion.Location = new System.Drawing.Point(61, 94);
            this.comboBoxTipoHabitacion.Name = "comboBoxTipoHabitacion";
            this.comboBoxTipoHabitacion.Size = new System.Drawing.Size(100, 21);
            this.comboBoxTipoHabitacion.TabIndex = 3;
            // 
            // buttonCrearHabitacion
            // 
            this.buttonCrearHabitacion.Location = new System.Drawing.Point(317, 231);
            this.buttonCrearHabitacion.Name = "buttonCrearHabitacion";
            this.buttonCrearHabitacion.Size = new System.Drawing.Size(75, 23);
            this.buttonCrearHabitacion.TabIndex = 7;
            this.buttonCrearHabitacion.Text = "Crear Habitacion";
            this.buttonCrearHabitacion.UseVisualStyleBackColor = true;
            this.buttonCrearHabitacion.Click += new System.EventHandler(this.buttonCrearHabitacion_Click);
            // 
            // checkBoxActiva
            // 
            this.checkBoxActiva.AutoSize = true;
            this.checkBoxActiva.Location = new System.Drawing.Point(326, 169);
            this.checkBoxActiva.Name = "checkBoxActiva";
            this.checkBoxActiva.Size = new System.Drawing.Size(56, 17);
            this.checkBoxActiva.TabIndex = 6;
            this.checkBoxActiva.Text = "Activa";
            this.checkBoxActiva.UseVisualStyleBackColor = true;
            // 
            // labelDescripcion
            // 
            this.labelDescripcion.AutoSize = true;
            this.labelDescripcion.Location = new System.Drawing.Point(12, 169);
            this.labelDescripcion.Name = "labelDescripcion";
            this.labelDescripcion.Size = new System.Drawing.Size(63, 13);
            this.labelDescripcion.TabIndex = 19;
            this.labelDescripcion.Text = "Descripcion";
            // 
            // textDescripcion
            // 
            this.textDescripcion.Location = new System.Drawing.Point(81, 139);
            this.textDescripcion.MaxLength = 500;
            this.textDescripcion.Multiline = true;
            this.textDescripcion.Name = "textDescripcion";
            this.textDescripcion.Size = new System.Drawing.Size(196, 115);
            this.textDescripcion.TabIndex = 18;
            // 
            // CrearHabitacion
            // 
            this.AcceptButton = this.buttonCrearHabitacion;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 281);
            this.Controls.Add(this.labelDescripcion);
            this.Controls.Add(this.textDescripcion);
            this.Controls.Add(this.checkBoxActiva);
            this.Controls.Add(this.buttonCrearHabitacion);
            this.Controls.Add(this.labelTipoHabitacion);
            this.Controls.Add(this.comboBoxTipoHabitacion);
            this.Controls.Add(this.labelUbicacion);
            this.Controls.Add(this.comboBoxUbicacion);
            this.Controls.Add(this.labelPiso);
            this.Controls.Add(this.textPiso);
            this.Controls.Add(this.labelNumero);
            this.Controls.Add(this.textNumero);
            this.Name = "CrearHabitacion";
            this.Text = "Crear Habitacion";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void onlyNumeric(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        #endregion

        private System.Windows.Forms.Label labelNumero;
        private System.Windows.Forms.TextBox textNumero;
        private System.Windows.Forms.Label labelPiso;
        private System.Windows.Forms.TextBox textPiso;
        private System.Windows.Forms.Label labelUbicacion;
        private System.Windows.Forms.ComboBox comboBoxUbicacion;
        private System.Windows.Forms.Label labelTipoHabitacion;
        private System.Windows.Forms.ComboBox comboBoxTipoHabitacion;
        private System.Windows.Forms.Button buttonCrearHabitacion;
        private System.Windows.Forms.CheckBox checkBoxActiva;
        private Label labelDescripcion;
        private TextBox textDescripcion;


    }
}