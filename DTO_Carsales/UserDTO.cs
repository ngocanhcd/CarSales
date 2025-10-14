using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_Carsales
{
    public class UserDTO
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class LoginDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class RegisterDTO
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }

    public class ChangePasswordDTO
    {
        public int UserID { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class UpdateProfileDTO
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }

    // ==================== CAR DTOs ====================
    public class CarDTO
    {
        public int CarID { get; set; }
        public string CarName { get; set; }
        public decimal Price { get; set; }
        public int CarTypeID { get; set; }
        public string CarTypeName { get; set; }
        public int StockQuantity { get; set; }
        public string Status { get; set; }
        public string ImagePath { get; set; }
    }

    public class CarTypeDTO
    {
        public int CarTypeID { get; set; }
        public string CarTypeName { get; set; }
    }

    public class AddCarDTO
    {
        public string CarName { get; set; }
        public decimal Price { get; set; }
        public int CarTypeID { get; set; }
        public int StockQuantity { get; set; }
        public string Status { get; set; }
        public string ImagePath { get; set; }
    }

    // ==================== ORDER DTOs ====================
    public class OrderDTO
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public string CustomerName { get; set; }
    }

    public class OrderItemDTO
    {
        public int OrderItemID { get; set; }
        public int OrderID { get; set; }
        public int CarID { get; set; }
        public string CarName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class CreateOrderDTO
    {
        public int UserID { get; set; }
        public System.Collections.Generic.List<OrderItemDTO> OrderItems { get; set; }
    }

    // ==================== PAYMENT DTOs ====================
    public class PaymentDTO
    {
        public int PaymentID { get; set; }
        public int OrderID { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
    }

    public class ProcessPaymentDTO
    {
        public int OrderID { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
    }

    // ==================== RESPONSE DTOs ====================
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }

    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
