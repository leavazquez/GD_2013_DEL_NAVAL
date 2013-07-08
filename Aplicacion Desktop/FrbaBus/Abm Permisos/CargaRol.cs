using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using FrbaBus.Entidades;

namespace FrbaBus.Abm_Permisos
{
    public partial class CargaRol : Form
    {
        private Dictionary<string, string> funcionalidades = new Dictionary<string, string>();
        private ErrorProvider errorNombre;
        private ErrorProvider errorFuncionalidades;
        private Proposito proposito;

        private Rol rol = new Rol();

        public CargaRol(Rol rol)
        {
            InitializeComponent();
            proposito = Proposito.Modificacion;
            this.rol = rol;

            // carga de datos
            txtNombreRol.Text = rol.nombre_rol;
            cargarListaFuncionalidades();
            cargarErrores();
        }

        public CargaRol()
        {
            InitializeComponent();
            this.proposito = Proposito.Alta;

            // carga de datos
            cargarListaFuncionalidades();
            cargarErrores();
        }

        private void cargarErrores()
        {
            this.errorNombre = new ErrorProvider();
            errorNombre.SetIconAlignment(this.txtNombreRol, ErrorIconAlignment.MiddleRight);

            this.errorFuncionalidades = new ErrorProvider();
            errorFuncionalidades.SetIconAlignment(this.listBoxFuncionalidades, ErrorIconAlignment.MiddleRight);
        }

        private void cargarListaFuncionalidades()
        {
            DataTable funcionalidadesRol = new DataTable();
            if (proposito == Proposito.Modificacion)
            {
                List<SqlParameter> parametrosFuncionalidadesRol = new List<SqlParameter>();
                parametrosFuncionalidadesRol.Add(new SqlParameter("@id_rol", rol.id_rol));
                funcionalidadesRol = DAC.ExecuteReader("SELECT FUNCIONALIDAD FROM DEL_NAVAL.ROLES_FUNCIONALIDADES WHERE ROL = @id_rol",parametrosFuncionalidadesRol);

                DataColumn[] pks = new DataColumn[funcionalidadesRol.Columns.Count];
                funcionalidadesRol.Columns.CopyTo(pks, 0);

                funcionalidadesRol.PrimaryKey = pks;
            }
            DataTable results = DAC.ExecuteReader("SELECT * FROM DEL_NAVAL.FUNCIONALIDADES");
            foreach (DataRow row in results.Rows)
            {
                listBoxFuncionalidades.Items.Add(row["nombre_funcionalidad"].ToString());
                funcionalidades.Add(row["nombre_funcionalidad"].ToString(), row["id_funcionalidad"].ToString());
                if (proposito == Proposito.Modificacion)
                {
                    if (funcionalidadesRol.Rows.Contains(row["id_funcionalidad"].ToString()))
                    {
                        listBoxFuncionalidades.SelectedItems.Add(row["nombre_funcionalidad"].ToString());
                    }
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombreRol.Text = "";
            listBoxFuncionalidades.SelectedItem = null;
            // deseleccionar funcionalidades
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // validar! no repetido tmb!
            bool isValid = true;
            if (txtNombreRol.Text == String.Empty)
            {
                isValid = false;
                errorNombre.SetError(txtNombreRol, "Nombre no válido");
            }
            if (listBoxFuncionalidades.SelectedItems.Count == 0)
            {
                isValid = false;
                errorFuncionalidades.SetError(listBoxFuncionalidades, "Debe seleccionar alguna funcionlidad");
            }
            if (isValid)
            {
                // validar repetición de nombre
                List<SqlParameter> parametrosNombre = new List<SqlParameter>();
                parametrosNombre.Add(new SqlParameter("@nombre", txtNombreRol.Text));
                int cantidad = (int)DAC.ExecuteScalar("SELECT COUNT(*) FROM DEL_NAVAL.ROLES WHERE NOMBRE_ROL = @nombre", parametrosNombre);
                if (cantidad > 0)
                {
                    errorNombre.SetError(txtNombreRol, "Ya existe un rol con este nombre.");
                }
                else
                {
                    // crear rol
                    DAC.ExecuteNonQuery("INSERT INTO DEL_NAVAL.ROLES VALUES (@nombre, 1)", parametrosNombre);
                    // obtener id de rol creado, utilizando atributo unique
                    string idRol = DAC.ExecuteScalar("SELECT ID_ROL FROM DEL_NAVAL.ROLES WHERE NOMBRE_ROL = @nombre", parametrosNombre).ToString();
                    // asignar funcionalidades al rol
                    foreach (string nombreRol in listBoxFuncionalidades.SelectedItems)
                    {
                        List<SqlParameter> parametrosFuncionalidades = new List<SqlParameter>();
                        parametrosFuncionalidades.Add(new SqlParameter("@id_rol", idRol));
                        parametrosFuncionalidades.Add(new SqlParameter("@id_funcionalidad", funcionalidades[nombreRol]));
                        DAC.ExecuteNonQuery("INSERT INTO DEL_NAVAL.ROLES_FUNCIONALIDADES VALUES (@id_rol, @id_funcionalidad)", parametrosFuncionalidades);
                    }
                    MessageBox.Show("Rol creado con éxito.");
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Por favor corrija los errores.");
            }
        }
    }
}
