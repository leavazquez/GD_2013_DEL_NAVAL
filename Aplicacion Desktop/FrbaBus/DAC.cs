using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrbaBus;
using System.Data.SqlClient;
using System.Data;

namespace FrbaBus
{
    static class DAC
    {
        public static SqlConnection CrearConexion()
        {
            SqlConnection conexion = new SqlConnection(Config.String_Conexion);
            return conexion;
        }

        public static DataTable ExecuteReader(string query)
        {
            return ExecuteReader(query, null);
        }

        public static DataTable ExecuteReader(string query, List<SqlParameter> parametros)
        {
            DataTable data = new DataTable();
            using (SqlConnection conexion = new SqlConnection(Config.String_Conexion))
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand(query, conexion);
                if (parametros != null)
                {
                    foreach (SqlParameter parametro in parametros)
                    {
                        comando.Parameters.Add(parametro);
                    }
                }
                SqlDataReader reader = comando.ExecuteReader();
                comando.Parameters.Clear();
                data.Load(reader);
            }
            return data;
        }

        public static object ExecuteScalar(string query)
        {
            return ExecuteScalar(query, null);
        }

        public static object ExecuteScalar(string query, List<SqlParameter> parametros)
        {
            object escalar;
            using (SqlConnection conexion = new SqlConnection(Config.String_Conexion))
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand(query, conexion);
                if (parametros != null)
                {
                    foreach (SqlParameter parametro in parametros)
                    {
                        comando.Parameters.Add(parametro);
                    }                    
                }
                escalar =  comando.ExecuteScalar();
                comando.Parameters.Clear();
            }
            return escalar;
        }

        public static object ExecuteNonQuery(string query)
        {
            return ExecuteNonQuery(query, null);
        }

        public static object ExecuteNonQuery(string query, List<SqlParameter> parametros)
        {
            int affected;
            using (SqlConnection conexion = new SqlConnection(Config.String_Conexion))
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand(query, conexion);
                if (parametros != null)
                {
                    foreach (SqlParameter parametro in parametros)
                    {
                        comando.Parameters.Add(parametro);
                    }
                }
                affected = comando.ExecuteNonQuery();
                comando.Parameters.Clear();
            }
            return affected;
        }

    }
}
