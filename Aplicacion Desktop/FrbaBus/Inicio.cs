using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrbaBus;
using System.Data.SqlClient;

namespace FrbaBus
{
    public partial class Inicio : Form
    {

        public Inicio()
        {
            InitializeComponent();
            cargarListafuncionalidadesCliente();
        }

        private void cargarListafuncionalidadesCliente()
        {
            using (SqlConnection conexion = DAC.CrearConexion())
            {
                conexion.Open();
                SqlCommand comando = conexion.CreateCommand();
                comando.CommandText = @"select nombre_funcionalidad
                    from DEL_NAVAL.funcionalidades fu, DEL_NAVAL.roles ro, DEL_NAVAL.roles_funcionalidades rf
                    where fu.id_funcionalidad = rf.funcionalidad
                    and rf.rol = ro.id_rol
                    and ro.nombre_rol = 'cliente'";
                SqlDataReader reader = comando.ExecuteReader();
                Point puntoInicial = new Point(20, 40);
                while (reader.Read())
                {
                    Button btnFuncionalidad = new Button();
                    string funcionalidad = reader["nombre_funcionalidad"].ToString();
                    btnFuncionalidad.Text = funcionalidad; // traducir
                    btnFuncionalidad.Tag = funcionalidad;
                    btnFuncionalidad.Click += iniciarFuncionalidad;
                    btnFuncionalidad.Location = puntoInicial;
                    this.Controls.Add(btnFuncionalidad);
                    puntoInicial.Y += 30;
                }
            }
        }

        private void iniciarFuncionalidad(object sender, EventArgs e)
        {
            Button btnFuncionalidad = sender as Button;
            Form formFuncionalidad = (Form)Activator.CreateInstance(Comun.Funcionalidades[btnFuncionalidad.Tag.ToString()]);
            formFuncionalidad.Show();
        }
    }
}
