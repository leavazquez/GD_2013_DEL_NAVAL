namespace FrbaBus.Canje_de_Ptos
{
    partial class DatosCliente
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
            this.labelDni = new System.Windows.Forms.Label();
            this.txtDni = new System.Windows.Forms.TextBox();
            this.btnCanjear = new System.Windows.Forms.Button();
            this.labelCantidad = new System.Windows.Forms.Label();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelDni
            // 
            this.labelDni.AutoSize = true;
            this.labelDni.Location = new System.Drawing.Point(13, 13);
            this.labelDni.Name = "labelDni";
            this.labelDni.Size = new System.Drawing.Size(77, 13);
            this.labelDni.TabIndex = 0;
            this.labelDni.Text = "DNI del cliente";
            // 
            // txtDni
            // 
            this.txtDni.Location = new System.Drawing.Point(172, 13);
            this.txtDni.Name = "txtDni";
            this.txtDni.Size = new System.Drawing.Size(100, 20);
            this.txtDni.TabIndex = 1;
            // 
            // btnCanjear
            // 
            this.btnCanjear.Location = new System.Drawing.Point(197, 87);
            this.btnCanjear.Name = "btnCanjear";
            this.btnCanjear.Size = new System.Drawing.Size(75, 23);
            this.btnCanjear.TabIndex = 2;
            this.btnCanjear.Text = "Canjear";
            this.btnCanjear.UseVisualStyleBackColor = true;
            this.btnCanjear.Click += new System.EventHandler(this.btnCanjear_Click);
            // 
            // labelCantidad
            // 
            this.labelCantidad.AutoSize = true;
            this.labelCantidad.Location = new System.Drawing.Point(16, 48);
            this.labelCantidad.Name = "labelCantidad";
            this.labelCantidad.Size = new System.Drawing.Size(49, 13);
            this.labelCantidad.TabIndex = 3;
            this.labelCantidad.Text = "Cantidad";
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(172, 45);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(100, 20);
            this.txtCantidad.TabIndex = 4;
            // 
            // DatosCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 122);
            this.Controls.Add(this.txtCantidad);
            this.Controls.Add(this.labelCantidad);
            this.Controls.Add(this.btnCanjear);
            this.Controls.Add(this.txtDni);
            this.Controls.Add(this.labelDni);
            this.Name = "DatosCliente";
            this.Text = "DatosCliente";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelDni;
        private System.Windows.Forms.TextBox txtDni;
        private System.Windows.Forms.Button btnCanjear;
        private System.Windows.Forms.Label labelCantidad;
        private System.Windows.Forms.TextBox txtCantidad;
    }
}