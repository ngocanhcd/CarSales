using BLL_CarSales;
using DTO_Carsales;
using System;
using System.Drawing;
using System.Windows.Forms;
using UTIL_CarSales;

namespace GUI_CarSales
{
    public partial class AdminMainForm : Form
    {
        private DashboardBLL dashboardBLL;
        private UserBLL userBLL;

        public AdminMainForm()
        {
            InitializeComponent();
            dashboardBLL = new DashboardBLL();
            userBLL = new UserBLL();

            this.Load += AdminMainForm_Load;
        }

        private void AdminMainForm_Load(object sender, EventArgs e)
        {
            // Kiểm tra quyền admin
            if (!SessionManager.IsAdmin())
            {
                MessageBox.Show("Bạn không có quyền truy cập!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            // Hiển thị thông tin admin
            var currentUser = SessionManager.GetCurrentUser();
            lblAdminName.Text = currentUser.FullName;

            // Load dữ liệu dashboard
            LoadDashboardData();

            // Setup DataGridView
            SetupDataGridViews();

            // Gắn events cho menu buttons
            btnDashboard.Click += btnDashboard_Click;
            btnUsers.Click += btnUsers_Click;
            btnCars.Click += btnCars_Click;
            btnOrders.Click += btnOrders_Click;
            btnReports.Click += btnReports_Click;
            btnSettings.Click += btnSettings_Click;

            // Drag form
            pnlTop.MouseDown += PnlTop_MouseDown;
            pnlTop.MouseMove += PnlTop_MouseMove;
            pnlTop.MouseUp += PnlTop_MouseUp;
        }

        // ==================== LOAD DỮ LIỆU DASHBOARD ====================
        private void LoadDashboardData()
        {
            try
            {
                // Hiển thị loading
                this.Cursor = Cursors.WaitCursor;

                // Lấy thống kê tổng quan
                DashboardStatsDTO stats = dashboardBLL.GetDashboardStats();

                // Cập nhật các card thống kê
                lblTotalUsersValue.Text = stats.TotalUsers.ToString("N0");
                lblTotalCarsValue.Text = stats.TotalCars.ToString("N0");
                lblTotalOrdersValue.Text = stats.TotalOrders.ToString("N0");
                lblTotalRevenueValue.Text = FormatCurrency(stats.TotalRevenue);

                // Load đơn hàng gần đây
                LoadRecentOrders();

                // Load top xe bán chạy
                LoadTopSellingCars();

                // Hiển thị thời gian cập nhật
                lblTitle.Text = $"📊 ADMIN DASHBOARD - Cập nhật: {DateTime.Now:HH:mm:ss dd/MM/yyyy}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu dashboard: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        // ==================== SETUP DATAGRIDVIEWS ====================
        private void SetupDataGridViews()
        {
            // Setup dgvRecentOrders
            dgvRecentOrders.Columns.Clear();
            dgvRecentOrders.Columns.Add("OrderID", "Mã Đơn");
            dgvRecentOrders.Columns.Add("OrderDate", "Ngày đặt");
            dgvRecentOrders.Columns.Add("CustomerName", "Khách hàng");
            dgvRecentOrders.Columns.Add("TotalAmount", "Tổng");
            dgvRecentOrders.Columns.Add("Status", "Trạng thái");

            dgvRecentOrders.Columns[0].Width = 110;
            dgvRecentOrders.Columns[1].Width = 100;
            dgvRecentOrders.Columns[2].Width = 100;
            dgvRecentOrders.Columns[3].Width = 80;
            dgvRecentOrders.Columns[4].Width = 100;

            dgvRecentOrders.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // Setup dgvTopCars
            dgvTopCars.Columns.Clear();
            dgvTopCars.Columns.Add("CarName", "Tên xe");
            dgvTopCars.Columns.Add("CarTypeName", "Loại");
            dgvTopCars.Columns.Add("TotalSold", "Đã bán");
            dgvTopCars.Columns.Add("TotalRevenue", "Doanh thu");

            dgvTopCars.Columns[0].Width = 100;
            dgvTopCars.Columns[1].Width = 100;
            dgvTopCars.Columns[2].Width = 100;
            dgvTopCars.Columns[3].Width = 100;

            dgvTopCars.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvTopCars.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // Style chung
            dgvRecentOrders.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvRecentOrders.DefaultCellStyle.SelectionBackColor = Color.FromArgb(94, 148, 255);
            dgvRecentOrders.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);

            dgvTopCars.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvTopCars.DefaultCellStyle.SelectionBackColor = Color.FromArgb(40, 167, 69);
            dgvTopCars.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
        }

        // ==================== LOAD ĐƠN HÀNG GẦN ĐÂY ====================
        private void LoadRecentOrders()
        {
            try
            {
                var orders = dashboardBLL.GetRecentOrders(10);
                dgvRecentOrders.Rows.Clear();

                if (orders == null || orders.Count == 0)
                {
                    // Không có đơn hàng
                    return;
                }

                foreach (var order in orders)
                {
                    int rowIndex = dgvRecentOrders.Rows.Add(
                        order.OrderID,
                        order.OrderDate.ToString("dd/MM/yyyy HH:mm"),
                        order.CustomerName,
                        FormatCurrency(order.TotalAmount),
                        GetStatusText(order.Status)
                    );

                    // Tô màu theo status
                    DataGridViewRow row = dgvRecentOrders.Rows[rowIndex];

                    switch (order.Status)
                    {
                        case "Pending":
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 243, 205);
                            row.DefaultCellStyle.ForeColor = Color.FromArgb(133, 100, 4);
                            break;
                        case "Processing":
                            row.DefaultCellStyle.BackColor = Color.FromArgb(209, 231, 221);
                            row.DefaultCellStyle.ForeColor = Color.FromArgb(21, 87, 36);
                            break;
                        case "Shipped":
                            row.DefaultCellStyle.BackColor = Color.FromArgb(204, 229, 255);
                            row.DefaultCellStyle.ForeColor = Color.FromArgb(1, 67, 97);
                            break;
                        case "Completed":
                            row.DefaultCellStyle.BackColor = Color.FromArgb(212, 237, 218);
                            row.DefaultCellStyle.ForeColor = Color.FromArgb(21, 87, 36);
                            break;
                        case "Cancelled":
                            row.DefaultCellStyle.BackColor = Color.FromArgb(248, 215, 218);
                            row.DefaultCellStyle.ForeColor = Color.FromArgb(114, 28, 36);
                            break;
                    }
                }

                // Thêm label title
                AddTableTitle("📦 Đơn hàng gần đây", 30, 155);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải đơn hàng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== LOAD TOP XE BÁN CHẠY ====================
        private void LoadTopSellingCars()
        {
            try
            {
                var cars = dashboardBLL.GetTopSellingCars(5);
                dgvTopCars.Rows.Clear();

                if (cars == null || cars.Count == 0)
                {
                    // Không có dữ liệu
                    return;
                }

                int rank = 1;
                foreach (var car in cars)
                {
                    int rowIndex = dgvTopCars.Rows.Add(
                        $"#{rank}. {car.CarName}",
                        car.CarTypeName,
                        car.TotalSold.ToString("N0"),
                        FormatCurrency(car.TotalRevenue)
                    );

                    // Tô màu top 1, 2, 3
                    DataGridViewRow row = dgvTopCars.Rows[rowIndex];
                    switch (rank)
                    {
                        case 1:
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 215, 0); // Gold
                            row.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                            break;
                        case 2:
                            row.DefaultCellStyle.BackColor = Color.FromArgb(192, 192, 192); // Silver
                            row.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                            break;
                        case 3:
                            row.DefaultCellStyle.BackColor = Color.FromArgb(205, 127, 50); // Bronze
                            row.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                            break;
                    }

                    rank++;
                }

                // Thêm label title
                AddTableTitle("🏆 Top 5 xe bán chạy nhất", 570, 155);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải top xe: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== HELPER METHODS ====================
        private void AddTableTitle(string text, int x, int y)
        {
            // Xóa label cũ nếu có
            foreach (Control ctrl in pnlMain.Controls)
            {
                if (ctrl is Label && ctrl.Location == new Point(x, y))
                {
                    pnlMain.Controls.Remove(ctrl);
                    break;
                }
            }

            Label lbl = new Label();
            lbl.Text = text;
            lbl.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lbl.ForeColor = Color.FromArgb(45, 52, 54);
            lbl.AutoSize = true;
            lbl.Location = new Point(x, y);
            pnlMain.Controls.Add(lbl);
            lbl.BringToFront();
        }

        private string FormatCurrency(decimal amount)
        {
            if (amount >= 1000000000) // Tỷ
            {
                return (amount / 1000000000).ToString("0.##") + " tỷ";
            }
            else if (amount >= 1000000) // Triệu
            {
                return (amount / 1000000).ToString("0.##") + " tr";
            }
            else
            {
                return amount.ToString("#,##0") + "đ";
            }
        }

        private string GetStatusText(string status)
        {
            switch (status)
            {
                case "Pending": return "Chờ xử lý";
                case "Processing": return "Đang xử lý";
                case "Shipped": return "Đã giao";
                case "Completed": return "Hoàn thành";
                case "Cancelled": return "Đã hủy";
                default: return status;
            }
        }

        // ==================== DRAG FORM ====================
        private bool isDragging = false;
        private Point lastCursor;
        private Point lastForm;

        private void PnlTop_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            lastCursor = Cursor.Position;
            lastForm = this.Location;
        }

        private void PnlTop_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point diff = Point.Subtract(Cursor.Position, new Size(lastCursor));
                this.Location = Point.Add(lastForm, new Size(diff));
            }
        }

        private void PnlTop_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        // ==================== WINDOW CONTROLS ====================
        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn đóng ứng dụng?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
                btnMaximize.Text = "❐";
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                btnMaximize.Text = "□";
            }
        }

