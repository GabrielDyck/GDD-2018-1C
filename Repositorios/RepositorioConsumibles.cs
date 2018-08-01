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
    public class RepositorioConsumibles : Repositorio<Consumible>
    {
        override public Consumible getById(int idConsumible)
        {
            //Elementos del Consumible a devolver
            Consumible consumible;
            int codigo = 0;
            String descripcion = "";
            float precio = 0;

            //Configuraciones de la consulta
            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            //Primera Consulta
            sqlCommand.Parameters.AddWithValue("@idConsumible", idConsumible);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT * FROM LOS_BORBOTONES.Consumible WHERE idConsumible = @idConsumible";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                codigo = (int)reader.GetDecimal(reader.GetOrdinal("Codigo"));
                descripcion = reader.GetString(reader.GetOrdinal("Descripcion"));
                precio = (float)reader.GetDecimal(reader.GetOrdinal("Precio"));
            }

            //Cierro Primera Consulta
            sqlConnection.Close();

            //Si no encuentro elemento con ese ID tiro una excepción
            if (codigo==0) throw new NoExisteIDException("No existe consumible con el ID asociado");

            //Armo el consumible completo
            consumible = new Consumible(idConsumible, codigo, descripcion, precio);

            return consumible;
        }

        override public List<Consumible> getAll()
        {
            List<Consumible> consumibles = new List<Consumible>();

            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;

            sqlCommand.CommandText = "SELECT idConsumible FROM LOS_BORBOTONES.Consumible";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                consumibles.Add(this.getById(reader.GetInt32(reader.GetOrdinal("idConsumible"))));
            }

            sqlConnection.Close();

            return consumibles;
        }

        public void asociarConsumibleConEstadia(Consumible consumible, int cantidad, int idEstadia)
        {
            int cantidadExistente = 0;
            
            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.AddWithValue("@idEstadia", idEstadia);
            sqlCommand.Parameters.AddWithValue("@idConsumible", consumible.getIdConsumible());

            //PRIMERO OBTENGO CUANTOS TENIA ANTES
            sqlCommand.CommandText = @"
                SELECT Cantidad
                FROM LOS_BORBOTONES.Estadia_X_Consumible
                WHERE idEstadia = @idEstadia AND idConsumible = @idConsumible
            ";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                cantidadExistente = reader.GetInt32(reader.GetOrdinal("Cantidad"));
            }

            sqlConnection.Close();

            //LUEGO ACTUALIZO LA NUEVA CANTIDAD
            sqlCommand.Parameters.AddWithValue("@cantidad", cantidadExistente + cantidad);

            //SI NO EXISTE EL ITEM HAGO UN INSERT
            if (cantidadExistente.Equals(0))
            {
                sqlCommand.CommandText = @"
                    INSERT INTO LOS_BORBOTONES.Estadia_X_Consumible(idEstadia, idConsumible, Cantidad)
                    VALUES(@idEstadia, @idConsumible, @cantidad)
                ";
            }
            else
            {
                sqlCommand.CommandText = @"
                    UPDATE LOS_BORBOTONES.Estadia_X_Consumible
                    SET cantidad = @cantidad
                    WHERE idEstadia = @idEstadia AND idConsumible = @idConsumible;
                ";
            }

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            reader.Read();

            sqlConnection.Close();
        }

        /*
        override public int create(Consumible consumible)
        {
            //dar de alta y antes de darlo validar del lado interfaz si quiere cambiar algo
            int idConsumible = 0;
            if (this.exists(consumible))
            {
                //aca valido que el codigo sea unico y el id distinto a 0
                throw new ElementoYaExisteException("Ya existe el consumible que intenta crear");
            }
            else
            {
                //creo el nuevo consumible
                String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                SqlCommand sqlCommand = new SqlCommand();
                SqlDataReader reader;

               
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Parameters.AddWithValue("@Code", consumible.getCodigo());
                sqlCommand.Parameters.AddWithValue("@Desc", consumible.getDescripcion());
                sqlCommand.Parameters.AddWithValue("@Price", consumible.getPrecio());


                StringBuilder sqlBuilder = new StringBuilder();
                sqlBuilder.Append(@"
                    BEGIN TRY
                    BEGIN TRANSACTION

                    INSERT INTO LOS_BORBOTONES.Consumible(Codigo,Descripcion,Precio)
                    OUTPUT INSERTED.idConsumible
                    VALUES(@Code,@Desc,@Price);

                    DECLARE @idConsumible int;
                    SET @idConsumible = SCOPE_IDENTITY();
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
                    idConsumible = reader.GetInt32(reader.GetOrdinal("idConsumible"));
                }

                sqlConnection.Close();
            }

            return idConsumible;
     
        }
        
        */

        /*
         public int registrar(Consumible consumible)
        {
                int consRegistrado=0;

                String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                SqlCommand sqlCommand = new SqlCommand();
                SqlDataReader reader;

            
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Parameters.AddWithValue("@IdConsumible", consumible.getIdConsumible());
                sqlCommand.Parameters.AddWithValue("@IdEstadia", consumible.getIdEstadia());

                StringBuilder sqlBuilder = new StringBuilder();
                sqlBuilder.Append(@"
                    BEGIN TRY
                    BEGIN TRANSACTION

                    INSERT INTO LOS_BORBOTONES.Estadia_X_Consumible(idEstadia,idConsumible)
                    OUTPUT INSERTED.idConsumible
                    VALUES(@IdEstadia,@IdConsumible);

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
                    consRegistrado = reader.GetInt32(reader.GetOrdinal("idConsumible"));
                }

                sqlConnection.Close();
                return consRegistrado;
         }
        */
        

        override public void update(Consumible consumible)
        {
            throw new NotImplementedException();
        }

        override public void delete(Consumible consumible)
        {
            throw new NotImplementedException();
        }

        override public int create(Consumible consumible)
        {
            throw new NotImplementedException();
        }

        override public Boolean exists(Consumible consumible)
        {
            throw new NotImplementedException();
        }

        /*
        public override Boolean exists(Consumible consumible)
        {

            int idConsumible = 0;
            int codigo = 0;

            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            sqlCommand.Parameters.AddWithValue("@idConsumible", consumible.getIdConsumible());
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT idConsumible FROM LOS_BORBOTONES.Consumible WHERE idConsumible = @idConsumible";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                idConsumible = reader.GetInt32(reader.GetOrdinal("idConsumible"));
            }

            sqlConnection.Close();

            sqlCommand.Parameters.AddWithValue("@Code", consumible.getCodigo());
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT codigo FROM LOS_BORBOTONES.Consumible WHERE codigo = @Code";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                codigo = (int)reader.GetDecimal(reader.GetOrdinal("Codigo"));
            }

            sqlConnection.Close();

            //Devuelve verdadero si el ID coincide o si el username coincide
            return idConsumible != 0 || codigo != 0;
        }
        */

        override public void bajaLogica(Consumible consumible)
        {
            throw new NotImplementedException();
        }

        public void baja(Consumible consumible, int idEstadia)
        {
            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.AddWithValue("@idEstadia", idEstadia);
            sqlCommand.Parameters.AddWithValue("@idConsumible", consumible.getIdConsumible());

            sqlCommand.CommandText = @"
                DELETE FROM LOS_BORBOTONES.Estadia_X_Consumible
                WHERE idEstadia = @idEstadia AND idConsumible = @idConsumible
            ";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            reader.Read();

            sqlConnection.Close();
        }

        public List<ConsumibleParaMostrar> getByQuery(int idEstadia)
        {
            List<ConsumibleParaMostrar> consumibles = new List<ConsumibleParaMostrar>();
            String query = @"
                SELECT *
	            FROM LOS_BORBOTONES.Estadia_X_Consumible estXcons
                WHERE estXcons.idEstadia = @idEstadia
            ";

            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.AddWithValue("@idEstadia",idEstadia);

                //HAGO LA CONSULTA
                sqlCommand.CommandText = query;
                sqlConnection.Open();
                reader = sqlCommand.ExecuteReader();

                //ARMAR CLIENTES
                while (reader.Read())
                {
                    Consumible consumible = this.getById(reader.GetInt32(reader.GetOrdinal("idConsumible")));
                    ConsumibleParaMostrar consCantidad = new ConsumibleParaMostrar(consumible, reader.GetInt32(reader.GetOrdinal("Cantidad")));
                    consumibles.Add(consCantidad);
                }

                sqlConnection.Close();
      
            return consumibles;
        }
    }
}

