using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SB2.Models;

namespace SB2.Tests.Models
{
    [TestClass]
    public class ConsultaTest
    {
        [TestMethod]
        public void Login()
        {
            string user = "glendyco";
            string pass = "hola123";
         
            // Arrange
            Consulta controller = new Consulta();

            // Act
            Usuario result = controller.Login(user,pass) as Usuario;

            // Assert
            Assert.IsNotNull(result);
         
        }


        [TestMethod]
        public void Registro()
        {
            string user = "Vigo4";
            string dpi = "123456799";
            string nombre = "Vigoberto Menchú";
            string correo = "vigo@gmail.com";
            string pass = "SoyunaPrueb4";

            int registrado = 1;

            // Arrange
            Consulta controller = new Consulta();

            // Act
            int result = controller.Registro(dpi,nombre,correo,user,pass);

            // Assert
            Assert.AreEqual(registrado,result);

        }
    }
}
