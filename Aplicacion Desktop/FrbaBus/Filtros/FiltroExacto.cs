using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrbaBus.Filtros;
using System.Data.SqlClient;

namespace FrbaBus.Filtros
{
    public partial class FiltroExacto : Filtro
    {
        public bool esNumerico = false;

        public FiltroExacto()
        {
            InitializeComponent();
        }

        public FiltroExacto(string nombre, string campo) : base(nombre, campo)
        {
           
        }

        public override SqlParameter Parametro
        {
            get
            {
                return new SqlParameter("@" + Campo, txtValor.Text);
            }
        }

        public override string Condicion
        {
            get
            {
                return Campo + "= @" + Campo;
            }
        }

        public override void MostrarError()
        {
            errorCampo.SetError(txtValor, "Valor inválido");
        }

        public override bool Valido()
        {
            if (esNumerico)
            {
                int dni = 0;
                if (!int.TryParse(txtValor.Text, out dni) || dni <= 0)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return true;
            }
        }
    }
}
