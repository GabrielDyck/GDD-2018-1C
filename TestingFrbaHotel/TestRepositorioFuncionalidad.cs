using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FrbaHotel.Modelo;
using FrbaHotel.Repositorios;
using System.Collections.Generic;
using FrbaHotel.Excepciones;

namespace TestingFrbaHotel
{
    [TestClass]
    public class TestRepositorioFuncionalidad
    {
        [TestMethod]
        [ExpectedException(typeof(NoExisteIDException), "No existe funcionalidad con el ID asociado")]
        public void Test_Repo_Funcionalidad_getById()
        {
            RepositorioFuncionalidad repositorioFuncionalidad = new RepositorioFuncionalidad();
            Funcionalidad funcionalidad = repositorioFuncionalidad.getById(50);
        }

        [TestMethod]
        public void Test_Repo_Funcionalidad_getAll() 
        {
            RepositorioFuncionalidad repositorioFuncionalidad = new RepositorioFuncionalidad();
            List<Funcionalidad> funcionalidades = repositorioFuncionalidad.getAll();

            Assert.AreEqual(11, funcionalidades.Count);
        }

        [TestMethod]
        public void Test_Repo_Funcionalidad_exists()
        {
            RepositorioFuncionalidad repositorioFuncionalidad = new RepositorioFuncionalidad();
            List<Funcionalidad> funcionalidades = repositorioFuncionalidad.getAll();

            Assert.IsTrue(funcionalidades.Exists(f => f.getDescripcion().Equals("ABMHotel")));

            Assert.IsFalse(funcionalidades.Exists(f => f.getDescripcion().Equals("")));
        }

        [TestMethod]
        [ExpectedException(typeof(NoExisteNombreException), "No existe funcionalidad con la Descripcion asociada")]
        public void Test_Repo_Funcionalidad_getByDescripcion()
        {
            RepositorioFuncionalidad repositorioFuncionalidad = new RepositorioFuncionalidad();
            Funcionalidad funcionalidad = repositorioFuncionalidad.getByDescripcion("ABMEmpleados");
        }
    }
}
