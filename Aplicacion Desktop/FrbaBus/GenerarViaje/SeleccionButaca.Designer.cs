namespace FrbaBus.GenerarViaje
{
    partial class SeleccionButaca
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
            this.labelTipo = new System.Windows.Forms.Label();
            this.cbTipo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbPiso = new System.Windows.Forms.ComboBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.dgvButacas = new System.Windows.Forms.DataGridView();
            this.btnSeleccionar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvButacas)).BeginInit();
            this.SuspendLayout();
            // 
            // labelTipo
            // 
            this.labelTipo.AutoSize = true;
            this.labelTipo.Location = new System.Drawing.Point(13, 13);
            this.labelTipo.Name = "labelTipo";
            this.labelTipo.Size = new System.Drawing.Size(80, 13);
            this.labelTipo.TabIndex = 0;
            this.labelTipo.Text = "Tipo de asiento";
            // 
            // cbTipo
            // 
            this.cbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipo.FormattingEnabled = true;
            this.cbTipo.Items.AddRange(new object[] {
            "Pasillo",
            "Ventanilla"});
            this.cbTipo.Location = new System.Drawing.Point(245, 13);
            this.cbTipo.Name = "cbTipo";
            this.cbTipo.Size = new System.Drawing.Size(121, 21);
            this.cbTipo.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Piso";
            // 
            // cbPiso
            // 
            this.cbPiso.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPiso.FormattingEnabled = true;
            this.cbPiso.Location = new System.Drawing.Point(245, 47);
            this.cbPiso.Name = "cbPiso";
            this.cbPiso.Size = new System.Drawing.Size(121, 21);
            this.cbPiso.TabIndex = 3;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(291, 94);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 4;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // dgvButacas
            // 
            this.dgvButacas.AllowUserToAddRows = false;
            this.dgvButacas.AllowUserToDeleteRows = false;
            this.dgvButacas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvButacas.Location = new System.Drawing.Point(13, 139);
            this.dgvButacas.MultiSelect = false;
            this.dgvButacas.Name = "dgvButacas";
            this.dgvButacas.ReadOnly = true;
            this.dgvButacas.RowHeadersVisible = false;
            this.dgvButacas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvButacas.Size = new System.Drawing.Size(353, 150);
            this.dgvButacas.TabIndex = 5;
            // 
            // btnSeleccionar
            // 
            this.btnSeleccionar.Location = new System.Drawing.Point(291, 305);
            this.btnSeleccionar.Name = "btnSeleccionar";
            this.btnSeleccionar.Size = new System.Drawing.Size(75, 23);
            this.btnSeleccionar.TabIndex = 6;
            this.btnSeleccionar.Text = "Seleccionar";
            this.btnSeleccionar.UseVisualStyleBackColor = true;
            this.btnSeleccionar.Click += new System.EventHandler(this.btnSeleccionar_Click);
            // 
            // SeleccionButaca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 340);
            this.Controls.Add(this.btnSeleccionar);
            this.Controls.Add(this.dgvButacas);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.cbPiso);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbTipo);
            this.Controls.Add(this.labelTipo);
            this.Name = "SeleccionButaca";
            this.Text = "SeleccionButaca";
            ((System.ComponentModel.ISupportInitialize)(this.dgvButacas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTipo;
        private System.Windows.Forms.ComboBox cbTipo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbPiso;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DataGridView dgvButacas;
        private System.Windows.Forms.Button btnSeleccionar;
    }
}