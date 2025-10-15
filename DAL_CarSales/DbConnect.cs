using System;
using System.Data.SqlClient;
using System.Configuration;

namespace DAL_CarSales
{
    public class DbConnect
    {
        private static string connectionString = "Data Source=CHULUONGNGOCANH\\NGOCANH;Initial Catalog=CarSalesDB;Integrated Security=True;";

        // Singleton pattern cho connection string
        public static string ConnectionString
        {
            get { return connectionString; }
        }

        // Method để lấy connection mới
        public SqlConnection GetConnection()
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                return connection;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi kết nối database: " + ex.Message);
            }
        }

        // Method kiểm tra kết nối
        public bool TestConnection()
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
