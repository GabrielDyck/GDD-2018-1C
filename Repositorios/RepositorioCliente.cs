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

namespace FrbaHotel.Repositorios
{
    public class RepositorioCliente : Repositorio<Cliente>
    {
        override public Cliente getById(int idCliente)
        {
            //Elementos del Cliente a devolver
            Cliente cliente;
            int idIdentidad = 0;
            Identidad identidad = null;
            RepositorioIdentidad repoIdentidad = new RepositorioIdentidad();
            Boolean activo = false;
            List<Reserva> reservas = new List<Reserva>();
            Boolean inconsistente = false;

            //Configuraciones de la consulta
            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            //Primera Consulta
            sqlCommand.Parameters.AddWithValue("@idCliente", idCliente);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT * FROM LOS_BORBOTONES.Cliente WHERE idCliente = @idCliente";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                idIdentidad = reader.GetInt32(reader.GetOrdinal("IdIdentidad"));
                identidad = repoIdentidad.getById(idIdentidad);
                activo = reader.GetBoolean(reader.GetOrdinal("Activo"));
                inconsistente = reader.GetBoolean(reader.GetOrdinal("Inconsistente"));
            }

            //Cierro Primera Consulta
            sqlConnection.Close();

            //Si no encuentro elemento con ese ID tiro una excepción
            if (identidad.Equals(null)) throw new NoExisteIDException("No existe cliente con el ID asociado");

            //Segunda Consulta
            sqlCommand.CommandText = @"
                
                SELECT r.idReserva
                FROM LOS_BORBOTONES.Reserva_X_Habitacion_X_Cliente rhc 
                INNER JOIN LOS_BORBOTONES.Reserva r ON r.idReserva = rhc.idReserva
                WHERE rhc.idCliente = @idCliente

            ";

            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();

            //Colecto las reservas
            while (reader.Read())
            {

                int idReserva = reader.GetInt32(reader.GetOrdinal("idReserva"));
                //RepositorioReserva repoReserva = new RepositorioReserva();
                //reservas.Add(repoReserva.getById(idReserva));    
                

            }

            sqlConnection.Close();

            //Armo el cliente completo
            cliente = new Cliente(idCliente, identidad, activo, reservas, inconsistente);

            return cliente;
        }

        //OPTIMIZO EL GETALL
        //NO TRAE LAS RESERVAS, POR EL MOMENTO
        //ASUMO QUE TENGO UNA SOLA DIRECCION SI TENGO MAS DE UNA ESTO ANDARIA MAL
        override public List<Cliente> getAll()
        {
            List<Cliente> clientes = new List<Cliente>();     
            
            //Configuraciones de la consulta
            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            //Primera Consulta
            //ASUMO QUE TENGO UNA SOLA DIRECCION SI TENGO MAS DE UNA ESTO ANDARIA MAL
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = @" SELECT * 
                                        FROM LOS_BORBOTONES.Cliente c
                                        INNER JOIN LOS_BORBOTONES.Identidad i
                                        ON i.idIdentidad = c.idIdentidad
                                        INNER JOIN LOS_BORBOTONES.Direccion d
                                        ON d.idIdentidad = i.idIdentidad";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                //ASUMO QUE TENGO UNA SOLA DIRECCION SI TENGO MAS DE UNA ESTO ANDARIA MAL
                Direccion direccion = new Direccion(reader.GetInt32(reader.GetOrdinal("idDireccion")),
                                                    reader.GetString(reader.GetOrdinal("Pais")), 
                                                    reader.SafeGetString(reader.GetOrdinal("Ciudad")), 
                                                    reader.GetString(reader.GetOrdinal("calle")), 
                                                    reader.GetInt32(reader.GetOrdinal("NumeroCalle")), 
                                                    reader.SafeGetInt32(reader.GetOrdinal("Piso")), 
                                                    reader.SafeGetString(reader.GetOrdinal("Depto")));   
             
                //ASUMO QUE TENGO UNA SOLA DIRECCION SI TENGO MAS DE UNA ESTO ANDARIA MAL
                List<Direccion> direcciones = new List<Direccion>();
                direcciones.Add(direccion);

                Identidad identidad = new Identidad(reader.GetInt32(reader.GetOrdinal("idIdentidad")), 
                                                    reader.GetString(reader.GetOrdinal("TipoIdentidad")), 
                                                    reader.GetString(reader.GetOrdinal("Nombre")), 
                                                    reader.SafeGetString(reader.GetOrdinal("Apellido")), 
                                                    reader.GetString(reader.GetOrdinal("TipoDocumento")), 
                                                    reader.GetString(reader.GetOrdinal("NumeroDocumento")), 
                                                    reader.GetString(reader.GetOrdinal("Mail")), 
                                                    reader.SafeGetDateTime(reader.GetOrdinal("FechaNacimiento")), 
                                                    reader.SafeGetString(reader.GetOrdinal("Nacionalidad")), 
                                                    reader.GetString(reader.GetOrdinal("Telefono")), 
                                                    direcciones);

                clientes.Add(new Cliente(reader.GetInt32(reader.GetOrdinal("idCliente")),
                                        identidad,
                                        reader.GetBoolean(reader.GetOrdinal("Activo")),
                                        new List<Reserva>(),
                                        reader.GetBoolean(reader.GetOrdinal("Inconsistente"))));
            }

