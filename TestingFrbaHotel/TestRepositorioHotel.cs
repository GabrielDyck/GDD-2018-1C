using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FrbaHotel.Modelo;
using FrbaHotel.Repositorios;
using System.Diagnostics;
using System.Collections.Generic;
using FrbaHotel.AbmHotel;
using TestingFrbaHotel.ModelBuilder;

namespace TestingFrbaHotel
{
    [TestClass]
    public class TestRepositorioHotel
    {
        [TestMethod]
        public void Test_Repo_Hotel_CreacionInstancia_Hotel()
        {
            RepositorioHotel repositorioHotel = new RepositorioHotel();
            Hotel hotel = repositorioHotel.getById(13);
            Direccion direccionHotel = hotel.getDireccion();
            Categoria categoriaHotel = hotel.getCategoria();

            Assert.AreEqual("Balcarce 2520", hotel.getNombre());
            Assert.AreEqual("No Posee", hotel.getMail());
            Assert.AreEqual("No Posee", hotel.getTelefono());
            Assert.AreEqual(2017, hotel.getFechaInicioActividades().Year);
            Assert.AreEqual(1, categoriaHotel.getEstrellas());
            Assert.AreEqual(10, categoriaHotel.getRecargaEstrellas());

            Assert.AreEqual("Argentina", direccionHotel.getPais());
            Assert.AreEqual("Bs. As. Oeste", direccionHotel.getCiudad());
            Assert.AreEqual("Balcarce", direccionHotel.getCalle());
            Assert.AreEqual(2520, direccionHotel.getNumeroCalle());
            Assert.AreEqual(0, direccionHotel.getPiso());
            Assert.AreEqual("", direccionHotel.getDepartamento());
        }

        //ESTE TEST TIENE QUE BORRAR LOS HOTELES QUE CREA...
        /*
        [TestMethod]
        public void Test_Repo_Hotel_Creacion_Hotel()
        {
            Hotel hotel1 = HotelBuilder.buildHotel();

            int savedHotelId= repositorioHotel.create(hotel1);

            Hotel hotelSearched= repositorioHotel.getById(savedHotelId);
            Assert.AreEqual(hotel1.Nombre, hotelSearched.getNombre());

            Assert.AreEqual(savedHotelId, hotelSearched.getIdHotel());
            Assert.AreEqual(hotel1.Mail, hotelSearched.getMail());
            Assert.AreEqual(hotel1.Telefono, hotelSearched.getTelefono());
            Debug.Assert(Math.Abs((hotel1.FechaInicioActividades - hotelSearched.getFechaInicioActividades()).TotalSeconds) < 1);
            Assert.AreEqual(hotel1.getCategoria().Estrellas, hotelSearched.getCategoria().getEstrellas());
            Assert.AreEqual(hotel1.getCategoria().RecargaEstrellas, hotelSearched.getCategoria().getRecargaEstrellas());
            Assert.AreEqual(hotel1.getDireccion().Pais, hotelSearched.getDireccion().getPais());
            Assert.AreEqual(hotel1.getDireccion().Ciudad, hotelSearched.getDireccion().getCiudad());
            Assert.AreEqual(hotel1.getDireccion().Calle, hotelSearched.getDireccion().getCalle());
            Assert.AreEqual(hotel1.getDireccion().NumeroCalle, hotelSearched.getDireccion().getNumeroCalle());
            Assert.AreEqual(0, hotelSearched.getDireccion().getPiso());


            //VALIDAR LISTA DE RESERVAS

            //VALIDAR LISTA DE CIERRES TEMPORALES

            //VALIDAR LISTA DE HABITACIONES

        }
        */

