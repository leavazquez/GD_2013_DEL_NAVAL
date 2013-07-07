namespace FrbaBus.Abm_Permisos
{
    partial class ABMRoles
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
            this.labelAccion = new System.Windows.Forms.Label();
            this.btnAlta = new System.Windows.Forms.Button();
            this.btnBajaModificacion = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelAccion
            // 
            this.labelAccion.AutoSize = true;
            this.labelAccion.Location = new System.Drawing.Point(13, 13);
            this.labelAccion.Name = "labelAccion";
            this.labelAccion.Size = new System.Drawing.Size(82, 13);
            this.labelAccion.TabIndex = 0;
            this.labelAccion.Text = "Elija una opción";
            // 
            // btnAlta
            // 
            this.btnAlta.Location = new System.Drawing.Point(16, 30);
            this.btnAlta.Name = "btnAlta";
            this.btnAlta.Size = new System.Drawing.Size(175, 23);
            this.btnAlta.TabIndex = 1;
            this.btnAlta.Text = "Alta de Rol";
            this.btnAlta.UseVisualStyleBackColor = true;
            this.btnAlta.Click += new System.EventHandler(this.btnAlta_Click);
            // 
            // btnBajaModificacion
            // 
            this.btnBajaModificacion.Location = new System.Drawing.Point(16, 60);
            this.btnBajaModificacion.Name = "btnBajaModificacion";
            this.btnBajaModificacion.Size = new System.Drawing.Size(175, 23);
            this.btnBajaModificacion.TabIndex = 2;
            this.btnBajaModificacion.Text = "Baja / Modificación de Rol";
            this.btnBajaModificacion.UseVisualStyleBackColor = true;
            this.btnBajaModificacion.Click += new System.EventHandler(this.btnBajaModificacion_Click);
            // 
            // ABMRoles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.btnBajaModificacion);
            this.Controls.Add(this.btnAlta);
            this.Controls.Add(this.labelAccion);
            this.Name = "ABMRoles";
            this.Text = "ABMRoles";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelAccion;
        private System.Windows.Forms.Button btnAlta;
        private System.Windows.Forms.Button btnBajaModificacion;
    }
}