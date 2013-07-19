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
        private Dictionary<string, int> asientosViaje = new Dictionary<string, int>();
        private Dictionary<string, int> kilosViaje = new Dictionary<string, int>();

        public ComprarPasajesEncomiendas()
        {
            InitializeComponent();
        }

        private void btnViaje_Click(object sender, EventArgs e)
        {
            ListadoViajes listadoViajes = new ListadoViajes();
            //if (dgvCompras
            listadoViajes.ShowDialog();
            if (listadoViajes.IdViaje != null)
            {
                btnViaje.Text = listadoViajes.Viaje;
            }
        }
    }
}
