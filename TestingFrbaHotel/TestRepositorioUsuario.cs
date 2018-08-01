using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FrbaHotel.Modelo;
using FrbaHotel.Repositorios;
using System.Collections.Generic;
using FrbaHotel.Excepciones;

namespace TestingFrbaHotel
{
    [TestClass]
    public class TestRepositorioUsuario
    {
        [TestMethod]
        public void Test_Repo_Usuario_CreacionInstancia_Usuario()
        {
            RepositorioUsuario repositorioUsuario = new RepositorioUsuario();
            Usuario admin = repositorioUsuario.getByUsername("admin");
            Usuario guest = repositorioUsuario.getByUsername("guest");

            Assert.IsTrue(admin.getActivo());
            Assert.IsTrue(guest.getActivo());

            Assert.AreEqual(0, admin.getIntentosFallidosLogin());
            Assert.AreEqual(0, guest.getIntentosFallidosLogin());

            Assert.IsTrue(admin.getRoles().Exists(rol => rol.getNombre().Equals("Administrador General")));
            Assert.IsTrue(guest.getRoles().Exists(rol => rol.getNombre().Equals("Guest")));

            Assert.IsTrue(admin.getIdentidad().getMail().Equals("admin@frba_utn.com"));
            Assert.IsTrue(guest.getIdentidad().getNumeroDocumento().Equals("1"));

            //FALTA VALIDAR QUE TRAIGA LOS HOTELES
            //Assert.AreEqual(5, admin.getHoteles().Count);
            //Assert.AreEqual(2, guest.getHoteles().Count);
        }

        [TestMethod]
        [ExpectedException(typeof(NoExisteIDException), "No existe usuario con el ID asociado")]
        public void Test_Repo_Usuario_getByIdFalla()
        {
            RepositorioUsuario repositorioUsuario = new RepositorioUsuario();
            Usuario usuario = repositorioUsuario.getById(50);
        }

        [TestMethod]
        public void Test_Repo_Usuario_getAll()
        {
            RepositorioUsuario repositorioUsuario = new RepositorioUsuario();
            List<Usuario> usuarios = repositorioUsuario.getAll();
            Assert.AreEqual(4, usuarios.Count);
        }

        [TestMethod]
        public void Test_Repo_Usuario_EncriptoBienPassAdmin()
        {
            RepositorioUsuario repoUsuario = new RepositorioUsuario();
            Usuario adminUteniano = repoUsuario.getByUsername("admin");
            String passwordEncriptadaSHA256 = adminUteniano.getPassword();
            String passwordPrueba = repoUsuario.EncriptarSHA256("w23e");
            Assert.AreEqual(passwordEncriptadaSHA256, passwordPrueba);
        }

        [TestMethod]
        [ExpectedException(typeof(NoExisteNombreException), "No existe usuario con el Nombre asociado")]
        public void Test_Repo_Usuario_getByUsernameFalla()
        {
            RepositorioUsuario repositorioUsuario = new RepositorioUsuario();
            Usuario admin = repositorioUsuario.getByUsername("Lanata");
        }
    }
}
