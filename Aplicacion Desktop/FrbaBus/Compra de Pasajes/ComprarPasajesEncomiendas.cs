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
        private string destinoActual;
        private string destinoObligado;
        private ErrorProvider errorKilos = new ErrorProvider();
        private ErrorProvider errorPasajero = new ErrorProvider();

        public ComprarPasajesEncomiendas()
        {
            InitializeComponent();
        }

        private void btnViaje_Click(object sender, EventArgs e)
        {
            ListadoViajes listadoViajes = new ListadoViajes();
            if (this.destinoObligado != null)
            {
                listadoViajes.DestinoObligado = this.destinoObligado;
            }
            listadoViajes.ShowDialog();
            if (listadoViajes.IdViaje != null)
            {
                btnViaje.Text = listadoViajes.Viaje;
                btnAsignarPasajero.Enabled = true;
                btnOcupar.Enabled = true;
                idViaje = listadoViajes.IdViaje;
                if (!asientosViaje.Keys.Contains(listadoViajes.IdViaje))
                {
                    asientosViaje.Add(listadoViajes.IdViaje, listadoViajes.AsientosDisponibles);
                    kilosViaje.Add(listadoViajes.IdViaje, listadoViajes.KilosDisponibles);
                    destinoActual = listadoViajes.Destino;
                }
            }
        }

        private void btnOcupar_Click(object sender, EventArgs e)
        {
            int kilosAOcupar = 0;
            if (int.TryParse(txtKilos.Text, out kilosAOcupar) && kilosAOcupar > 0)
            {
                if (kilosAOcupar <= kilosViaje[this.idViaje])
                {
                    // lanzar cargaCliente
                    // si esta ok restar kilos del viaje
                    // insertar registro en dgv

                }
                else
                {
                    errorKilos.SetError(txtKilos, "El pedido supera la capacidad disponible de la bodega");
                }
            }
            else
            {
                errorKilos.SetError(txtKilos, "Valor inválido para la encomienda");
            }
        }

        private void btnAsignarPasajero_Click(object sender, EventArgs e)
        {
            int pasajesAAsignar = 0;
            if (int.TryParse(txtCantidad.Text, out pasajesAAsignar) && pasajesAAsignar > 0)
            {
                if (pasajesAAsignar <= asientosViaje[this.idViaje])
                {
                    // lanzar cargaCliente
                    // si esta ok restar aientos del viaje
                    // insertar registro en dgv
                }
                else
                {
                    errorPasajero.SetError(txtCantidad, "El pedido supera la cantidad de butacas disponibles");
                }
            }
            else
            {
                errorKilos.SetError(txtCantidad, "Valor inválido para la asignación de pasajes");
            }
        }

        private void dgvCompras_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (dgvCompras.Rows.Count == 1)
            {
                this.destinoObligado = this.destinoActual;
            }
        }
    }
}
