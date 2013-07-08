﻿namespace FrbaBus
{
    partial class Listado
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
            this.gbFiltros = new System.Windows.Forms.GroupBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.dgvResultados = new System.Windows.Forms.DataGridView();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.gbComandos = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).BeginInit();
            this.gbComandos.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbFiltros
            // 
            this.gbFiltros.Location = new System.Drawing.Point(13, 13);
            this.gbFiltros.Name = "gbFiltros";
            this.gbFiltros.Size = new System.Drawing.Size(505, 94);
            this.gbFiltros.TabIndex = 0;
            this.gbFiltros.TabStop = false;
            this.gbFiltros.Text = "Filtros de Búsuqeda";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(362, 132);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(75, 23);
            this.btnLimpiar.TabIndex = 1;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(443, 132);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // dgvResultados
            // 
            this.dgvResultados.AllowUserToAddRows = false;
            this.dgvResultados.AllowUserToDeleteRows = false;
            this.dgvResultados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResultados.Location = new System.Drawing.Point(13, 170);
            this.dgvResultados.MultiSelect = false;
            this.dgvResultados.Name = "dgvResultados";
            this.dgvResultados.ReadOnly = true;
            this.dgvResultados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResultados.ShowEditingIcon = false;
            this.dgvResultados.Size = new System.Drawing.Size(505, 228);
            this.dgvResultados.TabIndex = 3;
            this.dgvResultados.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dgvResultados_RowStateChanged);
            // 
            // btnModificar
            // 
            this.btnModificar.Location = new System.Drawing.Point(6, 19);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(75, 23);
            this.btnModificar.TabIndex = 4;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(87, 19);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(75, 23);
            this.btnEliminar.TabIndex = 5;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // gbComandos
            // 
            this.gbComandos.Controls.Add(this.btnModificar);
            this.gbComandos.Controls.Add(this.btnEliminar);
            this.gbComandos.Location = new System.Drawing.Point(13, 113);
            this.gbComandos.Name = "gbComandos";
            this.gbComandos.Size = new System.Drawing.Size(176, 51);
            this.gbComandos.TabIndex = 6;
            this.gbComandos.TabStop = false;
            this.gbComandos.Text = "Comandos";
            // 
            // Listado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 410);
            this.Controls.Add(this.gbComandos);
            this.Controls.Add(this.dgvResultados);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.gbFiltros);
            this.Name = "Listado";
            this.Text = "Listado";
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).EndInit();
            this.gbComandos.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbFiltros;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DataGridView dgvResultados;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.GroupBox gbComandos;
    }
}