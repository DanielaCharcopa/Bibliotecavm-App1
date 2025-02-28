using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Data
{
    public class Persistencia
    {
        MySqlConnection _connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString);

        // Método para abrir la conexión a la base de datos.
        // Devuelve un objeto MySqlConnection que representa la conexión establecida.
        public MySqlConnection openConnection()
        {

            try
            {
                // Abre la conexión.
                _connection.Open();
                return _connection;// Devuelve el objeto MySqlConnection.
            }
            catch (Exception e)
            {
                // En caso de error, se captura la excepción y se muestra la información de la excepción en la consola.
                e.ToString();
                return null;// Devuelve null para indicar que la conexión no se pudo abrir.
            }
        }

        // Método para cerrar la conexión a la base de datos.
        public void closeConnection()
        {
            _connection.Close();// Cierra la conexión.
        }
    }
}