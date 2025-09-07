using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Logic
{
    public class MaterialAutorLog
    {
        MaterialAutorDat objMat = new MaterialAutorDat();

        public DataSet showMaterialAutor()
        {
            return objMat.showMaterialAutor();  //  nombre del método
        }
        public DataSet showMaterialAutorReport()
        {
            return objMat.showMaterialAutorReport();  //  nombre del método
        }
        public DataSet showMaterialAutorDDL()
        {
            return objMat.showMaterialAutorDDL();  //  nombre del método
        }


        public bool saveMaterialAutor(int _idmaterial_edu, int id_autores, string _descripcion)
        {
            return objMat.saveMaterialAutor(_idmaterial_edu, id_autores, _descripcion);
        }

        public bool updateMaterialAutor(int _id, int _idmaterial_edu, int id_autores, string _descripcion)
        {
            return objMat.updateMaterialAutor(_id, _idmaterial_edu, id_autores, _descripcion);
        }
        public bool deleteMaterialAutor(int _id)
        {
            return objMat.deleteMaterialAutor(_id);

        }
    }
}