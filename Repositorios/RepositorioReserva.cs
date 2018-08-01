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
    public class RepositorioReserva : Repositorio<Reserva>
    {
          public List<Reserva> getByIdHotel(int idHotel)
        {
            RepositorioHotel repoHotel = new RepositorioHotel();
            RepositorioRegimen repoRegimen = new RepositorioRegimen();
            RepositorioCliente repoCliente = new RepositorioCliente();
            RepositorioEstadia repoEstadia = new RepositorioEstadia();
            RepositorioEstadoReserva repoEstadoReserva = new RepositorioEstadoReserva();

            List<Reserva> reservas = new List<Reserva>();
            
            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;
            
            sqlCommand.Parameters.AddWithValue("@idHotel", idHotel);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT * FROM LOS_BORBOTONES.Reserva WHERE idHotel = @idHotel";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                int idReserva = reader.GetInt32(reader.GetOrdinal("idReserva"));
                decimal codigoReserva = reader.GetDecimal(reader.GetOrdinal("CodigoReserva"));
                DateTime fechaCreacion = reader.GetDateTime(reader.GetOrdinal("FechaCreacion"));
                DateTime fechaDesde = reader.GetDateTime(reader.GetOrdinal("FechaDesde"));
                DateTime fechaHasta = reader.GetDateTime(reader.GetOrdinal("FechaHasta"));
                decimal diasAlojados = reader.GetDecimal(reader.GetOrdinal("DiasAlojados"));
                Reserva reserva = new Reserva(idReserva, null, null, null, null, codigoReserva, diasAlojados, fechaCreacion, fechaDesde, fechaHasta, null);
                reservas.Add(reserva);
            }

            sqlConnection.Close();

            return reservas;

        }


        public Hotel getHotelByIdReserva(Reserva reserva) {
            Hotel hotel=null;
            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            sqlCommand.Parameters.AddWithValue("@idReserva", reserva.getIdReserva());
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT idHotel FROM LOS_BORBOTONES.Reserva WHERE idReserva = @idReserva";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            if (reader.Read())
            {
                int idHotel = reader.GetInt32(reader.GetOrdinal("idHotel"));
                RepositorioHotel repoHotel = new RepositorioHotel();
                hotel= repoHotel.getById(idHotel);
            }

            sqlConnection.Close();

            return hotel;
        }


        public void cancelarReservasNoShow(Hotel hotel) {
            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.AddWithValue("@idHotel", hotel.getIdHotel());


            sqlCommand.CommandText = "SELECT RES.idReserva FROM LOS_BORBOTONES.Reserva AS RES " + 
                "JOIN LOS_BORBOTONES.EstadoReserva AS ESRE ON RES.idReserva = ESRE.idReserva " +
                "WHERE ESRE.TipoEstado  IN ('RC','RM') " +
                "AND RES.idHotel= @idHotel " +
                "AND RES.FechaDesde < LOS_BORBOTONES.fn_getDate();";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                int idReserva = reader.GetInt32(reader.GetOrdinal("idReserva"));
                RepositorioEstadoReserva repoEstado = new RepositorioEstadoReserva();
                repoEstado.cancelarReservasPorNoShow(idReserva);
            }

            sqlConnection.Close();

        }
        
        public Regimen getRegimenByIdReserva(Reserva reserva)
        {
            Regimen regimen = null;
            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            sqlCommand.Parameters.AddWithValue("@idReserva", reserva.getIdReserva());
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT idRegimen FROM LOS_BORBOTONES.Reserva WHERE idReserva = @idReserva";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            if (reader.Read())
            {
                int idRegimen = reader.GetInt32(reader.GetOrdinal("idRegimen"));
                RepositorioRegimen repoRegimen = new RepositorioRegimen();
                regimen = repoRegimen.getById(idRegimen);
            }

            sqlConnection.Close();

            return regimen;
        }


        public Cliente getClienteByIdReserva(Reserva reserva) {
            Cliente cliente = null;
            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            sqlCommand.Parameters.AddWithValue("@idReserva", reserva.getIdReserva());
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT idCliente FROM LOS_BORBOTONES.Reserva WHERE idReserva = @idReserva";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            if (reader.Read())
            {
                int idCliente = reader.GetInt32(reader.GetOrdinal("idCliente"));
                RepositorioCliente repoCliente = new RepositorioCliente();
                cliente = repoCliente.getById(idCliente);
            }

            sqlConnection.Close();

            return cliente;
        }


        //el repositorio reserva no tira excepciones si falla al actualizar la reserva
        //pincha la aplicacion si falla
        public void cancelarReserva(Reserva reserva, Usuario usuario, String motivo) { 

            decimal codigoReserva= reserva.getCodigoReserva();
            //bool isRecepcionista = usuario.getRoles().Any(rol=>rol.getNombre().Equals("Recepcionista"));
            bool isGuest = usuario.getUsername().ToUpper().Equals("GUEST");

            String tipoEstado = "RCR";
            //todo lo que no sea generado por el guest, es generado por recepcion
            if(isGuest){
                tipoEstado="RCC";
            }
            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            sqlCommand.Parameters.AddWithValue("@fecha", Utils.getSystemDatetimeNow());
            sqlCommand.Parameters.AddWithValue("@descripcion", motivo);
            sqlCommand.Parameters.AddWithValue("@idUsuario", usuario.getIdUsuario());
            sqlCommand.Parameters.AddWithValue("@idReserva", reserva.getIdReserva());
            sqlCommand.Parameters.AddWithValue("@tipoEstado", tipoEstado);
            sqlCommand.Parameters.AddWithValue("@codigoReserva", codigoReserva);

            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "UPDATE LOS_BORBOTONES.EstadoReserva SET TipoEstado=@tipoEstado,Fecha=@fecha,Descripcion=@descripcion,idUsuario=@idUsuario " +
                                     "WHERE idReserva=@idReserva;";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();
            reader.Read();
            sqlConnection.Close();
            
        }

        public Reserva getReservaByCodigoReserva(int codigoReserva){


            Reserva reserva = null;

            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            sqlCommand.Parameters.AddWithValue("@codigoReserva", codigoReserva);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT * FROM LOS_BORBOTONES.Reserva WHERE CodigoReserva = @codigoReserva";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            if (reader.Read())
            {
                int idReserva = reader.GetInt32(reader.GetOrdinal("idReserva"));
                DateTime fechaCreacion = reader.GetDateTime(reader.GetOrdinal("FechaCreacion"));
                DateTime fechaDesde = reader.GetDateTime(reader.GetOrdinal("FechaDesde"));
                DateTime fechaHasta = reader.GetDateTime(reader.GetOrdinal("FechaHasta"));
                decimal diasAlojados = reader.GetDecimal(reader.GetOrdinal("DiasAlojados"));
                reserva = new Reserva(idReserva, null, null, null, null, codigoReserva, diasAlojados, fechaCreacion, fechaDesde, fechaHasta, null);
            }

            sqlConnection.Close();

            return reserva;
        }

        public Reserva getIdByIdEstadia(int idEstadia)
        {
            Reserva reserva = null;
            RepositorioRegimen repoRegimen = new RepositorioRegimen();
            RepositorioHotel repoHotel = new RepositorioHotel();
            RepositorioEstadia repoEstadia = new RepositorioEstadia();
            RepositorioCliente repoClientes = new RepositorioCliente();
            RepositorioEstadoReserva repoEstadoReserva = new RepositorioEstadoReserva();

            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            sqlCommand.Parameters.AddWithValue("@idEstadia", idEstadia);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT * FROM LOS_BORBOTONES.Reserva WHERE idEstadia = @idEstadia";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                int idReserva = reader.GetInt32(reader.GetOrdinal("idReserva"));
                decimal codigoReserva = reader.GetDecimal(reader.GetOrdinal("CodigoReserva"));
                decimal diasAlojados = reader.GetDecimal(reader.GetOrdinal("DiasAlojados"));
                DateTime fechaDesde = reader.SafeGetDateTime(reader.GetOrdinal("FechaDesde"));
                DateTime fechaHasta = reader.SafeGetDateTime(reader.GetOrdinal("FechaHasta"));
                DateTime fechaCreacion = reader.SafeGetDateTime(reader.GetOrdinal("FechaCreacion"));
                Hotel hotel = repoHotel.getById(reader.GetInt32(reader.GetOrdinal("idHotel"))); ;
                Regimen regimen = repoRegimen.getById(reader.GetInt32(reader.GetOrdinal("idRegimen")));
                Estadia estadia = repoEstadia.getById(reader.GetInt32(reader.GetOrdinal("idEstadia")));
                Cliente cliente = repoClientes.getById(reader.GetInt32(reader.GetOrdinal("idCliente")));
                EstadoReserva estado = repoEstadoReserva.getByIdReserva(idReserva);
                reserva = new Reserva(idReserva, hotel, estadia, regimen, cliente, codigoReserva, diasAlojados, fechaCreacion, fechaDesde, fechaHasta, estado);
            }
            sqlConnection.Close();

            return reserva;

        }


        public bool existReservaBetweenDate(DateTime fechaDesde, DateTime fechaHasta, int idHotel)
        {

            RepositorioHotel repoHotel = new RepositorioHotel();
            RepositorioEstadoReserva repoEstadoReserva = new RepositorioEstadoReserva();

            List<Reserva> reservas = new List<Reserva>();

            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            sqlCommand.Parameters.AddWithValue("@fechaDesde", fechaDesde);
            sqlCommand.Parameters.AddWithValue("@fechaHasta", fechaHasta);
            sqlCommand.Parameters.AddWithValue("@idHotel", idHotel);
            //  reserva.FechaDesde < cierreTemporal.FechaFin && cierreTemporal.FechaInicio < reserva.FechaHasta;
              
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT 1 FROM LOS_BORBOTONES.Reserva AS RES " +
                "JOIN LOS_BORBOTONES.EstadoReserva AS ESRE ON ESRE.idReserva = RES.idReserva " +
                "WHERE RES.FechaDesde < @fechaHasta AND @fechaDesde < RES.FechaHasta AND RES.idHotel = @idHotel " +
                "AND  ESRE.TipoEstado NOT IN ('RCR','RCC','RCNS')";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            bool exist = reader.Read();
           
            sqlConnection.Close();

            return exist;

        }
        override public Reserva getById(int idReserva)
        {

            RepositorioHotel repoHotel = new RepositorioHotel();
            RepositorioRegimen repoRegimen = new RepositorioRegimen();
            RepositorioCliente repoCliente = new RepositorioCliente();
            RepositorioEstadia repoEstadia = new RepositorioEstadia();
            RepositorioEstadoReserva repoEstadoReserva = new RepositorioEstadoReserva();

            //Elementos de la Reserva a devolver
            Reserva reserva;

  
            decimal codigoReserva = 0;
            decimal diasAlojados = 0;
            DateTime fechaCreacion =  Utils.getSystemDatetimeNow();
            DateTime fechaDesde =  Utils.getSystemDatetimeNow();
            DateTime fechaHasta =  Utils.getSystemDatetimeNow();
            
            //Configuraciones de la consulta
            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            //Primera Consulta
            sqlCommand.Parameters.AddWithValue("@idReserva", idReserva);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT * FROM LOS_BORBOTONES.Reserva WHERE idReserva = @idReserva";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                codigoReserva = reader.GetDecimal(reader.GetOrdinal("CodigoReserva"));
                diasAlojados = reader.GetDecimal(reader.GetOrdinal("DiasAlojados"));
                fechaDesde = reader.GetDateTime(reader.GetOrdinal("FechaDesde"));
                fechaHasta = reader.GetDateTime(reader.GetOrdinal("FechaHasta"));
                fechaCreacion = reader.GetDateTime(reader.GetOrdinal("FechaCreacion"));

            }

            //Cierro Primera Consulta
            sqlConnection.Close();

            //Armo la reserva completa
            reserva = new Reserva(idReserva, null, null, null, null, codigoReserva, diasAlojados, fechaCreacion, fechaDesde, fechaHasta, null);

            return reserva;
        }

        override public List<Reserva> getAll()
        {
            throw new NotImplementedException();            
        }

      

        override public int create(Reserva reserva)
        {
            decimal codigoReserva = 0;
            int idReserva = 0;


            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;

            sqlCommand.Parameters.AddWithValue("@fechaDesde", reserva.getFechaDesde());
            sqlCommand.Parameters.AddWithValue("@fechaHasta", reserva.getFechaHasta());
            sqlCommand.Parameters.AddWithValue("@diasAlojados", reserva.getDiasAlojados());
            sqlCommand.Parameters.AddWithValue("@idHotel", reserva.getHotel().getIdHotel());
            sqlCommand.Parameters.AddWithValue("@idRegimen", reserva.getRegimen().getIdRegimen());
            sqlCommand.Parameters.AddWithValue("@idCliente", reserva.getCliente().getIdCliente());
            sqlCommand.Parameters.AddWithValue("@idUsuario", reserva.getUsuarioGenerador().getIdUsuario());




            String CREATE_STATEMENT = "BEGIN TRANSACTION " +
                                        "BEGIN TRY " +
                                        "DECLARE @idReserva int; " +
                                        "DECLARE @codigoReserva decimal(18,0); " + 
                                        "SET @codigoReserva= (SELECT MAX(CodigoReserva) FROM LOS_BORBOTONES.Reserva) +1; " +
                                        "INSERT INTO LOS_BORBOTONES.Reserva(CodigoReserva, FechaCreacion, FechaDesde, FechaHasta,DiasAlojados,idHotel,idRegimen,idCliente) " +
                                        "VALUES(@codigoReserva, LOS_BORBOTONES.fn_getDate(),@fechaDesde,@fechaHasta,@diasAlojados,@idHotel,@idRegimen,@idCliente); " +
                                        "SET @idReserva = SCOPE_IDENTITY(); " +
                                        
                                        "INSERT INTO LOS_BORBOTONES.EstadoReserva(TipoEstado,Fecha,Descripcion,idUsuario,idReserva) " +
                                        "VALUES('RC', LOS_BORBOTONES.fn_getDate(),'Reserva Correcta',@idUsuario,@idReserva); " +
                                        
                                        getReservaXHabitacionInserts(reserva,sqlCommand) +
                                        
                                        "COMMIT TRANSACTION " +
                                        "SELECT @idReserva AS idReserva, @codigoReserva AS codigoReserva; " +
                                        "END TRY " +
                                        "BEGIN CATCH " +
                                        "RAISERROR('ERROR TRYING TO CREATE RESERVA', 16, 1) " +
                                        "ROLLBACK TRANSACTION " +
                                        "END CATCH";



            sqlCommand.CommandText = CREATE_STATEMENT;

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                idReserva = reader.GetInt32(reader.GetOrdinal("idReserva"));
                codigoReserva = reader.GetDecimal(reader.GetOrdinal("codigoReserva"));
            }
            
            sqlConnection.Close();

            reserva.setIdReserva(idReserva);
            reserva.setCodigoReserva(codigoReserva);
            return idReserva;

        }


        public void modificarReserva(Reserva reserva) {

            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;

            sqlCommand.Parameters.AddWithValue("@fechaDesde", reserva.getFechaDesde());
            sqlCommand.Parameters.AddWithValue("@fechaHasta", reserva.getFechaHasta());
            sqlCommand.Parameters.AddWithValue("@diasAlojados", reserva.getDiasAlojados());
            sqlCommand.Parameters.AddWithValue("@idHotel", reserva.getHotel().getIdHotel());
            sqlCommand.Parameters.AddWithValue("@idRegimen", reserva.getRegimen().getIdRegimen());
            sqlCommand.Parameters.AddWithValue("@idCliente", reserva.getCliente().getIdCliente());
            sqlCommand.Parameters.AddWithValue("@idUsuario", reserva.getUsuarioGenerador().getIdUsuario());
            sqlCommand.Parameters.AddWithValue("@idReserva", reserva.getIdReserva());




            String CREATE_STATEMENT = "BEGIN TRANSACTION " +
                                        "BEGIN TRY " +

                                        "UPDATE LOS_BORBOTONES.Reserva SET FechaDesde=@fechaDesde, FechaHasta=@fechaHasta,DiasAlojados=@diasAlojados,idHotel=@idHotel,idRegimen=@idRegimen,idCliente=@idCliente WHERE idReserva=@idReserva; " +

                                        "UPDATE LOS_BORBOTONES.EstadoReserva SET TipoEstado = 'RM',Fecha=LOS_BORBOTONES.fn_getDate(),Descripcion='Reserva Modificada',idUsuario=@idUsuario " +
                                        "WHERE idReserva=@idReserva; " +
                                        "DELETE FROM LOS_BORBOTONES.Reserva_X_Habitacion_X_Cliente WHERE idReserva=@idReserva; " +
                                        getReservaXHabitacionInserts(reserva, sqlCommand) +

                                        "COMMIT TRANSACTION " +
                                        "END TRY " +
                                        "BEGIN CATCH " +
                                        "RAISERROR('ERROR TRYING TO MODIFY RESERVA', 16, 1) " +
                                        "ROLLBACK TRANSACTION " +
                                        "END CATCH";



            sqlCommand.CommandText = CREATE_STATEMENT;

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();
            reader.Read();


            sqlConnection.Close();

        
        }



        private String getReservaXHabitacionInserts(Reserva reserva, SqlCommand sqlCommand)
        {
            String insert="";
            int index=0;

            foreach(Habitacion habitacion in reserva.getHabitaciones())
            {
                sqlCommand.Parameters.AddWithValue("@idHabitacion" + index, habitacion.getIdHabitacion());

                insert += "INSERT INTO LOS_BORBOTONES.Reserva_X_Habitacion_X_Cliente(idReserva, idHabitacion, idCliente) " +
                         "VALUES(@idReserva,@idHabitacion" + index + ",@idCliente); ";
            index++;

            }
            return insert;

        }

        override public void update(Reserva reserva)
        {
            if (this.exists(reserva))
            {
                //Actualizo el registro
            }
            else
            {
                //Error
            }
        }

        override public void delete(Reserva reserva)
        {
            if (this.exists(reserva))
            {
                //Borro el registro
            }
            else
            {
                //Error
            }
        }

        override public void bajaLogica(Reserva reserva)
        {
            throw new NotImplementedException();
        }

        override public Boolean exists(Reserva reserva)
        {
            int idReserva = 0;
            int idEstadia = 0;

            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            sqlCommand.Parameters.AddWithValue("@idReserva", reserva.getIdReserva());
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT idReserva FROM LOS_BORBOTONES.Reserva WHERE idReserva = @idReserva";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                idReserva = reader.GetInt32(reader.GetOrdinal("idReserva"));
            }

            sqlConnection.Close();

            Estadia estadia = null;
            estadia = reserva.getEstadia();

            //valido por el idEstadia que tiene en la base
            sqlCommand.Parameters.AddWithValue("@idEstadia", estadia.getIdEstadia());
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT idEstadia FROM LOS_BORBOTONES.Reserva WHERE idEstadia = @idEstadia";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                idEstadia = reader.GetInt32(reader.GetOrdinal("idEstadia"));
            }

            sqlConnection.Close();

            //Devuelve verdadero si el ID coincide o si el IdEstadia coincide
            return idReserva != 0 || estadia.getIdEstadia().Equals(idEstadia);
        }
        //luego hacer algun getBy que vea especial y el getByQuery
        public int getIdEstadiaByCodReserva(int codReserva)
        {
            int idEstadia = 0;
            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            sqlCommand.Parameters.AddWithValue("@CodReserva", codReserva);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT idEstadia FROM LOS_BORBOTONES.Reserva WHERE CodigoReserva = @CodReserva";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                //ESTADIA PUEDE TENER VALORES NULL PRIMERO PREGUNTO
                idEstadia = reader.IsDBNull(reader.GetOrdinal("idEstadia")) ? 0 : reader.GetInt32(reader.GetOrdinal("idEstadia"));   
            }

            sqlConnection.Close();
            
            return idEstadia;
        }
        public int GetReservaValida(int codReserva,DateTime date,Usuario user,int hotelIn)
        {
            int idHotel = 0;
            int idReserva = 0;
            int hotelFound = 0;
            decimal cantidadNoches = 0;
            DateTime fechaOut= Utils.getSystemDatetimeNow();
            RepositorioUsuario repouser = new RepositorioUsuario();
            Usuario userIn = user;
            if (userIn == null)
                return 4;

            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;
            
            sqlCommand.Parameters.AddWithValue("@CodReserva", codReserva);
            sqlCommand.Parameters.AddWithValue("@date", date);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT r.idReserva,r.idHotel,r.FechaHasta,r.DiasAlojados FROM LOS_BORBOTONES.Reserva as r,LOS_BORBOTONES.EstadoReserva as er WHERE r.CodigoReserva = @CodReserva and r.FechaDesde = @date and er.idReserva=r.idReserva and er.TipoEstado IN ('RC','RM')";
            
            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                idReserva = reader.GetInt32(reader.GetOrdinal("idReserva"));
                idHotel = reader.GetInt32(reader.GetOrdinal("idHotel"));
                fechaOut = reader.GetDateTime(reader.GetOrdinal("FechaHasta"));
                cantidadNoches = reader.GetDecimal(reader.GetOrdinal("DiasAlojados"));
            }

            sqlConnection.Close();
            if (idReserva == 0)
            {
                //llamo a cancelar la reserva en estado reserva
                RepositorioEstadia repoEstadia = new RepositorioEstadia();
                RepositorioEstadoReserva repoEstadoReserva = new RepositorioEstadoReserva();
                String estado=repoEstadia.getEstado(codReserva);
                if (estado.Equals("RC") | estado.Equals("RM"))
                {
                    repoEstadoReserva.rechazarReserva(codReserva,userIn.getIdUsuario(),date);
                    return 2;
                }
                return 5;

            }
            /* esto miraba que sea cualquier hotel del usuario, no en el que esta logueado
            foreach (Hotel h in userIn.getHoteles())
            {
                //si encuentro el hotel en el que el usuario trabaja entonces valido bien
                if (h.getIdHotel() == idHotel)
                    hotelFound = 1;
            }*/
            if (hotelIn == idHotel)
                hotelFound = 1;

            if (hotelFound == 0)
                return 3;

            if (idReserva != 0 && hotelFound != 0)
            {
                //llamo a actualizar la estadia
                RepositorioEstadia repoEstadia =new RepositorioEstadia();
                //int idEstadia = getIdEstadiaByCodReserva(codReserva);
                //comento lo de arriba ya que es un insert esto, no un update como pense al principio
                int idEstadia = 0;
                Boolean facturada = false;
                Usuario userOut=userIn;
                Estadia estadia = new Estadia(idEstadia, userIn, userOut,date,fechaOut,facturada,cantidadNoches);
                idEstadia= repoEstadia.create(estadia);
                //repoEstadia.updateIn(estadia);
               
                //hago update de reserva para darle id estadia
                Reserva reserva = getById(idReserva);
                
                this.updateIn(idReserva,idEstadia);

                //hago update de EstadoReserva
                RepositorioEstadoReserva repoEstadoReserva = new RepositorioEstadoReserva();
                
                int idEstadoReserva = 0;
                
                String desc = "Reserva Con Ingreso";
                String tipoEstado = "RCI";
                EstadoReserva estadoReserva = new EstadoReserva(idEstadoReserva, userIn, reserva, tipoEstado, date, desc);
                repoEstadoReserva.update(estadoReserva);

                return idEstadia;
            }
            return 0;
        }


         public bool existsReservasConRegimen(Regimen regimen,Hotel hotel) {

             String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
             SqlConnection sqlConnection = new SqlConnection(connectionString);
             SqlCommand sqlCommand = new SqlCommand();
             SqlDataReader reader;

             sqlCommand.Parameters.AddWithValue("@idRegimen", regimen.getIdRegimen());
             sqlCommand.Parameters.AddWithValue("@idHotel", hotel.getIdHotel());

             sqlCommand.CommandType = CommandType.Text;
             sqlCommand.Connection = sqlConnection;
             sqlCommand.CommandText =
                 "SELECT RES.idReserva FROM LOS_BORBOTONES.Reserva AS RES " +
                 "JOIN LOS_BORBOTONES.EstadoReserva AS ESRE ON ESRE.idReserva = RES.idReserva " +
                 "WHERE RES.idRegimen = @idRegimen " +
                 "AND RES.idHotel=@idHotel " +
                 "AND RES.FechaHasta > LOS_BORBOTONES.fn_getDate()" +
                 "AND ESRE.TipoEstado NOT  IN ('RCR','RCC','RCNS');";

             sqlConnection.Open();

             reader = sqlCommand.ExecuteReader();

             bool exist = reader.Read();
             sqlConnection.Close();

             return exist;
         }
    //metodo para traer el monto de una reserva
    public Decimal getMonto(Reserva reserva)
    {
            /*El valor de la habitación se obtiene a través de su precio base (ver abm de régimen)
            multiplicando la cantidad de personas que se alojarán en la habitación (tipo de habitación) y
            luego de ello aplicando un incremento en función de la categoría del Hotel (cantidad de
            estrellas)*/
             Decimal total = 0;
             RepositorioRegimen repoRegimen = new RepositorioRegimen();
             RepositorioHabitacion repoHabitacion = new RepositorioHabitacion();
        
             String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
             SqlConnection sqlConnection = new SqlConnection(connectionString);
             SqlCommand sqlCommand = new SqlCommand();
             SqlDataReader reader;

        //traigo el monto del regimen
             Decimal montoRegimen = repoRegimen.getMonto(reserva.getRegimen().getIdRegimen());
        
             sqlCommand.Parameters.AddWithValue("@idReserva", reserva.getIdReserva());
             sqlCommand.CommandType = CommandType.Text;
             sqlCommand.Connection = sqlConnection;
             sqlCommand.CommandText = "SELECT idHabitacion FROM LOS_BORBOTONES.Reserva_X_Habitacion_X_Cliente WHERE idReserva = @idReserva";

             sqlConnection.Open();

             reader = sqlCommand.ExecuteReader();

             while (reader.Read())
             {

                 Habitacion habitacion = repoHabitacion.getById(reader.GetInt32(reader.GetOrdinal("idHabitacion")));
                 TipoHabitacion tipoHabitacion= habitacion.getTipoHabitacion();

                 total = montoRegimen * tipoHabitacion.getPorcentual();
             }

             sqlConnection.Close();    
             
             //falta la suma por cantidad de estrellas
             Hotel hotel= reserva.getHotel();
             Categoria categoria=hotel.getCategoria();
             Decimal recargaEstrellas = categoria.getRecargaEstrellas();
             
             //Devuelve el monto total de las habitaciones para esa reserva.
             total = total + recargaEstrellas;

             return total;

         }
    public void updateIn(int idReserva,int idEstadia)
    {
        
            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            
            sqlCommand.Parameters.AddWithValue("@idEstadia", idEstadia);
            sqlCommand.Parameters.AddWithValue("@idReserva", idReserva);

            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append(@"
                    BEGIN TRY
                    BEGIN TRANSACTION

                    UPDATE LOS_BORBOTONES.Reserva
                    SET idEstadia = @idEstadia
                    WHERE idReserva = @idReserva;
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
    }
}

