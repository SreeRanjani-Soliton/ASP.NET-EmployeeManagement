using System.Collections.Generic;
using System.Data;
using EmployeeManagement.Models;

namespace EmployeeManagement.Services.Database
{
    public interface IDataAccess
    {
        //Get Employee Details

        /// <summary>
        /// Gets all the rows from the selected table
        /// </summary>
        /// <param name="tableName">Name of the table whoes values are needed</param>
        /// <returns>Data from DB in form of DataTable</returns>
        public DataTable GetAllData(string tableName);

        /// <summary>
        /// Gets a row that matches the given column name and value combination from the table
        /// </summary>
        /// <param name="tableName">Name of the table from which value needs to be obtained</param>
        /// <param name="colName">Name of column whoes value should match</param>
        /// <param name="value">Value to match</param>
        /// <returns>Data from DB in form of DataTable</returns>
        public DataRow GetSelectedData(string tableName, string colName, string value);

        /// <summary>
        /// Inserts a row into the table with given column names and corresponding values
        /// </summary>
        /// <param name="tableName">Name of table to be updated</param>
        /// <param name="colNames">List of column names</param>
        /// <param name="values">List of values of row to be added to the table. Order of column names and values should match</param>
        /// <returns>All data from database</returns>
        public DataTable InsertData(string tableName, List<string> colNames, List<string> values);

        /// <summary>
        /// Updates a row that matches the match column name and match column value combination with the update column names and values given
        /// </summary>
        /// <param name="tableName">Name of table to be updated</param>
        /// <param name="matchColName">Column name to match</param>
        /// <param name="matchColvalue">Column value to match</param>
        /// <param name="updateColNames">Column names to be whose values needs to be updated</param>
        /// <param name="updateColValues">Values to be updated</param>
        /// <returns>All data from database</returns>
        public DataRow UpdateData(string tableName, string matchColName, string matchColvalue, List<string> updateColNames, List<string> updateColValues);

        /// <summary>
        /// Deletes a row that matches the column name and value combination
        /// </summary>
        /// <param name="tableName">Name of the table from which row needs to be deleted</param>
        /// <param name="colName">Name of column whoes value should match</param>
        /// <param name="value">Value to match</param>
        /// <returns>Remaining data</returns>
        public DataTable DeleteSelectedData(string tableName, string colName, string value);
    }
}