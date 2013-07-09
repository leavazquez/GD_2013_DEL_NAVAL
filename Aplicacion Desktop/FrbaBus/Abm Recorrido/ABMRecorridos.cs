using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrbaBus.Filtros;
using FrbaBus.Entidades;

namespace FrbaBus.Abm_Recorrido
{
    public partial class ABMRecorridos : ABM
    {
        public ABMRecorridos()
        {
            InitializeComponent();
            this.Query = "SELECT * FROM DEL_NAVAL.RECORRIDOS, DEL_NAVAL.CIUDADES D, DEL_NAVAL.CIUDADES O, DEL_NAVAL.TIPOS_SERVICIO TS";
            this.Condicion = "ORIGEN = O.ID_CIUDAD AND DESTINO = D.ID_CIUDAD AND TS.ID_SERVICIO = TIPO_SERVICIO";
            FiltroExacto codigo = new FiltroExacto("Código Recorrido", "codigo_recorrido");
            AgregarFiltro(codigo);
        }

        protected override void crear()
        {
            Form cargaRecorrido = new CargaRecorrido();
            cargaRecorrido.Show();
        }

        protected override void modificar(DataGridViewCellCollection fila)
        {
            Recorrido recorrido = new Recorrido();
            recorrido.Id_recorrido = fila["id_recorrido"].Value.ToString();
            recorrido.Codigo = fila["codigo_recorrido"].Value.ToString();
            recorrido.Id_origen = fila["origen"].Value.ToString();
            recorrido.Id_destino = fila["destino"].Value.ToString();
            recorrido.Id_Servicio = fila["tipo_servicio"].Value.ToString();
            recorrido.Precio_Pasaje= float.Parse(fila["precio_base_pasaje"].Value.ToString());
            recorrido.Precio_Encomienda = float.Parse(fila["precio_kg_encomienda"].Value.ToString());
            Form cargaRecorrido = new CargaRecorrido(recorrido);
            cargaRecorrido.Show();
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
