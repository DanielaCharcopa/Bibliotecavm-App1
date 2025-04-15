using Data;
using System;
using System.Data;

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
        public bool saveVisits(DateTime fechaIngreso, TimeSpan duracion, int usuId, int matId)
        {
            return objVis.saveVisits(fechaIngreso, duracion, usuId, matId); // Llamamos al método correcto en la capa de datos
        }

        // Método para actualizar una visita existente
        public bool updateVisits(int idVisits, DateTime fechaIngreso, TimeSpan duracion, int usuId, int matId)
        {
            return objVis.updateVisits(idVisits, fechaIngreso, duracion, usuId, matId); // Llamamos al método correcto en la capa de datos
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
        public DataSet ListarMaterialesEducativos()
        {
            return objVis.ListarMaterialesEducativos();
        }

        public int ObtenerUltimaVisitaId(int usuId, int matId)
        {
            VisitsDat datos = new VisitsDat();
            return datos.ObtenerUltimaVisitaId(usuId, matId);
        }

        public void ActualizarDuracionVisita(int visitaId, string duracion)
        {
            VisitsDat datos = new VisitsDat();
            datos.ActualizarDuracionVisita(visitaId, duracion);
        }

        // Método para buscar visitas por correo electrónico
        public DataSet searchVisitsByEmail(string email)
        {
            try
            {
                // Validación básica del parámetro
                if (string.IsNullOrWhiteSpace(email))
                {
                    throw new ArgumentException("El correo electrónico no puede estar vacío");
                }

                // Llamada al método de la capa de datos
                return objVis.searchVisitsByEmail(email);
            }
            catch (Exception ex)
            {
                // Aquí puedes manejar el error, registrarlo o relanzarlo según tu estrategia
                throw new Exception("Error al buscar visitas por correo: " + ex.Message);
            }
        }
        public DataSet SearchVisitsByDateRange(string email, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                // Validación básica de fechas
                if (startDate.HasValue && endDate.HasValue && startDate > endDate)
                {
                    throw new ArgumentException("La fecha de inicio no puede ser mayor a la fecha fin");
                }

                return objVis.SearchVisitsByDateRange(email, startDate, endDate);
            }
            catch (Exception ex)
            {
                // Manejo de errores (puedes personalizar esto)
                throw new Exception("Error al buscar visitas: " + ex.Message);
            }
        }
    }
}