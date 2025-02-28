using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Logic
{
    public class AnswersLog
    {
         AnswerDat objAnswerDat = new AnswerDat();

        // Método para mostrar todas las Respuestas
        public DataSet showAnswers()
        {
            return objAnswerDat.showAnswers();
        }

        // Método para guardar una nueva Respuesta
        public bool saveAnswer(string _respuesta, int _question_id, int _usu_id)
        {
            return objAnswerDat.saveAnswer(_respuesta, _question_id, _usu_id);
        }

        // Método para actualizar una Respuesta
        public bool updateAnswer(int _answer_id, string _respuesta, int _question_id, int _usu_id)
        {
            return objAnswerDat.updateAnswer(_answer_id, _respuesta, _question_id, _usu_id);
        }

        // Método para borrar una Respuesta
        public bool deleteAnswer(int _answer_id, int _question_id, int _usu_id)
        {
            return objAnswerDat.deleteAnswer(_answer_id, _question_id, _usu_id); // Llamamos al método correcto en la capa de datos
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
    }
}