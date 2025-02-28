using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace Logic
{
    public class UserLogic
    {
        UserDat objUserDat = new UserDat();

        // Método para mostrar todos los Usuarios
        public DataSet showUsers()
        {
            return objUserDat.showUsers();
        }

        // Método para mostrar solamente el ID y el nombre completo de los Usuarios (DDL)
        public DataSet showUserDDL()
        {
            return objUserDat.showUsersDDL();
        }

        // Método para guardar un nuevo Usuario
        public bool saveUser(string nombre, string apellido, string correo, string contrasena, string salt, string rol, string nivelEstudios)
        {
            return objUserDat.saveUser(nombre, apellido, correo, contrasena, salt, rol, nivelEstudios);
        }

        // Método para actualizar un Usuario
        public bool updateUser(int idUser, string nombre, string apellido, string correo, string contrasena, string salt, string rol, string nivelEstudios)
        {
            return objUserDat.updateUser(idUser, nombre, apellido, correo, contrasena, salt, rol, nivelEstudios);
        }

        // Método para borrar un Usuario
        public bool deleteUser(int idUser)
        {
            return objUserDat.deleteUser(idUser);
        }

        // Método para validar el login de usuario
        public DataSet validateUserLogin(string correo, string contrasena)
        {
            return objUserDat.validateUserLogin(correo, contrasena);
        }

        // Método para verificar si un correo ya está registrado
        public bool isEmailRegistered(string correo)
        {
            return objUserDat.checkEmailExists(correo); // Llamamos al método correcto en la capa de datos
        }

        // Método para verificar si existe al menos un administrador
        public bool CheckAdminExists()
        {
            return objUserDat.AdminExists(); // Llamamos al método en la capa de datos
        }
    }
}