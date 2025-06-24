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

        // Método para mostrar ID y nombre completo (DDL)
        public DataSet showUserDDL()
        {
            return objUserDat.showUsersDDL();
        }

        // Método para guardar un nuevo Usuario (actualizado con celular)
        public int saveUser(string nombre, string apellido, string correo, string contrasena,
                          string salt, string celular, string rol)
        {
            // Validación del número celular
            if (string.IsNullOrWhiteSpace(celular) || !IsValidColombianPhone(celular))
            {
                throw new ArgumentException("El número celular debe tener 10 dígitos y comenzar con 3");
            }

            // Verificar si el celular ya existe
            if (objUserDat.checkCelularExists(celular))
            {
                throw new ArgumentException("El número de celular ya está registrado");
            }

            return objUserDat.saveUser(nombre, apellido, correo, contrasena, salt, celular, rol);
        }

        // Método para actualizar un Usuario (actualizado con celular)
        public bool updateUser(int idUser, string nombre, string apellido, string correo,
                             string contrasena, string salt, string celular, string rol, string estado)
        {
            // Validación del número celular
            if (string.IsNullOrWhiteSpace(celular) || !IsValidColombianPhone(celular))
            {
                throw new ArgumentException("El número celular debe tener 10 dígitos y comenzar con 3");
            }

            // Verificar si el celular ya existe en otro usuario
            var user = objUserDat.showUsersMail(correo);
            if (user != null && user.UsuId != idUser && objUserDat.checkCelularExists(celular))
            {
                throw new ArgumentException("El número de celular ya está registrado en otro usuario");
            }

            return objUserDat.updateUser(idUser, nombre, apellido, correo, contrasena, salt, celular, rol, estado);
        }

        // Método para borrar un Usuario
        public bool deleteUser(int idUser)
        {
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

        // Método para obtener datos de usuario (solo salt y estado)
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

        // Método para verificar si un celular ya está registrado
        public bool isCelularRegistered(string celular)
        {
            if (string.IsNullOrWhiteSpace(celular) || !IsValidColombianPhone(celular))
            {
                throw new ArgumentException("Número celular inválido");
            }

            return objUserDat.checkCelularExists(celular);
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

        // Método para verificar si el correo electrónico existe
        public bool CheckEmailExists(string correo)
        {
            return objUserDat.checkEmailExists(correo);
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
            User user = objUserDat.showUsersMail(userId.ToString());

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

        // Método privado para validar formato de celular colombiano
        private bool IsValidColombianPhone(string celular)
        {
            if (string.IsNullOrWhiteSpace(celular) || celular.Length != 10)
                return false;

            if (!celular.StartsWith("3"))
                return false;

            return long.TryParse(celular, out _);
        }
    }
}