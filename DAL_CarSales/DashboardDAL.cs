using System;
using System.Data;
using System.Data.SqlClient;
using DTO_Carsales;

namespace DAL_CarSales
{
    public class DashboardDAL
    {
        private DbConnect dbConnect;

        public DashboardDAL()
        {
            dbConnect = new DbConnect();
        }

        // ==================== THỐNG KÊ TỔNG QUAN ====================
        public DashboardStatsDTO GetDashboardStats()
        {
            DashboardStatsDTO stats = new DashboardStatsDTO();
            SqlConnection connection = null;

            try
            {
                connection = dbConnect.GetConnection();
                connection.Open();

                // Tổng số người dùng
                string queryUsers = "SELECT COUNT(*) FROM Users";
                SqlCommand cmdUsers = new SqlCommand(queryUsers, connection);
                stats.TotalUsers = (int)cmdUsers.ExecuteScalar();

                // Tổng số xe
                string queryCars = "SELECT COUNT(*) FROM Cars";
                SqlCommand cmdCars = new SqlCommand(queryCars, connection);
                stats.TotalCars = (int)cmdCars.ExecuteScalar();

                // Tổng số đơn hàng
                string queryOrders = "SELECT COUNT(*) FROM Orders";
                SqlCommand cmdOrders = new SqlCommand(queryOrders, connection);
                stats.TotalOrders = (int)cmdOrders.ExecuteScalar();

                // Tổng doanh thu
                string queryRevenue = "SELECT ISNULL(SUM(TotalAmount), 0) FROM Orders WHERE Status != 'Cancelled'";
                SqlCommand cmdRevenue = new SqlCommand(queryRevenue, connection);
                stats.TotalRevenue = (decimal)cmdRevenue.ExecuteScalar();

                // Đơn hàng hôm nay
                string queryTodayOrders = "SELECT COUNT(*) FROM Orders WHERE CAST(OrderDate AS DATE) = CAST(GETDATE() AS DATE)";
                SqlCommand cmdTodayOrders = new SqlCommand(queryTodayOrders, connection);
                stats.TodayOrders = (int)cmdTodayOrders.ExecuteScalar();

                // Doanh thu hôm nay
                string queryTodayRevenue = "SELECT ISNULL(SUM(TotalAmount), 0) FROM Orders WHERE CAST(OrderDate AS DATE) = CAST(GETDATE() AS DATE) AND Status != 'Cancelled'";
                SqlCommand cmdTodayRevenue = new SqlCommand(queryTodayRevenue, connection);
                stats.TodayRevenue = (decimal)cmdTodayRevenue.ExecuteScalar();

                // Đơn hàng đang xử lý
                string queryPendingOrders = "SELECT COUNT(*) FROM Orders WHERE Status IN ('Pending', 'Processing')";
                SqlCommand cmdPendingOrders = new SqlCommand(queryPendingOrders, connection);
                stats.PendingOrders = (int)cmdPendingOrders.ExecuteScalar();

                // Xe còn trong kho
                string queryInStock = "SELECT COUNT(*) FROM Cars WHERE Status = 'Available' AND StockQuantity > 0";
                SqlCommand cmdInStock = new SqlCommand(queryInStock, connection);
                stats.CarsInStock = (int)cmdInStock.ExecuteScalar();

                // Xe hết hàng
                string queryOutOfStock = "SELECT COUNT(*) FROM Cars WHERE StockQuantity = 0";
                SqlCommand cmdOutOfStock = new SqlCommand(queryOutOfStock, connection);
                stats.CarsOutOfStock = (int)cmdOutOfStock.ExecuteScalar();

                return stats;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy thống kê dashboard: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        // ==================== DOANH THU THEO THÁNG (12 THÁNG) ====================
        public System.Collections.Generic.List<MonthlyRevenueDTO> GetMonthlyRevenue()
        {
            System.Collections.Generic.List<MonthlyRevenueDTO> revenueList = new System.Collections.Generic.List<MonthlyRevenueDTO>();
            SqlConnection connection = null;
            SqlDataReader reader = null;

            try
            {
                connection = dbConnect.GetConnection();
                string query = @"
                    SELECT 
                        MONTH(OrderDate) as Month,
                        YEAR(OrderDate) as Year,
                        ISNULL(SUM(TotalAmount), 0) as Revenue
                    FROM Orders
                    WHERE OrderDate >= DATEADD(MONTH, -11, GETDATE())
                        AND Status != 'Cancelled'
                    GROUP BY YEAR(OrderDate), MONTH(OrderDate)
                    ORDER BY YEAR(OrderDate), MONTH(OrderDate)";

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    MonthlyRevenueDTO revenue = new MonthlyRevenueDTO
                    {
                        Month = Convert.ToInt32(reader["Month"]),
                        Year = Convert.ToInt32(reader["Year"]),
                        Revenue = Convert.ToDecimal(reader["Revenue"])
                    };
                    revenueList.Add(revenue);
                }

                return revenueList;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy doanh thu theo tháng: " + ex.Message);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        // ==================== TOP 5 XE BÁN CHẠY ====================
        public System.Collections.Generic.List<TopSellingCarDTO> GetTopSellingCars(int top = 5)
        {
            System.Collections.Generic.List<TopSellingCarDTO> topCars = new System.Collections.Generic.List<TopSellingCarDTO>();
            SqlConnection connection = null;
            SqlDataReader reader = null;

            try
            {
                connection = dbConnect.GetConnection();
                string query = @"
                    SELECT TOP (@Top)
                        c.CarID,
                        c.CarName,
                        ct.CarTypeName,
                        c.Price,
                        ISNULL(SUM(oi.Quantity), 0) as TotalSold,
                        ISNULL(SUM(oi.Quantity * oi.UnitPrice), 0) as TotalRevenue
                    FROM Cars c
                    LEFT JOIN CarTypes ct ON c.CarTypeID = ct.CarTypeID
                    LEFT JOIN OrderItems oi ON c.CarID = oi.CarID
                    LEFT JOIN Orders o ON oi.OrderID = o.OrderID
                    WHERE o.Status != 'Cancelled' OR o.Status IS NULL
                    GROUP BY c.CarID, c.CarName, ct.CarTypeName, c.Price
                    ORDER BY TotalSold DESC";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Top", top);

                connection.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    TopSellingCarDTO car = new TopSellingCarDTO
                    {
                        CarID = Convert.ToInt32(reader["CarID"]),
                        CarName = reader["CarName"].ToString(),
                        CarTypeName = reader["CarTypeName"] != DBNull.Value ? reader["CarTypeName"].ToString() : "",
                        Price = Convert.ToDecimal(reader["Price"]),
                        TotalSold = Convert.ToInt32(reader["TotalSold"]),
                        TotalRevenue = Convert.ToDecimal(reader["TotalRevenue"])
                    };
                    topCars.Add(car);
                }

                return topCars;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy top xe bán chạy: " + ex.Message);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        // ==================== ĐỐN HÀNG GẦN ĐÂY ====================
        public System.Collections.Generic.List<RecentOrderDTO> GetRecentOrders(int top = 10)
        {
            System.Collections.Generic.List<RecentOrderDTO> orders = new System.Collections.Generic.List<RecentOrderDTO>();
            SqlConnection connection = null;
            SqlDataReader reader = null;

            try
            {
                connection = dbConnect.GetConnection();
                string query = @"
                    SELECT TOP (@Top)
                        o.OrderID,
                        o.OrderDate,
                        u.FullName as CustomerName,
                        o.TotalAmount,
                        o.Status
                    FROM Orders o
                    LEFT JOIN Users u ON o.UserID = u.UserID
                    ORDER BY o.OrderDate DESC";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Top", top);

                connection.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    RecentOrderDTO order = new RecentOrderDTO
                    {
                        OrderID = Convert.ToInt32(reader["OrderID"]),
                        OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                        CustomerName = reader["CustomerName"] != DBNull.Value ? reader["CustomerName"].ToString() : "N/A",
                        TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                        Status = reader["Status"].ToString()
                    };
                    orders.Add(order);
                }

                return orders;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy đơn hàng gần đây: " + ex.Message);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        // ==================== THỐNG KÊ THEO TRẠNG THÁI ĐƠN HÀNG ====================
        public System.Collections.Generic.Dictionary<string, int> GetOrderStatusStats()
        {
            System.Collections.Generic.Dictionary<string, int> stats = new System.Collections.Generic.Dictionary<string, int>();
            SqlConnection connection = null;
            SqlDataReader reader = null;

            try
            {
                connection = dbConnect.GetConnection();
                string query = @"
                    SELECT Status, COUNT(*) as Count
                    FROM Orders
                    GROUP BY Status";

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string status = reader["Status"].ToString();
                    int count = Convert.ToInt32(reader["Count"]);
                    stats.Add(status, count);
                }

                return stats;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy thống kê trạng thái đơn hàng: " + ex.Message);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }
    }
}