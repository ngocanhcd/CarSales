using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BLL_CarSales;
using DTO_Carsales;
using UTIL_CarSales;

namespace GUI_CarSales
{
    public partial class CustomerMainForm : Form
    {
        private CarBLL carBLL;
        private OrderBLL orderBLL;
        private List<CartItemDTO> cartItems;

        public CustomerMainForm()
        {
            InitializeComponent();
            carBLL = new CarBLL();
            orderBLL = new OrderBLL();
            cartItems = new List<CartItemDTO>();

            this.Load += CustomerMainForm_Load;
        }

        private void CustomerMainForm_Load(object sender, EventArgs e)
        {
            // Kiểm tra quyền customer
            if (!SessionManager.IsCustomer())
            {
                MessageBox.Show("Bạn không có quyền truy cập!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            // Hiển thị thông tin customer
            var currentUser = SessionManager.GetCurrentUser();
            lblCustomerName.Text = currentUser.FullName;

            // Setup
            SetupDataGridViews();
            LoadCarTypes();
            LoadAvailableCars();
            UpdateCartBadge();

            // Gắn events
            btnSearch.Click += btnSearch_Click;
            btnRefresh.Click += btnRefresh_Click;
            btnAddToCart.Click += btnAddToCart_Click;
            btnViewCart.Click += btnViewCart_Click;
            btnMyOrders.Click += btnMyOrders_Click;
            btnLogout.Click += btnLogout_Click;

            // Drag form
            pnlTop.MouseDown += PnlTop_MouseDown;
            pnlTop.MouseMove += PnlTop_MouseMove;
            pnlTop.MouseUp += PnlTop_MouseUp;
        }

        // ==================== SETUP ====================
        private void SetupDataGridViews()
        {
            dgvCars.Columns.Clear();
            dgvCars.AutoGenerateColumns = false;

            dgvCars.Columns.Add("CarID", "Mã");
            dgvCars.Columns.Add("CarName", "Tên xe");
            dgvCars.Columns.Add("CarTypeName", "Loại");
            dgvCars.Columns.Add("Price", "Giá (VNĐ)");
            dgvCars.Columns.Add("StockQuantity", "Tồn kho");

            dgvCars.Columns[0].Width = 60;
            dgvCars.Columns[1].Width = 280;
            dgvCars.Columns[2].Width = 130;
            dgvCars.Columns[3].Width = 150;
            dgvCars.Columns[4].Width = 100;

            dgvCars.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCars.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void LoadCarTypes()
        {
            try
            {
                var response = carBLL.GetAllCarTypes();

                if (response.Success)
                {
                    cboCarType.Items.Clear();
                    cboCarType.Items.Add("-- Tất cả loại xe --");

                    foreach (var carType in response.Data)
                    {
                        cboCarType.Items.Add(carType.CarTypeName);
                    }

                    cboCarType.SelectedIndex = 0;
                    cboCarType.Tag = response.Data;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load loại xe: " + ex.Message);
            }
        }

        // ==================== LOAD AVAILABLE CARS ====================
        private void LoadAvailableCars()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                var response = carBLL.SearchCars("", null, "Available");

                if (response.Success)
                {
                    DisplayCars(response.Data);
                }
                else
                {
                    MessageBox.Show(response.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void DisplayCars(List<CarDTO> cars)
        {
            dgvCars.Rows.Clear();

            foreach (var car in cars)
            {
                if (car.Status == "Available" && car.StockQuantity > 0)
                {
                    int rowIndex = dgvCars.Rows.Add(
                        car.CarID,
                        car.CarName,
                        car.CarTypeName,
                        FormatCurrency(car.Price),
                        car.StockQuantity
                    );

                    DataGridViewRow row = dgvCars.Rows[rowIndex];
                    row.DefaultCellStyle.BackColor = Color.FromArgb(212, 237, 218);
                }
            }

            lblTotalCars.Text = $"Có {dgvCars.Rows.Count} xe đang bán";
        }

        // ==================== TÌM KIẾM ====================
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                string keyword = txtSearch.Text.Trim();
                int? carTypeID = null;

                if (cboCarType.SelectedIndex > 0)
                {
                    var carTypes = cboCarType.Tag as List<CarTypeDTO>;
                    if (carTypes != null)
                    {
                        carTypeID = carTypes[cboCarType.SelectedIndex - 1].CarTypeID;
                    }
                }

                var response = carBLL.SearchCars(keyword, carTypeID, "Available");

                if (response.Success)
                {
                    DisplayCars(response.Data);
                }
                else
                {
                    MessageBox.Show(response.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cboCarType.SelectedIndex = 0;
            LoadAvailableCars();
        }

        // ==================== THÊM VÀO GIỎ HÀNG ====================
        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (dgvCars.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn xe!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int carId = Convert.ToInt32(dgvCars.SelectedRows[0].Cells[0].Value);
            string carName = dgvCars.SelectedRows[0].Cells[1].Value.ToString();
            string carTypeName = dgvCars.SelectedRows[0].Cells[2].Value.ToString();
            string priceStr = dgvCars.SelectedRows[0].Cells[3].Value.ToString();
            int stock = Convert.ToInt32(dgvCars.SelectedRows[0].Cells[4].Value);

            // Parse price
            decimal price = ParseCurrency(priceStr);

            // Kiểm tra xe đã có trong giỏ chưa
            var existingItem = cartItems.Find(item => item.CarID == carId);

            if (existingItem != null)
            {
                // Tăng số lượng
                if (existingItem.Quantity >= stock)
                {
                    MessageBox.Show($"Xe này chỉ còn {stock} chiếc!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                existingItem.Quantity++;
                existingItem.TotalPrice = existingItem.Quantity * existingItem.Price;
            }
            else
            {
                // Thêm mới
                CartItemDTO newItem = new CartItemDTO
                {
                    CarID = carId,
                    CarName = carName,
                    CarTypeName = carTypeName,
                    Price = price,
                    Quantity = 1,
                    TotalPrice = price
                };

                cartItems.Add(newItem);
            }

            UpdateCartBadge();
            MessageBox.Show($"Đã thêm '{carName}' vào giỏ hàng!", "Thành công",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ==================== XEM GIỎ HÀNG ====================
        private void btnViewCart_Click(object sender, EventArgs e)
        {
            CartForm cartForm = new CartForm(cartItems);

            if (cartForm.ShowDialog() == DialogResult.OK)
            {
                // Nếu checkout thành công
                CheckoutDTO checkoutDTO = new CheckoutDTO
                {
                    UserID = SessionManager.GetCurrentUserId(),
                    CartItems = cartItems,
                    PaymentMethod = cartForm.SelectedPaymentMethod
                };

                var response = orderBLL.CreateOrder(checkoutDTO);

                if (response.Success)
                {
                    MessageBox.Show(response.Message, "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Clear giỏ hàng
                    cartItems.Clear();
                    UpdateCartBadge();
                    LoadAvailableCars();
                }
                else
                {
                    MessageBox.Show(response.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Cart được update, refresh badge
                UpdateCartBadge();
            }
        }

        // ==================== ĐƠN HÀNG CỦA TÔI ====================
        private void btnMyOrders_Click(object sender, EventArgs e)
        {
            MyOrdersForm ordersForm = new MyOrdersForm();
            ordersForm.ShowDialog();
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

        // ==================== HELPER METHODS ====================
        private void UpdateCartBadge()
        {
            int totalItems = 0;
            foreach (var item in cartItems)
            {
                totalItems += item.Quantity;
            }

            if (totalItems > 0)
            {
                btnViewCart.Text = $"🛒 Giỏ hàng ({totalItems})";
            }
            else
            {
                btnViewCart.Text = "🛒 Giỏ hàng";
            }
        }

        private string FormatCurrency(decimal amount)
        {
            if (amount >= 1000000000)
            {
                return (amount / 1000000000).ToString("0.##") + " tỷ";
            }
            else if (amount >= 1000000)
            {
                return (amount / 1000000).ToString("0.##") + " tr";
            }
            else
            {
                return amount.ToString("#,##0") + "đ";
            }
        }

        private decimal ParseCurrency(string currencyStr)
        {
            // Remove "tỷ", "tr", "đ" and parse
            currencyStr = currencyStr.Replace(" tỷ", "").Replace(" tr", "").Replace("đ", "").Trim();

            if (decimal.TryParse(currencyStr, out decimal value))
            {
                if (currencyStr.Contains("tỷ"))
                    return value * 1000000000;
                else if (currencyStr.Contains("tr"))
                    return value * 1000000;
                else
                    return value;
            }

            return 0;
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

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            SessionManager.Logout();
            base.OnFormClosing(e);
        }
    }
}