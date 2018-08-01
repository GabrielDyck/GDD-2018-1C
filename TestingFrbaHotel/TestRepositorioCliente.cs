using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FrbaHotel.Modelo;
using FrbaHotel.Repositorios;
using System.Collections.Generic;
using FrbaHotel.Excepciones;

namespace TestingFrbaHotel
{
    [TestClass]
    public class TestRepositorioCliente
    {
        [TestMethod]
        public void Test_Repo_Cliente_CreacionInstancia_Cliente()
        {
            RepositorioCliente repositorioCliente = new RepositorioCliente();
            Cliente dalia = repositorioCliente.getById(20990);

            //TEST BASICO
            Assert.IsTrue(dalia.getIdentidad().getNombre().Equals("DALIA"));
            Assert.IsTrue(dalia.getIdentidad().getApellido().Equals("Saavedra"));
            Assert.IsTrue(dalia.getIdentidad().getFechaNacimiento().Year.Equals(1953));
        }
    }
}
