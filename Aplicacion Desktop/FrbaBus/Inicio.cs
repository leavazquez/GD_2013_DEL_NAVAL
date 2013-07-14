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
        List<Button> m_btnsFuncionalidades = new List<Button>();

        public Inicio()
        {
            InitializeComponent();
            cargarListaFuncionalidadesUsuario("cliente");
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

        private void cargarListaFuncionalidadesUsuario(string rol)
        {
            foreach (Button btnFuncionalidad in m_btnsFuncionalidades)
            {
                this.Controls.Remove(btnFuncionalidad);
                btnFuncionalidad.Dispose();
            }
            m_btnsFuncionalidades.Clear();
            using (SqlConnection conexion = DAC.CrearConexion())
            {
                conexion.Open();
                SqlCommand comando = conexion.CreateCommand();
                comando.Parameters.Add(new SqlParameter("@rol", rol));
                comando.CommandText = @"select nombre_funcionalidad
                    from DEL_NAVAL.funcionalidades fu, DEL_NAVAL.roles ro, DEL_NAVAL.roles_funcionalidades rf
                    where fu.id_funcionalidad = rf.funcionalidad
                    and rf.rol = ro.id_rol
                    and ro.nombre_rol = @rol";
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
                    btnFuncionalidad.AutoSize = true;
                    m_btnsFuncionalidades.Add(btnFuncionalidad);
                    this.Controls.Add(btnFuncionalidad);
                    puntoInicial.Y += 30;
                }
                reader.Close();
            }
        }

        private void iniciarFuncionalidad(object sender, EventArgs e)
        {
            Button btnFuncionalidad = sender as Button;
            Form formFuncionalidad = (Form)Activator.CreateInstance(Comun.Funcionalidades[btnFuncionalidad.Tag.ToString()]);
            formFuncionalidad.ShowDialog();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (Sesion.Iniciada)
            {
                //testear
                Sesion.Terminar();
                cargarListaFuncionalidadesUsuario("cliente");
                cargarSeccionLogin();

            }
            else
            {
                Form formLogin = new Login.Login();
                DialogResult resultado = formLogin.ShowDialog();
                if (resultado == DialogResult.OK)
                {
                    using (SqlConnection conexion = DAC.CrearConexion())
                    {
                        string rol = "";
                        conexion.Open();
                        SqlCommand comandoRol = conexion.CreateCommand();
                        comandoRol.Parameters.Add(new SqlParameter("@id_rol", Sesion.IdRol));
                        comandoRol.CommandText = @"SELECT NOMBRE_ROL FROM DEL_NAVAL.ROLES WHERE ID_ROL = @id_rol";
                        rol = comandoRol.ExecuteScalar().ToString();
                        cargarListaFuncionalidadesUsuario(rol);
                        cargarSeccionLogin();
                    }
                }
            }
        }
    }
}
