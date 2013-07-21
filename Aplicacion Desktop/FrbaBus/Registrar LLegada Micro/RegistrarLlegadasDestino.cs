using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaBus.Registrar_LLegada_Micro
{
    public partial class RegistrarLlegadasDestino : Form
    {
        private string idMicro;
        private Dictionary<string, string> ciudades = new Dictionary<string, string>();
        private Dictionary<string, string> servicios = new Dictionary<string, string>();
        private ErrorProvider errorMicro = new ErrorProvider();

        public RegistrarLlegadasDestino()
        {
            InitializeComponent();
            cargaCiudades();
            cargaComboCiudad(cbOrigen);
            cargaComboCiudad(cbDestino);
            cbOrigen.SelectedIndex = 0;
            cbDestino.SelectedIndex = 1;
        }

        private void btnSeleccionarMicro_Click(object sender, EventArgs e)
        {
            ListadoMicros listadoMicros = new ListadoMicros();
            listadoMicros.ShowDialog();
            if (listadoMicros.IdMicro != null)
            {
                idMicro = listadoMicros.IdMicro;
                btnSeleccionarMicro.Text = listadoMicros.Patente;
            }
            
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

        private void btnRegistrarLlegada_Click(object sender, EventArgs e)
        {
            if (idMicro != null)
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(new SqlParameter("@micro", idMicro));
                parametros.Add(new SqlParameter("@origen", ciudades[cbOrigen.SelectedItem.ToString()]));
                parametros.Add(new SqlParameter("@destino", ciudades[cbDestino.SelectedItem.ToString()]));
                parametros.Add(new SqlParameter("@fecha", dtpLlegada.Value));
                int codigo = (int)DAC.ExecuteScalar(@"set dateformat dmy
                    declare @retorno int
                    exec DEL_NAVAL.registrarLlegada @micro, @origen, @destino, @fecha, @retorno output
                    select @retorno", parametros);
                switch (codigo)
                {
                    case -1:
                        MessageBox.Show("Los datos son inconsistentes, por favor verifíquelos");
                        break;
                    case 0:
                        MessageBox.Show("Llegada registrada exitosamente");
                        this.Close();
                        break;
                }
            }
            else
            {
                errorMicro.SetError(btnSeleccionarMicro, "Seleccione un micro");
            }
        }
    }
}
