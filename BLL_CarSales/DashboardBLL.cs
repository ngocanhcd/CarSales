using DAL_CarSales;
using DTO_Carsales;
using System;

namespace BLL_CarSales
{
    public class DashboardBLL
    {
        private DashboardDAL dashboardDAL;

        public DashboardBLL()
        {
            dashboardDAL = new DashboardDAL();
        }

        // ==================== LẤY THỐNG KÊ TỔNG QUAN ====================
        public DashboardStatsDTO GetDashboardStats()
        {
            try
            {
                return dashboardDAL.GetDashboardStats();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi BLL: " + ex.Message);
            }
        }

        // ==================== LẤY DOANH THU THEO THÁNG ====================
        public System.Collections.Generic.List<MonthlyRevenueDTO> GetMonthlyRevenue()
        {
            try
            {
                return dashboardDAL.GetMonthlyRevenue();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi BLL: " + ex.Message);
            }
        }

        // ==================== LẤY TOP XE BÁN CHẠY ====================
        public System.Collections.Generic.List<TopSellingCarDTO> GetTopSellingCars(int top = 5)
        {
            try
            {
                if (top <= 0)
                    top = 5;

                return dashboardDAL.GetTopSellingCars(top);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi BLL: " + ex.Message);
            }
        }

        // ==================== LẤY ĐƠN HÀNG GẦN ĐÂY ====================
        public System.Collections.Generic.List<RecentOrderDTO> GetRecentOrders(int top = 10)
        {
            try
            {
                if (top <= 0)
                    top = 10;

                return dashboardDAL.GetRecentOrders(top);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi BLL: " + ex.Message);
            }
        }

        // ==================== LẤY THỐNG KÊ TRẠNG THÁI ĐƠN HÀNG ====================
        public System.Collections.Generic.Dictionary<string, int> GetOrderStatusStats()
        {
            try
            {
                return dashboardDAL.GetOrderStatusStats();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi BLL: " + ex.Message);
            }
        }

        // ==================== TÍNH % TĂNG TRƯỞNG ====================
        public decimal CalculateGrowthRate(decimal current, decimal previous)
        {
            if (previous == 0)
                return current > 0 ? 100 : 0;

            return ((current - previous) / previous) * 100;
        }
    }
}