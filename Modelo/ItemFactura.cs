using FrbaHotel.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrbaHotel.Modelo
{
    public class ItemFactura
    {
        private int idItemFactura = 0;
        private int idConsumible = 0;
        private int idFactura = 0;
        private Consumible consumible = null;
        private float cantidad = 0;
        private float monto = 0;
        private DateTime fechaCreacion = Utils.getSystemDatetimeNow();

        public ItemFactura(int idItemFactura, int idConsumible, float cantidad, float monto, DateTime fechaCreacion,int idFactura)
        {
            this.idItemFactura = idItemFactura;
            this.idFactura = idFactura;
            this.idConsumible = idConsumible;
            //this.consumible = consumible;
            this.cantidad = cantidad;
            this.monto = monto;
            this.fechaCreacion = fechaCreacion;
        }

        public int getIdItemFactura()
        {
            return idItemFactura;
        }
        public int getIdFactura()
        {
            return idFactura;
        }
        public void setIdFactura(int idFactura)
        {
            this.idFactura = idFactura;
        }

        public int getIdConsumible()
        {
            return idConsumible;
        }

        public Consumible getConsumible()
        {
            return consumible;
        }

        public float getCantidad()
        {
            return cantidad;
        }

        public float getMonto()
        {
            return monto;
        }

        public DateTime getFechaCreacion()
        {
            return fechaCreacion;
        }

        public Boolean esNuevo()
        {
            return idItemFactura.Equals(0);
        }

        //Estos metodos extra los necesito para popular los combo box y data grid view
        //public int IdItemFactura { get { return this.getIdItemFactura(); } }
        public Consumible Consumible { get { return this.getConsumible(); } }
        public float Cantidad { get { return this.getCantidad(); } }
        public float Monto { get { return this.getMonto(); } }
        public DateTime FechaCreacion { get { return this.getFechaCreacion(); } }
    }
}
