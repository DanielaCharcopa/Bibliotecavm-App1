using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model
{
    public class User
    {
        // Propiedades
        public int UsuId { get; set; }
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public string Salt { get; set; }
        public string Rol { get; set; }

        // Constructor con parámetros
        public User(int usuId, string nombreCompleto, string correo, string contrasena, string salt, string rol)
        {
            UsuId = usuId;
            NombreCompleto = nombreCompleto;
            Correo = correo;
            Contrasena = contrasena;
            Salt = salt;
            Rol = rol;
        }

        // Constructor vacío
        public User()
        {
        }
    }
}