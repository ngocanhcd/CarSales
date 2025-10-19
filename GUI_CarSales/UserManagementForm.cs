using System;
using System.Drawing;
using System.Windows.Forms;
using BLL_CarSales;
using DTO_Carsales;

namespace GUI_CarSales
{
    public partial class UserManagementForm : Form
    {
        private UserManagementBLL userMgmtBLL;

        public UserManagementForm()
        {
            InitializeComponent();
            userMgmtBLL = new UserManagementBLL();

            this.Load += UserManagementForm_Load;
        }

        private void UserManagementForm_Load(object sender, EventArgs e)
        {
            // Setup DataGridView
            SetupDataGridView();

            // Load roles
            LoadRoles();

            // Load data
            LoadAllUsers();

            // Gắn events
            btnSearch.Click += btnSearch_Click;
            btnRefresh.Click += btnRefresh_Click;
            btnViewDetails.Click += btnViewDetails_Click;
            btnEditUser.Click += btnEditUser_Click;
            btnResetPassword.Click += btnResetPassword_Click;
            btnDeleteUser.Click += btnDeleteUser_Click;
            btnClose.Click += btnClose_Click;
            btnAddUser.Click += btnAddUser_Click;

        }

        // ==================== SETUP ====================
        private void SetupDataGridView()
        {
            dgvUsers.Columns.Clear();
            dgvUsers.AutoGenerateColumns = false;

            dgvUsers.Columns.Add("UserID", "ID");
            dgvUsers.Columns.Add("FullName", "Họ tên");
            dgvUsers.Columns.Add("Username", "Tên đăng nhập");
            dgvUsers.Columns.Add("Email", "Email");
            dgvUsers.Columns.Add("Phone", "Điện thoại");
            dgvUsers.Columns.Add("Role", "Vai trò");
            dgvUsers.Columns.Add("CreatedAt", "Ngày tạo");

            dgvUsers.Columns[0].Width = 50;
            dgvUsers.Columns[1].Width = 180;
            dgvUsers.Columns[2].Width = 130;
            dgvUsers.Columns[3].Width = 180;
            dgvUsers.Columns[4].Width = 110;
            dgvUsers.Columns[5].Width = 100;
            dgvUsers.Columns[6].Width = 130;

            dgvUsers.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvUsers.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void LoadRoles()
        {
            cboRole.SelectedIndex = 0; // "Tất cả vai trò"
        }

        // ==================== LOAD DỮ LIỆU ====================
        private void LoadAllUsers()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                var response = userMgmtBLL.GetAllUsers();

                if (response.Success)
                {
                    DisplayUsers(response.Data);
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

        private void DisplayUsers(System.Collections.Generic.List<UserDTO> users)
        {
            dgvUsers.Rows.Clear();

            int totalUsers = users.Count;
            int admins = 0;
            int employees = 0;
            int customers = 0;

            foreach (var user in users)
            {
                int rowIndex = dgvUsers.Rows.Add(
                    user.UserID,
                    user.FullName,
                    user.Username,
                    user.Email ?? "N/A",
                    user.Phone ?? "N/A",
                    GetRoleText(user.Role),
                    user.CreatedAt.ToString("dd/MM/yyyy")
                );

                // Tô màu theo vai trò
                DataGridViewRow row = dgvUsers.Rows[rowIndex];
                ApplyRoleColor(row, user.Role);

                // Thống kê
                switch (user.Role)
                {
                    case "Admin":
                        admins++;
                        break;
                    case "Employee":
                        employees++;
                        break;
                    case "Customer":
                        customers++;
                        break;
                }
            }

            // Cập nhật labels
            lblTotalUsers.Text = $"Tổng người dùng: {totalUsers}";
            lblAdmins.Text = $"| Admin: {admins}";
            lblEmployees.Text = $"| Nhân viên: {employees}";
            lblCustomers.Text = $"| Khách hàng: {customers}";
        }

        // ==================== TÌM KIẾM ====================
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                string keyword = txtSearch.Text.Trim();
                string role = cboRole.SelectedIndex > 0 ? cboRole.SelectedItem.ToString() : null;

                var response = userMgmtBLL.SearchUsers(keyword, role);

                if (response.Success)
                {
                    DisplayUsers(response.Data);
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

        // ==================== LÀM MỚI ====================
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cboRole.SelectedIndex = 0;
            LoadAllUsers();
        }

        // ==================== XEM CHI TIẾT ====================
        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn người dùng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int userId = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells[0].Value);

            // Hiển thị thông tin chi tiết
            var response = userMgmtBLL.GetUserById(userId);

            if (response.Success)
            {
                UserDTO user = response.Data;
                string details = $"ID: {user.UserID}\n" +
                                $"Họ tên: {user.FullName}\n" +
                                $"Username: {user.Username}\n" +
                                $"Email: {user.Email ?? "N/A"}\n" +
                                $"Điện thoại: {user.Phone ?? "N/A"}\n" +
                                $"Địa chỉ: {user.Address ?? "N/A"}\n" +
                                $"Vai trò: {GetRoleText(user.Role)}\n" +
                                $"Ngày tạo: {user.CreatedAt:dd/MM/yyyy HH:mm}";

                MessageBox.Show(details, "Thông tin người dùng",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(response.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== SỬA NGƯỜI DÙNG ====================
        private void btnEditUser_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn người dùng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int userId = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells[0].Value);

            // Lấy thông tin user
            var response = userMgmtBLL.GetUserById(userId);

            if (!response.Success)
            {
                MessageBox.Show(response.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            UserDTO user = response.Data;

            // Tạo form edit đơn giản
            using (Form editForm = new Form())
            {
                editForm.Text = "Sửa thông tin người dùng";
                editForm.Size = new Size(450, 350);
                editForm.StartPosition = FormStartPosition.CenterParent;
                editForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                editForm.MaximizeBox = false;
                editForm.MinimizeBox = false;

                int yPos = 20;

                // FullName
                Label lblFullName = new Label { Text = "Họ tên:", Location = new Point(20, yPos), AutoSize = true };
                TextBox txtFullName = new TextBox { Text = user.FullName, Location = new Point(20, yPos + 25), Width = 400 };
                editForm.Controls.Add(lblFullName);
                editForm.Controls.Add(txtFullName);
                yPos += 65;

                // Email
                Label lblEmail = new Label { Text = "Email:", Location = new Point(20, yPos), AutoSize = true };
                TextBox txtEmail = new TextBox { Text = user.Email, Location = new Point(20, yPos + 25), Width = 400 };
                editForm.Controls.Add(lblEmail);
                editForm.Controls.Add(txtEmail);
                yPos += 65;

                // Phone
                Label lblPhone = new Label { Text = "Điện thoại:", Location = new Point(20, yPos), AutoSize = true };
                TextBox txtPhone = new TextBox { Text = user.Phone, Location = new Point(20, yPos + 25), Width = 400 };
                editForm.Controls.Add(lblPhone);
                editForm.Controls.Add(txtPhone);
                yPos += 65;

                // Address
                Label lblAddress = new Label { Text = "Địa chỉ:", Location = new Point(20, yPos), AutoSize = true };
                TextBox txtAddress = new TextBox { Text = user.Address, Location = new Point(20, yPos + 25), Width = 400 };
                editForm.Controls.Add(lblAddress);
                editForm.Controls.Add(txtAddress);
                yPos += 65;

                // Buttons
                Button btnSave = new Button { Text = "Lưu", Location = new Point(245, yPos), Size = new Size(80, 35) };
                Button btnCancel = new Button { Text = "Hủy", Location = new Point(335, yPos), Size = new Size(80, 35) };

                btnSave.Click += (s, ev) =>
                {
                    UpdateProfileDTO updateDTO = new UpdateProfileDTO
                    {
                        UserID = userId,
                        FullName = txtFullName.Text.Trim(),
                        Email = txtEmail.Text.Trim(),
                        Phone = txtPhone.Text.Trim(),
                        Address = txtAddress.Text.Trim()
                    };

                    var updateResponse = userMgmtBLL.UpdateUserProfile(updateDTO);

                    if (updateResponse.Success)
                    {
                        MessageBox.Show(updateResponse.Message, "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        editForm.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show(updateResponse.Message, "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };

                btnCancel.Click += (s, ev) => editForm.Close();

                editForm.Controls.Add(btnSave);
                editForm.Controls.Add(btnCancel);

                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadAllUsers();
                }
            }
        }

        // ==================== RESET MẬT KHẨU ====================
        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn người dùng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int userId = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells[0].Value);
            string username = dgvUsers.SelectedRows[0].Cells[2].Value.ToString();

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc muốn RESET mật khẩu cho user '{username}'?\n\n" +
                "Mật khẩu mới sẽ là: 123456",
                "Xác nhận reset",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    var response = userMgmtBLL.ResetPassword(userId, "123456");

                    if (response.Success)
                    {
                        MessageBox.Show(response.Message, "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(response.Message, "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi reset mật khẩu: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ==================== XÓA NGƯỜI DÙNG ====================
        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn người dùng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int userId = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells[0].Value);
            string fullName = dgvUsers.SelectedRows[0].Cells[1].Value.ToString();

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc muốn XÓA người dùng '{fullName}'?\n\n" +
                "⚠️ Thao tác này không thể hoàn tác!",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    var response = userMgmtBLL.DeleteUser(userId);

                    if (response.Success)
                    {
                        MessageBox.Show(response.Message, "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadAllUsers();
                    }
                    else
                    {
                        MessageBox.Show(response.Message, "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa người dùng: " + ex.Message, "Lỗi",
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
        private void ApplyRoleColor(DataGridViewRow row, string role)
        {
            switch (role)
            {
                case "Admin":
                    row.DefaultCellStyle.BackColor = Color.FromArgb(248, 215, 218);
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(114, 28, 36);
                    break;
                case "Employee":
                    row.DefaultCellStyle.BackColor = Color.FromArgb(209, 231, 221);
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(21, 87, 36);
                    break;
                case "Customer":
                    row.DefaultCellStyle.BackColor = Color.FromArgb(204, 229, 255);
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(1, 67, 97);
                    break;
            }
        }

        private string GetRoleText(string role)
        {
            switch (role)
            {
                case "Admin": return "Quản trị";
                case "Employee": return "Nhân viên";
                case "Customer": return "Khách hàng";
                default: return role;
            }
        }
        private void btnAddUser_Click(object sender, EventArgs e)
        {
            UserAddForm addForm = new UserAddForm();

            if (addForm.ShowDialog() == DialogResult.OK)
            {
                LoadAllUsers(); // Reload danh sách
            }
        }
    }
}