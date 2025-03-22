using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Data
{
    public class MateriaEduDat
    {
        Persistencia objPer = new Persistencia();

        // Mostrar todos los materiales educativos
        public DataSet showMaterialEdu()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "proSelectMaterialEducativo"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData); // Llena el DataSet con los datos devueltos por el procedimiento almacenado
            objPer.closeConnection();

            return objData;
        }

        public bool saveMaterialEducativo(string _titulo, int _anoPublicacion, string _urlDescarga, decimal _precio,
                                string _keywords, string _formato, int _editorialId, int _categoriaId)
        {
            bool executed = false;
            MySqlCommand objInsertCmd = new MySqlCommand();
            objInsertCmd.Connection = objPer.openConnection();
            objInsertCmd.CommandText = "proInsertMaterialEducativo"; // Nombre del procedimiento almacenado
            objInsertCmd.CommandType = CommandType.StoredProcedure;

            // Parámetros del procedimiento almacenado
            objInsertCmd.Parameters.Add("titulo", MySqlDbType.VarChar).Value = _titulo;
            objInsertCmd.Parameters.Add("ano_publicacion", MySqlDbType.Int32).Value = _anoPublicacion;
            objInsertCmd.Parameters.Add("url_descarga", MySqlDbType.Text).Value = _urlDescarga;
            objInsertCmd.Parameters.Add("precio", MySqlDbType.Decimal).Value = _precio;
            objInsertCmd.Parameters.Add("keywords", MySqlDbType.Text).Value = _keywords ?? (object)DBNull.Value;
            objInsertCmd.Parameters.Add("formato", MySqlDbType.VarChar).Value = _formato ?? (object)DBNull.Value;
            objInsertCmd.Parameters.Add("editorial_id", MySqlDbType.Int32).Value = _editorialId;
            objInsertCmd.Parameters.Add("categoria_id", MySqlDbType.Int32).Value = _categoriaId;


            try
            {
                int rows = objInsertCmd.ExecuteNonQuery();
                executed = rows == 1; // Verifica si se insertó correctamente
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message); // Manejo de errores
            }
            finally
            {
                objPer.closeConnection(); // Cierra la conexión
            }

            return executed; // Devuelve true si se ejecutó correctamente
        }

        public bool updateMaterialEducativo(int _idMaterial, string _titulo, int _anoPublicacion, string _urlDescarga,
                                   decimal _precio, string _keywords, string _formato, int _editorialId,
                                   int _categoriaId)
        {
            bool executed = false;
            MySqlCommand objUpdateCmd = new MySqlCommand();
            objUpdateCmd.Connection = objPer.openConnection();
            objUpdateCmd.CommandText = "proUpdateMaterialEducativo"; // Nombre del procedimiento almacenado
            objUpdateCmd.CommandType = CommandType.StoredProcedure;

            // Parámetros del procedimiento almacenado
            objUpdateCmd.Parameters.Add("id", MySqlDbType.Int32).Value = _idMaterial;
            objUpdateCmd.Parameters.Add("titulo", MySqlDbType.VarChar).Value = _titulo;
            objUpdateCmd.Parameters.Add("ano_publicacion", MySqlDbType.Int32).Value = _anoPublicacion;
            objUpdateCmd.Parameters.Add("url_descarga", MySqlDbType.Text).Value = _urlDescarga;
            objUpdateCmd.Parameters.Add("precio", MySqlDbType.Decimal).Value = _precio;
            objUpdateCmd.Parameters.Add("keywords", MySqlDbType.Text).Value = _keywords ?? (object)DBNull.Value;
            objUpdateCmd.Parameters.Add("formato", MySqlDbType.VarChar).Value = _formato ?? (object)DBNull.Value;
            objUpdateCmd.Parameters.Add("editorial_id", MySqlDbType.Int32).Value = _editorialId;
            objUpdateCmd.Parameters.Add("categoria_id", MySqlDbType.Int32).Value = _categoriaId;


            try
            {
                int rows = objUpdateCmd.ExecuteNonQuery();
                executed = rows == 1; // Verifica si se actualizó correctamente
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message); // Manejo de errores
            }
            finally
            {
                objPer.closeConnection(); // Cierra la conexión
            }

            return executed; // Devuelve true si se ejecutó correctamente
        }

        // Eliminar un material educativo
        public bool deleteMaterialEducativo(int _idMaterial)
        {
            bool executed = false;
            MySqlCommand objDeleteCmd = new MySqlCommand();
            objDeleteCmd.Connection = objPer.openConnection();
            objDeleteCmd.CommandText = "proDeleteMaterialEducativo"; // Procedimiento almacenado
            objDeleteCmd.CommandType = CommandType.StoredProcedure;
            objDeleteCmd.Parameters.Add("id", MySqlDbType.Int32).Value = _idMaterial;


            try
            {
                int rows = objDeleteCmd.ExecuteNonQuery();
                executed = rows == 1; // Verifica si se eliminó correctamente
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message); // Manejo de errores
            }
            finally
            {
                objPer.closeConnection(); // Cierra la conexión
            }

            return executed; // Devuelve true si se ejecutó correctamente
        }


    }
}