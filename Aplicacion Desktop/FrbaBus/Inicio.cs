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
            cargarListaFuncionalidadesCliente();
            cargarSeccionLogin();
        }

        private void cargarSeccionLogin()
        {
            if (Sesion.Iniciada)
            {
                labelInicioSesion.Text = "Sesión Iniciada como: " + Sesion.Nombre_usuario;
                btnLogin.Text = "Cerrar Sesión";

            }
            else
            {
                labelInicioSesion.Text = "";
                btnLogin.Text = "Ingresar al Sistema";

            }
        }

        private void cargarListaFuncionalidadesCliente()
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

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (Sesion.Iniciada)
            {
                //testear
                Sesion.Terminar();
                cargarListaFuncionalidadesUsuario();
                cargarSeccionLogin();

            }
            else
            {
                Form formLogin = new Login.Login();
                DialogResult resultado = formLogin.ShowDialog();
                if (resultado == DialogResult.OK)
                {
                    cargarListaFuncionalidadesUsuario();
                    cargarSeccionLogin();
                }
            }
        }

        private void cargarListaFuncionalidadesUsuario()
        {
            // deberia ser como la de clientes pero con el rol del usuario... hacer la anterior más genérica
        }

    }
}
