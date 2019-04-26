using System;
using System.Collections.Generic;
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




        [TestMethod]
        public void CrearCuenta()
        {
            string idusuario = "2131807580101";
            int cuentacreada = 1;

            // Arrange
            Consulta controller = new Consulta();

            // Act
            int result = controller.CrearCuenta(idusuario);

            // Assert
            Assert.AreEqual(cuentacreada, result);

        }



        [TestMethod]
        public void getCuenta()
        {
            string idusuario = "2131807580122";
          

            // Arrange
            Consulta controller = new Consulta();

            // Act
            String result = controller.getCuenta(idusuario);

            // Assert
            Assert.AreEqual("SIN CUENTA BANCARIA", result);

        }




        [TestMethod]
        public void SolicitarCredito()
        {
            string monto = "8000";
            string descripcion = "Se solicita un credito de 8000 para compra de una moto.";
            string cuenta = "201025406";
          

            int registrado = 1;

            // Arrange
            Consulta controller = new Consulta();

            // Act
            int result = controller.SolicitarCredito(monto, descripcion, cuenta);

            // Assert
            Assert.AreEqual(registrado, result);

        }



        [TestMethod]
        public void VerSolicitudesCredito()
        {
            
            // Arrange
            Consulta controller = new Consulta();

            // Act
            List<SolicitudC> result = controller.VerSolicitudesCredito() as List<SolicitudC>;

            // Assert
            Assert.IsNotNull(result);

        }




        [TestMethod]
        public void AprobarCredito()
        {
            string id = "11";

            int aprobado = 1;

            // Arrange
            Consulta controller = new Consulta();

            // Act
            int result = controller.AprobarCredito(id);

            // Assert
            Assert.AreEqual(aprobado, result);

        }

        [TestMethod]
        public void AbonarCredito()
        {
            string id = "11";

            int abonado = 1;

            // Arrange
            Consulta controller = new Consulta();

            // Act
            int result = controller.AbonarCredito(id);

            // Assert
            Assert.AreEqual(abonado, result);

        }


        [TestMethod]
        public void RechazarCredito()
        {
            string id = "12";

            int rechazado = 1;

            // Arrange
            Consulta controller = new Consulta();

            // Act
            int result = controller.RechazarCredito(id);

            // Assert
            Assert.AreEqual(rechazado, result);

        }


        [TestMethod]
        public void TransferenciaEjecutar()
        {
            string origen = "201025409";
            string destino = "201011164";
            string monto = "1000";
            string descripcion = "Transferencia Monetaria";

            int ejecutado = 1;

            // Arrange
            Consulta controller = new Consulta();

            // Act
            int result = controller.TransferenciaEjecutar(origen,destino,monto,descripcion);

            // Assert
            Assert.AreEqual(ejecutado, result);

        }


        [TestMethod]
        public void ValidarSaldo()
        {
            string cuenta = "201011164";
            string monto = "10466.00";
         

            int validado = 1;

            // Arrange
            Consulta controller = new Consulta();

            // Act
            int result = controller.ValidarSaldo(cuenta,monto);

            // Assert
            Assert.AreEqual(validado, result);

        }




        [TestMethod]
        public void getSaldo()
        {
            string cuenta = "201025409";

            // Arrange
            Consulta controller = new Consulta();

            // Act
            string result = controller.getSaldo(cuenta);

            // Assert
            Assert.IsNotNull(result);

        }



        [TestMethod]
        public void TransferenciaAbonar()
        {
            string origen = "201011164";
            string destino = "201025409";
            string monto = "7000";
            
            int abonado = 1;

            // Arrange
            Consulta controller = new Consulta();

            // Act
            int result = controller.TransferenciaAbonar(origen, destino, monto);

            // Assert
            Assert.AreEqual(abonado, result);

        }

        [TestMethod]
        public void TransferenciaDebitar()
        {
            string origen = "201025406";
            string destino = "201025409";
            string monto = "200";

            int debitado = 1;

            // Arrange
            Consulta controller = new Consulta();

            // Act
            int result = controller.TransferenciaDebitar(origen, destino, monto);

            // Assert
            Assert.AreEqual(debitado, result);

        }


        [TestMethod]
        public void Historial()
        {
            string principal = "201025407";
            string tercero = "201025408";
            string monto = "400";
            string descripcion = "Transferencia Monetaria";
            string tipo = "Credito";

            int almacenado = 1;

            // Arrange
            Consulta controller = new Consulta();

            // Act
            int result = controller.Historial(principal, tercero, monto,descripcion,tipo);

            // Assert
            Assert.AreEqual(almacenado, result);

        }


        [TestMethod]
        public void HistorialCredito()
        {
            string id = "201025409";
   
            int historialcredito = 1;

            // Arrange
            Consulta controller = new Consulta();

            // Act
            int result = controller.HistorialCredito(id);

            // Assert
            Assert.AreEqual(historialcredito, result);

        }











    }
}
