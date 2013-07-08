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
    public abstract partial class Filtro : UserControl
    {
        public string Campo;
        public Filtro()
        {
            InitializeComponent();
        }

        public Filtro(string nombre, string campo)
        {
            InitializeComponent();
            this.Nombre.Text = nombre;
            this.Campo = campo;
        }

        public abstract string Condicion
        {
            get;
        }

        public abstract SqlParameter Parametro
        {
            get;
        }

        public string Valor
        {
            get
            {
                return txtValor.Text;
            }
        }

        public void Limpiar()
        {
            this.txtValor.Text = "";
        }
    }
}
