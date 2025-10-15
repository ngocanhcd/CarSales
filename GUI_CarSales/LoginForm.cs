using BLL_CarSales;
using DTO_Carsales;
using System;
using System.Drawing;
using System.Windows.Forms;
using UTIL_CarSales;
using static Guna.UI2.WinForms.Suite.Descriptions;

namespace GUI_CarSales
{
    public partial class LoginForm : Form
    {
        private UserBLL userBLL;

        public LoginForm()
        {
            InitializeComponent();
            userBLL = new UserBLL();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            // Cấu hình form
            this.Text = "Đăng nhập - Car Sales System";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Size = new Size(822, 548);

            // Cấu hình controls
            txtUsername.PlaceholderText = "Tên đăng nhập";
            txtUsername.IconLeft = Properties.Resources.user_icon; // Cần thêm icon vào Resources
            txtUsername.BorderRadius = 10;
            txtUsername.Font = new Font("Segoe UI", 12F);

            txtPassword.PlaceholderText = "Mật khẩu";
            txtPassword.IconLeft = Properties.Resources.lock_icon; // Cần thêm icon vào Resources
            txtPassword.UseSystemPasswordChar = true;
            txtPassword.BorderRadius = 10;
            txtPassword.Font = new Font("Segoe UI", 12F);
            txtPassword.IconRight = Properties.Resources.eye_icon; // Icon hiện/ẩn mật khẩu
            txtPassword.IconRightCursor = Cursors.Hand;

            btnLogin.Text = "ĐĂNG NHẬP";
            btnLogin.BorderRadius = 10;
            btnLogin.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnLogin.FillColor = Color.FromArgb(94, 148, 255);
            btnLogin.HoverState.FillColor = Color.FromArgb(60, 120, 240);
            btnLogin.Cursor = Cursors.Hand;

            // Events
            btnLogin.Click += BtnLogin_Click;
            txtPassword.KeyDown += TxtPassword_KeyDown;
            txtPassword.IconRightClick += TxtPassword_IconRightClick;

            // Thêm link đăng ký (nếu chưa có)
            LinkLabel lnkRegister = new LinkLabel();
            lnkRegister.Text = "Chưa có tài khoản? Đăng ký ngay";
            lnkRegister.Location = new Point(txtUsername.Left, btnLogin.Bottom + 20);
            lnkRegister.AutoSize = true;
            lnkRegister.LinkColor = Color.FromArgb(94, 148, 255);
            lnkRegister.Font = new Font("Segoe UI", 10F);
            lnkRegister.Cursor = Cursors.Hand;
            lnkRegister.LinkClicked += LnkRegister_LinkClicked;
            this.Controls.Add(lnkRegister);

            // Thêm label tiêu đề
            Label lblTitle = new Label();
            lblTitle.Text = "CAR SALES SYSTEM";
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(94, 148, 255);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(txtUsername.Left, txtUsername.Top - 80);
            this.Controls.Add(lblTitle);

            Label lblSubtitle = new Label();
            lblSubtitle.Text = "Hệ thống quản lý bán xe ô tô";
            lblSubtitle.Font = new Font("Segoe UI", 11F);
            lblSubtitle.ForeColor = Color.Gray;
            lblSubtitle.AutoSize = true;
            lblSubtitle.Location = new Point(txtUsername.Left, lblTitle.Bottom + 5);
            this.Controls.Add(lblSubtitle);

            // Checkbox "Remember Me"
            Guna.UI2.WinForms.Guna2CheckBox chkRemember = new Guna.UI2.WinForms.Guna2CheckBox();
            chkRemember.Text = "Ghi nhớ đăng nhập";
            chkRemember.Location = new Point(txtPassword.Left, txtPassword.Bottom + 15);
            chkRemember.AutoSize = true;
            chkRemember.Font = new Font("Segoe UI", 10F);
            this.Controls.Add(chkRemember);

            // Button đóng
            Guna.UI2.WinForms.Guna2Button btnClose = new Guna.UI2.WinForms.Guna2Button();
            btnClose.Size = new Size(40, 40);
            btnClose.Location = new Point(this.Width - 50, 10);
            btnClose.Text = "✕";
            btnClose.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnClose.BorderRadius = 5;
            btnClose.FillColor = Color.Transparent;
            btnClose.ForeColor = Color.Gray;
            btnClose.HoverState.FillColor = Color.Red;
            btnClose.HoverState.ForeColor = Color.White;
            btnClose.Cursor = Cursors.Hand;
            btnClose.Click += (s, e) => Application.Exit();
            this.Controls.Add(btnClose);
        }

        private void TxtPassword_IconRightClick(object sender, EventArgs e)
        {
            // Toggle hiện/ẩn mật khẩu
            txtPassword.UseSystemPasswordChar = !txtPassword.UseSystemPasswordChar;
        }

        private void TxtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnLogin_Click(sender, e);
            }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            // Hiển thị loading
            btnLogin.Text = "Đang xử lý...";
            btnLogin.Enabled = false;

            try
            {
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text;

                // Gọi BLL để xác thực
                ApiResponse<UserDTO> response = userBLL.AuthenticateUser(username, password);

                if (response.Success)
                {
                    // Lưu thông tin user vào Session
                    SessionManager.SetCurrentUser(response.Data);

                    MessageBox.Show(response.Message, "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Mở form chính theo role
                    this.Hide();
                    OpenMainForm(response.Data.Role);
                }
                else
                {
                    MessageBox.Show(response.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnLogin.Text = "ĐĂNG NHẬP";
                btnLogin.Enabled = true;
            }
        }

        private void OpenMainForm(string role)
        {
            Form mainForm = null;

            switch (role)
            {
                case "Admin":
                    mainForm = new AdminMainForm();
                    break;
                case "Employee":
                    mainForm = new EmployeeMainForm();
                    break;
                case "Customer":
                    mainForm = new CustomerMainForm();
                    break;
                default:
                    MessageBox.Show("Role không hợp lệ!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Show();
                    return;
            }

            mainForm.FormClosed += (s, e) =>
            {
                this.Show();
                txtUsername.Clear();
                txtPassword.Clear();
                txtUsername.Focus();
            };

            mainForm.Show();
        }

        private void LnkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Mở form đăng ký
            RegisterForm registerForm = new RegisterForm();
            DialogResult result = registerForm.ShowDialog();

            // Nếu đăng ký thành công, tự động điền username
            if (result == DialogResult.OK)
            {
                // Có thể tự động điền username nếu muốn
                txtUsername.Focus();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            SessionManager.Logout();
            base.OnFormClosing(e);
        }
    }
}