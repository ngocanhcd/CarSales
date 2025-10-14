using BLL_CarSales;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_CarSales
{
    public partial class LoginForm : Form
    {
        private UserBLL userBLL;

        public LoginForm()
        {
            InitializeComponent();
            userBLL = new BLL_CarSales.UserBLL();

            // Gắn sự kiện Click cho nút
            btnLogin.Click += btnLogin_Click;

            // Ẩn ký tự mật khẩu & đặt text nút
            txtPassword.UseSystemPasswordChar = true;
            btnLogin.Text = "Đăng nhập";

            // Enter để đăng nhập
            txtPassword.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) btnLogin.PerformClick(); };
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            bool isAuthenticated = userBLL.AuthenticateUser(username, password);

            if (isAuthenticated)
            {
                MessageBox.Show("Đăng nhập thành công!");
                // Open main menu or dashboard based on role
                // For now, show a message as a placeholder
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!");
            }
        }
    }
}
