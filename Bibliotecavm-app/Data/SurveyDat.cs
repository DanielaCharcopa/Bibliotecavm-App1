using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Data
{
    public class SurveyDat
    {
        Persistencia objPer = new Persistencia();

        //  Obtiene todas las encuestas registradas en la base de datos.


        public DataSet showSurveys()
        {
            DataSet objData = new DataSet();
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            MySqlCommand objSelectCmd = new MySqlCommand();

            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "procSelectSurvey"; // Procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();

            return objData;
        }


        // Recupera encuestas con sus nombres para su uso en listas desplegables.


        public DataSet showSurveysDDL()
        {
            DataSet objData = new DataSet();
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            MySqlCommand objSelectCmd = new MySqlCommand();

            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "procSelectSurveyDDL"; // Procedimiento almacenado que ahora incluye 'nombre'
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData); // Llenamos el DataSet con los datos obtenidos
            objPer.closeConnection();

            return objData;
        }

        //  Registra una nueva encuesta en la base de datos.

        public bool saveSurvey(string descripcionPregunta, int usuId)
        {
            bool executed = false;

            MySqlCommand objInsertCmd = new MySqlCommand();
            objInsertCmd.Connection = objPer.openConnection();
            objInsertCmd.CommandText = "procInsertSurvey"; // Procedimiento almacenado
            objInsertCmd.CommandType = CommandType.StoredProcedure;

            objInsertCmd.Parameters.Add("v_descripcion_pregunta", MySqlDbType.Text).Value = descripcionPregunta;
            objInsertCmd.Parameters.Add("v_usu_id", MySqlDbType.Int32).Value = usuId; // Agregar el parámetro faltante

            try
            {
                executed = objInsertCmd.ExecuteNonQuery() == 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                objPer.closeConnection();
            }

            return executed;
        }

        //  Modifica la información de una encuesta existente.  

        public bool updateSurvey(int surveyId, string descripcionPregunta, int usuId)
        {
            bool executed = false;

            MySqlCommand objUpdateCmd = new MySqlCommand();
            objUpdateCmd.Connection = objPer.openConnection();
            objUpdateCmd.CommandText = "procUpdateSurvey"; // Procedimiento almacenado
            objUpdateCmd.CommandType = CommandType.StoredProcedure;

            objUpdateCmd.Parameters.Add("v_en_id", MySqlDbType.Int32).Value = surveyId;
            objUpdateCmd.Parameters.Add("v_descripcion_pregunta", MySqlDbType.Text).Value = descripcionPregunta;
            objUpdateCmd.Parameters.Add("v_usu_id", MySqlDbType.Int32).Value = usuId; // Agregar el parámetro faltante

            try
            {
                executed = objUpdateCmd.ExecuteNonQuery() == 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                objPer.closeConnection();
            }

            return executed;
        }


        // Borra una encuesta de la base de datos.


        public bool deleteSurvey(int surveyId)
        {
            bool executed = false;

            MySqlCommand objDeleteCmd = new MySqlCommand();
            objDeleteCmd.Connection = objPer.openConnection();
            objDeleteCmd.CommandText = "procDeleteSurvey"; // Procedimiento almacenado
            objDeleteCmd.CommandType = CommandType.StoredProcedure;

            objDeleteCmd.Parameters.Add("v_en_id", MySqlDbType.Int32).Value = surveyId;

            try
            {
                executed = objDeleteCmd.ExecuteNonQuery() == 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                objPer.closeConnection();
            }

            return executed;
        }

    }
}