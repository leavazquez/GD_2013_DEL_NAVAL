using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrbaBus.Compra_de_Pasajes
{
    public partial class ComprarPasajesEncomiendas : Form
    {
        private string idViaje;

        public ComprarPasajesEncomiendas()
        {
            InitializeComponent();
        }

        private void btnViaje_Click(object sender, EventArgs e)
        {
            ListadoViajes listadoViajes = new ListadoViajes();
            listadoViajes.ShowDialog();
            if (listadoViajes.IdViaje != null)
            {
                btnViaje.Text = listadoViajes.Viaje;
            }
        }
    }
}
