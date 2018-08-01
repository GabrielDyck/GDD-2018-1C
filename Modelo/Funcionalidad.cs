using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrbaHotel.Modelo
{
    public class Funcionalidad
    {
        private int idFuncionalidad = 0;
        private String descripcion = "";

        public Funcionalidad(int idFuncionalidad, String descripcion)
        {
            this.idFuncionalidad = idFuncionalidad;
            this.descripcion = descripcion;
        }

        public int getIdFuncionalidad()
        {
            return this.idFuncionalidad;
        }

        public String getDescripcion()
        {
            return this.descripcion;
        }

        //Estos metodos extra los necesito para popular los combo box y data grid view
        //public int IdFuncionalidad { get { return this.getIdFuncionalidad(); } }
        public String Descripcion { get { return this.getDescripcion(); } }

    }
}
