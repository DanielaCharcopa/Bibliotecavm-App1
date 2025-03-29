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
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "procSelectUnansweredQuestionsByUser"; // Nombre del procedimiento almacenado
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

      //  Recupera las respuestas dadas por un usuario.

public DataSet showAnswersByUser(int userId)
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "procSelectAnswersByUser"; // Nombre del procedimiento almacenado
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

    }
}