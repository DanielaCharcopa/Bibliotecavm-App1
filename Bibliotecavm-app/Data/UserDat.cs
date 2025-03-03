using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Model;

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
            objSelectCmd.CommandText = "procSelectUsers"; // Procedimiento almacenado
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
            objSelectCmd.CommandText = "procSelectUsersDDL"; // Procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();

            return objData;
        }

        // Registra un nuevo usuario en la base de datos.
        public bool saveUser(string nombre, string apellido, string correo, string contrasena, string salt, string rol, string nivelEstudios)
        {
            bool executed = false;

            MySqlCommand objInsertCmd = new MySqlCommand();
            objInsertCmd.Connection = objPer.openConnection();
            objInsertCmd.CommandText = "procInsertUsers"; // Procedimiento almacenado
            objInsertCmd.CommandType = CommandType.StoredProcedure;

            objInsertCmd.Parameters.Add("v_nombre", MySqlDbType.VarChar).Value = nombre;
            objInsertCmd.Parameters.Add("v_apellido", MySqlDbType.VarChar).Value = apellido;
            objInsertCmd.Parameters.Add("v_correo", MySqlDbType.VarChar).Value = correo;
            objInsertCmd.Parameters.Add("v_contrasena", MySqlDbType.Text).Value = contrasena;
            objInsertCmd.Parameters.Add("v_salt", MySqlDbType.Text).Value = salt;
            objInsertCmd.Parameters.Add("v_rol", MySqlDbType.String).Value = rol; // Cambiar a String para ENUM
            objInsertCmd.Parameters.Add("v_nivel_estudios", MySqlDbType.String).Value = nivelEstudios; // Cambiar a String para ENUM

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

        // Modifica la información de un usuario existente.
        public bool updateUser(int idUser, string nombre, string apellido, string correo, string contrasena, string salt, string rol, string nivelEstudios)
        {
            bool executed = false;

            MySqlCommand objUpdateCmd = new MySqlCommand();
            objUpdateCmd.Connection = objPer.openConnection();
            objUpdateCmd.CommandText = "procUpdateUsers"; // Procedimiento almacenado
            objUpdateCmd.CommandType = CommandType.StoredProcedure;

            objUpdateCmd.Parameters.Add("v_id", MySqlDbType.Int32).Value = idUser;
            objUpdateCmd.Parameters.Add("v_nombre", MySqlDbType.VarChar).Value = nombre;
            objUpdateCmd.Parameters.Add("v_apellido", MySqlDbType.VarChar).Value = apellido;
            objUpdateCmd.Parameters.Add("v_correo", MySqlDbType.VarChar).Value = correo;
            objUpdateCmd.Parameters.Add("v_contrasena", MySqlDbType.Text).Value = contrasena;
            objUpdateCmd.Parameters.Add("v_salt", MySqlDbType.Text).Value = salt;
            objUpdateCmd.Parameters.Add("v_rol", MySqlDbType.String).Value = rol; // Cambiar a String para ENUM
            objUpdateCmd.Parameters.Add("v_nivel_estudios", MySqlDbType.String).Value = nivelEstudios; // Cambiar a String para ENUM

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

        // Comprueba si un correo ya está registrado en la base de datos.
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

        // Borra un usuario de la base de datos.
        public bool deleteUser(int idUser)
        {
            bool executed = false;

            MySqlCommand objDeleteCmd = new MySqlCommand();
            objDeleteCmd.Connection = objPer.openConnection();
            objDeleteCmd.CommandText = "procDeleteUsers"; // Procedimiento almacenado
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

        public User showUsersMail(string mail)
        {
            User objUser = null;
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "procValidateUserLogin"; // Procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Parámetros del procedimiento almacenado
            objSelectCmd.Parameters.Add("v_correo", MySqlDbType.VarChar).Value = mail;

            try
            {
                using (MySqlDataReader reader = objSelectCmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Crear un objeto User con los datos devueltos por el procedimiento almacenado
                        objUser = new User
                        {
                            UsuId = reader.GetInt32("usu_id"),
                            NombreCompleto = reader.GetString("nombre_completo"),
                            Correo = reader.GetString("usu_correo"),
                            Contrasena = reader.GetString("usu_contrasena"),
                            Salt = reader.GetString("usu_salt"),
                            Rol = reader.GetString("usu_rol")
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
                objPer.closeConnection(); // Cerrar la conexión usando Persistencia
            }

            return objUser;
        }

        // Comprueba si hay al menos un administrador registrado en la base de datos.
        public bool AdminExists()
        {
            bool exists = false;
            MySqlCommand cmd = new MySqlCommand("procCheckAdminExists", objPer.openConnection()); // Usamos objPer para la conexión
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                int count = Convert.ToInt32(cmd.ExecuteScalar()); // Ejecuta el procedimiento y obtiene el valor
                exists = count > 0; // Si hay al menos un administrador
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al verificar administrador: " + ex.Message);
            }
            finally
            {
                objPer.closeConnection(); // Cerramos la conexión
            }

            return exists;
        }
    }
}