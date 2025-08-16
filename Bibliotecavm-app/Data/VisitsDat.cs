using MySql.Data.MySqlClient;
using System;
using System.Data;

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
        // Registra una nueva visita en la base de datos y devuelve el ID generado
        public int saveVisits(DateTime fechaIngreso, TimeSpan duracion, int usuId, int matId)
        {
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                cmd.Connection = objPer.openConnection();

                // Primero ejecuta el INSERT
                cmd.CommandText = "procInsertVisits";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("v_fecha_ingreso", fechaIngreso);
                cmd.Parameters.AddWithValue("v_duracion", duracion);
                cmd.Parameters.AddWithValue("v_usu_id", usuId);
                cmd.Parameters.AddWithValue("v_mat_id", matId);

                cmd.ExecuteNonQuery();

                cmd.CommandText = "SELECT LAST_INSERT_ID()";
                object result = cmd.ExecuteScalar();

                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en saveVisits: {ex.ToString()}");
                return 0;
            }
            finally
            {
                objPer.closeConnection();
            }
        }

        // Modifica la información de una visita existente.
        public bool updateVisits(int idVisits, DateTime fechaIngreso, TimeSpan duracion, int usuId, int matId)
        {
            bool executed = false;

            MySqlCommand objUpdateCmd = new MySqlCommand();
            objUpdateCmd.Connection = objPer.openConnection();
            objUpdateCmd.CommandText = "procUpdateVisits"; // Procedimiento almacenado
            objUpdateCmd.CommandType = CommandType.StoredProcedure;

            objUpdateCmd.Parameters.Add("v_vis_id", MySqlDbType.Int32).Value = idVisits;
            objUpdateCmd.Parameters.Add("v_fecha_ingreso", MySqlDbType.Date).Value = fechaIngreso;
            objUpdateCmd.Parameters.Add("v_duracion", MySqlDbType.Time).Value = duracion;
            objUpdateCmd.Parameters.Add("v_usu_id", MySqlDbType.Int32).Value = usuId;
            objUpdateCmd.Parameters.Add("v_mat_id", MySqlDbType.Int32).Value = matId;

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

        // Borra una visita de la base de datos.
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

        // Recupera todas las visitas realizadas por un usuario específico.
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

        // Listar materiales educativos.
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

        public int ObtenerUltimaVisitaId(int usuId, int matId)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = objPer.openConnection();
            cmd.CommandText = "procObtenerUltimaVisitaId"; // Nombre del SP
            cmd.CommandType = CommandType.StoredProcedure;

            // Parámetros del SP
            cmd.Parameters.AddWithValue("v_usu_id", usuId);
            cmd.Parameters.AddWithValue("v_mat_id", matId);

            int visitaId = Convert.ToInt32(cmd.ExecuteScalar());
            objPer.closeConnection();

            return visitaId;
        }

        public bool ActualizarDuracionVisita(int visitaId, string duracion)
        {
            using (MySqlCommand cmd = new MySqlCommand())
            {
                cmd.Connection = objPer.openConnection();
                cmd.CommandText = "procActualizarDuracionVisita";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("v_visita_id", visitaId);

                // Asegurar formato HH:MM:SS para MySQL
                if (TimeSpan.TryParse(duracion, out TimeSpan tiempo))
                {
                    string mysqlTimeFormat = $"{tiempo.Hours:00}:{tiempo.Minutes:00}:{tiempo.Seconds:00}";
                    cmd.Parameters.AddWithValue("v_duracion", mysqlTimeFormat);
                }
                else
                {
                    // Formato por defecto si la conversión falla
                    cmd.Parameters.AddWithValue("v_duracion", "00:00:00");
                }

                try
                {
                    int affectedRows = cmd.ExecuteNonQuery();
                    return affectedRows > 0;
                }
                finally
                {
                    objPer.closeConnection();
                }
            }
        }

        // Buscar visitas por correo electrónico
        public DataSet searchVisitsByEmail(string email)
        {
            DataSet objData = new DataSet();
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            MySqlCommand objSelectCmd = new MySqlCommand();

            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "procSearchUserVisitsByEmail";
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Agregar parámetro
            objSelectCmd.Parameters.AddWithValue("@p_email", email);

            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();

            return objData;
        }
        // Buscar visitas por rango de fechas y/o email
        public DataSet SearchVisitsByDateRange(string email, DateTime? startDate, DateTime? endDate)
        {
            DataSet objData = new DataSet();
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            MySqlCommand objSelectCmd = new MySqlCommand();

            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "procSearchVisitsByDateRange";
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Parámetros con manejo de valores nulos
            objSelectCmd.Parameters.AddWithValue("@p_email",
                string.IsNullOrEmpty(email) ? (object)DBNull.Value : email);
            objSelectCmd.Parameters.AddWithValue("@p_fecha_inicio",
                startDate.HasValue ? (object)startDate.Value : DBNull.Value);
            objSelectCmd.Parameters.AddWithValue("@p_fecha_fin",
                endDate.HasValue ? (object)endDate.Value : DBNull.Value);

            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();

            return objData;
        }
    }
}
