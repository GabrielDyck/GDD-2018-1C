using FrbaHotel.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingFrbaHotel.ModelBuilder
{
    class HotelBuilder
    {

        static String hotelNombre = "HotelTest";
        static String dirCiudad = "BUE";
        static String dirPais = "AR";
        static int categoriaEstrellas = 5;
        static String hotelMail = "test@gmail.com";
        static String hotelTelefono = "123123";
        static DateTime hotelFechaInicioDeActividad = DateTime.Now;
        static decimal categoriaRecargaEstrellas = 10;
        static String dirCalle = "Medrano";
        static int dirCalleNumero = 979;


        public static  Hotel buildHotel()
        {
            Categoria categoria1 = new Categoria(0, categoriaEstrellas, categoriaRecargaEstrellas);
            Direccion direccion1 = new Direccion(0, dirPais, dirCiudad, dirCalle, dirCalleNumero, 0, null);
            return new Hotel(0, categoria1, direccion1, hotelNombre, hotelMail, hotelTelefono, hotelFechaInicioDeActividad, null, null, null, null);

        }
    }
}
