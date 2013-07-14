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
    public partial class Listado : ABM
    {
        public Listado(): this(false)
        {

        }
        public Listado(bool seleccionable)
        {
            InitializeComponent();
            this.gbComandos.Controls.Clear();
            if (seleccionable)
            {
                Button btnSeleccionar = new Button();
                btnSeleccionar.Text = "Seleccionar";
                btnSeleccionar.AutoSize = true;
                this.gbComandos.Controls.Add(btnSeleccionar);
            }
            else
            {
                gbComandos.Visible = false;
            }
        }

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
