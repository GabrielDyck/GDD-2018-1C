using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrbaHotel.Modelo
{
    public class Direccion
    {
        private int idDireccion = 0;
        private String pais = "";
        private String ciudad = "";
        private String calle = "";
        private String departamento = "";
        private int numeroCalle = 0;
        private int piso = 0;        
        private int idIdentidad = 0;

        public Direccion(int idDireccion, String pais, String ciudad,
            String calle, int numeroCalle, int piso, String departamento)
        {
            this.idDireccion = idDireccion;
            this.pais = pais;
            this.ciudad = ciudad;
            this.calle = calle;
            this.numeroCalle = numeroCalle;
            this.piso = piso;
            this.departamento = departamento;
        }
        public Direccion(int idDireccion, String pais, String ciudad,
            String calle, int numeroCalle, int piso, String departamento, int idIdentidad)
        {
            this.idDireccion = idDireccion;
            this.idIdentidad = idIdentidad;
            this.pais = pais;
            this.ciudad = ciudad;
            this.calle = calle;
            this.numeroCalle = numeroCalle;
            this.piso = piso;
            this.departamento = departamento;
        }
        public int getIdDireccion()
        {
            return this.idDireccion;
        }
        public int getIdIdentidad()
        {
            return this.idIdentidad;
        }
        public void setIdIdentidad(int idIdentidad)
        {
            this.idIdentidad = idIdentidad;
        }

        public String getPais()
        {
            return this.pais;
        }

        public String getCiudad()
        {
            return this.ciudad;
        }

        public String getCalle()
        {
            return this.calle;
        }

        public int getNumeroCalle()
        {
            return this.numeroCalle;
        }

        public int getPiso()
        {
            return this.piso;
        }

        public String getDepartamento()
        {
            return this.departamento;
        }

        public Boolean esNuevo()
        {
            return idDireccion.Equals(0);
        }

        //METODO AUXILIAR PARA EL ABM USUARIO
        public String getDireccionSimple()
        {
            return this.getCalle() + ", " + this.getNumeroCalle().ToString() + ", " + this.getPiso().ToString() + ", " + this.getCiudad();
        }

        //METODO AUXILIAR PARA EL ABM CLIENTES
        public String getDireccionCompleta()
        {
            return this.getCalle() + ", " + this.getNumeroCalle().ToString() + ", " + this.getPiso().ToString() + ", " + this.getCiudad() + ", " + this.getPais();
        }

        //METODO AUXILIAR PARA EL ABM HOTEL
        public String getDireccionCorta()
        {
            return this.getCalle() + ", " + this.getNumeroCalle().ToString() + ", " + this.getPiso().ToString();
        }

        //Estos metodos extra los necesito para popular los combo box y data grid view
        //public int IdDireccion { get { return this.getIdDireccion(); } }
        public String Pais { get { return this.getPais(); } }
        public String Ciudad { get { return this.getCiudad(); } }
        public String Calle { get { return this.getCalle(); } }
        public int NumeroCalle { get { return this.getNumeroCalle(); } }
        public int Piso { get { return this.getPiso(); } }
        public String Departamento { get { return this.getDepartamento(); } }
    }
}
