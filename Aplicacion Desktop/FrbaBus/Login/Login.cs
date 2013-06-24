using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Data.SqlClient;

namespace FrbaBus.Login
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            //SHA256 encriptador = SHA256Managed.Create();
            SHA256CryptoServiceProvider encriptador = new SHA256CryptoServiceProvider();
            string nombreUsuario = txtNombre.Text;
            byte[] passwordBytes = encriptador.ComputeHash(Encoding.UTF8.GetBytes(txtPassword.Text));
            string password = BitConverter.ToString((passwordBytes)).Replace("-", "");

            using (SqlConnection conexion = DAC.CrearConexion())
            {
                conexion.Open();
                SqlCommand comandoLoguear = conexion.CreateCommand();
                comandoLoguear.Parameters.Add(new SqlParameter("@nombre", nombreUsuario));
                comandoLoguear.CommandText = @"SELECT * FROM DEL_NAVAL.USUARIOS
                    WHERE NOMBRE_USUARIO = @nombre";
                SqlDataReader readerUsuario = comandoLoguear.ExecuteReader();
                bool encontrado = false;
                string res_id_usuario = "";
                string res_nombre_usuario = "";
                string res_pass = "";
                string res_id_rol = "";
                int res_intentos = 0;
                bool res_activo = false;

                if (readerUsuario.Read())
                {
                    encontrado = true;
                    res_id_usuario = readerUsuario["id_usuario"].ToString();
                    res_nombre_usuario = readerUsuario["nombre_usuario"].ToString();
                    res_pass = readerUsuario["pass"].ToString();
                    res_id_rol = readerUsuario["rol"].ToString();
                    res_intentos = int.Parse(readerUsuario["intentos"].ToString());
                    res_activo = bool.Parse(readerUsuario["activo"].ToString());
                }
                readerUsuario.Close();

                if (encontrado)
                {
                    if (res_activo)
                    {
                        if (res_pass == password)
                        {
                            Sesion.Iniciada = true;
                            Sesion.IdUsuario = res_id_usuario;
                            Sesion.Nombre_usuario = res_nombre_usuario;
                            Sesion.IdRol = res_id_rol;
                            MessageBox.Show("Bienvenido");
                            DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {

                            SqlCommand comandoIntento = conexion.CreateCommand();
                            comandoIntento.Parameters.Add(new SqlParameter("@usuario", nombreUsuario));
                            comandoIntento.CommandText = @"UPDATE DEL_NAVAL.USUARIOS SET
                                INTENTOS = INTENTOS + 1 WHERE NOMBRE_USUARIO = @usuario";
                            comandoIntento.ExecuteNonQuery();
                            string error = "Contraseña incorrecta.";
                            error += res_intentos == 2 ? "\n El usuario ha sido bloqueado" : "";
                            mostrarError(error);
                        }
                    }
                    else
                    {
                        mostrarError("Usuario bloqueado.");
                    }
                }
                else
                {
                    mostrarError("Usuario inexistente.");
                }
            }
        }

        private void mostrarError(string error)
        {
            MessageBox.Show(error);
            txtNombre.Text = "";
            txtPassword.Text = "";
        }
    }
}
