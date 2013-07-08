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

namespace FrbaBus.Abm_Permisos
{
    public partial class ListadoRoles : Listado
    {
        public ListadoRoles()
        {
            InitializeComponent();
            this.Query = "SELECT * FROM DEL_NAVAL.ROLES";
            AgregarFiltro(new FiltroParcial("Nombre", "nombre_rol"));
        }

        protected override void modificar(DataGridViewCellCollection fila)
        {
            Rol rol = new Rol();
            rol.id_rol = fila["id_rol"].Value.ToString();
            rol.nombre_rol = fila["nombre_rol"].Value.ToString();
            Form cargaRol = new CargaRol(rol);
            cargaRol.Show();
        }

        protected override void eliminar(DataGridViewCellCollection fila)
        {
            throw new NotImplementedException();
        }

        protected override void habilitar(DataGridViewCellCollection fila)
        {
            throw new NotImplementedException();
        }
    }
}
