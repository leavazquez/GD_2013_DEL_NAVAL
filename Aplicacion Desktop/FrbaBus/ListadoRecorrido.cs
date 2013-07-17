using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrbaBus.Filtros;

namespace FrbaBus
{
    public partial class ListadoRecorrido : Listado
    {
        public string idRecorrido;
        public string Recorrido;

        public ListadoRecorrido()
        {
            InitializeComponent();
            this.Text = "ABM de Recorridos";
            this.Query = "SELECT ID_RECORRIDO, CODIGO_RECORRIDO, ORIGEN, DESTINO, TIPO_SERVICIO, PRECIO_BASE_PASAJE, PRECIO_KG_ENCOMIENDA, CANCELADO, O.NOMBRE_CIUDAD AS CIUDAD_ORIGEN, D.NOMBRE_CIUDAD AS CIUDAD_DESTINO, TS.NOMBRE_SERVICIO AS NOMBRE_SERVICIO FROM DEL_NAVAL.RECORRIDOS, DEL_NAVAL.CIUDADES D, DEL_NAVAL.CIUDADES O, DEL_NAVAL.TIPOS_SERVICIO TS";
            this.Condicion = "ORIGEN = O.ID_CIUDAD AND DESTINO = D.ID_CIUDAD AND TS.ID_SERVICIO = TIPO_SERVICIO AND CANCELADO = 0";
            this.CampoBaja = "CANCELADO";
            this.CondicionCampoBaja = true;
            columnasVisibles.Add("CODIGO_RECORRIDO", "Código");
            columnasVisibles.Add("CIUDAD_ORIGEN", "Origen");
            columnasVisibles.Add("CIUDAD_DESTINO", "Destino");
            columnasVisibles.Add("NOMBRE_SERVICIO", "Tipo de Servicio");
            columnasVisibles.Add("PRECIO_BASE_PASAJE", "Precio Base Pasaje");
            columnasVisibles.Add("PRECIO_KG_ENCOMIENDA", "Precio Kg Encomienda");

            FiltroExacto codigo = new FiltroExacto("Código Recorrido (Exacto)", "codigo_recorrido");
            AgregarFiltro(codigo);
        }

        protected override void seleccionar(object sender, EventArgs e)
        {
            if (dgvResultados.SelectedRows.Count > 0)
            {
                this.idRecorrido = dgvResultados.SelectedRows[0].Cells["ID_RECORRIDO"].Value.ToString();
                this.Recorrido = dgvResultados.SelectedRows[0].Cells["CIUDAD_ORIGEN"].Value.ToString() + " - " + dgvResultados.SelectedRows[0].Cells["CIUDAD_DESTINO"].Value.ToString() + " - " + dgvResultados.SelectedRows[0].Cells["NOMBRE_SERVICIO"].Value.ToString();
                Close();
            }
            else
            {
                MessageBox.Show("Seleccione un recorrido");
            }
        }
    }
}
