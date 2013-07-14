using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrbaBus.Entidades;
using System.Data.SqlClient;

namespace FrbaBus.Abm_Micro
{
    public partial class CargaMicro : Form
    {
        public string ReemplazaA;
        private Micro micro = new Micro();
        private Proposito proposito;
        private Dictionary<string, string> servicios = new Dictionary<string, string>();
        private Dictionary<string, string> marcas = new Dictionary<string, string>();

        private ErrorProvider errorPatente = new ErrorProvider();
        private ErrorProvider errorNumero = new ErrorProvider();
        private ErrorProvider errorMarca = new ErrorProvider();
        private ErrorProvider errorModelo = new ErrorProvider();
        private ErrorProvider errorBodega = new ErrorProvider();
        private ErrorProvider errorServicio= new ErrorProvider();
        private ErrorProvider errorButacas = new ErrorProvider();
        private ErrorProvider errorFechas = new ErrorProvider();

        public CargaMicro()
        {
            InitializeComponent();
            proposito = Proposito.Alta;
            this.Text = proposito + " de Micro";

            cargaMarca();
            cargaServicio();
            cargaButacas();


            checkFueraServicio.Checked = false;
            labelDesde.Visible = false;
            labelHasta.Visible = false;
            dtpDesde.Visible = false;
            dtpHasta.Visible = false;
        }

