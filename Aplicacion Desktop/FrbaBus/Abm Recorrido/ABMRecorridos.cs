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
using System.Data.SqlClient;

namespace FrbaBus.Abm_Recorrido
{
    public partial class ABMRecorridos : ABM
    {
        public ABMRecorridos()
        {
            InitializeComponent();
            this.Text = "ABM de Recorridos";
            this.Query = "SELECT ID_RECORRIDO, CODIGO_RECORRIDO, ORIGEN, DESTINO, TIPO_SERVICIO, PRECIO_BASE_PASAJE, PRECIO_KG_ENCOMIENDA, CANCELADO, O.NOMBRE_CIUDAD AS CIUDAD_ORIGEN, D.NOMBRE_CIUDAD AS CIUDAD_DESTINO, TS.NOMBRE_SERVICIO AS NOMBRE_SERVICIO FROM DEL_NAVAL.RECORRIDOS, DEL_NAVAL.CIUDADES D, DEL_NAVAL.CIUDADES O, DEL_NAVAL.TIPOS_SERVICIO TS";
            this.Condicion = "ORIGEN = O.ID_CIUDAD AND DESTINO = D.ID_CIUDAD AND TS.ID_SERVICIO = TIPO_SERVICIO";
            this.CampoBaja = "CANCELADO";
            this.CondicionCampoBaja = true;
            columnasVisibles.Add("CANCELADO", "Cancelado");
            columnasVisibles.Add("CODIGO_RECORRIDO", "Código");
            columnasVisibles.Add("CIUDAD_ORIGEN", "Origen");
            columnasVisibles.Add("CIUDAD_DESTINO", "Destino");
            columnasVisibles.Add("NOMBRE_SERVICIO", "Tipo de Servicio");
            columnasVisibles.Add("PRECIO_BASE_PASAJE", "Precio Base Pasaje");
            columnasVisibles.Add("PRECIO_KG_ENCOMIENDA", "Precio Kg Encomienda");

            FiltroExacto codigo = new FiltroExacto("Código Recorrido (Exacto)", "codigo_recorrido");
            AgregarFiltro(codigo);
        }

        protected override void crear()
        {
            Form cargaRecorrido = new CargaRecorrido();
            cargaRecorrido.ShowDialog();
        }

        protected override void modificar(DataGridViewCellCollection fila)
        {
            Recorrido recorrido = new Recorrido();
            recorrido.Id_recorrido = fila["ID_RECORRIDO"].Value.ToString();
            recorrido.Codigo = fila["codigo_recorrido"].Value.ToString();
            recorrido.Id_origen = fila["origen"].Value.ToString();
            recorrido.Id_destino = fila["destino"].Value.ToString();
            recorrido.Id_Servicio = fila["tipo_servicio"].Value.ToString();
            recorrido.Precio_Pasaje= float.Parse(fila["precio_base_pasaje"].Value.ToString());
            recorrido.Precio_Encomienda = float.Parse(fila["precio_kg_encomienda"].Value.ToString());
            Form cargaRecorrido = new CargaRecorrido(recorrido);
            cargaRecorrido.ShowDialog();
        }

        protected override void eliminar(DataGridViewCellCollection fila)
        {
            string codigoCancelacion = Aleatorio.Nuevo(20);
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@id_rec", fila["ID_RECORRIDO"].Value.ToString()));
            parametros.Add(new SqlParameter("@cod_cancelacion", codigoCancelacion));
            parametros.Add(new SqlParameter("@fecha",Config.FechaSistema));
            DAC.ExecuteNonQuery("exec cancelarRecorrido @id_rec, @cod_cancelacion, @fecha, 'Baja de recorrido'", parametros);
            MessageBox.Show("Recorrido eliminado");
        }

        protected override void habilitar(DataGridViewCellCollection fila)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@id_rec", fila["ID_RECORRIDO"].Value.ToString()));
            DAC.ExecuteNonQuery("UPDATE DEL_NAVAL.RECORRIDOS SET CANCELADO = 0 WHERE ID_RECORRIDO = @id_rec", parametros);
            MessageBox.Show("Recorrido habilitado");
        }
    }
}
