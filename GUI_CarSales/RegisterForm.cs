using System;
using System.Drawing;
using System.Windows.Forms;
using BLL_CarSales;
using DTO_Carsales;

namespace GUI_CarSales
{
    public partial class RegisterForm : Form
    {
        private UserBLL userBLL;

        public RegisterForm()
        {
            InitializeComponent();
            userBLL = new UserBLL();

            // Thêm sự kiện
            this.Load += RegisterForm_Load;
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            // Focus vào ô đầu tiên
            txtFullName.Focus();

            // Enter để submit
            txtAddress.KeyDown += (s, ev) =>
            {
                if (ev.KeyCode == Keys.Enter)
                {
                    btnRegister.PerformClick();
                }
            };

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

        // ==================== HIỂN THỊ MẬT KHẨU ====================
        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
            txtConfirmPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        // ==================== ĐĂNG KÝ ====================
        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Disable button và hiển thị loading
            btnRegister.Text = "⏳ Đang xử lý...";
            btnRegister.Enabled = false;
            btnCancel.Enabled = false;

            try
            {
                // Tạo DTO
                RegisterDTO registerDTO = new RegisterDTO
                {
                    FullName = txtFullName.Text.Trim(),
                    Username = txtUsername.Text.Trim(),
                    Password = txtPassword.Text,
                    ConfirmPassword = txtConfirmPassword.Text,
                    Email = txtEmail.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    Address = txtAddress.Text.Trim()
                };

                // Validation trước khi gọi BLL (UI validation)
                if (string.IsNullOrWhiteSpace(registerDTO.FullName))
                {
                    ShowError("Vui lòng nhập họ tên!");
                    txtFullName.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(registerDTO.Username))
                {
                    ShowError("Vui lòng nhập tên đăng nhập!");
                    txtUsername.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(registerDTO.Password))
                {
                    ShowError("Vui lòng nhập mật khẩu!");
                    txtPassword.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(registerDTO.ConfirmPassword))
                {
                    ShowError("Vui lòng xác nhận mật khẩu!");
                    txtConfirmPassword.Focus();
                    return;
                }

                // Gọi BLL để đăng ký
                ApiResponse response = userBLL.Register(registerDTO);

                if (response.Success)
                {
                    // Thành công
                    MessageBox.Show(
                        response.Message + "\n\nBạn có thể đăng nhập ngay bây giờ!",
                        "Đăng ký thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    // Thất bại
                    ShowError(response.Message);
                }
            }
            catch (Exception ex)
            {
                ShowError("Lỗi hệ thống: " + ex.Message);
            }
            finally
            {
                // Reset button
                btnRegister.Text = "✓ ĐĂNG KÝ";
                btnRegister.Enabled = true;
                btnCancel.Enabled = true;
            }
        }

        // ==================== HỦY ====================
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn hủy đăng ký?\nThông tin đã nhập sẽ không được lưu.",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        // ==================== ĐÓNG FORM ====================
        private void btnClose_Click(object sender, EventArgs e)
        {
            btnCancel_Click(sender, e);
        }

        // ==================== HELPER METHODS ====================
        private void ShowError(string message)
        {
            MessageBox.Show(
                message,
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
        }

        private void ShowSuccess(string message)
        {
            MessageBox.Show(
                message,
                "Thành công",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        // ==================== VALIDATION REALTIME ====================
        // Có thể thêm validation realtime khi người dùng nhập
        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            // Chỉ cho phép chữ, số và dấu gạch dưới
            string text = txtUsername.Text;
            if (text.Length > 20)
            {
                txtUsername.Text = text.Substring(0, 20);
                txtUsername.SelectionStart = txtUsername.Text.Length;
            }
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            // Chỉ cho phép nhập số
            string text = txtPhone.Text;
            string numericText = "";

            foreach (char c in text)
            {
                if (char.IsDigit(c))
                    numericText += c;
            }

            if (numericText != text)
            {
                txtPhone.Text = numericText;
                txtPhone.SelectionStart = txtPhone.Text.Length;
            }

            // Giới hạn 11 số
            if (txtPhone.Text.Length > 11)
            {
                txtPhone.Text = txtPhone.Text.Substring(0, 11);
                txtPhone.SelectionStart = txtPhone.Text.Length;
            }
        }

        // ==================== OVERRIDE KEY PRESS ====================
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // ESC để đóng form
            if (keyData == Keys.Escape)
            {
                btnCancel_Click(this, EventArgs.Empty);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}