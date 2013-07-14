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
using System.Data.SqlClient;

namespace FrbaBus.Abm_Recorrido
{
    public partial class CargaRecorrido : Form
    {
        private Proposito proposito;
        private Recorrido recorrido = new Recorrido();
        private Dictionary<string, string> ciudades = new Dictionary<string, string>();
        private Dictionary<string, string> servicios = new Dictionary<string, string>();

        private ErrorProvider errorCodigo = new ErrorProvider();
        private ErrorProvider errorOrigen = new ErrorProvider();
        private ErrorProvider errorDestino = new ErrorProvider();
        private ErrorProvider errorServicio = new ErrorProvider();
        private ErrorProvider errorPrecioPasaje = new ErrorProvider();
        private ErrorProvider errorPrecioEncomienda = new ErrorProvider();

        public CargaRecorrido()
        {
            InitializeComponent();

            proposito = Proposito.Alta;
            this.Text = proposito + " de Recorrido";
            cargaCiudades();
            cargaComboCiudad(cbOrigen);
            cargaComboCiudad(cbDestino);
            cargaServicio();
        }

        public CargaRecorrido(Recorrido recorrido)
        {
            InitializeComponent();

            proposito = Proposito.Modificacion;
            this.recorrido = recorrido;
            this.Text = proposito + " de Recorrido";

            cargaCiudades();
            cargaComboCiudad(cbOrigen);
            cargaComboCiudad(cbDestino);
            cargaServicio();

            // recuperación de datos
            txtCodigo.Text = recorrido.Codigo;
            cbOrigen.Text = ciudades.First<KeyValuePair<string, string>>(x => x.Value == recorrido.Id_origen).Key;
            cbDestino.Text = ciudades.First<KeyValuePair<string, string>>(x => x.Value == recorrido.Id_destino).Key;
            cbServicio.Text = servicios.First<KeyValuePair<string, string>>(x => x.Value == recorrido.Id_Servicio).Key;
            txtPrecioBase.Text = recorrido.Precio_Pasaje.ToString();
            txtPrecioEncomienda.Text = recorrido.Precio_Encomienda.ToString();
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
                this.servicios.Add(servicio["nombre_servicio"].ToString(), servicio["id_servicio"].ToString());
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
            float precioPasaje = 0;
            if (txtPrecioBase.Text == "" || !float.TryParse(txtPrecioBase.Text, out precioPasaje))
            {
                isValid = false;
                errorPrecioPasaje.SetError(txtPrecioBase, "Precio inválido");
            }
            float precioEncomienda = 0;
            if (txtPrecioEncomienda.Text == "" || !float.TryParse(txtPrecioEncomienda.Text, out precioEncomienda))
            {
                isValid = false;
                errorPrecioEncomienda.SetError(txtPrecioEncomienda, "Precio inválido");
            }
            if (isValid)
            {
                // validar unicidad
                List<SqlParameter> parametrosRecorrido = new List<SqlParameter>();
                parametrosRecorrido.Add(new SqlParameter("@cod_rec", txtCodigo.Text));
                parametrosRecorrido.Add(new SqlParameter("@id_origen", ciudades[cbOrigen.SelectedItem.ToString()]));
                parametrosRecorrido.Add(new SqlParameter("@id_destino", ciudades[cbDestino.SelectedItem.ToString()]));
                parametrosRecorrido.Add(new SqlParameter("@id_servicio", servicios[cbServicio.SelectedItem.ToString()]));
                parametrosRecorrido.Add(new SqlParameter("@precio_pasaje", precioPasaje));
                parametrosRecorrido.Add(new SqlParameter("@precio_encomienda", precioEncomienda));
                string queryUnicidad = "SELECT COUNT(*) FROM DEL_NAVAL.RECORRIDOS WHERE (CODIGO_RECORRIDO =  @cod_rec OR (ORIGEN = @id_origen AND DESTINO = @id_destino AND TIPO_SERVICIO = @id_servicio))";
                if (proposito == Proposito.Modificacion)
                {
                    parametrosRecorrido.Add( new SqlParameter("@id_rec", recorrido.Id_recorrido));
                    queryUnicidad += " AND ID_RECORRIDO <> @id_rec";
                }
                int cantidad = (int)DAC.ExecuteScalar(queryUnicidad, parametrosRecorrido);
                if (cantidad == 0)
                {
                    // Crear/Modificar recorrido
                    switch (proposito)
                    {
                        case Proposito.Alta:
                            DAC.ExecuteNonQuery("INSERT INTO DEL_NAVAL.RECORRIDOS VALUES (@cod_rec, @id_origen, @id_destino, @id_servicio, @precio_pasaje, @precio_encomienda, 0)", parametrosRecorrido);
                            break;
                        case Proposito.Modificacion:
                            DAC.ExecuteNonQuery("UPDATE DEL_NAVAL.RECORRIDOS SET CODIGO_RECORRIDO = @cod_rec, ORIGEN = @id_origen, DESTINO = @id_destino, TIPO_SERVICIO = @id_servicio, PRECIO_BASE_PASAJE = @precio_pasaje, PRECIO_KG_ENCOMIENDA = @precio_encomienda WHERE ID_RECORRIDO = @id_rec", parametrosRecorrido);
                            break;
                    }
                    MessageBox.Show(proposito.ToString() + " de Recorrido realizada con éxito");
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    errorCodigo.SetError(txtCodigo, "Un recorrido con ese código ya existe");
                }
            }
        }
    }
}
