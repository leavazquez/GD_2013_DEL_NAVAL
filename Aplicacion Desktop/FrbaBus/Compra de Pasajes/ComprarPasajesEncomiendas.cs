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
using System.Data.SqlClient;

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
                    List<SqlParameter> parametros = new List<SqlParameter>();
                    parametros.Add(new SqlParameter("@comprador", cargaCliente.Dni));
                    parametros.Add(new SqlParameter("@forma_pago", cargaCliente.FormaPago));
                    parametros.Add(new SqlParameter("@fecha", Config.FechaSistema));
                    int voucher = (int)DAC.ExecuteScalar(@"set dateformat dmy
                        declare @voucher int
                        exec del_naval.insertarCompra @comprador, @forma_pago, @fecha, @voucher output
                        select @voucher", parametros);
                    decimal montoTotal = 0;
                    foreach (Pasaje pasaje in pasajes)
                    {
                        List<SqlParameter> parametrosPasaje = new List<SqlParameter>();
                        parametrosPasaje.Add(new SqlParameter("@voucher", voucher));
                        parametrosPasaje.Add(new SqlParameter("@viaje", pasaje.Viaje));
                        parametrosPasaje.Add(new SqlParameter("@pasajero", pasaje.Pasajero));
                        parametrosPasaje.Add(new SqlParameter("@butaca", pasaje.Butaca));
                        DataTable datosPasaje = DAC.ExecuteReader(@"declare @codigo_pasaje int
                            declare @outp int " +
                            (pasaje.Gratis ? "set @outp = 0 " : "") +
                            @"exec del_naval.insertarPasaje @voucher, @viaje, @pasajero, @butaca, @codigo_pasaje output, @outp output
                            select @outp as monto, @codigo_pasaje as codigo", parametrosPasaje);
                        pasaje.Monto = decimal.Parse(datosPasaje.Rows[0]["monto"].ToString()) / 100;
                        pasaje.Codigo = int.Parse(datosPasaje.Rows[0]["codigo"].ToString());
                        montoTotal += pasaje.Monto;
                    }

                    foreach (Encomienda encomienda in encomiendas)
                    {
                        List<SqlParameter> parametrosEncomienda = new List<SqlParameter>();
                        parametrosEncomienda.Add(new SqlParameter("@voucher", voucher));
                        parametrosEncomienda.Add(new SqlParameter("@viaje", encomienda.Viaje));
                        parametrosEncomienda.Add(new SqlParameter("@remitente", encomienda.Remitente));
                        parametrosEncomienda.Add(new SqlParameter("@peso", encomienda.Peso));
                        DataTable datosPasaje = DAC.ExecuteReader(@"declare @codigo_encomienda int
                            declare @outp int
                            exec del_naval.insertarEncomienda @voucher, @viaje, @remitente, @peso, @codigo_encomienda  output, @outp output
                            select @outp monto, @codigo_encomienda codigo", parametrosEncomienda);
                        encomienda.Monto = decimal.Parse(datosPasaje.Rows[0]["monto"].ToString()) / 100;
                        encomienda.Codigo = int.Parse(datosPasaje.Rows[0]["codigo"].ToString());
                        montoTotal += encomienda.Monto;
                    }
                    StringBuilder mensajeFinal = new StringBuilder();
                    mensajeFinal.AppendLine("Su número de voucher es: " + voucher.ToString());
                    mensajeFinal.AppendLine("");
                    foreach (Pasaje pasaje in pasajes)
                    {
                        mensajeFinal.AppendLine("Pasaje (código): " + pasaje.Codigo.ToString() + " - Monto: " + pasaje.Monto.ToString());
                    }
                    foreach (Encomienda encomienda in encomiendas)
                    {
                        mensajeFinal.AppendLine("Encomienda (código): " + encomienda.Codigo.ToString() + " - Monto: " + encomienda.Monto.ToString());
                    }
                    mensajeFinal.AppendLine("");
                    mensajeFinal.AppendLine("El monto total es: " + montoTotal.ToString());
                    MessageBox.Show(mensajeFinal.ToString());
                    this.Close();
                }
            }
            else
            {
                errorItems.SetError(dgvCompras, "No hay items a comprar");
            }
        }
    }
}
