using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrbaBus.GenerarViaje;
using FrbaBus.Entidades;

namespace FrbaBus.Compra_de_Pasajes
{
    public partial class ComprarPasajesEncomiendas : Form
    {
        private string idViaje;
        private int cantidadPisosMicro;
        private Dictionary<string, int> asientosViaje = new Dictionary<string, int>();
        private Dictionary<string, int> kilosViaje = new Dictionary<string, int>();
        private string destinoActual;
        private string destinoObligado;
        private ErrorProvider errorKilos = new ErrorProvider();
        private ErrorProvider errorPasajero = new ErrorProvider();
        private ErrorProvider errorViaje = new ErrorProvider();
        private ErrorProvider errorItems = new ErrorProvider();
        private List<string> idsButacasAsiganadas = new List<string>();
        private bool hayDiscapacitado = false;
        private bool hayAcompanante = false;
        private string idViajeDiscapacitado;

        private List<Encomienda> encomiendas = new List<Encomienda>();
        private List<Pasaje> pasajes = new List<Pasaje>();

        public ComprarPasajesEncomiendas()
        {
            InitializeComponent();
            DataGridViewColumn detalle = new DataGridViewColumn();
            detalle.Name = "Detalle";
            DataGridViewCell celda = new DataGridViewTextBoxCell();
            detalle.Width = 419;
            detalle.CellTemplate = celda;
            dgvCompras.Columns.Add(detalle);
        }

        private void btnViaje_Click(object sender, EventArgs e)
        {
            ListadoViajes listadoViajes;
            if (this.destinoObligado != null)
            {
                listadoViajes = new ListadoViajes(this.destinoObligado);
            }
            else
            {
                listadoViajes = new ListadoViajes(null);
            }
            listadoViajes.ShowDialog();
            if (listadoViajes.IdViaje != null)
            {
                btnViaje.Text = listadoViajes.Viaje;
                btnAsignarPasajero.Enabled = true;
                btnOcupar.Enabled = true;
                idViaje = listadoViajes.IdViaje;
                cantidadPisosMicro = listadoViajes.CantidadPisos;
                if (!asientosViaje.Keys.Contains(listadoViajes.IdViaje))
                {
                    asientosViaje.Add(listadoViajes.IdViaje, listadoViajes.AsientosDisponibles);
                    kilosViaje.Add(listadoViajes.IdViaje, listadoViajes.KilosDisponibles);
                    destinoActual = listadoViajes.Destino;
                }
                labelDisponibilidad.Text = "Hay " + this.asientosViaje[this.idViaje] + " asientos disponibles y " + this.kilosViaje[this.idViaje] + " kilos libres";
            }
        }

        private void btnOcupar_Click(object sender, EventArgs e)
        {
            int kilosAOcupar = 0;
            if (this.idViaje == null)
            {
                errorViaje.SetError(btnViaje, "Primero seleccione un viaje");
                return;
            }
            if (int.TryParse(txtKilos.Text, out kilosAOcupar) && kilosAOcupar > 0)
            {
                if (kilosAOcupar <= kilosViaje[this.idViaje])
                {
                    CargaCliente cargaCliente = new CargaCliente("Encomienda", 0, false, false);
                    cargaCliente.IdViaje = this.idViaje;
                    

                    if (cargaCliente.ShowDialog() == DialogResult.OK)
                    {
                        this.kilosViaje[this.idViaje] = this.kilosViaje[this.idViaje] - kilosAOcupar;
                        labelDisponibilidad.Text = "Hay " + this.asientosViaje[this.idViaje] + " asientos disponibles y " + this.kilosViaje[this.idViaje] + " kilos libres";
                        DataGridViewRow fila = new DataGridViewRow();
                        DataGridViewTextBoxCell detalle = new DataGridViewTextBoxCell();
                        detalle.Value = btnViaje.Text + " \n " + txtKilos.Text + " Kilos " + cargaCliente.Detalle;
                        fila.Height = 50;
                        fila.Cells.Add(detalle);
                        dgvCompras.Rows.Add(fila);

                        Encomienda encomienda = new Encomienda();
                        encomienda.Viaje = this.idViaje;
                        encomienda.Peso = kilosAOcupar;
                        encomienda.Remitente = cargaCliente.Dni;
                        encomiendas.Add(encomienda);
                    }

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
            if (this.idViaje == null)
            {
                errorViaje.SetError(btnViaje, "Primero seleccione un viaje");
                return;
            }
            if (int.TryParse(txtCantidad.Text, out pasajesAAsignar) && pasajesAAsignar > 0)
            {
                if (pasajesAAsignar <= asientosViaje[this.idViaje])
                {
                    for (int i = 0; i < pasajesAAsignar; i++)
                    {
                        bool continua = false;
                        if (i != pasajesAAsignar - 1)
                        {
                            continua = true;
                        }
                        bool pedirAcompanante = false;
                        if (hayDiscapacitado && idViajeDiscapacitado == idViaje && !this.hayAcompanante)
                        {
                            pedirAcompanante = true;
                        }

                        CargaCliente cargaCliente = new CargaCliente("Pasaje", i + 1, continua, pedirAcompanante);
                        cargaCliente.HayDiscapacitado = this.hayDiscapacitado;
                        cargaCliente.butacasAExcluir = this.idsButacasAsiganadas;
                        cargaCliente.IdViaje = this.idViaje;
                        cargaCliente.CantidadPisosMicro = this.cantidadPisosMicro;
                        cargaCliente.ShowDialog();
                        
                        if (cargaCliente.IdButaca != null)
                        {
                            this.asientosViaje[this.idViaje] = this.asientosViaje[this.idViaje] - 1;
                            labelDisponibilidad.Text = "Hay " + this.asientosViaje[this.idViaje] + " asientos disponibles y " + this.kilosViaje[this.idViaje] + " kilos libres";
                            DataGridViewRow fila = new DataGridViewRow();
                            DataGridViewTextBoxCell detalle = new DataGridViewTextBoxCell();
                            detalle.Value = btnViaje.Text + " \n " + cargaCliente.Detalle;
                            fila.Height = 50;
                            fila.Cells.Add(detalle);
                            dgvCompras.Rows.Add(fila);
                            if (cargaCliente.Discapacitado)
                            {
                                this.hayDiscapacitado = true;
                                this.idViajeDiscapacitado = this.idViaje;
                            }
                            this.hayAcompanante = cargaCliente.EsAcompanante;
                            this.idsButacasAsiganadas.Add(cargaCliente.IdButaca);

                            Pasaje pasaje = new Pasaje();
                            pasaje.Viaje = this.idViaje;
                            pasaje.Butaca = cargaCliente.IdButaca;
                            pasaje.Pasajero = cargaCliente.Dni;
                            pasaje.Gratis = cargaCliente.Discapacitado || cargaCliente.EsAcompanante;
                            pasajes.Add(pasaje);
                        }
                        
                    }
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

        private void btnComprar_Click(object sender, EventArgs e)
        {
            if (dgvCompras.Rows.Count != 0)
            {
                CargaCliente cargaCliente = new CargaCliente("Compra", 0, false, false);
                if (cargaCliente.ShowDialog() == DialogResult.OK)
                {
                    // comprar
                }
            }
            else
            {
                errorItems.SetError(dgvCompras, "No hay items a comprar");
            }
        }
    }
}
