using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DTO_Carsales;

namespace DAL_CarSales
{
    public class CarDAL
    {
        private DbConnect dbConnect;

        public CarDAL()
        {
            dbConnect = new DbConnect();
        }

        // ==================== LẤY TẤT CẢ XE ====================
        public List<CarDTO> GetAllCars()
        {
            List<CarDTO> cars = new List<CarDTO>();
            SqlConnection connection = null;
            SqlDataReader reader = null;

            try
            {
                connection = dbConnect.GetConnection();
                string query = @"SELECT c.CarID, c.CarName, c.Price, c.CarTypeID, 
                                ct.CarTypeName, c.StockQuantity, c.Status, c.ImagePath
                                FROM Cars c
                                LEFT JOIN CarTypes ct ON c.CarTypeID = ct.CarTypeID
                                ORDER BY c.CarID DESC";

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CarDTO car = new CarDTO
                    {
                        CarID = Convert.ToInt32(reader["CarID"]),
                        CarName = reader["CarName"].ToString(),
                        Price = Convert.ToDecimal(reader["Price"]),
                        CarTypeID = Convert.ToInt32(reader["CarTypeID"]),
                        CarTypeName = reader["CarTypeName"] != DBNull.Value ? reader["CarTypeName"].ToString() : "",
                        StockQuantity = Convert.ToInt32(reader["StockQuantity"]),
                        Status = reader["Status"].ToString(),
                        ImagePath = reader["ImagePath"] != DBNull.Value ? reader["ImagePath"].ToString() : ""
                    };
                    cars.Add(car);
                }

                return cars;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy danh sách xe: " + ex.Message);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        // ==================== TÌM KIẾM XE ====================
        public List<CarDTO> SearchCars(string keyword, int? carTypeID, string status)
        {
            List<CarDTO> cars = new List<CarDTO>();
            SqlConnection connection = null;
            SqlDataReader reader = null;

            try
            {
                connection = dbConnect.GetConnection();
                string query = @"SELECT c.CarID, c.CarName, c.Price, c.CarTypeID, 
                                ct.CarTypeName, c.StockQuantity, c.Status, c.ImagePath
                                FROM Cars c
                                LEFT JOIN CarTypes ct ON c.CarTypeID = ct.CarTypeID
                                WHERE 1=1";

                if (!string.IsNullOrWhiteSpace(keyword))
                    query += " AND c.CarName LIKE @Keyword";

                if (carTypeID.HasValue && carTypeID.Value > 0)
                    query += " AND c.CarTypeID = @CarTypeID";

                if (!string.IsNullOrWhiteSpace(status))
                    query += " AND c.Status = @Status";

                query += " ORDER BY c.CarID DESC";

                SqlCommand command = new SqlCommand(query, connection);

                if (!string.IsNullOrWhiteSpace(keyword))
                    command.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");

                if (carTypeID.HasValue && carTypeID.Value > 0)
                    command.Parameters.AddWithValue("@CarTypeID", carTypeID.Value);

                if (!string.IsNullOrWhiteSpace(status))
                    command.Parameters.AddWithValue("@Status", status);

                connection.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CarDTO car = new CarDTO
                    {
                        CarID = Convert.ToInt32(reader["CarID"]),
                        CarName = reader["CarName"].ToString(),
                        Price = Convert.ToDecimal(reader["Price"]),
                        CarTypeID = Convert.ToInt32(reader["CarTypeID"]),
                        CarTypeName = reader["CarTypeName"] != DBNull.Value ? reader["CarTypeName"].ToString() : "",
                        StockQuantity = Convert.ToInt32(reader["StockQuantity"]),
                        Status = reader["Status"].ToString(),
                        ImagePath = reader["ImagePath"] != DBNull.Value ? reader["ImagePath"].ToString() : ""
                    };
                    cars.Add(car);
                }

                return cars;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tìm kiếm xe: " + ex.Message);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        // ==================== LẤY XE THEO ID ====================
        public CarDTO GetCarById(int carId)
        {
            CarDTO car = null;
            SqlConnection connection = null;
            SqlDataReader reader = null;

            try
            {
                connection = dbConnect.GetConnection();
                string query = @"SELECT c.CarID, c.CarName, c.Price, c.CarTypeID, 
                                ct.CarTypeName, c.StockQuantity, c.Status, c.ImagePath
                                FROM Cars c
                                LEFT JOIN CarTypes ct ON c.CarTypeID = ct.CarTypeID
                                WHERE c.CarID = @CarID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CarID", carId);

                connection.Open();
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    car = new CarDTO
                    {
                        CarID = Convert.ToInt32(reader["CarID"]),
                        CarName = reader["CarName"].ToString(),
                        Price = Convert.ToDecimal(reader["Price"]),
                        CarTypeID = Convert.ToInt32(reader["CarTypeID"]),
                        CarTypeName = reader["CarTypeName"] != DBNull.Value ? reader["CarTypeName"].ToString() : "",
                        StockQuantity = Convert.ToInt32(reader["StockQuantity"]),
                        Status = reader["Status"].ToString(),
                        ImagePath = reader["ImagePath"] != DBNull.Value ? reader["ImagePath"].ToString() : ""
                    };
                }

                return car;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy thông tin xe: " + ex.Message);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        // ==================== THÊM XE MỚI ====================
        public bool AddCar(AddCarDTO carDTO)
        {
            SqlConnection connection = null;
            try
            {
                connection = dbConnect.GetConnection();
                string query = @"INSERT INTO Cars (CarName, Price, CarTypeID, StockQuantity, Status, ImagePath)
                                VALUES (@CarName, @Price, @CarTypeID, @StockQuantity, @Status, @ImagePath)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CarName", carDTO.CarName);
                command.Parameters.AddWithValue("@Price", carDTO.Price);
                command.Parameters.AddWithValue("@CarTypeID", carDTO.CarTypeID);
                command.Parameters.AddWithValue("@StockQuantity", carDTO.StockQuantity);
                command.Parameters.AddWithValue("@Status", carDTO.Status);
                command.Parameters.AddWithValue("@ImagePath", string.IsNullOrWhiteSpace(carDTO.ImagePath) ? (object)DBNull.Value : carDTO.ImagePath);

                connection.Open();
                int result = command.ExecuteNonQuery();
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi thêm xe: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        // ==================== CẬP NHẬT XE ====================
        public bool UpdateCar(CarDTO carDTO)
        {
            SqlConnection connection = null;
            try
            {
                connection = dbConnect.GetConnection();
                string query = @"UPDATE Cars 
                                SET CarName = @CarName, 
                                    Price = @Price, 
                                    CarTypeID = @CarTypeID, 
                                    StockQuantity = @StockQuantity, 
                                    Status = @Status, 
                                    ImagePath = @ImagePath
                                WHERE CarID = @CarID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CarID", carDTO.CarID);
                command.Parameters.AddWithValue("@CarName", carDTO.CarName);
                command.Parameters.AddWithValue("@Price", carDTO.Price);
                command.Parameters.AddWithValue("@CarTypeID", carDTO.CarTypeID);
                command.Parameters.AddWithValue("@StockQuantity", carDTO.StockQuantity);
                command.Parameters.AddWithValue("@Status", carDTO.Status);
                command.Parameters.AddWithValue("@ImagePath", string.IsNullOrWhiteSpace(carDTO.ImagePath) ? (object)DBNull.Value : carDTO.ImagePath);

                connection.Open();
                int result = command.ExecuteNonQuery();
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi cập nhật xe: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        // ==================== XÓA XE ====================
        public bool DeleteCar(int carId)
        {
            SqlConnection connection = null;
            try
            {
                connection = dbConnect.GetConnection();

                // Kiểm tra xe có trong đơn hàng chưa
                string checkQuery = "SELECT COUNT(*) FROM OrderItems WHERE CarID = @CarID";
                SqlCommand checkCmd = new SqlCommand(checkQuery, connection);
                checkCmd.Parameters.AddWithValue("@CarID", carId);

                connection.Open();
                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    throw new Exception("Không thể xóa xe đã có trong đơn hàng!");
                }

                // Xóa xe
                string deleteQuery = "DELETE FROM Cars WHERE CarID = @CarID";
                SqlCommand deleteCmd = new SqlCommand(deleteQuery, connection);
                deleteCmd.Parameters.AddWithValue("@CarID", carId);

                int result = deleteCmd.ExecuteNonQuery();
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi xóa xe: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        // ==================== LẤY TẤT CẢ LOẠI XE ====================
        public List<CarTypeDTO> GetAllCarTypes()
        {
            List<CarTypeDTO> carTypes = new List<CarTypeDTO>();
            SqlConnection connection = null;
            SqlDataReader reader = null;

            try
            {
                connection = dbConnect.GetConnection();
                string query = "SELECT CarTypeID, CarTypeName FROM CarTypes ORDER BY CarTypeName";

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CarTypeDTO carType = new CarTypeDTO
                    {
                        CarTypeID = Convert.ToInt32(reader["CarTypeID"]),
                        CarTypeName = reader["CarTypeName"].ToString()
                    };
                    carTypes.Add(carType);
                }

                return carTypes;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy loại xe: " + ex.Message);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        // ==================== CẬP NHẬT SỐ LƯỢNG TỒN KHO ====================
        public bool UpdateStock(int carId, int quantity)
        {
            SqlConnection connection = null;
            try
            {
                connection = dbConnect.GetConnection();
                string query = "UPDATE Cars SET StockQuantity = @Quantity WHERE CarID = @CarID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CarID", carId);
                command.Parameters.AddWithValue("@Quantity", quantity);

                connection.Open();
                int result = command.ExecuteNonQuery();
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi cập nhật tồn kho: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }
    }
}