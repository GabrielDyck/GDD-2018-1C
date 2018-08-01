using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrbaHotel.Modelo;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using FrbaHotel.Excepciones;

namespace FrbaHotel.Repositorios
{
    public class RepositorioFuncionalidad : Repositorio<Funcionalidad>
    {
        override public Funcionalidad getById(int idFuncionalidad)
        {
            //Elementos de la Funcionalidad a devolver
            String descripcion = "";
            Funcionalidad funcionalidad;

            //Configuraciones de la consulta
            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            //Primera Consulta
            sqlCommand.Parameters.AddWithValue("@idFuncionalidad", idFuncionalidad);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT * FROM LOS_BORBOTONES.Funcionalidad WHERE idFuncionalidad = @idFuncionalidad";
                     
            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                descripcion = reader.GetString(reader.GetOrdinal("Descripcion"));
            }

            //Cierro Primera Consulta
            sqlConnection.Close();

            //Si no encuentro elemento con ese ID tiro una excepción
            if (descripcion.Equals("")) throw new NoExisteIDException("No existe funcionalidad con el ID asociado");

            //Armo la funcionalidad completa
            funcionalidad = new Funcionalidad(idFuncionalidad, descripcion);

            return funcionalidad;
        }

        override public List<Funcionalidad> getAll()
        {
            List<Funcionalidad> funcionalidades = new List<Funcionalidad>();

            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;

            sqlCommand.CommandText = "SELECT idFuncionalidad FROM LOS_BORBOTONES.Funcionalidad";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                funcionalidades.Add(this.getById(reader.GetInt32(reader.GetOrdinal("idFuncionalidad"))));
            }

            sqlConnection.Close();

            return funcionalidades;
        }

        override public int create(Funcionalidad funcionalidad)
        {
            if (this.exists(funcionalidad))
            {
                //Error
            } else {
                //Creo un nuevo registro
            }


            throw new NotImplementedException();
        }

        override public void update(Funcionalidad funcionalidad)
        {
            if (this.exists(funcionalidad))
            {
                //Actualizo el registro
            }
            else
            {
                //Error
            }
        }

        override public void delete(Funcionalidad funcionalidad)
        {
            if (this.exists(funcionalidad))
            {
                //Borro el registro
            }
            else
            {
                //Error
            }
        }

        override public void bajaLogica(Funcionalidad funcionalidad)
        {
            throw new NotImplementedException();
        }

        override public Boolean exists(Funcionalidad funcionalidad)
        {
            int idFuncionalidad = 0;
            String descripcion = "";

            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            sqlCommand.Parameters.AddWithValue("@idFuncionalidad", funcionalidad.getIdFuncionalidad());
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT idFuncionalidad FROM LOS_BORBOTONES.Funcionalidad WHERE idFuncionalidad = @idFuncionalidad";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                idFuncionalidad = reader.GetInt32(reader.GetOrdinal("idFuncionalidad"));
            }

            sqlConnection.Close();

            sqlCommand.Parameters.AddWithValue("@Descripcion", funcionalidad.getDescripcion());
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT Descripcion FROM LOS_BORBOTONES.Funcionalidad WHERE Descripcion = @Descripcion";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                descripcion = reader.GetString(reader.GetOrdinal("Descripcion"));
            }

            sqlConnection.Close();

            //Devuelve verdadero si el ID coincide o si el Nombre coincide
            return idFuncionalidad != 0 || funcionalidad.getDescripcion().Equals(descripcion);
        }

        public Funcionalidad getByDescripcion(String descripcion)
        {
            int idFuncionalidad = 0;

            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            sqlCommand.Parameters.AddWithValue("@Descripcion", descripcion);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;

            sqlCommand.CommandText = "SELECT idFuncionalidad FROM LOS_BORBOTONES.Funcionalidad WHERE descripcion = @Descripcion";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                idFuncionalidad = reader.GetInt32(reader.GetOrdinal("idFuncionalidad"));
            }

            sqlConnection.Close();

            //Si no encuentro elemento con esa Descripcion tiro una excepción
            if (idFuncionalidad.Equals(0)) throw new NoExisteNombreException("No existe funcionalidad con la Descripcion asociada");

            return getById(idFuncionalidad);
        }
    }
}
