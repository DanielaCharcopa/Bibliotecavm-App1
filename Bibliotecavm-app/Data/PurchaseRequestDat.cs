using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Data
{
    public class PurchaseRequestDat
    {
        Persistencia objPer = new Persistencia();

        // Mostrar todas las solicitudes de compra
        public DataSet showPurchaseRequest()
        {
            DataSet objData = new DataSet();

            using (MySqlCommand objSelectCmd = new MySqlCommand("procSelectPurchase_request", objPer.openConnection()))
            {
                objSelectCmd.CommandType = CommandType.StoredProcedure;

                using (MySqlDataAdapter objAdapter = new MySqlDataAdapter(objSelectCmd))
                {
                    objAdapter.Fill(objData);
                }
            }
            return objData;
        }

        // Mostrar solicitudes en formato DDL
        public DataSet showPurchaseRequestDDL()
        {
            DataSet objData = new DataSet();

            using (MySqlCommand objSelectCmd = new MySqlCommand("procSelectPurchase_requestDDL", objPer.openConnection()))
            {
                objSelectCmd.CommandType = CommandType.StoredProcedure;

                using (MySqlDataAdapter objAdapter = new MySqlDataAdapter(objSelectCmd))
                {
                    objAdapter.Fill(objData);
                }
            }

            return objData;
        }

        // Guardar una solicitud de compra
        public bool savePurchaseRequest(string solic_ticket, DateTime solic_fecha, int user_id, int solic_cantidad, int mat_id, out string errorMessage)
        {
            bool executed = false;
            errorMessage = string.Empty;

            using (MySqlCommand objSelectCmd = new MySqlCommand("procInsertPurchase_request", objPer.openConnection()))
            {
                objSelectCmd.CommandType = CommandType.StoredProcedure;
                objSelectCmd.Parameters.AddWithValue("v_solic_ticket", solic_ticket);
                objSelectCmd.Parameters.AddWithValue("v_solic_fecha", solic_fecha);
                objSelectCmd.Parameters.AddWithValue("v_tbl_usuarios_usu_id", user_id);
                objSelectCmd.Parameters.AddWithValue("v_solic_cantidad", solic_cantidad);
                objSelectCmd.Parameters.AddWithValue("v_tbl_material_edu_mat_id", mat_id);

                try
                {
                    executed = objSelectCmd.ExecuteNonQuery() > 0;
                }
                catch (MySqlException ex)
                {
                    errorMessage = ex.Message;
                }
                catch (Exception e)
                {
                    errorMessage = "Error inesperado: " + e.Message;
                }
            }
            return executed;
        }

        // Actualizar una solicitud de compra
        public bool updatePurchaseRequest(int solic_id, string solic_ticket, DateTime solic_fecha, int user_id, int solic_cantidad, int mat_id)
        {
            bool executed = false;

            using (MySqlCommand objSelectCmd = new MySqlCommand("procUpdatePurchase_request", objPer.openConnection()))
            {
                objSelectCmd.CommandType = CommandType.StoredProcedure;
                objSelectCmd.Parameters.AddWithValue("v_solic_id", solic_id);
                objSelectCmd.Parameters.AddWithValue("v_solic_ticket", solic_ticket);
                objSelectCmd.Parameters.AddWithValue("v_solic_fecha", solic_fecha);
                objSelectCmd.Parameters.AddWithValue("v_tbl_usuarios_usu_id", user_id);
                objSelectCmd.Parameters.AddWithValue("v_solic_cantidad", solic_cantidad);
                objSelectCmd.Parameters.AddWithValue("v_tbl_material_edu_mat_id", mat_id);

                try
                {
                    executed = objSelectCmd.ExecuteNonQuery() > 0;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error al actualizar la solicitud: " + e.Message);
                    throw;
                }
            }

            return executed;
        }

        // Eliminar una solicitud de compra
        public bool deletePurchaseRequest(int solic_id)
        {
            bool executed = false;

            using (MySqlCommand objSelectCmd = new MySqlCommand("procDeletePurchase_request", objPer.openConnection()))
            {
                objSelectCmd.CommandType = CommandType.StoredProcedure;
                objSelectCmd.Parameters.AddWithValue("v_solic_id", solic_id);

                try
                {
                    executed = objSelectCmd.ExecuteNonQuery() > 0;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                    throw;
                }
                finally
                {
                    objPer.closeConnection();
                }
            }

            return executed;
        }

        // Contar las solicitudes de compra
        public int countPurchaseRequests()
        {
            int totalRequests = -1;

            using (MySqlCommand objCountCmd = new MySqlCommand("procCountPurchaseRequests", objPer.openConnection()))
            {
                objCountCmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    object result = objCountCmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        totalRequests = Convert.ToInt32(result);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al contar las solicitudes: " + ex.Message);
                }
            }
            return totalRequests;
        }

        // Mostrar las solicitudes del usuario logueado
        public DataSet showPurchaseRequestsByUser(int userId)
        {
            DataSet objData = new DataSet();

            using (MySqlConnection connection = objPer.openConnection())
            {
                using (MySqlCommand objSelectCmd = new MySqlCommand("procSelectPurchaseRequestsByUser", connection))
                {
                    objSelectCmd.CommandType = CommandType.StoredProcedure;
                    objSelectCmd.Parameters.AddWithValue("v_user_id", userId);

                    using (MySqlDataAdapter objAdapter = new MySqlDataAdapter(objSelectCmd))
                    {
                        objAdapter.Fill(objData);
                    }
                }
            }

            return objData;
        }

        // Obtener lista completa de materiales educativos con sus datos
        public DataSet showMaterialEducativos()
        {
            DataSet objData = new DataSet();
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            MySqlCommand objSelectCmd = new MySqlCommand();

            try
            {
                objSelectCmd.Connection = objPer.openConnection();
                objSelectCmd.CommandText = "proSelectMaterialEducativo"; // Procedimiento almacenado
                objSelectCmd.CommandType = CommandType.StoredProcedure;

                objAdapter.SelectCommand = objSelectCmd;
                objAdapter.Fill(objData); // Llenar el DataSet con los resultados
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en showMaterialEducativos: " + ex.Message);
            }
            finally
            {
                objPer.closeConnection();
            }

            return objData;
        }

    }
}