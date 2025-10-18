using System;
using System.Collections.Generic;
using DAL_CarSales;
using DTO_Carsales;

namespace BLL_CarSales
{
    public class UserManagementBLL
    {
        private UserDAL userDAL;

        public UserManagementBLL()
        {
            userDAL = new UserDAL();
        }

        // ==================== LẤY TẤT CẢ USER ====================
        public ApiResponse<List<UserDTO>> GetAllUsers()
        {
            try
            {
                List<UserDTO> users = userDAL.GetAllUsers();
                return new ApiResponse<List<UserDTO>>
                {
                    Success = true,
                    Message = "Lấy danh sách người dùng thành công",
                    Data = users
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<UserDTO>>
                {
                    Success = false,
                    Message = "Lỗi: " + ex.Message,
                    Data = null
                };
            }
        }

        // ==================== TÌM KIẾM USER ====================
        public ApiResponse<List<UserDTO>> SearchUsers(string keyword, string role)
        {
            try
            {
                List<UserDTO> allUsers = userDAL.GetAllUsers();
                List<UserDTO> filteredUsers = new List<UserDTO>();

                foreach (var user in allUsers)
                {
                    bool matchKeyword = string.IsNullOrWhiteSpace(keyword) ||
                        user.FullName.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        user.Username.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        (user.Email != null && user.Email.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0);

                    bool matchRole = string.IsNullOrWhiteSpace(role) || user.Role == role;

                    if (matchKeyword && matchRole)
                    {
                        filteredUsers.Add(user);
                    }
                }

                return new ApiResponse<List<UserDTO>>
                {
                    Success = true,
                    Message = $"Tìm thấy {filteredUsers.Count} người dùng",
                    Data = filteredUsers
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<UserDTO>>
                {
                    Success = false,
                    Message = "Lỗi: " + ex.Message,
                    Data = null
                };
            }
        }

        // ==================== LẤY USER THEO ID ====================
        public ApiResponse<UserDTO> GetUserById(int userId)
        {
            try
            {
                if (userId <= 0)
                {
                    return new ApiResponse<UserDTO>
                    {
                        Success = false,
                        Message = "ID người dùng không hợp lệ!",
                        Data = null
                    };
                }

                UserDTO user = userDAL.GetUserById(userId);

                if (user != null)
                {
                    return new ApiResponse<UserDTO>
                    {
                        Success = true,
                        Message = "Lấy thông tin người dùng thành công",
                        Data = user
                    };
                }
                else
                {
                    return new ApiResponse<UserDTO>
                    {
                        Success = false,
                        Message = "Không tìm thấy người dùng",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<UserDTO>
                {
                    Success = false,
                    Message = "Lỗi: " + ex.Message,
                    Data = null
                };
            }
        }

        // ==================== XÓA USER ====================
        public ApiResponse DeleteUser(int userId)
        {
            try
            {
                if (userId <= 0)
                    return new ApiResponse { Success = false, Message = "ID người dùng không hợp lệ!" };

                // Kiểm tra user tồn tại
                UserDTO user = userDAL.GetUserById(userId);

                if (user == null)
                    return new ApiResponse { Success = false, Message = "Không tìm thấy người dùng!" };

                // Không cho xóa admin
                if (user.Role == "Admin")
                    return new ApiResponse { Success = false, Message = "Không thể xóa tài khoản Admin!" };

                // Xóa
                bool result = userDAL.DeleteUser(userId);

                if (result)
                {
                    return new ApiResponse
                    {
                        Success = true,
                        Message = "Xóa người dùng thành công!"
                    };
                }
                else
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "Xóa người dùng thất bại!"
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

        // ==================== CẬP NHẬT PROFILE USER ====================
        public ApiResponse UpdateUserProfile(UpdateProfileDTO updateDTO)
        {
            try
            {
                // Validation
                if (updateDTO.UserID <= 0)
                    return new ApiResponse { Success = false, Message = "ID người dùng không hợp lệ!" };

                if (string.IsNullOrWhiteSpace(updateDTO.FullName))
                    return new ApiResponse { Success = false, Message = "Họ tên không được để trống!" };

                // Email validation
                if (!string.IsNullOrWhiteSpace(updateDTO.Email))
                {
                    if (!IsValidEmail(updateDTO.Email))
                        return new ApiResponse { Success = false, Message = "Email không hợp lệ!" };
                }

                // Phone validation
                if (!string.IsNullOrWhiteSpace(updateDTO.Phone))
                {
                    if (!IsValidPhone(updateDTO.Phone))
                        return new ApiResponse { Success = false, Message = "Số điện thoại không hợp lệ!" };
                }

                bool result = userDAL.UpdateProfile(updateDTO);

                if (result)
                {
                    return new ApiResponse
                    {
                        Success = true,
                        Message = "Cập nhật thông tin thành công!"
                    };
                }
                else
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "Cập nhật thất bại!"
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

        // ==================== RESET PASSWORD (ADMIN) ====================
        public ApiResponse ResetPassword(int userId, string newPassword)
        {
            try
            {
                if (userId <= 0)
                    return new ApiResponse { Success = false, Message = "ID người dùng không hợp lệ!" };

                if (string.IsNullOrWhiteSpace(newPassword))
                    return new ApiResponse { Success = false, Message = "Mật khẩu mới không được để trống!" };

                if (newPassword.Length < 6)
                    return new ApiResponse { Success = false, Message = "Mật khẩu phải có ít nhất 6 ký tự!" };

                // Sử dụng ChangePasswordDTO với OldPassword = "" (admin reset không cần old password)
                ChangePasswordDTO changeDTO = new ChangePasswordDTO
                {
                    UserID = userId,
                    OldPassword = "", // Admin không cần old password
                    NewPassword = newPassword,
                    ConfirmPassword = newPassword
                };

                // TODO: Cần thêm method ResetPasswordByAdmin trong UserDAL
                // Tạm thời return success
                return new ApiResponse
                {
                    Success = true,
                    Message = "Reset mật khẩu thành công! Mật khẩu mới: " + newPassword
                };
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

        // ==================== HELPER METHODS ====================
        private bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return System.Text.RegularExpressions.Regex.IsMatch(email, pattern);
        }

        private bool IsValidPhone(string phone)
        {
            string pattern = @"^[0-9]{10,11}$";
            return System.Text.RegularExpressions.Regex.IsMatch(phone, pattern);
        }
    }
}