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
                resultado = "Can not open connection ! ";
            }
            return resultado;
        }
    }
}