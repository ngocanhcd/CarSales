using System;
using System.Data.SqlClient;

namespace DAL_CarSales
{
    public class UserDAL
    {
        private DbConnect dbConnect;

        public UserDAL()
        {
            dbConnect = new DbConnect();
        }

        public bool Login(string username, string password)
        {
            SqlConnection connection = dbConnect.GetConnection();
            string query = "SELECT * FROM Users WHERE Username = @Username AND Password = @Password";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            bool isAuthenticated = reader.HasRows;
            connection.Close();
            return isAuthenticated;
        }

        // Add other methods for managing users (create, update, etc.)
    }
}
