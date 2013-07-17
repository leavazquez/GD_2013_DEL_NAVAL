namespace FrbaBus.GenerarViaje
{
    partial class GenerarViajes
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
            this.labelRecorrido = new System.Windows.Forms.Label();
            this.labelMicro = new System.Windows.Forms.Label();
            this.labelSalida = new System.Windows.Forms.Label();
            this.labelLlegada = new System.Windows.Forms.Label();
            this.btnRecorrido = new System.Windows.Forms.Button();
            this.btnMicro = new System.Windows.Forms.Button();
            this.dtpSalida = new System.Windows.Forms.DateTimePicker();
            this.dtpLlegada = new System.Windows.Forms.DateTimePicker();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelRecorrido
            // 
            this.labelRecorrido.AutoSize = true;
            this.labelRecorrido.Location = new System.Drawing.Point(12, 28);
            this.labelRecorrido.Name = "labelRecorrido";
            this.labelRecorrido.Size = new System.Drawing.Size(53, 13);
            this.labelRecorrido.TabIndex = 0;
            this.labelRecorrido.Text = "Recorrido";
            // 
            // labelMicro
            // 
            this.labelMicro.AutoSize = true;
            this.labelMicro.Location = new System.Drawing.Point(15, 108);
            this.labelMicro.Name = "labelMicro";
            this.labelMicro.Size = new System.Drawing.Size(33, 13);
            this.labelMicro.TabIndex = 1;
            this.labelMicro.Text = "Micro";
            // 
            // labelSalida
            // 
            this.labelSalida.AutoSize = true;
            this.labelSalida.Location = new System.Drawing.Point(12, 161);
            this.labelSalida.Name = "labelSalida";
            this.labelSalida.Size = new System.Drawing.Size(36, 13);
            this.labelSalida.TabIndex = 2;
            this.labelSalida.Text = "Salida";
            // 
            // labelLlegada
            // 
            this.labelLlegada.AutoSize = true;
            this.labelLlegada.Location = new System.Drawing.Point(12, 212);
            this.labelLlegada.Name = "labelLlegada";
            this.labelLlegada.Size = new System.Drawing.Size(90, 13);
            this.labelLlegada.TabIndex = 3;
            this.labelLlegada.Text = "Llegada estimada";
            // 
            // btnRecorrido
            // 
            this.btnRecorrido.Location = new System.Drawing.Point(90, 13);
            this.btnRecorrido.Name = "btnRecorrido";
            this.btnRecorrido.Size = new System.Drawing.Size(182, 43);
            this.btnRecorrido.TabIndex = 4;
            this.btnRecorrido.Text = "< Seleccione un Recorrido >";
            this.btnRecorrido.UseVisualStyleBackColor = true;
            this.btnRecorrido.Click += new System.EventHandler(this.btnRecorrido_Click);
            // 
            // btnMicro
            // 
            this.btnMicro.Location = new System.Drawing.Point(90, 93);
            this.btnMicro.Name = "btnMicro";
            this.btnMicro.Size = new System.Drawing.Size(182, 43);
            this.btnMicro.TabIndex = 5;
            this.btnMicro.Text = "< Seleccione un Micro >";
            this.btnMicro.UseVisualStyleBackColor = true;
            this.btnMicro.Click += new System.EventHandler(this.btnMicro_Click);
            // 
            // dtpSalida
            // 
            this.dtpSalida.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpSalida.Location = new System.Drawing.Point(12, 177);
            this.dtpSalida.Name = "dtpSalida";
            this.dtpSalida.Size = new System.Drawing.Size(200, 20);
            this.dtpSalida.TabIndex = 6;
            // 
            // dtpLlegada
            // 
            this.dtpLlegada.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpLlegada.Location = new System.Drawing.Point(12, 228);
            this.dtpLlegada.Name = "dtpLlegada";
            this.dtpLlegada.Size = new System.Drawing.Size(200, 20);
            this.dtpLlegada.TabIndex = 7;
            // 
            // btnGenerar
            // 
            this.btnGenerar.Location = new System.Drawing.Point(90, 278);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(182, 23);
            this.btnGenerar.TabIndex = 8;
            this.btnGenerar.Text = "Generar Viaje";
            this.btnGenerar.UseVisualStyleBackColor = true;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // GenerarViajes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 313);
            this.Controls.Add(this.btnGenerar);
            this.Controls.Add(this.dtpLlegada);
            this.Controls.Add(this.dtpSalida);
            this.Controls.Add(this.btnMicro);
            this.Controls.Add(this.btnRecorrido);
            this.Controls.Add(this.labelLlegada);
            this.Controls.Add(this.labelSalida);
            this.Controls.Add(this.labelMicro);
            this.Controls.Add(this.labelRecorrido);
            this.Name = "GenerarViajes";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelRecorrido;
        private System.Windows.Forms.Label labelMicro;
        private System.Windows.Forms.Label labelSalida;
        private System.Windows.Forms.Label labelLlegada;
        private System.Windows.Forms.Button btnRecorrido;
        private System.Windows.Forms.Button btnMicro;
        private System.Windows.Forms.DateTimePicker dtpSalida;
        private System.Windows.Forms.DateTimePicker dtpLlegada;
        private System.Windows.Forms.Button btnGenerar;
    }
}