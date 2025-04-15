using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Data
{
    public class AnswerDat
    {
        Persistencia objPer = new Persistencia();

        // Método para mostrar todas las respuestas
        public DataSet showAnswers()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "procSelectAnswer"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }
        // Guarda una respuesta en la base de datos utilizando un procedimiento almacenado.

        public bool saveAnswer(string _respuesta, int _question_id, int _usu_id)
        {
            bool executed = false;
            int row;
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "procInsertAnswer"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("v_res_respuesta", MySqlDbType.Text).Value = _respuesta; // Respuesta
            objSelectCmd.Parameters.Add("v_en_id", MySqlDbType.Int32).Value = _question_id; // ID de la encuesta
            objSelectCmd.Parameters.Add("v_usu_id", MySqlDbType.Int32).Value = _usu_id; // ID del usuario

            try
            {
                row = objSelectCmd.ExecuteNonQuery();
                if (row == 1)
                {
                    executed = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e.ToString());
            }
            objPer.closeConnection();
            return executed;
        }


        // Modifica una respuesta existente en la base de datos.

        public bool updateAnswer(int _answer_id, string _respuesta, int _question_id, int _usu_id)
        {
            bool executed = false;
            int row;
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "procUpdateAnswer"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("v_res_id", MySqlDbType.Int32).Value = _answer_id; // ID de la respuesta
            objSelectCmd.Parameters.Add("v_en_id", MySqlDbType.Int32).Value = _question_id; // ID de la encuesta
            objSelectCmd.Parameters.Add("v_usu_id", MySqlDbType.Int32).Value = _usu_id; // ID del usuario
            objSelectCmd.Parameters.Add("v_res_respuesta", MySqlDbType.Text).Value = _respuesta; // Nueva respuesta

            try
            {
                row = objSelectCmd.ExecuteNonQuery();
                if (row == 1)
                {
                    executed = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e.ToString());
            }
            objPer.closeConnection();
            return executed;
        }


        // Borra una respuesta de la base de datos.

        public bool deleteAnswer(int _answer_id, int _question_id, int _usu_id)
        {
            bool executed = false;
            int row;
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "procDeleteAnswer"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("v_res_id", MySqlDbType.Int32).Value = _answer_id; // ID de la respuesta
            objSelectCmd.Parameters.Add("v_en_id", MySqlDbType.Int32).Value = _question_id; // ID de la encuesta
            objSelectCmd.Parameters.Add("v_usu_id", MySqlDbType.Int32).Value = _usu_id; // ID del usuario

            try
            {
                row = objSelectCmd.ExecuteNonQuery();
                if (row == 1)
                {
                    executed = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e.ToString());
            }
            objPer.closeConnection();
            return executed;
        }


        // Obtiene las preguntas que un usuario aún no ha respondido.

        public DataSet showUnansweredQuestionsByUser(int userId)
        {
            DataSet ds = new DataSet();
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            MySqlCommand objSelectCmd = new MySqlCommand();

            try
            {
                objSelectCmd.Connection = objPer.openConnection();
                objSelectCmd.CommandText = "procSelectUnansweredQuestionsByUser"; // Nombre del SP
                objSelectCmd.CommandType = CommandType.StoredProcedure;
                objSelectCmd.Parameters.AddWithValue("v_usu_id", userId); // Parámetro del SP

                objAdapter.SelectCommand = objSelectCmd;
                objAdapter.Fill(ds); // Llena el DataSet

                // Debug: Verifica filas retornadas (opcional, para diagnóstico)
                Console.WriteLine($"Número de preguntas no respondidas: {ds.Tables[0].Rows.Count}");
            }
            catch (MySqlException ex)
            {
                // Captura errores específicos de MySQL (ej: SP no existe, sintaxis incorrecta)
                Console.WriteLine($"Error MySQL: {ex.Number} - {ex.Message}");
                throw new Exception("Error al cargar preguntas. Contacte al administrador.");
            }
            catch (Exception ex)
            {
                // Captura otros errores genéricos (ej: conexión fallida)
                Console.WriteLine($"Error inesperado: {ex.Message}");
                throw;
            }
            finally
            {
                objPer.closeConnection(); // Cierra la conexión siempre
            }

            return ds;
        }

        //  Recupera las respuestas dadas por un usuario.

        public DataSet showAnswersByUser(int userId)
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "procSelectAnswerByUser"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("v_user_id", MySqlDbType.Int32).Value = userId; // ID del usuario

            try
            {
                objAdapter.SelectCommand = objSelectCmd;
                objAdapter.Fill(objData);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.ToString());
            }
            finally
            {
                objPer.closeConnection();
            }

            return objData;
        }

        // Método para obtener el conteo de respuestas por pregunta
        public DataSet CountAnswersByQuestion(int questionId)
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            try
            {
                MySqlCommand objCmd = new MySqlCommand();
                objCmd.Connection = objPer.openConnection();
                objCmd.CommandText = "procCountAnswersByQuestion";
                objCmd.CommandType = CommandType.StoredProcedure;

                // Agregar parámetro de entrada
                objCmd.Parameters.AddWithValue("p_en_id", questionId);

                objAdapter.SelectCommand = objCmd;
                objAdapter.Fill(objData);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al contar respuestas: " + ex.Message);
            }
            finally
            {
                objPer.closeConnection();
            }

            return objData;
        }

        // Método para obtener todas las preguntas de encuestas
        public DataSet GetAllSurveyQuestions()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            try
            {
                MySqlCommand objCmd = new MySqlCommand();
                objCmd.Connection = objPer.openConnection();
                objCmd.CommandText = "procGetAllSurveyQuestions";
                objCmd.CommandType = CommandType.StoredProcedure;

                objAdapter.SelectCommand = objCmd;
                objAdapter.Fill(objData);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener preguntas de encuestas: " + ex.Message);
            }
            finally
            {
                objPer.closeConnection();
            }

            return objData;
        }

    }

}
