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
            txtNombreRol.Text = rol.Nombre_rol;
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
                parametrosFuncionalidadesRol.Add(new SqlParameter("@id_rol", rol.Id_rol));
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
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // validar
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
                string queryNombre = "SELECT COUNT(*) FROM DEL_NAVAL.ROLES WHERE NOMBRE_ROL = @nombre";
                if (proposito == Proposito.Modificacion)
                {
                    parametrosNombre.Add(new SqlParameter("@id_rol", rol.Id_rol));
                    queryNombre += " AND ID_ROL <> @id_rol";
                }
                int cantidad = (int)DAC.ExecuteScalar(queryNombre, parametrosNombre);
                if (cantidad > 0)
                {
                    errorNombre.SetError(txtNombreRol, "Ya existe un rol con este nombre.");
                }
                else
                {
                    // crear rol
                    switch (proposito)
                    {
                        case Proposito.Alta:
                            DAC.ExecuteNonQuery("INSERT INTO DEL_NAVAL.ROLES VALUES (@nombre, 1)", parametrosNombre);
                            break;
                        case Proposito.Modificacion:
                            DAC.ExecuteNonQuery("UPDATE DEL_NAVAL.ROLES SET NOMBRE_ROL = @nombre WHERE ID_ROL = @id_rol", parametrosNombre);
                            break;
                    }
                    // obtener id de rol creado, utilizando atributo unique
                    string idRol = "";
                    // asignar funcionalidades al rol
                    switch (proposito)
                    {
                        case Proposito.Alta:
                            idRol = DAC.ExecuteScalar("SELECT ID_ROL FROM DEL_NAVAL.ROLES WHERE NOMBRE_ROL = @nombre", parametrosNombre).ToString();
                            break;
                        case Proposito.Modificacion:
                            idRol = rol.Id_rol;
                            List<SqlParameter> parametrosModificacion = new List<SqlParameter>();
                            parametrosModificacion.Add(new SqlParameter("@id_rol", idRol));
                            DAC.ExecuteNonQuery("DELETE FROM DEL_NAVAL.ROLES_FUNCIONALIDADES WHERE ROL = @id_rol", parametrosModificacion);
                            break;
                    }
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
        }
    }
}
