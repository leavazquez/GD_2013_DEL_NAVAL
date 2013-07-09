using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrbaBus;
using FrbaBus.Entidades;

namespace FrbaBus.Abm_Recorrido
{
    public partial class CargaRecorrido : Form
    {
        private Proposito proposito;
        private Recorrido recorrido = new Recorrido();
        private Dictionary<string, string> ciudades = new Dictionary<string, string>();
        private Dictionary<string, string> servicios = new Dictionary<string, string>();

        private ErrorProvider errorCodigo;
        private ErrorProvider errorOrigen;
        private ErrorProvider errorDestino;
        private ErrorProvider errorServicio;
        private ErrorProvider errorPrecioPasaje;
        private ErrorProvider errorPrecioEncomienda;

        public CargaRecorrido()
        {
            InitializeComponent();

            proposito = Proposito.Alta;
            cargaCiudades();
            cargaComboCiudad(cbOrigen);
            cargaComboCiudad(cbDestino);
        }

        public CargaRecorrido(Recorrido recorrido)
        {
            InitializeComponent();

            proposito = Proposito.Modificacion;
            this.recorrido = recorrido;

            cargaCiudades();
            cargaComboCiudad(cbOrigen);
            cargaComboCiudad(cbDestino);
        }

        private void cargaCiudades()
        {
            DataTable ciudades = DAC.ExecuteReader("SELECT * FROM DEL_NAVAL.CIUDADES");
            foreach (DataRow row in ciudades.Rows)
            {
                this.ciudades.Add(row["nombre_ciudad"].ToString(), row["id_ciudad"].ToString());
            }
        }

        private void cargaComboCiudad(ComboBox cb)
        {
            foreach (KeyValuePair<string, string> ciudad in ciudades)
            {
                cb.Items.Add(ciudad.Key);
            }
        }

        private void cargaServicio()
        {
            DataTable servicios = DAC.ExecuteReader("SELECT * FROM DEL_NAVAL.TIPOS_SERVICIO");
            foreach (DataRow servicio in servicios.Rows)
            {
                this.servicios.Add(servicio["nombre_servicio"].ToString() + " (" + servicio["porcentaje"].ToString() + ")", servicio["id_servicio"].ToString());
                cbServicio.Items.Add(servicio["nombre_servicio"].ToString());
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtCodigo.Text = "";
            txtPrecioBase.Text = "";
            txtPrecioEncomienda.Text = "";
            cbServicio.SelectedItem = null;
            cbDestino.SelectedItem = null;
            cbOrigen.SelectedItem = null;
        }

        private void Guardar_Click(object sender, EventArgs e)
        {
            // validación
            bool isValid = true;
            if (txtCodigo.Text == String.Empty) // enunciado permite alfanumerico
            {
                isValid = false;
                errorCodigo.SetError(txtCodigo, "Código no válido");
            }
            if (cbOrigen.SelectedItem == null)
            {
                isValid = false;
                errorOrigen.SetError(cbOrigen, "Seleccione un origen");
            }
            if (cbDestino.SelectedItem == null)
            {
                isValid = false;
                errorDestino.SetError(cbDestino, "Selecciones un destino");
            }
            if (cbServicio.SelectedItem == null)
            {
                isValid = false;
                errorServicio.SetError(cbServicio, "Seleccione un tipo de servicio");
            }
            float precioPasaje;
            if (txtPrecioBase.Text == "" || float.TryParse(txtPrecioBase.Text, out precioPasaje))
            {
                isValid = false;
                errorPrecioPasaje.SetError(txtPrecioBase, "Precio inválido");
            }
            if (txtPrecioEncomienda.Text == "" || float.TryParse(txtPrecioEncomienda.Text, out precioPasaje))
            {
                isValid = false;
                errorPrecioEncomienda.SetError(txtPrecioEncomienda, "Precio inválido");
            }
            if (isValid)
            {
                // validar unicidad
            }
        }
    }
}
