using FrbaHotel.Modelo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FrbaHotel.Repositorios
{
    class RepositorioCierreTemporal : Repositorio<CierreTemporal>
    {
        public override int create(CierreTemporal cierreTemporal)
        {
            int idEstadoHotel = 0;
            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.AddWithValue("@cierreFechaInicio", cierreTemporal.FechaInicio);
            sqlCommand.Parameters.AddWithValue("@cierreFechaFin", cierreTemporal.FechaFin);
            sqlCommand.Parameters.AddWithValue("@cierreDescripcion", cierreTemporal.Descripcion);
            sqlCommand.Parameters.AddWithValue("@cierreidHotel", cierreTemporal.getHotel().getIdHotel());
            sqlCommand.CommandText =
                "INSERT INTO LOS_BORBOTONES.CierreTemporal (FechaInicio,FechaFin,Descripcion,idHotel) OUTPUT INSERTED.idEstadoHotel VALUES(@cierreFechaInicio,@cierreFechaFin,@cierreDescripcion,@cierreidHotel);";


            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            if (reader.Read())
            {
                idEstadoHotel = reader.GetInt32(reader.GetOrdinal("idEstadoHotel"));
            }

            //Cierro Primera Consulta
            sqlConnection.Close();

            return idEstadoHotel;

        }

        
        public List<CierreTemporal> getByIdHotel(Hotel hotel)
        {
            List<CierreTemporal> cierresTemporales = new List<CierreTemporal>();
            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.AddWithValue("@idHotel", hotel.getIdHotel());
            sqlCommand.CommandText = @"
                SELECT *
                FROM LOS_BORBOTONES.CierreTemporal c
                WHERE c.idHotel = @idHotel;";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                int idEstadoHotel = reader.GetInt32(reader.GetOrdinal("idEstadoHotel"));
                DateTime fechaInicio = reader.GetDateTime(reader.GetOrdinal("FechaInicio"));
                DateTime fechaFin = reader.GetDateTime(reader.GetOrdinal("FechaFin"));
                String descripcion = reader.SafeGetString(reader.GetOrdinal("Descripcion"));

                CierreTemporal cierreTemporal = new CierreTemporal(idEstadoHotel, fechaInicio, fechaFin, descripcion, hotel);
                cierresTemporales.Add(cierreTemporal);
            }

            //Cierro Primera Consulta
            sqlConnection.Close();

            return cierresTemporales;

        }

         
        public override void delete(CierreTemporal t)
        {
            throw new NotImplementedException();
        }

        public override void bajaLogica(CierreTemporal t)
        {
            throw new NotImplementedException();
        }

        public override bool exists(CierreTemporal t)
        {
            throw new NotImplementedException();
        }

        public override List<CierreTemporal> getAll()
        {
            throw new NotImplementedException();
        }

        public override CierreTemporal getById(int id)
        {
            throw new NotImplementedException();
        }

        public override void update(CierreTemporal t)
        {
            throw new NotImplementedException();
        }
        /*
        public List<CierreTemporal> getByHotelId(int id){

            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            List<CierreTemporal> cierresTemporales = new List<CierreTemporal>();


            sqlCommand.Parameters.AddWithValue("@idHotel", id);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT idEstadoHotel,FechaInicio,FechaFin,Descripcion,idHotel FROM LOS_BORBOTONES.CierreTemporal WHERE idHotel = @idHotel";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                int idCierreTemporal = reader.GetInt32(reader.GetOrdinal("idEstadoHotel"));
                DateTime fechaInicio = reader.SafeGetDateTime(reader.GetOrdinal("FechaInicio"));
                DateTime fechaFin = reader.SafeGetDateTime(reader.GetOrdinal("FechaFin"));
                String descripcion = reader.GetString(reader.GetOrdinal("Descripcion"));
                int idHotel = reader.GetInt32(reader.GetOrdinal("idHotel"));

                cierresTemporales.Add(new CierreTemporal(idCierreTemporal, fechaInicio, fechaFin, descripcion,idHotel));
            }
            //Cierro Primera Consulta
            sqlConnection.Close();

            return cierresTemporales;

        }
         * 
         * */
    }
}
