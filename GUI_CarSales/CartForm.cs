using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DTO_Carsales;

namespace GUI_CarSales
{
    public partial class CartForm : Form
    {
        private List<CartItemDTO> cartItems;
        private DataGridView dgvCart;
        private Label lblTotal;
        private ComboBox cboPaymentMethod;
        private Button btnCheckout;
        private Button btnCancel;
        private Button btnRemove;
        private Button btnUpdateQuantity;

        public string SelectedPaymentMethod { get; private set; }

        public CartForm(List<CartItemDTO> items)
        {
            this.cartItems = items;
            InitializeComponents();
            LoadCartItems();
        }

        private void InitializeComponents()
        {
            this.Text = "Giỏ hàng của bạn";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(243, 244, 246);

            // Title Panel
            Panel pnlTop = new Panel();
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Height = 60;
            pnlTop.BackColor = Color.FromArgb(94, 148, 255);
            this.Controls.Add(pnlTop);

            Label lblTitle = new Label();
            lblTitle.Text = "🛒 GIỎ HÀNG CỦA BẠN";
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(20, 17);
            pnlTop.Controls.Add(lblTitle);

            // DataGridView
            dgvCart = new DataGridView();
            dgvCart.Location = new Point(20, 80);
            dgvCart.Size = new Size(740, 300);
            dgvCart.AllowUserToAddRows = false;
            dgvCart.AllowUserToDeleteRows = false;
            dgvCart.ReadOnly = true;
            dgvCart.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCart.MultiSelect = false;
            dgvCart.AutoGenerateColumns = false;
            dgvCart.BackgroundColor = Color.White;
            dgvCart.BorderStyle = BorderStyle.None;
            dgvCart.RowHeadersVisible = false;
            dgvCart.RowTemplate.Height = 40;
            dgvCart.ColumnHeadersHeight = 45;

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { Name = "CarName", HeaderText = "Tên xe", Width = 250 });
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { Name = "Price", HeaderText = "Giá", Width = 130 });
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { Name = "Quantity", HeaderText = "SL", Width = 70 });
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { Name = "TotalPrice", HeaderText = "Thành tiền", Width = 140 });

            dgvCart.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(94, 148, 255);
            dgvCart.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvCart.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvCart.EnableHeadersVisualStyles = false;

            this.Controls.Add(dgvCart);

            // Action buttons
            btnRemove = new Button();
            btnRemove.Text = "🗑️ Xóa";
            btnRemove.Location = new Point(20, 395);
            btnRemove.Size = new Size(100, 35);
            btnRemove.BackColor = Color.FromArgb(220, 53, 69);
            btnRemove.ForeColor = Color.White;
            btnRemove.FlatStyle = FlatStyle.Flat;
            btnRemove.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRemove.Cursor = Cursors.Hand;
            btnRemove.Click += BtnRemove_Click;
            this.Controls.Add(btnRemove);

            btnUpdateQuantity = new Button();
            btnUpdateQuantity.Text = "✏️ Sửa SL";
            btnUpdateQuantity.Location = new Point(130, 395);
            btnUpdateQuantity.Size = new Size(100, 35);
            btnUpdateQuantity.BackColor = Color.FromArgb(255, 193, 7);
            btnUpdateQuantity.ForeColor = Color.White;
            btnUpdateQuantity.FlatStyle = FlatStyle.Flat;
            btnUpdateQuantity.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnUpdateQuantity.Cursor = Cursors.Hand;
            btnUpdateQuantity.Click += BtnUpdateQuantity_Click;
            this.Controls.Add(btnUpdateQuantity);

            // Total
            lblTotal = new Label();
            lblTotal.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTotal.ForeColor = Color.FromArgb(220, 53, 69);
            lblTotal.Location = new Point(20, 450);
            lblTotal.Size = new Size(400, 30);
            lblTotal.Text = "💰 Tổng cộng: 0đ";
            this.Controls.Add(lblTotal);

            // Payment method
            Label lblPayment = new Label();
            lblPayment.Text = "Phương thức thanh toán:";
            lblPayment.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPayment.Location = new Point(20, 490);
            lblPayment.AutoSize = true;
            this.Controls.Add(lblPayment);

            cboPaymentMethod = new ComboBox();
            cboPaymentMethod.Location = new Point(20, 515);
            cboPaymentMethod.Size = new Size(300, 30);
            cboPaymentMethod.DropDownStyle = ComboBoxStyle.DropDownList;
            cboPaymentMethod.Items.AddRange(new object[] {
                "Bank Transfer",
                "Cash",
                "Credit Card"
            });
            cboPaymentMethod.SelectedIndex = 0;
            cboPaymentMethod.Font = new Font("Segoe UI", 10F);
            this.Controls.Add(cboPaymentMethod);

            // Checkout button
            btnCheckout = new Button();
            btnCheckout.Text = "✅ ĐẶT HÀNG";
            btnCheckout.Location = new Point(570, 505);
            btnCheckout.Size = new Size(180, 45);
            btnCheckout.BackColor = Color.FromArgb(40, 167, 69);
            btnCheckout.ForeColor = Color.White;
            btnCheckout.FlatStyle = FlatStyle.Flat;
            btnCheckout.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnCheckout.Cursor = Cursors.Hand;
            btnCheckout.Click += BtnCheckout_Click;
            this.Controls.Add(btnCheckout);

            // Cancel button
            btnCancel = new Button();
            btnCancel.Text = "← Đóng";
            btnCancel.Location = new Point(380, 505);
            btnCancel.Size = new Size(180, 45);
            btnCancel.BackColor = Color.FromArgb(108, 117, 125);
            btnCancel.ForeColor = Color.White;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;
            this.Controls.Add(btnCancel);
        }

        private void LoadCartItems()
        {
            dgvCart.Rows.Clear();
            decimal total = 0;

            foreach (var item in cartItems)
            {
                dgvCart.Rows.Add(
                    item.CarName,
                    FormatCurrency(item.Price),
                    item.Quantity,
                    FormatCurrency(item.TotalPrice)
                );

                total += item.TotalPrice;
            }

            lblTotal.Text = $"💰 Tổng cộng: {FormatCurrency(total)}";

            if (cartItems.Count == 0)
            {
                btnCheckout.Enabled = false;
                lblTotal.Text = "💰 Giỏ hàng trống";
            }
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            if (dgvCart.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn xe cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedIndex = dgvCart.SelectedRows[0].Index;
            string carName = cartItems[selectedIndex].CarName;

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc muốn xóa '{carName}' khỏi giỏ hàng?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                cartItems.RemoveAt(selectedIndex);
                LoadCartItems();
            }
        }

        private void BtnUpdateQuantity_Click(object sender, EventArgs e)
        {
            if (dgvCart.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn xe cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedIndex = dgvCart.SelectedRows[0].Index;
            CartItemDTO item = cartItems[selectedIndex];

            using (Form quantityForm = new Form())
            {
                quantityForm.Text = "Cập nhật số lượng";
                quantityForm.Size = new Size(350, 200);
                quantityForm.StartPosition = FormStartPosition.CenterParent;
                quantityForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                quantityForm.MaximizeBox = false;
                quantityForm.MinimizeBox = false;

                Label lblInfo = new Label();
                lblInfo.Text = $"Xe: {item.CarName}\nGiá: {FormatCurrency(item.Price)}";
                lblInfo.Location = new Point(20, 20);
                lblInfo.AutoSize = true;
                quantityForm.Controls.Add(lblInfo);

                Label lblQty = new Label();
                lblQty.Text = "Số lượng:";
                lblQty.Location = new Point(20, 70);
                lblQty.AutoSize = true;
                quantityForm.Controls.Add(lblQty);

                NumericUpDown numQuantity = new NumericUpDown();
                numQuantity.Location = new Point(20, 95);
                numQuantity.Size = new Size(300, 30);
                numQuantity.Minimum = 1;
                numQuantity.Maximum = 100;
                numQuantity.Value = item.Quantity;
                quantityForm.Controls.Add(numQuantity);

                Button btnOK = new Button();
                btnOK.Text = "Cập nhật";
                btnOK.Location = new Point(145, 130);
                btnOK.DialogResult = DialogResult.OK;
                quantityForm.Controls.Add(btnOK);

                Button btnCancelDialog = new Button();
                btnCancelDialog.Text = "Hủy";
                btnCancelDialog.Location = new Point(230, 130);
                btnCancelDialog.DialogResult = DialogResult.Cancel;
                quantityForm.Controls.Add(btnCancelDialog);

                quantityForm.AcceptButton = btnOK;
                quantityForm.CancelButton = btnCancelDialog;

                if (quantityForm.ShowDialog() == DialogResult.OK)
                {
                    item.Quantity = (int)numQuantity.Value;
                    item.TotalPrice = item.Quantity * item.Price;
                    LoadCartItems();
                }
            }
        }

        private void BtnCheckout_Click(object sender, EventArgs e)
        {
            if (cartItems.Count == 0)
            {
                MessageBox.Show("Giỏ hàng trống!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal total = 0;
            foreach (var item in cartItems)
            {
                total += item.TotalPrice;
            }

            string itemsList = "";
            foreach (var item in cartItems)
            {
                itemsList += $"- {item.CarName} x{item.Quantity} = {FormatCurrency(item.TotalPrice)}\n";
            }

            DialogResult result = MessageBox.Show(
                $"XÁC NHẬN ĐẶT HÀNG\n\n{itemsList}\n" +
                $"Tổng cộng: {FormatCurrency(total)}\n" +
                $"Thanh toán: {cboPaymentMethod.SelectedItem}\n\n" +
                "Bạn có chắc muốn đặt hàng?",
                "Xác nhận đặt hàng",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                SelectedPaymentMethod = cboPaymentMethod.SelectedItem.ToString();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private string FormatCurrency(decimal amount)
        {
            return amount.ToString("#,##0") + "đ";
        }
    }
}