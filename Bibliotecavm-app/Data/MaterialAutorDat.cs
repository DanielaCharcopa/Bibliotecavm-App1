using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Data
{
    public class MaterialAutorDat
    {
        Persistencia objPer = new Persistencia(); // Instancia de conexión a la base de datos

        // Mostrar todas las Categorías
        public DataSet showMaterialAutor()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "procSelectMaterial_Autores"; // Procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        public DataSet showMaterialAutorDDL()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "procSelectMaterial_Autor_DDL"; // Procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }


        // Guardar Material_Autores
        public bool saveMaterialAutor(int _idmaterial_edu, int id_autores, string _descripcion)
        {

            bool executed = false;
            int row;
            MySqlCommand objInsertCmd = new MySqlCommand();
            objInsertCmd.Connection = objPer.openConnection();
            objInsertCmd.CommandText = "proInsertMaterialAutor"; // Procedimiento almacenado
            objInsertCmd.CommandType = CommandType.StoredProcedure;
            objInsertCmd.Parameters.Add("v_tbl_material_edu_mat_id", MySqlDbType.Int32).Value = _idmaterial_edu;
            objInsertCmd.Parameters.Add("v_tbl_autores_au_id", MySqlDbType.Int32).Value = id_autores;
            objInsertCmd.Parameters.Add("v_descripcion", MySqlDbType.Text).Value = _descripcion;

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


        // Actualizar Material_Autores
        public bool updateMaterialAutor(int _id, int _idmaterial_edu, int id_autores, string _descripcion)
        {
            bool executed = false;
            int row;
            MySqlCommand objUpdateCmd = new MySqlCommand();
            objUpdateCmd.Connection = objPer.openConnection();
            objUpdateCmd.CommandText = "procUpdateMaterial_Autor"; // Procedimiento almacenado
            objUpdateCmd.CommandType = CommandType.StoredProcedure;
            objUpdateCmd.Parameters.Add("v_id_material_autores", MySqlDbType.Int32).Value = _id;
            objUpdateCmd.Parameters.Add("v_tbl_material_edu_mat_id", MySqlDbType.Int32).Value = _idmaterial_edu;
            objUpdateCmd.Parameters.Add("v_tbl_autores_au_id", MySqlDbType.Int32).Value = id_autores;
            objUpdateCmd.Parameters.Add("v_descripcion", MySqlDbType.Text).Value = _descripcion;

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

        // Eliminar Material_Autores
        public bool deleteMaterialAutor(int _id)
        {
            bool executed = false;
            int row;
            MySqlCommand objDeleteCmd = new MySqlCommand();
            objDeleteCmd.Connection = objPer.openConnection();
            objDeleteCmd.CommandText = "procDeleteMaterial_Autor"; // Procedimiento almacenado
            objDeleteCmd.CommandType = CommandType.StoredProcedure;
            objDeleteCmd.Parameters.Add("v_id_material_autores", MySqlDbType.Int32).Value = _id;

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