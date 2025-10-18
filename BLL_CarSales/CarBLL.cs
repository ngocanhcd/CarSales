using System;
using System.Collections.Generic;
using DAL_CarSales;
using DTO_Carsales;

namespace BLL_CarSales
{
    public class CarBLL
    {
        private CarDAL carDAL;

        public CarBLL()
        {
            carDAL = new CarDAL();
        }

        // ==================== LẤY TẤT CẢ XE ====================
        public ApiResponse<List<CarDTO>> GetAllCars()
        {
            try
            {
                List<CarDTO> cars = carDAL.GetAllCars();
                return new ApiResponse<List<CarDTO>>
                {
                    Success = true,
                    Message = "Lấy danh sách xe thành công",
                    Data = cars
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<CarDTO>>
                {
                    Success = false,
                    Message = "Lỗi: " + ex.Message,
                    Data = null
                };
            }
        }

        // ==================== TÌM KIẾM XE ====================
        public ApiResponse<List<CarDTO>> SearchCars(string keyword, int? carTypeID, string status)
        {
            try
            {
                List<CarDTO> cars = carDAL.SearchCars(keyword, carTypeID, status);
                return new ApiResponse<List<CarDTO>>
                {
                    Success = true,
                    Message = $"Tìm thấy {cars.Count} xe",
                    Data = cars
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<CarDTO>>
                {
                    Success = false,
                    Message = "Lỗi: " + ex.Message,
                    Data = null
                };
            }
        }

        // ==================== LẤY XE THEO ID ====================
        public ApiResponse<CarDTO> GetCarById(int carId)
        {
            try
            {
                CarDTO car = carDAL.GetCarById(carId);

                if (car != null)
                {
                    return new ApiResponse<CarDTO>
                    {
                        Success = true,
                        Message = "Lấy thông tin xe thành công",
                        Data = car
                    };
                }
                else
                {
                    return new ApiResponse<CarDTO>
                    {
                        Success = false,
                        Message = "Không tìm thấy xe",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<CarDTO>
                {
                    Success = false,
                    Message = "Lỗi: " + ex.Message,
                    Data = null
                };
            }
        }

        // ==================== THÊM XE MỚI ====================
        public ApiResponse AddCar(AddCarDTO carDTO)
        {
            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(carDTO.CarName))
                    return new ApiResponse { Success = false, Message = "Tên xe không được để trống!" };

                if (carDTO.Price <= 0)
                    return new ApiResponse { Success = false, Message = "Giá xe phải lớn hơn 0!" };

                if (carDTO.CarTypeID <= 0)
                    return new ApiResponse { Success = false, Message = "Vui lòng chọn loại xe!" };

                if (carDTO.StockQuantity < 0)
                    return new ApiResponse { Success = false, Message = "Số lượng không được âm!" };

                if (string.IsNullOrWhiteSpace(carDTO.Status))
                    return new ApiResponse { Success = false, Message = "Vui lòng chọn trạng thái!" };

                // Thêm xe
                bool result = carDAL.AddCar(carDTO);

                if (result)
                {
                    return new ApiResponse
                    {
                        Success = true,
                        Message = "Thêm xe thành công!"
                    };
                }
                else
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "Thêm xe thất bại!"
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

        // ==================== CẬP NHẬT XE ====================
        public ApiResponse UpdateCar(CarDTO carDTO)
        {
            try
            {
                // Validation
                if (carDTO.CarID <= 0)
                    return new ApiResponse { Success = false, Message = "ID xe không hợp lệ!" };

                if (string.IsNullOrWhiteSpace(carDTO.CarName))
                    return new ApiResponse { Success = false, Message = "Tên xe không được để trống!" };

                if (carDTO.Price <= 0)
                    return new ApiResponse { Success = false, Message = "Giá xe phải lớn hơn 0!" };

                if (carDTO.CarTypeID <= 0)
                    return new ApiResponse { Success = false, Message = "Vui lòng chọn loại xe!" };

                if (carDTO.StockQuantity < 0)
                    return new ApiResponse { Success = false, Message = "Số lượng không được âm!" };

                if (string.IsNullOrWhiteSpace(carDTO.Status))
                    return new ApiResponse { Success = false, Message = "Vui lòng chọn trạng thái!" };

                // Cập nhật xe
                bool result = carDAL.UpdateCar(carDTO);

                if (result)
                {
                    return new ApiResponse
                    {
                        Success = true,
                        Message = "Cập nhật xe thành công!"
                    };
                }
                else
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "Cập nhật xe thất bại!"
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

        // ==================== XÓA XE ====================
        public ApiResponse DeleteCar(int carId)
        {
            try
            {
                if (carId <= 0)
                    return new ApiResponse { Success = false, Message = "ID xe không hợp lệ!" };

                bool result = carDAL.DeleteCar(carId);

                if (result)
                {
                    return new ApiResponse
                    {
                        Success = true,
                        Message = "Xóa xe thành công!"
                    };
                }
                else
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "Xóa xe thất bại!"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        // ==================== LẤY LOẠI XE ====================
        public ApiResponse<List<CarTypeDTO>> GetAllCarTypes()
        {
            try
            {
                List<CarTypeDTO> carTypes = carDAL.GetAllCarTypes();
                return new ApiResponse<List<CarTypeDTO>>
                {
                    Success = true,
                    Message = "Lấy danh sách loại xe thành công",
                    Data = carTypes
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<CarTypeDTO>>
                {
                    Success = false,
                    Message = "Lỗi: " + ex.Message,
                    Data = null
                };
            }
        }

        // ==================== CẬP NHẬT TỒN KHO ====================
        public ApiResponse UpdateStock(int carId, int quantity)
        {
            try
            {
                if (carId <= 0)
                    return new ApiResponse { Success = false, Message = "ID xe không hợp lệ!" };

                if (quantity < 0)
                    return new ApiResponse { Success = false, Message = "Số lượng không được âm!" };

                bool result = carDAL.UpdateStock(carId, quantity);

                if (result)
                {
                    return new ApiResponse
                    {
                        Success = true,
                        Message = "Cập nhật tồn kho thành công!"
                    };
                }
                else
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "Cập nhật tồn kho thất bại!"
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
    }
}