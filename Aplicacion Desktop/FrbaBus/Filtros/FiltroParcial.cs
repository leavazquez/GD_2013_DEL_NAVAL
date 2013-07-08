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
    public partial class FiltroParcial : Filtro
    {
        public FiltroParcial()
        {
            InitializeComponent();
        }

        public FiltroParcial(string nombre, string campo) : base(nombre, campo)
        {
           
        }

        override public SqlParameter Parametro
        {
            get
            {
                return new SqlParameter("@" + Campo, "%" + txtValor.Text + "%");
            }
        }

        public override string Condicion
        {
            get
            {
                return Campo + " LIKE @" + Campo;
            }
        }
    }
}
