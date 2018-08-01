namespace FrbaHotel.AbmReserva
{
    partial class EditarReserva
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
            this.labelCodigoReserva = new System.Windows.Forms.Label();
            this.textCodigoReserva = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonCancelar = new System.Windows.Forms.Button();
            this.buttonModificar = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridReserva = new System.Windows.Forms.DataGridView();
            this.buttonBuscar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridReserva)).BeginInit();
            this.SuspendLayout();
            // 
            // labelCodigoReserva
            // 
            this.labelCodigoReserva.AutoSize = true;
            this.labelCodigoReserva.Location = new System.Drawing.Point(121, 35);
            this.labelCodigoReserva.Name = "labelCodigoReserva";
            this.labelCodigoReserva.Size = new System.Drawing.Size(101, 13);
            this.labelCodigoReserva.TabIndex = 6;
            this.labelCodigoReserva.Text = "Codigo de Reserva:";
            // 
            // textCodigoReserva
            // 
            this.textCodigoReserva.Location = new System.Drawing.Point(239, 32);
            this.textCodigoReserva.Name = "textCodigoReserva";
            this.textCodigoReserva.Size = new System.Drawing.Size(107, 20);
            this.textCodigoReserva.TabIndex = 1;
            this.textCodigoReserva.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.onlyNumeric);
            this.textCodigoReserva.MaxLength = 9;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.buttonCancelar);
            this.groupBox3.Controls.Add(this.buttonModificar);
            this.groupBox3.Location = new System.Drawing.Point(109, 218);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(303, 75);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Acciones";
            // 
            // buttonCancelar
            // 
            this.buttonCancelar.Enabled = false;
            this.buttonCancelar.Location = new System.Drawing.Point(109, 29);
            this.buttonCancelar.Name = "buttonCancelar";
            this.buttonCancelar.Size = new System.Drawing.Size(104, 23);
            this.buttonCancelar.TabIndex = 5;
            this.buttonCancelar.Text = "Cancelar Reserva";
            this.buttonCancelar.UseVisualStyleBackColor = true;
            this.buttonCancelar.Click += new System.EventHandler(this.buttonCancelar_Click);
            // 
            // buttonModificar
            // 
            this.buttonModificar.Enabled = false;
            this.buttonModificar.Location = new System.Drawing.Point(15, 29);
            this.buttonModificar.Name = "buttonModificar";
            this.buttonModificar.Size = new System.Drawing.Size(88, 23);
            this.buttonModificar.TabIndex = 4;
            this.buttonModificar.Text = "Modificar...";
            this.buttonModificar.UseVisualStyleBackColor = true;
            this.buttonModificar.Click += new System.EventHandler(this.buttonModificar_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridReserva);
            this.groupBox2.Location = new System.Drawing.Point(43, 100);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(434, 112);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Resultados";
            // 
            // dataGridReserva
            // 
            this.dataGridReserva.AllowUserToAddRows = false;
            this.dataGridReserva.AllowUserToDeleteRows = false;
            this.dataGridReserva.AllowUserToOrderColumns = true;
            this.dataGridReserva.AllowUserToResizeRows = false;
            this.dataGridReserva.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridReserva.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dataGridReserva.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dataGridReserva.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridReserva.Location = new System.Drawing.Point(6, 19);
            this.dataGridReserva.MultiSelect = false;
            this.dataGridReserva.Name = "dataGridReserva";
            this.dataGridReserva.ReadOnly = true;
            this.dataGridReserva.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridReserva.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridReserva.Size = new System.Drawing.Size(422, 73);
            this.dataGridReserva.TabIndex = 3;
            // 
            // buttonBuscar
            // 
            this.buttonBuscar.Location = new System.Drawing.Point(221, 61);
            this.buttonBuscar.Name = "buttonBuscar";
            this.buttonBuscar.Size = new System.Drawing.Size(75, 23);
            this.buttonBuscar.TabIndex = 2;
            this.buttonBuscar.Text = "Buscar";
            this.buttonBuscar.UseVisualStyleBackColor = true;
            this.buttonBuscar.Click += new System.EventHandler(this.buttonBuscar_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(219, 29);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(78, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Salir";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // EditarReserva
            // 
            this.AcceptButton = this.buttonBuscar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 326);
            this.Controls.Add(this.buttonBuscar);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.labelCodigoReserva);
            this.Controls.Add(this.textCodigoReserva);
            this.Name = "EditarReserva";
            this.Text = "Editar Reserva";
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridReserva)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelCodigoReserva;
        private System.Windows.Forms.TextBox textCodigoReserva;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonCancelar;
        private System.Windows.Forms.Button buttonModificar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridReserva;
        private System.Windows.Forms.Button buttonBuscar;
        private System.Windows.Forms.Button button1;
    }
}