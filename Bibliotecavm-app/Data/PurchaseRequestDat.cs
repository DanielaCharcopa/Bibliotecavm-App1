using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Data
{
    public class PurchaseRequestDat
    {
        Persistencia objPer = new Persistencia();

        // Mostrar todas las solicitudes de compra
        public DataSet showPurchaseRequest()
        {
            DataSet objData = new DataSet();
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            MySqlCommand objSelectCmd = new MySqlCommand();

            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "procSelectPurchase_request";
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();

            return objData;
        }


        // Mostrar solicitudes en formato DDL
        public DataSet showPurchaseRequestDDL()
        {
            DataSet objData = new DataSet();
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            MySqlCommand objSelectCmd = new MySqlCommand();

            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "procSelectPurchase_requestDDL";
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();

            return objData;
        }

        // Guardar una solicitud de compra      

        public bool savePurchaseRequest(string solic_ticket, DateTime solic_fecha, int user_id, int solic_cantidad, int mat_id)
        {
            bool executed = false;
            int row;
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "procInsertPurchase_request"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("v_solic_ticket", MySqlDbType.VarChar).Value = solic_ticket;
            objSelectCmd.Parameters.Add("v_solic_fecha", MySqlDbType.Date).Value = solic_fecha;
            objSelectCmd.Parameters.Add("v_tbl_usuarios_usu_id", MySqlDbType.Int32).Value = user_id;
            objSelectCmd.Parameters.Add("v_solic_cantidad", MySqlDbType.Int32).Value = solic_cantidad;
            objSelectCmd.Parameters.Add("v_tbl_material_edu_mat_id", MySqlDbType.Int32).Value = mat_id; // Nuevo parámetro

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

        // Actualizar una solicitud de compra

        public bool updatePurchaseRequest(int solic_id, string solic_ticket, DateTime solic_fecha, int user_id, int solic_cantidad, int mat_id)
        {
            bool executed = false;
            int row;
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "procUpdatePurchase_request"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("v_solic_id", MySqlDbType.Int32).Value = solic_id;
            objSelectCmd.Parameters.Add("v_solic_ticket", MySqlDbType.VarChar).Value = solic_ticket;
            objSelectCmd.Parameters.Add("v_solic_fecha", MySqlDbType.Date).Value = solic_fecha;
            objSelectCmd.Parameters.Add("v_tbl_usuarios_usu_id", MySqlDbType.Int32).Value = user_id;
            objSelectCmd.Parameters.Add("v_solic_cantidad", MySqlDbType.Int32).Value = solic_cantidad;
            objSelectCmd.Parameters.Add("v_tbl_material_edu_mat_id", MySqlDbType.Int32).Value = mat_id; // Nuevo parámetro

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

        // Eliminar una solicitud de compra

        public bool deletePurchaseRequest(int solic_id)
        {
            bool executed = false;
            int row;
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "procDeletePurchase_request"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("v_solic_id", MySqlDbType.Int32).Value = solic_id;

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

        // Contar las solicitudes de compra
        public int countPurchaseRequests()
        {
            int totalRequests = 0;

            MySqlCommand objCountCmd = new MySqlCommand();
            objCountCmd.Connection = objPer.openConnection();
            objCountCmd.CommandText = "procCountPurchaseRequests";
            objCountCmd.CommandType = CommandType.StoredProcedure;

            try
            {
                totalRequests = Convert.ToInt32(objCountCmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                objPer.closeConnection();
            }

            return totalRequests;
        }

        //  Método para Mostrar las Solicitudes del Usuario
        public DataSet showPurchaseRequestsByUser(int userId)
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "procSelectPurchaseRequestsByUser"; // Nombre del procedimiento almacenado
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