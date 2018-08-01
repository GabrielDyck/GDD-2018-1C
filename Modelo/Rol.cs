using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrbaHotel.Modelo
{
    public class Rol
    {
        private int idRol = 0;
        private String nombre = "";
        private Boolean activo = false;
        private List<Funcionalidad> funcionalidades = new List<Funcionalidad>();

        public Rol(int idRol, String nombre, Boolean activo, List<Funcionalidad> funcionalidades)
        {
            this.idRol = idRol;
            this.nombre = nombre;
            this.activo = activo;
            this.funcionalidades = funcionalidades;
        }

        public int getIdRol()
        {
            return this.idRol;
        }

        public String getNombre()
        {
            return this.nombre;
        }

        public Boolean getActivo()
        {
            return this.activo;
        }

        public List<Funcionalidad> getFuncionalidades()
        {
            return this.funcionalidades;
        }

        public Boolean esNuevo()
        {
            return idRol.Equals(0);
        }

        public void setNombre(String nombre)
        {
            this.nombre = nombre;
        }

        public void setActivo(Boolean activo)
        {
            this.activo = activo;
        }

        public void setFuncionalidades(List<Funcionalidad> funcionalidades)
        {
            this.funcionalidades = funcionalidades;
        }

        //Estos metodos extra los necesito para popular los combo box y data grid view
        //public int IdRol { get { return this.getIdRol(); } }
        public String Nombre { get { return this.getNombre(); } }
        public Boolean Activo { get { return this.getActivo(); } }
        public List<Funcionalidad> Funcionalidades { get { return this.getFuncionalidades(); } }

    }
}
