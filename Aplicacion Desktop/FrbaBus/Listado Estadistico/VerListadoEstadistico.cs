using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrbaBus.Listado_Estadistico
{
    public partial class VerListadoEstadistico : Form
    {
        private DateTime desde;
        private DateTime hasta;

        public VerListadoEstadistico()
        {
            InitializeComponent();
            cbAno.SelectedIndex = 0;
            cbSemestre.SelectedIndex = 0;
        }

        private void btnPasajesComprados_Click(object sender, EventArgs e)
        {
            generarFechas();
            ListadoPasajesMasComprados listado = new ListadoPasajesMasComprados(desde, hasta);
            listado.ShowDialog();
        }

        private void btnMicrosVacios_Click(object sender, EventArgs e)
        {
            generarFechas();
            ListadoMicrosMasVacios listado = new ListadoMicrosMasVacios(desde, hasta);
            listado.ShowDialog();
        }

        private void btnPuntosAcumulados_Click(object sender, EventArgs e)
        {
            generarFechas();
            ListadoPuntosAcumulados listado = new ListadoPuntosAcumulados(desde, hasta);
            listado.ShowDialog();
        }

        private void btnPasajesCancelados_Click(object sender, EventArgs e)
        {
            generarFechas();
            ListadoPasajesCancelados listado = new ListadoPasajesCancelados(desde, hasta);
            listado.ShowDialog();
        }

        private void btnMicrosServicio_Click(object sender, EventArgs e)
        {
            generarFechas();
            ListadoDiasServicio listado = new ListadoDiasServicio(desde, hasta);
            listado.ShowDialog();
        }

        private void generarFechas()
        {
            int diaDesde = 0;
            int mesDesde = 0;
            int diaHasta = 0;
            int mesHasta = 0;

            switch (cbSemestre.SelectedItem.ToString())
            {
                case "1":
                    diaDesde = 1;
                    mesDesde = 1;
                    diaHasta = 30;
                    mesHasta = 6;
                    break;
                case "2":
                    diaDesde = 1;
                    mesDesde = 7;
                    diaHasta = 31;
                    mesHasta = 12;
                    break;
            }
            int ano = int.Parse(cbAno.SelectedItem.ToString());
            this.desde = new DateTime(ano, mesDesde, diaDesde);
            this.hasta = new DateTime(ano, mesHasta, diaHasta);
        }
    }
}
