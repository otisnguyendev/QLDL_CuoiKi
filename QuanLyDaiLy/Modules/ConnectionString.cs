using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace QuanLyDaiLy.Modules
{
    public class ConnectionString
    {
        public string ServerName { get; set; }
        public string DatabaseName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public MySqlConnection sqlCNN = null;

        public ConnectionString()
        {
            ServerName = "localhost";
            DatabaseName = "dbcuoiky";
            UserName = "root";
            Password = "123456";
        }

        public void getConnectionString()
        {
            string strConn = $"Server={ServerName};Database={DatabaseName};User ID={UserName};Password={Password};";
            sqlCNN = new MySqlConnection(strConn);

            if (sqlCNN != null && sqlCNN.State == ConnectionState.Closed)
            {
                sqlCNN.Open();
                Console.WriteLine("Connection opened successfully.");
            }
        }

        public void closeConnection()
        {
            if (sqlCNN != null && sqlCNN.State == ConnectionState.Open)
            {
                sqlCNN.Close();
                Console.WriteLine("Connection closed successfully.");
            }
        }
    }
}
