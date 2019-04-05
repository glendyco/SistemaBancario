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
    }
}
