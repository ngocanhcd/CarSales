using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DTO_Carsales;

namespace DAL_CarSales
{
    public class OrderDAL
    {
        private DbConnect dbConnect;

        public OrderDAL()
        {
            dbConnect = new DbConnect();
        }

        // ==================== LẤY TẤT CẢ ĐƠN HÀNG ====================
        public List<OrderDTO> GetAllOrders()
        {
            List<OrderDTO> orders = new List<OrderDTO>();
            SqlConnection connection = null;
            SqlDataReader reader = null;

            try
            {
                connection = dbConnect.GetConnection();
                string query = @"SELECT o.OrderID, o.UserID, o.OrderDate, o.TotalAmount, o.Status,
                                u.FullName as CustomerName
                                FROM Orders o
                                LEFT JOIN Users u ON o.UserID = u.UserID
                                ORDER BY o.OrderDate DESC";

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    OrderDTO order = new OrderDTO
                    {
                        OrderID = Convert.ToInt32(reader["OrderID"]),
                        UserID = Convert.ToInt32(reader["UserID"]),
                        OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                        TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                        Status = reader["Status"].ToString(),
                        CustomerName = reader["CustomerName"] != DBNull.Value ? reader["CustomerName"].ToString() : "N/A"
                    };
                    orders.Add(order);
                }

                return orders;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy danh sách đơn hàng: " + ex.Message);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        // ==================== TÌM KIẾM ĐƠN HÀNG ====================
        public List<OrderDTO> SearchOrders(string keyword, string status, DateTime? fromDate, DateTime? toDate)
        {
            List<OrderDTO> orders = new List<OrderDTO>();
            SqlConnection connection = null;
            SqlDataReader reader = null;

            try
            {
                connection = dbConnect.GetConnection();
                string query = @"SELECT o.OrderID, o.UserID, o.OrderDate, o.TotalAmount, o.Status,
                                u.FullName as CustomerName
                                FROM Orders o
                                LEFT JOIN Users u ON o.UserID = u.UserID
                                WHERE 1=1";

                // Tìm theo tên khách hàng hoặc OrderID
                if (!string.IsNullOrWhiteSpace(keyword))
                    query += " AND (u.FullName LIKE @Keyword OR CAST(o.OrderID AS VARCHAR) LIKE @Keyword)";

                // Lọc theo trạng thái
                if (!string.IsNullOrWhiteSpace(status))
                    query += " AND o.Status = @Status";

                // Lọc theo ngày
                if (fromDate.HasValue)
                    query += " AND CAST(o.OrderDate AS DATE) >= @FromDate";

                if (toDate.HasValue)
                    query += " AND CAST(o.OrderDate AS DATE) <= @ToDate";

                query += " ORDER BY o.OrderDate DESC";

                SqlCommand command = new SqlCommand(query, connection);

                if (!string.IsNullOrWhiteSpace(keyword))
                    command.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");

                if (!string.IsNullOrWhiteSpace(status))
                    command.Parameters.AddWithValue("@Status", status);

                if (fromDate.HasValue)
                    command.Parameters.AddWithValue("@FromDate", fromDate.Value.Date);

                if (toDate.HasValue)
                    command.Parameters.AddWithValue("@ToDate", toDate.Value.Date);

                connection.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    OrderDTO order = new OrderDTO
                    {
                        OrderID = Convert.ToInt32(reader["OrderID"]),
                        UserID = Convert.ToInt32(reader["UserID"]),
                        OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                        TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                        Status = reader["Status"].ToString(),
                        CustomerName = reader["CustomerName"] != DBNull.Value ? reader["CustomerName"].ToString() : "N/A"
                    };
                    orders.Add(order);
                }

                return orders;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tìm kiếm đơn hàng: " + ex.Message);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        // ==================== LẤY ĐƠN HÀNG THEO ID ====================
        public OrderDTO GetOrderById(int orderId)
        {
            OrderDTO order = null;
            SqlConnection connection = null;
            SqlDataReader reader = null;

            try
            {
                connection = dbConnect.GetConnection();
                string query = @"SELECT o.OrderID, o.UserID, o.OrderDate, o.TotalAmount, o.Status,
                                u.FullName as CustomerName
                                FROM Orders o
                                LEFT JOIN Users u ON o.UserID = u.UserID
                                WHERE o.OrderID = @OrderID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@OrderID", orderId);

                connection.Open();
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    order = new OrderDTO
                    {
                        OrderID = Convert.ToInt32(reader["OrderID"]),
                        UserID = Convert.ToInt32(reader["UserID"]),
                        OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                        TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                        Status = reader["Status"].ToString(),
                        CustomerName = reader["CustomerName"] != DBNull.Value ? reader["CustomerName"].ToString() : "N/A"
                    };
                }

                return order;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy thông tin đơn hàng: " + ex.Message);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        // ==================== LẤY CHI TIẾT ĐƠN HÀNG ====================
        public List<OrderItemDTO> GetOrderItems(int orderId)
        {
            List<OrderItemDTO> items = new List<OrderItemDTO>();
            SqlConnection connection = null;
            SqlDataReader reader = null;

            try
            {
                connection = dbConnect.GetConnection();
                string query = @"SELECT oi.OrderItemID, oi.OrderID, oi.CarID, oi.Quantity, 
                                oi.UnitPrice, oi.Discount,
                                c.CarName,
                                (oi.Quantity * oi.UnitPrice - oi.Discount) as TotalPrice
                                FROM OrderItems oi
                                LEFT JOIN Cars c ON oi.CarID = c.CarID
                                WHERE oi.OrderID = @OrderID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@OrderID", orderId);

                connection.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    OrderItemDTO item = new OrderItemDTO
                    {
                        OrderItemID = Convert.ToInt32(reader["OrderItemID"]),
                        OrderID = Convert.ToInt32(reader["OrderID"]),
                        CarID = Convert.ToInt32(reader["CarID"]),
                        CarName = reader["CarName"] != DBNull.Value ? reader["CarName"].ToString() : "",
                        Quantity = Convert.ToInt32(reader["Quantity"]),
                        UnitPrice = Convert.ToDecimal(reader["UnitPrice"]),
                        Discount = Convert.ToDecimal(reader["Discount"]),
                        TotalPrice = Convert.ToDecimal(reader["TotalPrice"])
                    };
                    items.Add(item);
                }

                return items;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy chi tiết đơn hàng: " + ex.Message);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        // ==================== CẬP NHẬT TRẠNG THÁI ĐƠN HÀNG ====================
        public bool UpdateOrderStatus(int orderId, string status)
        {
            SqlConnection connection = null;
            try
            {
                connection = dbConnect.GetConnection();
                string query = "UPDATE Orders SET Status = @Status WHERE OrderID = @OrderID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@OrderID", orderId);
                command.Parameters.AddWithValue("@Status", status);

                connection.Open();
                int result = command.ExecuteNonQuery();
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi cập nhật trạng thái: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        // ==================== HỦY ĐƠN HÀNG ====================
        public bool CancelOrder(int orderId)
        {
            SqlConnection connection = null;
            SqlTransaction transaction = null;

            try
            {
                connection = dbConnect.GetConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                // Cập nhật trạng thái đơn hàng
                string updateOrderQuery = "UPDATE Orders SET Status = 'Cancelled' WHERE OrderID = @OrderID";
                SqlCommand updateOrderCmd = new SqlCommand(updateOrderQuery, connection, transaction);
                updateOrderCmd.Parameters.AddWithValue("@OrderID", orderId);
                updateOrderCmd.ExecuteNonQuery();

                // Hoàn trả số lượng xe về kho
                string restoreStockQuery = @"UPDATE Cars 
                                            SET StockQuantity = StockQuantity + oi.Quantity
                                            FROM Cars c
                                            INNER JOIN OrderItems oi ON c.CarID = oi.CarID
                                            WHERE oi.OrderID = @OrderID";

                SqlCommand restoreStockCmd = new SqlCommand(restoreStockQuery, connection, transaction);
                restoreStockCmd.Parameters.AddWithValue("@OrderID", orderId);
                restoreStockCmd.ExecuteNonQuery();

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                if (transaction != null)
                    transaction.Rollback();

                throw new Exception("Lỗi hủy đơn hàng: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        // ==================== XÓA ĐƠN HÀNG (ADMIN) ====================
        public bool DeleteOrder(int orderId)
        {
            SqlConnection connection = null;
            SqlTransaction transaction = null;

            try
            {
                connection = dbConnect.GetConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                // Xóa payments
                string deletePaymentsQuery = "DELETE FROM Payments WHERE OrderID = @OrderID";
                SqlCommand deletePaymentsCmd = new SqlCommand(deletePaymentsQuery, connection, transaction);
                deletePaymentsCmd.Parameters.AddWithValue("@OrderID", orderId);
                deletePaymentsCmd.ExecuteNonQuery();

                // Xóa order items
                string deleteItemsQuery = "DELETE FROM OrderItems WHERE OrderID = @OrderID";
                SqlCommand deleteItemsCmd = new SqlCommand(deleteItemsQuery, connection, transaction);
                deleteItemsCmd.Parameters.AddWithValue("@OrderID", orderId);
                deleteItemsCmd.ExecuteNonQuery();

                // Xóa order
                string deleteOrderQuery = "DELETE FROM Orders WHERE OrderID = @OrderID";
                SqlCommand deleteOrderCmd = new SqlCommand(deleteOrderQuery, connection, transaction);
                deleteOrderCmd.Parameters.AddWithValue("@OrderID", orderId);
                int result = deleteOrderCmd.ExecuteNonQuery();

                transaction.Commit();
                return result > 0;
            }
            catch (Exception ex)
            {
                if (transaction != null)
                    transaction.Rollback();

                throw new Exception("Lỗi xóa đơn hàng: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        // ==================== LẤY PAYMENT THEO ORDER ====================
        public PaymentDTO GetPaymentByOrderId(int orderId)
        {
            PaymentDTO payment = null;
            SqlConnection connection = null;
            SqlDataReader reader = null;

            try
            {
                connection = dbConnect.GetConnection();
                string query = @"SELECT PaymentID, OrderID, PaymentMethod, PaymentStatus, 
                                Amount, PaymentDate
                                FROM Payments
                                WHERE OrderID = @OrderID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@OrderID", orderId);

                connection.Open();
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    payment = new PaymentDTO
                    {
                        PaymentID = Convert.ToInt32(reader["PaymentID"]),
                        OrderID = Convert.ToInt32(reader["OrderID"]),
                        PaymentMethod = reader["PaymentMethod"].ToString(),
                        PaymentStatus = reader["PaymentStatus"].ToString(),
                        Amount = Convert.ToDecimal(reader["Amount"]),
                        PaymentDate = Convert.ToDateTime(reader["PaymentDate"])
                    };
                }

                return payment;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy thông tin thanh toán: " + ex.Message);
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