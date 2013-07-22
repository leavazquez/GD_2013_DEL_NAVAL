using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrbaBus.Entidades
{
    public class Viaje
    {
        public string IdViaje;
        public DateTime salida;
        public DateTime llegada;
        public List<int> pasajeros = new List<int>();
    }
}
