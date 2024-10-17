using System;
using System.Data.SqlClient;

namespace DatabaseConnection
{
    public class DatabaseConnector
    {
        private readonly string _connectionString;
        private SqlConnection? _connection; 

        public DatabaseConnector(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void OpenConnection()
        {
            if (_connection == null)
            {
                _connection = new SqlConnection(_connectionString);
            }
            _connection.Open();
        }

        public void CloseConnection()
        {
            if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
            }
        }

        public SqlConnection? GetConnection() 
        {
            return _connection;
        }
    }
}
