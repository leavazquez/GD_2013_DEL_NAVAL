using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaBus.Canje_de_Ptos
{
    public partial class DatosCliente : Form
    {
        private ErrorProvider errorDni = new ErrorProvider();
        private ErrorProvider errorCantidad = new ErrorProvider();
        private string idProducto;

        public DatosCliente(string idProducto)
        {
            InitializeComponent();
            this.idProducto = idProducto;
        }

        private void btnCanjear_Click(object sender, EventArgs e)
        {
            bool isValid = true;
            int dni;
            if (!int.TryParse(txtDni.Text, out dni) || dni <= 0)
	        {
                isValid = false;
        		errorDni.SetError(txtDni, "DNI inválido");
	        }
            int cantidad = 0;
            if (!int.TryParse(txtCantidad.Text, out cantidad) || cantidad <= 0)
            {
                isValid = false;
                errorCantidad.SetError(txtCantidad, "Cantidad inválida");
            }
            if (isValid)
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(new SqlParameter("@dni", dni));
                int cantidadClientes = (int)DAC.ExecuteScalar("SELECT COUNT(*) FROM DEL_NAVAL.CLIENTES WHERE ID_DNI = @dni", parametros);
                if (cantidadClientes > 0)
                {
                    parametros.Add(new SqlParameter("@producto", this.idProducto));
                    parametros.Add(new SqlParameter("@cantidad", cantidad));
                    parametros.Add(new SqlParameter("@fecha", Config.FechaSistema));
                    int codigo = (int)DAC.ExecuteScalar(@"set dateformat dmy
                        declare @outp int
                        exec del_naval.canjearPuntos @dni, @producto, @cantidad, @fecha, @outp output
                        select @outp", parametros);
                    switch (codigo)
                    {
                        case -3:
                            MessageBox.Show("El canje no se pudo realizar por motivos desconocidos");
                            break;
                        case -2:
                            MessageBox.Show("El cliente no tiene puntos suficientes para realizar el canje");
                            break;
                        case -1:
                            MessageBox.Show("No hay suficiente stock para realizar el canje");
                            break;
                        case 0:
                            MessageBox.Show("Canje realizado con éxito");
                            break;
                    }
                }
                else
                {
                    errorDni.SetError(txtDni, "El DNI no existe en la base de datos de clientes");
                }
            }
        }
    }
}
