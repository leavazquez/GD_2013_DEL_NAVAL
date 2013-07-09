﻿using System;
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
            this.Query = "SELECT * FROM DEL_NAVAL.ROLES";
            AgregarFiltro(new FiltroParcial("Nombre", "nombre_rol"));
        }

        protected override void modificar(DataGridViewCellCollection fila)
        {
            Rol rol = new Rol();
            rol.Id_rol = fila["id_rol"].Value.ToString();
            rol.Nombre_rol = fila["nombre_rol"].Value.ToString();
            Form cargaRol = new CargaRol(rol);
            cargaRol.Show();
        }

        protected override void crear()
        {
            CargaRol formCargaRol = new CargaRol();
            formCargaRol.Show();
        }

        protected override void eliminar(DataGridViewCellCollection fila)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@id_rol", fila["id_rol"].Value.ToString()));
            DAC.ExecuteNonQuery("exec dbo.inhabilitar_rol @id_rol", parametros);
        }

        protected override void habilitar(DataGridViewCellCollection fila)
        {
            throw new NotImplementedException();
        }
    }
}
