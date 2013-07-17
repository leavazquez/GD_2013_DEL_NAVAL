using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaBus.GenerarViaje
{
    public partial class GenerarViajes : Form
    {
        private string idMicro;
        private string idRecorrido;

        private ErrorProvider errorRecorrido = new ErrorProvider();
        private ErrorProvider errorMicro = new ErrorProvider();
        private ErrorProvider errorFechas = new ErrorProvider();

        public GenerarViajes()
        {
            InitializeComponent();
            dtpLlegada.Value = Config.FechaSistema;
            dtpSalida.Value = Config.FechaSistema;
        }

        private void btnRecorrido_Click(object sender, EventArgs e)
        {
            ListadoRecorrido listadoRecorrido = new ListadoRecorrido();
            listadoRecorrido.ShowDialog();
            if (listadoRecorrido.idRecorrido != null)
            {
                idRecorrido = listadoRecorrido.idRecorrido;
                btnRecorrido.Text = listadoRecorrido.Recorrido;
            }
        }

        private void btnMicro_Click(object sender, EventArgs e)
        {
            ListadoMicros listadoMicros = new ListadoMicros();
            listadoMicros.ShowDialog();
            if (listadoMicros.IdMicro != null)
            {
                idMicro = listadoMicros.IdMicro;
                btnMicro.Text = listadoMicros.Patente;
            }
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            bool isValid = true;
            if (this.idRecorrido == null) 
            {
                isValid = false;
                errorRecorrido.SetError(btnRecorrido, "Seleccione un recorrido");
            }
            if (this.idMicro == null)
            {
                isValid = false;
                errorMicro.SetError(btnMicro, "Seleccione un micro");
            }
            if (dtpSalida.Value < Config.FechaSistema || dtpLlegada.Value < Config.FechaSistema)
            {
                isValid = false;
                errorFechas.SetError(dtpSalida, "La fechas deben ser futura");
                return;
            }
            if (dtpLlegada.Value < dtpSalida.Value)
            {
                isValid = false;
                errorFechas.SetError(dtpSalida, "Rango de fechas inválido");
                return;
            }
            if (dtpLlegada.Value.Subtract(dtpSalida.Value).Days >= 1)
            {
                isValid = false;
                errorFechas.SetError(dtpSalida, "El viaje no puede durar más de 24 horas.");
                return;
            }
            if (isValid)
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(new SqlParameter("@recorrido", idRecorrido));
                parametros.Add(new SqlParameter("@micro", idMicro));
                parametros.Add(new SqlParameter("@salida", dtpSalida.Value));
                parametros.Add(new SqlParameter("@llegada", dtpLlegada.Value));
                int codigo = (int)DAC.ExecuteScalar(@"declare @outp int
                    exec del_naval.insertarViaje @recorrido, @micro, @salida, @llegada, @outp output
                    select @outp", parametros);
                switch (codigo)
                {
                    case 0:
                        MessageBox.Show("Viaje generado con éxito");
                        Close();
                        break;
                    case 1:
                        errorMicro.SetError(btnMicro, "Micro no disponible para la fecha seleccionada");
                        break;
                    case 2:
                        errorMicro.SetError(btnMicro, "Incompatibilidad entre tipos de serivicio de micro y recorrido");
                        break;
                }
            }
        }
    }
}
