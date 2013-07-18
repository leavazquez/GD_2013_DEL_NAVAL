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
    public partial class FiltroSeleccion : Filtro
    {
        private ComboBox cbValor = new ComboBox();

        public FiltroSeleccion()
        {
            InitializeComponent();
        }

        public FiltroSeleccion(string nombre, string campo, List<string> valores) : base(nombre, campo)
        {
            this.Controls.Add(cbValor);
            cbValor.DropDownStyle = ComboBoxStyle.DropDownList;
            cbValor.Left = txtValor.Left;
            cbValor.Top = txtValor.Top;
            this.txtValor.Visible = false;
            foreach (string valor in valores)
            {
                cbValor.Items.Add(valor);
            }
        }

        public override SqlParameter Parametro
        {
            get
            {
                return new SqlParameter("@" + Campo.Replace(".",""), cbValor.SelectedItem.ToString());
            }
        }

        public override string Condicion
        {
            get
            {
                return Campo + "= @" + Campo.Replace(".", "");
            }
        }

        public override string Valor
        {
            get
            {
                return cbValor.SelectedItem.ToString();
            }
        }
    }
}
