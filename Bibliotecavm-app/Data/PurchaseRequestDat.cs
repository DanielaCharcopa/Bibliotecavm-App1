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
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            MySqlCommand objSelectCmd = new MySqlCommand();

            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "procSelectPurchase_request"; // Procedimiento almacenado
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
            objSelectCmd.CommandText = "procSelectPurchase_requestDDL"; // Procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();

            return objData;
        }

        // Guardar una solicitud de compra
        public bool savePurchaseRequest(string solic_ticket, DateTime solic_fecha, int user_id, int solic_cantidad, int mat_id, out string errorMessage)
        {
            bool executed = false;
            errorMessage = string.Empty;
            MySqlCommand objSelectCmd = new MySqlCommand();

            try
            {
                objSelectCmd.Connection = objPer.openConnection();
                objSelectCmd.CommandText = "procInsertPurchase_request"; // Procedimiento almacenado
                objSelectCmd.CommandType = CommandType.StoredProcedure;
                objSelectCmd.Parameters.AddWithValue("v_solic_ticket", solic_ticket);
                objSelectCmd.Parameters.AddWithValue("v_solic_fecha", solic_fecha);
                objSelectCmd.Parameters.AddWithValue("v_tbl_usuarios_usu_id", user_id);
                objSelectCmd.Parameters.AddWithValue("v_solic_cantidad", solic_cantidad);
                objSelectCmd.Parameters.AddWithValue("v_tbl_material_edu_mat_id", mat_id);

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
            finally
            {
                objPer.closeConnection();
            }

            return executed;
        }

        // Actualizar una solicitud de compra
        public bool updatePurchaseRequest(int solic_id, string solic_ticket, DateTime solic_fecha, int user_id, int solic_cantidad, int mat_id)
        {
            bool executed = false;
            MySqlCommand objSelectCmd = new MySqlCommand();

            try
            {
                objSelectCmd.Connection = objPer.openConnection();
                objSelectCmd.CommandText = "procUpdatePurchase_request"; // Procedimiento almacenado
                objSelectCmd.CommandType = CommandType.StoredProcedure;
                objSelectCmd.Parameters.AddWithValue("v_solic_id", solic_id);
                objSelectCmd.Parameters.AddWithValue("v_solic_ticket", solic_ticket);
                objSelectCmd.Parameters.AddWithValue("v_solic_fecha", solic_fecha);
                objSelectCmd.Parameters.AddWithValue("v_tbl_usuarios_usu_id", user_id);
                objSelectCmd.Parameters.AddWithValue("v_solic_cantidad", solic_cantidad);
                objSelectCmd.Parameters.AddWithValue("v_tbl_material_edu_mat_id", mat_id);

                executed = objSelectCmd.ExecuteNonQuery() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al actualizar la solicitud: " + e.Message);
                throw;
            }
            finally
            {
                objPer.closeConnection();
            }

            return executed;
        }

        // Eliminar una solicitud de compra
        public bool deletePurchaseRequest(int solic_id)
        {
            bool executed = false;
            MySqlCommand objSelectCmd = new MySqlCommand();

            try
            {
                objSelectCmd.Connection = objPer.openConnection();
                objSelectCmd.CommandText = "procDeletePurchase_request"; // Procedimiento almacenado
                objSelectCmd.CommandType = CommandType.StoredProcedure;
                objSelectCmd.Parameters.AddWithValue("v_solic_id", solic_id);

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

            return executed;
        }

        // Contar las solicitudes de compra
        public int countPurchaseRequests()
        {
            int totalRequests = -1;
            MySqlCommand objCountCmd = new MySqlCommand();

            try
            {
                objCountCmd.Connection = objPer.openConnection();
                objCountCmd.CommandText = "procCountPurchaseRequests"; // Procedimiento almacenado
                objCountCmd.CommandType = CommandType.StoredProcedure;

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
            finally
            {
                objPer.closeConnection();
            }

            return totalRequests;
        }

        // Mostrar las solicitudes del usuario logueado
        public DataSet showPurchaseRequestsByUser(int userId)
        {
            DataSet objData = new DataSet();
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            MySqlCommand objSelectCmd = new MySqlCommand();

            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "procSelectPurchaseRequestsByUser"; // Procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.AddWithValue("v_user_id", userId);

            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();

            return objData;
        }

        // Obtener lista  de materiales educativos con sus datos
        public DataSet showGetMaterials()
        {
            DataSet objData = new DataSet();
            try
            {
                MySqlDataAdapter objAdapter = new MySqlDataAdapter();
                MySqlCommand objSelectCmd = new MySqlCommand();

                objSelectCmd.Connection = objPer.openConnection();
                objSelectCmd.CommandText = "procGetMaterials";
                objSelectCmd.CommandType = CommandType.StoredProcedure;

                objAdapter.SelectCommand = objSelectCmd;
                objAdapter.Fill(objData);

                objPer.closeConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener materiales: " + ex.Message);
            }
            return objData;
        }

        //listar los materiales educativos
        public DataSet ListarMaterialesEducativos()
        {
            DataSet ds = new DataSet();
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            MySqlCommand objSelectCmd = new MySqlCommand();

            try
            {
                objSelectCmd.Connection = objPer.openConnection();
                objSelectCmd.CommandText = "procListarMaterialesEducativos"; // Nombre del SP
                objSelectCmd.CommandType = CommandType.StoredProcedure;

                objAdapter.SelectCommand = objSelectCmd;
                objAdapter.Fill(ds); // Llenar el DataSet con los resultados del SP
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar materiales educativos: " + ex.Message);
            }
            finally
            {
                objPer.closeConnection();
            }

            return ds;
        }
        // Alternar estado de completado (nuevo método)
        public bool ToggleCompletada(int solicId)
        {
            MySqlCommand objCmd = new MySqlCommand();
            try
            {
                objCmd.Connection = objPer.openConnection();
                objCmd.CommandText = "proc_toggle_completada";
                objCmd.CommandType = CommandType.StoredProcedure;

                // Agregar parámetro
                objCmd.Parameters.AddWithValue("@p_solic_id", solicId);

                // Ejecutar procedimiento
                int affectedRows = objCmd.ExecuteNonQuery();

                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                // Puedes registrar el error si es necesario
                Console.WriteLine("Error en ToggleCompletada: " + ex.Message);
                return false;
            }
            finally
            {
                objPer.closeConnection();
            }
        }

        // Implementación del nuevo SP para obtener material por ID
        public DataTable GetMaterialById(int matId)
        {
            DataTable dtResult = new DataTable();

            try
            {
                using (MySqlConnection connection = objPer.openConnection())
                {
                    MySqlCommand command = new MySqlCommand("procGetMaterialById", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetro de entrada
                    command.Parameters.AddWithValue("@p_mat_id", matId);

                    // Ejecutar y llenar DataTable
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        dtResult.Load(reader);
                    }
                }

                return dtResult;
            }
            catch (Exception ex)
            {
                // Manejo de errores 
                throw new Exception($"Error al obtener material por ID: {ex.Message}");
            }
            finally
            {
                objPer.closeConnection();
            }
        }

    }
}