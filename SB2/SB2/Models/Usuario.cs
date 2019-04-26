using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
namespace SB2.Models
{
    public class Usuario
    {
       public string nombre, username, id_usuario, rol,codigo;
        public Consulta consulta = new Consulta();


        public Usuario ( string nom, string user, string id, string id_rol,string codigo_usuario)
        {

            nombre = nom;
            username = user;
            id_usuario = id;
            rol = id_rol;
            codigo = codigo_usuario;
        }


        public bool isAdmin() {
            if (rol.Equals("1")) return true;
            return false;
        }

        public string getRol() {
            if (rol.Equals("1")) return "Administrador";
            return "Usuario";
        }


        public string getCuenta( )
        {
            return consulta.getCuenta(id_usuario);
        }


        

    }

}