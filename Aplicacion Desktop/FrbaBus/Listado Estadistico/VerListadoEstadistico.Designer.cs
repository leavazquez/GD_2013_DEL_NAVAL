namespace FrbaBus.Listado_Estadistico
{
    partial class VerListadoEstadistico
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
            this.labelAno = new System.Windows.Forms.Label();
            this.cbAno = new System.Windows.Forms.ComboBox();
            this.labelSemestre = new System.Windows.Forms.Label();
            this.cbSemestre = new System.Windows.Forms.ComboBox();
            this.btnPasajesComprados = new System.Windows.Forms.Button();
            this.btnMicrosVacios = new System.Windows.Forms.Button();
            this.btnPuntosAcumulados = new System.Windows.Forms.Button();
            this.btnPasajesCancelados = new System.Windows.Forms.Button();
            this.btnMicrosServicio = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelAno
            // 
            this.labelAno.AutoSize = true;
            this.labelAno.Location = new System.Drawing.Point(13, 13);
            this.labelAno.Name = "labelAno";
            this.labelAno.Size = new System.Drawing.Size(26, 13);
            this.labelAno.TabIndex = 0;
            this.labelAno.Text = "Año";
            // 
            // cbAno
            // 
            this.cbAno.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAno.FormattingEnabled = true;
            this.cbAno.Items.AddRange(new object[] {
            "2010",
            "2011",
            "2012",
            "2013",
            "2014",
            "2015",
            "2016",
            "2017",
            "2018",
            "2019",
            "2020"});
            this.cbAno.Location = new System.Drawing.Point(151, 10);
            this.cbAno.Name = "cbAno";
            this.cbAno.Size = new System.Drawing.Size(121, 21);
            this.cbAno.TabIndex = 1;
            // 
            // labelSemestre
            // 
            this.labelSemestre.AutoSize = true;
            this.labelSemestre.Location = new System.Drawing.Point(13, 44);
            this.labelSemestre.Name = "labelSemestre";
            this.labelSemestre.Size = new System.Drawing.Size(51, 13);
            this.labelSemestre.TabIndex = 2;
            this.labelSemestre.Text = "Semestre";
            // 
            // cbSemestre
            // 
            this.cbSemestre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSemestre.FormattingEnabled = true;
            this.cbSemestre.Items.AddRange(new object[] {
            "1",
            "2"});
            this.cbSemestre.Location = new System.Drawing.Point(151, 44);
            this.cbSemestre.Name = "cbSemestre";
            this.cbSemestre.Size = new System.Drawing.Size(121, 21);
            this.cbSemestre.TabIndex = 3;
            // 
            // btnPasajesComprados
            // 
            this.btnPasajesComprados.Location = new System.Drawing.Point(13, 84);
            this.btnPasajesComprados.Name = "btnPasajesComprados";
            this.btnPasajesComprados.Size = new System.Drawing.Size(259, 38);
            this.btnPasajesComprados.TabIndex = 4;
            this.btnPasajesComprados.Text = "Top 5 de los destinos con más pasajes comprados";
            this.btnPasajesComprados.UseVisualStyleBackColor = true;
            this.btnPasajesComprados.Click += new System.EventHandler(this.btnPasajesComprados_Click);
            // 
            // btnMicrosVacios
            // 
            this.btnMicrosVacios.Location = new System.Drawing.Point(13, 128);
            this.btnMicrosVacios.Name = "btnMicrosVacios";
            this.btnMicrosVacios.Size = new System.Drawing.Size(259, 38);
            this.btnMicrosVacios.TabIndex = 5;
            this.btnMicrosVacios.Text = "Top 5 de los destinos con micros más vacíos";
            this.btnMicrosVacios.UseVisualStyleBackColor = true;
            this.btnMicrosVacios.Click += new System.EventHandler(this.btnMicrosVacios_Click);
            // 
            // btnPuntosAcumulados
            // 
            this.btnPuntosAcumulados.Location = new System.Drawing.Point(13, 172);
            this.btnPuntosAcumulados.Name = "btnPuntosAcumulados";
            this.btnPuntosAcumulados.Size = new System.Drawing.Size(259, 38);
            this.btnPuntosAcumulados.TabIndex = 6;
            this.btnPuntosAcumulados.Text = "Top 5 de los Clientes con más puntos acumulados a la fecha";
            this.btnPuntosAcumulados.UseVisualStyleBackColor = true;
            this.btnPuntosAcumulados.Click += new System.EventHandler(this.btnPuntosAcumulados_Click);
            // 
            // btnPasajesCancelados
            // 
            this.btnPasajesCancelados.Location = new System.Drawing.Point(13, 217);
            this.btnPasajesCancelados.Name = "btnPasajesCancelados";
            this.btnPasajesCancelados.Size = new System.Drawing.Size(259, 38);
            this.btnPasajesCancelados.TabIndex = 7;
            this.btnPasajesCancelados.Text = "Top 5 de los destinos con pasajes cancelados";
            this.btnPasajesCancelados.UseVisualStyleBackColor = true;
            this.btnPasajesCancelados.Click += new System.EventHandler(this.btnPasajesCancelados_Click);
            // 
            // btnMicrosServicio
            // 
            this.btnMicrosServicio.Location = new System.Drawing.Point(13, 262);
            this.btnMicrosServicio.Name = "btnMicrosServicio";
            this.btnMicrosServicio.Size = new System.Drawing.Size(259, 38);
            this.btnMicrosServicio.TabIndex = 8;
            this.btnMicrosServicio.Text = "Top 5 de los micros con mayor cantidad de días fuera de servicio";
            this.btnMicrosServicio.UseVisualStyleBackColor = true;
            this.btnMicrosServicio.Click += new System.EventHandler(this.btnMicrosServicio_Click);
            // 
            // VerListadoEstadistico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 312);
            this.Controls.Add(this.btnMicrosServicio);
            this.Controls.Add(this.btnPasajesCancelados);
            this.Controls.Add(this.btnPuntosAcumulados);
            this.Controls.Add(this.btnMicrosVacios);
            this.Controls.Add(this.btnPasajesComprados);
            this.Controls.Add(this.cbSemestre);
            this.Controls.Add(this.labelSemestre);
            this.Controls.Add(this.cbAno);
            this.Controls.Add(this.labelAno);
            this.Name = "VerListadoEstadistico";
            this.Text = "VerListadoEstadistico";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelAno;
        private System.Windows.Forms.ComboBox cbAno;
        private System.Windows.Forms.Label labelSemestre;
        private System.Windows.Forms.ComboBox cbSemestre;
        private System.Windows.Forms.Button btnPasajesComprados;
        private System.Windows.Forms.Button btnMicrosVacios;
        private System.Windows.Forms.Button btnPuntosAcumulados;
        private System.Windows.Forms.Button btnPasajesCancelados;
        private System.Windows.Forms.Button btnMicrosServicio;
    }
}