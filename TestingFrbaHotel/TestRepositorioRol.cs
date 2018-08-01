using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FrbaHotel.Modelo;
using FrbaHotel.Repositorios;
using System.Collections.Generic;
using FrbaHotel.Excepciones;

namespace TestingFrbaHotel
{
    [TestClass]
    public class TestRepositorioRol
    {
        [TestMethod]
        public void Test_Repo_Rol_CreacionInstancia_Rol()
        {
            RepositorioRol repositorioRol = new RepositorioRol();
            Rol rol = null;

            rol = repositorioRol.getByNombre("Recepcionista");
            Assert.AreEqual(6, rol.getFuncionalidades().Count);

            rol = repositorioRol.getByNombre("Guest");
            Assert.AreEqual(1, rol.getFuncionalidades().Count);
        }

        [TestMethod]
        [ExpectedException(typeof(NoExisteIDException), "No existe rol con el ID asociado")]
        public void Test_Repo_Rol_getByIdFalla()
        {
            RepositorioRol repositorioRol = new RepositorioRol();
            Rol rol = repositorioRol.getById(50);
        }

        [TestMethod]
        public void Test_Repo_Rol_getAll() 
        {
            RepositorioRol repositorioRol = new RepositorioRol();

            Assert.AreEqual(5, repositorioRol.getAll().Count);

            Assert.IsTrue(repositorioRol.getAll().Exists(r => r.getFuncionalidades().Exists(f => f.getDescripcion().Equals("ABMHotel"))));

            Assert.IsTrue(repositorioRol.getAll().Exists(r => r.getActivo().Equals(false)));

            Assert.IsTrue(repositorioRol.getAll().Exists(r => r.getNombre().Equals("Guest")));
        }

        [TestMethod]
        public void Test_Repo_Rol_exists()
        {
            RepositorioRol repositorioRol = new RepositorioRol();

            Assert.IsFalse(repositorioRol.exists(new Rol(50, "Dummy", false, null)));
        }

        [TestMethod]
        [ExpectedException(typeof(NoExisteNombreException), "No existe rol con el Nombre asociado")]
        public void Test_Repo_Rol_getByNameFalla()
        {
            RepositorioRol repositorioRol = new RepositorioRol();
            Rol rol = repositorioRol.getByNombre("RolFicticio");
        }

        [TestMethod]
        public void Test_Repo_Rol_getByQuery()
        {
            RepositorioRol repositorioRol = new RepositorioRol();

            //SIN FILTRO
            Assert.AreEqual(5, repositorioRol.getByQuery("", new KeyValuePair<String, Boolean>(), null).Count);

            //FILTRO ESTADO
            Assert.AreEqual(4, repositorioRol.getByQuery("", new KeyValuePair<String, Boolean>("", true), null).Count);

            //FILTRO NOMBRE Y ESTADO
            Assert.AreEqual(0, repositorioRol.getByQuery("Administrador", new KeyValuePair<String, Boolean>("", false), null).Count);

            //FALTA FILTRO FUNCIONALIDAD
        }

        [TestMethod]
        public void Test_Repo_Rol_Alta_Baja_Modificacion_Rol()
        {
            //INICIALIZO VARIABLES
            int idRolTest = 0;
            RepositorioRol repositorioRol = new RepositorioRol();
            List<Funcionalidad> funcionalidades = new List<Funcionalidad>();
            RepositorioFuncionalidad repositorioFuncionalidad = new RepositorioFuncionalidad();

            //INICIALIZO EL ROL A CREAR
            String nombreRol = "RolTest";
            Boolean activo = false;
            funcionalidades.Add(repositorioFuncionalidad.getByDescripcion("ABMRol"));
            funcionalidades.Add(repositorioFuncionalidad.getByDescripcion("ABMUsuario"));
            funcionalidades.Add(repositorioFuncionalidad.getByDescripcion("ABMHotel"));
            funcionalidades.Add(repositorioFuncionalidad.getByDescripcion("ABMRegimenEstadia"));
            Rol rolTest = new Rol(0, nombreRol, activo, funcionalidades);

            //DOY DE ALTA EL ROL            
            idRolTest = repositorioRol.create(rolTest);

            //RECUPERO EL ROL CREADO
            rolTest = repositorioRol.getById(idRolTest);

            //VALIDO
            //QUE AHORA HAY UN ROL MAS (SON 5 POR DEFAULT)
            Assert.AreEqual(6, repositorioRol.getAll().Count);
            //QUE TRAIGA LOS VALORES QUE CARGUE
            Assert.AreEqual(nombreRol, rolTest.getNombre());
            Assert.AreEqual(activo, rolTest.getActivo());
            Assert.AreEqual(4, rolTest.getFuncionalidades().Count);
            Assert.IsTrue(rolTest.getFuncionalidades().Exists(f => f.getDescripcion().Equals("ABMRol")));
            Assert.IsTrue(rolTest.getFuncionalidades().Exists(f => f.getDescripcion().Equals("ABMUsuario")));
            Assert.IsTrue(rolTest.getFuncionalidades().Exists(f => f.getDescripcion().Equals("ABMHotel")));
            Assert.IsTrue(rolTest.getFuncionalidades().Exists(f => f.getDescripcion().Equals("ABMRegimenEstadia")));

            //MODIFICACION DE ROL
            String nuevoNombre = "NuevoNombre";
            Boolean nuevoEstado = true;
            funcionalidades.Clear();
            funcionalidades.Add(repositorioFuncionalidad.getByDescripcion("ABMReserva"));
            funcionalidades.Add(repositorioFuncionalidad.getByDescripcion("ABMCliente"));
            funcionalidades.Add(repositorioFuncionalidad.getByDescripcion("ABMHabitacion"));
            rolTest.setNombre(nuevoNombre);
            rolTest.setActivo(nuevoEstado);
            rolTest.setFuncionalidades(funcionalidades);

            //ACTUALIZACION DEL ROL EN LA BASE
            repositorioRol.update(rolTest);

            //RECUPERO EL ROL ACTUALIZADO
            rolTest = repositorioRol.getById(idRolTest);

            //VALIDO
            //QUE LA CANTIDAD DE LOS ROLES SIGUE SIENDO LA MISMA
            Assert.AreEqual(6, repositorioRol.getAll().Count);
            //QUE EL NOMBRE CAMBIO
            Assert.AreEqual(nuevoNombre, rolTest.getNombre());
            //QUE EL ESTADO CAMBIO
            Assert.AreEqual(nuevoEstado, rolTest.getActivo());
            //QUE LAS FUNCIONALIDADES CAMBIARON
            Assert.AreEqual(3, rolTest.getFuncionalidades().Count);
            Assert.IsTrue(rolTest.getFuncionalidades().Exists(f => f.getDescripcion().Equals("ABMReserva")));
            Assert.IsTrue(rolTest.getFuncionalidades().Exists(f => f.getDescripcion().Equals("ABMCliente")));
            Assert.IsTrue(rolTest.getFuncionalidades().Exists(f => f.getDescripcion().Equals("ABMHabitacion")));

            //TESTEO LA BAJA LOGICA
            repositorioRol.bajaLogica(rolTest);
            rolTest = repositorioRol.getById(idRolTest);
            Assert.AreEqual(false, rolTest.getActivo());

            //ELIMINACION DE ROL PARA MANTENER CONSISTENCIA DE LA BASE Y LA COHERENCIA DE LOS TESTS
            repositorioRol.delete(rolTest);

            //VALIDO QUE LA CANTIDAD DE ROLES VUELVA A 5
            Assert.AreEqual(5, repositorioRol.getAll().Count);
        }
    }
}
