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
        public string Estado { get; set; } 

        // Constructor con parámetros (actualizado)
        public User(int usuId, string nombreCompleto, string correo, string contrasena, string salt, string rol, string estado)
        {
            UsuId = usuId;
            NombreCompleto = nombreCompleto;
            Correo = correo;
            Contrasena = contrasena;
            Salt = salt;
            Rol = rol;
            Estado = estado; // Añadido el parámetro estado
        }

        // Constructor vacío
        public User()
        {
            // Inicialización opcional de valores por defecto
            Estado = "Activo"; // Puedes establecer un valor por defecto si lo deseas
        }
    }
}