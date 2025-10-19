using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BLL_CarSales;
using DTO_Carsales;
using UTIL_CarSales;

namespace GUI_CarSales
{
    public partial class EmployeeMainForm : Form
    {
        private OrderBLL orderBLL;

        public EmployeeMainForm()
        {
            InitializeComponent();
            orderBLL = new OrderBLL();

            this.Load += EmployeeMainForm_Load;
        }

        private void EmployeeMainForm_Load(object sender, EventArgs e)
        {
            // Kiểm tra quyền employee
            if (!SessionManager.IsEmployee())
            {
                MessageBox.Show("Bạn không có quyền truy cập!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            // Hiển thị thông tin employee
            var currentUser = SessionManager.GetCurrentUser();
            lblEmployeeName.Text = currentUser.FullName;

            // Setup
            SetupDataGridView();
            LoadStatuses();
            LoadPendingOrders(); // Load đơn hàng chờ xử lý

            // Gắn events
            btnSearch.Click += btnSearch_Click;
            btnRefresh.Click += btnRefresh_Click;
            btnViewDetails.Click += btnViewDetails_Click;
            btnUpdateStatus.Click += btnUpdateStatus_Click;
            btnPrintInvoice.Click += btnPrintInvoice_Click;
            btnLogout.Click += btnLogout_Click;
            chkDateFilter.CheckedChanged += chkDateFilter_CheckedChanged;

            // Drag form
            pnlTop.MouseDown += PnlTop_MouseDown;
            pnlTop.MouseMove += PnlTop_MouseMove;
            pnlTop.MouseUp += PnlTop_MouseUp;
        }

        // ==================== SETUP ====================
        private void SetupDataGridView()
        {
            dgvOrders.Columns.Clear();
            dgvOrders.AutoGenerateColumns = false;

            dgvOrders.Columns.Add("OrderID", "Mã ĐH");
            dgvOrders.Columns.Add("OrderDate", "Ngày đặt");
            dgvOrders.Columns.Add("CustomerName", "Khách hàng");
            dgvOrders.Columns.Add("TotalAmount", "Tổng tiền");
            dgvOrders.Columns.Add("Status", "Trạng thái");

            dgvOrders.Columns[0].Width = 80;
            dgvOrders.Columns[1].Width = 150;
            dgvOrders.Columns[2].Width = 200;
            dgvOrders.Columns[3].Width = 150;
            dgvOrders.Columns[4].Width = 130;

            dgvOrders.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvOrders.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void LoadStatuses()
        {
            cboStatus.Items.Clear();
            cboStatus.Items.Add("-- Tất cả trạng thái --");
            cboStatus.Items.Add("Pending");
            cboStatus.Items.Add("Processing");
            cboStatus.Items.Add("Shipped");
            cboStatus.Items.Add("Completed");
            cboStatus.SelectedIndex = 0;
        }

        // ==================== LOAD PENDING ORDERS ====================
        private void LoadPendingOrders()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                var response = orderBLL.SearchOrders("", "Pending", null, null);

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

            int totalOrders = orders.Count;
            int pendingOrders = 0;
            int processingOrders = 0;
            decimal totalAmount = 0;

            foreach (var order in orders)
            {
                int rowIndex = dgvOrders.Rows.Add(
                    order.OrderID,
                    order.OrderDate.ToString("dd/MM/yyyy HH:mm"),
                    order.CustomerName,
                    FormatCurrency(order.TotalAmount),
                    GetStatusText(order.Status)
                );

                // Tô màu theo trạng thái
                DataGridViewRow row = dgvOrders.Rows[rowIndex];
                ApplyStatusColor(row, order.Status);

                // Thống kê
                if (order.Status == "Pending")
                    pendingOrders++;
                else if (order.Status == "Processing")
                    processingOrders++;

                if (order.Status != "Cancelled")
                    totalAmount += order.TotalAmount;
            }

            // Cập nhật labels
            lblTotalOrders.Text = $"Tổng: {totalOrders} đơn";
            lblPendingOrders.Text = $"| Chờ xử lý: {pendingOrders}";
            lblProcessingOrders.Text = $"| Đang xử lý: {processingOrders}";
        }

        // ==================== TÌM KIẾM ====================
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                string keyword = txtSearch.Text.Trim();
                string status = cboStatus.SelectedIndex > 0 ? cboStatus.SelectedItem.ToString() : null;
                DateTime? fromDate = chkDateFilter.Checked ? (DateTime?)dtpFromDate.Value.Date : null;
                DateTime? toDate = chkDateFilter.Checked ? (DateTime?)dtpToDate.Value.Date : null;

                var response = orderBLL.SearchOrders(keyword, status, fromDate, toDate);

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
            cboStatus.SelectedIndex = 0;
            chkDateFilter.Checked = false;
            LoadPendingOrders();
        }

        // ==================== XEM CHI TIẾT ====================
        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn đơn hàng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int orderId = Convert.ToInt32(dgvOrders.SelectedRows[0].Cells[0].Value);

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

                ShowOrderDetails(orderResponse.Data, itemsResponse.Data,
                    paymentResponse.Success ? paymentResponse.Data : null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi");
            }
        }

