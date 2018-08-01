namespace FrbaHotel.Login
{
    partial class SeleccionRolYHotel
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
            this.labelRoles = new System.Windows.Forms.Label();
            this.labelHoteles = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridRoles = new System.Windows.Forms.DataGridView();
            this.dataGridHoteles = new System.Windows.Forms.DataGridView();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridRoles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridHoteles)).BeginInit();
            this.SuspendLayout();
            // 
            // labelRoles
            // 
            this.labelRoles.AutoSize = true;
            this.labelRoles.Location = new System.Drawing.Point(9, 33);
            this.labelRoles.Name = "labelRoles";
            this.labelRoles.Size = new System.Drawing.Size(34, 13);
            this.labelRoles.TabIndex = 0;
            this.labelRoles.Text = "Roles";
            // 
            // labelHoteles
            // 
            this.labelHoteles.AutoSize = true;
            this.labelHoteles.Location = new System.Drawing.Point(11, 150);
            this.labelHoteles.Name = "labelHoteles";
            this.labelHoteles.Size = new System.Drawing.Size(43, 13);
            this.labelHoteles.TabIndex = 1;
            this.labelHoteles.Text = "Hoteles";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(240, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Debe elegir un Rol y un Hotel antes de continuar:";
            // 
            // dataGridRoles
            // 
            this.dataGridRoles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridRoles.Location = new System.Drawing.Point(9, 53);
            this.dataGridRoles.MultiSelect = false;
            this.dataGridRoles.Name = "dataGridRoles";
            this.dataGridRoles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridRoles.Size = new System.Drawing.Size(693, 91);
            this.dataGridRoles.TabIndex = 3;
            // 
            // dataGridHoteles
            // 
            this.dataGridHoteles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridHoteles.Location = new System.Drawing.Point(9, 170);
            this.dataGridHoteles.MultiSelect = false;
            this.dataGridHoteles.Name = "dataGridHoteles";
            this.dataGridHoteles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridHoteles.Size = new System.Drawing.Size(693, 255);
            this.dataGridHoteles.TabIndex = 4;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(279, 440);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 5;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(360, 440);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Cancelar";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // SeleccionRolYHotel
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 475);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.dataGridHoteles);
            this.Controls.Add(this.dataGridRoles);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelHoteles);
            this.Controls.Add(this.labelRoles);
            this.Name = "SeleccionRolYHotel";
            this.Text = "Elegir Rol y Hotel";
            this.Load += new System.EventHandler(this.SeleccionRolYHotel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridRoles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridHoteles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelRoles;
        private System.Windows.Forms.Label labelHoteles;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridRoles;
        private System.Windows.Forms.DataGridView dataGridHoteles;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
    }
}