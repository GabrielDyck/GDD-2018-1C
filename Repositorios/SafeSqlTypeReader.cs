using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using FrbaHotel.Commons;

namespace FrbaHotel.Modelo
{
    public static class SafeSqlTypeReader
    {
        //Metodo de extensión del SqlDataReader
        //Si es un campo String y hay null devuelvo String vacío
        public static string SafeGetString(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            return string.Empty;
        }

        //Metodo de extensión del SqlDataReader
        //Si es un campo DateTime y hay null devuelvo DateTime vacío
        public static DateTime SafeGetDateTime(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetDateTime(colIndex);
            return Utils.getSystemDatetimeNow();
        }

        //Metodo de extensión del SqlDataReader
        //Si es un campo Int32 y hay null devuelvo 0
        public static int SafeGetInt32(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetInt32(colIndex);
            return 0;
        }
    }
}
