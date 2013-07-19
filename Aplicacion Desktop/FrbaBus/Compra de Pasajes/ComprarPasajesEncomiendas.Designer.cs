namespace FrbaBus.Compra_de_Pasajes
{
    partial class ComprarPasajesEncomiendas
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
            this.labelViaje = new System.Windows.Forms.Label();
            this.btnViaje = new System.Windows.Forms.Button();
            this.labelCantidad = new System.Windows.Forms.Label();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.btnAsignarPasajero = new System.Windows.Forms.Button();
            this.labelKilos = new System.Windows.Forms.Label();
            this.txtKilos = new System.Windows.Forms.TextBox();
            this.btnOcupar = new System.Windows.Forms.Button();
            this.dgvCompras = new System.Windows.Forms.DataGridView();
            this.btnComprar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompras)).BeginInit();
            this.SuspendLayout();
            // 
            // labelViaje
            // 
            this.labelViaje.AutoSize = true;
            this.labelViaje.Location = new System.Drawing.Point(12, 28);
            this.labelViaje.Name = "labelViaje";
            this.labelViaje.Size = new System.Drawing.Size(30, 13);
            this.labelViaje.TabIndex = 0;
            this.labelViaje.Text = "Viaje";
            // 
            // btnViaje
            // 
            this.btnViaje.Location = new System.Drawing.Point(121, 12);
            this.btnViaje.Name = "btnViaje";
            this.btnViaje.Size = new System.Drawing.Size(311, 43);
            this.btnViaje.TabIndex = 1;
            this.btnViaje.Text = "< Seleccione un viaje >";
            this.btnViaje.UseVisualStyleBackColor = true;
            this.btnViaje.Click += new System.EventHandler(this.btnViaje_Click);
            // 
            // labelCantidad
            // 
            this.labelCantidad.AutoSize = true;
            this.labelCantidad.Location = new System.Drawing.Point(12, 91);
            this.labelCantidad.Name = "labelCantidad";
            this.labelCantidad.Size = new System.Drawing.Size(106, 13);
            this.labelCantidad.TabIndex = 2;
            this.labelCantidad.Text = "Cantidad de pasajes ";
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(124, 86);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(87, 20);
            this.txtCantidad.TabIndex = 3;
            // 
            // btnAsignarPasajero
            // 
            this.btnAsignarPasajero.Location = new System.Drawing.Point(288, 86);
            this.btnAsignarPasajero.Name = "btnAsignarPasajero";
            this.btnAsignarPasajero.Size = new System.Drawing.Size(144, 23);
            this.btnAsignarPasajero.TabIndex = 4;
            this.btnAsignarPasajero.Text = "Asignar pasajeros";
            this.btnAsignarPasajero.UseVisualStyleBackColor = true;
            this.btnAsignarPasajero.Click += new System.EventHandler(this.btnAsignarPasajero_Click);
            // 
            // labelKilos
            // 
            this.labelKilos.AutoSize = true;
            this.labelKilos.Location = new System.Drawing.Point(13, 117);
            this.labelKilos.Name = "labelKilos";
            this.labelKilos.Size = new System.Drawing.Size(101, 13);
            this.labelKilos.TabIndex = 5;
            this.labelKilos.Text = "Kgs de encomienda";
            // 
            // txtKilos
            // 
            this.txtKilos.Location = new System.Drawing.Point(124, 115);
            this.txtKilos.Name = "txtKilos";
            this.txtKilos.Size = new System.Drawing.Size(88, 20);
            this.txtKilos.TabIndex = 6;
            // 
            // btnOcupar
            // 
            this.btnOcupar.Location = new System.Drawing.Point(288, 112);
            this.btnOcupar.Name = "btnOcupar";
            this.btnOcupar.Size = new System.Drawing.Size(143, 23);
            this.btnOcupar.TabIndex = 7;
            this.btnOcupar.Text = "Ocupar";
            this.btnOcupar.UseVisualStyleBackColor = true;
            this.btnOcupar.Click += new System.EventHandler(this.btnOcupar_Click);
            // 
            // dgvCompras
            // 
            this.dgvCompras.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCompras.Location = new System.Drawing.Point(13, 167);
            this.dgvCompras.Name = "dgvCompras";
            this.dgvCompras.Size = new System.Drawing.Size(419, 135);
            this.dgvCompras.TabIndex = 8;
            this.dgvCompras.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvCompras_RowsAdded);
            // 
            // btnComprar
            // 
            this.btnComprar.Location = new System.Drawing.Point(288, 322);
            this.btnComprar.Name = "btnComprar";
            this.btnComprar.Size = new System.Drawing.Size(144, 23);
            this.btnComprar.TabIndex = 9;
            this.btnComprar.Text = "Comprar";
            this.btnComprar.UseVisualStyleBackColor = true;
            // 
            // ComprarPasajesEncomiendas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 357);
            this.Controls.Add(this.btnComprar);
            this.Controls.Add(this.dgvCompras);
            this.Controls.Add(this.btnOcupar);
            this.Controls.Add(this.txtKilos);
            this.Controls.Add(this.labelKilos);
            this.Controls.Add(this.btnAsignarPasajero);
            this.Controls.Add(this.txtCantidad);
            this.Controls.Add(this.labelCantidad);
            this.Controls.Add(this.btnViaje);
            this.Controls.Add(this.labelViaje);
            this.Name = "ComprarPasajesEncomiendas";
            this.Text = "Compra de Pasajes / Encomiendas";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompras)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelViaje;
        private System.Windows.Forms.Button btnViaje;
        private System.Windows.Forms.Label labelCantidad;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Button btnAsignarPasajero;
        private System.Windows.Forms.Label labelKilos;
        private System.Windows.Forms.TextBox txtKilos;
        private System.Windows.Forms.Button btnOcupar;
        private System.Windows.Forms.DataGridView dgvCompras;
        private System.Windows.Forms.Button btnComprar;
    }
}