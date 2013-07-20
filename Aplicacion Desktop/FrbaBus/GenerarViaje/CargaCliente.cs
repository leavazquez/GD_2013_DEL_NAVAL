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
    public partial class CargaCliente : Form
    {
        public string IdButaca;
        public string Butaca;
        public string IdViaje;
        public int CantidadPisosMicro;

        public CargaCliente()
        {
            InitializeComponent();
            dtpFechaNacimiento.Value = Config.FechaSistema;
        }

        private void btnSeleccionarAsiento_Click(object sender, EventArgs e)
        {
            SeleccionButaca seleccionButaca = new SeleccionButaca(this.CantidadPisosMicro);
            seleccionButaca.IdViaje = this.IdViaje;
            seleccionButaca.ShowDialog();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtDni.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtDirección.Text = "";
            dtpFechaNacimiento.Value = Config.FechaSistema;
            rbHombre.Checked = false;
            rbMujer.Checked = false;
            cbDiscapacitado.Checked = false;
            txtMail.Text = "";
            txtTelefono.Text = "";
            this.IdButaca = null;
            this.Butaca = null;
            btnSeleccionarAsiento.Text = "< Seleccione un asiento >";
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

        }

        private void txtDni_Leave(object sender, EventArgs e)
        {
            int dni = 0;
            if (int.TryParse(txtDni.Text, out dni) && dni > 0)
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(new SqlParameter("@dni", dni));
                DataTable cliente = DAC.ExecuteReader("SELECT * FROM DEL_NAVAL.CLIENTES WHERE ID_DNI = @dni", parametros);
                if (cliente.Rows.Count > 0)
                {
                    DataRow fila = cliente.Rows[0];
                    txtNombre.Text = fila["NOMBRE"].ToString();
                    txtApellido.Text = fila["APELLIDO"].ToString();
                    if (fila["SEXO"].ToString() == "H")
                    {
                        rbHombre.Checked = true;
                        rbMujer.Checked = false;
                    }
                    if (fila["SEXO"].ToString() == "M")
                    {
                        rbHombre.Checked = false;
                        rbMujer.Checked = true;
                    }
                    cbDiscapacitado.Checked = fila["DISCAPACITADO"].ToString() == "1";
                    txtDirección.Text = fila["DIRECCION"].ToString();
                    txtTelefono.Text = fila["TELEFONO"].ToString();
                    txtMail.Text = fila["MAIL"].ToString();
                    dtpFechaNacimiento.Value = Convert.ToDateTime(fila["FECHA_NACIMIENTO"]);
                }
            }
        }
    }
}
