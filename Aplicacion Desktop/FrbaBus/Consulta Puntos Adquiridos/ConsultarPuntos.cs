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
        FiltroExacto dni;

        public ConsultarPuntos()
        {
            InitializeComponent();
            this.Text = "Consulta de puntos";
            this.Query = "select * from DEL_NAVAL.consultarPuntos (@cliente, @fecha)";
            this.Condicion = "cliente = @cliente";
            this.parametros.Add(new SqlParameter("@fecha", Config.FechaSistema));
            columnasVisibles.Add("fecha_movimiento", "Fecha del movimiento");
            columnasVisibles.Add("descripcion", "Descripción");
            columnasVisibles.Add("puntos", "Puntos");
            dni = new FiltroExacto("DNI", "cliente");
            dni.esNumerico = true;
            AgregarFiltro(dni);
            btnSeleccionar.Text = "Total de puntos acumulados";
           
        }

        protected override void seleccionar(object sender, EventArgs e)
        {
            if (dni.Valido())
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(new SqlParameter("@dni", dni.Valor));
                parametros.Add(new SqlParameter("@fecha", Config.FechaSistema));
                int puntos = (int)DAC.ExecuteScalar(@"set dateformat dmy
                    select SUM (puntos) - SUM(puntos_usados) from DEL_NAVAL.consultarPuntos (@dni, @fecha)
                    where id_canje is null", parametros);
                MessageBox.Show("Puntos acumulados a la fecha: " + puntos.ToString());
            }
            else 
            {
                dni.MostrarError();
            }
        }
    }
}
