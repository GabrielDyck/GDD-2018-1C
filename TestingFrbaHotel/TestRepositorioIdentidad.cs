using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FrbaHotel.Modelo;
using FrbaHotel.Repositorios;
using System.Collections.Generic;
using FrbaHotel.Excepciones;

namespace TestingFrbaHotel
{
    [TestClass]
    public class TestRepositorioIdentidad
    {
        [TestMethod]
        public void Test_Repo_Identidad_CreacionInstancia_Identidad()
        {
            RepositorioIdentidad repositorioIdentidad = new RepositorioIdentidad();
            Identidad identidadAdmin = repositorioIdentidad.getById(1);
            Identidad identidadUnCliente = repositorioIdentidad.getById(10);

            Assert.AreEqual("Usuario", identidadAdmin.getTipoIdentidad());
            Assert.AreEqual("Cliente", identidadUnCliente.getTipoIdentidad());

            Assert.AreEqual(1, identidadAdmin.getDirecciones().Count);
            Assert.AreEqual(1, identidadUnCliente.getDirecciones().Count);
        }

        [TestMethod]
        [ExpectedException(typeof(NoExisteIDException), "No existe identidad con el ID asociado")]
        public void Test_Repo_Identidad_getByIdFalla()
        {
            RepositorioIdentidad repositorioIdentidad = new RepositorioIdentidad();
            Identidad identidad = repositorioIdentidad.getById(999999999);
        }
    }
}
