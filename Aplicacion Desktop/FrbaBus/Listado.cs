using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaBus
{
    public partial class Listado : Form
    {
        private List<Control> filtros = new List<Control>();
        protected string Query;

        public Listado()
        {
            InitializeComponent();
        }

        public void AgregarFiltro (Control filtro)
        {
            gbFiltros.Controls.Add(filtro);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            string nuevaQuery = this.Query;
            nuevaQuery += " WHERE ";
            foreach (Filtros.FiltroExacto filtro in filtros) // usar clase base de fltros
            {
                nuevaQuery += " AND " + filtro.Condición;
                parametros.Add(filtro.Parametro);
            }
            // ejecutar quey y llenar el datatable
        }
    }
}
