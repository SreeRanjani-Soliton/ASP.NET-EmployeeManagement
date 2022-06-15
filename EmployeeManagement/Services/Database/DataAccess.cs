using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace EmployeeManagement.Services.Database
{
    /// <summary>
    /// Implementation of IDataAccess, this is used to perform CRUD operation of SQL server DB
    /// </summary>
    public class DataAccess : IDataAccess
    {
        private string _conStr;
        public DataAccess()
        {
            _conStr = "data source=CBE-HPLT-446\\SQLEXPRESS;database = EmployeeDetails;integrated security=true";
        }
        public DataTable GetAllData(string tableName)
        {
            //"Using" will automatically close the connection after the code block
            using (SqlConnection connection = new SqlConnection(_conStr))
            {
                connection.Open();
                //Using SqlAdapter to get data from database
                SqlDataAdapter da = new SqlDataAdapter($"Select * from Employee", connection);
                DataTable dt = new DataTable();
                //Filling the Datatable from the database
                da.Fill(dt);
                return dt;
            }
        }

        public DataRow GetSelectedData(string tableName, string colName, string value)
        {
            //"Using" will automatically close the connection after the code block
            using (SqlConnection connection = new SqlConnection(_conStr))
            {
                //Using SqlAdapter to get data from database
                SqlDataAdapter da = new SqlDataAdapter($"Select * from {tableName} where {colName} = @{colName}", connection);
                da.SelectCommand.Parameters.AddWithValue($"@{colName}", value);
                DataTable dt = new DataTable();
                //Filling the Datatable from the database
                da.Fill(dt);
                //Returning only the first row matching the condition
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0];
                }
                else
                {
                    return null;
                }

            }
        }

        public DataTable InsertData(string tableName, List<string> colNames, List<string> values)
        {
            //"Using" will automatically close the connection after the code block
            using (SqlConnection connection = new SqlConnection(_conStr))
            {
                //Sample Query- "Insert Into dbo.regist (FirstName, Lastname, Username)"+ "VALUES (@FirstName, @Lastname, @Username, @Password)"
                string sqlQuery = $"Insert into {tableName} (";
                string colNameStr = "";
                string colParamStr = "";
                List<SqlParameter> paramList = new List<SqlParameter>();
                SqlCommand cmd = new SqlCommand();
                for (int i = 0; i < colNames.Count; i++)
                {
                    colNameStr += $"{colNames[i]},";
                    colParamStr += $"@{colNames[i]},";
                    paramList.Add(new SqlParameter($"@{colNames[i]}", values[i]));
                }

                sqlQuery = sqlQuery + colNameStr.Remove(colNameStr.Length - 1, 1) + ") VALUES(" + colParamStr.Remove(colParamStr.Length - 1, 1) + ")";

                //Adding insert command to data adapter
                cmd.CommandText = sqlQuery;
                cmd.Connection = connection;
                connection.Open();
                cmd.Parameters.AddRange(paramList.ToArray<SqlParameter>());
                if (cmd.ExecuteNonQuery() > 0)
                {
                    return GetAllData(tableName);
                }

                return null;

            }
        }

        public DataRow UpdateData(string tableName, string matchColName, string matchColValue, List<string> updateColNames, List<string> updateColValues)
        {
            using (SqlConnection connection = new SqlConnection(_conStr))
            {
                //Sample Query- "Update dbo.regist set FirstName=@FirstName, Lastname=@Lastname, Username=@Username where Id = @Id"
                string sqlQuery = $"Update {tableName} set ";
                string updateParamStr = "";
                SqlCommand cmd = new SqlCommand();
                List<SqlParameter> paramList = new List<SqlParameter>();
                for (int i = 0; i < updateColNames.Count; i++)
                {
                    updateParamStr += $"{updateColNames[i]}=@{updateColNames[i]},";
                    paramList.Add(new SqlParameter($"@{updateColNames[i]}", updateColValues[i]));
                }
                sqlQuery = sqlQuery + updateParamStr.Remove(updateParamStr.Length - 1, 1) + $" where {matchColName} = {matchColValue}";
                cmd.CommandText = sqlQuery;
                cmd.Connection = connection;
                cmd.Parameters.AddRange(paramList.ToArray<SqlParameter>());
                connection.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    return GetSelectedData(tableName, matchColName, matchColValue);
                }

                return null;
            }
        }

        public DataTable DeleteSelectedData(string tableName, string colName, string value)
        {
            //"Using" will automatically close the connection after the code block
            using (SqlConnection connection = new SqlConnection(_conStr))
            {
                SqlCommand cmd = new SqlCommand($"Delete from {tableName} where {colName} = @{colName}", connection);
                cmd.Parameters.AddWithValue($"@{colName}", value);
                connection.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    return GetAllData(tableName);
                }
                return null;
            }
        }
    }
}