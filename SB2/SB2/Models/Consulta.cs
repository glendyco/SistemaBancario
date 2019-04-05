using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace SB2.Models
{
    public class Consulta
    {

        public Usuario Login(string username, string pass)
        {

            Usuario logged_usr = null;

            string conexion = "server=remotemysql.com;database=jOXtL7Pjql;uid=jOXtL7Pjql;pwd=ecjOPpQQ8e;";
            MySqlConnection conn = new MySqlConnection(conexion);
            conn.Open();

            string query = "Select * From USUARIO Where username = @username AND contrasena = @pass";

            MySqlCommand mycomand = new MySqlCommand(query, conn);
           mycomand.Parameters.AddWithValue("@username", username);
           mycomand.Parameters.AddWithValue("@pass", pass);


            MySqlDataReader myreader = mycomand.ExecuteReader();
           

           if (myreader.Read())
            {
                string usr_nick = myreader["username"].ToString();
                string usr_nam = myreader["nombre"].ToString();
                string usr_id = myreader["id_usuario"].ToString();

                logged_usr = new Usuario(usr_nam, usr_nick, usr_id);

            }
            myreader.Close();
            return logged_usr;
            
        }
    }
}