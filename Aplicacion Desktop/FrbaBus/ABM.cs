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
        protected List<Button> comandos = new List<Button>();
        protected string Query;
        protected string Condicion = "";
        protected string GroupBy = "";
        protected string CampoBaja;
        protected bool CondicionCampoBaja;
        protected Dictionary<string, string> columnasVisibles = new Dictionary<string, string>();
        private Point puntoInicialFiltros = new Point(20,25);

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
            filtro.Location = new Point(puntoInicialFiltros.X, puntoInicialFiltros.Y);
            filtros.Add(filtro);
            if (filtros.Count % 3 == 0)
            {
                puntoInicialFiltros.Y -= 60;
                puntoInicialFiltros.X += 250;
            }
            else
            {
                puntoInicialFiltros.Y += 30;
            }
            gbFiltros.Controls.Add(filtro);
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
            if (this.GroupBy != "")
            {
                nuevaQuery += " " + this.GroupBy;
            }
            DataTable resultados = DAC.ExecuteReader(nuevaQuery, parametros);
            // ejecutar quey y llenar el datatable
            dgvResultados.DataSource = resultados;
            if (columnasVisibles.Count > 0)
            {
                foreach (DataGridViewColumn columna in dgvResultados.Columns)
                {
                    columna.Visible = false;
                }
                foreach (DataColumn columna in resultados.Columns)
                {
                    if (columnasVisibles.Keys.Contains<string>(columna.ColumnName))
                    {
                        dgvResultados.Columns[columna.ColumnName].HeaderText = columnasVisibles[columna.ColumnName];
                        dgvResultados.Columns[columna.ColumnName].Visible = true;
                    }
                }
            }
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

                string condicion = this.CondicionCampoBaja ? "True" : "False";
                if (this.CampoBaja != null)
                {
                    if (dgvResultados.SelectedRows[0].Cells[this.CampoBaja].Value.ToString() == condicion)
                    {
                        btnEliminar.Text = "Habilitar";
                        btnModificar.Enabled = false;
                    }
                    else
                    {
                        btnEliminar.Text = "Eliminar";
                    }
                }
            }
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            crear();
            btnBuscar.PerformClick();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            modificar(dgvResultados.SelectedRows[0].Cells);
            btnBuscar.PerformClick();
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
