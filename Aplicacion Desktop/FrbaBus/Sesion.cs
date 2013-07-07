using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrbaBus
{
    static class Sesion
    {
        private static string nombreUsuario;
        private static string idUsuario;
        private static string idRol;
        private static bool iniciada;

        public static string Nombre_usuario
        {
            get { return nombreUsuario; }
            set { nombreUsuario = value; }
        }

        public static string IdUsuario
        {
            get { return idUsuario; }
            set { idUsuario = value; }
        }

        public static string IdRol
        {
            get { return idRol; }
            set { idRol = value; }
        }

        public static bool Iniciada
        {
            get { return iniciada; }
            set { iniciada = value; }
        }

        internal static void Terminar()
        {
            nombreUsuario = "";
            idUsuario = "";
            idRol = "";
            iniciada = false;
        }
    }
}
