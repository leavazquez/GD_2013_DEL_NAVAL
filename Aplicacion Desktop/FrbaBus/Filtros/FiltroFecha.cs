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
    public partial class FiltroFecha : Filtro
    {
        DateTimePicker dtpValor = new DateTimePicker();

        public FiltroFecha()
        {
            InitializeComponent();
        }

        public FiltroFecha(string nombre, string campo) : base(nombre, campo)
        {
            this.Controls.Add(dtpValor);
            dtpValor.Left = txtValor.Left;
            dtpValor.Top = txtValor.Top;
            dtpValor.Format = DateTimePickerFormat.Short;
            this.txtValor.Visible = false;
            dtpValor.Value = Config.FechaSistema;
        }

        public override SqlParameter Parametro
        {
            get
            {
                return new SqlParameter("@" + Campo, dtpValor.Value);
            }
        }

        public override string Condicion
        {
            get
            {
                return "CONVERT(date, " + Campo + ") = CONVERT(date, @" + Campo + ")";
            }
        }

        public override string Valor
        {
            get
            {
                return "Siempre tiene valor";
            }
        }
    }
}