        // ==================== ĐĂNG XUẤT ====================
        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn đăng xuất?",
                "Xác nhận đăng xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                SessionManager.Logout();
                this.Close();
            }
        }

        // ==================== NAVIGATION BUTTONS ====================
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            // Reload dashboard
            LoadDashboardData();
            MessageBox.Show("Dashboard đã được làm mới!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Chức năng quản lý người dùng đang được phát triển!\n\n" +
                "Các tính năng:\n" +
                "- Xem danh sách người dùng\n" +
                "- Thêm/Sửa/Xóa người dùng\n" +
                "- Phân quyền người dùng\n" +
                "- Khóa/Mở khóa tài khoản",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void btnCars_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Chức năng quản lý xe đang được phát triển!\n\n" +
                "Các tính năng:\n" +
                "- Xem danh sách xe\n" +
                "- Thêm/Sửa/Xóa xe\n" +
                "- Quản lý loại xe\n" +
                "- Quản lý kho\n" +
                "- Upload hình ảnh",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Chức năng quản lý đơn hàng đang được phát triển!\n\n" +
                "Các tính năng:\n" +
                "- Xem danh sách đơn hàng\n" +
                "- Cập nhật trạng thái đơn hàng\n" +
                "- Xem chi tiết đơn hàng\n" +
                "- In hóa đơn\n" +
                "- Hủy đơn hàng",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Chức năng báo cáo đang được phát triển!\n\n" +
                "Các tính năng:\n" +
                "- Báo cáo doanh thu theo tháng/năm\n" +
                "- Báo cáo xe bán chạy\n" +
                "- Báo cáo tồn kho\n" +
                "- Báo cáo khách hàng\n" +
                "- Xuất file Excel/PDF",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Chức năng cài đặt đang được phát triển!\n\n" +
                "Các tính năng:\n" +
                "- Thông tin cá nhân\n" +
                "- Đổi mật khẩu\n" +
                "- Cấu hình hệ thống\n" +
                "- Backup/Restore database\n" +
                "- Cài đặt giao diện",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        // ==================== REFRESH BUTTON ====================
        private Timer refreshTimer;

        private void StartAutoRefresh()
        {
            // Auto refresh mỗi 30 giây
            refreshTimer = new Timer();
            refreshTimer.Interval = 30000; // 30 seconds
            refreshTimer.Tick += (s, e) => LoadDashboardData();
            refreshTimer.Start();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Dừng timer khi đóng form
            if (refreshTimer != null)
            {
                refreshTimer.Stop();
                refreshTimer.Dispose();
            }

            base.OnFormClosing(e);
        }
    }
}