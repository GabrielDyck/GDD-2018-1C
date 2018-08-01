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
    class RepositorioRegimen : Repositorio<Regimen>
    {
        //NO SE VA A HACER ABM DEL REGIMEN POR LO QUE SOLO HABRIA QUE HACER METODOS DE LECTURA
        public override int create(Regimen t)
        {
            throw new NotImplementedException();
        }

        public override void delete(Regimen t)
        {
            throw new NotImplementedException();
        }

        public override bool exists(Regimen t)
        {
            throw new NotImplementedException();
        }

        public override List<Regimen> getAll()
        {
            List<Regimen> regimenes = new List<Regimen>();
            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText =
                "SELECT idRegimen,Codigo,Descripcion,Precio,Activo FROM LOS_BORBOTONES.Regimen AS REG;";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                int idRegimen = reader.GetInt32(reader.GetOrdinal("idRegimen"));
                String codigo = reader.GetString(reader.GetOrdinal("Codigo"));
                String descripcion = reader.SafeGetString(reader.GetOrdinal("Descripcion"));
                decimal precio = reader.GetDecimal(reader.GetOrdinal("Precio"));
                bool activo = reader.GetBoolean(reader.GetOrdinal("Activo"));

                Regimen regimen = new Regimen(idRegimen, codigo, descripcion, precio, activo);

                regimenes.Add(regimen);
            }
            return regimenes;
        }

        public override Regimen getById(int id)
        {

            Regimen regimen = null;
            //Configuraciones de la consulta
            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            //Primera Consulta
            sqlCommand.Parameters.AddWithValue("@idRegimen", id);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT * FROM LOS_BORBOTONES.Regimen WHERE idRegimen = @idRegimen";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            if (reader.Read())
            {
            
                int idRegimen = reader.GetInt32(reader.GetOrdinal("idRegimen"));
                String codigo = reader.SafeGetString(reader.GetOrdinal("Codigo"));
                String descripcion = reader.SafeGetString(reader.GetOrdinal("Descripcion"));
                decimal precio = reader.GetDecimal(reader.GetOrdinal("Precio"));
                bool activo = reader.GetBoolean(reader.GetOrdinal("Activo"));
                regimen = new Regimen(idRegimen, codigo, descripcion, precio, activo);

            }

            //Cierro Primera Consulta
            sqlConnection.Close();

          
            return regimen;
        }

        public override void update(Regimen t)
        {
            throw new NotImplementedException();
        }

        public override void bajaLogica(Regimen regimen)
        {
            throw new System.NotImplementedException();
        }
        
        public List<Regimen> getByIdHotel(int idHotel){

            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            List<Regimen> regimenes = new List<Regimen>() ;

            sqlCommand.Parameters.AddWithValue("@idHotel", idHotel);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT REG.idRegimen,REG.Codigo,REG.Descripcion,REG.Precio,REG.Activo FROM LOS_BORBOTONES.Regimen_X_Hotel AS RXH  "+ 
                "JOIN LOS_BORBOTONES.Regimen AS REG ON REG.idRegimen = RXH.idRegimen " +
                "WHERE RXH.idHotel = @idHotel" ;

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {  
                int idRegimen = reader.GetInt32(reader.GetOrdinal("idRegimen"));
                String codigo = reader.GetString(reader.GetOrdinal("Codigo"));
                String descripcion = reader.GetString(reader.GetOrdinal("Descripcion"));
                decimal precio = reader.GetDecimal(reader.GetOrdinal("Precio"));
                Boolean estado = reader.GetBoolean(reader.GetOrdinal("Activo"));

                regimenes.Add(new Regimen(idRegimen, codigo, descripcion, precio, estado));
            }
            //Cierro Primera Consulta
            sqlConnection.Close();

            return regimenes;
        }
        public Decimal getMonto(int idRegimen)
        {
            
            Decimal total = 0;

            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];

            SqlConnection sqlConnection = new SqlConnection(connectionString);

            SqlCommand sqlCommand = new SqlCommand();

            SqlDataReader reader;



            sqlCommand.Parameters.AddWithValue("@idRegimen", idRegimen);

            sqlCommand.CommandType = CommandType.Text;

            sqlCommand.Connection = sqlConnection;


            sqlCommand.CommandText = "SELECT Precio FROM LOS_BORBOTONES.Regimen WHERE idRegimen = @idRegimen";

            sqlConnection.Open();



            reader = sqlCommand.ExecuteReader();



            while (reader.Read())
            {

                total = reader.GetDecimal(reader.GetOrdinal("Precio"));

            }


            sqlConnection.Close();
            //Devuelve el monto de ese regimen.

            return total;

        }
    }
}

