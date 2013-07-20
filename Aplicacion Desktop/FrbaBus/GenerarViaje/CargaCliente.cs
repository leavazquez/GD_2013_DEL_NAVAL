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
        public string Detalle;
        public int NumeroOrden;
        public bool Continua;
        public List<string> butacasAExcluir = new List<string>();
        public string Proposito;
        public bool Discapacitado = false;
        public bool EsAcompanante = false;
        public int Dni;

        public bool HayDiscapacitado;

        private ErrorProvider errorDni = new ErrorProvider();
        private ErrorProvider errorNombre = new ErrorProvider();
        private ErrorProvider errorApellido = new ErrorProvider();
        private ErrorProvider errorSexo = new ErrorProvider();
        private ErrorProvider errorDireccion = new ErrorProvider();
        private ErrorProvider errorTelefono = new ErrorProvider();
        private ErrorProvider errorMail = new ErrorProvider();
        private ErrorProvider errorFechaNacimiento = new ErrorProvider();
        private ErrorProvider errorButaca = new ErrorProvider();
        private ErrorProvider errorDiscapacitado = new ErrorProvider();

        public CargaCliente(string proposito, int numeroOrden, bool continua, bool pedirAcompanante)
        {
            InitializeComponent();
            dtpFechaNacimiento.Value = Config.FechaSistema;
            if (pedirAcompanante)
            {
                labelAcompanante.Visible = true;
                cbAcompanante.Visible = true;
            }
            this.Proposito = proposito;
            switch (this.Proposito)
            {
                case "Pasaje":
                    this.NumeroOrden = numeroOrden;
                    if (this.NumeroOrden != 0)
                    {
                        this.Text = "Pasaje #" + this.NumeroOrden;
                    }
                    this.Continua = continua;
                    if (this.Continua)
                    {
                        btnAceptar.AutoSize = true;
                        btnAceptar.Left = btnAceptar.Left - 25;
                        btnAceptar.Text += " y continuar";
                    }
                    break;
                case "Encomienda":
                    this.Text = "Datos del remitente";
                    btnSeleccionarAsiento.Visible = false;
                    labelAsiento.Visible = false;
                    break;
                case "Compra":
                    this.Text = "Datos del comprador";
                    btnSeleccionarAsiento.Visible = false;
                    labelAsiento.Visible = false;
                    labelFormaPago.Visible = true;
                    cbFormaPago.Visible = true;
                    cbFormaPago.Items.Add("Tarjeta");
                    cbFormaPago.SelectedIndex = 0;
                    if (Sesion.Iniciada)
                    {
                        cbFormaPago.Items.Add("Efectivo");
                    }
                    break;
            }
        }

        public CargaCliente()
        {
            InitializeComponent();
            dtpFechaNacimiento.Value = Config.FechaSistema;
        }

        private void btnSeleccionarAsiento_Click(object sender, EventArgs e)
        {
            int dni = 0;
            if (int.TryParse(txtDni.Text, out dni))
            {
                // chequear si el pasajero puede viajar
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(new SqlParameter("@dni", dni));
                parametros.Add(new SqlParameter("@viaje", IdViaje));
                int codigo = (int)DAC.ExecuteScalar(@"declare @outp int
                    exec del_naval.pasajeroPuedeViajar @dni, @viaje, @outp output
                    select @outp", parametros);
                if (codigo == 0)
                {
                    SeleccionButaca seleccionButaca = new SeleccionButaca(this.CantidadPisosMicro);
                    seleccionButaca.IdViaje = this.IdViaje;
                    seleccionButaca.ButacasAExcluir = this.butacasAExcluir;
                    seleccionButaca.ShowDialog();
                    if (seleccionButaca.IdButaca != null)
                    {
                        this.IdButaca = seleccionButaca.IdButaca;
                        btnSeleccionarAsiento.Text = seleccionButaca.Butaca;
                        this.Butaca = seleccionButaca.Butaca;
                    }
                }
                else
                {
                    MessageBox.Show("El pasajero ya tiene otro viaje asignado para el misno rango de fecha/hora");
                }
            }
            else
            {
                errorDni.SetError(txtDni, "DNI inválido");
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
            if (txtMail.Text != "" && ( !txtMail.Text.Contains('@') || txtMail.Text.Contains(' ') ))
            {
                isValid = false;
                errorMail.SetError(txtMail, "Mail inválido");
            }
            if (dtpFechaNacimiento.Value.Date >= Config.FechaSistema.Date)
            {
                isValid = false;
                errorFechaNacimiento.SetError(dtpFechaNacimiento, "Fecha de nacimiento inválida");
            }
            if (this.IdButaca == null && this.Proposito == "Pasaje")
            {
                isValid = false;
                errorButaca.SetError(btnSeleccionarAsiento, "Seleccione un asiento");
            }
            if (this.HayDiscapacitado && cbDiscapacitado.Checked)
            {
                isValid = false;
                errorDiscapacitado.SetError(cbDiscapacitado, "Ya hay un discapacitado en la comptra y no se permite más de uno");
            }
            if (isValid)
            {
                List<SqlParameter> parametrosCliente = new List<SqlParameter>();
                parametrosCliente.Add(new SqlParameter("@dni", dni));
                char sexo = rbHombre.Checked ? 'H' : 'M';
                parametrosCliente.Add(new SqlParameter("@nombre", txtNombre.Text));
                parametrosCliente.Add(new SqlParameter("@apellido", txtApellido.Text));
                parametrosCliente.Add(new SqlParameter("@fecha_nacimiento", dtpFechaNacimiento.Value));
                parametrosCliente.Add(new SqlParameter("@sexo", sexo));
                parametrosCliente.Add(new SqlParameter("@discapacitado", cbDiscapacitado.Checked));
                parametrosCliente.Add(new SqlParameter("@direccion", txtDirección.Text));
                parametrosCliente.Add(new SqlParameter("@telefono", txtTelefono.Text));
                parametrosCliente.Add(new SqlParameter("@jubilado_pensionado", cbJubiladoPensionado.Checked));
                if (txtMail.Text != "")
                {
                    parametrosCliente.Add(new SqlParameter("@mail", txtMail.Text));
                }
                else
                {
                    parametrosCliente.Add(new SqlParameter("@mail", DBNull.Value));
                }
                int encontrados = int.Parse(DAC.ExecuteScalar("SELECT COUNT(*) FROM DEL_NAVAL.CLIENTES WHERE ID_DNI = @dni", parametrosCliente).ToString());
                if (encontrados == 0)
                {
                    // alta
                    DAC.ExecuteNonQuery("INSERT INTO DEL_NAVAL.CLIENTES VALUES (@dni, @nombre, @apellido, @fecha_nacimiento, @sexo, @discapacitado, @jubilado_pensionado, @direccion, @telefono, @mail)", parametrosCliente);
                }
                else
                {
                    // modificación
                    DAC.ExecuteNonQuery("UPDATE DEL_NAVAL.CLIENTES SET ID_DNI = @dni, NOMBRE = @nombre, APELLIDO = @apellido, FECHA_NACIMIENTO = @fecha_nacimiento, SEXO = @sexo, DISCAPACITADO = @discapacitado, JUBILADO_PENSIONADO = @jubilado_pensionado, DIRECCION = @direccion, TELEFONO = @telefono, MAIL = @mail WHERE ID_DNI = @dni", parametrosCliente);
                }
                this.Discapacitado = cbDiscapacitado.Checked;
                this.Dni = dni;
                switch (Proposito)
                {
                    case "Pasaje":
                        this.Detalle = "DNI: " + txtDni.Text + " - " + this.Butaca;
                        MessageBox.Show("Asiento asignado");
                        this.EsAcompanante = cbAcompanante.Checked;
                        this.DialogResult = DialogResult.OK;
                        break;
                    case "Encomienda":
                        this.Detalle = "DNI: " + txtDni.Text;
                        MessageBox.Show("Datos del remitente cargados");
                        this.DialogResult = DialogResult.OK;
                        break;
                    case "Compra":
                        break;
                }
                
                Close();
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

                    if (fila["SEXO"].ToString() == "")
                    {
                        rbHombre.Checked = false;
                        rbMujer.Checked = false;
                    }
                    cbDiscapacitado.Checked = fila["DISCAPACITADO"].ToString() == "True";
                    cbJubiladoPensionado.Checked = fila["JUBILADO_PENSIONADO"].ToString() == "True";
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
