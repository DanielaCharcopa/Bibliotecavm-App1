using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Data
{
    public class AuthorsDat
    {
        Persistencia objPer = new Persistencia();

        // Método para mostrar todos los Autores
        public DataSet showAuthors()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "procSelectAuthors"; // Procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        public DataSet showAuthorsDDL()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "procSelectAutorDDL"; // Procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }
        // Método para guardar un nuevo Autor
        public bool saveAuthor(string _nombre, string _apellido, string _municipio)
        {
            bool executed = false;
            int row;
            MySqlCommand objInsertCmd = new MySqlCommand();
            objInsertCmd.Connection = objPer.openConnection();
            objInsertCmd.CommandText = "proInsertAuthors"; // Procedimiento almacenado
            objInsertCmd.CommandType = CommandType.StoredProcedure;
            objInsertCmd.Parameters.Add("v_au_nombre", MySqlDbType.VarChar).Value = _nombre;
            objInsertCmd.Parameters.Add("v_au_apellido", MySqlDbType.VarChar).Value = _apellido;
            objInsertCmd.Parameters.Add("v_au_municipio", MySqlDbType.VarChar).Value = _municipio;

            try
            {
                row = objInsertCmd.ExecuteNonQuery();
                if (row == 1)
                {
                    executed = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            objPer.closeConnection();
            return executed;
        }

        // Método para actualizar un Autor
        public bool updateAuthor(int _idAuthor, string _nombre, string _apellido, string _municipio)
        {
            bool executed = false;
            int row;
            MySqlCommand objUpdateCmd = new MySqlCommand();
            objUpdateCmd.Connection = objPer.openConnection();
            objUpdateCmd.CommandText = "procUpdateAuthor"; // Procedimiento almacenado
            objUpdateCmd.CommandType = CommandType.StoredProcedure;
            objUpdateCmd.Parameters.Add("v_au_id", MySqlDbType.Int32).Value = _idAuthor;
            objUpdateCmd.Parameters.Add("v_au_nombre", MySqlDbType.VarChar).Value = _nombre;
            objUpdateCmd.Parameters.Add("v_au_apellido", MySqlDbType.VarChar).Value = _apellido;
            objUpdateCmd.Parameters.Add("v_au_municipio", MySqlDbType.VarChar).Value = _municipio;

            try
            {
                row = objUpdateCmd.ExecuteNonQuery();
                if (row == 1)
                {
                    executed = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            objPer.closeConnection();
            return executed;
        }

        // Método para borrar un Autor
        public bool deleteAuthor(int _idAuthor)
        {
            bool executed = false;
            int row;
            MySqlCommand objDeleteCmd = new MySqlCommand();
            objDeleteCmd.Connection = objPer.openConnection();
            objDeleteCmd.CommandText = "procDeleteAuthors"; // Procedimiento almacenado
            objDeleteCmd.CommandType = CommandType.StoredProcedure;
            objDeleteCmd.Parameters.Add("v_au_id", MySqlDbType.Int32).Value = _idAuthor;

            try
            {
                row = objDeleteCmd.ExecuteNonQuery();
                if (row == 1)
                {
                    executed = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            objPer.closeConnection();
            return executed;
        }
    }
}