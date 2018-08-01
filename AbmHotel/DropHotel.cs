using System;

using System.Windows.Forms;
using System.Drawing;
using FrbaHotel.Modelo;
using FrbaHotel.Repositorios;
using FrbaHotel.Excepciones;
using FrbaHotel.Commons;

namespace FrbaHotel.AbmHotel
{
    public partial class DropHotel
       : Form

    {

        public DropHotel(Hotel hotel)
        {
            InitializeComponent();
            this.hotelBaja = hotel;
            limpiarDatos();
        }
        private Hotel hotelBaja;
        private GroupBox groupBox1;
        private Button bajaHotel;
        private Label descripcionBajaLabel;
        private TextBox descripcionBajaText;
        private Button buttonSalir;
        private Button buttonLimpiarDatos;
        private DateTimePicker calendarioDesde;
        private Label label2;
        private Label label1;
        private DateTimePicker calendarioHasta;


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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.calendarioHasta = new System.Windows.Forms.DateTimePicker();
            this.calendarioDesde = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSalir = new System.Windows.Forms.Button();
            this.buttonLimpiarDatos = new System.Windows.Forms.Button();
            this.descripcionBajaLabel = new System.Windows.Forms.Label();
            this.descripcionBajaText = new System.Windows.Forms.TextBox();
            this.bajaHotel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.calendarioHasta);
            this.groupBox1.Controls.Add(this.calendarioDesde);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.buttonSalir);
            this.groupBox1.Controls.Add(this.buttonLimpiarDatos);
            this.groupBox1.Controls.Add(this.descripcionBajaLabel);
            this.groupBox1.Controls.Add(this.descripcionBajaText);
            this.groupBox1.Controls.Add(this.bajaHotel);
            this.groupBox1.Location = new System.Drawing.Point(13, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(561, 314);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Baja Hotel";
            // 
            // calendarioHasta
            // 
            this.calendarioHasta.Location = new System.Drawing.Point(95, 148);
            this.calendarioHasta.Name = "calendarioHasta";
            this.calendarioHasta.Size = new System.Drawing.Size(200, 20);
            this.calendarioHasta.TabIndex = 44;
            // 
            // calendarioDesde
            // 
            this.calendarioDesde.Location = new System.Drawing.Point(95, 87);
            this.calendarioDesde.Name = "calendarioDesde";
            this.calendarioDesde.Size = new System.Drawing.Size(200, 20);
            this.calendarioDesde.TabIndex = 43;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 42;
            this.label2.Text = "Fecha Hasta:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 41;
            this.label1.Text = "Fecha Desde:";
            // 
            // buttonSalir
            // 
            this.buttonSalir.Location = new System.Drawing.Point(376, 271);
            this.buttonSalir.Name = "buttonSalir";
            this.buttonSalir.Size = new System.Drawing.Size(75, 23);
            this.buttonSalir.TabIndex = 19;
            this.buttonSalir.Text = "Salir";
            this.buttonSalir.Click += new System.EventHandler(this.buttonSalir_Click);
            // 
            // buttonLimpiarDatos
            // 
            this.buttonLimpiarDatos.Location = new System.Drawing.Point(117, 271);
            this.buttonLimpiarDatos.Name = "buttonLimpiarDatos";
            this.buttonLimpiarDatos.Size = new System.Drawing.Size(75, 23);
            this.buttonLimpiarDatos.TabIndex = 18;
            this.buttonLimpiarDatos.Text = "Limpiar";
            this.buttonLimpiarDatos.Click += new System.EventHandler(this.buttonLimpiarDatos_Click);
            // 
            // descripcionBajaLabel
            // 
            this.descripcionBajaLabel.AutoSize = true;
            this.descripcionBajaLabel.Location = new System.Drawing.Point(23, 39);
            this.descripcionBajaLabel.Name = "descripcionBajaLabel";
            this.descripcionBajaLabel.Size = new System.Drawing.Size(66, 13);
            this.descripcionBajaLabel.TabIndex = 15;
            this.descripcionBajaLabel.Text = "Descripcion:";
            // 
            // descripcionBajaText
            // 
            this.descripcionBajaText.Location = new System.Drawing.Point(95, 36);
            this.descripcionBajaText.MaxLength = 45;
            this.descripcionBajaText.Name = "descripcionBajaText";
            this.descripcionBajaText.Size = new System.Drawing.Size(435, 20);
            this.descripcionBajaText.TabIndex = 14;
            // 
            // bajaHotel
            // 
            this.bajaHotel.Location = new System.Drawing.Point(198, 271);
            this.bajaHotel.Name = "bajaHotel";
            this.bajaHotel.Size = new System.Drawing.Size(172, 23);
            this.bajaHotel.TabIndex = 0;
            this.bajaHotel.Text = "Agregar Cierre Temporal";
            this.bajaHotel.Click += new System.EventHandler(this.bajaHotel_Click);
            // 
            // DropHotel
            // 
            this.AcceptButton = this.bajaHotel;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 334);
            this.Controls.Add(this.groupBox1);
            this.Name = "DropHotel";
            this.Text = "Crear Cierre Temporal del Hotel";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private void bajaHotel_Click(object sender, EventArgs e)
        {
            DateTime fechaDesde = calendarioDesde.Value;
            DateTime fechaHasta = calendarioHasta.Value;

            if (fechaDesde < Utils.getSystemDatetimeNow())
            {
                MessageBox.Show("No puede realizar reservas en el pasado", "Error");
                return;
            }

            if (fechaDesde > fechaHasta)
            {
                MessageBox.Show("La fecha inicio no puede ser superior a la fecha final.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (descripcionBajaText.Text.Equals(""))
                {
                    MessageBox.Show("Debe ingresar una descripción válida para el cierre temporal.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {
                        CierreTemporal cierreTemporal = new CierreTemporal(0, fechaDesde, fechaHasta, descripcionBajaText.Text, hotelBaja);
                        RepositorioHotel repoHotel = new RepositorioHotel();
                        repoHotel.crearBajaTemporal(cierreTemporal);
                        MessageBox.Show("Cierre temporal creado exitosamente.", "Gestion de Datos TP 2018 1C - LOS_BORBOTONES", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.buttonLimpiarDatos_Click(sender, e);

                    }
                    //catch (RequestInvalidoException exception)
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message, "Gestion de Datos TP 2018 1C - LOS_BORBOTONES", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
        }

        private void buttonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonLimpiarDatos_Click(object sender, EventArgs e)
        {
            limpiarDatos();
        }

        private void limpiarDatos() {
            calendarioDesde.Value = Utils.getSystemDatetimeNow().Date;
            calendarioHasta.Value = Utils.getSystemDatetimeNow().Date.AddDays(1);
            descripcionBajaText.Text = "";
        }
    }

}
