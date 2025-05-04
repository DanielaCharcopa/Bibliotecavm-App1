using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Logic
{
    public class CategoryLog
    {
        CategoryDat objCat = new CategoryDat();

        // Metodo para mostrar todas las categorias
        public DataSet showCategory()
        {
            return objCat.showCategory();  //  nombre del método
        }
        // Metodo para mostrar  las categorias por ddl

        public DataSet showCategoryDDL()
        {
            return objCat.showCategoryDDL();  //  nombre del método
        }

        // Metodo para guardar una categoria

        public bool saveCategory(string _nombre, string _description)
        {
            return objCat.saveCategory(_nombre, _description);  //  nombre del método
        }

        // Metodo para actualizar categoria

        public bool updateCategory(int _idCategory, string _nombre, string _description)
        {
            return objCat.updateCategory(_idCategory, _nombre, _description);  //  nombre del método
        }
        // Metodo para eliminar categoria
        public bool deleteCategory(int _id)
        {
            return objCat.deleteCategory(_id);  //  nombre del método
        }
    }
}