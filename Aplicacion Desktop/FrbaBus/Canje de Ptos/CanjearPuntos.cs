using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrbaBus.Canje_de_Ptos
{
    public partial class CanjearPuntos : Listado
    {
        public CanjearPuntos()
        {
            InitializeComponent();
            this.Query = "SELECT * FROM DEL_NAVAL.PRODUCTOS WHERE STOCK <> 0";
            gbComandos.Controls.Clear();
            Button btnCanjear = new Button();
            btnCanjear.Text = "Canjear";
            btnCanjear.AutoSize = true;
            this.gbComandos.Controls.Add(btnCanjear);
            btnCanjear.Click += seleccionar;
            dgvResultados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvResultados.MultiSelect = false;

            btnCanjear.Left += 20;
            btnCanjear.Top += 20;
        }

        protected override void seleccionar(object sender, EventArgs e)
        {
            string idProducto = dgvResultados.SelectedRows[0].Cells["id_producto"].Value.ToString();
            DatosCliente datosCliente = new DatosCliente(idProducto);
            if (datosCliente.ShowDialog() == DialogResult.OK)
            {
                this.Close();
            }
        }
    }
}
