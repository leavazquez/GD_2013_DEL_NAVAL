using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrbaBus.Filtros;
using System.Data.SqlClient;

namespace FrbaBus
{
    public partial class ListadoViajes : Listado
    {
        public string IdViaje;
        public int AsientosDisponibles;
        public int KilosDisponibles;
        public string Viaje;
        public string Destino;
        public string DestinoObligado;

        public ListadoViajes()
        {
            InitializeComponent();
            this.Text = "Seleccione un viaje";
            this.Size = new Size(974, 536);
            this.Query = @"select vi.id_viaje ID_VIAJE, vi.fecha_salida SALIDA, ts.nombre_servicio SERVICIO, (mi.cantidad_asientos - (select count(*) from DEL_NAVAL.butacas_ocupadas where viaje = vi.id_viaje)) BUTACAS_DISPONIBLES, o.nombre_ciudad ORIGEN, d.nombre_ciudad DESTINO
                ,(mi.kilos_bodega - SUM(en.peso)) as KILOS_DISPONIBLES
                from DEL_NAVAL.viajes vi 
                left join DEL_NAVAL.recorridos re on vi.recorrido = re.id_recorrido
                left join DEL_NAVAL.tipos_servicio ts on re.tipo_servicio = ts.id_servicio
                left join DEL_NAVAL.micros mi on  mi.id_micro = vi.micro
                left join DEL_NAVAL.encomiendas EN on vi.id_viaje = EN.viaje
                left join DEL_NAVAL.ciudades o on re.origen = o.id_ciudad
                left join DEL_NAVAL.ciudades d on re.destino = d.id_ciudad";
            this.GroupBy = "group by vi.id_viaje, vi.fecha_salida, ts.nombre_servicio, mi.cantidad_asientos, mi.kilos_bodega, o.nombre_ciudad, d.nombre_ciudad";
            this.columnasVisibles.Add("SALIDA", "Salida");
            this.columnasVisibles.Add("SERVICIO", "Tipo de Servicio");
            this.columnasVisibles.Add("BUTACAS_DISPONIBLES", "Butacas disponibles");
            this.columnasVisibles.Add("KILOS_DISPONIBLES", "Kilos disponibles");
            
            // agregar filtro de fecha y ciudades
            FiltroFecha dia = new FiltroFecha("Día del viaje", "fecha_salida");
            AgregarFiltro(dia);
            List<string> ciudades = new List<string>();
            foreach (DataRow fila in DAC.ExecuteReader("SELECT NOMBRE_CIUDAD FROM DEL_NAVAL.CIUDADES").Rows)
	        {
        	    ciudades.Add(fila["NOMBRE_CIUDAD"].ToString());
	        }

            FiltroSeleccion origen = new FiltroSeleccion("Origen", "o.nombre_ciudad", ciudades);
            AgregarFiltro(origen);
            FiltroSeleccion destino = new FiltroSeleccion("Destino", "d.nombre_ciudad", ciudades);
            AgregarFiltro(destino);
            if (this.DestinoObligado != null)
            {
                destino.cbValor.SelectedItem = this.DestinoObligado;
                destino.cbValor.Enabled = false;
            }
        }

        protected override void seleccionar(object sender, EventArgs e)
        {
            if (dgvResultados.SelectedRows.Count > 0)
            {
                this.IdViaje = dgvResultados.SelectedRows[0].Cells["ID_VIAJE"].Value.ToString();
                this.Viaje = dgvResultados.SelectedRows[0].Cells["ORIGEN"].Value.ToString() + " - " + dgvResultados.SelectedRows[0].Cells["DESTINO"].Value.ToString() + " - " + dgvResultados.SelectedRows[0].Cells["SERVICIO"].Value.ToString() + " - " + dgvResultados.SelectedRows[0].Cells["SALIDA"].Value.ToString();
                this.AsientosDisponibles = int.Parse(dgvResultados.SelectedRows[0].Cells["BUTACAS_DISPONIBLES"].Value.ToString());
                this.KilosDisponibles = int.Parse(dgvResultados.SelectedRows[0].Cells["KILOS_DISPONIBLES"].Value.ToString());
                this.Destino = dgvResultados.SelectedRows[0].Cells["DESTINO"].Value.ToString();
                Close();
            }
            else
            {
                MessageBox.Show("Seleccione un viaje");
            }
        }


    }
}
