﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaBus.Listado_Estadistico
{
    public partial class ListadoPasajesCancelados : Listado
    {
        public ListadoPasajesCancelados(DateTime desde, DateTime hasta)
        {
            InitializeComponent();
            this.Query = "select top 5 * from DEL_NAVAL.destinos_pasajes_cancelados (@desde, @hasta)";
            this.parametros.Add(new SqlParameter("@desde", desde));
            this.parametros.Add(new SqlParameter("@hasta", hasta));
            gbComandos.Controls.Clear();
            btnBuscar_Click(null, null);
        }

        protected override void seleccionar(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
