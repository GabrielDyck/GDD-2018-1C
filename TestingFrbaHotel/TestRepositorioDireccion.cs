using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FrbaHotel.Modelo;
using FrbaHotel.Repositorios;
namespace TestingFrbaHotel
{
    [TestClass]
    public class TestRepositorioDireccion
    {
        //LOS METODOS DE TEST QUE ESCRIBEN EN LA BASE DEBEN BORRAR LO QUE ESCRIBEN
        //PARA PROBAR LA INSTANCIACION DE UNA DIRECCION NO HACE FALTA CREAR UNA...
        //TENEMOS DE SOBRA EN LA BASE DE DATOS MIGRADA
        /*
        [TestMethod]
        public void Test_Repo_Direccion_Creacion_Direccion()
        {
            String ciudad = "BUE";
            String pais= "AR";
            String calle = "MEDRANO";
            int numeroCalle = 123;
            int piso =1;
            String departamento = "A";
            RepositorioDireccion repositorioDireccion = new RepositorioDireccion();
            Direccion direccion = new Direccion(0, pais, ciudad, calle, numeroCalle, piso, departamento);
            int idDireccionInserted = repositorioDireccion.create(direccion);
            Direccion direccionBuscada = repositorioDireccion.getById(idDireccionInserted);

            Assert.AreEqual(ciudad, direccionBuscada.getCiudad());
            Assert.AreEqual(pais, direccionBuscada.getPais());
            Assert.AreEqual(calle, direccionBuscada.getCalle());
            Assert.AreEqual(numeroCalle, direccionBuscada.getNumeroCalle());
            Assert.AreEqual(piso, direccionBuscada.getPiso());
            Assert.AreEqual(departamento, direccionBuscada.getDepartamento());
            Assert.IsTrue(direccionBuscada.getIdDireccion() !=0);


        }
        */
    }
}