            sqlConnection.Close();
            return clientes;
        }

        override public int create(Cliente cliente)
        {
            int idCliente = 0;
            RepositorioIdentidad repoIdentidad = new RepositorioIdentidad();
            if (this.exists(cliente) || repoIdentidad.exists(cliente.getIdentidad()))
            {
                throw new ElementoYaExisteException("Ya existe un cliente o un usuario con el mismo documento o el mismo mail.");
            }
            else
            {
                //llamo a crear la identidad y traigo el IdIdentidad
                int idIdentidad = repoIdentidad.create(cliente.getIdentidad());

                //llamo a crear la direccion y traigo el IdDireccion, le seteo el idIdentidad que lo necesita
                cliente.getIdentidad().getDireccion().setIdIdentidad(idIdentidad);
                RepositorioDireccion repoDireccion = new RepositorioDireccion();
                int idDireccion = repoDireccion.create(cliente.getIdentidad().getDireccion());

                //ahora ya tengo todo para crear el cliente

                String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                SqlCommand sqlCommand = new SqlCommand();
                SqlDataReader reader;

                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Parameters.AddWithValue("@Activo", cliente.getActivo());
                sqlCommand.Parameters.AddWithValue("@idIdentidad", idIdentidad);

                StringBuilder sqlBuilder = new StringBuilder();
                sqlBuilder.Append(@"
                    BEGIN TRY
                    BEGIN TRANSACTION

                    INSERT INTO LOS_BORBOTONES.Cliente(Activo,idIdentidad)
                    OUTPUT INSERTED.idCliente
                    VALUES(@Activo,@idIdentidad);

                    DECLARE @idCliente int;
                    SET @idCliente = SCOPE_IDENTITY();
                ");

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
                    idCliente = reader.GetInt32(reader.GetOrdinal("idCliente"));
                }

                sqlConnection.Close();
            }

            return idCliente;

        }

        public void limpioInconsistencia(Cliente clienteActualizado)
        {
            //PRIMERO LIMPIO LAS IDENTIDADES DUPLICADAS
            RepositorioIdentidad repoIdentidad = new RepositorioIdentidad();

            List<Identidad> identidadesDuplicadas = new List<Identidad>();

            //Configuraciones de la consulta
            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            sqlCommand.Parameters.AddWithValue("@idIdentidad", clienteActualizado.getIdentidad().getIdIdentidad());
            sqlCommand.Parameters.AddWithValue("@Mail", clienteActualizado.getIdentidad().getMail());
            sqlCommand.Parameters.AddWithValue("@Num", clienteActualizado.getIdentidad().getNumeroDocumento());
            sqlCommand.Parameters.AddWithValue("@Tipo", clienteActualizado.getIdentidad().getTipoDocumento());

            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = @"
                SELECT identidad.idIdentidad 
                FROM LOS_BORBOTONES.Identidad identidad
                WHERE identidad.idIdentidad <> @idIdentidad
                  AND (identidad.TipoDocumento = @Tipo AND identidad.NumeroDocumento = @Num)

                UNION

                SELECT identidad.idIdentidad 
                FROM LOS_BORBOTONES.Identidad identidad
                WHERE identidad.idIdentidad <> @idIdentidad
                  AND (identidad.Mail = @Mail)
            ";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                identidadesDuplicadas.Add(repoIdentidad.getById(reader.GetInt32(reader.GetOrdinal("idIdentidad"))));
            }

            //Cierro Primera Consulta
            sqlConnection.Close();

            identidadesDuplicadas.ForEach(identDup => repoIdentidad.limpiarDuplicadoMarcarInconsistente(identDup));
        }

        public void updateAutoritativo(Cliente cliente)
        {
            RepositorioIdentidad repoIdentidad = new RepositorioIdentidad();

            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                SqlCommand sqlCommand = new SqlCommand();
                SqlDataReader reader;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = sqlConnection;

                //PARAMETERS DE LA DIRECCION
                sqlCommand.Parameters.AddWithValue("@Pais", cliente.getIdentidad().getDireccion().getPais());
                sqlCommand.Parameters.AddWithValue("@Ciudad", cliente.getIdentidad().getDireccion().getCiudad());
                sqlCommand.Parameters.AddWithValue("@Calle", cliente.getIdentidad().getDireccion().getCalle());
                sqlCommand.Parameters.AddWithValue("@NumeroCalle", cliente.getIdentidad().getDireccion().getNumeroCalle());
                sqlCommand.Parameters.AddWithValue("@Piso", cliente.getIdentidad().getDireccion().getPiso());
                sqlCommand.Parameters.AddWithValue("@Departamento", cliente.getIdentidad().getDireccion().getDepartamento());
                sqlCommand.Parameters.AddWithValue("@idDireccion", cliente.getIdentidad().getDireccion().getIdDireccion());

                //PARAMETERS DE LA IDENTIDAD
                sqlCommand.Parameters.AddWithValue("@TipoIdent", cliente.getIdentidad().getTipoIdentidad());
                sqlCommand.Parameters.AddWithValue("@Nombre", cliente.getIdentidad().getNombre());
                sqlCommand.Parameters.AddWithValue("@Apellido", cliente.getIdentidad().getApellido());
                sqlCommand.Parameters.AddWithValue("@TipoDoc", cliente.getIdentidad().getTipoDocumento());
                sqlCommand.Parameters.AddWithValue("@NroDoc", cliente.getIdentidad().getNumeroDocumento());
                sqlCommand.Parameters.AddWithValue("@Mail", cliente.getIdentidad().getMail());
                sqlCommand.Parameters.AddWithValue("@FecNac", cliente.getIdentidad().getFechaNacimiento());
                sqlCommand.Parameters.AddWithValue("@Nacion", cliente.getIdentidad().getNacionalidad());
                sqlCommand.Parameters.AddWithValue("@Tel", cliente.getIdentidad().getTelefono());
                sqlCommand.Parameters.AddWithValue("@idIdentidad", cliente.getIdentidad().getIdIdentidad());

                //PARAMETERS DEL CLIENTE
                /////////////////////////
                //POR EL MOMENTO NO CONSIDERAMOS LAS RESERVAS EN EL CLIENTE EN EL UPDATE
                /////////////////////////
                sqlCommand.Parameters.AddWithValue("@Activo", cliente.getActivo());
                sqlCommand.Parameters.AddWithValue("@idCliente", cliente.getIdCliente());

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

                    UPDATE LOS_BORBOTONES.Cliente
                    SET Activo = @Activo, idIdentidad = @idIdentidad, Inconsistente = 0
                    WHERE idCliente = @idCliente;

                    COMMIT
                    END TRY

                    BEGIN CATCH
                    ROLLBACK
                    END CATCH
                ");

                //LA LOGICA EN PONER 'INCONSISTENTE = 0' EN TODOS LOS UPDATE
                //ES QUE POR GUI YO AVISO QUE EL CLIENTE ESTA INCONSISTENTE
                //SI EL USUARIO DEL SISTEMA DECIDE CONTINUAR EDITANDOLO DEBE GARANTIZAR QUE 
                //VERIFICA LA IDENTIDAD DEL CLIENTE MEDIANTE DOCUMENTO Y VALIDA SU MAIL

                sqlCommand.CommandText = sqlBuilder.ToString();
                sqlConnection.Open();
                reader = sqlCommand.ExecuteReader();

                sqlConnection.Close();
        }

        override public void update(Cliente cliente)
        {
            RepositorioIdentidad repoIdentidad = new RepositorioIdentidad();

            //AGREGO VALIDACIONES AL UPDATE
            if (repoIdentidad.yaExisteOtraIdentidadMismoMailOTipoYNumDoc(cliente.getIdentidad()))
            {
                throw new ElementoYaExisteException("Ya existe un cliente o un usuario con el mismo mail o tipo y numero de documento.");
            }

            if (this.exists(cliente))
            {
                String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                SqlCommand sqlCommand = new SqlCommand();
                SqlDataReader reader;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = sqlConnection;

                //PARAMETERS DE LA DIRECCION
                sqlCommand.Parameters.AddWithValue("@Pais", cliente.getIdentidad().getDireccion().getPais());
                sqlCommand.Parameters.AddWithValue("@Ciudad", cliente.getIdentidad().getDireccion().getCiudad());
                sqlCommand.Parameters.AddWithValue("@Calle", cliente.getIdentidad().getDireccion().getCalle());
                sqlCommand.Parameters.AddWithValue("@NumeroCalle", cliente.getIdentidad().getDireccion().getNumeroCalle());
                sqlCommand.Parameters.AddWithValue("@Piso", cliente.getIdentidad().getDireccion().getPiso());
                sqlCommand.Parameters.AddWithValue("@Departamento", cliente.getIdentidad().getDireccion().getDepartamento());
                sqlCommand.Parameters.AddWithValue("@idDireccion", cliente.getIdentidad().getDireccion().getIdDireccion());

                //PARAMETERS DE LA IDENTIDAD
                sqlCommand.Parameters.AddWithValue("@TipoIdent", cliente.getIdentidad().getTipoIdentidad());
                sqlCommand.Parameters.AddWithValue("@Nombre", cliente.getIdentidad().getNombre());
                sqlCommand.Parameters.AddWithValue("@Apellido", cliente.getIdentidad().getApellido());
                sqlCommand.Parameters.AddWithValue("@TipoDoc", cliente.getIdentidad().getTipoDocumento());
                sqlCommand.Parameters.AddWithValue("@NroDoc", cliente.getIdentidad().getNumeroDocumento());
                sqlCommand.Parameters.AddWithValue("@Mail", cliente.getIdentidad().getMail());
                sqlCommand.Parameters.AddWithValue("@FecNac", cliente.getIdentidad().getFechaNacimiento());
                sqlCommand.Parameters.AddWithValue("@Nacion", cliente.getIdentidad().getNacionalidad());
                sqlCommand.Parameters.AddWithValue("@Tel", cliente.getIdentidad().getTelefono());
                sqlCommand.Parameters.AddWithValue("@idIdentidad", cliente.getIdentidad().getIdIdentidad());

                //PARAMETERS DEL CLIENTE
                /////////////////////////
                //POR EL MOMENTO NO CONSIDERAMOS LAS RESERVAS EN EL CLIENTE EN EL UPDATE
                /////////////////////////
                sqlCommand.Parameters.AddWithValue("@Activo", cliente.getActivo());
                sqlCommand.Parameters.AddWithValue("@idCliente", cliente.getIdCliente());

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

                    UPDATE LOS_BORBOTONES.Cliente
                    SET Activo = @Activo, idIdentidad = @idIdentidad, Inconsistente = 0
                    WHERE idCliente = @idCliente;

                    COMMIT
                    END TRY

                    BEGIN CATCH
                    ROLLBACK
                    END CATCH
                ");

                //LA LOGICA EN PONER 'INCONSISTENTE = 0' EN TODOS LOS UPDATE
                //ES QUE POR GUI YO AVISO QUE EL CLIENTE ESTA INCONSISTENTE
                //SI EL USUARIO DEL SISTEMA DECIDE CONTINUAR EDITANDOLO DEBE GARANTIZAR QUE 
                //VERIFICA LA IDENTIDAD DEL CLIENTE MEDIANTE DOCUMENTO Y VALIDA SU MAIL

                sqlCommand.CommandText = sqlBuilder.ToString();
                sqlConnection.Open();
                reader = sqlCommand.ExecuteReader();

                sqlConnection.Close();
            }
            else
            {
                throw new NoExisteIDException("No existe el cliente que intenta actualizar");
            }
        }

        override public void delete(Cliente cliente)
        {
            throw new NotImplementedException();
        }

        override public void bajaLogica(Cliente cliente)
        {
            cliente.setActivo(false);
            this.update(cliente);
        }

        override public Boolean exists(Cliente cliente)
        {
            int idCliente = 0;
            int idIdentidad = 0;

            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            sqlCommand.Parameters.AddWithValue("@idCliente", cliente.getIdCliente());
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT idCliente FROM LOS_BORBOTONES.Cliente WHERE idCliente = @idCliente";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                idCliente = reader.GetInt32(reader.GetOrdinal("idCliente"));
            }

            sqlConnection.Close();

            //valido por el mail y por el dni
            sqlCommand.Parameters.AddWithValue("@tipoDoc", cliente.getIdentidad().getTipoDocumento());
            sqlCommand.Parameters.AddWithValue("@nroDoc", cliente.getIdentidad().getNumeroDocumento());
            //sqlCommand.Parameters.AddWithValue("@Mail", cliente.getIdentidad().getMail());
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = @"
                SELECT idIdentidad
                FROM LOS_BORBOTONES.Identidad
                WHERE (TipoDocumento = @tipoDoc AND NumeroDocumento = @nroDoc AND TipoIdentidad='Cliente')
                --WHERE (TipoDocumento = @tipoDoc AND NumeroDocumento = @nroDoc) 
                --   OR (Mail = @Mail)
            ";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                idIdentidad = reader.GetInt32(reader.GetOrdinal("idIdentidad"));
            }

            sqlConnection.Close();

            //Devuelve verdadero si el ID coincide o si el IdIdentidad es distinto de 0 que es que ya hay con ese mail y doc
            return idCliente != 0 || idIdentidad != 0;
        }

        //filtro por idIdentidad que tambien despues lo uso para traer la Identidad a esta clase
        public Cliente getByIdIdentidad(int idIdentidad)
        {
            int idCliente = 0;

            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            sqlCommand.Parameters.AddWithValue("@IdIdentidad", idIdentidad);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;

            sqlCommand.CommandText = "SELECT idCliente FROM LOS_BORBOTONES.Cliente WHERE idIdentidad = @IdIdentidad";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                idCliente = reader.GetInt32(reader.GetOrdinal("idCliente"));
            }

            sqlConnection.Close();

            //Si no encuentro elemento con ese idIdentidad tiro una excepción
            if (idCliente.Equals(0)) throw new NoExisteIDException("No existe Identidad con el ID asociado");

            return getById(idCliente);
        }

        public List<Cliente> getByQuery(String nombre,String apellido,String tipoDoc,String dni, KeyValuePair<String, Boolean> estado, String mail)
        {
            //ESTO ESTABA FUNCIONANDO BIEN SALVO EL FILTRO DE ESTADO, AL ESTAR ACTIVOS TODOS LOS CLIENTES
            //TERMINA HACIENDO EL GETALLBYID ANTERIOR, QUE ES EL QUE TARDABA DEMASIADO POR LO QUE ESTE METODO LO QUE VA A HACER A PARTIR DE AHORA
            //ES FILTRAR LA LISTA DEL GETALL A NIVEL LOGICO, EL GETALL AHORA NO TARDA TANTO.
            //PODRIA TAMBIEN PONER LA CONSULTA EN ESTE METODO PERO TENDRIA QUE SEGUIR REPITIENDO CODIGO

            List<Cliente> clientes = this.getAll();

                //AGREGO FILTRO NOMBRE
                if (!nombre.Equals(""))
                {
                    clientes.RemoveAll(c => !c.getIdentidad().getNombre().ToUpper().Contains(nombre.ToUpper()));
                }
                //AGREGO FILTRO Apellido
                if (!apellido.Equals(""))
                {
                    clientes.RemoveAll(c => !c.getIdentidad().getApellido().ToUpper().Contains(apellido.ToUpper()));
                }
                //AGREGO FILTRO tipoDoc
                if (!tipoDoc.Equals(""))
                {
                    clientes.RemoveAll(c => !c.getIdentidad().getTipoDocumento().ToUpper().Equals(tipoDoc.ToUpper()));
                }
                //AGREGO FILTRO DNI
                if (!dni.Equals(""))
                {
                    clientes.RemoveAll(c => !c.getIdentidad().getNumeroDocumento().Contains(dni));
                }
                //AGREGO FILTRO ESTADO
                if (estado.Key != null)
                {
                    clientes.RemoveAll(c => !c.getActivo().Equals(estado.Value));
                }
                //AGREGO FILTRO MAIL
                if (!mail.Equals(""))
                {
                    clientes.RemoveAll(c => !c.getIdentidad().getMail().ToUpper().Contains(mail.ToUpper()));
                }

                return clientes;

        }
    }
}

