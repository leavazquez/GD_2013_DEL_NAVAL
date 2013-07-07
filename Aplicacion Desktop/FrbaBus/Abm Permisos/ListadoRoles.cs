using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrbaBus.Filtros;

namespace FrbaBus.Abm_Permisos
{
    public partial class ListadoRoles : Listado
    {
        public ListadoRoles()
        {
            InitializeComponent();
            AgregarFiltro(new FiltroExacto("Nombre", "nombre_rol"));
        }
    }
}
