using System;
using System.Data;
using System.Data.SqlClient;
using DTO_Carsales;

namespace DAL_CarSales
{
    public class UserDAL
    {
        private DbConnect dbConnect;

        public UserDAL()
        {
            dbConnect = new DbConnect();
        }

        // ==================== ĐĂNG NHẬP - TRẢ VỀ UserDTO ====================
        public UserDTO Login(string username, string password)
        {
            UserDTO user = null;
            SqlConnection connection = null;
            SqlDataReader reader = null;

            try
            {
                connection = dbConnect.GetConnection();
                string query = @"SELECT UserID, FullName, Username, Email, Phone, Address, Role, CreatedAt 
                                FROM Users 
                                WHERE Username = @Username AND Password = @Password";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                connection.Open();
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    user = new UserDTO
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        FullName = reader["FullName"].ToString(),
                        Username = reader["Username"].ToString(),
                        Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : "",
                        Phone = reader["Phone"] != DBNull.Value ? reader["Phone"].ToString() : "",
                        Address = reader["Address"] != DBNull.Value ? reader["Address"].ToString() : "",
                        Role = reader["Role"].ToString(),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi đăng nhập DAL: " + ex.Message);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return user;
        }

        // ==================== KIỂM TRA USERNAME ĐÃ TỒN TẠI ====================
        public bool CheckUsernameExists(string username)
        {
            SqlConnection connection = null;
            try
            {
                connection = dbConnect.GetConnection();
                string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);

                connection.Open();
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi kiểm tra username: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        // ==================== ĐĂNG KÝ NGƯỜI DÙNG MỚI ====================
        public bool Register(RegisterDTO registerDTO)
        {
            SqlConnection connection = null;
            try
            {
                connection = dbConnect.GetConnection();
                string query = @"INSERT INTO Users (FullName, Username, Password, Email, Phone, Address, Role) 
                                VALUES (@FullName, @Username, @Password, @Email, @Phone, @Address, 'Customer')";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FullName", registerDTO.FullName);
                command.Parameters.AddWithValue("@Username", registerDTO.Username);
                command.Parameters.AddWithValue("@Password", registerDTO.Password);
                command.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(registerDTO.Email) ? (object)DBNull.Value : registerDTO.Email);
                command.Parameters.AddWithValue("@Phone", string.IsNullOrWhiteSpace(registerDTO.Phone) ? (object)DBNull.Value : registerDTO.Phone);
                command.Parameters.AddWithValue("@Address", string.IsNullOrWhiteSpace(registerDTO.Address) ? (object)DBNull.Value : registerDTO.Address);

                connection.Open();
                int result = command.ExecuteNonQuery();
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi đăng ký DAL: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        // ==================== CẬP NHẬT THÔNG TIN CÁ NHÂN ====================
        public bool UpdateProfile(UpdateProfileDTO updateDTO)
        {
            SqlConnection connection = null;
            try
            {
                connection = dbConnect.GetConnection();
                string query = @"UPDATE Users 
                                SET FullName = @FullName, 
                                    Email = @Email, 
                                    Phone = @Phone, 
                                    Address = @Address 
                                WHERE UserID = @UserID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", updateDTO.UserID);
                command.Parameters.AddWithValue("@FullName", updateDTO.FullName);
                command.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(updateDTO.Email) ? (object)DBNull.Value : updateDTO.Email);
                command.Parameters.AddWithValue("@Phone", string.IsNullOrWhiteSpace(updateDTO.Phone) ? (object)DBNull.Value : updateDTO.Phone);
                command.Parameters.AddWithValue("@Address", string.IsNullOrWhiteSpace(updateDTO.Address) ? (object)DBNull.Value : updateDTO.Address);

                connection.Open();
                int result = command.ExecuteNonQuery();
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi cập nhật profile DAL: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        // ==================== ĐỔI MẬT KHẨU ====================
        public bool ChangePassword(ChangePasswordDTO changeDTO)
        {
            SqlConnection connection = null;
            try
            {
                connection = dbConnect.GetConnection();
                connection.Open();

                // Bước 1: Kiểm tra mật khẩu cũ có đúng không
                string checkQuery = "SELECT COUNT(*) FROM Users WHERE UserID = @UserID AND Password = @OldPassword";
                SqlCommand checkCmd = new SqlCommand(checkQuery, connection);
                checkCmd.Parameters.AddWithValue("@UserID", changeDTO.UserID);
                checkCmd.Parameters.AddWithValue("@OldPassword", changeDTO.OldPassword);

                int count = (int)checkCmd.ExecuteScalar();

                if (count == 0)
                    return false; // Mật khẩu cũ sai

                // Bước 2: Cập nhật mật khẩu mới
                string updateQuery = "UPDATE Users SET Password = @NewPassword WHERE UserID = @UserID";
                SqlCommand updateCmd = new SqlCommand(updateQuery, connection);
                updateCmd.Parameters.AddWithValue("@UserID", changeDTO.UserID);
                updateCmd.Parameters.AddWithValue("@NewPassword", changeDTO.NewPassword);

                int result = updateCmd.ExecuteNonQuery();
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi đổi mật khẩu DAL: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        // ==================== LẤY THÔNG TIN USER THEO ID ====================
        public UserDTO GetUserById(int userId)
        {
            UserDTO user = null;
            SqlConnection connection = null;
            SqlDataReader reader = null;

            try
            {
                connection = dbConnect.GetConnection();
                string query = "SELECT UserID, FullName, Username, Email, Phone, Address, Role, CreatedAt FROM Users WHERE UserID = @UserID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userId);

                connection.Open();
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    user = new UserDTO
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        FullName = reader["FullName"].ToString(),
                        Username = reader["Username"].ToString(),
                        Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : "",
                        Phone = reader["Phone"] != DBNull.Value ? reader["Phone"].ToString() : "",
                        Address = reader["Address"] != DBNull.Value ? reader["Address"].ToString() : "",
                        Role = reader["Role"].ToString(),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy thông tin user DAL: " + ex.Message);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return user;
        }

        // ==================== XÓA USER (CHỈ ADMIN) ====================
        public bool DeleteUser(int userId)
        {
            SqlConnection connection = null;
            try
            {
                connection = dbConnect.GetConnection();
                string query = "DELETE FROM Users WHERE UserID = @UserID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userId);

                connection.Open();
                int result = command.ExecuteNonQuery();
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi xóa user DAL: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        // ==================== LẤY TẤT CẢ USER (ADMIN) ====================
        public System.Collections.Generic.List<UserDTO> GetAllUsers()
        {
            System.Collections.Generic.List<UserDTO> users = new System.Collections.Generic.List<UserDTO>();
            SqlConnection connection = null;
            SqlDataReader reader = null;

            try
            {
                connection = dbConnect.GetConnection();
                string query = "SELECT UserID, FullName, Username, Email, Phone, Address, Role, CreatedAt FROM Users ORDER BY CreatedAt DESC";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    UserDTO user = new UserDTO
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        FullName = reader["FullName"].ToString(),
                        Username = reader["Username"].ToString(),
                        Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : "",
                        Phone = reader["Phone"] != DBNull.Value ? reader["Phone"].ToString() : "",
                        Address = reader["Address"] != DBNull.Value ? reader["Address"].ToString() : "",
                        Role = reader["Role"].ToString(),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                    };
                    users.Add(user);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy danh sách user DAL: " + ex.Message);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return users;
        }
        // Thêm vào DAL_CarSales/UserDAL.cs

        public bool UpdateUserRole(string username, string role)
        {
            SqlConnection connection = null;
            try
            {
                connection = dbConnect.GetConnection();
                string query = "UPDATE Users SET Role = @Role WHERE Username = @Username";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Role", role);
                command.Parameters.AddWithValue("@Username", username);

                connection.Open();
                int result = command.ExecuteNonQuery();
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi cập nhật role: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

    }
}