using System;
using System.Collections;
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
                string usr_rol = myreader["id_rol"].ToString();

                logged_usr = new Usuario(usr_nam, usr_nick, usr_id,usr_rol);

            }
            myreader.Close();
            return logged_usr;
            
        }


        public int Registro (string dpi, string nombre, string correo, string username, string pass)
        {
            // devuelve 1 si el registro es exitoso
            // devuelve -1 en caso de fallo
            

            string conexion = "server=remotemysql.com;database=jOXtL7Pjql;uid=jOXtL7Pjql;pwd=ecjOPpQQ8e;";
            MySqlConnection conn = new MySqlConnection(conexion);
            conn.Open();

            string query = "INSERT INTO USUARIO(id_usuario,DPI, nombre, username, email, contrasena) " +
                "VALUES (@userid, @userdpi, @username,@usernick, @useremail, @userpass)";

            

            MySqlCommand mycomand = new MySqlCommand(query, conn);
            mycomand.Parameters.AddWithValue("@usernick", username);
            mycomand.Parameters.AddWithValue("@userid", dpi);
            mycomand.Parameters.AddWithValue("@userdpi", dpi);
            mycomand.Parameters.AddWithValue("@username", nombre);
            mycomand.Parameters.AddWithValue("@useremail", correo);
            mycomand.Parameters.AddWithValue("@userpass", pass);

            int rowsaffected= mycomand.ExecuteNonQuery();

            conn.Close();
            return rowsaffected;

        }


        public int CrearCuenta(string id_usuario) {
            string conexion = "server=remotemysql.com;database=jOXtL7Pjql;uid=jOXtL7Pjql;pwd=ecjOPpQQ8e;";
            MySqlConnection conn = new MySqlConnection(conexion);
            conn.Open();

            string query = "INSERT INTO CUENTA(saldo, id_usuario) " +
                "VALUES (0,@userid)";
            
            MySqlCommand mycomand = new MySqlCommand(query, conn);
        
            mycomand.Parameters.AddWithValue("@userid", id_usuario);
           
            int rowsaffected = mycomand.ExecuteNonQuery();

            conn.Close();
            return rowsaffected;
        }
        public string getCuenta(string id_Usuario) {
            string conexion = "server=remotemysql.com;database=jOXtL7Pjql;uid=jOXtL7Pjql;pwd=ecjOPpQQ8e;";
            MySqlConnection conn = new MySqlConnection(conexion);
            conn.Open();

            string query = "Select id_cuenta From CUENTA Where id_usuario = @id ";

            MySqlCommand mycomand = new MySqlCommand(query, conn);
            mycomand.Parameters.AddWithValue("@id", id_Usuario);
        

            MySqlDataReader myreader = mycomand.ExecuteReader();


            if (myreader.Read())
            {
                string id_cuenta = myreader["id_cuenta"].ToString();
                return id_cuenta;
            }
            myreader.Close();
            return "SIN CUENTA BANCARIA";
        }

        public int SolicitarCredito(string monto, string descripcion, string cuenta)
        {
            // devuelve 1 si el registro es exitoso
            // devuelve -1 en caso de fallo


            string conexion = "server=remotemysql.com;database=jOXtL7Pjql;uid=jOXtL7Pjql;pwd=ecjOPpQQ8e;";
            MySqlConnection conn = new MySqlConnection(conexion);
            conn.Open();

            string query = "INSERT INTO CREDITO(descripcion,id_cuenta, estado, monto)" +
                "VALUES (@descripcion, @cuenta, 'Pendiente', @monto)";



            MySqlCommand mycomand = new MySqlCommand(query, conn);
            mycomand.Parameters.AddWithValue("@descripcion", descripcion);
            mycomand.Parameters.AddWithValue("@cuenta", cuenta);
            mycomand.Parameters.AddWithValue("@monto", monto);
           

            int rowsaffected = mycomand.ExecuteNonQuery();

            conn.Close();
            return rowsaffected;

        }

        public List<SolicitudC> VerSolicitudesCredito()
        {
        

            string conexion = "server=remotemysql.com;database=jOXtL7Pjql;uid=jOXtL7Pjql;pwd=ecjOPpQQ8e;";
            MySqlConnection conn = new MySqlConnection(conexion);
            conn.Open();

            string query = "SELECT * FROM CREDITO where estado = 'Pendiente' ";


            MySqlCommand mycomand = new MySqlCommand(query, conn);

            MySqlDataReader myreader = mycomand.ExecuteReader();
            List<SolicitudC> Lista = new List<SolicitudC>();
            while (myreader.Read())
            {
                SolicitudC solic = new SolicitudC();
                solic.cuenta = myreader["id_cuenta"].ToString();
                solic.descripcion = myreader["descripcion"].ToString();
                solic.monto = myreader["monto"].ToString();
                solic.id = myreader["id_credito"].ToString();
                Lista.Add(solic);

                //string usr_nick = myreader["username"].ToString();
                Console.WriteLine(myreader["descripcion"].ToString());


            }
            myreader.Close();
            conn.Close();
            return Lista;

        }


        public int AprobarCredito(string id)
        {
            AbonarCredito(id);
            
            string conexion = "server=remotemysql.com;database=jOXtL7Pjql;uid=jOXtL7Pjql;pwd=ecjOPpQQ8e;";
            MySqlConnection conn = new MySqlConnection(conexion);
            conn.Open();

            string query = "UPDATE CREDITO SET estado = 'Aprobado' where id_credito = @id";

            MySqlCommand mycomand = new MySqlCommand(query, conn);
            mycomand.Parameters.AddWithValue("@id", id);

            int rowsaffected = mycomand.ExecuteNonQuery();

            conn.Close();


            return rowsaffected;
            

        }

        public int AbonarCredito(string id) {
            //UPDATE CUENTA SET SALDO = saldo + (SELECT monto from CREDITO WHERE id_credito= 6)
            // WHERE id_cuenta =  (SELECT id_cuenta from CREDITO WHERE id_credito = 6)

            string conexion = "server=remotemysql.com;database=jOXtL7Pjql;uid=jOXtL7Pjql;pwd=ecjOPpQQ8e;";
            MySqlConnection conn = new MySqlConnection(conexion);
            conn.Open();

            string query = "UPDATE CUENTA SET SALDO = saldo + (SELECT monto from CREDITO WHERE id_credito= @id) WHERE id_cuenta =  (SELECT id_cuenta from CREDITO WHERE id_credito = @id)";

            MySqlCommand mycomand = new MySqlCommand(query, conn);
            mycomand.Parameters.AddWithValue("@id", id);

            int rowsaffected = mycomand.ExecuteNonQuery();

            conn.Close();


            return rowsaffected;
        }

        public int RechazarCredito(string id)
        {


            string conexion = "server=remotemysql.com;database=jOXtL7Pjql;uid=jOXtL7Pjql;pwd=ecjOPpQQ8e;";
            MySqlConnection conn = new MySqlConnection(conexion);
            conn.Open();

            string query = "UPDATE CREDITO SET estado = 'Rechazado' where id_credito = @id";

            MySqlCommand mycomand = new MySqlCommand(query, conn);
            mycomand.Parameters.AddWithValue("@id", id);

            int rowsaffected = mycomand.ExecuteNonQuery();

            conn.Close();
            return rowsaffected;


        }
    }
}