        public CargaMicro(Micro micro)
        {
            InitializeComponent();
            this.micro = micro;
            proposito = Proposito.Modificacion;
            this.Text = proposito + " de Micro";

            cargaMarca();
            cargaServicio();
            cargaButacas();

            checkFueraServicio.Checked = micro.Baja_servicio;
            labelDesde.Visible = micro.Baja_servicio;
            labelHasta.Visible = micro.Baja_servicio;
            dtpDesde.Visible = micro.Baja_servicio;
            dtpHasta.Visible = micro.Baja_servicio;

            if (micro.Baja_servicio)
            {
                dtpDesde.Value = micro.Fecha_servicio_desde;
                dtpHasta.Value = micro.Fecha_servicio_hasta;
            }
            txtPatente.Text = micro.Patente;
            txtNumero.Text = micro.Numero;
            txtModelo.Text = micro.Modelo;
            txtBodega.Text = micro.Kilos_bodega.ToString();
            cbMarca.SelectedItem = micro.Marca;
            cbServicio.SelectedItem = micro.Id_servicio;
            List<SqlParameter> paramtetros = new List<SqlParameter>();
            paramtetros.Add(new SqlParameter("@micro", micro.Id_micro));
            DataTable butacas = DAC.ExecuteReader("SELECT NUMERO, TIPO, PISO FROM DEL_NAVAL.BUTACAS WHERE MICRO = @micro", paramtetros);
            foreach (DataRow fila in butacas.Rows)
            {
                dgvButacas.Rows.Add(fila["NUMERO"].ToString(), fila["TIPO"].ToString(), fila["PISO"].ToString());
            }
            dgvButacas.Enabled = false;
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

        private void cargaMarca()
        {
            DataTable servicios = DAC.ExecuteReader("SELECT * FROM DEL_NAVAL.MARCAS");
            foreach (DataRow servicio in servicios.Rows)
            {
                this.marcas.Add(servicio["marca"].ToString(), servicio["id_marca"].ToString());
                cbMarca.Items.Add(servicio["marca"].ToString());
            }
        }

        private void cargaButacas()
        {
            DataGridViewComboBoxColumn tipo = new DataGridViewComboBoxColumn();
            tipo.HeaderText = "Tipo";
            tipo.Name = "tipo";
            tipo.Items.Add("Pasillo");
            tipo.Items.Add("Ventanilla");
            DataGridViewComboBoxColumn piso = new DataGridViewComboBoxColumn();
            piso.HeaderText = "Piso";
            piso.Name = "piso";
            piso.Items.Add("1");
            piso.Items.Add("2");
            dgvButacas.Columns.Add("numero", "Número");
            dgvButacas.Columns.Add(tipo);
            dgvButacas.Columns.Add(piso);
        }

        private void CargaMicro_Click(object sender, EventArgs e)
        {
            if (checkFueraServicio.Checked)
            {
                labelDesde.Visible = true;
                labelHasta.Visible = true;
                dtpDesde.Visible = true;
                dtpHasta.Visible = true;
            }
            else
            {
                labelDesde.Visible = false;
                labelHasta.Visible = false;
                dtpDesde.Visible = false;
                dtpHasta.Visible = false;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtPatente.Text = "";
            txtNumero.Text = "";
            txtModelo.Text = "";
            txtBodega.Text = "";
            cbMarca.SelectedItem = null;
            cbServicio.SelectedItem = null;
            if (dgvButacas.Enabled)
            {
                dgvButacas.Rows.Clear();
            }
            checkFueraServicio.Checked = false;
            labelDesde.Visible = false;
            labelHasta.Visible = false;
            dtpDesde.Visible = false;
            dtpHasta.Visible = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // validar
            bool isValid = true;
            if (txtPatente.Text.Length < 7 || !txtPatente.Text.Substring(0, 3).All(char.IsLetter) || !txtPatente.Text.Substring(4, 3).All(char.IsDigit) || txtPatente.Text[3] != '-')
            {
                isValid = false;
                errorPatente.SetError(txtPatente, "Patente inválida");
            }
            int numero = 0;
            if (txtNumero.Text == "" || !int.TryParse(txtNumero.Text, out numero))
            {
                isValid = false;
                errorNumero.SetError(txtNumero, "Número invalido");
            }
            if (cbMarca.SelectedItem == null)
            {
                isValid = false;
                errorMarca.SetError(cbMarca, "Seleccione una marca");
            }
            if (txtModelo.Text == "")
            {
                isValid = false;
                errorModelo.SetError(txtModelo, "Modelo inválido");
            }
            if (cbServicio.SelectedItem == null)
            {
                isValid = false;
                errorServicio.SetError(cbServicio, "Seleccione un tipo de servicio");
            }
            float bodega = 0;
            if (txtBodega.Text == "" || !float.TryParse(txtBodega.Text, out bodega))
            {
                isValid = false;
                errorBodega.SetError(txtBodega, "Valor inválido");
            }
            if (checkFueraServicio.Checked && dtpDesde.Value.Date > dtpHasta.Value.Date)
            {
                isValid = false;
                errorFechas.SetError(checkFueraServicio, "Rango de fechas inválido");
            }
            int butacasValidas = 0;
            foreach (DataGridViewRow fila in dgvButacas.Rows)
            {
                int numeroMicro = 0;
                if (fila.Cells["numero"].Value != null)
                {
                    if (!int.TryParse(fila.Cells["numero"].Value.ToString(), out numeroMicro) || fila.Cells["tipo"].Value == null || fila.Cells["piso"].Value == null)
                    {
                        isValid = false;
                        errorButacas.SetError(dgvButacas, "Alguna(s) butacas tiene(n) datos inválidos");
                    }
                    else
                    {
                        butacasValidas++;
                    }
                }
            }
            if (isValid)
            {
                // validar unicidad
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(new SqlParameter("@patente", txtPatente.Text));
                string queryUnicidad = "SELECT COUNT(*) FROM DEL_NAVAL.MICROS WHERE PATENTE = @patente";
                if (proposito == Proposito.Modificacion)
                {
                    parametros.Add(new SqlParameter("@id_micro", micro.Id_micro));
                    queryUnicidad += " AND ID_MICRO <> @id_micro";
                }
                int cantidad = (int)DAC.ExecuteScalar(queryUnicidad, parametros);
                if (cantidad == 0)
                {
                    // crear/modificar el micro
                    parametros.Add(new SqlParameter("@numero", int.Parse(txtNumero.Text)));
                    parametros.Add(new SqlParameter("@marca", marcas[cbMarca.SelectedItem.ToString()]));
                    parametros.Add(new SqlParameter("@modelo", txtModelo.Text));
                    parametros.Add(new SqlParameter("@servicio", servicios[cbServicio.SelectedItem.ToString()]));
                    parametros.Add(new SqlParameter("@bodega", txtBodega.Text));
                    parametros.Add(new SqlParameter("@alta", Config.FechaSistema));
                    parametros.Add(new SqlParameter("@asientos", butacasValidas));
                    if (checkFueraServicio.Checked)
                    {
                        parametros.Add(new SqlParameter("@baja_servicio", checkFueraServicio.Checked));
                        parametros.Add(new SqlParameter("@servicio_desde", dtpDesde.Value.Date));
                        parametros.Add(new SqlParameter("@servicio_hasta", dtpHasta.Value.Date));
                    }
                    else
                    {
                        parametros.Add(new SqlParameter("@baja_servicio", false));
                    }
                    switch (proposito)
                    {
                        case Proposito.Alta:
                            if (checkFueraServicio.Checked)
                            {
                                DAC.ExecuteNonQuery("INSERT INTO DEL_NAVAL.MICROS VALUES (@numero, @servicio, @bodega, @asientos, @marca, @modelo, @patente, 0, @baja_servicio, @alta, @servicio_desde, @servicio_hasta, null)", parametros);
                            }
                            else
                            {
                                DAC.ExecuteNonQuery("INSERT INTO DEL_NAVAL.MICROS VALUES (@numero, @servicio, @bodega, @asientos, @marca, @modelo, @patente, 0, 0, @alta, null, null, null)", parametros);
                            }

                            int idMicroCreado = (int)DAC.ExecuteScalar("SELECT ID_MICRO FROM DEL_NAVAL.MICROS WHERE PATENTE = @patente", parametros);

                            // carga asientos
                            foreach (DataGridViewRow fila in dgvButacas.Rows)
                            {
                                if (fila.Cells["numero"].Value != null)
                                {
                                    List<SqlParameter> parametrosButaca = new List<SqlParameter>();
                                    parametrosButaca.Add(new SqlParameter("@micro", idMicroCreado));
                                    parametrosButaca.Add(new SqlParameter("@tipo", fila.Cells["tipo"].Value.ToString()));
                                    parametrosButaca.Add(new SqlParameter("@piso", fila.Cells["piso"].Value.ToString()));
                                    parametrosButaca.Add(new SqlParameter("@numero", int.Parse(fila.Cells["numero"].Value.ToString())));
                                    DAC.ExecuteNonQuery("INSERT INTO DEL_NAVAL.BUTACAS VALUES (@micro, @tipo, @piso, @numero)", parametrosButaca);    
                                }
                            }
                            break;
                        case Proposito.Modificacion:
                            if (checkFueraServicio.Checked)
                            {
                                List<SqlParameter> parametrosServicio = new List<SqlParameter>();
                                parametrosServicio.Add(new SqlParameter("@id_micro", micro.Id_micro));
                                parametrosServicio.Add(new SqlParameter("@desde", dtpDesde.Value.Date));
                                parametrosServicio.Add(new SqlParameter("@hasta", dtpHasta.Value.Date));
                                int codigoRetorno = (int)DAC.ExecuteScalar(@"declare @retorno int
                                    exec intentarBajarMicro @id_micro, @desde, @hasta, @retorno output
                                    select @retorno ", parametrosServicio);
                                switch (codigoRetorno)
                                {
                                    case -1:
                                        
                                        break;
                                    case -2:
                                        Form conflictoMicroAlta = new ConfilctoMicro(int.Parse(micro.Id_micro));
                                        if (conflictoMicroAlta.ShowDialog() != DialogResult.OK)
                                        {
                                            return;
                                        }
                                        else
                                        {
                                            int segundoCodigoRetorno = (int)DAC.ExecuteScalar(@"declare @retorno int
                                                exec intentarBajarMicro @id_micro, @desde, @hasta, @retorno output
                                                select @retorno ", parametrosServicio);
                                        }
                                        break;
                                    default:
                                        ConfilctoMicro conflictoMicroReemplazo = new ConfilctoMicro(int.Parse(micro.Id_micro), codigoRetorno);
                                        conflictoMicroReemplazo.Desde = dtpDesde.Value.Date;
                                        conflictoMicroReemplazo.Hasta = dtpHasta.Value.Date;
                                        if (conflictoMicroReemplazo.ShowDialog() != DialogResult.OK)
                                        {
                                            return;
                                        }
                                        else
                                        {
                                            int segundoCodigoRetorno = (int)DAC.ExecuteScalar(@"declare @retorno int
                                                exec intentarBajarMicro @id_micro, @desde, @hasta, @retorno output
                                                select @retorno ", parametrosServicio);
                                        }
                                        break;
                                }

                                DAC.ExecuteNonQuery("UPDATE DEL_NAVAL.MICROS SET NUMERO = @numero, TIPO_SERVICIO = @servicio, KILOS_BODEGA = @bodega, CANTIDAD_ASIENTOS = @asientos, MARCA= @marca, MODELO = @modelo, PATENTE = @patente, BAJA_FIN_VIDA_UTIL = 0, BAJA_SERVICIO = @baja_servicio, FECHA_SERVICIO_DESDE = @servicio_desde, FECHA_SERVICIO_HASTA = @servicio_hasta, FECHA_BAJA = null WHERE ID_MICRO = @id_micro", parametros);
                            }
                            else
                            {
                                DAC.ExecuteNonQuery("UPDATE DEL_NAVAL.MICROS SET NUMERO = @numero, TIPO_SERVICIO = @servicio, KILOS_BODEGA = @bodega, CANTIDAD_ASIENTOS = @asientos, MARCA= @marca, MODELO = @modelo, PATENTE = @patente, BAJA_FIN_VIDA_UTIL = 0, BAJA_SERVICIO = 0, FECHA_SERVICIO_DESDE = null, FECHA_SERVICIO_HASTA = null, FECHA_BAJA = null WHERE ID_MICRO = @id_micro", parametros);
                            }
                            break;
                    }
                    MessageBox.Show(proposito.ToString() + " de Micro realizada con éxito");
                    this.Close();
                }
                else
                {
                    errorPatente.SetError(txtPatente, "Ya existe un micro con esta patente");
                }
            }
        }
    }
}
