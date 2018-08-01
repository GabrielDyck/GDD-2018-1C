using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Configuration;
using System.Windows.Forms;


namespace FrbaHotel
{
    public class RepositorioListadoEstadistico
    {
        String connectionString = ConfigurationManager.AppSettings["BaseLocal"];

        public RepositorioListadoEstadistico() {}

        public DataTable getHotelesMayorCantidadReservasCanceladas(String trimestre, String anio)
        {
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand myCmd = new SqlCommand("LOS_BORBOTONES.lista_hoteles_maxResCancel", sqlConnection);
            myCmd.Parameters.AddWithValue("@trimestre", trimestre);
            myCmd.Parameters.AddWithValue("@anio", anio);
            myCmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(myCmd);
            da.Fill(dt);
            sqlConnection.Close();
            return dt;
        }

        public DataTable hotelesMayorCantidadConsumiblesFacturados(String trimestre, String anio)
        {
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand myCmd = new SqlCommand("LOS_BORBOTONES.lista_hoteles_maxConFacturados", sqlConnection);
            myCmd.Parameters.AddWithValue("@trimestre", trimestre);
            myCmd.Parameters.AddWithValue("@anio", anio);
            myCmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(myCmd);
            da.Fill(dt);
            sqlConnection.Close();
            return dt;
        }

        public DataTable hotelesMayorCantidadDiasFueraServicio(String trimestre, String anio)
        {
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand myCmd = new SqlCommand("LOS_BORBOTONES.lista_Hotel_DiasFueraServ", sqlConnection);
            myCmd.Parameters.AddWithValue("@trimestre", trimestre);
            myCmd.Parameters.AddWithValue("@anio", anio);
            myCmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(myCmd);
            da.Fill(dt);
            sqlConnection.Close();
            return dt;
        }

        public DataTable habitacionesMasOcupadas(String trimestre, String anio)
        {
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand myCmd = new SqlCommand("LOS_BORBOTONES.listaHabitacionesVecesOcupada", sqlConnection);
            myCmd.Parameters.AddWithValue("@trimestre", trimestre);
            myCmd.Parameters.AddWithValue("@anio", anio);
            myCmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(myCmd);
            da.Fill(dt);
            sqlConnection.Close();
            return dt;
        }

        public DataTable clientesConMasPuntos(String trimestre, String anio)
        {
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand myCmd = new SqlCommand("LOS_BORBOTONES.listaMaximosPuntajes", sqlConnection);
            myCmd.Parameters.AddWithValue("@trimestre", trimestre);
            myCmd.Parameters.AddWithValue("@anio", anio);
            myCmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(myCmd);
            da.Fill(dt);
            sqlConnection.Close();
            return dt;
        }

    }
}