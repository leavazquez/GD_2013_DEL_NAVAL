using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrbaBus.Filtros;
using System.Data.SqlClient;

namespace FrbaBus.Consulta_Puntos_Adquiridos
{
    public partial class ConsultarPuntos : Listado
    {
        public ConsultarPuntos()
        {
            InitializeComponent();
            this.Query = "select * from DEL_NAVAL.consultarPuntos (@cliente, @fecha)";
            this.Condicion = "cliente = @cliente";
            this.parametros.Add(new SqlParameter("@fecha", Config.FechaSistema));
            FiltroExacto dni = new FiltroExacto("DNI", "cliente");
            AgregarFiltro(dni);
            gbComandos.Controls.Clear();
        }

        protected override void seleccionar(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
