namespace FrbaHotel.CambiarPassword
{
    partial class CambiarPasswordForm
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
            this.labelNuevaContraseña = new System.Windows.Forms.Label();
            this.textNuevaContraseña = new System.Windows.Forms.TextBox();
            this.buttonCambiarPassword = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelNuevaContraseña
            // 
            this.labelNuevaContraseña.AutoSize = true;
            this.labelNuevaContraseña.Location = new System.Drawing.Point(96, 51);
            this.labelNuevaContraseña.Name = "labelNuevaContraseña";
            this.labelNuevaContraseña.Size = new System.Drawing.Size(95, 13);
            this.labelNuevaContraseña.TabIndex = 3;
            this.labelNuevaContraseña.Text = "Nueva contraseña";
            // 
            // textNuevaContraseña
            // 
            this.textNuevaContraseña.Location = new System.Drawing.Point(66, 78);
            this.textNuevaContraseña.MaxLength = 255;
            this.textNuevaContraseña.Name = "textNuevaContraseña";
            this.textNuevaContraseña.PasswordChar = '*';
            this.textNuevaContraseña.Size = new System.Drawing.Size(162, 20);
            this.textNuevaContraseña.TabIndex = 2;
            // 
            // buttonCambiarPassword
            // 
            this.buttonCambiarPassword.Location = new System.Drawing.Point(109, 153);
            this.buttonCambiarPassword.Name = "buttonCambiarPassword";
            this.buttonCambiarPassword.Size = new System.Drawing.Size(75, 23);
            this.buttonCambiarPassword.TabIndex = 5;
            this.buttonCambiarPassword.Text = "Cambiar Password";
            this.buttonCambiarPassword.UseVisualStyleBackColor = true;
            this.buttonCambiarPassword.Click += new System.EventHandler(this.cambiarPassword_click);
            // 
            // CambiarPasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.buttonCambiarPassword);
            this.Controls.Add(this.labelNuevaContraseña);
            this.Controls.Add(this.textNuevaContraseña);
            this.Name = "CambiarPasswordForm";
            this.Text = "CambiarPasswordForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelNuevaContraseña;
        private System.Windows.Forms.TextBox textNuevaContraseña;
        private System.Windows.Forms.Button buttonCambiarPassword;
    }
}