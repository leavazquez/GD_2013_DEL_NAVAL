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
    public partial class ListadoMicros : Listado
    {
        public string IdMicro;
        public string Patente;

        public ListadoMicros()
        {
            InitializeComponent();
            InitializeComponent();
            this.Text = "ABM de Micros";
            this.Size = new Size(974, 536);
            this.Query = @"SELECT MI.ID_MICRO, MI.PATENTE PATENTE, MI.NUMERO, MA.MARCA MARCA, MODELO, TS.NOMBRE_SERVICIO SERVICIO, MI.CANTIDAD_ASIENTOS ASIENTOS, MI.KILOS_BODEGA BODEGA, MI.BAJA_SERVICIO EN_SERVICIO, MI.FECHA_ALTA ALTA, MI.BAJA_SERVICIO BAJA_SERVICIO, MI.FECHA_SERVICIO_DESDE SERVICIO_DESDE, MI.FECHA_SERVICIO_HASTA SERVICIO_HASTA, MI.BAJA_FIN_VIDA_UTIL BAJA, MI.FECHA_BAJA FECHA_BAJA, MI.TIPO_SERVICIO, MI.MARCA, MI.NUMERO
                FROM DEL_NAVAL.MICROS MI, DEL_NAVAL.TIPOS_SERVICIO TS , DEL_NAVAL.MARCAS MA";
            this.Condicion = @"TS.ID_SERVICIO = TIPO_SERVICIO
                AND MA.ID_MARCA = MI.MARCA
                AND MI.BAJA_FIN_VIDA_UTIL = 0";
            this.CampoBaja = "BAJA";
            this.CondicionCampoBaja = true;
            FiltroParcial patente = new FiltroParcial("Patente", "patente");
            AgregarFiltro(patente);
            columnasVisibles.Add("PATENTE", "Patente");
            columnasVisibles.Add("NUMERO", "Número");
            columnasVisibles.Add("MARCA", "Marca");
            columnasVisibles.Add("MODELO", "Modelo");
            columnasVisibles.Add("SERVICIO", "Tipo de Servicio");
            columnasVisibles.Add("ASIENTOS", "Cantidad de Asientos");
            columnasVisibles.Add("BODEGA", "Capacidad de la bodega");
            columnasVisibles.Add("ALTA", "Fecha de Alta");
            columnasVisibles.Add("EN_SERVICIO", "En servicio");
            columnasVisibles.Add("SERVICIO_DESDE", "En servicio desde");
            columnasVisibles.Add("SERVICIO_HASTA", "En servicio hasta");
            columnasVisibles.Add("FECHA_BAJA", "Fecha de baja");
        }

        protected override void seleccionar(object sender, EventArgs e)
        {
            this.IdMicro = dgvResultados.SelectedRows[0].Cells["ID_MICRO"].Value.ToString();
            this.Patente = dgvResultados.SelectedRows[0].Cells["PATENTE"].Value.ToString();
            Close();
        }

    }
}
