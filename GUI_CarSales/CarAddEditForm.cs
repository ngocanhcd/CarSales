using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using BLL_CarSales;
using DTO_Carsales;

namespace GUI_CarSales
{
    public partial class CarAddEditForm : Form
    {
        private CarBLL carBLL;
        private int? carId;
        private bool isEditMode;
        private string selectedImagePath = "";

        public CarAddEditForm(int? carId = null)
        {
            this.carId = carId;
            this.isEditMode = carId.HasValue;

            InitializeComponent();
            carBLL = new CarBLL();

            // Cập nhật title
            lblTitle.Text = isEditMode ? "SỬA THÔNG TIN XE" : "THÊM XE MỚI";
            btnSave.Text = isEditMode ? "💾 LƯU THAY ĐỔI" : "➕ THÊM XE";

            // Load data
            this.Load += CarAddEditForm_Load;
            btnSave.Click += BtnSave_Click;

            // Drag form
            pnlTop.MouseDown += PnlTop_MouseDown;
            pnlTop.MouseMove += PnlTop_MouseMove;
            pnlTop.MouseUp += PnlTop_MouseUp;
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

        // ==================== LOAD FORM ====================
        private void CarAddEditForm_Load(object sender, EventArgs e)
        {
            LoadCarTypes();
            cboStatus.SelectedIndex = 0; // Mặc định chọn "Available"

            if (isEditMode)
            {
                LoadCarData();
            }
        }

        // ==================== LOAD LOẠI XE ====================
        private void LoadCarTypes()
        {
            try
            {
                var response = carBLL.GetAllCarTypes();

                if (response.Success)
                {
                    cboCarType.Items.Clear();

                    foreach (var carType in response.Data)
                    {
                        cboCarType.Items.Add(carType.CarTypeName);
                    }

                    // Lưu list CarType vào Tag để dùng sau
                    cboCarType.Tag = response.Data;

                    if (cboCarType.Items.Count > 0)
                        cboCarType.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show(response.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load loại xe: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== LOAD DỮ LIỆU XE (CHẾ ĐỘ SỬA) ====================
        private void LoadCarData()
        {
            try
            {
                var response = carBLL.GetCarById(carId.Value);

                if (response.Success)
                {
                    CarDTO car = response.Data;

                    txtCarName.Text = car.CarName;
                    txtPrice.Text = car.Price.ToString("0");
                    txtStockQuantity.Text = car.StockQuantity.ToString();

                    // Set CarType
                    var carTypes = cboCarType.Tag as System.Collections.Generic.List<CarTypeDTO>;
                    if (carTypes != null)
                    {
                        for (int i = 0; i < carTypes.Count; i++)
                        {
                            if (carTypes[i].CarTypeID == car.CarTypeID)
                            {
                                cboCarType.SelectedIndex = i;
                                break;
                            }
                        }
                    }

                    // Set Status
                    cboStatus.SelectedItem = car.Status;

                    // Load Image
                    if (!string.IsNullOrWhiteSpace(car.ImagePath))
                    {
                        selectedImagePath = car.ImagePath;
                        LoadImage(car.ImagePath);
                    }
                }
                else
                {
                    MessageBox.Show(response.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        // ==================== UPLOAD ẢNH ====================
        private void btnUploadImage_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                openFileDialog.Title = "Chọn ảnh xe";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string sourceFile = openFileDialog.FileName;

                    // Tạo thư mục lưu ảnh nếu chưa có
                    string projectPath = Directory.GetParent(Application.StartupPath).Parent.Parent.FullName;
                    string imagesFolder = Path.Combine(projectPath, "Images", "Cars");

                    if (!Directory.Exists(imagesFolder))
                    {
                        Directory.CreateDirectory(imagesFolder);
                    }

                    // Tạo tên file mới (tránh trùng)
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(sourceFile);
                    string destFile = Path.Combine(imagesFolder, fileName);

                    // Copy file vào thư mục project
                    File.Copy(sourceFile, destFile, true);

                    // Lưu đường dẫn tương đối
                    selectedImagePath = Path.Combine("Images", "Cars", fileName);

                    // Hiển thị ảnh
                    LoadImage(destFile);

                    // Hiển thị tên file
                    lblImagePath.Text = Path.GetFileName(destFile);
                    lblImagePath.ForeColor = Color.Green;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi upload ảnh: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== LOAD ẢNH VÀO PICTUREBOX ====================
        private void LoadImage(string imagePath)
        {
            try
            {
                // Nếu là đường dẫn tương đối
                if (!Path.IsPathRooted(imagePath))
                {
                    string projectPath = Directory.GetParent(Application.StartupPath).Parent.Parent.FullName;
                    imagePath = Path.Combine(projectPath, imagePath);
                }

                if (File.Exists(imagePath))
                {
                    // Load ảnh
                    using (var img = Image.FromFile(imagePath))
                    {
                        picCarImage.Image = new Bitmap(img);
                    }

                    lblImagePath.Text = Path.GetFileName(imagePath);
                    lblImagePath.ForeColor = Color.Green;
                }
                else
                {
                    picCarImage.Image = null;
                    lblImagePath.Text = "File không tồn tại";
                    lblImagePath.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                picCarImage.Image = null;
                lblImagePath.Text = "Lỗi load ảnh: " + ex.Message;
                lblImagePath.ForeColor = Color.Red;
            }
        }

        // ==================== LƯU DỮ LIỆU ====================
        private void BtnSave_Click(object sender, EventArgs e)
        {
            btnSave.Text = "⏳ Đang xử lý...";
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            try
            {
                // Validate
                if (string.IsNullOrWhiteSpace(txtCarName.Text))
                {
                    ShowError("Vui lòng nhập tên xe!");
                    txtCarName.Focus();
                    return;
                }

                if (!decimal.TryParse(txtPrice.Text, out decimal price) || price <= 0)
                {
                    ShowError("Giá xe không hợp lệ!");
                    txtPrice.Focus();
                    return;
                }

                if (!int.TryParse(txtStockQuantity.Text, out int stock) || stock < 0)
                {
                    ShowError("Số lượng không hợp lệ!");
                    txtStockQuantity.Focus();
                    return;
                }

                if (cboCarType.SelectedIndex < 0)
                {
                    ShowError("Vui lòng chọn loại xe!");
                    return;
                }

                if (cboStatus.SelectedIndex < 0)
                {
                    ShowError("Vui lòng chọn trạng thái!");
                    return;
                }

                // Lấy CarTypeID
                var carTypes = cboCarType.Tag as System.Collections.Generic.List<CarTypeDTO>;
                int carTypeID = carTypes[cboCarType.SelectedIndex].CarTypeID;

                ApiResponse response;

                if (isEditMode)
                {
                    // Sửa xe
                    CarDTO carDTO = new CarDTO
                    {
                        CarID = carId.Value,
                        CarName = txtCarName.Text.Trim(),
                        Price = price,
                        CarTypeID = carTypeID,
                        StockQuantity = stock,
                        Status = cboStatus.SelectedItem.ToString(),
                        ImagePath = selectedImagePath
                    };

                    response = carBLL.UpdateCar(carDTO);
                }
                else
                {
                    // Thêm xe
                    AddCarDTO addCarDTO = new AddCarDTO
                    {
                        CarName = txtCarName.Text.Trim(),
                        Price = price,
                        CarTypeID = carTypeID,
                        StockQuantity = stock,
                        Status = cboStatus.SelectedItem.ToString(),
                        ImagePath = selectedImagePath
                    };

                    response = carBLL.AddCar(addCarDTO);
                }

                if (response.Success)
                {
                    MessageBox.Show(response.Message, "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    ShowError(response.Message);
                }
            }
            catch (Exception ex)
            {
                ShowError("Lỗi: " + ex.Message);
            }
            finally
            {
                btnSave.Text = isEditMode ? "💾 LƯU THAY ĐỔI" : "➕ THÊM XE";
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
            }
        }

        // ==================== HELPER METHODS ====================
        private void ShowError(string message)
        {
            MessageBox.Show(message, "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // ==================== KEY PRESS ====================
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}