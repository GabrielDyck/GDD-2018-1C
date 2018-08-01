namespace FrbaHotel.AbmReserva
{
    partial class ConfirmarReservaWindow
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
            this.labelTipoHabitacion = new System.Windows.Forms.Label();
            this.confirmarReservaButton = new System.Windows.Forms.Button();
            this.rechazarReservaButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelTipoHabitacion
            // 
            this.labelTipoHabitacion.AutoSize = true;
            this.labelTipoHabitacion.Location = new System.Drawing.Point(59, 85);
            this.labelTipoHabitacion.Name = "labelTipoHabitacion";
            this.labelTipoHabitacion.Size = new System.Drawing.Size(0, 13);
            this.labelTipoHabitacion.TabIndex = 25;
            // 
            // confirmarReservaButton
            // 
            this.confirmarReservaButton.Location = new System.Drawing.Point(136, 207);
            this.confirmarReservaButton.Name = "confirmarReservaButton";
            this.confirmarReservaButton.Size = new System.Drawing.Size(135, 23);
            this.confirmarReservaButton.TabIndex = 26;
            this.confirmarReservaButton.Text = "Reservar";
            this.confirmarReservaButton.Click += new System.EventHandler(this.confirmarReservaButton_Click);
            // 
            // rechazarReservaButton
            // 
            this.rechazarReservaButton.Location = new System.Drawing.Point(339, 207);
            this.rechazarReservaButton.Name = "rechazarReservaButton";
            this.rechazarReservaButton.Size = new System.Drawing.Size(135, 23);
            this.rechazarReservaButton.TabIndex = 27;
            this.rechazarReservaButton.Text = "Rechazar";
            this.rechazarReservaButton.Click += new System.EventHandler(this.rechazarReservaButton_Click);
            // 
            // ConfirmarReservaWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 352);
            this.Controls.Add(this.rechazarReservaButton);
            this.Controls.Add(this.confirmarReservaButton);
            this.Controls.Add(this.labelTipoHabitacion);
            this.Name = "ConfirmarReservaWindow";
            this.Text = "Confirmar Reserva";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTipoHabitacion;
        private System.Windows.Forms.Button confirmarReservaButton;
        private System.Windows.Forms.Button rechazarReservaButton;
    }
}