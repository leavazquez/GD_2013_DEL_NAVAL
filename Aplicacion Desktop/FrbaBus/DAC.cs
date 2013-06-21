using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrbaBus;
using System.Data.SqlClient;

namespace FrbaBus
{
    static class DAC
    {
        public static SqlConnection CrearConexion()
        {
            SqlConnection conexion = new SqlConnection(Config.String_Conexion);
            return conexion;
        }
    }
}
