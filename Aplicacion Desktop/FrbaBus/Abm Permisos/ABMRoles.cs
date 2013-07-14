using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrbaBus.Filtros;
using FrbaBus.Entidades;
using System.Data.SqlClient;

namespace FrbaBus.Abm_Permisos
{
    public partial class ABMRoles : ABM
    {
        public ABMRoles()
        {
            InitializeComponent();
            this.Text = "ABM de Roles";
            this.Query = "SELECT * FROM DEL_NAVAL.ROLES";
            this.CampoBaja = "activo";
            this.CondicionCampoBaja = false;
            AgregarFiltro(new FiltroParcial("Nombre", "nombre_rol"));
            columnasVisibles.Add("nombre_rol", "Nombre");
            columnasVisibles.Add("activo", "Activo");
        }

        protected override void modificar(DataGridViewCellCollection fila)
        {
            Rol rol = new Rol();
            rol.Id_rol = fila["id_rol"].Value.ToString();
            rol.Nombre_rol = fila["nombre_rol"].Value.ToString();
            Form cargaRol = new CargaRol(rol);
            cargaRol.ShowDialog();
        }

        protected override void crear()
        {
            CargaRol formCargaRol = new CargaRol();
            formCargaRol.ShowDialog();
        }

        protected override void eliminar(DataGridViewCellCollection fila)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@id_rol", fila["id_rol"].Value.ToString()));
            DAC.ExecuteNonQuery("exec dbo.inhabilitar_rol @id_rol", parametros);
            MessageBox.Show("Rol eliminado");
        }

        protected override void habilitar(DataGridViewCellCollection fila)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@id_rol", fila["id_rol"].Value.ToString()));
            DAC.ExecuteNonQuery("UPDATE DEL_NAVAL.ROLES SET ACTIVO = 1 WHERE ID_ROL = @id_rol", parametros);
            MessageBox.Show("Rol hablilatado");
        }
    }
}
