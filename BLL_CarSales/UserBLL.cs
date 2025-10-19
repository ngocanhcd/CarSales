using System;
using System.Text.RegularExpressions;
using DAL_CarSales;
using DTO_Carsales;

namespace BLL_CarSales
{
    public class UserBLL
    {
        private UserDAL userDAL;

        public UserBLL()
        {
            userDAL = new UserDAL();
        }

        // Xác thực và đăng nhập
        public ApiResponse<UserDTO> AuthenticateUser(string username, string password)
        {
            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(username))
                {
                    return new ApiResponse<UserDTO>
                    {
                        Success = false,
                        Message = "Tên đăng nhập không được để trống!",
                        Data = null
                    };
                }

                if (string.IsNullOrWhiteSpace(password))
                {
                    return new ApiResponse<UserDTO>
                    {
                        Success = false,
                        Message = "Mật khẩu không được để trống!",
                        Data = null
                    };
                }

                // Gọi DAL để đăng nhập
                UserDTO user = userDAL.Login(username.Trim(), password);

                if (user != null)
                {
                    return new ApiResponse<UserDTO>
                    {
                        Success = true,
                        Message = "Đăng nhập thành công!",
                        Data = user
                    };
                }
                else
                {
                    return new ApiResponse<UserDTO>
                    {
                        Success = false,
                        Message = "Tên đăng nhập hoặc mật khẩu không đúng!",
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

        // Đăng ký người dùng mới
        public ApiResponse Register(RegisterDTO registerDTO)
        {
            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(registerDTO.FullName))
                    return new ApiResponse { Success = false, Message = "Họ tên không được để trống!" };

                if (string.IsNullOrWhiteSpace(registerDTO.Username))
                    return new ApiResponse { Success = false, Message = "Tên đăng nhập không được để trống!" };

                if (registerDTO.Username.Length < 4)
                    return new ApiResponse { Success = false, Message = "Tên đăng nhập phải có ít nhất 4 ký tự!" };

                if (string.IsNullOrWhiteSpace(registerDTO.Password))
                    return new ApiResponse { Success = false, Message = "Mật khẩu không được để trống!" };

                if (registerDTO.Password.Length < 6)
                    return new ApiResponse { Success = false, Message = "Mật khẩu phải có ít nhất 6 ký tự!" };

                if (registerDTO.Password != registerDTO.ConfirmPassword)
                    return new ApiResponse { Success = false, Message = "Mật khẩu xác nhận không khớp!" };

                // Validate email nếu có
                if (!string.IsNullOrWhiteSpace(registerDTO.Email))
                {
                    if (!IsValidEmail(registerDTO.Email))
                        return new ApiResponse { Success = false, Message = "Email không hợp lệ!" };
                }

                // Validate phone nếu có
                if (!string.IsNullOrWhiteSpace(registerDTO.Phone))
                {
                    if (!IsValidPhone(registerDTO.Phone))
                        return new ApiResponse { Success = false, Message = "Số điện thoại không hợp lệ!" };
                }

                // Kiểm tra username đã tồn tại
                if (userDAL.CheckUsernameExists(registerDTO.Username.Trim()))
                    return new ApiResponse { Success = false, Message = "Tên đăng nhập đã tồn tại!" };

                // Đăng ký
                bool result = userDAL.Register(registerDTO);

                if (result)
                    return new ApiResponse { Success = true, Message = "Đăng ký thành công!" };
                else
                    return new ApiResponse { Success = false, Message = "Đăng ký thất bại!" };
            }
            catch (Exception ex)
            {
                return new ApiResponse { Success = false, Message = "Lỗi: " + ex.Message };
            }
        }

        // Cập nhật thông tin cá nhân
        public ApiResponse UpdateProfile(UpdateProfileDTO updateDTO)
        {
            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(updateDTO.FullName))
                    return new ApiResponse { Success = false, Message = "Họ tên không được để trống!" };

                if (!string.IsNullOrWhiteSpace(updateDTO.Email))
                {
                    if (!IsValidEmail(updateDTO.Email))
                        return new ApiResponse { Success = false, Message = "Email không hợp lệ!" };
                }

                if (!string.IsNullOrWhiteSpace(updateDTO.Phone))
                {
                    if (!IsValidPhone(updateDTO.Phone))
                        return new ApiResponse { Success = false, Message = "Số điện thoại không hợp lệ!" };
                }

                bool result = userDAL.UpdateProfile(updateDTO);

                if (result)
                    return new ApiResponse { Success = true, Message = "Cập nhật thông tin thành công!" };
                else
                    return new ApiResponse { Success = false, Message = "Cập nhật thất bại!" };
            }
            catch (Exception ex)
            {
                return new ApiResponse { Success = false, Message = "Lỗi: " + ex.Message };
            }
        }

        // Đổi mật khẩu
        public ApiResponse ChangePassword(ChangePasswordDTO changeDTO)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(changeDTO.OldPassword))
                    return new ApiResponse { Success = false, Message = "Mật khẩu cũ không được để trống!" };

                if (string.IsNullOrWhiteSpace(changeDTO.NewPassword))
                    return new ApiResponse { Success = false, Message = "Mật khẩu mới không được để trống!" };

                if (changeDTO.NewPassword.Length < 6)
                    return new ApiResponse { Success = false, Message = "Mật khẩu mới phải có ít nhất 6 ký tự!" };

                if (changeDTO.NewPassword != changeDTO.ConfirmPassword)
                    return new ApiResponse { Success = false, Message = "Mật khẩu xác nhận không khớp!" };

                bool result = userDAL.ChangePassword(changeDTO);

                if (result)
                    return new ApiResponse { Success = true, Message = "Đổi mật khẩu thành công!" };
                else
                    return new ApiResponse { Success = false, Message = "Mật khẩu cũ không đúng!" };
            }
            catch (Exception ex)
            {
                return new ApiResponse { Success = false, Message = "Lỗi: " + ex.Message };
            }
        }

        // Lấy thông tin user
        public UserDTO GetUserById(int userId)
        {
            try
            {
                return userDAL.GetUserById(userId);
            }
            catch
            {
                return null;
            }
        }

        // Helper methods
        private bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        private bool IsValidPhone(string phone)
        {
            string pattern = @"^[0-9]{10,11}$";
            return Regex.IsMatch(phone, pattern);
        }
        // Thêm vào BLL_CarSales/UserBLL.cs

        public ApiResponse UpdateUserRole(string username, string role)
        {
            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(username))
                    return new ApiResponse { Success = false, Message = "Username không hợp lệ!" };

                if (string.IsNullOrWhiteSpace(role))
                    return new ApiResponse { Success = false, Message = "Role không hợp lệ!" };

                // Kiểm tra role hợp lệ
                if (role != "Admin" && role != "Employee" && role != "Customer")
                    return new ApiResponse { Success = false, Message = "Role phải là Admin, Employee hoặc Customer!" };

                // Gọi DAL
                bool result = userDAL.UpdateUserRole(username, role);

                if (result)
                    return new ApiResponse { Success = true, Message = "Cập nhật role thành công!" };
                else
                    return new ApiResponse { Success = false, Message = "Cập nhật role thất bại!" };
            }
            catch (Exception ex)
            {
                return new ApiResponse { Success = false, Message = "Lỗi: " + ex.Message };
            }
        }
    }
}