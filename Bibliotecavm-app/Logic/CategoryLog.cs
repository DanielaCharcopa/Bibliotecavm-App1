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

        public DataSet showCategory()
        {
            return objCat.showCategory();  //  nombre del método
        }

        public bool saveCategory(string _nombre, string _description)
        {
            return objCat.saveCategory(_nombre, _description);  //  nombre del método
        }

        public bool updateCategory(int _idCategory, string _nombre, string _description)
        {
            return objCat.updateCategory(_idCategory, _nombre, _description);  //  nombre del método
        }

        public bool deleteCategory(int _id)
        {
            return objCat.deleteCategory(_id);  //  nombre del método
        }
    }
}