using System;
using System.Data.SqlClient;

namespace DAL_CarSales
{
    public class DbConnect
    {
        private string connectionString = "Data Source=CHULUONGNGOCANH\\NGOCANH;Initial Catalog=CarSalesDB;Integrated Security=True";

        public SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
    }
}
