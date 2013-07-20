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
            dgvButacas.DataSource = butacas;
            dgvButacas.Columns["micro"].Visible = false;
            dgvButacas.Columns["id_butaca"].Visible = false;
        }
    }
}
