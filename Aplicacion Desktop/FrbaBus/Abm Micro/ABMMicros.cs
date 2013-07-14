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

namespace FrbaBus.Abm_Micro
{
    public partial class ABMMicros : ABM
    {
        public ABMMicros()
        {
            InitializeComponent();
            this.Text = "ABM de Micros";
            this.Size = new Size(974, 536);
            this.Query = @"SELECT MI.ID_MICRO, MI.PATENTE PATENTE, MI.NUMERO, MA.MARCA MARCA, MODELO, TS.NOMBRE_SERVICIO SERVICIO, MI.CANTIDAD_ASIENTOS ASIENTOS, MI.KILOS_BODEGA BODEGA, MI.BAJA_SERVICIO EN_SERVICIO, MI.FECHA_ALTA ALTA, MI.BAJA_SERVICIO BAJA_SERVICIO, MI.FECHA_SERVICIO_DESDE SERVICIO_DESDE, MI.FECHA_SERVICIO_HASTA SERVICIO_HASTA, MI.BAJA_FIN_VIDA_UTIL BAJA, MI.FECHA_BAJA FECHA_BAJA, MI.TIPO_SERVICIO, MI.MARCA, MI.NUMERO
                FROM DEL_NAVAL.MICROS MI, DEL_NAVAL.TIPOS_SERVICIO TS , DEL_NAVAL.MARCAS MA";
            this.Condicion = @"TS.ID_SERVICIO = TIPO_SERVICIO
                AND MA.ID_MARCA = MI.MARCA";
            this.CampoBaja = "BAJA";
            this.CondicionCampoBaja = true;
            FiltroParcial patente = new FiltroParcial("Patente", "patente");
            AgregarFiltro(patente);
            columnasVisibles.Add("PATENTE", "Patente");
            columnasVisibles.Add("NUMERO", "Número");
            columnasVisibles.Add("MARCA", "Marca");
            columnasVisibles.Add("MODELO", "Modelo");
            columnasVisibles.Add("SERVICIO", "Tipo de Servicio");
            columnasVisibles.Add("ASIENTOS", "Cantidad de Asientos");
            columnasVisibles.Add("BODEGA", "Capacidad de la bodega");
            columnasVisibles.Add("ALTA", "Fecha de Alta");
            columnasVisibles.Add("EN_SERVICIO", "En servicio");
            columnasVisibles.Add("SERVICIO_DESDE", "En servicio desde");
            columnasVisibles.Add("SERVICIO_HASTA", "En servicio hasta");
            columnasVisibles.Add("FECHA_BAJA", "Fecha de baja");
            columnasVisibles.Add("BAJA", "Baja por fin de vida útil");
        }

        protected override void crear()
        {
            Form cargaMicro = new CargaMicro();
            cargaMicro.ShowDialog();
        }

        protected override void modificar(DataGridViewCellCollection fila)
        {
            Micro micro = new Micro();
            micro.Id_micro = fila["ID_MICRO"].Value.ToString();
            micro.Id_servicio = fila["SERVICIO"].Value.ToString();
            micro.Kilos_bodega = int.Parse(fila["BODEGA"].Value.ToString());
            micro.Marca = fila["MARCA"].Value.ToString();
            micro.Modelo = fila["MODELO"].Value.ToString();
            micro.Numero = fila["NUMERO"].Value.ToString();
            micro.Patente = fila["PATENTE"].Value.ToString();
            micro.Baja_fin_vida_util = bool.Parse(fila["BAJA"].Value.ToString());
            micro.Baja_servicio = bool.Parse(fila["BAJA_SERVICIO"].Value.ToString());
            micro.Cantidad_asientos = int.Parse(fila["ASIENTOS"].Value.ToString());
            if (fila["ALTA"].Value != DBNull.Value)
            {
                micro.Fecha_alta = DateTime.Parse(fila["ALTA"].Value.ToString());
            }
            if (fila["FECHA_BAJA"].Value != DBNull.Value)
            {
                micro.Fecha_baja = DateTime.Parse(fila["FECHA_BAJA"].Value.ToString());
            }
            if (fila["SERVICIO_DESDE"].Value != DBNull.Value)
            {
                micro.Fecha_servicio_desde = DateTime.Parse(fila["SERVICIO_DESDE"].Value.ToString());
            }
            if (fila["SERVICIO_HASTA"].Value != DBNull.Value)
            {
                micro.Fecha_servicio_hasta = DateTime.Parse(fila["SERVICIO_HASTA"].Value.ToString());
            }
            Form cargaMicro = new CargaMicro(micro);
            cargaMicro.ShowDialog();
        }

        protected override void eliminar(DataGridViewCellCollection fila)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@id_micro", fila["ID_MICRO"].Value.ToString()));
            parametros.Add(new SqlParameter("@fecha", Config.FechaSistema));
            int codigoRetorno = (int)DAC.ExecuteScalar(@"declare @retorno int
                exec intentarBajarMicro @id_micro, @fecha, NULL, @retorno output
                select @retorno ", parametros);
            switch (codigoRetorno)
            {
                case -1:
                    MessageBox.Show("Baja de micro realizada con éxito");
                    break;
                case -2:
                    Form conflictoMicroAlta = new ConfilctoMicro(int.Parse(fila["ID_MICRO"].Value.ToString()));
                    if (conflictoMicroAlta.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                    else
                    {
                        int segundoCodigoRetorno = (int)DAC.ExecuteScalar(@"declare @retorno int
                            exec intentarBajarMicro @id_micro, @fecha, NULL, @retorno output
                            select @retorno ", parametros);
                        if (segundoCodigoRetorno == -1)
                        {
                            MessageBox.Show("Micro dado de baja");
                        }
                    }
                    break;
                default:
                    Form conflictoMicroReemplazo = new ConfilctoMicro(int.Parse(fila["ID_MICRO"].Value.ToString()), codigoRetorno);
                    if (conflictoMicroReemplazo.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                    else
                    {
                        int segundoCodigoRetorno = (int)DAC.ExecuteScalar(@"declare @retorno int
                            exec intentarBajarMicro @id_micro, @fecha, NULL, @retorno output
                            select @retorno ", parametros);
                        if (segundoCodigoRetorno == -1)
                        {
                            MessageBox.Show("Micro dado de baja");
                        }
                    }
                    break;
            }
            
        }

        protected override void habilitar(DataGridViewCellCollection fila)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@id_micro", fila["ID_MICRO"].Value.ToString()));
            DAC.ExecuteNonQuery("UPDATE DEL_NAVAL.MICROS SET BAJA_FIN_VIDA_UTIL = 0, FECHA_BAJA = NULL WHERE ID_MICRO = @id_micro", parametros);
            MessageBox.Show("Micro habilitado");
        }
    }
}
