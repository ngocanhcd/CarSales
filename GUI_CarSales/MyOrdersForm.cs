using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BLL_CarSales;
using DTO_Carsales;
using UTIL_CarSales;

namespace GUI_CarSales
{
    public partial class MyOrdersForm : Form
    {
        private OrderBLL orderBLL;
        private DataGridView dgvOrders;
        private Label lblTotal;
        private Button btnViewDetails;
        private Button btnCancelOrder;
        private Button btnRefresh;
        private Button btnClose;

        public MyOrdersForm()
        {
            orderBLL = new OrderBLL();
            InitializeComponents();
            LoadMyOrders();
        }

        private void InitializeComponents()
        {
            this.Text = "Đơn hàng của tôi";
            this.Size = new Size(900, 600);
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
            lblTitle.Text = "📋 ĐƠN HÀNG CỦA TÔI";
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(20, 17);
            pnlTop.Controls.Add(lblTitle);

            // Refresh button
            btnRefresh = new Button();
            btnRefresh.Text = "🔄 Làm mới";
            btnRefresh.Location = new Point(740, 15);
            btnRefresh.Size = new Size(120, 30);
            btnRefresh.BackColor = Color.FromArgb(108, 117, 125);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRefresh.Cursor = Cursors.Hand;
            btnRefresh.Click += (s, e) => LoadMyOrders();
            pnlTop.Controls.Add(btnRefresh);

            // Total label
            lblTotal = new Label();
            lblTotal.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTotal.ForeColor = Color.FromArgb(94, 148, 255);
            lblTotal.Location = new Point(20, 75);
            lblTotal.AutoSize = true;
            lblTotal.Text = "Tổng: 0 đơn hàng";
            this.Controls.Add(lblTotal);

            // DataGridView
            dgvOrders = new DataGridView();
            dgvOrders.Location = new Point(20, 105);
            dgvOrders.Size = new Size(840, 370);
            dgvOrders.AllowUserToAddRows = false;
            dgvOrders.AllowUserToDeleteRows = false;
            dgvOrders.ReadOnly = true;
            dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrders.MultiSelect = false;
            dgvOrders.AutoGenerateColumns = false;
            dgvOrders.BackgroundColor = Color.White;
            dgvOrders.BorderStyle = BorderStyle.None;
            dgvOrders.RowHeadersVisible = false;
            dgvOrders.RowTemplate.Height = 40;
            dgvOrders.ColumnHeadersHeight = 45;

            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "OrderID", HeaderText = "Mã ĐH", Width = 80 });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "OrderDate", HeaderText = "Ngày đặt", Width = 150 });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "TotalAmount", HeaderText = "Tổng tiền", Width = 150 });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "Status", HeaderText = "Trạng thái", Width = 130 });

            dgvOrders.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(94, 148, 255);
            dgvOrders.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvOrders.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvOrders.EnableHeadersVisualStyles = false;

            this.Controls.Add(dgvOrders);

            // Action buttons
            btnViewDetails = new Button();
            btnViewDetails.Text = "👁️ Chi tiết";
            btnViewDetails.Location = new Point(530, 495);
            btnViewDetails.Size = new Size(120, 40);
            btnViewDetails.BackColor = Color.FromArgb(94, 148, 255);
            btnViewDetails.ForeColor = Color.White;
            btnViewDetails.FlatStyle = FlatStyle.Flat;
            btnViewDetails.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnViewDetails.Cursor = Cursors.Hand;
            btnViewDetails.Click += BtnViewDetails_Click;
            this.Controls.Add(btnViewDetails);

            btnCancelOrder = new Button();
            btnCancelOrder.Text = "🚫 Hủy đơn";
            btnCancelOrder.Location = new Point(660, 495);
            btnCancelOrder.Size = new Size(120, 40);
            btnCancelOrder.BackColor = Color.FromArgb(220, 53, 69);
            btnCancelOrder.ForeColor = Color.White;
            btnCancelOrder.FlatStyle = FlatStyle.Flat;
            btnCancelOrder.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCancelOrder.Cursor = Cursors.Hand;
            btnCancelOrder.Click += BtnCancelOrder_Click;
            this.Controls.Add(btnCancelOrder);

            btnClose = new Button();
            btnClose.Text = "← Đóng";
            btnClose.Location = new Point(20, 495);
            btnClose.Size = new Size(120, 40);
            btnClose.BackColor = Color.FromArgb(108, 117, 125);
            btnClose.ForeColor = Color.White;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnClose.Cursor = Cursors.Hand;
            btnClose.Click += (s, e) => this.Close();
            this.Controls.Add(btnClose);
        }

        private void LoadMyOrders()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                int userId = SessionManager.GetCurrentUserId();
                var response = orderBLL.GetOrdersByCustomerId(userId);

                if (response.Success)
                {
                    DisplayOrders(response.Data);
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

        private void DisplayOrders(List<OrderDTO> orders)
        {
            dgvOrders.Rows.Clear();

            foreach (var order in orders)
            {
                int rowIndex = dgvOrders.Rows.Add(
                    order.OrderID,
                    order.OrderDate.ToString("dd/MM/yyyy HH:mm"),
                    FormatCurrency(order.TotalAmount),
                    GetStatusText(order.Status)
                );

                DataGridViewRow row = dgvOrders.Rows[rowIndex];
                ApplyStatusColor(row, order.Status);
            }

            lblTotal.Text = $"Tổng: {orders.Count} đơn hàng";
        }

        private void BtnViewDetails_Click(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn đơn hàng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int orderId = Convert.ToInt32(dgvOrders.SelectedRows[0].Cells[0].Value);

            // Show order details
            ShowOrderDetails(orderId);
        }

        private void ShowOrderDetails(int orderId)
        {
            try
            {
                var orderResponse = orderBLL.GetOrderById(orderId);
                var itemsResponse = orderBLL.GetOrderItems(orderId);
                var paymentResponse = orderBLL.GetPaymentByOrderId(orderId);

                if (!orderResponse.Success || !itemsResponse.Success)
                {
                    MessageBox.Show("Không thể tải chi tiết đơn hàng!", "Lỗi");
                    return;
                }

                OrderDTO order = orderResponse.Data;
                List<OrderItemDTO> items = itemsResponse.Data;
                PaymentDTO payment = paymentResponse.Success ? paymentResponse.Data : null;

                // Create details form
                Form detailsForm = new Form();
                detailsForm.Text = $"Chi tiết đơn hàng #{orderId}";
                detailsForm.Size = new Size(600, 500);
                detailsForm.StartPosition = FormStartPosition.CenterParent;
                detailsForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                detailsForm.MaximizeBox = false;
                detailsForm.MinimizeBox = false;

                // Content
                TextBox txtDetails = new TextBox();
                txtDetails.Multiline = true;
                txtDetails.ReadOnly = true;
                txtDetails.ScrollBars = ScrollBars.Vertical;
                txtDetails.Font = new Font("Segoe UI", 10F);
                txtDetails.Location = new Point(20, 20);
                txtDetails.Size = new Size(540, 390);

                string details = $"ĐƠN HÀNG #{order.OrderID}\n";
                details += "═══════════════════════════════════\n\n";
                details += $"Ngày đặt: {order.OrderDate:dd/MM/yyyy HH:mm}\n";
                details += $"Trạng thái: {GetStatusText(order.Status)}\n\n";
                details += "SẢN PHẨM:\n";
                details += "───────────────────────────────────\n";

                foreach (var item in items)
                {
                    details += $"• {item.CarName}\n";
                    details += $"  Số lượng: {item.Quantity}\n";
                    details += $"  Đơn giá: {FormatCurrency(item.UnitPrice)}\n";
                    details += $"  Thành tiền: {FormatCurrency(item.TotalPrice)}\n\n";
                }

                details += "═══════════════════════════════════\n";
                details += $"TỔNG CỘNG: {FormatCurrency(order.TotalAmount)}\n\n";

                if (payment != null)
                {
                    details += "THANH TOÁN:\n";
                    details += $"Phương thức: {payment.PaymentMethod}\n";
                    details += $"Trạng thái: {payment.PaymentStatus}\n";
                }

                txtDetails.Text = details;
                detailsForm.Controls.Add(txtDetails);

                // Close button
                Button btnCloseDetails = new Button();
                btnCloseDetails.Text = "Đóng";
                btnCloseDetails.Location = new Point(460, 420);
                btnCloseDetails.Size = new Size(100, 35);
                btnCloseDetails.Click += (s, ev) => detailsForm.Close();
                detailsForm.Controls.Add(btnCloseDetails);

                detailsForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi");
            }
        }

        private void BtnCancelOrder_Click(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn đơn hàng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int orderId = Convert.ToInt32(dgvOrders.SelectedRows[0].Cells[0].Value);
            string status = dgvOrders.SelectedRows[0].Cells[3].Value.ToString();

            if (status == "Đã hủy")
            {
                MessageBox.Show("Đơn hàng đã bị hủy trước đó!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (status == "Hoàn thành")
            {
                MessageBox.Show("Không thể hủy đơn hàng đã hoàn thành!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc muốn HỦY đơn hàng #{orderId}?\n\nHành động này không thể hoàn tác!",
                "Xác nhận hủy đơn",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    var response = orderBLL.CancelOrder(orderId);

                    if (response.Success)
                    {
                        MessageBox.Show(response.Message, "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadMyOrders();
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
            }
        }

        private void ApplyStatusColor(DataGridViewRow row, string status)
        {
            switch (status)
            {
                case "Pending":
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 243, 205);
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(133, 100, 4);
                    break;
                case "Processing":
                    row.DefaultCellStyle.BackColor = Color.FromArgb(204, 229, 255);
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(1, 67, 97);
                    break;
                case "Shipped":
                    row.DefaultCellStyle.BackColor = Color.FromArgb(209, 231, 221);
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(21, 87, 36);
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
    }
}