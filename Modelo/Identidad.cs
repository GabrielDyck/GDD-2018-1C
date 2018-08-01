using FrbaHotel.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrbaHotel.Modelo
{
    public class Identidad
    {
        private int idIdentidad = 0;
        private String tipoIdentidad = "";
        private String nombre = "";
        private String apellido = "";
        private String tipoDocumento = "";
        private String numeroDocumento = "";
        private String mail = "";
        private DateTime fechaNacimiento = Utils.getSystemDatetimeNow();
        private String nacionalidad = "";
        private String telefono = "";
        private List<Direccion> direcciones = new List<Direccion>();
        private Direccion direccion = null;

        public Identidad(int idIdentidad, String tipoIdentidad, String nombre, String apellido, String tipoDocumento, String numeroDocumento,
            String mail, DateTime fechaNacimiento, String nacionalidad, String telefono, List<Direccion> direcciones)
        {
            this.idIdentidad = idIdentidad;
            this.tipoIdentidad = tipoIdentidad;
            this.nombre = nombre;
            this.apellido = apellido;
            this.tipoDocumento = tipoDocumento;
            this.numeroDocumento = numeroDocumento;
            this.mail = mail;
            this.fechaNacimiento = fechaNacimiento;
            this.nacionalidad = nacionalidad;
            this.telefono = telefono;
            this.direcciones = direcciones;
            //PARA MANTENER LA COMPATIBILIDAD ENTRE LOS CONSTRUCTORES Y EL MODELO
            this.direccion = direcciones.First();
        }
        public Identidad(int idIdentidad,String tipoIdentidad, String nombre, String apellido, String tipoDocumento, String numeroDocumento,
            String mail, DateTime fechaNacimiento, String nacionalidad, String telefono, Direccion direccion)
        {
            this.idIdentidad = idIdentidad;
            this.tipoIdentidad = tipoIdentidad;
            this.nombre = nombre;
            this.apellido = apellido;
            this.tipoDocumento = tipoDocumento;
            this.numeroDocumento = numeroDocumento;
            this.mail = mail;
            this.fechaNacimiento = fechaNacimiento;
            this.nacionalidad = nacionalidad;
            this.telefono = telefono;
            this.direccion = direccion;
            //PARA MANTENER LA COMPATIBILIDAD ENTRE LOS CONSTRUCTORES Y EL MODELO
            this.direcciones.Add(direccion);
        }
        public int getIdIdentidad()
        {
            return this.idIdentidad;
        }

        public String getTipoIdentidad()
        {
            return this.tipoIdentidad;
        }

        public String getNombre()
        {
            return this.nombre;
        }

        public String getNombreCompleto()
        {
            return this.getApellido() + ", " + this.getNombre();
        }

        public String getApellido()
        {
            return this.apellido;
        }

        public String getTipoDocumento()
        {
            return this.tipoDocumento;
        }

        public String getNumeroDocumento()
        {
            return this.numeroDocumento;
        }

        public String getMail()
        {
            return this.mail;
        }

        public DateTime getFechaNacimiento()
        {
            return this.fechaNacimiento;
        }

        public String getNacionalidad()
        {
            return this.nacionalidad;
        }

        public String getTelefono()
        {
            return this.telefono;
        }

        public List<Direccion> getDirecciones()
        {
            return this.direcciones;
        }
        public Direccion getDireccion()
        {
            return this.direccion;
        }
        public Boolean esNuevo()
        {
            return idIdentidad.Equals(0);
        }

        //Estos metodos extra los necesito para popular los combo box y data grid view
        //public int IdIdentidad { get { return this.getIdIdentidad(); } }
        public String TipoIdentidad { get { return this.getTipoIdentidad(); } }
        public String Nombre { get { return this.getNombre(); } }
        public String Apellido { get { return this.getApellido(); } }
        public String TipoDocumento { get { return this.getTipoDocumento(); } }
        public String NumeroDocumento { get { return this.getNumeroDocumento(); } }
        public String Mail { get { return this.getMail(); } }
        public DateTime FechaNacimiento { get { return this.getFechaNacimiento(); } }
        public String Nacionalidad { get { return this.getNacionalidad(); } }
        public String Telefono { get { return this.getTelefono(); } }
        public List<Direccion> Direcciones { get { return this.getDirecciones(); } }
    }
}
