using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Data
{
    public class VisitsDat
    {

        Persistencia objPer = new Persistencia();

        // Mostrar todas las visitas
        public DataSet showVisits()
        {
            DataSet objData = new DataSet();
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            MySqlCommand objSelectCmd = new MySqlCommand();

            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "procSelectVisits"; // Procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();

            return objData;
        }
        //  Registra una nueva visita en la base de datos.

        public bool saveVisits(DateTime fechaIngreso, TimeSpan duracion, string dispositivo, int usuId, int matId)
        {
            bool executed = false;

            MySqlCommand objInsertCmd = new MySqlCommand();
            objInsertCmd.Connection = objPer.openConnection();
            objInsertCmd.CommandText = "procInsertVisits"; // Procedimiento almacenado
            objInsertCmd.CommandType = CommandType.StoredProcedure;

            objInsertCmd.Parameters.Add("v_fecha_ingreso", MySqlDbType.Date).Value = fechaIngreso;
            objInsertCmd.Parameters.Add("v_duracion", MySqlDbType.Time).Value = duracion;
            objInsertCmd.Parameters.Add("v_dispositivo", MySqlDbType.String).Value = dispositivo; // Cambiar a String para ENUM
            objInsertCmd.Parameters.Add("v_usu_id", MySqlDbType.Int32).Value = usuId;
            objInsertCmd.Parameters.Add("v_mat_id", MySqlDbType.Int32).Value = matId; // Agregar el parámetro faltante

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

        //  Modifica la información de una visita existente.

        public bool updateVisits(int idVisits, DateTime fechaIngreso, TimeSpan duracion, string dispositivo, int usuId, int matId)
        {
            bool executed = false;

            MySqlCommand objUpdateCmd = new MySqlCommand();
            objUpdateCmd.Connection = objPer.openConnection();
            objUpdateCmd.CommandText = "procUpdateVisits"; // Procedimiento almacenado
            objUpdateCmd.CommandType = CommandType.StoredProcedure;

            objUpdateCmd.Parameters.Add("v_vis_id", MySqlDbType.Int32).Value = idVisits;
            objUpdateCmd.Parameters.Add("v_fecha_ingreso", MySqlDbType.Date).Value = fechaIngreso;
            objUpdateCmd.Parameters.Add("v_duracion", MySqlDbType.Time).Value = duracion;
            objUpdateCmd.Parameters.Add("v_dispositivo", MySqlDbType.String).Value = dispositivo; // Cambiar a String para ENUM
            objUpdateCmd.Parameters.Add("v_usu_id", MySqlDbType.Int32).Value = usuId;
            objUpdateCmd.Parameters.Add("v_mat_id", MySqlDbType.Int32).Value = matId; // Agregar el parámetro faltante

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

        //  Borra una visita de la base de datos.

        public bool deleteVisits(int idVisits)
        {
            bool executed = false;

            MySqlCommand objDeleteCmd = new MySqlCommand();
            objDeleteCmd.Connection = objPer.openConnection();
            objDeleteCmd.CommandText = "procDeleteVisits"; // Procedimiento almacenado
            objDeleteCmd.CommandType = CommandType.StoredProcedure;

            objDeleteCmd.Parameters.Add("v_vis_id", MySqlDbType.Int32).Value = idVisits;

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

        //  Obtiene todas las visitas registradas en la base de datos.

        public DataSet showvisits()
        {
            DataSet objData = new DataSet();
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            MySqlCommand objSelectCmd = new MySqlCommand();

            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "procSelectVisits"; // Procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();

            return objData;
        }

        // Devuelve el número total de visitas registradas.

        public int countTotalVisits()
        {
            int totalVisits = 0;

            MySqlCommand objCountCmd = new MySqlCommand();
            objCountCmd.Connection = objPer.openConnection();
            objCountCmd.CommandText = "procCountVisits"; // Procedimiento almacenado
            objCountCmd.CommandType = CommandType.StoredProcedure;

            try
            {
                totalVisits = Convert.ToInt32(objCountCmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                objPer.closeConnection();
            }

            return totalVisits;
        }


        // Calcula cuántas visitas han sido realizadas por docentes.


        public int countVisitsByTeacher()
        {
            int totalVisits = 0;

            MySqlCommand objCountCmd = new MySqlCommand();
            objCountCmd.Connection = objPer.openConnection();
            objCountCmd.CommandText = "procCountVisitsByTeacher"; // Procedimiento almacenado
            objCountCmd.CommandType = CommandType.StoredProcedure;

            try
            {
                totalVisits = Convert.ToInt32(objCountCmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                objPer.closeConnection();
            }

            return totalVisits;
        }


        // Calcula cuántas visitas han sido realizadas por estudiantes.


        public int countVisitsByStudent()
        {
            int totalVisits = 0;

            MySqlCommand objCountCmd = new MySqlCommand();
            objCountCmd.Connection = objPer.openConnection();
            objCountCmd.CommandText = "procCountVisitsByStudent"; // Procedimiento almacenado
            objCountCmd.CommandType = CommandType.StoredProcedure;

            try
            {
                totalVisits = Convert.ToInt32(objCountCmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                objPer.closeConnection();
            }

            return totalVisits;
        }

        // Lista los materiales más consultados por los usuarios.

        public DataSet GetMaterialAndVisitStats()
        {
            DataSet objData = new DataSet();
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            MySqlCommand objSelectCmd = new MySqlCommand();

            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "procGetMaterialAndVisitStats"; // Procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData); // Llena el DataSet con los resultados del procedimiento
            objPer.closeConnection();

            return objData;
        }


        // Recupera todas las visitas realizadas por un usuario específico.


        public DataSet GetMostVisitedMaterials()
        {
            DataSet objData = new DataSet();
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            MySqlCommand objSelectCmd = new MySqlCommand();

            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "procGetMostVisitedMaterials"; // Procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData); // Llena el DataSet con los resultados del procedimiento
            objPer.closeConnection();

            return objData;
        }

        //  

        public DataSet GetVisitsByUser(int userId)
        {
            DataSet objData = new DataSet();
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            MySqlCommand objSelectCmd = new MySqlCommand();

            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "procSelectVisitsByUser"; // Procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objSelectCmd.Parameters.Add("v_user_id", MySqlDbType.Int32).Value = userId; // Parámetro del procedimiento

            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData); // Llena el DataSet con los resultados
            objPer.closeConnection();

            return objData;
        }

    }
}