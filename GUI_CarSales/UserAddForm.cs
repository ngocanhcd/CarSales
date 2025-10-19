using System;
using System.Drawing;
using System.Windows.Forms;
using BLL_CarSales;
using DTO_Carsales;

namespace GUI_CarSales
{
    public partial class UserAddForm : Form
    {
        private UserBLL userBLL;

        public UserAddForm()
        {
            InitializeComponent();
            userBLL = new UserBLL();

            // Set default role
            cboRole.SelectedIndex = 1; // Employee
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.Text = "⏳ Đang tạo...";
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(txtFullName.Text))
                {
                    ShowError("Vui lòng nhập họ tên!");
                    txtFullName.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    ShowError("Vui lòng nhập username!");
                    txtUsername.Focus();
                    return;
                }

                if (txtUsername.Text.Length < 4 || txtUsername.Text.Length > 20)
                {
                    ShowError("Username phải từ 4-20 ký tự!");
                    txtUsername.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    ShowError("Vui lòng nhập mật khẩu!");
                    txtPassword.Focus();
                    return;
                }

                if (txtPassword.Text.Length < 6)
                {
                    ShowError("Mật khẩu phải có ít nhất 6 ký tự!");
                    txtPassword.Focus();
                    return;
                }

                if (txtPassword.Text != txtConfirmPassword.Text)
                {
                    ShowError("Mật khẩu xác nhận không khớp!");
                    txtConfirmPassword.Focus();
                    return;
                }

                if (cboRole.SelectedIndex < 0)
                {
                    ShowError("Vui lòng chọn vai trò!");
                    return;
                }

                // Tạo DTO
                RegisterDTO registerDTO = new RegisterDTO
                {
                    FullName = txtFullName.Text.Trim(),
                    Username = txtUsername.Text.Trim(),
                    Password = txtPassword.Text,
                    ConfirmPassword = txtConfirmPassword.Text,
                    Email = txtEmail.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    Address = ""
                };

                // Đăng ký
                var response = userBLL.Register(registerDTO);

                if (response.Success)
                {
                    // Cập nhật Role nếu không phải Customer
                    string selectedRole = cboRole.SelectedItem.ToString();
                    if (selectedRole != "Customer")
                    {
                        var roleResponse = userBLL.UpdateUserRole(registerDTO.Username, selectedRole);

                        if (!roleResponse.Success)
                        {
                            MessageBox.Show("Tạo user thành công nhưng cập nhật role thất bại!\n" +
                                roleResponse.Message, "Cảnh báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                            return;
                        }
                    }

                    // Thông báo thành công
                    MessageBox.Show(
                        $"✅ TẠO TÀI KHOẢN THÀNH CÔNG!\n\n" +
                        $"━━━━━━━━━━━━━━━━━━━━━━━━\n" +
                        $"Họ tên: {registerDTO.FullName}\n" +
                        $"Username: {registerDTO.Username}\n" +
                        $"Password: {registerDTO.Password}\n" +
                        $"Vai trò: {GetRoleText(selectedRole)}\n" +
                        $"━━━━━━━━━━━━━━━━━━━━━━━━\n\n" +
                        $"⚠️ Lưu lại thông tin đăng nhập!",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

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
                btnSave.Text = "➕ TẠO TÀI KHOẢN";
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            btnCancel_Click(sender, e);
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
            txtConfirmPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        private string GetRoleText(string role)
        {
            switch (role)
            {
                case "Admin": return "👑 Quản trị viên";
                case "Employee": return "👨‍💼 Nhân viên";
                case "Customer": return "🛒 Khách hàng";
                default: return role;
            }
        }

        private void ShowError(string message)
        {
            MessageBox.Show(
                message,
                "⚠️ Lỗi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
        }

        // Drag form
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
    }
}