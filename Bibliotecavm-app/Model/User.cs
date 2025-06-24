namespace Model
{
    public class User
    {
        // Propiedades
        public int UsuId { get; set; }
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }
        public string Celular { get; set; } // Nueva propiedad para el número celular
        public string Contrasena { get; set; }
        public string Salt { get; set; }
        public string Rol { get; set; }
        public string Estado { get; set; }

        // Constructor con parámetros (actualizado con celular)
        public User(int usuId, string nombreCompleto, string correo, string celular,
                   string contrasena, string salt, string rol, string estado)
        {
            UsuId = usuId;
            NombreCompleto = nombreCompleto;
            Correo = correo;
            Celular = celular;
            Contrasena = contrasena;
            Salt = salt;
            Rol = rol;
            Estado = estado;
        }

        // Constructor vacío (actualizado con valor por defecto para celular)
        public User()
        {
            // Valores por defecto
            Estado = "Activo";
            Celular = "3000000000"; // Valor por defecto temporal para Colombia
        }

        // Método ToString opcional (puedes personalizarlo)
        public override string ToString()
        {
            return $"{NombreCompleto} ({Correo}) - Tel: {Celular}";
        }
    }
}