using System;
using DTO_Carsales;

namespace UTIL_CarSales
{
    // Quản lý phiên đăng nhập của user hiện tại
    public static class SessionManager
    {
        private static UserDTO currentUser = null;

        // Lưu thông tin user đã đăng nhập
        public static void SetCurrentUser(UserDTO user)
        {
            currentUser = user;
        }

        // Lấy thông tin user hiện tại
        public static UserDTO GetCurrentUser()
        {
            return currentUser;
        }

        // Kiểm tra đã đăng nhập chưa
        public static bool IsLoggedIn()
        {
            return currentUser != null;
        }

        // Đăng xuất
        public static void Logout()
        {
            currentUser = null;
        }

        // Kiểm tra quyền
        public static bool IsAdmin()
        {
            return currentUser != null && currentUser.Role == "Admin";
        }

        public static bool IsEmployee()
        {
            return currentUser != null && currentUser.Role == "Employee";
        }

        public static bool IsCustomer()
        {
            return currentUser != null && currentUser.Role == "Customer";
        }

        // Lấy UserID hiện tại
        public static int GetCurrentUserId()
        {
            return currentUser != null ? currentUser.UserID : 0;
        }

        // Lấy Username hiện tại
        public static string GetCurrentUsername()
        {
            return currentUser != null ? currentUser.Username : string.Empty;
        }

        // Lấy Role hiện tại
        public static string GetCurrentRole()
        {
            return currentUser != null ? currentUser.Role : string.Empty;
        }
    }
}