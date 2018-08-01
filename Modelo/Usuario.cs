using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrbaHotel.Modelo
{
    public class Usuario
    {
        private int idUsuario = 0;
        private Identidad identidad = null;
        private String username = "";
        private String password = "";
        private int intentosFallidosLogin = 0;
        private Boolean activo = false;
        private List<Rol> roles = new List<Rol>();
        private List<Hotel> hoteles = new List<Hotel>();

        public Usuario(int idUsuario, Identidad identidad, String username, String password, int intentosFallidosLogin, Boolean activo, List<Rol> roles, List<Hotel> hoteles)
        {
            this.idUsuario = idUsuario;
            this.identidad = identidad;
            this.username = username;
            this.password = password;
            this.intentosFallidosLogin = intentosFallidosLogin;
            this.activo = activo;
            this.roles = roles;
            this.hoteles = hoteles;
        }

        public int getIdUsuario()
        {
            return this.idUsuario;
        }

        public Identidad getIdentidad()
        {
            return this.identidad;
        }

        public String getUsername()
        {
            return this.username;
        }

        public String getPassword()
        {
            return this.password;
        }

        public int getIntentosFallidosLogin()
        {
            return this.intentosFallidosLogin;
        }

        public Boolean getActivo()
        {
            return this.activo;
        }

        public List<Rol> getRoles()
        {
            return this.roles;
        }

        public List<Hotel> getHoteles()
        {
            return this.hoteles;
        }

        public Boolean esNuevo()
        {
            return idUsuario.Equals(0);
        }

        public void setActivo(Boolean activo)
        {
            this.activo = activo;
        }

        public void incrementarIntentosFallidosLogin()
        {
            this.intentosFallidosLogin++;
        }

        public void resetearIntentosFallidosLogin()
        {
            this.intentosFallidosLogin = 0;
        }

        public void setPassword(String password){
            this.password = password;
        }
        //Estos metodos extra los necesito para popular los combo box y data grid view
        //public int IdUsuario { get { return this.getIdUsuario(); } }
        public String Username { get { return this.getUsername(); } }
        //PARA SIMPLIFICAR SE MUESTRA UN SOLO ROL
        public String Rol { get { return this.getRoles().First().getNombre(); } }
        public String Nombre { get { return this.getIdentidad().getNombre(); } }
        public String Apellido { get { return this.getIdentidad().getApellido(); } }
        public String TipoDoc { get { return this.getIdentidad().getTipoDocumento(); } }
        public String NroDoc { get { return this.getIdentidad().getNumeroDocumento(); } }
        public String Mail { get { return this.getIdentidad().getMail(); } }
        public String Telefono { get { return this.getIdentidad().getTelefono(); } }
        //ACA ASUMO QUE TENGO UNA SOLA DIRECCION EN EL USUARIO
        public String Direccion { get { return this.getIdentidad().getDirecciones().First().getDireccionSimple(); } }
        public String FechaNac { get { return this.getIdentidad().getFechaNacimiento().ToString(); } }
        //ACA ASUMO QUE TRABAJA EN UN SOLO HOTEL
        //public String Hotel { get { return this.getHoteles().First().getNombre(); } }
        //FALTA TERMINAR LA OBTENCION DEL HOTEL EN EL MODELO.
        public int IntentosFallidosLogin { get { return this.getIntentosFallidosLogin(); } }
        public Boolean Activo { get { return this.getActivo(); } }
    }
}
