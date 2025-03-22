using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Logic
{
    public class MaterialEducativoLog
    {
        MateriaEduDat objMatEdu = new MateriaEduDat();

        // Mostrar todos los registros de Material Educativo
        public DataSet showMaterialEdu()
        {
            return objMatEdu.showMaterialEdu();
        }

        // Insertar un nuevo material educativo
        public bool saveMaterialEducativo(string _titulo, int _anoPublicacion, string _urlDescarga, decimal _precio,
                                 string _keywords, string _formato, int _editorialId, int _categoriaId)
        {

            return objMatEdu.saveMaterialEducativo(_titulo, _anoPublicacion, _urlDescarga, _precio, _keywords, _formato,
                                          _editorialId, _categoriaId);
        }

        // Actualizar un material educativo
        public bool updateMaterialEducativo(int _idMaterial, string _titulo, int _anoPublicacion, string _urlDescarga,
                                   decimal _precio, string _keywords, string _formato, int _editorialId,
                                   int _categoriaId)
        {

            return objMatEdu.updateMaterialEducativo(_idMaterial, _titulo, _anoPublicacion, _urlDescarga, _precio,
                                            _keywords, _formato, _editorialId, _categoriaId);
        }
        public bool deleteMaterialEducativo(int _idMaterial)
        {

            return objMatEdu.deleteMaterialEducativo(_idMaterial);
        }
    }
}