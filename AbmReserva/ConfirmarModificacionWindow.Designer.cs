namespace FrbaHotel.AbmReserva
{
    partial class ConfirmarModificacionWindow
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
            this.rechazarReservaButton = new System.Windows.Forms.Button();
            this.modificarReservaButton = new System.Windows.Forms.Button();
            this.labelInformacionDeModificacion = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rechazarReservaButton
            // 
            this.rechazarReservaButton.Location = new System.Drawing.Point(561, 485);
            this.rechazarReservaButton.Name = "rechazarReservaButton";
            this.rechazarReservaButton.Size = new System.Drawing.Size(135, 23);
            this.rechazarReservaButton.TabIndex = 29;
            this.rechazarReservaButton.Text = "Rechazar";
            this.rechazarReservaButton.Click += new System.EventHandler(this.rechazarReservaButton_Click);
            // 
            // modificarReservaButton
            // 
            this.modificarReservaButton.Location = new System.Drawing.Point(358, 485);
            this.modificarReservaButton.Name = "modificarReservaButton";
            this.modificarReservaButton.Size = new System.Drawing.Size(135, 23);
            this.modificarReservaButton.TabIndex = 28;
            this.modificarReservaButton.Text = "Modificar Reserva";
            this.modificarReservaButton.Click += new System.EventHandler(this.modificarReservaButton_Click);
            // 
            // labelInformacionDeModificacion
            // 
            this.labelInformacionDeModificacion.AutoSize = true;
            this.labelInformacionDeModificacion.Location = new System.Drawing.Point(136, 57);
            this.labelInformacionDeModificacion.Name = "labelInformacionDeModificacion";
            this.labelInformacionDeModificacion.Size = new System.Drawing.Size(79, 13);
            this.labelInformacionDeModificacion.TabIndex = 30;
            this.labelInformacionDeModificacion.Text = "{{Placeholder}}";
            // 
            // ConfirmarModificacionWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 638);
            this.Controls.Add(this.labelInformacionDeModificacion);
            this.Controls.Add(this.rechazarReservaButton);
            this.Controls.Add(this.modificarReservaButton);
            this.Name = "ConfirmarModificacionWindow";
            this.Text = "ConfirmarModificacionWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button rechazarReservaButton;
        private System.Windows.Forms.Button modificarReservaButton;
        private System.Windows.Forms.Label labelInformacionDeModificacion;
    }
}