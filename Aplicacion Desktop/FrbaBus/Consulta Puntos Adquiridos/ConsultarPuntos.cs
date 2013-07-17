using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrbaBus.Filtros;

namespace FrbaBus.Consulta_Puntos_Adquiridos
{
    public partial class ConsultarPuntos : Listado
    {
        public ConsultarPuntos()
        {
            InitializeComponent();
            this.Query = "SELECT DEL_NAVAL.PUNTOS.CLIENTE CLIENTE FROM DEL_NAVAL.PUNTOS P, DEL_NAVAL.CANJES C";
            this.Condicion = "P.CLIENTE = C.CLIENTE";
        }

        protected override void seleccionar(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
