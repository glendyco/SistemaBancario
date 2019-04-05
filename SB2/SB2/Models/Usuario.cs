using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
namespace SB2.Models
{
    public class Usuario
    {
       public string nombre, username, id_usuario;


        public Usuario ( string nom, string user, string id)
        {

            nombre = nom;
            username = user;
            id_usuario = id;
        }


        

    }

}