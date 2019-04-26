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

        public Usuario Login(string codigo, string username, string pass)
        {
           
                Usuario logged_usr = null;

                string conexion = "server=remotemysql.com;database=jOXtL7Pjql;uid=jOXtL7Pjql;pwd=ecjOPpQQ8e;";
                MySqlConnection conn = new MySqlConnection(conexion);
                conn.Open();

                string query = "Select * From USUARIO Where username = @username AND contrasena = @pass AND codigo=@codigo";

                MySqlCommand mycomand = new MySqlCommand(query, conn);
                mycomand.Parameters.AddWithValue("@username", username);
                mycomand.Parameters.AddWithValue("@pass", pass);
                mycomand.Parameters.AddWithValue("@codigo", codigo);


            MySqlDataReader myreader = mycomand.ExecuteReader();


                if (myreader.Read())
                {
                    string usr_nick = myreader["username"].ToString();
                    string usr_nam = myreader["nombre"].ToString();
                    string usr_id = myreader["id_usuario"].ToString();
                    string usr_cod = myreader["codigo"].ToString();
                    string usr_rol = myreader["id_rol"].ToString();

                    logged_usr = new Usuario(usr_nam, usr_nick, usr_id, usr_rol,usr_cod);

                }
                myreader.Close();
                return logged_usr;
            
        }

        public Usuario Login( string username, string pass)
        {

            Usuario logged_usr = null;

            string conexion = "server=remotemysql.com;database=jOXtL7Pjql;uid=jOXtL7Pjql;pwd=ecjOPpQQ8e;";
            MySqlConnection conn = new MySqlConnection(conexion);
            conn.Open();

            string query = "Select * From USUARIO Where username = @username AND contrasena = @pass ";

            MySqlCommand mycomand = new MySqlCommand(query, conn);
            mycomand.Parameters.AddWithValue("@username", username);
            mycomand.Parameters.AddWithValue("@pass", pass);
        


            MySqlDataReader myreader = mycomand.ExecuteReader();


            if (myreader.Read())
            {
                string usr_nick = myreader["username"].ToString();
                string usr_nam = myreader["nombre"].ToString();
                string usr_id = myreader["codigo"].ToString();
                string usr_rol = myreader["id_rol"].ToString();

                logged_usr = new Usuario(usr_nam, usr_nick, usr_id, usr_rol,usr_id);

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

        public List<Transaccion> VerHistorial(string idCuenta)
        {


            string conexion = "server=remotemysql.com;database=jOXtL7Pjql;uid=jOXtL7Pjql;pwd=ecjOPpQQ8e;";
            MySqlConnection conn = new MySqlConnection(conexion);
            conn.Open();

            string query = "SELECT * FROM HISTORIAL where cuenta_principal = @id ";


            MySqlCommand mycomand = new MySqlCommand(query, conn);
            mycomand.Parameters.AddWithValue("@id", idCuenta);

            MySqlDataReader myreader = mycomand.ExecuteReader();
            List<Transaccion> Lista = new List<Transaccion>();
            while (myreader.Read())
            {
                Transaccion transac = new Transaccion();
                transac.tipo = myreader["tipo"].ToString();
                transac.monto = myreader["monto"].ToString();
                transac.descripcion = myreader["descripcion"].ToString();
                transac.fecha = myreader["fecha"].ToString();
                transac.cuenta_tercero = myreader["cuenta_tercero"].ToString();
                Lista.Add(transac);

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
                 
            string conexion = "server=remotemysql.com;database=jOXtL7Pjql;uid=jOXtL7Pjql;pwd=ecjOPpQQ8e;";
            MySqlConnection conn = new MySqlConnection(conexion);
            try {
                conn.Open();

                string query = "UPDATE CUENTA SET SALDO = saldo + (SELECT monto from CREDITO WHERE id_credito= @id) WHERE id_cuenta =  (SELECT id_cuenta from CREDITO WHERE id_credito = @id)";

                MySqlCommand mycomand = new MySqlCommand(query, conn);
                mycomand.Parameters.AddWithValue("@id", id);

                int rowsaffected = mycomand.ExecuteNonQuery();

                conn.Close();
                HistorialCredito(id);
                return rowsaffected;
            } catch (Exception e)
            {
                return 0;
            }
            
            
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

        public int TransferenciaEjecutar(string origen, string destino, string monto, string descripcion) {

            //VERIFICAR SALDO SUFICIENTE
            if (ValidarSaldo(origen, monto) == 1)
            {
                //DEBITAR
                int deb = TransferenciaDebitar(origen, destino, monto);
                //DEBITO A HISTORIAL
                if (deb == 1)
                {
                    Historial(origen, destino, monto, descripcion, "Debito");

                    //ABONAR
                    int cred = TransferenciaAbonar(origen, destino, monto);
                    //CRÉDITO A HISTORIAL
                    if (cred == 1)
                    {
                        Historial(destino, origen, monto, descripcion, "Credito");
                        return 1;
                    }
                }

            }
            else {
                Console.WriteLine("saldo insuficiente");
            }

            return 0;
        }

        public int ValidarSaldo(string cuenta, string monto) {
            string conexion = "server=remotemysql.com;database=jOXtL7Pjql;uid=jOXtL7Pjql;pwd=ecjOPpQQ8e;";
            MySqlConnection conn = new MySqlConnection(conexion);
            try
            {
                conn.Open();
                string query = "SELECT SALDO FROM CUENTA WHERE  id_cuenta =  @cuenta";

                MySqlCommand mycomand = new MySqlCommand(query, conn);
                mycomand.Parameters.AddWithValue("@cuenta", cuenta);


                MySqlDataReader myreader = mycomand.ExecuteReader();

                string saldo = null;

                if (myreader.Read())
                {
                    saldo = myreader["saldo"].ToString();
                  
                }
                myreader.Close();
                conn.Close();

                if (saldo == null) return 0;

                if (Convert.ToDouble(saldo) >= Convert.ToDouble(monto)) {
                    return 1;
                }
                return 0;


            }
            catch (Exception e)
            {
                return 0;
            }
        }


        public string getSaldo(string cuenta)
        {
            string conexion = "server=remotemysql.com;database=jOXtL7Pjql;uid=jOXtL7Pjql;pwd=ecjOPpQQ8e;";
            MySqlConnection conn = new MySqlConnection(conexion);
            try
            {
                conn.Open();
                string query = "SELECT SALDO FROM CUENTA WHERE  id_cuenta =  @cuenta";

                MySqlCommand mycomand = new MySqlCommand(query, conn);
                mycomand.Parameters.AddWithValue("@cuenta", cuenta);


                MySqlDataReader myreader = mycomand.ExecuteReader();

                string saldo = null;

                if (myreader.Read())
                {
                    saldo = myreader["saldo"].ToString();

                }
                myreader.Close();
                conn.Close();

                return saldo;

            }
            catch (Exception e)
            {
                return null;
            }
        }

        public int TransferenciaAbonar(string origen, string destino, string monto) {

            string conexion = "server=remotemysql.com;database=jOXtL7Pjql;uid=jOXtL7Pjql;pwd=ecjOPpQQ8e;";
            MySqlConnection conn = new MySqlConnection(conexion);
            try
            {
                conn.Open();
                string query = "UPDATE CUENTA SET SALDO = saldo + @monto WHERE id_cuenta = @destino";

                MySqlCommand mycomand = new MySqlCommand(query, conn);
                mycomand.Parameters.AddWithValue("@monto", monto);
                mycomand.Parameters.AddWithValue("@destino", destino);

                int rowsaffected = mycomand.ExecuteNonQuery();

                conn.Close();

                if (rowsaffected == 1) { return 1; }
                return 0;
              
                
            }
            catch (Exception e) {
                return 0;
            }

        }
        public int TransferenciaDebitar(string origen, string destino, string monto)
        {
            string conexion = "server=remotemysql.com;database=jOXtL7Pjql;uid=jOXtL7Pjql;pwd=ecjOPpQQ8e;";
            MySqlConnection conn = new MySqlConnection(conexion);
            try
            {
                conn.Open();
                string query = "UPDATE CUENTA SET SALDO = saldo - @monto WHERE id_cuenta = @origen";

                MySqlCommand mycomand = new MySqlCommand(query, conn);
                mycomand.Parameters.AddWithValue("@monto", monto);
                mycomand.Parameters.AddWithValue("@origen", origen);

                int rowsaffected = mycomand.ExecuteNonQuery();

                conn.Close();

                if (rowsaffected == 1) { return 1; }
                return 0;
          

            }
            catch (Exception e)
            {
                return 0;
            }

        }

        public int Historial(string principal, string tercero, string monto, string descripcion, string tipo)
        {

            string conexion = "server=remotemysql.com;database=jOXtL7Pjql;uid=jOXtL7Pjql;pwd=ecjOPpQQ8e;";
            MySqlConnection conn = new MySqlConnection(conexion);
            try
            {
                conn.Open();
             
                string query = "INSERT INTO HISTORIAL(tipo, monto, descripcion, fecha, cuenta_principal, cuenta_tercero) " +
                                "VALUES (@tipo, @monto, @descripcion,@fecha, @principal, @tercero)";
                MySqlCommand mycomand = new MySqlCommand(query, conn);
                mycomand.Parameters.AddWithValue("@tipo", tipo);
                mycomand.Parameters.AddWithValue("@monto", monto);
                mycomand.Parameters.AddWithValue("@descripcion", descripcion);
                mycomand.Parameters.AddWithValue("@fecha", DateTime.Now);
                mycomand.Parameters.AddWithValue("@principal", principal);
                mycomand.Parameters.AddWithValue("@tercero", tercero);

                int rowsaffected = mycomand.ExecuteNonQuery();

                conn.Close();
                if (rowsaffected == 1) { return 1; }
                return 0;

            }
            catch (Exception e)
            {
                return 0;
            }

        }


        public int HistorialCredito(string id)
        {

            string conexion = "server=remotemysql.com;database=jOXtL7Pjql;uid=jOXtL7Pjql;pwd=ecjOPpQQ8e;";
            MySqlConnection conn = new MySqlConnection(conexion);
            try
            {
                conn.Open();

                string query = "SELECT * FROM CREDITO WHERE id_credito = @id";
                MySqlCommand mycomand = new MySqlCommand(query, conn);
                mycomand.Parameters.AddWithValue("@id", id);

                MySqlDataReader myreader = mycomand.ExecuteReader();

                string cuenta = null;
                string descripcion = null;
                string monto = null;

                if (myreader.Read())
                {   cuenta = myreader["id_cuenta"].ToString();
                    descripcion = myreader["descripcion"].ToString();
                    monto = myreader["monto"].ToString();
                }
                myreader.Close();
                conn.Close();

                if (cuenta != null && descripcion != null && monto != null) {
                    Historial(cuenta, "100100100",monto,"Crédito Aprobado: "+descripcion,"Crédito");
                }
                return 1;

            }
            catch (Exception e)
            {
                return 0;
            }

        }
    }
}