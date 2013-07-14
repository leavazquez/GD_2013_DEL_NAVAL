using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrbaBus.Entidades
{
    public class Micro
    {
        public string Id_micro;
        public string Id_servicio;
        public float Kilos_bodega;
        public int Cantidad_asientos;
        public string Marca;
        public string Modelo;
        public string Patente;
        public string Numero;
        public bool Baja_fin_vida_util;
        public bool Baja_servicio;
        public DateTime Fecha_alta;
        public DateTime Fecha_servicio_desde;
        public DateTime Fecha_servicio_hasta;
        public DateTime Fecha_baja;
    }
}
