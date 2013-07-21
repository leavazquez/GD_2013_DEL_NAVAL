namespace FrbaBus.Registrar_LLegada_Micro
{
    partial class RegistrarLlegadasDestino
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
            this.labelMicro = new System.Windows.Forms.Label();
            this.btnSeleccionarMicro = new System.Windows.Forms.Button();
            this.labelOrigen = new System.Windows.Forms.Label();
            this.cbOrigen = new System.Windows.Forms.ComboBox();
            this.labelDestino = new System.Windows.Forms.Label();
            this.cbDestino = new System.Windows.Forms.ComboBox();
            this.btnRegistrarLlegada = new System.Windows.Forms.Button();
            this.dtpLlegada = new System.Windows.Forms.DateTimePicker();
            this.labelLlegada = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelMicro
            // 
            this.labelMicro.AutoSize = true;
            this.labelMicro.Location = new System.Drawing.Point(12, 18);
            this.labelMicro.Name = "labelMicro";
            this.labelMicro.Size = new System.Drawing.Size(33, 13);
            this.labelMicro.TabIndex = 0;
            this.labelMicro.Text = "Micro";
            // 
            // btnSeleccionarMicro
            // 
            this.btnSeleccionarMicro.Location = new System.Drawing.Point(105, 13);
            this.btnSeleccionarMicro.Name = "btnSeleccionarMicro";
            this.btnSeleccionarMicro.Size = new System.Drawing.Size(167, 23);
            this.btnSeleccionarMicro.TabIndex = 1;
            this.btnSeleccionarMicro.Text = "< Seleccione un micro >";
            this.btnSeleccionarMicro.UseVisualStyleBackColor = true;
            this.btnSeleccionarMicro.Click += new System.EventHandler(this.btnSeleccionarMicro_Click);
            // 
            // labelOrigen
            // 
            this.labelOrigen.AutoSize = true;
            this.labelOrigen.Location = new System.Drawing.Point(12, 55);
            this.labelOrigen.Name = "labelOrigen";
            this.labelOrigen.Size = new System.Drawing.Size(38, 13);
            this.labelOrigen.TabIndex = 2;
            this.labelOrigen.Text = "Origen";
            // 
            // cbOrigen
            // 
            this.cbOrigen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOrigen.FormattingEnabled = true;
            this.cbOrigen.Location = new System.Drawing.Point(105, 52);
            this.cbOrigen.Name = "cbOrigen";
            this.cbOrigen.Size = new System.Drawing.Size(167, 21);
            this.cbOrigen.TabIndex = 3;
            // 
            // labelDestino
            // 
            this.labelDestino.AutoSize = true;
            this.labelDestino.Location = new System.Drawing.Point(12, 89);
            this.labelDestino.Name = "labelDestino";
            this.labelDestino.Size = new System.Drawing.Size(43, 13);
            this.labelDestino.TabIndex = 4;
            this.labelDestino.Text = "Destino";
            // 
            // cbDestino
            // 
            this.cbDestino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDestino.FormattingEnabled = true;
            this.cbDestino.Location = new System.Drawing.Point(105, 86);
            this.cbDestino.Name = "cbDestino";
            this.cbDestino.Size = new System.Drawing.Size(167, 21);
            this.cbDestino.TabIndex = 5;
            // 
            // btnRegistrarLlegada
            // 
            this.btnRegistrarLlegada.Location = new System.Drawing.Point(170, 227);
            this.btnRegistrarLlegada.Name = "btnRegistrarLlegada";
            this.btnRegistrarLlegada.Size = new System.Drawing.Size(102, 23);
            this.btnRegistrarLlegada.TabIndex = 6;
            this.btnRegistrarLlegada.Text = "Registrar llegada";
            this.btnRegistrarLlegada.UseVisualStyleBackColor = true;
            this.btnRegistrarLlegada.Click += new System.EventHandler(this.btnRegistrarLlegada_Click);
            // 
            // dtpLlegada
            // 
            this.dtpLlegada.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpLlegada.Location = new System.Drawing.Point(15, 160);
            this.dtpLlegada.Name = "dtpLlegada";
            this.dtpLlegada.Size = new System.Drawing.Size(200, 20);
            this.dtpLlegada.TabIndex = 7;
            // 
            // labelLlegada
            // 
            this.labelLlegada.AutoSize = true;
            this.labelLlegada.Location = new System.Drawing.Point(15, 141);
            this.labelLlegada.Name = "labelLlegada";
            this.labelLlegada.Size = new System.Drawing.Size(109, 13);
            this.labelLlegada.TabIndex = 8;
            this.labelLlegada.Text = "Día y hora de llegada";
            // 
            // RegistrarLlegadasDestino
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.labelLlegada);
            this.Controls.Add(this.dtpLlegada);
            this.Controls.Add(this.btnRegistrarLlegada);
            this.Controls.Add(this.cbDestino);
            this.Controls.Add(this.labelDestino);
            this.Controls.Add(this.cbOrigen);
            this.Controls.Add(this.labelOrigen);
            this.Controls.Add(this.btnSeleccionarMicro);
            this.Controls.Add(this.labelMicro);
            this.Name = "RegistrarLlegadasDestino";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelMicro;
        private System.Windows.Forms.Button btnSeleccionarMicro;
        private System.Windows.Forms.Label labelOrigen;
        private System.Windows.Forms.ComboBox cbOrigen;
        private System.Windows.Forms.Label labelDestino;
        private System.Windows.Forms.ComboBox cbDestino;
        private System.Windows.Forms.Button btnRegistrarLlegada;
        private System.Windows.Forms.DateTimePicker dtpLlegada;
        private System.Windows.Forms.Label labelLlegada;
    }
}