using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


    namespace Logic
    {
        public class PublishersLog
        {
            PublishersDat objPub = new PublishersDat();


            public DataSet showEditorials()
            {
                return objPub.showEditorials();
            }
        public DataSet showEditorialsDDL()
        {
            return objPub.showEditorialsDDL();
        }


        // Método para guardar una nueva Editorial
        public bool saveEditorial(string _nombre, string _ciudad, string _telefono, string _correo)
            {
                return objPub.saveEditorial(_nombre, _ciudad, _telefono, _correo);
            }

            // Método para actualizar una Editorial existente
            public bool updateEditorial(int _idEditorial, string _nombre, string _ciudad, string _telefono, string _correo)
            {
                return objPub.updateEditorial(_idEditorial, _nombre, _ciudad, _telefono, _correo);
            }

            // Método para eliminar una Editorial por su ID
            public bool deleteEditorial(int _idEditorial)
            {
                return objPub.deleteEditorial(_idEditorial);
            }
        }
    }