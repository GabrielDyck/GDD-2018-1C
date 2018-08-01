using FrbaHotel.Excepciones;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrbaHotel.Commons
{
    public static class Utils
    {
        public static Object validateFields(Object field, String fieldName)
        {
            if (field == null)
            {
                throw new RequestInvalidoException(fieldName + " no puede ser nulo");
            }
            return field;
        }

        public static String validateStringFields(String field, String fieldName)
        {
            if (field == null || field == "")
            {
                throw new RequestInvalidoException(fieldName + " no puede ser nulo");
            }
            return field;
        }

        public static int validateIntField(String field, String fieldName)
        {
            if (field == null || field == "")
            {
                throw new RequestInvalidoException(fieldName + " no puede ser nulo");
            }
            try
            {
                return Int32.Parse(field);
            }
            catch (System.FormatException e)
            {
                throw new RequestInvalidoException(fieldName + " debe ser numerico: " + e.Message);
            }
        }

        public static void validateListField(DataGridViewSelectedRowCollection field, String fieldName)
        {
            if (field == null || field.Count ==0)
            {
                throw new RequestInvalidoException(fieldName + " no puede ser nulo o vacio");
            }
            
        }


        public static bool validateTimeRanges( DateTime fechaDesde ,DateTime fechaHasta){

            if (fechaDesde >= fechaHasta)
            {
                MessageBox.Show("La fecha desde no puede ser superior o igual a la fecha hasta", "Error");
                return false;
            }
            return true;
        }

        public static DateTime getSystemDatetimeNow() {

            //2018-06-01 00:00:00.000
            String fechaSistema = ConfigurationManager.AppSettings["FechaSistema"];
            return DateTime.ParseExact(fechaSistema, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
        }

    }
}
