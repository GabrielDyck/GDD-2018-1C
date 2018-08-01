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
    class RepositorioFactura : Repositorio<Factura>
    {
        override public List<Factura> getAll()
        {
            throw new NotImplementedException();
        }
        override public Factura getById(int idFactura)
        {
            throw new NotImplementedException();
        }

        override public int create(Factura factura)
        {
            int idFactura = 0;

            if (this.exists(factura))
            {
                throw new ElementoYaExisteException("Ya existe la factura que intenta crear");
            }
            else
            {
                String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                SqlCommand sqlCommand = new SqlCommand();
                SqlDataReader reader;

                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Parameters.AddWithValue("@NumeroFactura", factura.getNumeroFactura());
                sqlCommand.Parameters.AddWithValue("@FechaFacturacion", factura.getFechaFacturacion());
                sqlCommand.Parameters.AddWithValue("@Total", factura.getTotal());
                sqlCommand.Parameters.AddWithValue("@Puntos", factura.getPuntos());
                sqlCommand.Parameters.AddWithValue("@TipoPago", factura.getTipoPago());
                sqlCommand.Parameters.AddWithValue("@IdEstadia", factura.getEstadia().getIdEstadia());
                sqlCommand.Parameters.AddWithValue("@IdReserva", factura.getReserva().getIdReserva());
                sqlCommand.Parameters.AddWithValue("@Titular", factura.getNombreTarjeta());
                sqlCommand.Parameters.AddWithValue("@NroTarjeta", factura.getNroTarjeta());
                sqlCommand.Parameters.AddWithValue("@CodSegTarjeta", factura.getCodSegTarjeta());
                sqlCommand.Parameters.AddWithValue("@VencTarjeta", factura.getVencTarjeta());

                StringBuilder sqlBuilder = new StringBuilder();
                sqlBuilder.Append(@"
                    BEGIN TRY
                    BEGIN TRANSACTION
                    INSERT INTO LOS_BORBOTONES.Factura(NumeroFactura,FechaFacturacion,Total,Puntos,TipoPago,idEstadia,idReserva,Titular,NroTarjeta,CodigoSeguridad,Vencimiento)
                    OUTPUT INSERTED.idFactura
                    VALUES(@NumeroFactura,@FechaFacturacion,@Total,@Puntos,@TipoPago,@IdEstadia,@IdReserva,@Titular,@NroTarjeta,@CodSegTarjeta,@VencTarjeta);
                    DECLARE @idFactura int;
                    SET @idFactura = SCOPE_IDENTITY();
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
                    idFactura = reader.GetInt32(reader.GetOrdinal("idFactura"));
                }

                sqlConnection.Close();
            }

            return idFactura;
        }


        override public void update(Factura factura)
        {
            if (this.exists(factura))
            {
                //Actualizo el registro
            }
            else
            {
                //Error
            }
        }

        override public void delete(Factura factura)
        {
            if (this.exists(factura))
            {
                //Borro el registro
            }
            else
            {
                //Error
            }
        }

        override public void bajaLogica(Factura factura)
        {
            throw new NotImplementedException();
        }

        override public Boolean exists(Factura factura)
        {
            int idFactura = 0;

            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            sqlCommand.Parameters.AddWithValue("@idFactura", factura.getIdFactura());
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT idFactura FROM LOS_BORBOTONES.Factura WHERE idFactura = @idFactura";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                idFactura = reader.GetInt32(reader.GetOrdinal("idFactura"));
            }

            sqlConnection.Close();

            //Devuelve verdadero si el ID coincide
            return idFactura != 0;
        }


        public int facturar(List<Estadia> estadias, List<ConsumibleParaMostrar> consumiblesXEstadia, String tipoPago,String nombreTarjeta,decimal nroTarjeta,int codSegTarjeta,int vencTarjeta)
        {
            int idFactura = 0;
            int resultado = 0;
            int numeroFactura = 0;
            RepositorioEstadia repoEstadia = new RepositorioEstadia();
            RepositorioReserva repoReserva = new RepositorioReserva();
            RepositorioItemFactura repoItemFactura = new RepositorioItemFactura();
            Estadia estadia=null;
            Reserva reserva = null;
            
            //suponemos que hay solo 1 estadia por estrategia enunciado, pero lo dejo aca como para hacer mas si necesito.
            estadia=estadias.First();

            //traigo la reserva y comparo los dias que sean los mismos los de la reserva que los que tuve efectivamente la estadia
            reserva = repoReserva.getIdByIdEstadia(estadia.getIdEstadia());

            List<ItemFactura> itemsFactura = new List<ItemFactura>();
            float total = 0;
            int puntos = 0;

            //float cant = 1;//pongo cant siempre en 1 ya que no marco mas que 1 solo consumible y lo mismo con la habitacion
            float monto = 0;//va a ser siempre el precio del cons ya que no marco mas que 1 solo consumible
            DateTime fecha = Utils.getSystemDatetimeNow();
            int idItemFactura = 0;
            //int idConsumible = 0;          
                  
            //traigo el numero de factura asi le sumo 1 que sera el nuevo.
            numeroFactura = getLastNumeroFactura() + 1 ;

            //traigo regimen para sumarlo o ver si es all inclusive
            RepositorioRegimen repoRegimen= new RepositorioRegimen();
            Regimen regimen = reserva.getRegimen();
            Boolean allInclusive = false;
            if (regimen.getCodigoRegimen().Equals("RGAI")) 
                allInclusive = true;

            //traigo el total
            //hago for each de los consumibles y sumo total y puntos
            ItemFactura itemFactura = null;
            float montoTotal = 0;

            foreach (ConsumibleParaMostrar cm in consumiblesXEstadia)
            {
                int cantidad = cm.getCantidad();
                
                //SI ES ALL INCLUSIVE NO SUMAN PARA EL TOTAL DE LA FACTURA
                if (!allInclusive)
                {
                    monto = cm.getConsumible().getPrecio();
                }
                else monto = 0;

                itemFactura = new ItemFactura(idItemFactura, cm.getConsumible().getIdConsumible(), cm.getCantidad(), monto, fecha, idFactura);
                itemsFactura.Add(itemFactura);
                montoTotal = monto * cm.getCantidad() + montoTotal;
            }             

            if (allInclusive)
            {
                //es all inclusive, hago un solo itemFactura all inclusive y no se cobra nada
                int idConsumible = 6;//poner el idConsumible del all inclusive
                monto = 0;
                //necesito un nuevo campo que sea descripcion por regimen de estadia
                itemFactura = new ItemFactura(idItemFactura, idConsumible, 1, monto, fecha, idFactura);
                itemsFactura.Add(itemFactura);
            }
            
            //sumo la habitacion x los dias alojados
            float montoHabitacion = 0;
            float totalHabitacion = 0;

            //conseguir el montoHabitacion de la reserva
            montoHabitacion = (float)repoReserva.getMonto(reserva);

            if( reserva.getDiasAlojados()==estadia.getCantidadNoches())
                {
                    float diasAlojados=(float)reserva.getDiasAlojados();
                    //se quedo toda la estadia hago solo un item factura con los dias x el monto
                    totalHabitacion = montoHabitacion * diasAlojados;
                    int idConsumible = 5; //con 5 marco que es la habitacion
                    itemFactura = new ItemFactura(idItemFactura, idConsumible, diasAlojados, montoHabitacion, fecha, idFactura);
                    itemsFactura.Add(itemFactura);
                
                }else
                {
                    //no se quedo toda la estadia hago un item factura con los diasAlojados x el monto y aparte un item por cada dia que quedo.
                
                    float diasAlojados = (float)estadia.getCantidadNoches();
                    float diasAlojadosTotal = (float)reserva.getDiasAlojados();
                
                totalHabitacion = montoHabitacion * diasAlojadosTotal;
                int idConsumible = 5; //con 5 marco que es la habitacion
                itemFactura = new ItemFactura(idItemFactura, idConsumible, diasAlojados, montoHabitacion, fecha, idFactura);
                itemsFactura.Add(itemFactura);

                float gap= (float)(reserva.getDiasAlojados() - estadia.getCantidadNoches());
                itemFactura = new ItemFactura(idItemFactura, idConsumible, gap, montoHabitacion, fecha, idFactura);
                itemsFactura.Add(itemFactura);
                }
                
            
            //sumo los puntos
            float dias = (float)reserva.getDiasAlojados();
            puntos = (int)(montoHabitacion * dias) / 20;//puntos de habitacion
            puntos = puntos + (int)(montoTotal / 10);//puntos de consumibles
            total = totalHabitacion + montoTotal;
            Factura factura = new Factura(idFactura,estadia,reserva,numeroFactura,fecha,total,puntos,tipoPago,itemsFactura,nombreTarjeta,nroTarjeta,codSegTarjeta,vencTarjeta);
            idFactura = this.create(factura);
            if (idFactura != 0)
            {
                foreach (ItemFactura item in itemsFactura)
                {
                    //hago el set de idFactura para el create
                    item.setIdFactura(idFactura);
                }
                repoItemFactura.createTodos(itemsFactura);
                //hacer update de la estadia avisando que ya facture
                repoEstadia.facturado(estadia.getIdEstadia());
                repoEstadia.updateEstadoFacturado(reserva.getIdReserva());
                resultado = 1;
            }
            else resultado = 2;//falla creando factura
            
            return resultado;
        }
        
        public int getLastNumeroFactura()
        {
            
            int numeroFactura=0;

            //Configuraciones de la consulta
            String connectionString = ConfigurationManager.AppSettings["BaseLocal"];
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader reader;

            //Primera Consulta
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT TOP 1 NumeroFactura FROM LOS_BORBOTONES.Factura order by NumeroFactura desc";

            sqlConnection.Open();

            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                numeroFactura = (int)reader.GetDecimal(reader.GetOrdinal("NumeroFactura"));
                
            }

            //Cierro Primera Consulta
            sqlConnection.Close();

            return numeroFactura;
        }

    }
}
