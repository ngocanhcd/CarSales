using System;
using System.Collections.Generic;
using DAL_CarSales;
using DTO_Carsales;

namespace BLL_CarSales
{
    public class OrderBLL
    {
        private OrderDAL orderDAL;

        public OrderBLL()
        {
            orderDAL = new OrderDAL();
        }

        // ==================== LẤY TẤT CẢ ĐƠN HÀNG ====================
        public ApiResponse<List<OrderDTO>> GetAllOrders()
        {
            try
            {
                List<OrderDTO> orders = orderDAL.GetAllOrders();
                return new ApiResponse<List<OrderDTO>>
                {
                    Success = true,
                    Message = "Lấy danh sách đơn hàng thành công",
                    Data = orders
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<OrderDTO>>
                {
                    Success = false,
                    Message = "Lỗi: " + ex.Message,
                    Data = null
                };
            }
        }

        // ==================== TÌM KIẾM ĐƠN HÀNG ====================
        public ApiResponse<List<OrderDTO>> SearchOrders(string keyword, string status, DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                // Validation
                if (fromDate.HasValue && toDate.HasValue && fromDate > toDate)
                {
                    return new ApiResponse<List<OrderDTO>>
                    {
                        Success = false,
                        Message = "Ngày bắt đầu không thể lớn hơn ngày kết thúc!",
                        Data = null
                    };
                }

                List<OrderDTO> orders = orderDAL.SearchOrders(keyword, status, fromDate, toDate);
                return new ApiResponse<List<OrderDTO>>
                {
                    Success = true,
                    Message = $"Tìm thấy {orders.Count} đơn hàng",
                    Data = orders
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<OrderDTO>>
                {
                    Success = false,
                    Message = "Lỗi: " + ex.Message,
                    Data = null
                };
            }
        }

        // ==================== LẤY ĐƠN HÀNG THEO ID ====================
        public ApiResponse<OrderDTO> GetOrderById(int orderId)
        {
            try
            {
                if (orderId <= 0)
                {
                    return new ApiResponse<OrderDTO>
                    {
                        Success = false,
                        Message = "ID đơn hàng không hợp lệ!",
                        Data = null
                    };
                }

                OrderDTO order = orderDAL.GetOrderById(orderId);

                if (order != null)
                {
                    return new ApiResponse<OrderDTO>
                    {
                        Success = true,
                        Message = "Lấy thông tin đơn hàng thành công",
                        Data = order
                    };
                }
                else
                {
                    return new ApiResponse<OrderDTO>
                    {
                        Success = false,
                        Message = "Không tìm thấy đơn hàng",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<OrderDTO>
                {
                    Success = false,
                    Message = "Lỗi: " + ex.Message,
                    Data = null
                };
            }
        }

        // ==================== LẤY CHI TIẾT ĐƠN HÀNG ====================
        public ApiResponse<List<OrderItemDTO>> GetOrderItems(int orderId)
        {
            try
            {
                if (orderId <= 0)
                {
                    return new ApiResponse<List<OrderItemDTO>>
                    {
                        Success = false,
                        Message = "ID đơn hàng không hợp lệ!",
                        Data = null
                    };
                }

                List<OrderItemDTO> items = orderDAL.GetOrderItems(orderId);
                return new ApiResponse<List<OrderItemDTO>>
                {
                    Success = true,
                    Message = "Lấy chi tiết đơn hàng thành công",
                    Data = items
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<OrderItemDTO>>
                {
                    Success = false,
                    Message = "Lỗi: " + ex.Message,
                    Data = null
                };
            }
        }

        // ==================== CẬP NHẬT TRẠNG THÁI ====================
        public ApiResponse UpdateOrderStatus(int orderId, string status)
        {
            try
            {
                // Validation
                if (orderId <= 0)
                    return new ApiResponse { Success = false, Message = "ID đơn hàng không hợp lệ!" };

                if (string.IsNullOrWhiteSpace(status))
                    return new ApiResponse { Success = false, Message = "Trạng thái không được để trống!" };

                // Kiểm tra trạng thái hợp lệ
                string[] validStatuses = { "Pending", "Processing", "Shipped", "Completed", "Cancelled" };
                bool isValidStatus = false;
                foreach (string s in validStatuses)
                {
                    if (s.Equals(status, StringComparison.OrdinalIgnoreCase))
                    {
                        isValidStatus = true;
                        break;
                    }
                }

                if (!isValidStatus)
                    return new ApiResponse { Success = false, Message = "Trạng thái không hợp lệ!" };

                // Cập nhật
                bool result = orderDAL.UpdateOrderStatus(orderId, status);

                if (result)
                {
                    return new ApiResponse
                    {
                        Success = true,
                        Message = "Cập nhật trạng thái thành công!"
                    };
                }
                else
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "Cập nhật trạng thái thất bại!"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Lỗi: " + ex.Message
                };
            }
        }

        // ==================== HỦY ĐƠN HÀNG ====================
        public ApiResponse CancelOrder(int orderId)
        {
            try
            {
                if (orderId <= 0)
                    return new ApiResponse { Success = false, Message = "ID đơn hàng không hợp lệ!" };

                // Kiểm tra trạng thái đơn hàng
                OrderDTO order = orderDAL.GetOrderById(orderId);

                if (order == null)
                    return new ApiResponse { Success = false, Message = "Không tìm thấy đơn hàng!" };

                if (order.Status == "Cancelled")
                    return new ApiResponse { Success = false, Message = "Đơn hàng đã bị hủy trước đó!" };

                if (order.Status == "Completed")
                    return new ApiResponse { Success = false, Message = "Không thể hủy đơn hàng đã hoàn thành!" };

                // Hủy đơn hàng
                bool result = orderDAL.CancelOrder(orderId);

                if (result)
                {
                    return new ApiResponse
                    {
                        Success = true,
                        Message = "Hủy đơn hàng thành công! Đã hoàn trả số lượng xe về kho."
                    };
                }
                else
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "Hủy đơn hàng thất bại!"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Lỗi: " + ex.Message
                };
            }
        }

        // ==================== XÓA ĐƠN HÀNG ====================
        public ApiResponse DeleteOrder(int orderId)
        {
            try
            {
                if (orderId <= 0)
                    return new ApiResponse { Success = false, Message = "ID đơn hàng không hợp lệ!" };

                // Kiểm tra trạng thái
                OrderDTO order = orderDAL.GetOrderById(orderId);

                if (order == null)
                    return new ApiResponse { Success = false, Message = "Không tìm thấy đơn hàng!" };

                if (order.Status != "Cancelled")
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "Chỉ có thể xóa đơn hàng đã bị hủy!"
                    };
                }

                // Xóa
                bool result = orderDAL.DeleteOrder(orderId);

                if (result)
                {
                    return new ApiResponse
                    {
                        Success = true,
                        Message = "Xóa đơn hàng thành công!"
                    };
                }
                else
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "Xóa đơn hàng thất bại!"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Lỗi: " + ex.Message
                };
            }
        }

        // ==================== LẤY PAYMENT ====================
        public ApiResponse<PaymentDTO> GetPaymentByOrderId(int orderId)
        {
            try
            {
                if (orderId <= 0)
                {
                    return new ApiResponse<PaymentDTO>
                    {
                        Success = false,
                        Message = "ID đơn hàng không hợp lệ!",
                        Data = null
                    };
                }

                PaymentDTO payment = orderDAL.GetPaymentByOrderId(orderId);

                if (payment != null)
                {
                    return new ApiResponse<PaymentDTO>
                    {
                        Success = true,
                        Message = "Lấy thông tin thanh toán thành công",
                        Data = payment
                    };
                }
                else
                {
                    return new ApiResponse<PaymentDTO>
                    {
                        Success = false,
                        Message = "Chưa có thông tin thanh toán",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<PaymentDTO>
                {
                    Success = false,
                    Message = "Lỗi: " + ex.Message,
                    Data = null
                };
            }
        }
    }
}