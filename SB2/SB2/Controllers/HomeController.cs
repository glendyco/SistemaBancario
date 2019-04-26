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

        public ActionResult UserProfile()
        {
            //ViewBag.Message = "Your contact page.";
            if (Session["LoggedUser"] != null)
            {
                return View("UserProfile", Session["LoggedUser"]);
            }
            return View("Login");
        }

        public ActionResult EstadoCuenta()
        {
            //ViewBag.Message = "Your contact page.";
            return View("EstadoCuenta", Session["LoggedUser"]);
        }

        public ActionResult Logout()
        {
            //ViewBag.Message = "Your contact page.";
            Session["LoggedUser"] = null;
            return View("Login");
        }



        [HttpPost]
        public ActionResult RegistroUsuario(string dpi, string nombre, string correo, string usuario, string pass)
        {
            
            Models.Consulta consulta = new Models.Consulta();
            int resultadoRegistro = consulta.Registro(dpi, nombre, correo, usuario, pass);


            if (resultadoRegistro == 1)
            {
                Console.WriteLine("USUARIO REGISTRADO");
            
                Models.Usuario usuario_logeado = consulta.Login(usuario, pass);
               
                if (usuario_logeado != null)
                {
                    // Console.WriteLine("USUARIO LOGUEADO ES NULO");
                    usuario_logeado.consulta.CrearCuenta(usuario_logeado.id_usuario);
                    Session["LoggedUser"] = usuario_logeado;
                    Session["SolicitudesC"] = consulta.VerSolicitudesCredito();
                    return View("UserProfile", usuario_logeado);
                }
               

            }
           

                ViewData["MensajeError"] = "Error en Registro";
                return View("Registro");
            


        }


        [HttpPost]
        public ActionResult SolicitarCredito(string monto, string descripcion, string cuenta)
        {

            Models.Consulta consulta = new Models.Consulta();
         
            int resultadoRegistro = consulta.SolicitarCredito(monto, descripcion, cuenta);


            if (resultadoRegistro == 1)
            {
                Console.WriteLine("CREDITO EN PROCESO");
                return View("UserProfile",(Models.Usuario) Session["LoggedUser"] );
            }
            else
            {
                ViewData["MensajeError"] = "Error en Registro";
                return View("UserProfile", (Models.Usuario)Session["LoggedUser"]);
            }

        }


        [HttpPost]
        public ActionResult AprobarCredito(string id_Solicitud)
        {

            Models.Consulta consulta = new Models.Consulta();

            int resultadoRegistro = consulta.AprobarCredito(id_Solicitud);


            if (resultadoRegistro == 1)
            {
                
                return View("UserProfile", (Models.Usuario)Session["LoggedUser"]);
            }
            else
            {
               
                return View("UserProfile", (Models.Usuario)Session["LoggedUser"]);
            }

        }

        [HttpPost]
        public ActionResult RechazarCredito(string id_Solicitud)
        {

            Models.Consulta consulta = new Models.Consulta();

            int resultadoRegistro = consulta.RechazarCredito(id_Solicitud);


            if (resultadoRegistro == 1)
            {

                return View("UserProfile", (Models.Usuario)Session["LoggedUser"]);
            }
            else
            {

                return View("UserProfile", (Models.Usuario)Session["LoggedUser"]);
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
                // Console.WriteLine("USUARIO LOGUEADO ES NULO");
                
                Session["LoggedUser"] = usuario_logeado;
                Session["SolicitudesC"] = consulta.VerSolicitudesCredito();
                return View("UserProfile", usuario_logeado);
            }

            ViewData["MensajeError"] = "Error en Autenticación";
            return View("Login");

            
        }



        [HttpPost]
        public ActionResult Transferencia(string origen, string destino, string monto)
        {

            Models.Consulta consulta = new Models.Consulta();

            int resultadoRegistro = consulta.TransferenciaEjecutar(origen, destino, monto, "Transferencia Monetaria" );


            if (resultadoRegistro == 1)
            {
                Console.WriteLine("TRANSFERENCIA REALIZADA");
                return View("UserProfile", (Models.Usuario)Session["LoggedUser"]);
            }
            else
            {
                ViewData["MensajeError"] = "Error en Registro";
                return View("UserProfile", (Models.Usuario)Session["LoggedUser"]);
            }

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