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
    class RepositorioItemFactura : Repositorio<ItemFactura>
    {

        override public int create(ItemFactura itemFactura)
        {
            int idItemFactura = 0;

            if (this.exists(itemFactura))
            {
                //si existe no hago nada ya que ya fue facturado para ese mismo consumible con la cantidad adecuada
                //throw new ElementoYaExisteException("Ya existe el itemFactura que intenta crear");
            }
            else
            {
                String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                SqlCommand sqlCommand = new SqlCommand();
                SqlDataReader reader;

                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Parameters.AddWithValue("@FechaCreacion", itemFactura.getFechaCreacion());
                sqlCommand.Parameters.AddWithValue("@Cantidad", itemFactura.getCantidad());
                sqlCommand.Parameters.AddWithValue("@Monto", itemFactura.getMonto());
                sqlCommand.Parameters.AddWithValue("@IdFactura", itemFactura.getIdFactura());
                sqlCommand.Parameters.AddWithValue("@IdConsumible", itemFactura.getIdConsumible());

                StringBuilder sqlBuilder = new StringBuilder();
                sqlBuilder.Append(@"
                    BEGIN TRY
                    BEGIN TRANSACTION
                    INSERT INTO LOS_BORBOTONES.ItemFactura(Cantidad,Monto,FechaCreacion,idFactura,idConsumible)
                    OUTPUT INSERTED.idItemFactura
                    VALUES(@Cantidad,@Monto,@FechaCreacion,@IdFactura,@IdConsumible);
                    DECLARE @idItemFactura int;
                    SET @idItemFactura = SCOPE_IDENTITY();
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
                    idItemFactura = reader.GetInt32(reader.GetOrdinal("idItemFactura"));
                }

                sqlConnection.Close();
            }

            return idItemFactura;
        }


        override public void update(ItemFactura itemFactura)
        {
            if (this.exists(itemFactura))
            {
                //Actualizo el registro
            }
            else
            {
                //Error
            }
        }

        override public void delete(ItemFactura itemFactura)
        {
            if (this.exists(itemFactura))
            {
                //Borro el registro
            }
            else
            {
                //Error
            }
        }

        override public void bajaLogica(ItemFactura itemFactura)
        {
            throw new NotImplementedException();
        }
        override public List<ItemFactura> getAll()
        {
            throw new NotImplementedException();
        }
        override public ItemFactura getById(int idItemFactura)
        {
            throw new NotImplementedException();
        }
        /*
        override public Boolean exists(ItemFactura itemFactura)
        {
            int idItemFactura = 0;

            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            sqlCommand.Parameters.AddWithValue("@idItemFactura", itemFactura.getIdItemFactura());
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT idItemFactura FROM LOS_BORBOTONES.ItemFactura WHERE idItemFactura = @idItemFactura";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                idItemFactura = reader.GetInt32(reader.GetOrdinal("idItemFactura"));
            }

            sqlConnection.Close();

            //Devuelve verdadero si el ID coincide
            return idItemFactura != 0;
        }*/
        override public Boolean exists(ItemFactura itemFactura)
        {
            int idItemFactura = 0;

            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            sqlCommand.Parameters.AddWithValue("@FechaCreacion", itemFactura.getFechaCreacion());
            sqlCommand.Parameters.AddWithValue("@Cantidad", itemFactura.getCantidad());
            sqlCommand.Parameters.AddWithValue("@Monto", itemFactura.getMonto());
            sqlCommand.Parameters.AddWithValue("@IdFactura", itemFactura.getIdFactura());
            sqlCommand.Parameters.AddWithValue("@IdConsumible", itemFactura.getIdConsumible());
            sqlCommand.CommandType = CommandType.Text;
            
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT idItemFactura FROM LOS_BORBOTONES.ItemFactura WHERE FechaCreacion = @FechaCreacion and Cantidad = @Cantidad and Monto = @Monto and idFactura = @IdFactura and idConsumible = @IdConsumible";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();


            while (reader.Read())
            {
                idItemFactura = reader.GetInt32(reader.GetOrdinal("idItemFactura"));
            }

            sqlConnection.Close();

            //Devuelve verdadero si el ID coincide

            return idItemFactura != 0;

        }
        public void createTodos(List<ItemFactura> itemsFactura)
        {
            foreach(ItemFactura item in itemsFactura)
            {
                this.create(item);
            }
            
            
            
            /*

            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append(@"
                    BEGIN TRY
                    BEGIN TRANSACTION

                ");

            int k = 1;
            foreach (ItemFactura item in itemsFactura)
            {    
                if (this.exists(item))
                {
                    //throw new ElementoYaExisteException("Ya existe el itemFactura que intenta crear");
                }
                else
                {
                    String paramName = "@FechaCreacion" + k.ToString();
                    String paramName1 = "@Cantidad" + k.ToString();
                    String paramName2 = "@Monto" + k.ToString();
                    String paramName3 = "@IdFactura" + k.ToString();
                    String paramName4 = "@IdConsumible" + k.ToString();
                    sqlBuilder.AppendFormat("INSERT INTO LOS_BORBOTONES.ItemFactura(FechaCreacion,Cantidad,Monto,idFactura,idConsumible) VALUES({0},{1},{2},{3},{4})", paramName, paramName1, paramName2, paramName3, paramName4);

                    float temp1 = item.getCantidad();
                    float temp2 = item.getMonto();
                    DateTime temp3 = item.getFechaCreacion();
                    int temp4 = item.getIdFactura();
                    int temp5 = item.getIdConsumible();
                    
                    sqlCommand.Parameters.AddWithValue(paramName, item.getCantidad());
                    sqlCommand.Parameters.AddWithValue(paramName1, item.getMonto());
                    sqlCommand.Parameters.AddWithValue(paramName2, item.getFechaCreacion());
                    sqlCommand.Parameters.AddWithValue(paramName3, item.getIdFactura());
                    sqlCommand.Parameters.AddWithValue(paramName4, item.getIdConsumible());

                    k++;
                }
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
            */

            
        }

    }
}
