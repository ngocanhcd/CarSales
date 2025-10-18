using System;
using System.Drawing;
using System.Windows.Forms;
using BLL_CarSales;
using DTO_Carsales;

namespace GUI_CarSales
{
    public partial class OrderManagementForm : Form
    {
        private OrderBLL orderBLL;

        public OrderManagementForm()
        {
            InitializeComponent();
            orderBLL = new OrderBLL();

            this.Load += OrderManagementForm_Load;
        }

        private void OrderManagementForm_Load(object sender, EventArgs e)
        {
            // Setup DataGridView
            SetupDataGridView();

            // Load statuses
            LoadStatuses();

            // Load data
            LoadAllOrders();

            // Gắn events
            btnSearch.Click += btnSearch_Click;
            btnRefresh.Click += btnRefresh_Click;
            btnViewDetails.Click += btnViewDetails_Click;
            btnPrintInvoice.Click += btnPrintInvoice_Click;
            btnUpdateStatus.Click += btnUpdateStatus_Click;
            btnCancelOrder.Click += btnCancelOrder_Click;
            btnDeleteOrder.Click += btnDeleteOrder_Click;
            btnClose.Click += btnClose_Click;
            chkDateFilter.CheckedChanged += chkDateFilter_CheckedChanged;
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
            dgvOrders.Columns[4].Width = 120;

            dgvOrders.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvOrders.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void LoadStatuses()
        {
            cboStatus.SelectedIndex = 0; // "Tất cả trạng thái"
        }

        // ==================== LOAD DỮ LIỆU ====================
        private void LoadAllOrders()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                var response = orderBLL.GetAllOrders();

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

        private void DisplayOrders(System.Collections.Generic.List<OrderDTO> orders)
        {
            dgvOrders.Rows.Clear();

            int totalOrders = orders.Count;
            int pendingOrders = 0;
            int completedOrders = 0;
            decimal totalRevenue = 0;

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
                if (order.Status == "Pending" || order.Status == "Processing")
                    pendingOrders++;

                if (order.Status == "Completed")
                    completedOrders++;

                if (order.Status != "Cancelled")
                    totalRevenue += order.TotalAmount;
            }

            // Cập nhật labels
            lblTotalOrders.Text = $"Tổng đơn hàng: {totalOrders}";
            lblPendingOrders.Text = $"| Chờ xử lý: {pendingOrders}";
            lblCompletedOrders.Text = $"| Hoàn thành: {completedOrders}";
            lblTotalRevenue.Text = $"💰 Tổng doanh thu: {FormatCurrency(totalRevenue)}";
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

        // ==================== LÀM MỚI ====================
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cboStatus.SelectedIndex = 0;
            chkDateFilter.Checked = false;
            LoadAllOrders();
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
                // Lấy thông tin đơn hàng
                var orderResponse = orderBLL.GetOrderById(orderId);
                var itemsResponse = orderBLL.GetOrderItems(orderId);
                var paymentResponse = orderBLL.GetPaymentByOrderId(orderId);

                if (!orderResponse.Success || !itemsResponse.Success)
                {
                    MessageBox.Show("Không thể tải thông tin đơn hàng!", "Lỗi");
                    return;
                }

                // Tạo nội dung hóa đơn
                string invoice = GenerateInvoiceContent(orderResponse.Data, itemsResponse.Data,
                    paymentResponse.Success ? paymentResponse.Data : null);

                // Hiển thị preview và in
                InvoicePreviewForm previewForm = new InvoicePreviewForm(invoice, orderId);
                previewForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi in hóa đơn: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GenerateInvoiceContent(OrderDTO order,
            System.Collections.Generic.List<OrderItemDTO> items, PaymentDTO payment)
        {
            System.Text.StringBuilder invoice = new System.Text.StringBuilder();

            invoice.AppendLine("════════════════════════════════════════════════");
            invoice.AppendLine("              HÓA ĐƠN BÁN XE Ô TÔ");
            invoice.AppendLine("            CAR SALES MANAGEMENT SYSTEM");
            invoice.AppendLine("════════════════════════════════════════════════");
            invoice.AppendLine();
            invoice.AppendLine($"Mã đơn hàng: #{order.OrderID}");
            invoice.AppendLine($"Ngày đặt: {order.OrderDate:dd/MM/yyyy HH:mm}");
            invoice.AppendLine($"Khách hàng: {order.CustomerName}");
            invoice.AppendLine($"Trạng thái: {GetStatusText(order.Status)}");
            invoice.AppendLine();
            invoice.AppendLine("────────────────────────────────────────────────");
            invoice.AppendLine("                  SẢN PHẨM");
            invoice.AppendLine("────────────────────────────────────────────────");
            invoice.AppendLine();

            decimal subtotal = 0;
            decimal totalDiscount = 0;

            foreach (var item in items)
            {
                invoice.AppendLine($"Tên xe: {item.CarName}");
                invoice.AppendLine($"Số lượng: {item.Quantity}");
                invoice.AppendLine($"Đơn giá: {item.UnitPrice:#,##0}đ");

                if (item.Discount > 0)
                {
                    invoice.AppendLine($"Giảm giá: -{item.Discount:#,##0}đ");
                    totalDiscount += item.Discount;
                }

                invoice.AppendLine($"Thành tiền: {item.TotalPrice:#,##0}đ");
                invoice.AppendLine();

                subtotal += item.Quantity * item.UnitPrice;
            }

            invoice.AppendLine("────────────────────────────────────────────────");
            invoice.AppendLine($"Tạm tính:        {subtotal:#,##0}đ");

            if (totalDiscount > 0)
                invoice.AppendLine($"Giảm giá:       -{totalDiscount:#,##0}đ");

            invoice.AppendLine($"TỔNG CỘNG:       {order.TotalAmount:#,##0}đ");
            invoice.AppendLine("────────────────────────────────────────────────");
            invoice.AppendLine();

            if (payment != null)
            {
                invoice.AppendLine("            THÔNG TIN THANH TOÁN");
                invoice.AppendLine("────────────────────────────────────────────────");
                invoice.AppendLine($"Phương thức: {payment.PaymentMethod}");
                invoice.AppendLine($"Trạng thái: {payment.PaymentStatus}");
                invoice.AppendLine($"Ngày thanh toán: {payment.PaymentDate:dd/MM/yyyy HH:mm}");
                invoice.AppendLine();
            }

            invoice.AppendLine("════════════════════════════════════════════════");
            invoice.AppendLine("         CẢM ƠN QUÝ KHÁCH ĐÃ MUA HÀNG!");
            invoice.AppendLine("         Hotline: 1900 xxxx");
            invoice.AppendLine("════════════════════════════════════════════════");

            return invoice.ToString();
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

            // Mở form chi tiết (sẽ tạo sau)
            OrderDetailsForm detailsForm = new OrderDetailsForm(orderId);
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

            // Hiển thị form chọn trạng thái mới
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

                Button btnCancelDialog = new Button();
                btnCancelDialog.Text = "Hủy";
                btnCancelDialog.Location = new Point(230, 130);
                btnCancelDialog.DialogResult = DialogResult.Cancel;
                statusForm.Controls.Add(btnCancelDialog);

                statusForm.AcceptButton = btnOK;
                statusForm.CancelButton = btnCancelDialog;

                if (statusForm.ShowDialog() == DialogResult.OK)
                {
                    string newStatus = cboNewStatus.SelectedItem.ToString();

                    var response = orderBLL.UpdateOrderStatus(orderId, newStatus);

                    if (response.Success)
                    {
                        MessageBox.Show(response.Message, "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadAllOrders();
                    }
                    else
                    {
                        MessageBox.Show(response.Message, "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // ==================== HỦY ĐƠN HÀNG ====================
        private void btnCancelOrder_Click(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn đơn hàng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int orderId = Convert.ToInt32(dgvOrders.SelectedRows[0].Cells[0].Value);
            string customerName = dgvOrders.SelectedRows[0].Cells[2].Value.ToString();

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc muốn HỦY đơn hàng #{orderId} của '{customerName}'?\n\n" +
                "Số lượng xe sẽ được hoàn trả về kho.",
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
                        LoadAllOrders();
                    }
                    else
                    {
                        MessageBox.Show(response.Message, "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi hủy đơn: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ==================== XÓA ĐƠN HÀNG ====================
        private void btnDeleteOrder_Click(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn đơn hàng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int orderId = Convert.ToInt32(dgvOrders.SelectedRows[0].Cells[0].Value);
            string customerName = dgvOrders.SelectedRows[0].Cells[2].Value.ToString();

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc muốn XÓA VĨNH VIỄN đơn hàng #{orderId} của '{customerName}'?\n\n" +
                "⚠️ Thao tác này không thể hoàn tác!\n" +
                "Chỉ có thể xóa đơn hàng đã bị hủy.",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    var response = orderBLL.DeleteOrder(orderId);

                    if (response.Success)
                    {
                        MessageBox.Show(response.Message, "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadAllOrders();
                    }
                    else
                    {
                        MessageBox.Show(response.Message, "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa đơn: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ==================== ĐÓNG FORM ====================
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // ==================== ENABLE/DISABLE DATE FILTER ====================
        private void chkDateFilter_CheckedChanged(object sender, EventArgs e)
        {
            dtpFromDate.Enabled = chkDateFilter.Checked;
            dtpToDate.Enabled = chkDateFilter.Checked;
        }

        // ==================== HELPER METHODS ====================
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
            if (amount >= 1000000000) // Tỷ
            {
                return (amount / 1000000000).ToString("0.##") + " tỷ";
            }
            else if (amount >= 1000000) // Triệu
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

    // ==================== ORDER DETAILS FORM (PLACEHOLDER) ====================
    public class OrderDetailsForm : Form
    {
        private int orderId;
        private OrderBLL orderBLL;

        public OrderDetailsForm(int orderId)
        {
            this.orderId = orderId;
            this.orderBLL = new OrderBLL();

            InitializeComponents();
            LoadOrderDetails();
        }

        private void InitializeComponents()
        {
            this.Text = $"Chi tiết đơn hàng #{orderId}";
            this.Size = new Size(700, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Panel top
            Guna.UI2.WinForms.Guna2Panel pnlTop = new Guna.UI2.WinForms.Guna2Panel();
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Height = 60;
            pnlTop.FillColor = Color.FromArgb(94, 148, 255);
            this.Controls.Add(pnlTop);

            Label lblTitle = new Label();
            lblTitle.Text = $"📋 CHI TIẾT ĐƠN HÀNG #{orderId}";
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(20, 17);
            pnlTop.Controls.Add(lblTitle);

            // Close button
            Button btnClose = new Button();
            btnClose.Text = "Đóng";
            btnClose.Location = new Point(590, 415);
            btnClose.Size = new Size(90, 35);
            btnClose.Click += (s, e) => this.Close();
            this.Controls.Add(btnClose);
        }

        private void LoadOrderDetails()
        {
            try
            {
                // Lấy thông tin đơn hàng
                var orderResponse = orderBLL.GetOrderById(orderId);
                var itemsResponse = orderBLL.GetOrderItems(orderId);
                var paymentResponse = orderBLL.GetPaymentByOrderId(orderId);

                if (!orderResponse.Success || !itemsResponse.Success)
                {
                    MessageBox.Show("Không thể tải chi tiết đơn hàng!", "Lỗi");
                    this.Close();
                    return;
                }

                // Hiển thị thông tin
                int yPos = 80;

                // Thông tin khách hàng
                AddLabel($"👤 Khách hàng: {orderResponse.Data.CustomerName}", 20, yPos);
                yPos += 30;
                AddLabel($"📅 Ngày đặt: {orderResponse.Data.OrderDate:dd/MM/yyyy HH:mm}", 20, yPos);
                yPos += 30;
                AddLabel($"📊 Trạng thái: {GetStatusText(orderResponse.Data.Status)}", 20, yPos);
                yPos += 30;
                AddLabel($"💰 Tổng tiền: {FormatCurrency(orderResponse.Data.TotalAmount)}", 20, yPos);
                yPos += 40;

                // Danh sách sản phẩm
                AddLabel("📦 SẢN PHẨM:", 20, yPos, true);
                yPos += 30;

                foreach (var item in itemsResponse.Data)
                {
                    AddLabel($"• {item.CarName} - SL: {item.Quantity} - Giá: {FormatCurrency(item.UnitPrice)}", 40, yPos);
                    yPos += 25;
                }

                yPos += 20;

                // Thông tin thanh toán
                if (paymentResponse.Success && paymentResponse.Data != null)
                {
                    AddLabel("💳 THANH TOÁN:", 20, yPos, true);
                    yPos += 30;
                    AddLabel($"Phương thức: {paymentResponse.Data.PaymentMethod}", 40, yPos);
                    yPos += 25;
                    AddLabel($"Trạng thái: {paymentResponse.Data.PaymentStatus}", 40, yPos);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi");
            }
        }

        private void AddLabel(string text, int x, int y, bool bold = false)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.Location = new Point(x, y);
            lbl.AutoSize = true;
            lbl.Font = new Font("Segoe UI", bold ? 11F : 10F, bold ? FontStyle.Bold : FontStyle.Regular);
            this.Controls.Add(lbl);
        }

        private string FormatCurrency(decimal amount)
        {
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
    }

    // ==================== INVOICE PREVIEW FORM ====================
    public class InvoicePreviewForm : Form
    {
        private TextBox txtInvoice;
        private int orderId;

        public InvoicePreviewForm(string invoiceContent, int orderId)
        {
            this.orderId = orderId;
            InitializeComponents(invoiceContent);
        }

        private void InitializeComponents(string content)
        {
            this.Text = $"Xem trước hóa đơn #{orderId}";
            this.Size = new Size(650, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // TextBox hiển thị hóa đơn
            txtInvoice = new TextBox();
            txtInvoice.Multiline = true;
            txtInvoice.ReadOnly = true;
            txtInvoice.ScrollBars = ScrollBars.Vertical;
            txtInvoice.Font = new Font("Consolas", 10F);
            txtInvoice.Location = new Point(20, 20);
            txtInvoice.Size = new Size(590, 570);
            txtInvoice.Text = content;
            this.Controls.Add(txtInvoice);

            // Button In
            Button btnPrint = new Button();
            btnPrint.Text = "🖨️ In hóa đơn";
            btnPrint.Location = new Point(350, 610);
            btnPrint.Size = new Size(120, 40);
            btnPrint.Click += (s, e) => PrintInvoice();
            this.Controls.Add(btnPrint);

            // Button Lưu file
            Button btnSave = new Button();
            btnSave.Text = "💾 Lưu file";
            btnSave.Location = new Point(480, 610);
            btnSave.Size = new Size(120, 40);
            btnSave.Click += (s, e) => SaveInvoice();
            this.Controls.Add(btnSave);

            // Button Đóng
            Button btnClose = new Button();
            btnClose.Text = "Đóng";
            btnClose.Location = new Point(20, 610);
            btnClose.Size = new Size(100, 40);
            btnClose.Click += (s, e) => this.Close();
            this.Controls.Add(btnClose);
        }

        private void PrintInvoice()
        {
            try
            {
                System.Drawing.Printing.PrintDocument printDoc = new System.Drawing.Printing.PrintDocument();
                printDoc.PrintPage += PrintDoc_PrintPage;

                System.Windows.Forms.PrintDialog printDialog = new System.Windows.Forms.PrintDialog();
                printDialog.Document = printDoc;

                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    printDoc.Print();
                    MessageBox.Show("In hóa đơn thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi in: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font font = new Font("Consolas", 10);
            float yPos = 50;
            float leftMargin = 50;

            string[] lines = txtInvoice.Text.Split('\n');

            foreach (string line in lines)
            {
                e.Graphics.DrawString(line, font, Brushes.Black, leftMargin, yPos);
                yPos += font.GetHeight(e.Graphics) + 2;
            }
        }

        private void SaveInvoice()
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Text Files|*.txt|All Files|*.*";
                saveDialog.FileName = $"HoaDon_{orderId}_{DateTime.Now:yyyyMMdd_HHmmss}.txt";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    System.IO.File.WriteAllText(saveDialog.FileName, txtInvoice.Text);
                    MessageBox.Show("Lưu file thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu file: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}