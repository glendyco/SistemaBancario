using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;


namespace SB2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = Metodo();
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            //ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult Registro()
        {
            //ViewBag.Message = "Your contact page.";
            return View();
        }

        [HttpPost]
        public ActionResult RegistroUsuario(string dpi, string nombre, string correo, string usuario, string pass)
        {
            
            Models.Consulta consulta = new Models.Consulta();
            int resultadoRegistro = consulta.Registro(dpi, nombre, correo, usuario, pass);


            if (resultadoRegistro == 1)
            {
                Console.WriteLine("USUARIO REGISTRADO");
                return View("Login");
            }
            else
            {
                ViewData["MensajeError"] = "Error en Registro";
                return View("Registro");
            }

        }



        [HttpPost]
        public ActionResult Autenticacion(string nick, string password)
        {
            Console.WriteLine("autenticando nick, pwd " + nick + "," + password);
            Models.Consulta consulta = new Models.Consulta();
            Models.Usuario usuario_logeado = consulta.Login(nick, password);


            if (usuario_logeado != null)
            {
                Console.WriteLine("USUARIO LOGUEADO ES NULO");
                return View("UserProfile", usuario_logeado);
            }

            ViewData["MensajeError"] = "Error en Autenticación";
            return View("Login");

            
        }


        public String Metodo()
        {
            string resultado = "nada";
            string connetionString = null;
            MySqlConnection cnn;
            connetionString = "server=remotemysql.com;database=jOXtL7Pjql;uid=jOXtL7Pjql;pwd=ecjOPpQQ8e;";
            cnn = new MySqlConnection(connetionString);
            try
            {
                cnn.Open();
                resultado = "Connection Open ! ";
                cnn.Close();
            }
            catch (Exception ex)
            {
                resultado = "Can not open connection !, Exception: "+ ex.Message; ;
            }
            return resultado;
        }
        

    }
}