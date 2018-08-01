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
    public class RepositorioTipoHabitacion : Repositorio<TipoHabitacion>
    {
        public override int create(TipoHabitacion tipoHab)
        {

            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;
            int idTipoHabitacion = 0;

            sqlCommand.Parameters.AddWithValue("@Codigo", tipoHab.Codigo);
            sqlCommand.Parameters.AddWithValue("@Descripcion", tipoHab.Descripcion);
            sqlCommand.Parameters.AddWithValue("@Porcentual", tipoHab.Porcentual);

            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "INSERT INTO LOS_BORBOTONES.TipoHabitacion( Codigo,Descripcion,Porcentual) OUTPUT INSERTED.idTipoHabitacion VALUES (@Codigo,@Descripcion,@Porcentual);";

            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();

            if (reader.Read())
            {
                idTipoHabitacion = reader.GetInt32(reader.GetOrdinal("idTipoHabitacion"));
            }

            sqlConnection.Close();
            return idTipoHabitacion;
        }

        public override void delete(TipoHabitacion t)
        {
            throw new NotImplementedException();
        }

        public override bool exists(TipoHabitacion t)
        {
            throw new NotImplementedException();
        }

        public override List<TipoHabitacion> getAll()
        {
            List<TipoHabitacion> tipoHabitaciones = new List<TipoHabitacion>();
            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT * FROM LOS_BORBOTONES.TipoHabitacion";

            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                int idTipoHabitacion = reader.GetInt32(reader.GetOrdinal("idTipoHabitacion"));
                String codigo = reader.SafeGetString(reader.GetOrdinal("Codigo"));
                String descripcion = reader.SafeGetString(reader.GetOrdinal("Descripcion"));
                decimal porcentual = reader.GetDecimal(reader.GetOrdinal("Porcentual"));

                tipoHabitaciones.Add(new TipoHabitacion(idTipoHabitacion, codigo, porcentual, descripcion));
            }

            sqlConnection.Close();
            return tipoHabitaciones;
        }

        public override TipoHabitacion getById(int id)
        {

            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            TipoHabitacion tipoHabitacion = null;

            sqlCommand.Parameters.AddWithValue("@idTipoHabitacion", id);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT idTipoHabitacion,Codigo,Porcentual,Descripcion FROM LOS_BORBOTONES.TipoHabitacion AS TIPOHAB WHERE TIPOHAB.idTipoHabitacion = @idTipoHabitacion";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            if (reader.Read())
            {
                int idTipoHabitacion = reader.GetInt32(reader.GetOrdinal("idTipoHabitacion"));
                String codigo = reader.SafeGetString(reader.GetOrdinal("Codigo"));
                decimal porcentual = reader.GetDecimal(reader.GetOrdinal("Porcentual"));
                String descripcion = reader.GetString(reader.GetOrdinal("Descripcion"));

                tipoHabitacion= new TipoHabitacion(idTipoHabitacion,codigo,porcentual,descripcion);
            }
            //Cierro Primera Consulta
            sqlConnection.Close();

            return tipoHabitacion;
        }

        public override void update(TipoHabitacion t)
        {
            throw new NotImplementedException();
        }

        override public void bajaLogica(TipoHabitacion tipoHabitacion)
        {
            throw new NotImplementedException();
        }
    }
}
