using System.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using FrbaHotel.Modelo;
using FrbaHotel.Excepciones;

namespace FrbaHotel.Repositorios {

    public class RepositorioHotel : Repositorio<Hotel>
    {

        public int crearBajaTemporal(CierreTemporal cierreTemporal){

            RepositorioCierreTemporal repositorioCierreTemporal = new RepositorioCierreTemporal();
            RepositorioReserva repositorioReserva = new RepositorioReserva();
            Hotel hotel = cierreTemporal.getHotel();
            bool existReservas = repositorioReserva.existReservaBetweenDate(cierreTemporal.getFechaInicio(), cierreTemporal.getFechaFin(), cierreTemporal.getHotel().getIdHotel());
            if(existReservas){
                    throw new RequestInvalidoException("No es posible dar de baja temporal el hotel. Existen reservas para la fecha la cual se quiere dar de baja el hotel");
                }
            return repositorioCierreTemporal.create(cierreTemporal);
        }


        public List<Hotel> getByQuery(String nombreHotel,int? estrellas,String ciudad, String pais)
        {

            RepositorioCategoria repositorioCategoria = new RepositorioCategoria();
            RepositorioRegimen repositorioRegimen = new RepositorioRegimen();
            RepositorioCierreTemporal repositorioCierreTemporal = new RepositorioCierreTemporal();
            RepositorioDireccion repositorioDireccion = new RepositorioDireccion();
            RepositorioReserva repositorioReserva = new RepositorioReserva();

            List<Hotel> hoteles = new List<Hotel>();
            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText =
                "SELECT DISTINCT(HOT.idHotel),HOT.Nombre,HOT.Mail,HOT.Telefono,HOT.FechaInicioActividades,HOT.idCategoria,HOT.idDireccion FROM LOS_BORBOTONES.Hotel AS HOT" +
                " JOIN LOS_BORBOTONES.Categoria AS CAT ON CAT.idCategoria= HOT.idCategoria" +
                " JOIN LOS_BORBOTONES.Direccion AS DIR ON DIR.idDireccion = HOT.idDireccion" + getCondiciones(nombreHotel, estrellas, ciudad, pais,sqlCommand) + ";";
            
            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                int idHotel = reader.GetInt32(reader.GetOrdinal("idHotel"));
                String nombre = reader.GetString(reader.GetOrdinal("Nombre"));
                String mail = reader.SafeGetString(reader.GetOrdinal("Mail"));
                String telefono = reader.SafeGetString(reader.GetOrdinal("Telefono"));
                DateTime fechaInicio = reader.GetDateTime(reader.GetOrdinal("FechaInicioActividades"));
                int idCategoria = reader.GetInt32(reader.GetOrdinal("idCategoria"));
                int idDireccion = reader.GetInt32(reader.GetOrdinal("idDireccion"));

                Categoria categoria = repositorioCategoria.getById(idCategoria);

                Direccion direccion = repositorioDireccion.getById(idDireccion);

                Hotel hotel = new Hotel(idHotel, categoria, direccion, nombre, mail, telefono, fechaInicio);
                hoteles.Add(hotel);
            }

            //Cierro Primera Consulta
            sqlConnection.Close();
            return hoteles;

        }

        private String getCondiciones(String nombreHotel, int? estrellas, String ciudad, String pais, SqlCommand sqlCommand)
        {

            List<String> condiciones = new List<String>();
            if (nombreHotel != null)
            {
                condiciones.Add("HOT.Nombre LIKE '%' + @hotNombreHotel + '%'");
                sqlCommand.Parameters.AddWithValue("@hotNombreHotel", nombreHotel);

            }
            if (ciudad != null)
            {
                condiciones.Add("DIR.Ciudad LIKE '%' + @dirCiudad + '%'");
                sqlCommand.Parameters.AddWithValue("@dirCiudad", ciudad);

            }
            if (pais != null)
            {
                condiciones.Add("DIR.Pais LIKE '%' + @dirPais + '%'");
                sqlCommand.Parameters.AddWithValue("@dirPais", pais);
            }
            if (estrellas != null)
            {
                condiciones.Add("CAT.Estrellas=@catEstrellas");
                sqlCommand.Parameters.AddWithValue("@catEstrellas", estrellas);
            }
            if (condiciones.Count != 0)
            {
                return " WHERE " + string.Join(" AND ", condiciones.ToArray());
            }
            return "";
        }

        public Boolean yaExisteHotelMismoNombre(Hotel hotel)
        {
            int idHotelExistente = 0;

            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            sqlCommand.Parameters.AddWithValue("@nombre", hotel.getNombre());
            sqlCommand.Parameters.AddWithValue("@idHotel", hotel.getIdHotel());
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = @"
                SELECT idHotel
                FROM LOS_BORBOTONES.Hotel
                WHERE Nombre = @nombre
                  AND idHotel <> @idHotel
            ";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                idHotelExistente = reader.GetInt32(reader.GetOrdinal("idHotel"));
            }

            sqlConnection.Close();

