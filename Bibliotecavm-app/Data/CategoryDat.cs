using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Data
{
    public class CategoryDat
    {
        Persistencia objPer = new Persistencia(); // Instancia de conexión a la base de datos

        // Mostrar todas las Categorías
        public DataSet showCategory()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "procSelectCategory"; // Procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }



        // Guardar Categoría
        public bool saveCategory(string _nombre, string _description)
        {
            bool executed = false;
            int row;
            MySqlCommand objInsertCmd = new MySqlCommand();
            objInsertCmd.Connection = objPer.openConnection();
            objInsertCmd.CommandText = "procInsertCategory"; // Procedimiento almacenado
            objInsertCmd.CommandType = CommandType.StoredProcedure;
            objInsertCmd.Parameters.Add("v_nombre", MySqlDbType.Enum).Value = _nombre;
            objInsertCmd.Parameters.Add("v_description", MySqlDbType.Text).Value = _description;

            try
            {
                row = objInsertCmd.ExecuteNonQuery();
                executed = row > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            objPer.closeConnection();
            return executed;
        }

        // Actualizar Categoría
        public bool updateCategory(int _id, string _nombre, string _description)
        {
            bool executed = false;
            int row;
            MySqlCommand objUpdateCmd = new MySqlCommand();
            objUpdateCmd.Connection = objPer.openConnection();
            objUpdateCmd.CommandText = "procUpdateCategory"; // Procedimiento almacenado
            objUpdateCmd.CommandType = CommandType.StoredProcedure;
            objUpdateCmd.Parameters.Add("v_id", MySqlDbType.Int32).Value = _id;
            objUpdateCmd.Parameters.Add("v_nombre", MySqlDbType.Enum).Value = _nombre;
            objUpdateCmd.Parameters.Add("v_descripcion", MySqlDbType.Text).Value = _description;

            try
            {
                row = objUpdateCmd.ExecuteNonQuery();
                executed = row > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            objPer.closeConnection();
            return executed;
        }

        // Eliminar Categoría
        public bool deleteCategory(int _id)
        {
            bool executed = false;
            int row;
            MySqlCommand objDeleteCmd = new MySqlCommand();
            objDeleteCmd.Connection = objPer.openConnection();
            objDeleteCmd.CommandText = "procDeleteCategory"; // Procedimiento almacenado
            objDeleteCmd.CommandType = CommandType.StoredProcedure;
            objDeleteCmd.Parameters.Add("v_id", MySqlDbType.Int32).Value = _id;

            try
            {
                row = objDeleteCmd.ExecuteNonQuery();
                executed = row > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            objPer.closeConnection();
            return executed;
        }
    }
}