using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrbaBus.Filtros;

namespace FrbaBus.Abm_Recorrido
{
    public partial class ABMRecorridos : ABM
    {
        public ABMRecorridos()
        {
            InitializeComponent();
            this.Query = "SELECT CODIGO_RECORRIDO, O.NOMBRE_CIUDAD, D.NOMBRE_CIUDAD, TS.NOMBRE_SERVICIO, PRECIO_BASE_PASAJE, PRECIO_KG_ENCOMIENDA  FROM DEL_NAVAL.RECORRIDOS, DEL_NAVAL.CIUDADES D, DEL_NAVAL.CIUDADES O, DEL_NAVAL.TIPOS_SERVICIO TS";
            this.Condicion = "ORIGEN = O.ID_CIUDAD AND DESTINO = D.ID_CIUDAD AND TS.ID_SERVICIO = TIPO_SERVICIO";
            FiltroExacto codigo = new FiltroExacto("Código Recorrido", "codigo_recorrido");
            AgregarFiltro(codigo);
        }

        protected override void crear()
        {
            throw new NotImplementedException();
        }

        protected override void modificar(DataGridViewCellCollection fila)
        {
            throw new NotImplementedException();
        }

        protected override void eliminar(DataGridViewCellCollection fila)
        {
            throw new NotImplementedException();
        }

        protected override void habilitar(DataGridViewCellCollection fila)
        {
            throw new NotImplementedException();
        }
    }
}
