using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FrbaHotel.Modelo;
using FrbaHotel.Repositorios;
using System.Collections.Generic;
using FrbaHotel.Excepciones;

namespace TestingFrbaHotel
{
    [TestClass]
    public class TestRepositorioEstadia
    {
        [TestMethod]
        public void Test_Repo_Estadia_CreacionInstancia_Estadia()
        {
            RepositorioEstadia repositorioEstadia = new RepositorioEstadia();
            Estadia estadia = repositorioEstadia.getById(1);

            Assert.AreEqual(2, estadia.getUsuarioCheckIn().getIdUsuario());
            Assert.AreEqual(2, estadia.getUsuarioCheckOut().getIdUsuario());
        }
    }
}
