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
    public partial class SeleccionButaca : Form
    {
        public string IdViaje;
        public string IdButaca;
        public string Butaca;
        public List<string> ButacasAExcluir = new List<string>();

        private ErrorProvider errorButaca = new ErrorProvider();

        public SeleccionButaca(int cantidadPisos)
        {
            InitializeComponent();
            cbTipo.SelectedItem = "Pasillo";
            cbPiso.Items.Add("1");
            if (cantidadPisos == 2)
            {
                cbPiso.Items.Add("2");
            }
            cbPiso.SelectedItem = "1";
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@id_viaje", this.IdViaje));
            DataTable butacas = DAC.ExecuteReader("select * from del_naval.ButacasDisponiblesXviaje(@id_viaje)", parametros);
            List<DataRow> aEliminar = new List<DataRow>();
            foreach (DataRow fila in butacas.Rows)
            {
                if (fila["piso"].ToString() != cbPiso.SelectedItem.ToString() || fila["tipo"].ToString() != cbTipo.SelectedItem.ToString() || this.ButacasAExcluir.Contains(fila["id_butaca"].ToString()))
                {
                    aEliminar.Add(fila);
                }
            }
            foreach (DataRow fila in aEliminar)
            {
                butacas.Rows.Remove(fila);
            }
            dgvButacas.DataSource = butacas;
            dgvButacas.Columns["micro"].Visible = false;
            dgvButacas.Columns["id_butaca"].Visible = false;
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (dgvButacas.SelectedRows.Count > 0)
            {
                this.IdButaca = dgvButacas.SelectedRows[0].Cells["id_butaca"].Value.ToString();
                this.Butaca = "Asiento #: " + dgvButacas.SelectedRows[0].Cells["numero"].Value.ToString() + " Piso: " + dgvButacas.SelectedRows[0].Cells["piso"].Value.ToString() + " - " + dgvButacas.SelectedRows[0].Cells["tipo"].Value.ToString(); ;
                this.Close();
            }
            else
            {
                errorButaca.SetError(dgvButacas, "Seleccione un asiento");
            }
        }
    }
}
