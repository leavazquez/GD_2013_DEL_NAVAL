using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrbaBus
{
    public abstract partial class Listado : ABM
    {
        public Listado()
        {
            InitializeComponent();
            this.gbComandos.Controls.Clear();
            Button btnSeleccionar = new Button();
            btnSeleccionar.Text = "Seleccionar";
            btnSeleccionar.AutoSize = true;
            this.gbComandos.Controls.Add(btnSeleccionar);
            btnSeleccionar.Click += seleccionar;
            dgvResultados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvResultados.MultiSelect = false;

            btnSeleccionar.Left += 20;
            btnSeleccionar.Top += 20;
        }

        protected abstract void seleccionar(object sender, EventArgs e);

        protected override void habilitar(DataGridViewCellCollection fila)
        {
            throw new NotImplementedException();
        }

        protected override void modificar(DataGridViewCellCollection fila)
        {
            throw new NotImplementedException();
        }

        protected override void eliminar(DataGridViewCellCollection fila)
        {
            throw new NotImplementedException();
        }

        protected override void crear()
        {
            throw new NotImplementedException();
        }
    }
}
