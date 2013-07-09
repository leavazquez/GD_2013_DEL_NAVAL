using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using FrbaBus.Filtros;

namespace FrbaBus
{
    public abstract partial class ABM : Form
    {
        private List<Control> filtros = new List<Control>();
        private List<Button> comandos = new List<Button>();
        protected string Query;
        protected string Condicion = "";
        protected string CampoBaja;

        public ABM()
        {
            InitializeComponent();
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
            comandos.Add(btnEliminar);
            comandos.Add(btnModificar);
        }

        public void AgregarFiltro (Control filtro)
        {
            gbFiltros.Controls.Add(filtro);
            filtros.Add(filtro);
        }

        public void AgregarComando(Button comando)
        {
            gbComandos.Controls.Add(comando);
            comandos.Add(comando);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            string nuevaQuery = this.Query;

            bool primero = this.Condicion == "";
            nuevaQuery += this.Condicion != "" ? " WHERE " + this.Condicion : "" ;
            foreach (Filtro filtro in filtros) // usar clase base de fltros
            {
                if (filtro.Valor != "")
                {
                    nuevaQuery += primero ? " WHERE " : " AND ";
                    primero = false;
                    nuevaQuery += filtro.Condicion;
                    parametros.Add(filtro.Parametro);
                }
            }
            DataTable resultados = DAC.ExecuteReader(nuevaQuery, parametros);
            // ejecutar quey y llenar el datatable
            dgvResultados.DataSource = resultados;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            foreach (Filtro filtro in filtros)
            {
                filtro.Limpiar();
            }
        }

        private void dgvResultados_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            //if (e.StateChanged != DataGridViewElementStates.Selected) return;
            if (dgvResultados.SelectedRows.Count == 0)
            {
                btnEliminar.Enabled = false;
                btnModificar.Enabled = false;
            }
            else
            {
                btnEliminar.Enabled = true;
                btnModificar.Enabled = true;

                if (this.CampoBaja != null)
                {
                    if (dgvResultados.SelectedRows[0].Cells[this.CampoBaja].ToString() == "true")
                    {
                        btnEliminar.Text = "Habilitar";
                    }
                }
            }
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            crear();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            modificar(dgvResultados.SelectedRows[0].Cells);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            switch (btnEliminar.Text)
            {
                case "Eliminar": eliminar(dgvResultados.SelectedRows[0].Cells);
                    break;
                case "Habilitar": habilitar(dgvResultados.SelectedRows[0].Cells);
                    break;
            }
            btnBuscar.PerformClick();
        }

        protected abstract void crear();
        protected abstract void modificar(DataGridViewCellCollection fila);
        protected abstract void eliminar(DataGridViewCellCollection fila);
        protected abstract void habilitar(DataGridViewCellCollection fila);
    }
}
