using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Logic
{
    public class VisitsLog
    {
        VisitsDat objVis = new VisitsDat();

        // Método para mostrar todas las visitas
        public DataSet showVisits()
        {
            return objVis.showVisits();
        }

        // Método para guardar una nueva visita
        public bool saveVisits(DateTime fechaIngreso, TimeSpan duracion, string dispositivo, int usuId, int matId)
        {
            return objVis.saveVisits(fechaIngreso, duracion, dispositivo, usuId, matId); // Llamamos al método correcto en la capa de datos
        }

        // Método para actualizar una visita existente
        public bool updateVisits(int idVisits, DateTime fechaIngreso, TimeSpan duracion, string dispositivo, int usuId, int matId)
        {
            return objVis.updateVisits(idVisits, fechaIngreso, duracion, dispositivo, usuId, matId); // Llamamos al método correcto en la capa de datos
        }

        // Método para eliminar una visita
        public bool deleteVisits(int idVisits)
        {
            return objVis.deleteVisits(idVisits); // Llamamos al método correcto en la capa de datos
        }

        // Método para contar el total de visitas
        public int countTotalVisits()
        {
            return objVis.countTotalVisits();
        }

        // Método para contar visitas por docente
        public int countVisitsByTeacher()
        {
            return objVis.countVisitsByTeacher();
        }

        // Método para contar visitas por estudiante
        public int countVisitsByStudent()
        {
            return objVis.countVisitsByStudent();
        }

        // Método para obtener estadísticas de materiales y visitas
        public DataSet GetMaterialAndVisitStats()
        {
            return objVis.GetMaterialAndVisitStats();
        }

        // Método para obtener los materiales más visitados
        public DataSet GetMostVisitedMaterials()
        {
            return objVis.GetMostVisitedMaterials();
        }

        // Método para obtener visitas por usuario logueado
        public DataSet GetVisitsByUser(int userId)
        {
            return objVis.GetVisitsByUser(userId);
        }
    }
}