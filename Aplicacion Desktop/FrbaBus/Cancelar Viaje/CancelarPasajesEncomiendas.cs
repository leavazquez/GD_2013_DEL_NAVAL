using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrbaBus.Filtros;
using System.Data.SqlClient;

namespace FrbaBus.Cancelar_Viaje
{
    public partial class CancelarPasajesEncomiendas : Form
    {
        private ErrorProvider errorVoucher = new ErrorProvider();
        private ErrorProvider errorMotivo = new ErrorProvider();

        public CancelarPasajesEncomiendas()
        {
            InitializeComponent();
            dgvPasajesEncomiendas.Visible = false;
            btnCancelar.Visible = false;
            txtMotivo.Visible = false;
            labelMotivo.Visible = false;
            labelSeleccion.Visible = false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dgvPasajesEncomiendas.DataSource = null;
            // validar
            bool isValid = true;
            long voucher;
            if (!long.TryParse(txtVoucher.Text, out voucher))
            {
                isValid = false;
                errorVoucher.SetError(txtVoucher, "N° de voucher inválido");
            }
            if (isValid)
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(new SqlParameter("@voucher", voucher));
                parametros.Add(new SqlParameter("@fecha", Config.FechaSistema));
                DataTable items = DAC.ExecuteReader(@"SELECT id_pasaje ID, codigo_pasaje CODIGO, pasajero PASAJERO, NULL PESO, monto MONTO FROM DEL_NAVAL.pasajes p, DEL_NAVAL.viajes
                    WHERE voucher = @voucher AND fecha_salida > @fecha AND p.cancelado = 0 AND id_viaje = viaje
                    UNION 
                    SELECT id_encomienda, codigo_encomienda, NULL, peso, monto  FROM DEL_NAVAL.encomiendas e, DEL_NAVAL.viajes
                    WHERE voucher = @voucher AND fecha_salida > @fecha AND e.cancelado = 0 AND id_viaje = viaje", parametros);
                if (items.Rows.Count == 0)
                {
                    errorVoucher.SetError(txtVoucher, "Los pasajes/encomiendas correspondientes ya fueron cancelados, o su viaje realizado");
                    dgvPasajesEncomiendas.Visible = false;
                    btnCancelar.Visible = false;
                    txtMotivo.Visible = false;
                    labelMotivo.Visible = false;
                    return;
                }
                dgvPasajesEncomiendas.Visible = true;
                btnCancelar.Visible = true;
                txtMotivo.Visible = true;
                labelMotivo.Visible = true;
                labelSeleccion.Visible = true;

                dgvPasajesEncomiendas.DataSource = items;
            }
            else
            {
                dgvPasajesEncomiendas.Visible = false;
                btnCancelar.Visible = false;
                txtMotivo.Visible = false;
                labelMotivo.Visible = false;
                labelSeleccion.Visible = false;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // validar
            decimal monto = 0;
            string codigoCancelacion = Aleatorio.Nuevo(20);
            if (txtMotivo.Text != "")
            {
                foreach (DataGridViewRow item in dgvPasajesEncomiendas.SelectedRows)
                {
                    List<SqlParameter> parametros = new List<SqlParameter>();

                    parametros.Add(new SqlParameter("@codigo_devolucion", codigoCancelacion));
                    parametros.Add(new SqlParameter("@fecha", Config.FechaSistema));
                    parametros.Add(new SqlParameter("@motivo", txtMotivo.Text));

                    if (item.Cells["pasajero"].Value != DBNull.Value)
                    {
                        parametros.Add(new SqlParameter("@pasaje", int.Parse(item.Cells["id"].Value.ToString())));
                        monto += Convert.ToDecimal(DAC.ExecuteScalar(@"set dateformat dmy
                            declare @monto numeric(18,2)
                            exec del_naval.devolverPasaje @pasaje, @codigo_devolucion, @fecha, @motivo, @monto output
                            select @monto", parametros));
                        
                    }
                    else
                    {
                        parametros.Add(new SqlParameter("@encomienda", int.Parse(item.Cells["id"].Value.ToString())));
                        monto += Convert.ToDecimal(DAC.ExecuteScalar(@"set dateformat dmy
                            declare @monto numeric(18,2)
                            exec del_naval.devolverEncomienda @encomienda, @codigo_devolucion, @fecha, @motivo, @monto output
                            select @monto", parametros));
                    }
                }
                MessageBox.Show("Pasajes y/o encomiendas cancelados. Monto a devolver: " + monto.ToString());
            }
            else 
            {
                errorMotivo.SetError(txtMotivo, "Indique un motivo");
            }
        }
    }
}
