using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrbaBus
{
    static class Comun
    {
        public static Dictionary<string, Type> Funcionalidades = new Dictionary<string, Type>()
        {
            {"ABMRoles", typeof(Abm_Permisos.ABMRoles)},
            {"ABMRecorridos", typeof(Abm_Recorrido.ABMRecorridos)},
            {"ABMMicros", typeof(Abm_Micro.ABMMicros)},
            {"GenerarViajes", typeof(GenerarViaje.GenerarViajes)},
            {"RegistrarLlegadasDestino", typeof(Registrar_LLegada_Micro.RegistrarLlegadasDestino)},
            {"ComprarPasajesEncomiendas", typeof(Compra_de_Pasajes.ComprarPasajesEncomiendas)},
            {"CancelarPasajesEncomiendas", typeof(Cancelar_Viaje.CancelarPasajesEncomiendas)},
            {"ConsultarPuntos", typeof(Consulta_Puntos_Adquiridos.ConsultarPuntos)},
            {"CanjearPuntos", typeof(Canje_de_Ptos.CanjearPuntos)},
            {"VerListadoEstadistico", typeof(Listado_Estadistico.VerListadoEstadistico)}
        };
    }

    enum Proposito
    {
        Alta,
        Modificacion
    }

    public static class Aleatorio
    {
        public static string Nuevo(int longitud)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(
                Enumerable.Repeat(chars, longitud)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
        }
    }
}
