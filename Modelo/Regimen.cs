using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrbaHotel.Modelo
{
    public class Regimen
    {
        private int idRegimen = 0;
        //private Hotel hotel = null;
        private String codigoRegimen = "";
        private String descripcion = "";
        private decimal precio = 0;
        Boolean estado = true;

        //public Regimen(int idRegimen, Hotel hotel, int codigoRegimen, String descripcion, float precio, String estado)
        public Regimen(int idRegimen, String codigoRegimen, String descripcion, decimal precio, Boolean estado)
        {
            this.idRegimen = idRegimen;
            //this.hotel = hotel;
            this.codigoRegimen = codigoRegimen;
            this.descripcion = descripcion;
            this.precio = precio;
            this.estado = estado;
        }

        public int getIdRegimen()
        {
            return this.idRegimen;
        }

        /*
        public Hotel getHotel()
        {
            return this.hotel;
        }
        */

        public String getCodigoRegimen()
        {
            return this.codigoRegimen;
        }

        public String getDescripcion()
        {
            return this.descripcion;
        }

        public decimal getPrecio()
        {
            return this.precio;
        }

        public Boolean getActivo()
        {
            return this.estado;
        }

        public Boolean esNuevo()
        {
            return idRegimen.Equals(0);
        }

        //Estos metodos extra los necesito para popular los combo box y data grid view
        //public Hotel Hotel { get { return this.getHotel(); } }
        public String CodigoRegimen { get { return this.getCodigoRegimen(); } }
        public String Descripcion { get { return this.getDescripcion(); } }
        public decimal Precio { get { return this.getPrecio(); } }
        public Boolean Activo { get { return this.getActivo(); } }
    }
}
