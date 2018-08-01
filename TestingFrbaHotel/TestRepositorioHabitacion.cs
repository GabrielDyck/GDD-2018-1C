using Microsoft.VisualStudio.TestTools.UnitTesting;
using FrbaHotel.Repositorios;
using System;
using TestingFrbaHotel.ModelBuilder;
using FrbaHotel.Modelo;

namespace TestingFrbaHotel
{
    [TestClass]
    public class TestRepositorioHabitacion
    {

        //LO QUE SE CREA EN LOS TEST DEBE SER DADO DE BAJA DESPUES...
        //PARA PROBAR LA INSTANCIACION DE UNA HABITACION NO HACE FALTA CREAR UNA
        //SE PUEDE OBTENER UNA DE LAS YA EXISTENTES
        /*
        [TestMethod]
        public void Test_Repo_Habitacion_create_OK()
        {
            RepositorioHabitacion  repositorioHabitacion = new RepositorioHabitacion();
            RepositorioHotel repositorioHotel = new RepositorioHotel();
            RepositorioTipoHabitacion repositorioTipoHabitacion = new RepositorioTipoHabitacion();
            Hotel hotel = HotelBuilder.buildHotel();
            TipoHabitacion tipoHabitacion = TipoHabitacionBuilder.build();
            int idTipoHabitacion= repositorioTipoHabitacion.create(tipoHabitacion);
            tipoHabitacion.setIdTipoHabitacion(idTipoHabitacion);
            int idHotel = repositorioHotel.create(hotel);
            hotel.IdHotel = idHotel;
            Habitacion habitacion = HabitacionBuilder.buildHabitacion(tipoHabitacion, idHotel);

            int idHabitacion=repositorioHabitacion.create(habitacion);
            habitacion.setIdHabitacion(idHabitacion);
            Habitacion habitacionSearched = repositorioHabitacion.getById(idHabitacion);

            //ASSERT DE HABITACION
            Assert.AreEqual(habitacion.IdHabitacion, habitacionSearched.IdHabitacion);
            Assert.AreEqual(habitacion.Numero, habitacionSearched.Numero);
            Assert.AreEqual(habitacion.Piso, habitacionSearched.Piso);
            Assert.AreEqual(habitacion.Ubicacion, habitacionSearched.Ubicacion);
            Assert.AreEqual(habitacion.Activa, habitacionSearched.Activa);

            //ASSERT DE TIPO_DE_HABITACION

            Assert.AreEqual(habitacion.TipoHabitacion.Descripcion, habitacionSearched.TipoHabitacion.Descripcion);
            Assert.AreEqual(habitacion.TipoHabitacion.Codigo, habitacionSearched.TipoHabitacion.Codigo);
            Assert.AreEqual(habitacion.TipoHabitacion.Porcentual, habitacionSearched.TipoHabitacion.Porcentual);


            //ASSERT idHotel
            Assert.AreEqual(habitacion.getHotel().IdHotel, habitacionSearched.getHotel().IdHotel);

        }
        */

        //LO QUE SE CREA EN LOS TEST DEBE SER DADO DE BAJA DESPUES...
        //PARA PROBAR LA EXISTENCIA DE UNA HABITACION NO HACE FALTA CREAR UNA
        //SE PUEDE USAR UNA EXISTENTE
        /*
        [TestMethod]
        public void Test_Repo_Habitacion_exists_OK()
        {
            RepositorioHabitacion repositorioHabitacion = new RepositorioHabitacion();
            RepositorioHotel repositorioHotel = new RepositorioHotel();
            RepositorioTipoHabitacion repositorioTipoHabitacion = new RepositorioTipoHabitacion();
            Hotel hotel = HotelBuilder.buildHotel();
            TipoHabitacion tipoHabitacion = TipoHabitacionBuilder.build();
            int idTipoHabitacion = repositorioTipoHabitacion.create(tipoHabitacion);
            tipoHabitacion.setIdTipoHabitacion(idTipoHabitacion);
            int idHotel = repositorioHotel.create(hotel);
            hotel.IdHotel = idHotel;
            Habitacion habitacion = HabitacionBuilder.buildHabitacion(tipoHabitacion, idHotel);

            int idHabitacion = repositorioHabitacion.create(habitacion);
            habitacion.setIdHabitacion(idHabitacion);
            Habitacion habitacionSearched = repositorioHabitacion.getById(idHabitacion);

            Assert.IsTrue(repositorioHabitacion.exists(habitacionSearched));

        }
        */

        //LO QUE SE CREA EN LOS TEST DEBE SER DADO DE BAJA DESPUES...
        //PARA PROBAR LA BAJA LOGICA DE UNA HABITACION NO HACE FALTA CREAR UNA
        //SE PUEDE DAR DE BAJA UNA EXISTENTE Y LUEGO VOLVER A DARLA DE ALTA PARA NO 
        //CAMBIAR EL ESTADO DE LA BASE
        /*
        [TestMethod]
        public void Test_Repo_Habitacion_bajaLogica_OK()
        {
            RepositorioHabitacion repositorioHabitacion = new RepositorioHabitacion();
            RepositorioHotel repositorioHotel = new RepositorioHotel();
            RepositorioTipoHabitacion repositorioTipoHabitacion = new RepositorioTipoHabitacion();
            Hotel hotel = HotelBuilder.buildHotel();
            TipoHabitacion tipoHabitacion = TipoHabitacionBuilder.build();
            int idTipoHabitacion = repositorioTipoHabitacion.create(tipoHabitacion);
            tipoHabitacion.setIdTipoHabitacion(idTipoHabitacion);
            int idHotel = repositorioHotel.create(hotel);
            hotel.IdHotel = idHotel;
            Habitacion habitacion = HabitacionBuilder.buildHabitacion(tipoHabitacion, idHotel);

            int idHabitacion = repositorioHabitacion.create(habitacion);
            habitacion.setIdHabitacion(idHabitacion);
            Habitacion habitacionSearched = repositorioHabitacion.getById(idHabitacion);
            Assert.IsTrue(habitacionSearched.Activa);

            habitacionSearched.setActiva(false);
            repositorioHabitacion.bajaLogica(habitacionSearched);
            habitacionSearched = repositorioHabitacion.getById(idHabitacion);
            Assert.IsFalse(habitacionSearched.Activa);

            habitacionSearched.setActiva(true);
            repositorioHabitacion.bajaLogica(habitacionSearched);
            habitacionSearched = repositorioHabitacion.getById(idHabitacion);
            Assert.IsTrue(habitacionSearched.Activa);
        }
        */

    }
}
