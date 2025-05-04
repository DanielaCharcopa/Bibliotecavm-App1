using Data;
using System.Data;


namespace Logic
{
    public class SurveyLogic
    {
        SurveyDat objSurveyDat = new SurveyDat();

        // Método para mostrar todas las Encuestas
        public DataSet showSurveys()
        {
            return objSurveyDat.showSurveys();
        }

        // Método para mostrar únicamente el ID, la descripción de la pregunta (DDL), y el nombre completo del usuario
        public DataSet showSurveysDDL()
        {
            return objSurveyDat.showSurveysDDL();
        }


        // Método para guardar una nueva Encuesta
        public bool saveSurvey(string descripcionPregunta, int usuId)
        {
            return objSurveyDat.saveSurvey(descripcionPregunta, usuId); // Llamamos al método correcto en la capa de datos
        }

        // Método para actualizar una Encuesta
        public bool updateSurvey(int surveyId, string descripcionPregunta, int usuId)
        {
            return objSurveyDat.updateSurvey(surveyId, descripcionPregunta, usuId); // Llamamos al método correcto en la capa de datos
        }

        // Método para eliminar una Encuesta
        public bool deleteSurvey(int surveyId)
        {
            return objSurveyDat.deleteSurvey(surveyId);
        }
    }
}