        //ESTE TEST TIENE QUE BORRAR LOS HOTELES QUE CREA
        /*
        [TestMethod]
        public void Test_Repo_Hotel_searchHotel()
        {
            Hotel hotelSaved = HotelBuilder.buildHotel();
            int savedHotelId = repositorioHotel.create(hotelSaved);

            //POR NOMBRE
            String hotNombre = hotelSaved.Nombre;
            List<Hotel> hoteles = repositorioHotel.getByQuery(hotNombre, null, null, null);
            Assert.IsTrue(hoteles.Count > 0);
            foreach(var hotel in hoteles){ Assert.IsTrue(hotel.Nombre.Equals(hotNombre));}

            //POR ESTRELLAS
            int categoriaEstrellas = hotelSaved.getCategoria().Estrellas;
            hoteles = repositorioHotel.getByQuery(null, categoriaEstrellas, null, null);
            Assert.IsTrue(hoteles.Count > 0);
            foreach (var hotel in hoteles) { Assert.IsTrue(hotel.getCategoria().Estrellas.Equals(categoriaEstrellas)); }

            //POR CIUDAD
            String dirCiudad = hotelSaved.getDireccion().Ciudad;

            hoteles = repositorioHotel.getByQuery(null, null, dirCiudad, null);
            Assert.IsTrue(hoteles.Count > 0);
            foreach (var hotel in hoteles) { Assert.IsTrue(hotel.getDireccion().Ciudad.Equals(dirCiudad)); }


            //POR PAIS
            String dirPais = hotelSaved.getDireccion().Pais;

            hoteles = repositorioHotel.getByQuery(null, null, null, dirPais);
            Assert.IsTrue(hoteles.Count > 0);
            foreach (var hotel in hoteles) { Assert.IsTrue(hotel.getDireccion().Pais.Equals(dirPais)); }


            //POR NOMBRE, CIUDAD, PAIS , ESTRELLAS
            hoteles = repositorioHotel.getByQuery(null, null, null, dirPais);
            Assert.IsTrue(hoteles.Count > 0);
            foreach (var hotel in hoteles) {
                Assert.IsTrue(hotel.Nombre.Equals(hotNombre));
                Assert.IsTrue(hotel.getCategoria().Estrellas.Equals(categoriaEstrellas));
                Assert.IsTrue(hotel.getDireccion().Pais.Equals(dirPais));
                Assert.IsTrue(hotel.getDireccion().Pais.Equals(dirPais));
            }


        }
        */

        //ESTE TEST NO TIENE QUE CREAR NINGUN HOTEL... CON LOS MIGRADOS ALCANZA
        /*
        [TestMethod]
        public void Test_Repo_Hotel_getAll()
        {
            //ESTE TEST DA TIMEOUT
            Hotel hotel = HotelBuilder.buildHotel();

            repositorioHotel.create(hotel);
            repositorioHotel.create(hotel);
            repositorioHotel.create(hotel);
            List<Hotel> hoteles = repositorioHotel.getAll();
            Assert.IsTrue(hoteles.Count > 2);
        }
        */

   
        //IDEM ARRIBA
        /*
        [TestMethod]
        public void Test_Repo_Hotel_exists_OK()
        {

            Hotel hotelSaved = HotelBuilder.buildHotel();
            int savedidHotel= repositorioHotel.create(hotelSaved);
            hotelSaved.IdHotel = savedidHotel;

            Assert.IsTrue(repositorioHotel.exists(hotelSaved));

        }
        */

        //NO HACE FALTA INSTANCIAR UN HOTEL PARA VERIFICAR QUE EL ID 999999999 no existe...
        /*
        [TestMethod]
        public void Test_Repo_Hotel_not_exists()
        {

            Hotel hotel = HotelBuilder.buildHotel();

            hotel.IdHotel = 99999999;
            Assert.IsFalse(repositorioHotel.exists(hotel));

        }
        */

        //LO QUE SE CREA EN EL TEST SE TIENE QUE BORRAR
        /*
        [TestMethod]
        public void Test_Repo_Hotel_crear_bajaTemporalSinReservasEnHotelOk()
        {
            Hotel hotel = HotelBuilder.buildHotel();
            int idsavedHotel= repositorioHotel.create(hotel);
            hotel = repositorioHotel.getById(idsavedHotel);


            Assert.IsTrue(hotel.getCierresTemporales().Count == 0);

            String descripcion = "CIERRE TEMPORAL DE TEST1";

            DateTime fechaInicio = DateTime.Now.AddDays(-3);
            DateTime fechaFin = DateTime.Now;
            int idHotel = hotel.getIdHotel();
            CierreTemporal request = new CierreTemporal(0, fechaInicio, fechaFin, descripcion,idHotel);
            repositorioHotel.crearBajaTemporal(request);
            repositorioHotel.crearBajaTemporal(request);
            Hotel hotelBuscadoConCierresTemporales = repositorioHotel.getById(idHotel);

            List<CierreTemporal> cierresTemporales = hotelBuscadoConCierresTemporales.getCierresTemporales();
            Assert.IsTrue(cierresTemporales.Count > 0);
            foreach (var cierre in cierresTemporales)
            {
                Assert.AreEqual(cierre.Descripcion, descripcion);
                Assert.AreEqual(cierre.IdHotel, idHotel);
            }
        }
        */
    }
}
