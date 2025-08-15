using Model;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Data
{
    public class UserDat
    {
        Persistencia objPer = new Persistencia();

        // Mostrar todos los Usuarios
        public DataSet showUsers()
        {
            DataSet objData = new DataSet();
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            MySqlCommand objSelectCmd = new MySqlCommand();

            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "procSelectUsers";
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();

            return objData;
        }

        // Mostrar ID y nombre completo para DDL
        public DataSet showUsersDDL()
        {
            DataSet objData = new DataSet();
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            MySqlCommand objSelectCmd = new MySqlCommand();

            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "procSelectUsersDDL";
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();

            return objData;
        }

        // Registra un nuevo usuario en la base de datos 
        public int saveUser(string nombre, string apellido, string correo, string contrasena,
                          string salt, string celular, string rol)
        {
            MySqlCommand objInsertCmd = new MySqlCommand();
            objInsertCmd.Connection = objPer.openConnection();
            objInsertCmd.CommandText = "procInsertUsers";
            objInsertCmd.CommandType = CommandType.StoredProcedure;

            objInsertCmd.Parameters.Add("v_nombre", MySqlDbType.VarChar, 50).Value = nombre;
            objInsertCmd.Parameters.Add("v_apellido", MySqlDbType.VarChar, 50).Value = apellido;
            objInsertCmd.Parameters.Add("v_correo", MySqlDbType.VarChar, 80).Value = correo;
            objInsertCmd.Parameters.Add("v_contrasena", MySqlDbType.Text).Value = contrasena;
            objInsertCmd.Parameters.Add("v_salt", MySqlDbType.Text).Value = salt;
            objInsertCmd.Parameters.Add("v_celular", MySqlDbType.VarChar, 10).Value = celular;
            objInsertCmd.Parameters.Add("v_rol", MySqlDbType.String).Value = rol;

            var result = objInsertCmd.ExecuteScalar();
            objPer.closeConnection();

            return Convert.ToInt32(result);
        }

        // Modifica la información de un usuario existente
        public bool updateUser(int idUser, string nombre, string apellido, string correo,
                      string contrasena, string salt, string celular, string rol, string estado)
        {
            bool executed = false;
            MySqlConnection connection = null;

            try
            {
                connection = objPer.openConnection();
                MySqlCommand objUpdateCmd = new MySqlCommand();
                objUpdateCmd.Connection = connection;
                objUpdateCmd.CommandText = "procUpdateUsers";
                objUpdateCmd.CommandType = CommandType.StoredProcedure;

                objUpdateCmd.Parameters.Add("v_id", MySqlDbType.Int32).Value = idUser;
                objUpdateCmd.Parameters.Add("v_nombre", MySqlDbType.VarChar).Value = nombre;
                objUpdateCmd.Parameters.Add("v_apellido", MySqlDbType.VarChar).Value = apellido;
                objUpdateCmd.Parameters.Add("v_correo", MySqlDbType.VarChar).Value = correo;
                objUpdateCmd.Parameters.Add("v_contrasena", MySqlDbType.Text).Value = contrasena ?? "";
                objUpdateCmd.Parameters.Add("v_salt", MySqlDbType.Text).Value = salt ?? "";
                objUpdateCmd.Parameters.Add("v_celular", MySqlDbType.VarChar, 10).Value = celular;
                objUpdateCmd.Parameters.Add("v_rol", MySqlDbType.String).Value = rol;
                objUpdateCmd.Parameters.Add("v_estado", MySqlDbType.String).Value = estado;

                var result = objUpdateCmd.ExecuteScalar();
                if (result != null)
                {
                    executed = Convert.ToInt32(result) > 0;
                }
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1644)
                {
                    throw new ApplicationException(ex.Message);
                }
                throw;
            }
            finally
            {
                if (connection != null)
                    objPer.closeConnection();
            }

            return executed;
        }

        // Comprueba si un correo ya está registrado
        public bool checkEmailExists(string correo)
        {
            bool exists = false;
            MySqlCommand cmd = new MySqlCommand("procCheckEmailExists", objPer.openConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("v_correo", MySqlDbType.VarChar).Value = correo;

            try
            {
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                exists = count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al verificar correo: " + ex.Message);
            }
            finally
            {
                objPer.closeConnection();
            }

            return exists;
        }

        // Comprueba si un celular ya está registrado
        public bool checkCelularExists(string celular)
        {
            bool exists = false;
            MySqlCommand cmd = new MySqlCommand("procCheckCelularExists", objPer.openConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("v_celular", MySqlDbType.VarChar, 10).Value = celular;

            try
            {
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                exists = count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al verificar celular: " + ex.Message);
            }
            finally
            {
                objPer.closeConnection();
            }

            return exists;
        }

        // Borra un usuario de la base de datos
        public bool deleteUser(int idUser)
        {
            bool executed = false;

            MySqlCommand objDeleteCmd = new MySqlCommand();
            objDeleteCmd.Connection = objPer.openConnection();
            objDeleteCmd.CommandText = "procDeleteUsers";
            objDeleteCmd.CommandType = CommandType.StoredProcedure;

            objDeleteCmd.Parameters.Add("v_id", MySqlDbType.Int32).Value = idUser;

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

        // Obtiene información de usuario por correo (para login)
        public User showUsersMail(string mail)
        {
            User objUser = null;
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "procValidateUserLogin";
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objSelectCmd.Parameters.Add("v_correo", MySqlDbType.VarChar).Value = mail;

            try
            {
                using (MySqlDataReader reader = objSelectCmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        objUser = new User
                        {
                            UsuId = reader.GetInt32("usu_id"),
                            NombreCompleto = reader.GetString("nombre_completo"),
                            Correo = reader.GetString("usu_correo"),
                            Celular = reader.GetString("usu_celular"),
                            Contrasena = reader.GetString("usu_contrasena"),
                            Salt = reader.GetString("usu_salt"),
                            Rol = reader.GetString("usu_rol"),
                            Estado = reader.GetString("usu_estado")
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el usuario por correo", ex);
            }
            finally
            {
                objPer.closeConnection();
            }

            return objUser;
        }

        // Comprueba si hay al menos un administrador registrado
        public bool AdminExists()
        {
            bool exists = false;
            MySqlCommand cmd = new MySqlCommand("procCheckAdminExists", objPer.openConnection());
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                exists = count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al verificar administrador: " + ex.Message);
            }
            finally
            {
                objPer.closeConnection();
            }

            return exists;
        }

        // Buscar usuarios por correo electrónico
        public DataSet SearchUsersByEmail(string email)
        {
            DataSet objData = new DataSet();
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            MySqlCommand objSelectCmd = new MySqlCommand();

            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "procSearchUsersByEmail";
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objSelectCmd.Parameters.AddWithValue("p_correo", email);

            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();

            return objData;
        }

        // Obtener solo usuarios activos
        public DataSet GetActiveUsers()
        {
            DataSet objData = new DataSet();
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            MySqlCommand objSelectCmd = new MySqlCommand();

            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "procSelectActiveUsers";
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();

            return objData;
        }

        // Buscar usuarios por estado
        public DataSet SearchUsersByStatus(string email, string estado)
        {
            DataSet objData = new DataSet();
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            MySqlCommand objSelectCmd = new MySqlCommand();

            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "procSearchUsersByStatus";
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objSelectCmd.Parameters.AddWithValue("p_correo", email);
            objSelectCmd.Parameters.AddWithValue("p_estado", estado);

            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();

            return objData;
        }

        // Activar usuario
        public bool ActivateUser(int userId)
        {
            bool executed = false;
            MySqlCommand cmd = new MySqlCommand("procActiveUser", objPer.openConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("p_usu_id", userId);

            try
            {
                executed = cmd.ExecuteNonQuery() == 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al activar usuario: " + ex.Message);
            }
            finally
            {
                objPer.closeConnection();
            }

            return executed;
        }

        // Desactivar usuario
        public bool DeactivateUser(int userId)
        {
            bool executed = false;
            MySqlCommand cmd = new MySqlCommand("procDeactivateUser", objPer.openConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("p_usu_id", userId);

            try
            {
                executed = cmd.ExecuteNonQuery() == 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al desactivar usuario: " + ex.Message);
            }
            finally
            {
                objPer.closeConnection();
            }

            return executed;
        }

        // Obtener el número celular
        public string GetUserPhone(int userId)
        {
            MySqlCommand objCmd = new MySqlCommand();
            objCmd.Connection = objPer.openConnection();
            objCmd.CommandText = "procGetUserPhone";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("v_user_id", userId);

            object result = objCmd.ExecuteScalar();
            objPer.closeConnection();

            return result?.ToString() ?? string.Empty;
        }

    }

}