            return idHotelExistente != 0;
        }

        public override int create(Hotel hotel)
        {

            int idHotel = 0;

          

            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;


            String CREATE_STATEMENT = "BEGIN TRANSACTION" +
                                      " BEGIN TRY" +
                                      " DECLARE @idDireccion int; " +
                                      
                                       "DECLARE @hotidHotel int;" +
                                      
                                      "INSERT INTO LOS_BORBOTONES.Direccion(Pais, Ciudad, Calle, NumeroCalle) VALUES(@dirPais,@dirCiudad,@dirCalle, @dirNumeroCalle);" +
                                      "SET @idDireccion = SCOPE_IDENTITY();" +
                                      "INSERT INTO LOS_BORBOTONES.Hotel(idCategoria, Nombre, Mail, Telefono, FechaInicioActividades, idDireccion) OUTPUT INSERTED.idHotel " +
                                      "VALUES(@hotidCategoria,@hotNombre, @hotMail, @hotTelefono, @hotFechaIniciaActividades, @idDireccion); " +
                                      "SET @hotidHotel = SCOPE_IDENTITY(); " +
                                      this.insertsRegimenes(hotel, sqlCommand) +
                                      "COMMIT TRANSACTION " +
                                      "END TRY " +
                                      "BEGIN CATCH " +
                                      "RAISERROR('ERROR TRYING TO CREATE HOTEL', 16, 1) " +
                                      "ROLLBACK TRANSACTION " +
                                      "END CATCH";


            //HOTEL
            sqlCommand.Parameters.AddWithValue("@hotNombre", hotel.getNombre());
            sqlCommand.Parameters.AddWithValue("@hotMail", hotel.getMail());
            sqlCommand.Parameters.AddWithValue("@hotTelefono", hotel.getTelefono());
            sqlCommand.Parameters.AddWithValue("@hotFechaIniciaActividades", hotel.getFechaInicioActividades());
            //CATEGORIA
            sqlCommand.Parameters.AddWithValue("@hotidCategoria", hotel.getCategoria().getIdCategoria());
            //DIRECCION
            sqlCommand.Parameters.AddWithValue("@dirPais", hotel.getDireccion().getPais());
            sqlCommand.Parameters.AddWithValue("@dirCiudad", hotel.getDireccion().getCiudad());
            sqlCommand.Parameters.AddWithValue("@dirCalle", hotel.getDireccion().getCalle());
            sqlCommand.Parameters.AddWithValue("@dirNumeroCalle", hotel.getDireccion().getNumeroCalle());

            sqlCommand.CommandText = CREATE_STATEMENT;

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                idHotel = reader.GetInt32(reader.GetOrdinal("idHotel"));
            }
            sqlConnection.Close();
            return idHotel;

        }
    

        public override void delete(Hotel t)
        {
            throw new System.NotImplementedException();
        }

        public override void bajaLogica(Hotel t)
        {
            throw new System.NotImplementedException();
        }

        public override bool exists(Hotel hotel)
        {
            return this.getById(hotel.getIdHotel()) !=null;
        }

        public override List<Hotel> getAll()
        {

            RepositorioCategoria repositorioCategoria = new RepositorioCategoria();
            RepositorioRegimen repositorioRegimen = new RepositorioRegimen();
            RepositorioCierreTemporal repositorioCierreTemporal = new RepositorioCierreTemporal();
            RepositorioDireccion repositorioDireccion = new RepositorioDireccion();
            RepositorioReserva repositorioReserva = new RepositorioReserva();

            List<Hotel> hoteles = new List<Hotel>();
            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;
            Hotel hotel = null;

            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText =
                "SELECT idHotel,Nombre,Mail,Telefono,FechaInicioActividades,idCategoria,idDireccion FROM LOS_BORBOTONES.Hotel AS HOT;";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                int idHotel = reader.GetInt32(reader.GetOrdinal("idHotel"));
                String nombre = reader.GetString(reader.GetOrdinal("Nombre"));
                String mail = reader.SafeGetString(reader.GetOrdinal("Mail"));
                String telefono = reader.SafeGetString(reader.GetOrdinal("Telefono"));
                DateTime fechaInicio = reader.GetDateTime(reader.GetOrdinal("FechaInicioActividades"));
                int idCategoria = reader.GetInt32(reader.GetOrdinal("idCategoria"));
                int idDireccion = reader.GetInt32(reader.GetOrdinal("idDireccion"));

                Categoria categoria = repositorioCategoria.getById(idCategoria);

                Direccion direccion = repositorioDireccion.getById(idDireccion);

                hotel = new Hotel(idHotel, categoria, direccion, nombre, mail, telefono,
                                fechaInicio);
                hoteles.Add(hotel);
            }

            //Cierro Primera Consulta
            sqlConnection.Close();


            return hoteles;
        }

        public override Hotel getById(int id)
        {

            RepositorioCategoria repositorioCategoria = new RepositorioCategoria();
            RepositorioRegimen repositorioRegimen = new RepositorioRegimen();
            RepositorioCierreTemporal repositorioCierreTemporal = new RepositorioCierreTemporal();
            RepositorioDireccion repositorioDireccion = new RepositorioDireccion();
            RepositorioReserva repositorioReserva = new RepositorioReserva();

            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;
            Hotel hotel=null;
            sqlCommand.Parameters.AddWithValue("@idHotel", id);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT * FROM LOS_BORBOTONES.Hotel WHERE idHotel = @idHotel";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            if (reader.Read())
            {
                int idHotel = reader.GetInt32(reader.GetOrdinal("idHotel"));
                String nombre = reader.GetString(reader.GetOrdinal("Nombre"));
                String mail = reader.SafeGetString(reader.GetOrdinal("Mail"));
                String telefono = reader.SafeGetString(reader.GetOrdinal("Telefono"));
                DateTime fechaInicio = reader.GetDateTime(reader.GetOrdinal("FechaInicioActividades"));
                int idCategoria = reader.GetInt32(reader.GetOrdinal("idCategoria"));
                int idDireccion = reader.GetInt32(reader.GetOrdinal("idDireccion"));

                Categoria categoria = repositorioCategoria.getById(idCategoria);

                Direccion direccion = repositorioDireccion.getById(idDireccion);



                hotel = new Hotel(idHotel, categoria, direccion, nombre, mail, telefono, fechaInicio);
            }
            else
            {
                //Si no encuentro elemento con ese ID tiro una excepción
                throw new NoExisteIDException("No existe hotel con el ID asociado");
            }

            //Cierro Primera Consulta
            sqlConnection.Close();

            return hotel;
        }

        public override void update(Hotel hotel)
        {

            if (this.exists(hotel))
            {
                String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                SqlCommand sqlCommand = new SqlCommand();
                SqlDataReader reader;

                Direccion direccion = hotel.getDireccion();
                Categoria categoria = hotel.getCategoria();

                //HOTEL
                sqlCommand.Parameters.AddWithValue("@hotidHotel", hotel.getIdHotel());
                sqlCommand.Parameters.AddWithValue("@hotnombre", hotel.Nombre);
                sqlCommand.Parameters.AddWithValue("@hotmail", hotel.Mail);
                sqlCommand.Parameters.AddWithValue("@hottelefono", hotel.Telefono);
                sqlCommand.Parameters.AddWithValue("@hotfechaInicioActividades", hotel.FechaInicioActividades);
                
                //DIRECCION
                sqlCommand.Parameters.AddWithValue("@dirpais", direccion.Pais);
                sqlCommand.Parameters.AddWithValue("@dirciudad", direccion.Ciudad);
                sqlCommand.Parameters.AddWithValue("@dircalle", direccion.Calle);
                sqlCommand.Parameters.AddWithValue("@dirnumeroCalle", direccion.NumeroCalle);
                sqlCommand.Parameters.AddWithValue("@dirpiso", direccion.Piso);
                sqlCommand.Parameters.AddWithValue("@dirdepartamento", direccion.Departamento);
                sqlCommand.Parameters.AddWithValue("@idDireccion", direccion.getIdDireccion());

                //CATEGORIA
                sqlCommand.Parameters.AddWithValue("@catId", categoria.getIdCategoria());

                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = 
                    "BEGIN TRANSACTION " +
                    "DELETE FROM LOS_BORBOTONES.Regimen_X_Hotel WHERE idHotel=@hotidHotel; " + 
                    insertsRegimenes(hotel,sqlCommand) +
                    "UPDATE LOS_BORBOTONES.Direccion " +
                    "SET Pais= @dirpais,Ciudad= @dirciudad, Calle=@dircalle, NumeroCalle= @dirnumeroCalle, " +
                    "Piso=@dirpiso, Depto=@dirdepartamento WHERE idDireccion=@idDireccion; " +
                   
                    "UPDATE LOS_BORBOTONES.Hotel " +
                    "SET Nombre= @hotnombre, Mail= @hotmail, Telefono= @hottelefono, FechaInicioActividades= @hotfechaInicioActividades, " +
                    "idCategoria=@catId " +
                    "WHERE idHotel= @hotidHotel; " + 
                    "COMMIT";

                sqlConnection.Open();
                reader = sqlCommand.ExecuteReader();
                //Checkear excepcion si no existe u ocurrio algun problema con el update

                //Cierro Primera Consulta
                sqlConnection.Close();
            }
            else
            {
                //throw new RequestInvalidoException("No es posible actualizar: No existe el hotel con id " + hotel.getIdHotel() + "en la base de datos");
                throw new NoExisteIDException("No existe el Hotel que intenta actualizar");
            }
        }


        private String insertsRegimenes(Hotel hotel,SqlCommand sqlCommand)
        {
            String update = "";
            List<Regimen> regimenes = hotel.getRegimenes();
            int index=0;
            foreach (Regimen regimen in regimenes)
            {
               sqlCommand.Parameters.AddWithValue("@rxhidRegimen"+index, regimen.getIdRegimen());

               update += " INSERT INTO LOS_BORBOTONES.Regimen_X_Hotel(idHotel,idRegimen) values(@hotidHotel," + "@rxhidRegimen" + index + "); ";
               index++;
            }
            return update;
        }


    }
}