        private void ShowOrderDetails(OrderDTO order, List<OrderItemDTO> items, PaymentDTO payment)
        {
            Form detailsForm = new Form();
            detailsForm.Text = $"Chi tiết đơn hàng #{order.OrderID}";
            detailsForm.Size = new Size(600, 500);
            detailsForm.StartPosition = FormStartPosition.CenterParent;
            detailsForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            detailsForm.MaximizeBox = false;
            detailsForm.MinimizeBox = false;

            TextBox txtDetails = new TextBox();
            txtDetails.Multiline = true;
            txtDetails.ReadOnly = true;
            txtDetails.ScrollBars = ScrollBars.Vertical;
            txtDetails.Font = new Font("Segoe UI", 10F);
            txtDetails.Location = new Point(20, 20);
            txtDetails.Size = new Size(540, 390);

            string details = $"ĐƠN HÀNG #{order.OrderID}\n";
            details += "═══════════════════════════════════\n\n";
            details += $"Khách hàng: {order.CustomerName}\n";
            details += $"Ngày đặt: {order.OrderDate:dd/MM/yyyy HH:mm}\n";
            details += $"Trạng thái: {GetStatusText(order.Status)}\n\n";
            details += "SẢN PHẨM:\n";
            details += "───────────────────────────────────\n";

            foreach (var item in items)
            {
                details += $"• {item.CarName}\n";
                details += $"  SL: {item.Quantity} x {FormatCurrency(item.UnitPrice)}\n";
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

            Button btnClose = new Button();
            btnClose.Text = "Đóng";
            btnClose.Location = new Point(460, 420);
            btnClose.Size = new Size(100, 35);
            btnClose.Click += (s, ev) => detailsForm.Close();
            detailsForm.Controls.Add(btnClose);

            detailsForm.ShowDialog();
        }

        // ==================== CẬP NHẬT TRẠNG THÁI ====================
        private void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn đơn hàng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int orderId = Convert.ToInt32(dgvOrders.SelectedRows[0].Cells[0].Value);
            string currentStatus = dgvOrders.SelectedRows[0].Cells[4].Value.ToString();

            using (Form statusForm = new Form())
            {
                statusForm.Text = "Cập nhật trạng thái";
                statusForm.Size = new Size(350, 200);
                statusForm.StartPosition = FormStartPosition.CenterParent;
                statusForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                statusForm.MaximizeBox = false;
                statusForm.MinimizeBox = false;

                Label lblInfo = new Label();
                lblInfo.Text = $"Đơn hàng #{orderId}\nTrạng thái hiện tại: {currentStatus}";
                lblInfo.Location = new Point(20, 20);
                lblInfo.AutoSize = true;
                statusForm.Controls.Add(lblInfo);

                Label lblNew = new Label();
                lblNew.Text = "Trạng thái mới:";
                lblNew.Location = new Point(20, 70);
                lblNew.AutoSize = true;
                statusForm.Controls.Add(lblNew);

                ComboBox cboNewStatus = new ComboBox();
                cboNewStatus.Location = new Point(20, 95);
                cboNewStatus.Size = new Size(300, 30);
                cboNewStatus.DropDownStyle = ComboBoxStyle.DropDownList;
                cboNewStatus.Items.AddRange(new object[] {
                    "Pending", "Processing", "Shipped", "Completed"
                });
                cboNewStatus.SelectedIndex = 0;
                statusForm.Controls.Add(cboNewStatus);

                Button btnOK = new Button();
                btnOK.Text = "Cập nhật";
                btnOK.Location = new Point(145, 130);
                btnOK.DialogResult = DialogResult.OK;
                statusForm.Controls.Add(btnOK);

                Button btnCancel = new Button();
                btnCancel.Text = "Hủy";
                btnCancel.Location = new Point(230, 130);
                btnCancel.DialogResult = DialogResult.Cancel;
                statusForm.Controls.Add(btnCancel);

                statusForm.AcceptButton = btnOK;
                statusForm.CancelButton = btnCancel;

                if (statusForm.ShowDialog() == DialogResult.OK)
                {
                    string newStatus = cboNewStatus.SelectedItem.ToString();
                    var response = orderBLL.UpdateOrderStatus(orderId, newStatus);

                    if (response.Success)
                    {
                        MessageBox.Show(response.Message, "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadPendingOrders();
                    }
                    else
                    {
                        MessageBox.Show(response.Message, "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // ==================== IN HÓA ĐƠN ====================
        private void btnPrintInvoice_Click(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn đơn hàng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int orderId = Convert.ToInt32(dgvOrders.SelectedRows[0].Cells[0].Value);

            try
            {
                var orderResponse = orderBLL.GetOrderById(orderId);
                var itemsResponse = orderBLL.GetOrderItems(orderId);
                var paymentResponse = orderBLL.GetPaymentByOrderId(orderId);

                if (!orderResponse.Success || !itemsResponse.Success)
                {
                    MessageBox.Show("Không thể tải thông tin đơn hàng!", "Lỗi");
                    return;
                }

                string invoice = GenerateInvoice(orderResponse.Data, itemsResponse.Data,
                    paymentResponse.Success ? paymentResponse.Data : null);

                ShowInvoicePreview(invoice, orderId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi in hóa đơn: " + ex.Message, "Lỗi");
            }
        }

        private string GenerateInvoice(OrderDTO order, List<OrderItemDTO> items, PaymentDTO payment)
        {
            System.Text.StringBuilder invoice = new System.Text.StringBuilder();

            invoice.AppendLine("════════════════════════════════════════════════");
            invoice.AppendLine("              HÓA ĐƠN BÁN XE Ô TÔ");
            invoice.AppendLine("════════════════════════════════════════════════");
            invoice.AppendLine();
            invoice.AppendLine($"Mã đơn hàng: #{order.OrderID}");
            invoice.AppendLine($"Ngày: {order.OrderDate:dd/MM/yyyy HH:mm}");
            invoice.AppendLine($"Khách hàng: {order.CustomerName}");
            invoice.AppendLine();
            invoice.AppendLine("────────────────────────────────────────────────");
            invoice.AppendLine("                  SẢN PHẨM");
            invoice.AppendLine("────────────────────────────────────────────────");

            foreach (var item in items)
            {
                invoice.AppendLine($"\n{item.CarName}");
                invoice.AppendLine($"SL: {item.Quantity} x {FormatCurrency(item.UnitPrice)}");
                invoice.AppendLine($"Thành tiền: {FormatCurrency(item.TotalPrice)}");
            }

            invoice.AppendLine("\n────────────────────────────────────────────────");
            invoice.AppendLine($"TỔNG CỘNG: {FormatCurrency(order.TotalAmount)}");
            invoice.AppendLine("────────────────────────────────────────────────");
            invoice.AppendLine("\n        CẢM ƠN QUÝ KHÁCH!");

            return invoice.ToString();
        }

        private void ShowInvoicePreview(string invoice, int orderId)
        {
            Form previewForm = new Form();
            previewForm.Text = $"Hóa đơn #{orderId}";
            previewForm.Size = new Size(600, 600);
            previewForm.StartPosition = FormStartPosition.CenterParent;

            TextBox txtInvoice = new TextBox();
            txtInvoice.Multiline = true;
            txtInvoice.ReadOnly = true;
            txtInvoice.ScrollBars = ScrollBars.Vertical;
            txtInvoice.Font = new Font("Consolas", 10F);
            txtInvoice.Location = new Point(20, 20);
            txtInvoice.Size = new Size(540, 480);
            txtInvoice.Text = invoice;
            previewForm.Controls.Add(txtInvoice);

            Button btnClose = new Button();
            btnClose.Text = "Đóng";
            btnClose.Location = new Point(460, 520);
            btnClose.Size = new Size(100, 35);
            btnClose.Click += (s, e) => previewForm.Close();
            previewForm.Controls.Add(btnClose);

            previewForm.ShowDialog();
        }

        // ==================== ĐĂNG XUẤT ====================
        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn đăng xuất?",
                "Xác nhận",
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
        private void chkDateFilter_CheckedChanged(object sender, EventArgs e)
        {
            dtpFromDate.Enabled = chkDateFilter.Checked;
            dtpToDate.Enabled = chkDateFilter.Checked;
        }

        private void ApplyStatusColor(DataGridViewRow row, string status)
        {
            switch (status)
            {
                case "Pending":
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 243, 205);
                    break;
                case "Processing":
                    row.DefaultCellStyle.BackColor = Color.FromArgb(204, 229, 255);
                    break;
                case "Shipped":
                    row.DefaultCellStyle.BackColor = Color.FromArgb(209, 231, 221);
                    break;
                case "Completed":
                    row.DefaultCellStyle.BackColor = Color.FromArgb(212, 237, 218);
                    break;
            }
        }

        private string FormatCurrency(decimal amount)
        {
            if (amount >= 1000000000)
                return (amount / 1000000000).ToString("0.##") + " tỷ";
            else if (amount >= 1000000)
                return (amount / 1000000).ToString("0.##") + " tr";
            else
                return amount.ToString("#,##0") + "đ";
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
    }
}