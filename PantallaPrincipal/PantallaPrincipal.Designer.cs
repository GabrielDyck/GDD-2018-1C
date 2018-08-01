namespace FrbaHotel
{
    partial class PantallaPrincipal
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
            this.buttonReservar = new System.Windows.Forms.Button();
            this.buttonEditarReserva = new System.Windows.Forms.Button();
            this.buttonFuncionesAdicionales = new System.Windows.Forms.Button();
            this.buttonSalir = new System.Windows.Forms.Button();
            this.labelUsuario = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonReservar
            // 
            this.buttonReservar.Location = new System.Drawing.Point(12, 12);
            this.buttonReservar.Name = "buttonReservar";
            this.buttonReservar.Size = new System.Drawing.Size(120, 41);
            this.buttonReservar.TabIndex = 0;
            this.buttonReservar.Text = "Reservar...";
            this.buttonReservar.UseVisualStyleBackColor = true;
            this.buttonReservar.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonEditarReserva
            // 
            this.buttonEditarReserva.Location = new System.Drawing.Point(185, 12);
            this.buttonEditarReserva.Name = "buttonEditarReserva";
            this.buttonEditarReserva.Size = new System.Drawing.Size(120, 41);
            this.buttonEditarReserva.TabIndex = 1;
            this.buttonEditarReserva.Text = "Editar una reserva...";
            this.buttonEditarReserva.UseVisualStyleBackColor = true;
            this.buttonEditarReserva.Click += new System.EventHandler(this.button3_Click);
            // 
            // buttonFuncionesAdicionales
            // 
            this.buttonFuncionesAdicionales.Location = new System.Drawing.Point(352, 12);
            this.buttonFuncionesAdicionales.Name = "buttonFuncionesAdicionales";
            this.buttonFuncionesAdicionales.Size = new System.Drawing.Size(120, 41);
            this.buttonFuncionesAdicionales.TabIndex = 2;
            this.buttonFuncionesAdicionales.Text = "Funciones Adicionales";
            this.buttonFuncionesAdicionales.UseVisualStyleBackColor = true;
            this.buttonFuncionesAdicionales.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonSalir
            // 
            this.buttonSalir.Location = new System.Drawing.Point(185, 62);
            this.buttonSalir.Name = "buttonSalir";
            this.buttonSalir.Size = new System.Drawing.Size(120, 41);
            this.buttonSalir.TabIndex = 3;
            this.buttonSalir.Text = "Salir";
            this.buttonSalir.UseVisualStyleBackColor = true;
            this.buttonSalir.Click += new System.EventHandler(this.button4_Click);
            // 
            // labelUsuario
            // 
            this.labelUsuario.AutoSize = true;
            this.labelUsuario.Location = new System.Drawing.Point(12, 75);
            this.labelUsuario.Name = "labelUsuario";
            this.labelUsuario.Size = new System.Drawing.Size(121, 13);
            this.labelUsuario.TabIndex = 13;
            this.labelUsuario.Text = "Usuario: guest (invitado)";
            // 
            // PantallaPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 115);
            this.Controls.Add(this.labelUsuario);
            this.Controls.Add(this.buttonSalir);
            this.Controls.Add(this.buttonFuncionesAdicionales);
            this.Controls.Add(this.buttonEditarReserva);
            this.Controls.Add(this.buttonReservar);
            this.Name = "PantallaPrincipal";
            this.Text = "Gestión de Datos TP 2018 1C - LOS_BORBOTONES";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonReservar;
        private System.Windows.Forms.Button buttonEditarReserva;
        private System.Windows.Forms.Button buttonFuncionesAdicionales;
        private System.Windows.Forms.Button buttonSalir;
        private System.Windows.Forms.Label labelUsuario;
    }
}