using Data;
using System;
using System.Data;

namespace Logic
{
    public class AnswersLog
    {
        AnswerDat objAnswerDat = new AnswerDat();

        // Método para mostrar todas las respuestas (Administrador)
        public DataSet showAnswers()
        {
            return objAnswerDat.showAnswers();
        }

        // Método para guardar una nueva respuesta
        public bool saveAnswer(string respuesta, int questionId, int usuarioId)
        {
            // Validar que la respuesta sea "Sí" o "No"
            if (respuesta != "Sí" && respuesta != "No")
            {
                throw new ArgumentException("La respuesta debe ser 'Sí' o 'No'.");
            }

            return objAnswerDat.saveAnswer(respuesta, questionId, usuarioId);
        }

        // Método para actualizar una respuesta
        public bool updateAnswer(int answerId, string respuesta, int questionId, int usuarioId)
        {
            // Validar que la respuesta sea "Sí" o "No"
            if (respuesta != "Sí" && respuesta != "No")
            {
                throw new ArgumentException("La respuesta debe ser 'Sí' o 'No'.");
            }

            return objAnswerDat.updateAnswer(answerId, respuesta, questionId, usuarioId);
        }

        // Método para borrar una respuesta
        public bool deleteAnswer(int answerId, int questionId, int usuarioId)
        {
            return objAnswerDat.deleteAnswer(answerId, questionId, usuarioId);
        }

        // Método para mostrar preguntas no respondidas por el usuario
        public DataSet showUnansweredQuestionsByUser(int userId)
        {
            return objAnswerDat.showUnansweredQuestionsByUser(userId);
        }

        // Método para mostrar respuestas filtradas por usuario
        public DataSet showAnswersByUser(int userId)
        {
            return objAnswerDat.showAnswersByUser(userId);
        }
        public DataSet GetSurveyStatistics(int questionId)
        {
            try
            {
                return objAnswerDat.CountAnswersByQuestion(questionId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener estadísticas: " + ex.Message);
            }
        }

        // Método para obtener todas las respuestas 
        public DataTable GetAllSurveyQuestions()
        {
            try
            {
                DataSet ds = objAnswerDat.GetAllSurveyQuestions();
                return ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener preguntas: " + ex.Message);
            }
        }
    }
}