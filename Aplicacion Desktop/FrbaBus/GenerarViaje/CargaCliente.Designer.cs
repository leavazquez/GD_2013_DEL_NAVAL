namespace FrbaBus.GenerarViaje
{
    partial class CargaCliente
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
            this.labelApellido = new System.Windows.Forms.Label();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.labelNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.labelDireccion = new System.Windows.Forms.Label();
            this.txtDirección = new System.Windows.Forms.TextBox();
            this.labelTelefono = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.labelMail = new System.Windows.Forms.Label();
            this.txtMail = new System.Windows.Forms.TextBox();
            this.labelFechaNacimiento = new System.Windows.Forms.Label();
            this.dtpFechaNacimiento = new System.Windows.Forms.DateTimePicker();
            this.labelSexo = new System.Windows.Forms.Label();
            this.rbHombre = new System.Windows.Forms.RadioButton();
            this.rbMujer = new System.Windows.Forms.RadioButton();
            this.labelAsiento = new System.Windows.Forms.Label();
            this.btnSeleccionarAsiento = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.labelDiscapacitado = new System.Windows.Forms.Label();
            this.cbDiscapacitado = new System.Windows.Forms.CheckBox();
            this.labelJubilado = new System.Windows.Forms.Label();
            this.cbJubiladoPensionado = new System.Windows.Forms.CheckBox();
            this.labelAcompanante = new System.Windows.Forms.Label();
            this.cbAcompanante = new System.Windows.Forms.CheckBox();
            this.labelFormaPago = new System.Windows.Forms.Label();
            this.cbFormaPago = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // labelDni
            // 
            this.labelDni.AutoSize = true;
            this.labelDni.Location = new System.Drawing.Point(13, 13);
            this.labelDni.Name = "labelDni";
            this.labelDni.Size = new System.Drawing.Size(26, 13);
            this.labelDni.TabIndex = 0;
            this.labelDni.Text = "DNI";
            // 
            // txtDni
            // 
            this.txtDni.Location = new System.Drawing.Point(172, 13);
            this.txtDni.MaxLength = 9;
            this.txtDni.Name = "txtDni";
            this.txtDni.Size = new System.Drawing.Size(100, 20);
            this.txtDni.TabIndex = 1;
            this.txtDni.Leave += new System.EventHandler(this.txtDni_Leave);
            // 
            // labelApellido
            // 
            this.labelApellido.AutoSize = true;
            this.labelApellido.Location = new System.Drawing.Point(12, 70);
            this.labelApellido.Name = "labelApellido";
            this.labelApellido.Size = new System.Drawing.Size(44, 13);
            this.labelApellido.TabIndex = 2;
            this.labelApellido.Text = "Apellido";
            // 
            // txtApellido
            // 
            this.txtApellido.Location = new System.Drawing.Point(172, 67);
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.Size = new System.Drawing.Size(100, 20);
            this.txtApellido.TabIndex = 3;
            // 
            // labelNombre
            // 
            this.labelNombre.AutoSize = true;
            this.labelNombre.Location = new System.Drawing.Point(13, 41);
            this.labelNombre.Name = "labelNombre";
            this.labelNombre.Size = new System.Drawing.Size(44, 13);
            this.labelNombre.TabIndex = 4;
            this.labelNombre.Text = "Nombre";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(172, 38);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(100, 20);
            this.txtNombre.TabIndex = 5;
            // 
            // labelDireccion
            // 
            this.labelDireccion.AutoSize = true;
            this.labelDireccion.Location = new System.Drawing.Point(12, 194);
            this.labelDireccion.Name = "labelDireccion";
            this.labelDireccion.Size = new System.Drawing.Size(52, 13);
            this.labelDireccion.TabIndex = 6;
            this.labelDireccion.Text = "Dirección";
            // 
            // txtDirección
            // 
            this.txtDirección.Location = new System.Drawing.Point(123, 191);
            this.txtDirección.Name = "txtDirección";
            this.txtDirección.Size = new System.Drawing.Size(149, 20);
            this.txtDirección.TabIndex = 7;
            // 
            // labelTelefono
            // 
            this.labelTelefono.AutoSize = true;
            this.labelTelefono.Location = new System.Drawing.Point(13, 224);
            this.labelTelefono.Name = "labelTelefono";
            this.labelTelefono.Size = new System.Drawing.Size(49, 13);
            this.labelTelefono.TabIndex = 8;
            this.labelTelefono.Text = "Telefono";
            // 
            // txtTelefono
            // 
            this.txtTelefono.Location = new System.Drawing.Point(172, 221);
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(100, 20);
            this.txtTelefono.TabIndex = 9;
            // 
            // labelMail
            // 
            this.labelMail.AutoSize = true;
            this.labelMail.Location = new System.Drawing.Point(13, 250);
            this.labelMail.Name = "labelMail";
            this.labelMail.Size = new System.Drawing.Size(26, 13);
            this.labelMail.TabIndex = 10;
            this.labelMail.Text = "Mail";
            // 
            // txtMail
            // 
            this.txtMail.Location = new System.Drawing.Point(123, 247);
            this.txtMail.Name = "txtMail";
            this.txtMail.Size = new System.Drawing.Size(149, 20);
            this.txtMail.TabIndex = 11;
            // 
            // labelFechaNacimiento
            // 
            this.labelFechaNacimiento.AutoSize = true;
            this.labelFechaNacimiento.Location = new System.Drawing.Point(13, 277);
            this.labelFechaNacimiento.Name = "labelFechaNacimiento";
            this.labelFechaNacimiento.Size = new System.Drawing.Size(108, 13);
            this.labelFechaNacimiento.TabIndex = 12;
            this.labelFechaNacimiento.Text = "Fecha de Nacimiento";
            // 
            // dtpFechaNacimiento
            // 
            this.dtpFechaNacimiento.Location = new System.Drawing.Point(12, 293);
            this.dtpFechaNacimiento.Name = "dtpFechaNacimiento";
            this.dtpFechaNacimiento.Size = new System.Drawing.Size(215, 20);
            this.dtpFechaNacimiento.TabIndex = 13;
            this.dtpFechaNacimiento.ValueChanged += new System.EventHandler(this.dtpFechaNacimiento_ValueChanged);
            // 
            // labelSexo
            // 
            this.labelSexo.AutoSize = true;
            this.labelSexo.Location = new System.Drawing.Point(12, 96);
            this.labelSexo.Name = "labelSexo";
            this.labelSexo.Size = new System.Drawing.Size(31, 13);
            this.labelSexo.TabIndex = 14;
            this.labelSexo.Text = "Sexo";
            // 
            // rbHombre
            // 
            this.rbHombre.AutoSize = true;
            this.rbHombre.Location = new System.Drawing.Point(124, 97);
            this.rbHombre.Name = "rbHombre";
            this.rbHombre.Size = new System.Drawing.Size(62, 17);
            this.rbHombre.TabIndex = 15;
            this.rbHombre.TabStop = true;
            this.rbHombre.Text = "Hombre";
            this.rbHombre.UseVisualStyleBackColor = true;
            // 
            // rbMujer
            // 
            this.rbMujer.AutoSize = true;
            this.rbMujer.Location = new System.Drawing.Point(193, 97);
            this.rbMujer.Name = "rbMujer";
            this.rbMujer.Size = new System.Drawing.Size(51, 17);
            this.rbMujer.TabIndex = 16;
            this.rbMujer.TabStop = true;
            this.rbMujer.Text = "Mujer";
            this.rbMujer.UseVisualStyleBackColor = true;
            // 
            // labelAsiento
            // 
            this.labelAsiento.AutoSize = true;
            this.labelAsiento.Location = new System.Drawing.Point(12, 331);
            this.labelAsiento.Name = "labelAsiento";
            this.labelAsiento.Size = new System.Drawing.Size(42, 13);
            this.labelAsiento.TabIndex = 17;
            this.labelAsiento.Text = "Asiento";
            // 
            // btnSeleccionarAsiento
            // 
            this.btnSeleccionarAsiento.Location = new System.Drawing.Point(76, 331);
            this.btnSeleccionarAsiento.Name = "btnSeleccionarAsiento";
            this.btnSeleccionarAsiento.Size = new System.Drawing.Size(196, 23);
            this.btnSeleccionarAsiento.TabIndex = 18;
            this.btnSeleccionarAsiento.Text = "< Seleccione un asiento >";
            this.btnSeleccionarAsiento.UseVisualStyleBackColor = true;
            this.btnSeleccionarAsiento.Click += new System.EventHandler(this.btnSeleccionarAsiento_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(197, 402);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 19;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(12, 402);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(75, 23);
            this.btnLimpiar.TabIndex = 20;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // labelDiscapacitado
            // 
            this.labelDiscapacitado.AutoSize = true;
            this.labelDiscapacitado.Location = new System.Drawing.Point(13, 161);
            this.labelDiscapacitado.Name = "labelDiscapacitado";
            this.labelDiscapacitado.Size = new System.Drawing.Size(75, 13);
            this.labelDiscapacitado.TabIndex = 23;
            this.labelDiscapacitado.Text = "Discapacitado";
            // 
            // cbDiscapacitado
            // 
            this.cbDiscapacitado.AutoSize = true;
            this.cbDiscapacitado.Location = new System.Drawing.Point(171, 161);
            this.cbDiscapacitado.Name = "cbDiscapacitado";
            this.cbDiscapacitado.Size = new System.Drawing.Size(15, 14);
            this.cbDiscapacitado.TabIndex = 24;
            this.cbDiscapacitado.UseVisualStyleBackColor = true;
            // 
            // labelJubilado
            // 
            this.labelJubilado.AutoSize = true;
            this.labelJubilado.Location = new System.Drawing.Point(12, 125);
            this.labelJubilado.Name = "labelJubilado";
            this.labelJubilado.Size = new System.Drawing.Size(113, 13);
            this.labelJubilado.TabIndex = 25;
            this.labelJubilado.Text = "Jubilado / Pensionado";
            // 
            // cbJubiladoPensionado
            // 
            this.cbJubiladoPensionado.AutoSize = true;
            this.cbJubiladoPensionado.Location = new System.Drawing.Point(171, 125);
            this.cbJubiladoPensionado.Name = "cbJubiladoPensionado";
            this.cbJubiladoPensionado.Size = new System.Drawing.Size(15, 14);
            this.cbJubiladoPensionado.TabIndex = 26;
            this.cbJubiladoPensionado.UseVisualStyleBackColor = true;
            // 
            // labelAcompanante
            // 
            this.labelAcompanante.AutoSize = true;
            this.labelAcompanante.Location = new System.Drawing.Point(12, 369);
            this.labelAcompanante.Name = "labelAcompanante";
            this.labelAcompanante.Size = new System.Drawing.Size(73, 13);
            this.labelAcompanante.TabIndex = 27;
            this.labelAcompanante.Text = "Acompañante";
            this.labelAcompanante.Visible = false;
            // 
            // cbAcompanante
            // 
            this.cbAcompanante.AutoSize = true;
            this.cbAcompanante.Location = new System.Drawing.Point(171, 369);
            this.cbAcompanante.Name = "cbAcompanante";
            this.cbAcompanante.Size = new System.Drawing.Size(15, 14);
            this.cbAcompanante.TabIndex = 28;
            this.cbAcompanante.UseVisualStyleBackColor = true;
            this.cbAcompanante.Visible = false;
            // 
            // labelFormaPago
            // 
            this.labelFormaPago.AutoSize = true;
            this.labelFormaPago.Location = new System.Drawing.Point(12, 370);
            this.labelFormaPago.Name = "labelFormaPago";
            this.labelFormaPago.Size = new System.Drawing.Size(79, 13);
            this.labelFormaPago.TabIndex = 29;
            this.labelFormaPago.Text = "Forma de Pago";
            this.labelFormaPago.Visible = false;
            // 
            // cbFormaPago
            // 
            this.cbFormaPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFormaPago.FormattingEnabled = true;
            this.cbFormaPago.Location = new System.Drawing.Point(151, 366);
            this.cbFormaPago.Name = "cbFormaPago";
            this.cbFormaPago.Size = new System.Drawing.Size(121, 21);
            this.cbFormaPago.TabIndex = 30;
            this.cbFormaPago.Visible = false;
            // 
            // CargaCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 437);
            this.Controls.Add(this.cbFormaPago);
            this.Controls.Add(this.labelFormaPago);
            this.Controls.Add(this.cbAcompanante);
            this.Controls.Add(this.labelAcompanante);
            this.Controls.Add(this.cbJubiladoPensionado);
            this.Controls.Add(this.labelJubilado);
            this.Controls.Add(this.cbDiscapacitado);
            this.Controls.Add(this.labelDiscapacitado);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.btnSeleccionarAsiento);
            this.Controls.Add(this.labelAsiento);
            this.Controls.Add(this.rbMujer);
            this.Controls.Add(this.rbHombre);
            this.Controls.Add(this.labelSexo);
            this.Controls.Add(this.dtpFechaNacimiento);
            this.Controls.Add(this.labelFechaNacimiento);
            this.Controls.Add(this.txtMail);
            this.Controls.Add(this.labelMail);
            this.Controls.Add(this.txtTelefono);
            this.Controls.Add(this.labelTelefono);
            this.Controls.Add(this.txtDirección);
            this.Controls.Add(this.labelDireccion);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.labelNombre);
            this.Controls.Add(this.txtApellido);
            this.Controls.Add(this.labelApellido);
            this.Controls.Add(this.txtDni);
            this.Controls.Add(this.labelDni);
            this.Name = "CargaCliente";
            this.Text = "CargaCliente";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelDni;
        private System.Windows.Forms.TextBox txtDni;
        private System.Windows.Forms.Label labelApellido;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Label labelNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label labelDireccion;
        private System.Windows.Forms.TextBox txtDirección;
        private System.Windows.Forms.Label labelTelefono;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label labelMail;
        private System.Windows.Forms.TextBox txtMail;
        private System.Windows.Forms.Label labelFechaNacimiento;
        private System.Windows.Forms.DateTimePicker dtpFechaNacimiento;
        private System.Windows.Forms.Label labelSexo;
        private System.Windows.Forms.RadioButton rbHombre;
        private System.Windows.Forms.RadioButton rbMujer;
        private System.Windows.Forms.Label labelAsiento;
        private System.Windows.Forms.Button btnSeleccionarAsiento;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Label labelDiscapacitado;
        private System.Windows.Forms.CheckBox cbDiscapacitado;
        private System.Windows.Forms.Label labelJubilado;
        private System.Windows.Forms.CheckBox cbJubiladoPensionado;
        private System.Windows.Forms.Label labelAcompanante;
        private System.Windows.Forms.CheckBox cbAcompanante;
        private System.Windows.Forms.Label labelFormaPago;
        private System.Windows.Forms.ComboBox cbFormaPago;
    }
}