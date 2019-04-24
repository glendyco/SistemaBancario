using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
namespace SB2.Models
{
    public class Usuario
    {
       public string nombre, username, id_usuario, rol;
        public Consulta consulta = new Consulta();


        public Usuario ( string nom, string user, string id, string id_rol)
        {

            nombre = nom;
            username = user;
            id_usuario = id;
            rol = id_rol;
        }


        public bool isAdmin() {
            if (rol.Equals("1")) return true;
            return false;
        }


        

    }

}