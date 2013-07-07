using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrbaBus.Abm_Permisos
{
    public partial class ABMRoles : Form
    {
        public ABMRoles()
        {
            InitializeComponent();
        }

        private void btnAlta_Click(object sender, EventArgs e)
        {
            CargaRol formAltaRol = new CargaRol();
            DialogResult resultado = formAltaRol.ShowDialog();
        }

        private void btnBajaModificacion_Click(object sender, EventArgs e)
        {
            ListadoRoles formListadoRoles = new ListadoRoles();
            DialogResult resultado = formListadoRoles.ShowDialog();
        }
    }
}
