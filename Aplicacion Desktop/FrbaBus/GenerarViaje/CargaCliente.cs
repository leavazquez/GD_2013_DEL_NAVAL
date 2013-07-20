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

        private ErrorProvider errorDni = new ErrorProvider();
        private ErrorProvider errorNombre = new ErrorProvider();
        private ErrorProvider errorApellido = new ErrorProvider();
        private ErrorProvider errorSexo = new ErrorProvider();
        private ErrorProvider errorDireccion = new ErrorProvider();
        private ErrorProvider errorTelefono = new ErrorProvider();
        private ErrorProvider errorMail = new ErrorProvider();
        private ErrorProvider errorFechaNacimiento = new ErrorProvider();
        private ErrorProvider errorButaca = new ErrorProvider();


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
            if (seleccionButaca.IdButaca != null)
            {
                this.IdButaca = seleccionButaca.IdButaca;
                btnSeleccionarAsiento.Text = seleccionButaca.Butaca;
            }
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
            cbJubiladoPensionado.Checked = false;
            txtMail.Text = "";
            txtTelefono.Text = "";
            this.IdButaca = null;
            this.Butaca = null;
            btnSeleccionarAsiento.Text = "< Seleccione un asiento >";
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            bool isValid = true;
            int dni = 0;
            if (!int.TryParse(txtDni.Text, out dni) || dni <= 0)
            {
                isValid = false;
                errorDni.SetError(txtDni, "DNI inválido");
            }
            if (txtNombre.Text == "")
            {
                isValid = false;
                errorNombre.SetError(txtNombre, "Nombre inválido");
            }
            if (txtApellido.Text == "")
            {
                isValid = false;
                errorApellido.SetError(txtApellido, "Apellido inválido");
            }
            if (rbHombre.Checked == false && rbMujer.Checked == false)
            {
                isValid = false;
                errorSexo.SetError(rbMujer, "Elija un sexo");
            }
            if (txtDirección.Text == "")
            {
                isValid = false;
                errorDireccion.SetError(txtDirección, "Dirección inválida");
            }
            long telefono = 0;
            if (!long.TryParse(txtTelefono.Text, out telefono) || telefono <= 0)
            {
                isValid = false;
                errorTelefono.SetError(txtTelefono, "Telefono inválido");
            }
            if (txtMail.Text == "" || !txtMail.Text.Contains('@') || txtMail.Text.Contains(' '))
            {
                isValid = false;
                errorMail.SetError(txtMail, "Mail inválido");
            }
            if (dtpFechaNacimiento.Value.Date >= Config.FechaSistema.Date)
            {
                isValid = false;
                errorFechaNacimiento.SetError(dtpFechaNacimiento, "Fecha de nacimiento inválida");
            }
            if (isValid)
            {

            }
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

        private void dtpFechaNacimiento_ValueChanged(object sender, EventArgs e)
        {
            int edad = Config.FechaSistema.Date.Subtract(dtpFechaNacimiento.Value).Days / 365;
            if (edad >= 65 && rbHombre.Checked)
            {
                cbJubiladoPensionado.Checked = true;
            }
            if (edad >= 60 && rbMujer.Checked)
            {
                cbJubiladoPensionado.Checked = true;
            }
        }
    }
}
