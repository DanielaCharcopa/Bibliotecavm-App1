using Data;
using System;
using System.Data;

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

        // Eliminar un material educativo
        public bool deleteMaterialEducativo(int _idMaterial)
        {
            return objMatEdu.deleteMaterialEducativo(_idMaterial);
        }

        public void ActualizarDuracionVisita(int visitaId, string duracion)
        {
            try
            {
                // Actualizar la duración de la visita
                objMatEdu.ActualizarDuracionVisita(visitaId, duracion);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la duración de la visita: " + ex.Message);
            }
        }

    }
}