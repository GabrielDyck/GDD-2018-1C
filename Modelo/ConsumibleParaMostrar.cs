using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrbaHotel.Modelo
{
    public class ConsumibleParaMostrar
    {
        private Consumible consumible = null;
        private int cantidad = 0;

        public ConsumibleParaMostrar(Consumible consumible, int cantidad)
        {
            this.consumible = consumible;
            this.cantidad = cantidad;
        }

        public Consumible getConsumible()
        {
            return this.consumible;
        }

        public int getCantidad()
        {
            return this.cantidad;
        }

        public void setCantidad(int cantidad)
        {
            this.cantidad = cantidad;
        }

        //Estos metodos extra los necesito para popular los combo box y data grid view
        public int Codigo { get { return this.getConsumible().getCodigo(); } }
        public String Descripcion { get { return this.getConsumible().getDescripcion(); } }
        public float Precio { get { return this.getConsumible().getPrecio(); } }
        public float Cantidad { get { return this.getCantidad(); } }
    }
}
