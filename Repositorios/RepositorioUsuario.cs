using FrbaHotel.Excepciones;
using FrbaHotel.Modelo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace FrbaHotel.Repositorios
{
    public class RepositorioUsuario : Repositorio<Usuario>
    {
        override public Usuario getById(int idUsuario)
        {
            //Elementos del usuario a devolver
            Usuario usuario;
            Boolean activo = false;
            int idIdentidad = 0;
            Identidad identidad = null;
            RepositorioIdentidad repoIdentidad = new RepositorioIdentidad();
            String username = "";
            String password = "";
            int intentosFallidosLogin = 0; 
            List<Rol> roles = new List<Rol>();
            List<Hotel> hoteles = new List<Hotel>();

            //Configuraciones de la consulta
            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            //Primera Consulta
            sqlCommand.Parameters.AddWithValue("@idUsuario", idUsuario);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT * FROM LOS_BORBOTONES.Usuario WHERE idUsuario = @idUsuario";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                username = reader.GetString(reader.GetOrdinal("Username"));
                password = reader.GetString(reader.GetOrdinal("Password"));
                activo = reader.GetBoolean(reader.GetOrdinal("Activo"));
                intentosFallidosLogin = reader.GetInt32(reader.GetOrdinal("IntentosFallidosLogin"));
                idIdentidad = reader.GetInt32(reader.GetOrdinal("idIdentidad"));
                identidad = repoIdentidad.getById(idIdentidad);
            }

            //Cierro Primera Consulta
            sqlConnection.Close();

            //Si no encuentro elemento con ese ID tiro una excepción
            if (username.Equals("")) throw new NoExisteIDException("No existe usuario con el ID asociado");

            //Segunda Consulta para llenar los roles
            sqlCommand.CommandText = @"
                SELECT r.idRol
                FROM LOS_BORBOTONES.Rol_X_Usuario ru
                INNER JOIN LOS_BORBOTONES.Rol r ON r.idRol = ru.idRol
                WHERE ru.idUsuario = @idUsuario
            ";

            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();

            //Colecto los roles
            while (reader.Read())
            {
                //lleno el rol con el getbyID
                int idRol = reader.GetInt32(reader.GetOrdinal("idRol"));
                RepositorioRol repoRol = new RepositorioRol();
                roles.Add(repoRol.getById(idRol));
            }

            sqlConnection.Close();

            //Tercera Consulta para llenar los hoteles
            sqlCommand.CommandText = @"
                SELECT h.idHotel
                FROM LOS_BORBOTONES.Hotel_X_Usuario hu
                INNER JOIN LOS_BORBOTONES.Hotel h ON h.idHotel = hu.idHotel
                WHERE hu.idUsuario = @idUsuario
            ";

            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();

            //Colecto los hoteles
            while (reader.Read())
            {
                //lleno el hotel con el getbyID                
                int idHotel = reader.GetInt32(reader.GetOrdinal("idHotel"));
                RepositorioHotel repoHotel = new RepositorioHotel();
                hoteles.Add(repoHotel.getById(idHotel));         
            }

            sqlConnection.Close();
    
            //Armo el usuario completo
            usuario = new Usuario(idUsuario, identidad, username, password, intentosFallidosLogin, activo, roles, hoteles);

            return usuario;
        }

        override public List<Usuario> getAll()
        {
            List<Usuario> usuarios = new List<Usuario>();

            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;

            sqlCommand.CommandText = "SELECT idUsuario FROM LOS_BORBOTONES.Usuario";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                usuarios.Add(this.getById(reader.GetInt32(reader.GetOrdinal("idUsuario"))));
            }

            sqlConnection.Close();

            return usuarios;
        }

        override public int create(Usuario usuario)
        {
            int idUsuario = 0;
            RepositorioIdentidad repoIdentidad = new RepositorioIdentidad();
            if (this.exists(usuario) || repoIdentidad.exists(usuario.getIdentidad()))
            {
                throw new ElementoYaExisteException("Ya existe un usuario con el mismo nombre, o su Mail/Tipo y Numero de documento está repetido.");
            }
            else
            {
                //llamo a crear la identidad y traigo el IdIdentidad
                //RepositorioIdentidad repoIdentidad = new RepositorioIdentidad();
                int idIdentidad = repoIdentidad.create(usuario.getIdentidad());

                //llamo a crear la direccion y traigo el IdDireccion, le seteo el idIdentidad que lo necesita
                usuario.getIdentidad().getDireccion().setIdIdentidad(idIdentidad);
                RepositorioDireccion repoDireccion = new RepositorioDireccion();
                int idDireccion= repoDireccion.create(usuario.getIdentidad().getDireccion());

                //ahora ya tengo todo para crear el usuario

                String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                SqlCommand sqlCommand = new SqlCommand();
                SqlDataReader reader;

                //encripto clave
                string passwordEncriptada = this.EncriptarSHA256(usuario.getPassword());
        
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Parameters.AddWithValue("@Username", usuario.getUsername());
                sqlCommand.Parameters.AddWithValue("@Activo", usuario.getActivo());
                sqlCommand.Parameters.AddWithValue("@Password", passwordEncriptada);
                sqlCommand.Parameters.AddWithValue("@IntentosFallidosLogin", usuario.getIntentosFallidosLogin());
                sqlCommand.Parameters.AddWithValue("@idIdentidad", idIdentidad);

                StringBuilder sqlBuilder = new StringBuilder();
                sqlBuilder.Append(@"
                    BEGIN TRY
                    BEGIN TRANSACTION

                    INSERT INTO LOS_BORBOTONES.Usuario(Username,Password,IntentosFallidosLogin,Activo,idIdentidad)
                    OUTPUT INSERTED.idUsuario
                    VALUES(@Username,@Password,@IntentosFallidosLogin,@Activo,@idIdentidad);

                    DECLARE @idUsuario int;
                    SET @idUsuario = SCOPE_IDENTITY();
                ");

                //AGREGO DINAMICAMENTE LOS ROLES A LA CONSULTA
                int i = 1;
                foreach (Rol r in usuario.getRoles())
                {
                    String paramName = "@idRol" + i.ToString();
                    sqlBuilder.AppendFormat("INSERT INTO LOS_BORBOTONES.Rol_X_Usuario(idRol,idUsuario) VALUES ({0}, @idUsuario)", paramName);
                    sqlCommand.Parameters.AddWithValue(paramName, r.getIdRol());
                    i++;
                }
                //AGREGO DINAMICAMENTE LOS HOTELES A LA CONSULTA
                int k = 1;
                foreach (Hotel h in usuario.getHoteles())
                {
                    String paramName = "@idHotel" + k.ToString();
                    sqlBuilder.AppendFormat("INSERT INTO LOS_BORBOTONES.Hotel_X_Usuario(idHotel,idUsuario) VALUES ({0}, @idUsuario)", paramName);
                    sqlCommand.Parameters.AddWithValue(paramName, h.getIdHotel());
                    k++;
                }

                sqlBuilder.Append(@"
                    COMMIT
                    END TRY

                    BEGIN CATCH
                    ROLLBACK
                    END CATCH
                ");

                sqlCommand.CommandText = sqlBuilder.ToString();
                sqlConnection.Open();
                reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    idUsuario = reader.GetInt32(reader.GetOrdinal("idUsuario"));
                }

                sqlConnection.Close();
            }

            return idUsuario;
        }

        /*
        private Boolean yaExisteMismoMailDistintoUsuario(Usuario usuario)
        {
            int idOtraIdentidad = -1;

            //Configuraciones de la consulta
            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            //Primera Consulta
            sqlCommand.Parameters.AddWithValue("@Mail", usuario.getIdentidad().getMail());
            sqlCommand.Parameters.AddWithValue("@idUsuario", usuario.getIdUsuario());
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = @"
                SELECT identidad.idIdentidad
                FROM LOS_BORBOTONES.Identidad identidad
                    ,LOS_BORBOTONES.Usuario usuario
                WHERE usuario.idUsuario <> @idUsuario
                  AND identidad.idIdentidad = usuario.idIdentidad
                  AND identidad.Mail = @Mail
            ";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                idOtraIdentidad = reader.GetInt32(reader.GetOrdinal("idIdentidad"));
            }

            //Cierro Primera Consulta
            sqlConnection.Close();

            return !idOtraIdentidad.Equals(-1);
        }

        private Boolean yaExisteMismoTipoYDocDistintoUsuario(Usuario usuario)
        {
            int idOtraIdentidad = -1;

            //Configuraciones de la consulta
            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            //Primera Consulta
            sqlCommand.Parameters.AddWithValue("@Tipo", usuario.getIdentidad().getTipoDocumento());
            sqlCommand.Parameters.AddWithValue("@Num", usuario.getIdentidad().getNumeroDocumento());
            sqlCommand.Parameters.AddWithValue("@idUsuario", usuario.getIdUsuario());
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = @"
                SELECT TOP 1 * 
                FROM LOS_BORBOTONES.Identidad identidad
                    ,LOS_BORBOTONES.Usuario usuario
                WHERE usuario.idUsuario <> @idUsuario
                  AND identidad.idIdentidad = usuario.idIdentidad
                  AND identidad.TipoDocumento = @Tipo
                  AND identidad.NumeroDocumento = @Num
            ";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                idOtraIdentidad = reader.GetInt32(reader.GetOrdinal("idIdentidad"));
            }

            //Cierro Primera Consulta
            sqlConnection.Close();

            return !idOtraIdentidad.Equals(-1);
        }
        */

        private Boolean yaExisteMismoUsername(Usuario usuario)
        {
            int idOtroUsuario = -1;

            //Configuraciones de la consulta
            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            //Primera Consulta
            sqlCommand.Parameters.AddWithValue("@idUsuario", usuario.getIdUsuario());
            sqlCommand.Parameters.AddWithValue("@userName", usuario.getUsername());
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = @"
                SELECT usuario.idUsuario
                FROM LOS_BORBOTONES.Usuario usuario
                WHERE usuario.idUsuario <> @idUsuario
                  AND usuario.Username = @userName
            ";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                idOtroUsuario = reader.GetInt32(reader.GetOrdinal("idUsuario"));
            }

            //Cierro Primera Consulta
            sqlConnection.Close();

            return !idOtroUsuario.Equals(-1);
        }

        override public void update(Usuario usuario)
        {
            RepositorioIdentidad repoIdentidad = new RepositorioIdentidad();

            //AGREGO VALIDACIONES AL UPDATE
            if (repoIdentidad.yaExisteOtraIdentidadMismoMailOTipoYNumDoc(usuario.getIdentidad()))
            {
                throw new ElementoYaExisteException("Ya existe un cliente o un usuario con el mismo mail o tipo y numero de documento.");
            }

            /*
            if (this.yaExisteMismoMailDistintoUsuario(usuario))
            {
                throw new ElementoYaExisteException("Ya existe un usuario o cliente con el mismo mail.");
            }

            if (this.yaExisteMismoTipoYDocDistintoUsuario(usuario))
            {
                throw new ElementoYaExisteException("Ya existe un usuario o cliente con el mismo documento.");
            }
            */

            if (this.yaExisteMismoUsername(usuario))
            {
                throw new ElementoYaExisteException("Ya existe un usuario con el mismo nombre de usuario.");
            }

            if (this.exists(usuario))
            {
                String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                SqlCommand sqlCommand = new SqlCommand();
                SqlDataReader reader;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = sqlConnection;

                //PARAMETERS DE LA DIRECCION
                sqlCommand.Parameters.AddWithValue("@Pais", usuario.getIdentidad().getDireccion().getPais());
                sqlCommand.Parameters.AddWithValue("@Ciudad", usuario.getIdentidad().getDireccion().getCiudad());
                sqlCommand.Parameters.AddWithValue("@Calle", usuario.getIdentidad().getDireccion().getCalle());
                sqlCommand.Parameters.AddWithValue("@NumeroCalle", usuario.getIdentidad().getDireccion().getNumeroCalle());
                sqlCommand.Parameters.AddWithValue("@Piso", usuario.getIdentidad().getDireccion().getPiso());
                sqlCommand.Parameters.AddWithValue("@Departamento", usuario.getIdentidad().getDireccion().getDepartamento());
                sqlCommand.Parameters.AddWithValue("@idDireccion", usuario.getIdentidad().getDireccion().getIdDireccion());

                //PARAMETERS DE LA IDENTIDAD
                sqlCommand.Parameters.AddWithValue("@TipoIdent", usuario.getIdentidad().getTipoIdentidad());
                sqlCommand.Parameters.AddWithValue("@Nombre", usuario.getIdentidad().getNombre());
                sqlCommand.Parameters.AddWithValue("@Apellido", usuario.getIdentidad().getApellido());
                sqlCommand.Parameters.AddWithValue("@TipoDoc", usuario.getIdentidad().getTipoDocumento());
                sqlCommand.Parameters.AddWithValue("@NroDoc", usuario.getIdentidad().getNumeroDocumento());
                sqlCommand.Parameters.AddWithValue("@Mail", usuario.getIdentidad().getMail());
                sqlCommand.Parameters.AddWithValue("@FecNac", usuario.getIdentidad().getFechaNacimiento());
                sqlCommand.Parameters.AddWithValue("@Nacion", usuario.getIdentidad().getNacionalidad());
                sqlCommand.Parameters.AddWithValue("@Tel", usuario.getIdentidad().getTelefono());
                sqlCommand.Parameters.AddWithValue("@idIdentidad", usuario.getIdentidad().getIdIdentidad());

                //PARAMETERS DEL USUARIO
                sqlCommand.Parameters.AddWithValue("@Username", usuario.getUsername());
                sqlCommand.Parameters.AddWithValue("@Activo", usuario.getActivo());
                sqlCommand.Parameters.AddWithValue("@Password", usuario.getPassword());
                
                //SI CAMBIO EL ESTADO DEL USUARIO
                //RESETEO LA CANTIDAD DE INTENTOS FALLIDOS              
                Boolean usuarioActivoEnBase = this.getById(usuario.getIdUsuario()).getActivo();
                Boolean usuarioActivoEnModelo = usuario.getActivo();

                if (!usuarioActivoEnBase && usuarioActivoEnModelo)
                {
                    sqlCommand.Parameters.AddWithValue("@IntentosFallidosLogin", 0);
                } 
                else 
                {
                    sqlCommand.Parameters.AddWithValue("@IntentosFallidosLogin", usuario.getIntentosFallidosLogin());
                }
                
                sqlCommand.Parameters.AddWithValue("@idUsuario", usuario.getIdUsuario());

                //HABRÍA QUE ANALIZAR PROS Y CONTRAS DE ACTUALIZAR/CREAR UN USUARIO TODO EN LA MISMA CONSULTA COMO EN ESTE METODO
                //O ACTUALIZARLO/CREARLO POR SEPARADO CON LOS METODOS DEL REPOSITORIO (USANDO LOS METODOS DEL REPO HAY QUE DEFINIR COMO SE MANEJA EL ROLLBACK SI UN UPDATE O CREATE FALLA)
                StringBuilder sqlBuilder = new StringBuilder();
                sqlBuilder.Append(@"
                    BEGIN TRY
                    BEGIN TRANSACTION

                    UPDATE LOS_BORBOTONES.Direccion
                    SET Pais = @Pais, Ciudad = @Ciudad, Calle = @Calle, NumeroCalle = @NumeroCalle, Piso = @Piso, Depto = @Departamento
                    WHERE idDireccion = @idDireccion;

                    UPDATE LOS_BORBOTONES.Identidad
                    SET TipoIdentidad = @TipoIdent, Nombre = @Nombre, Apellido = @Apellido, TipoDocumento = @TipoDoc, NumeroDocumento = @NroDoc, Mail = @Mail, FechaNacimiento = @FecNac, Nacionalidad = @Nacion, Telefono = @Tel
                    WHERE idIdentidad = @idIdentidad;

                    UPDATE LOS_BORBOTONES.Usuario
                    SET Username = @Username, Password = @Password, IntentosFallidosLogin = @IntentosFallidosLogin, Activo = @Activo, idIdentidad = @idIdentidad
                    WHERE idUsuario = @idUsuario;
                ");

                //TENGO QUE BORRAR TODAS LAS RELACIONES QUE TENGO CON LOS ROLES
                sqlBuilder.Append("DELETE FROM LOS_BORBOTONES.Rol_X_Usuario WHERE idUsuario = @idUsuario;");

                //TENGO QUE BORRAR TODAS LAS RELACIONES QUE TENGO CON LOS HOTELES
                sqlBuilder.Append("DELETE FROM LOS_BORBOTONES.Hotel_X_Usuario WHERE idUsuario = @idUsuario;");  

                //AGREGO DINAMICAMENTE LOS ROLES A LA CONSULTA
                int i = 1;
                foreach (Rol r in usuario.getRoles())
                {
                    String paramName = "@idRol" + i.ToString();
                    sqlBuilder.AppendFormat("INSERT INTO LOS_BORBOTONES.Rol_X_Usuario(idRol,idUsuario) VALUES ({0}, @idUsuario)", paramName);
                    sqlCommand.Parameters.AddWithValue(paramName, r.getIdRol());
                    i++;
                }
                //AGREGO DINAMICAMENTE LOS HOTELES A LA CONSULTA
                int k = 1;
                foreach (Hotel h in usuario.getHoteles())
                {
                    String paramName = "@idHotel" + k.ToString();
                    sqlBuilder.AppendFormat("INSERT INTO LOS_BORBOTONES.Hotel_X_Usuario(idHotel,idUsuario) VALUES ({0}, @idUsuario)", paramName);
                    sqlCommand.Parameters.AddWithValue(paramName, h.getIdHotel());
                    k++;
                }

                sqlBuilder.Append(@"
                    COMMIT
                    END TRY

                    BEGIN CATCH
                    ROLLBACK
                    END CATCH
                ");

                sqlCommand.CommandText = sqlBuilder.ToString();
                sqlConnection.Open();
                reader = sqlCommand.ExecuteReader();

                sqlConnection.Close();
            }
            else
            {
                throw new NoExisteIDException("No existe el usuario que intenta actualizar");
            }
        }

        override public void delete(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        override public void bajaLogica(Usuario usuario)
        {
            usuario.setActivo(false);
            this.update(usuario);
        }

        override public Boolean exists(Usuario usuario)
        {
            int idUsuario = 0;
            String username = "";

            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            sqlCommand.Parameters.AddWithValue("@idUsuario", usuario.getIdUsuario());
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT idUsuario FROM LOS_BORBOTONES.Usuario WHERE idUsuario = @idUsuario";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                idUsuario = reader.GetInt32(reader.GetOrdinal("idUsuario"));
            }

            sqlConnection.Close();

            sqlCommand.Parameters.AddWithValue("@Username", usuario.getUsername());
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = @"
                SELECT usuario.username
                FROM LOS_BORBOTONES.Usuario usuario
                WHERE usuario.username = @Username
            ";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                username = reader.GetString(reader.GetOrdinal("username"));
            }

            sqlConnection.Close();

            //Devuelve verdadero si el ID coincide o si el username coincide
            return idUsuario != 0 || usuario.getUsername().ToUpper().Equals(username.ToUpper());
        }

        public Usuario getByUsername(String username)
        {
            int idUsuario = 0;

            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            sqlCommand.Parameters.AddWithValue("@Username", username);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;

            sqlCommand.CommandText = "SELECT idUsuario FROM LOS_BORBOTONES.Usuario WHERE username = @Username";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                idUsuario = reader.GetInt32(reader.GetOrdinal("idUsuario"));
            }

            sqlConnection.Close();

            //Si no encuentro elemento con ese username tiro una excepción
            if (idUsuario.Equals(0)) throw new NoExisteNombreException("No existe usuario con el Username asociado");

            return getById(idUsuario);
        }
        //getByQuery que va a filtrar por lo que desee buscar el administrador en el listado
        // despues agregar el nombre y el apellido de la identidad

        public List<Usuario> getByQuery(String username, KeyValuePair<String, Boolean> estado, Hotel hotel, Rol rol)
        {
            List<Usuario> usuarios = new List<Usuario>();
            String query = "SELECT u.idUsuario FROM LOS_BORBOTONES.Usuario u";

            //Consulta SIN FILTRO
            if (username.Equals("") && estado.Key == null && hotel == null && rol == null)
            {
                usuarios = this.getAll();
            }
            else
            {
                //Consulta CON FILTROS
                //PREPARO TODO PARA HACER LA CONSULTA
                String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                SqlCommand sqlCommand = new SqlCommand();
                SqlDataReader reader;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = sqlConnection;

                //Booleano que uso para armar bien la consulta
                Boolean primerCriterioWhere = true;

                //Consulta por Hotel
                //Va primero porque el JOIN va antes que el WHERE
                if (hotel != null)
                {
                    sqlCommand.Parameters.AddWithValue("@Nombre", hotel.getNombre());
                    query = query + " INNER JOIN LOS_BORBOTONES.Hotel_X_Usuario hxu ON u.idUsuario = hxu.idUsuario INNER JOIN LOS_BORBOTONES.Hotel h ON h.idHotel = hxu.idHotel WHERE h.Nombre = @Nombre";
                    primerCriterioWhere = false;
                }
                //Consulta por Rol
                //Va primero porque el JOIN va antes que el WHERE
                if (rol != null)
                {
                    sqlCommand.Parameters.AddWithValue("@nombreRol", rol.getNombre());
                    query = query + " INNER JOIN LOS_BORBOTONES.Rol_X_Usuario rxu ON u.idUsuario = rxu.idUsuario INNER JOIN LOS_BORBOTONES.Rol r ON r.idRol = rxu.idRol WHERE r.Nombre = @nombreRol";
                    primerCriterioWhere = false;
                }

                //AGREGO FILTRO NOMBRE
                if (!username.Equals(""))
                {
                    if (primerCriterioWhere)
                    {
                        query = query + " WHERE u.Username LIKE @Nombre";
                        primerCriterioWhere = false;
                    }
                    else
                    {
                        query = query + " AND u.Username LIKE @Nombre";
                    }
                    sqlCommand.Parameters.AddWithValue("@Nombre", "%" + username + "%");
                }

                //AGREGO FILTRO ESTADO
                if (estado.Key != null)
                {
                    if (primerCriterioWhere)
                    {
                        query = query + " WHERE u.Activo = @Estado";
                        primerCriterioWhere = false;
                    }
                    else
                    {
                        query = query + " AND u.Activo = @Estado";
                    }
                    sqlCommand.Parameters.AddWithValue("@Estado", Convert.ToInt32(estado.Value));
                }

                //HAGO LA CONSULTA
                sqlCommand.CommandText = query;
                sqlConnection.Open();
                reader = sqlCommand.ExecuteReader();

                //ARMAR USUARIOS
                while (reader.Read())
                {
                    usuarios.Add(this.getById(reader.GetInt32(reader.GetOrdinal("idUsuario"))));
                }

                sqlConnection.Close();
            }

            return usuarios;
        }

        public string EncriptarSHA256(string input)
        {
            SHA256CryptoServiceProvider provider = new SHA256CryptoServiceProvider();

            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashedBytes = provider.ComputeHash(inputBytes);

            StringBuilder output = new StringBuilder();

            for (int i = 0; i < hashedBytes.Length; i++)
                output.Append(hashedBytes[i].ToString("x2").ToLower());

            return output.ToString();
        }

        public Usuario AutenticarUsuario(String username, String password)
        {
            string passwordEncriptada = this.EncriptarSHA256(password);
            Usuario usuario = null;

            //VALIDO SI EXISTE PRIMERO EL USUARIO
            if (this.getAll().Exists(usr => usr.getUsername().ToUpper().Equals(username.ToUpper())))
            {
                usuario = this.getByUsername(username);

                //SI LA PASSWORD ESTA BIEN Y EL USUARIO NO ESTA BLOQUEADO
                String passwordUser = usuario.getPassword();
                int intentosFallidos = usuario.getIntentosFallidosLogin();
                Boolean usuarioActivo = usuario.getActivo();

                if (passwordUser.Equals(passwordEncriptada) && intentosFallidos < 3 && usuarioActivo)
                {
                    usuario.resetearIntentosFallidosLogin();
                    this.update(usuario);
                    return usuario;
                }
                else
                {
                    //SI HUBO UN ERROR DE CREDENCIALES Y EL USUARIO TODAVIA NO ESTA BLOQUEADO
                    if (!passwordUser.Equals(passwordEncriptada) && intentosFallidos < 3 && usuarioActivo)
                    {
                        usuario.incrementarIntentosFallidosLogin();
                        
                        if (usuario.getIntentosFallidosLogin() >= 3)
                        {
                            usuario.setActivo(false);
                        }
                        
                        this.update(usuario);

                        throw new ErrorDeAutenticacionException("Las credenciales son incorrectas");
                    }
                    //LUEGO SI EL USUARIO ESTA BLOQUEADO
                    else if (usuario.getIntentosFallidosLogin() >= 3 || !usuario.getActivo())
                    {
                        usuario.incrementarIntentosFallidosLogin();
                        this.update(usuario);

                        throw new UsuarioBloqueadoException("El usuario esta bloqueado o deshabilitado");
                    }
                }
            }
            else
            {
                //EL USUARIO NO EXISTE
                throw new ErrorDeAutenticacionException("Las credenciales son incorrectas");
            }

            return usuario;
        }

        public void changePassword(Usuario usuario){

            if (this.exists(usuario))
            {
                String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                SqlCommand sqlCommand = new SqlCommand();
                SqlDataReader reader;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = sqlConnection;
                string passwordEncriptada = this.EncriptarSHA256(usuario.getPassword());

                sqlCommand.Parameters.AddWithValue("@Password", passwordEncriptada);
                sqlCommand.Parameters.AddWithValue("@idUsuario", usuario.getIdUsuario());

                sqlCommand.CommandText = "UPDATE LOS_BORBOTONES.Usuario SET Password = @Password WHERE idUsuario = @idUsuario;  ";
                sqlConnection.Open();
                reader = sqlCommand.ExecuteReader();

                sqlConnection.Close();
            }
            else
            {
                throw new NoExisteIDException("No existe el usuario que intenta actualizar");
            }
        }
    }



}
