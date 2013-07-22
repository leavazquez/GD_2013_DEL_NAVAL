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

namespace FrbaBus.Compra_de_Pasajes
{
    public partial class DatosTarjeta : Form
    {
        private Dictionary<string, string> tarjetas = new Dictionary<string, string>();
        private List<string> enCuotas = new List<string>();

        private ErrorProvider errorNumero = new ErrorProvider();
        private ErrorProvider errorCodigo = new ErrorProvider();
        private ErrorProvider errorFecha = new ErrorProvider();

        public Tarjeta Datos;

        public DatosTarjeta()
        {
            InitializeComponent();
            // traer nombres de tarjetas
            cbTipo.SelectedIndex = 0;

            DataTable datosTarjetas = DAC.ExecuteReader("SELECT * FROM DEL_NAVAL.TARJETAS");
            foreach (DataRow fila in datosTarjetas.Rows)
            {
                tarjetas.Add(fila["nombre"].ToString(), fila["id_tarjeta"].ToString());
                cbNombre.Items.Add(fila["nombre"].ToString());
                if (fila["cuotas"].ToString() == "True")
                {
                    enCuotas.Add(fila["nombre"].ToString());
                }
            }
            cbNombre.SelectedIndex = 0;
            cbCuotas.SelectedIndex = 0;
            cbTipo.SelectedIndex = 0;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            bool isValid = true;
            long numero = 0;
            if (!long.TryParse(txtNumero.Text, out numero) || numero <= 0)
            {
                isValid = false;
                errorNumero.SetError(txtNumero, "Número de tarjeta inválido");
            }
            long codigo = 0;
            if (!long.TryParse(txtCodigo.Text, out codigo) || codigo <= 0)
            {
                isValid = false;
                errorCodigo.SetError(txtCodigo, "Código de tarjeta inválido");
            }
            if (dtpFecha.Value.Date <= Config.FechaSistema)
            {
                isValid = false;
                errorFecha.SetError(dtpFecha, "Fecha de vencimiento inválida");
            }
            if (isValid)
            {
                // guardar datos de la tarjeta
                DateTime fechaVencimiento = new DateTime(dtpFecha.Value.Date.Year, dtpFecha.Value.Date.Month, 1);
                Datos = new Tarjeta();
                Datos.Codigo = txtCodigo.Text;
                Datos.IdTarjeta = tarjetas[cbNombre.SelectedItem.ToString()];
                Datos.Numero = txtNumero.Text;
                Datos.Vencimiento = fechaVencimiento;
                MessageBox.Show("Datos de la tarjeta cargados");
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void cbNombre_SelectedValueChanged(object sender, EventArgs e)
        {
            if (enCuotas.Contains(cbNombre.SelectedItem.ToString()))
            {
                labelCuotas.Visible = true;
                cbCuotas.Visible = true;
            }
            else
            {
                labelCuotas.Visible = false;
                cbCuotas.Visible = false;
            }
        }
    }
}
