using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrbaHotel.Modelo
{
    public class TipoHabitacion
    {
        private int idTipoHabitacion = 0;
        private String codigo = "";
        private decimal porcentual = 0;
        private String descripcion = "";
        public TipoHabitacion() { }

        public TipoHabitacion(int idTipoHabitacion, String codigo, decimal porcentual, String descripcion)
        {
            this.idTipoHabitacion = idTipoHabitacion;
            this.codigo = codigo;
            this.porcentual = porcentual;
            this.descripcion = descripcion;
        }

        public int getIdTipoHabitacion()
        {
            return this.idTipoHabitacion;
        }

        public String getCodigo()
        {
            return this.codigo;
        }

        public decimal getPorcentual()
        {
            return this.porcentual;
        }

        public String getDescripcion()
        {
            return this.descripcion;
        }

        public void setIdTipoHabitacion(int idTipoHabitacion)
        {
            this.idTipoHabitacion = idTipoHabitacion;
        }

        //Estos metodos extra los necesito para popular los combo box y data grid view

        public String Codigo { get { return this.getCodigo(); } }
        public decimal Porcentual { get { return this.getPorcentual(); } }
        public String Descripcion { get { return this.getDescripcion(); } }
        //public int IdTipoHabitacion { get { return this.getIdTipoHabitacion(); } }
    }
}
