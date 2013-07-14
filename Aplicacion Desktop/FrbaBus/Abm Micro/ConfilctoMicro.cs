using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using FrbaBus.Entidades;

namespace FrbaBus.Abm_Micro
{
    public partial class ConfilctoMicro : Form
    {
        public DateTime Desde;
        public DateTime Hasta;

        private int idMicroReemplazo = -1;
        private string patenteMicroReemplazo;
        private int idMicro;
        

        public ConfilctoMicro(int IdMicro, int idMicroReemplazo)
        {
            InitializeComponent();
            this.idMicroReemplazo = idMicroReemplazo;
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@id_micro", idMicroReemplazo));
            string patente = DAC.ExecuteScalar("SELECT PATENTE FROM DEL_NAVAL.MICROS WHERE ID_MICRO = @id_micro", parametros).ToString();
            btnCrearReemplazar.Text = "Utilizar el micro " + patente + " como reemplazo";
            this.idMicro = IdMicro;
        }

        public ConfilctoMicro(int IdMicro)
        {
            InitializeComponent();
            this.idMicro = IdMicro;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@id_micro", this.idMicro));
            parametros.Add(new SqlParameter("@fecha", Config.FechaSistema));
            parametros.Add(new SqlParameter("@codigo_cancelacion", Aleatorio.Nuevo(20)));
            parametros.Add(new SqlParameter("@motivo", "Cancelado por baja/servicio de micro"));
            DAC.ExecuteNonQuery("exec cancelarViajesDeUnMicro @id_micro, @fecha, null, @codigo_cancelacion, @fecha, @motivo", parametros);
            MessageBox.Show("Viajes cancelados");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCrearReemplazar_Click(object sender, EventArgs e)
        {
            if (this.idMicroReemplazo == -1)
            {
                Form cargaMicro = new CargaMicro();
                DialogResult resultado = cargaMicro.ShowDialog();
                if (resultado == DialogResult.OK)
                {
                    Close();
                }
            }
            else
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(new SqlParameter("@id_micro", this.idMicro));
                parametros.Add(new SqlParameter("@id_micro_reemplazo", this.idMicroReemplazo));
                parametros.Add(new SqlParameter("@desde", this.Desde));
                parametros.Add(new SqlParameter("@hasta", this.Hasta));
                DAC.ExecuteNonQuery("exec reemplazarMicro @id_micro, @id_micro_reemplazo, @desde, @hasta", parametros);
                MessageBox.Show("Viajes cancelados");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
