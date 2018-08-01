using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrbaHotel.Modelo
{
    public class Consumible
    {
        private int idConsumible = 0;
        private int codigo = 0;
        private String descripcion = "";
        private float precio = 0;
        //private int idEstadia = 0;

        public Consumible(int idConsumible, int codigo, String descripcion, float precio)
        {
            this.idConsumible = idConsumible;
            this.codigo = codigo;
            this.descripcion = descripcion;
            this.precio = precio;
        }
        
        public int getIdConsumible()
        {
            return this.idConsumible;
        }

        /*
        public int getIdEstadia()
        {
            return this.idEstadia;
        }
        
        public void setIdEstadia(int idEstadia)
        {
            this.idEstadia = idEstadia;
        }
        */

        public int getCodigo()
        {
            return this.codigo;
        }

        public String getDescripcion()
        {
            return this.descripcion;
        }

        public float getPrecio()
        {
            return this.precio;
        }

        public Boolean esNuevo()
        {
            return idConsumible.Equals(0);
        }

        //Estos metodos extra los necesito para popular los combo box y data grid view
        public int Codigo { get { return this.getCodigo(); } }
        public String Descripcion { get { return this.getDescripcion(); } }
        public float Precio { get { return this.getPrecio(); } }
    }
}
