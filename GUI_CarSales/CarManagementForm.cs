using System;
using System.Drawing;
using System.Windows.Forms;
using BLL_CarSales;
using DTO_Carsales;

namespace GUI_CarSales
{
    public partial class CarManagementForm : Form
    {
        private CarBLL carBLL;

        public CarManagementForm()
        {
            InitializeComponent();
            carBLL = new CarBLL();

            this.Load += CarManagementForm_Load;
        }

        private void CarManagementForm_Load(object sender, EventArgs e)
        {
            // Load dữ liệu ban đầu
            LoadCarTypes();
            LoadStatuses();
            SetupDataGridView();
            LoadAllCars();
        }

        // ==================== SETUP ====================
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load loại xe: " + ex.Message);
            }
        }

        private void LoadStatuses()
        {
            cboStatus.Items.Clear();
            cboStatus.Items.Add("-- Tất cả trạng thái --");
            cboStatus.Items.Add("Available");
            cboStatus.Items.Add("Sold");
            cboStatus.Items.Add("Reserved");
            cboStatus.SelectedIndex = 0;
        }

        private void SetupDataGridView()
        {
            dgvCars.Columns.Clear();
            dgvCars.AutoGenerateColumns = false;

            dgvCars.Columns.Add("CarID", "Mã xe");
            dgvCars.Columns.Add("CarName", "Tên xe");
            dgvCars.Columns.Add("CarTypeName", "Loại xe");
            dgvCars.Columns.Add("Price", "Giá (VNĐ)");
            dgvCars.Columns.Add("StockQuantity", "Tồn kho");
            dgvCars.Columns.Add("Status", "Trạng thái");

            dgvCars.Columns[0].Width = 80;
            dgvCars.Columns[1].Width = 250;
            dgvCars.Columns[2].Width = 150;
            dgvCars.Columns[3].Width = 150;
            dgvCars.Columns[4].Width = 100;
            dgvCars.Columns[5].Width = 120;

            dgvCars.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCars.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCars.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        // ==================== LOAD DỮ LIỆU ====================
        private void LoadAllCars()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                var response = carBLL.GetAllCars();

                if (response.Success)
                {
                    DisplayCars(response.Data);
                }
                else
                {
                    MessageBox.Show(response.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void DisplayCars(System.Collections.Generic.List<CarDTO> cars)
        {
            dgvCars.Rows.Clear();

            int totalCars = cars.Count;
            int inStock = 0;
            int outOfStock = 0;

            foreach (var car in cars)
            {
                int rowIndex = dgvCars.Rows.Add(
                    car.CarID,
                    car.CarName,
                    car.CarTypeName,
                    FormatCurrency(car.Price),
                    car.StockQuantity,
                    GetStatusText(car.Status)
                );

                // Tô màu theo trạng thái
                DataGridViewRow row = dgvCars.Rows[rowIndex];

                switch (car.Status)
                {
                    case "Available":
                        row.DefaultCellStyle.BackColor = Color.FromArgb(212, 237, 218);
                        break;
                    case "Sold":
                        row.DefaultCellStyle.BackColor = Color.FromArgb(248, 215, 218);
                        break;
                    case "Reserved":
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 243, 205);
                        break;
                }

                // Đếm thống kê
                if (car.StockQuantity > 0)
                    inStock++;
                else
                    outOfStock++;
            }

            // Cập nhật labels
            lblTotalCars.Text = $"Tổng số xe: {totalCars}";
            lblInStock.Text = $"| Còn hàng: {inStock}";
            lblOutOfStock.Text = $"| Hết hàng: {outOfStock}";
        }

        // ==================== TÌM KIẾM ====================
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                string keyword = txtSearch.Text.Trim();
                int? carTypeID = null;
                string status = null;

                // Lấy CarTypeID nếu chọn
                if (cboCarType.SelectedIndex > 0)
                {
                    var response = carBLL.GetAllCarTypes();
                    if (response.Success)
                    {
                        carTypeID = response.Data[cboCarType.SelectedIndex - 1].CarTypeID;
                    }
                }

                // Lấy Status nếu chọn
                if (cboStatus.SelectedIndex > 0)
                {
                    status = cboStatus.SelectedItem.ToString();
                }

                var searchResponse = carBLL.SearchCars(keyword, carTypeID, status);

                if (searchResponse.Success)
                {
                    DisplayCars(searchResponse.Data);
                }
                else
                {
                    MessageBox.Show(searchResponse.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        // ==================== LÀM MỚI ====================
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cboCarType.SelectedIndex = 0;
            cboStatus.SelectedIndex = 0;
            LoadAllCars();
        }

        // ==================== THÊM XE ====================
        private void btnAdd_Click(object sender, EventArgs e)
        {
            CarAddEditForm addForm = new CarAddEditForm();

            if (addForm.ShowDialog() == DialogResult.OK)
            {
                LoadAllCars();
            }
        }

        // ==================== SỬA XE ====================
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvCars.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn xe cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int carId = Convert.ToInt32(dgvCars.SelectedRows[0].Cells[0].Value);

            CarAddEditForm editForm = new CarAddEditForm(carId);

            if (editForm.ShowDialog() == DialogResult.OK)
            {
                LoadAllCars();
            }
        }

        // ==================== XÓA XE ====================
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCars.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn xe cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int carId = Convert.ToInt32(dgvCars.SelectedRows[0].Cells[0].Value);
            string carName = dgvCars.SelectedRows[0].Cells[1].Value.ToString();

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc muốn xóa xe '{carName}'?\n\nThao tác này không thể hoàn tác!",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    var response = carBLL.DeleteCar(carId);

                    if (response.Success)
                    {
                        MessageBox.Show(response.Message, "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadAllCars();
                    }
                    else
                    {
                        MessageBox.Show(response.Message, "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa xe: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ==================== ĐÓNG FORM ====================
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // ==================== HELPER METHODS ====================
        private string FormatCurrency(decimal amount)
        {
            return amount.ToString("#,##0") + " đ";
        }

        private string GetStatusText(string status)
        {
            switch (status)
            {
                case "Available": return "Còn hàng";
                case "Sold": return "Đã bán";
                case "Reserved": return "Đã đặt";
                default: return status;
            }
        }
    }
}