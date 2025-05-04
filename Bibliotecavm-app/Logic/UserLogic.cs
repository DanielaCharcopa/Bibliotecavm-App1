using Data;
using Model;
using System;
using System.Data;
using System.Diagnostics;

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
            // Validaciones básicas
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido) ||
                string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(contrasena))
            {
                throw new ArgumentException("Todos los campos obligatorios deben estar completos");
            }

            return objUserDat.saveUser(nombre, apellido, correo, contrasena, salt, rol, nivelEstudios);
        }

        // Método para actualizar un Usuario
        public bool updateUser(int idUser, string nombre, string apellido, string correo,
                             string contrasena, string salt, string rol, string nivelEstudios, string estado)
        {
            // Validaciones básicas
            if (idUser <= 0)
            {
                throw new ArgumentException("ID de usuario inválido");
            }

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido) ||
                string.IsNullOrWhiteSpace(correo))
            {
                throw new ArgumentException("Todos los campos obligatorios deben estar completos");
            }

            return objUserDat.updateUser(idUser, nombre, apellido, correo, contrasena, salt, rol, nivelEstudios, estado);
        }

        // Método para borrar un Usuario
        public bool deleteUser(int idUser)
        {
            if (idUser <= 0)
            {
                throw new ArgumentException("ID de usuario inválido");
            }

            // Verificar que no sea el último administrador
            if (IsLastAdmin(idUser))
            {
                throw new InvalidOperationException("No se puede eliminar el último administrador del sistema");
            }

            return objUserDat.deleteUser(idUser);
        }

        // Método para validar el login de usuario
        public User validateUserLogin(string correo, string contrasenaEncriptada)
        {
            // 1. Validación básica de campos
            if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(contrasenaEncriptada))
                throw new ArgumentException("Correo y contraseña son requeridos");

            // 2. Obtener usuario de la base de datos
            User objUser = objUserDat.showUsersMail(correo);

            // 3. Verificar si el usuario existe
            if (objUser == null)
                throw new Exception("Credenciales incorrectas");

            // 4. Verificar estado del usuario
            if (objUser.Estado != "Activo")
                throw new Exception("Usuario inactivo. Contacte al administrador.");

            // 5. Comparar contraseñas (ya viene encriptada desde presentación)
            if (objUser.Contrasena != contrasenaEncriptada)
                throw new Exception("Credenciales incorrectas");

            // 6. Si todo es correcto, devolver el usuario
            return objUser;
        }

        // NUEVO MÉTODO PARA OBTENER DATOS DE USUARIO (SOLO SALT Y ESTADO)
        public User GetUserForLogin(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
                throw new ArgumentException("El correo es requerido");

            return objUserDat.showUsersMail(correo);
        }



        // Método para verificar si un correo ya está registrado
        public bool isEmailRegistered(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
            {
                throw new ArgumentException("El correo no puede estar vacío");
            }

            return objUserDat.checkEmailExists(correo);
        }

        // Método para verificar si existe al menos un administrador
        public bool CheckAdminExists()
        {
            return objUserDat.AdminExists();
        }

        // Método para buscar usuarios por correo electrónico (coincidencia parcial)
        public DataSet SearchUsersByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("El término de búsqueda no puede estar vacío");
            }

            return objUserDat.SearchUsersByEmail(email);
        }

        // Método para obtener usuarios activos
        public DataSet GetActiveUsers()
        {
            return objUserDat.GetActiveUsers();
        }

        // Método para buscar usuarios por estado
        public DataSet SearchUsersByStatus(string email, string estado)
        {
            if (string.IsNullOrEmpty(estado))
            {
                throw new ArgumentException("El estado no puede estar vacío");
            }

            return objUserDat.SearchUsersByStatus(email, estado);
        }

        // Método para activar usuario
        public bool ActivateUser(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("ID de usuario inválido");
            }

            return objUserDat.ActivateUser(userId);
        }

        // Método para desactivar usuario
        public bool DeactivateUser(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("ID de usuario inválido");
            }

            // Verificar que no sea el último administrador
            if (IsLastAdmin(userId))
            {
                throw new InvalidOperationException("No se puede desactivar el último administrador del sistema");
            }

            return objUserDat.DeactivateUser(userId);
        }

        // Método privado para verificar si es el último administrador
        private bool IsLastAdmin(int userId)
        {
            // Obtener el usuario actual
            User user = objUserDat.showUsersMail(userId.ToString()); // Esto necesitaría ajustarse

            // Verificar si es administrador
            if (user?.Rol == "Administrador")
            {
                // Contar cuántos administradores activos hay
                DataSet admins = objUserDat.SearchUsersByStatus("", "Activo");
                int adminCount = admins.Tables[0].Select("usu_rol = 'Administrador'").Length;

                return adminCount <= 1;
            }

            return false;
        }
    }
}