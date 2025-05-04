using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Logic
{
    public class AuthorsLog
    {
        AuthorsDat objAut = new AuthorsDat(); // Crea una instancia de la clase AuthorsDat llamada objAut

        // Método que muestra todos los autores, retorna un DataSet.
        public DataSet showAuthors()
        {
            return objAut.showAuthors();
        }
        
        // Método para guardar un nuevo autor, recibe nombre, apellido y municipio
        public bool saveAuthor(string _nombre, string _apellido, string _municipio)
        {
            return objAut.saveAuthor(_nombre, _apellido, _municipio);
        }

        // Método para actualizar un autor existente, recibe id y los nuevos datos
        public bool updateAuthor(int _idAuthor, string _nombre, string _apellido, string _municipio)
        {
            return objAut.updateAuthor(_idAuthor, _nombre, _apellido, _municipio);
        }

        // Método para eliminar un autor por su ID
        public bool deleteAuthor(int _idAuthor)
        {
            return objAut.deleteAuthor(_idAuthor);
        }
    }
}