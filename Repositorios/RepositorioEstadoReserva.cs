using FrbaHotel.Commons;
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
    public class RepositorioEstadoReserva : Repositorio<EstadoReserva>
    {
        public EstadoReserva getByIdReserva(int idReserva)
        {
            RepositorioUsuario repoUsuario = new RepositorioUsuario();
            RepositorioReserva repoReserva = new RepositorioReserva();

            EstadoReserva estadoReserva= null;
            
            //Configuraciones de la consulta
            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            //Primera Consulta
            sqlCommand.Parameters.AddWithValue("@idReserva", idReserva);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = @"
                SELECT *
                FROM LOS_BORBOTONES.EstadoReserva
                WHERE idReserva = @idReserva;";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            if (reader.Read())
            {
                int idEstadoReserva = reader.GetInt32(reader.GetOrdinal("idEstado"));
                String tipoEstado = reader.GetString(reader.GetOrdinal("TipoEstado"));
                DateTime fecha = reader.SafeGetDateTime(reader.GetOrdinal("Fecha"));
                String descripcion = reader.GetString(reader.GetOrdinal("Descripcion"));
                Usuario usuario = repoUsuario.getById(reader.GetInt32(reader.GetOrdinal("IdUsuario")));
                Reserva reserva = repoReserva.getById(reader.GetInt32(reader.GetOrdinal("IdReserva")));
                estadoReserva= new EstadoReserva(idEstadoReserva, usuario, reserva, tipoEstado, fecha, descripcion);
            }
            
            sqlConnection.Close();
            
            return estadoReserva;
        }

        override public EstadoReserva getById(int idEstadoReserva)
        {
            
            RepositorioUsuario repoUsuario = new RepositorioUsuario();
            RepositorioReserva repoReserva = new RepositorioReserva();

            //Elementos del EstadoReserva a devolver
            EstadoReserva estadoReserva=null;
            Usuario usuario = null;
            Reserva reserva = null;
            String tipoEstado = "";
            DateTime fecha = Utils.getSystemDatetimeNow();
            String descripcion = "";
            
            
            //Configuraciones de la consulta
            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            //Primera Consulta
            sqlCommand.Parameters.AddWithValue("@idEstadoReserva", idEstadoReserva);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT * FROM LOS_BORBOTONES.EstadoReserva WHERE idEstado = @idEstadoReserva";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                usuario = repoUsuario.getById(reader.GetOrdinal("IdUsuario"));
                //reserva = repoReserva.getById(reader.GetOrdinal("IdReserva"));
                //fecha = reader.GetDateTime(reader.GetOrdinal("Fecha"));
                tipoEstado = reader.GetString(reader.GetOrdinal("TipoEstado"));
                descripcion = reader.GetString(reader.GetOrdinal("Descripcion"));
                estadoReserva = new EstadoReserva(idEstadoReserva, usuario, reserva, tipoEstado, fecha, descripcion);

            }

            //Cierro Primera Consulta
            sqlConnection.Close();

            //Si no encuentro elemento con ese ID tiro una excepción
            //if (usuario.Equals(null)) throw new NoExisteIDException("No existe estadoReserva con el ID asociado");

            //Armo el estadoReserva completo
            return estadoReserva;
        }

        override public List<EstadoReserva> getAll()
        {
            List<EstadoReserva> estadosReservas = new List<EstadoReserva>();

            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;

            sqlCommand.CommandText = "SELECT idEstado FROM LOS_BORBOTONES.EstadoReserva";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                estadosReservas.Add(this.getById(reader.GetInt32(reader.GetOrdinal("idEstado"))));
            }

            sqlConnection.Close();

            return estadosReservas;
        }

        override public int create(EstadoReserva estadoReserva)
        {
            if (this.exists(estadoReserva))
            {
                //Error
            }
            else
            {
                //Creo un nuevo registro
            }

            throw new System.NotImplementedException();
        }

        //poner el check in post create de la estadia
        //poner el check out con check out
        override public void update(EstadoReserva estadoReserva)
        {
            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.AddWithValue("@Desc", estadoReserva.getDescripcion());
            sqlCommand.Parameters.AddWithValue("@IdReserva", estadoReserva.getReserva().getIdReserva());
            sqlCommand.Parameters.AddWithValue("@TipoEstado", estadoReserva.getTipoEstado());
            sqlCommand.Parameters.AddWithValue("@IdUser", estadoReserva.getUsuario().getIdUsuario());
            sqlCommand.Parameters.AddWithValue("@Date", estadoReserva.getFecha());

            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append(@"
                    BEGIN TRY
                    BEGIN TRANSACTION
                    UPDATE LOS_BORBOTONES.EstadoReserva
                    SET TipoEstado = @TipoEstado, Descripcion= @Desc,idUsuario = @IdUser,Fecha=@Date
                    WHERE idReserva = @IdReserva;
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

            sqlConnection.Close();
        }



        public void cancelarReservasPorNoShow(int idReserva)
        {

            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            RepositorioUsuario repoUsuario = new RepositorioUsuario();
            Usuario usuario = repoUsuario.getByUsername("guest");

            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;

            sqlCommand.Parameters.AddWithValue("@idReserva", idReserva);
            sqlCommand.Parameters.AddWithValue("@idUsuario", usuario.getIdUsuario());


            sqlCommand.CommandText = @"UPDATE LOS_BORBOTONES.EstadoReserva SET TipoEstado='RCNS',Fecha=LOS_BORBOTONES.fn_getDate(),Descripcion='Reserva Cancelada No Show',idUsuario=@idUsuario
                                        WHERE idReserva = @idReserva;";
            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();

            reader.Read();

            sqlConnection.Close();
            
        }
        override public void delete(EstadoReserva estadoReserva)
        {
            if (this.exists(estadoReserva))
            {
                //Borro el registro
            }
            else
            {
                //Error
            }
        }

        override public void bajaLogica(EstadoReserva estadoReserva)
        {
            throw new NotImplementedException();
        }

        override public Boolean exists(EstadoReserva estadoReserva)
        {
            int idEstado = 0;
            int idReserva = 0;

            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            sqlCommand.Parameters.AddWithValue("@idEstado", estadoReserva.getIdEstadoReserva());
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT idEstado FROM LOS_BORBOTONES.EstadoReserva WHERE idEstado = @idEstado";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                idEstado = reader.GetInt32(reader.GetOrdinal("idEstado"));
            }

            sqlConnection.Close();
            Reserva reserva = null;
            reserva = estadoReserva.getReserva();

            //valido por el idReserva que tiene en la base
            sqlCommand.Parameters.AddWithValue("@idReserva", reserva.getIdReserva());
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT idReserva FROM LOS_BORBOTONES.EstadoReserva WHERE idReserva = @idReserva";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                idReserva = reader.GetInt32(reader.GetOrdinal("idReserva"));
            }

            sqlConnection.Close();

            //Devuelve verdadero si el ID coincide o si el IdReserva coincide
            return idEstado != 0 || reserva.getIdReserva().Equals(idReserva);
        }
        //luego hacer algun getBy que vea especial y el getByQuery
        public void rechazarReserva(int codReserva,int idUser,DateTime date)
        {
                String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                SqlCommand sqlCommand = new SqlCommand();
                SqlDataReader reader;

                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Parameters.AddWithValue("@CodReserva", codReserva);
                sqlCommand.Parameters.AddWithValue("@IdUser", idUser);
                sqlCommand.Parameters.AddWithValue("@Date", date);
            
                StringBuilder sqlBuilder = new StringBuilder();
                sqlBuilder.Append(@"
                    BEGIN TRY
                    BEGIN TRANSACTION

                    UPDATE LOS_BORBOTONES.EstadoReserva
                    SET TipoEstado = 'RCR', Descripcion= 'Reserva Cancelada por Recepcion',idUsuario = @IdUser,Fecha=@Date
                    WHERE idReserva = (SELECT idReserva FROM LOS_BORBOTONES.Reserva where CodigoReserva= @CodReserva);
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

                sqlConnection.Close();
        }


        public Usuario getUsuarioByIdReservaAndTipoEstado(int idReserva, String tipoEstado) {
            
            Usuario usuario = null;
           
            //Configuraciones de la consulta
            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            //Primera Consulta
            sqlCommand.Parameters.AddWithValue("@idReserva", idReserva);
            sqlCommand.Parameters.AddWithValue("@tipoEstado", tipoEstado);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT idUsuario FROM LOS_BORBOTONES.EstadoReserva WHERE idReserva =@idReserva  AND TipoEstado=tipoEstado;";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            if (reader.Read())
            {
               int idUsuario = reader.GetInt32(reader.GetOrdinal("idEstado"));
                RepositorioUsuario repoUsuario= new RepositorioUsuario();

                usuario=repoUsuario.getById(idUsuario);
            }
            sqlConnection.Close();

            return usuario;
        
        }

        public EstadoReserva getByIdEstadia(int idEstadia)
        {
            EstadoReserva estadoReserva = null;
            int idEstadoReserva = 0;
            //Configuraciones de la consulta
            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            //Primera Consulta
            sqlCommand.Parameters.AddWithValue("@idEstadia", idEstadia);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT * FROM LOS_BORBOTONES.EstadoReserva WHERE idReserva =" +
                " (SELECT TOP 1 idReserva FROM LOS_BORBOTONES.Reserva WHERE idEstadia = @idEstadia);";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
               idEstadoReserva = reader.GetInt32(reader.GetOrdinal("idEstado"));
            }
            estadoReserva = this.getById(idEstadoReserva);

            sqlConnection.Close();

            return estadoReserva;
        }

    }
}

