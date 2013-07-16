namespace FrbaBus.Cancelar_Viaje
{
    partial class CancelarPasajesEncomiendas
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
            this.labelVoucher = new System.Windows.Forms.Label();
            this.txtVoucher = new System.Windows.Forms.TextBox();
            this.dgvPasajesEncomiendas = new System.Windows.Forms.DataGridView();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.txtMotivo = new System.Windows.Forms.TextBox();
            this.labelMotivo = new System.Windows.Forms.Label();
            this.labelSeleccion = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPasajesEncomiendas)).BeginInit();
            this.SuspendLayout();
            // 
            // labelVoucher
            // 
            this.labelVoucher.AutoSize = true;
            this.labelVoucher.Location = new System.Drawing.Point(13, 13);
            this.labelVoucher.Name = "labelVoucher";
            this.labelVoucher.Size = new System.Drawing.Size(76, 13);
            this.labelVoucher.TabIndex = 0;
            this.labelVoucher.Text = "N° de voucher";
            // 
            // txtVoucher
            // 
            this.txtVoucher.Location = new System.Drawing.Point(95, 10);
            this.txtVoucher.Name = "txtVoucher";
            this.txtVoucher.Size = new System.Drawing.Size(100, 20);
            this.txtVoucher.TabIndex = 1;
            // 
            // dgvPasajesEncomiendas
            // 
            this.dgvPasajesEncomiendas.AllowUserToAddRows = false;
            this.dgvPasajesEncomiendas.AllowUserToDeleteRows = false;
            this.dgvPasajesEncomiendas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPasajesEncomiendas.Location = new System.Drawing.Point(16, 64);
            this.dgvPasajesEncomiendas.Name = "dgvPasajesEncomiendas";
            this.dgvPasajesEncomiendas.RowHeadersVisible = false;
            this.dgvPasajesEncomiendas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPasajesEncomiendas.Size = new System.Drawing.Size(504, 145);
            this.dgvPasajesEncomiendas.TabIndex = 2;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(407, 35);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(100, 23);
            this.btnBuscar.TabIndex = 3;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(377, 253);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(130, 54);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = "Cancelar Pasajes/Encomiendas seleccionados";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // txtMotivo
            // 
            this.txtMotivo.Location = new System.Drawing.Point(72, 253);
            this.txtMotivo.Multiline = true;
            this.txtMotivo.Name = "txtMotivo";
            this.txtMotivo.Size = new System.Drawing.Size(276, 54);
            this.txtMotivo.TabIndex = 5;
            // 
            // labelMotivo
            // 
            this.labelMotivo.AutoSize = true;
            this.labelMotivo.Location = new System.Drawing.Point(16, 253);
            this.labelMotivo.Name = "labelMotivo";
            this.labelMotivo.Size = new System.Drawing.Size(39, 13);
            this.labelMotivo.TabIndex = 6;
            this.labelMotivo.Text = "Motivo";
            // 
            // labelSeleccion
            // 
            this.labelSeleccion.AutoSize = true;
            this.labelSeleccion.Location = new System.Drawing.Point(16, 212);
            this.labelSeleccion.Name = "labelSeleccion";
            this.labelSeleccion.Size = new System.Drawing.Size(432, 13);
            this.labelSeleccion.TabIndex = 7;
            this.labelSeleccion.Text = "Seleccione los Pasajes/Encomiendas a cancelar (mantener CTRL para selección múlti" +
                "ple)";
            // 
            // CancelarPasajesEncomiendas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 319);
            this.Controls.Add(this.labelSeleccion);
            this.Controls.Add(this.labelMotivo);
            this.Controls.Add(this.txtMotivo);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.dgvPasajesEncomiendas);
            this.Controls.Add(this.txtVoucher);
            this.Controls.Add(this.labelVoucher);
            this.Name = "CancelarPasajesEncomiendas";
            this.Text = "Cancelación de Pasajes / Encomiendas";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPasajesEncomiendas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelVoucher;
        private System.Windows.Forms.TextBox txtVoucher;
        private System.Windows.Forms.DataGridView dgvPasajesEncomiendas;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.TextBox txtMotivo;
        private System.Windows.Forms.Label labelMotivo;
        private System.Windows.Forms.Label labelSeleccion;
    }
}