using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaBus.Filtros
{
    public partial class FiltroExacto : UserControl
    {
        public string Campo;

        public FiltroExacto(string nombre, string campo)
        {
            InitializeComponent();
            this.Nombre.Text = nombre;
            this.Campo = campo;
        }

        public string Condición
        {
            get
            {
                return Campo + "= @" + Campo;
            }
        }

        public SqlParameter Parametro
        {
            get
            {
                return new SqlParameter("@" + Campo, txtValor.Text);
            }
        }
    }
}
