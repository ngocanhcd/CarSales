namespace GUI_CarSales
{
    partial class AdminMainForm
    {
        private System.ComponentModel.IContainer components = null;

        // Sidebar
        private Guna.UI2.WinForms.Guna2Panel pnlSidebar;
        private Guna.UI2.WinForms.Guna2Button btnDashboard;
        private Guna.UI2.WinForms.Guna2Button btnUsers;
        private Guna.UI2.WinForms.Guna2Button btnCars;
        private Guna.UI2.WinForms.Guna2Button btnOrders;
        private Guna.UI2.WinForms.Guna2Button btnReports;
        private Guna.UI2.WinForms.Guna2Button btnSettings;
        private Guna.UI2.WinForms.Guna2Button btnLogout;
        private System.Windows.Forms.Label lblAdminName;
        private System.Windows.Forms.Label lblRole;
        private Guna.UI2.WinForms.Guna2CirclePictureBox picAdmin;

        // Top Panel
        private Guna.UI2.WinForms.Guna2Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private Guna.UI2.WinForms.Guna2Button btnMinimize;
        private Guna.UI2.WinForms.Guna2Button btnMaximize;
        private Guna.UI2.WinForms.Guna2Button btnClose;
        private Guna.UI2.WinForms.Guna2Button btnRefresh;

        // Main Content
        private Guna.UI2.WinForms.Guna2Panel pnlMain;

        // TableLayoutPanel cho responsive
        private System.Windows.Forms.TableLayoutPanel tableLayoutMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutCards;
        private System.Windows.Forms.TableLayoutPanel tableLayoutTables;

        // Stats Cards
        private Guna.UI2.WinForms.Guna2Panel cardTotalUsers;
        private Guna.UI2.WinForms.Guna2Panel cardTotalCars;
        private Guna.UI2.WinForms.Guna2Panel cardTotalOrders;
        private Guna.UI2.WinForms.Guna2Panel cardTotalRevenue;

        private System.Windows.Forms.Label lblTotalUsersValue;
        private System.Windows.Forms.Label lblTotalUsersLabel;
        private System.Windows.Forms.Label lblTotalCarsValue;
        private System.Windows.Forms.Label lblTotalCarsLabel;
        private System.Windows.Forms.Label lblTotalOrdersValue;
        private System.Windows.Forms.Label lblTotalOrdersLabel;
        private System.Windows.Forms.Label lblTotalRevenueValue;
        private System.Windows.Forms.Label lblTotalRevenueLabel;

        // Tables
        private Guna.UI2.WinForms.Guna2Panel pnlRecentOrders;
        private Guna.UI2.WinForms.Guna2Panel pnlTopCars;
        private System.Windows.Forms.Label lblRecentOrdersTitle;
        private System.Windows.Forms.Label lblTopCarsTitle;
        private Guna.UI2.WinForms.Guna2DataGridView dgvRecentOrders;
        private Guna.UI2.WinForms.Guna2DataGridView dgvTopCars;

        // Shadow
        private Guna.UI2.WinForms.Guna2ShadowForm shadowForm;
        private Guna.UI2.WinForms.Guna2Elipse elipseForm;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlSidebar = new Guna.UI2.WinForms.Guna2Panel();
            this.btnLogout = new Guna.UI2.WinForms.Guna2Button();
            this.btnSettings = new Guna.UI2.WinForms.Guna2Button();
            this.btnReports = new Guna.UI2.WinForms.Guna2Button();
            this.btnOrders = new Guna.UI2.WinForms.Guna2Button();
            this.btnCars = new Guna.UI2.WinForms.Guna2Button();
            this.btnUsers = new Guna.UI2.WinForms.Guna2Button();
            this.btnDashboard = new Guna.UI2.WinForms.Guna2Button();
            this.lblRole = new System.Windows.Forms.Label();
            this.lblAdminName = new System.Windows.Forms.Label();
            this.picAdmin = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.pnlTop = new Guna.UI2.WinForms.Guna2Panel();
            this.btnRefresh = new Guna.UI2.WinForms.Guna2Button();
            this.btnClose = new Guna.UI2.WinForms.Guna2Button();
            this.btnMaximize = new Guna.UI2.WinForms.Guna2Button();
            this.btnMinimize = new Guna.UI2.WinForms.Guna2Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlMain = new Guna.UI2.WinForms.Guna2Panel();
            this.tableLayoutMain = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutCards = new System.Windows.Forms.TableLayoutPanel();
            this.cardTotalUsers = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTotalUsersValue = new System.Windows.Forms.Label();
            this.lblTotalUsersLabel = new System.Windows.Forms.Label();
            this.cardTotalCars = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTotalCarsValue = new System.Windows.Forms.Label();
            this.lblTotalCarsLabel = new System.Windows.Forms.Label();
            this.cardTotalOrders = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTotalOrdersValue = new System.Windows.Forms.Label();
            this.lblTotalOrdersLabel = new System.Windows.Forms.Label();
            this.cardTotalRevenue = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTotalRevenueValue = new System.Windows.Forms.Label();
            this.lblTotalRevenueLabel = new System.Windows.Forms.Label();
            this.tableLayoutTables = new System.Windows.Forms.TableLayoutPanel();
            this.pnlRecentOrders = new Guna.UI2.WinForms.Guna2Panel();
            this.dgvRecentOrders = new Guna.UI2.WinForms.Guna2DataGridView();
            this.lblRecentOrdersTitle = new System.Windows.Forms.Label();
            this.pnlTopCars = new Guna.UI2.WinForms.Guna2Panel();
            this.dgvTopCars = new Guna.UI2.WinForms.Guna2DataGridView();
            this.lblTopCarsTitle = new System.Windows.Forms.Label();
            this.shadowForm = new Guna.UI2.WinForms.Guna2ShadowForm(this.components);
            this.elipseForm = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.pnlSidebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAdmin)).BeginInit();
            this.pnlTop.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.tableLayoutMain.SuspendLayout();
            this.tableLayoutCards.SuspendLayout();
            this.cardTotalUsers.SuspendLayout();
            this.cardTotalCars.SuspendLayout();
            this.cardTotalOrders.SuspendLayout();
            this.cardTotalRevenue.SuspendLayout();
            this.tableLayoutTables.SuspendLayout();
            this.pnlRecentOrders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentOrders)).BeginInit();
            this.pnlTopCars.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopCars)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.pnlSidebar.Controls.Add(this.btnLogout);
            this.pnlSidebar.Controls.Add(this.btnSettings);
            this.pnlSidebar.Controls.Add(this.btnReports);
            this.pnlSidebar.Controls.Add(this.btnOrders);
            this.pnlSidebar.Controls.Add(this.btnCars);
            this.pnlSidebar.Controls.Add(this.btnUsers);
            this.pnlSidebar.Controls.Add(this.btnDashboard);
            this.pnlSidebar.Controls.Add(this.lblRole);
            this.pnlSidebar.Controls.Add(this.lblAdminName);
            this.pnlSidebar.Controls.Add(this.picAdmin);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(210, 640);
            this.pnlSidebar.TabIndex = 0;
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnLogout.Animated = true;
            this.btnLogout.BorderRadius = 8;
            this.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogout.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(15, 593);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(180, 41);
            this.btnLogout.TabIndex = 9;
            this.btnLogout.Text = "🚪  Đăng xuất";
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Animated = true;
            this.btnSettings.BorderRadius = 8;
            this.btnSettings.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSettings.FillColor = System.Drawing.Color.Transparent;
            this.btnSettings.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSettings.ForeColor = System.Drawing.Color.White;
            this.btnSettings.Location = new System.Drawing.Point(15, 443);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(180, 41);
            this.btnSettings.TabIndex = 8;
            this.btnSettings.Text = "    ⚙️  Cài đặt";
            this.btnSettings.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // btnReports
            // 
            this.btnReports.Animated = true;
            this.btnReports.BorderRadius = 8;
            this.btnReports.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnReports.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReports.FillColor = System.Drawing.Color.Transparent;
            this.btnReports.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnReports.ForeColor = System.Drawing.Color.White;
            this.btnReports.Location = new System.Drawing.Point(15, 390);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(180, 41);
            this.btnReports.TabIndex = 7;
            this.btnReports.Text = "    📈  Báo cáo";
            this.btnReports.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // btnOrders
            // 
            this.btnOrders.Animated = true;
            this.btnOrders.BorderRadius = 8;
            this.btnOrders.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnOrders.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOrders.FillColor = System.Drawing.Color.Transparent;
            this.btnOrders.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnOrders.ForeColor = System.Drawing.Color.White;
            this.btnOrders.Location = new System.Drawing.Point(15, 337);
            this.btnOrders.Name = "btnOrders";
            this.btnOrders.Size = new System.Drawing.Size(180, 41);
            this.btnOrders.TabIndex = 6;
            this.btnOrders.Text = "    📦  Đơn hàng";
            this.btnOrders.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // btnCars
            // 
            this.btnCars.Animated = true;
            this.btnCars.BorderRadius = 8;
            this.btnCars.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnCars.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCars.FillColor = System.Drawing.Color.Transparent;
            this.btnCars.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCars.ForeColor = System.Drawing.Color.White;
            this.btnCars.Location = new System.Drawing.Point(15, 284);
            this.btnCars.Name = "btnCars";
            this.btnCars.Size = new System.Drawing.Size(180, 41);
            this.btnCars.TabIndex = 5;
            this.btnCars.Text = "    🚗  Quản lý xe";
            this.btnCars.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // btnUsers
            // 
            this.btnUsers.Animated = true;
            this.btnUsers.BorderRadius = 8;
            this.btnUsers.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnUsers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUsers.FillColor = System.Drawing.Color.Transparent;
            this.btnUsers.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnUsers.ForeColor = System.Drawing.Color.White;
            this.btnUsers.Location = new System.Drawing.Point(15, 232);
            this.btnUsers.Name = "btnUsers";
            this.btnUsers.Size = new System.Drawing.Size(180, 41);
            this.btnUsers.TabIndex = 4;
            this.btnUsers.Text = "    👥  Người dùng";
            this.btnUsers.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // btnDashboard
            // 
            this.btnDashboard.Animated = true;
            this.btnDashboard.BorderRadius = 8;
            this.btnDashboard.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnDashboard.Checked = true;
            this.btnDashboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDashboard.FillColor = System.Drawing.Color.Transparent;
            this.btnDashboard.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnDashboard.ForeColor = System.Drawing.Color.White;
            this.btnDashboard.Location = new System.Drawing.Point(15, 179);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(180, 41);
            this.btnDashboard.TabIndex = 3;
            this.btnDashboard.Text = "    📊  Dashboard";
            this.btnDashboard.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // lblRole
            // 
            this.lblRole.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.lblRole.Location = new System.Drawing.Point(0, 134);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(210, 16);
            this.lblRole.TabIndex = 2;
            this.lblRole.Text = "Administrator";
            this.lblRole.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAdminName
            // 
            this.lblAdminName.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblAdminName.ForeColor = System.Drawing.Color.White;
            this.lblAdminName.Location = new System.Drawing.Point(0, 114);
            this.lblAdminName.Name = "lblAdminName";
            this.lblAdminName.Size = new System.Drawing.Size(210, 24);
            this.lblAdminName.TabIndex = 1;
            this.lblAdminName.Text = "Admin Name";
            this.lblAdminName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picAdmin
            // 
            this.picAdmin.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.picAdmin.ImageRotate = 0F;
            this.picAdmin.Location = new System.Drawing.Point(68, 24);
            this.picAdmin.Name = "picAdmin";
            this.picAdmin.Size = new System.Drawing.Size(75, 81);
            this.picAdmin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picAdmin.TabIndex = 0;
            this.picAdmin.TabStop = false;
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.White;
            this.pnlTop.Controls.Add(this.btnRefresh);
            this.pnlTop.Controls.Add(this.btnClose);
            this.pnlTop.Controls.Add(this.btnMaximize);
            this.pnlTop.Controls.Add(this.btnMinimize);
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(210, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(890, 57);
            this.pnlTop.TabIndex = 1;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Animated = true;
            this.btnRefresh.BorderRadius = 8;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(677, 15);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(91, 27);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "🔄 Làm mới";
            this.btnRefresh.Click += new System.EventHandler(this.btnDashboard_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Animated = true;
            this.btnClose.BorderRadius = 5;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FillColor = System.Drawing.Color.Transparent;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.Gray;
            this.btnClose.Location = new System.Drawing.Point(851, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(37, 33);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "✕";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnMaximize
            // 
            this.btnMaximize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMaximize.Animated = true;
            this.btnMaximize.BorderRadius = 5;
            this.btnMaximize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMaximize.FillColor = System.Drawing.Color.Transparent;
            this.btnMaximize.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnMaximize.ForeColor = System.Drawing.Color.Gray;
            this.btnMaximize.Location = new System.Drawing.Point(811, 2);
            this.btnMaximize.Name = "btnMaximize";
            this.btnMaximize.Size = new System.Drawing.Size(37, 33);
            this.btnMaximize.TabIndex = 3;
            this.btnMaximize.Text = "□";
            this.btnMaximize.Click += new System.EventHandler(this.btnMaximize_Click);
            // 
            // btnMinimize
            // 
            this.btnMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimize.Animated = true;
            this.btnMinimize.BorderRadius = 5;
            this.btnMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMinimize.FillColor = System.Drawing.Color.Transparent;
            this.btnMinimize.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnMinimize.ForeColor = System.Drawing.Color.Gray;
            this.btnMinimize.Location = new System.Drawing.Point(771, 2);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(37, 33);
            this.btnMinimize.TabIndex = 2;
            this.btnMinimize.Text = "─";
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblTitle.Location = new System.Drawing.Point(15, 16);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(267, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📊 ADMIN DASHBOARD";
            // 
            // pnlMain
            // 
            this.pnlMain.AutoScroll = true;
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.pnlMain.Controls.Add(this.tableLayoutMain);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(210, 57);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(15, 16, 15, 16);
            this.pnlMain.Size = new System.Drawing.Size(890, 583);
            this.pnlMain.TabIndex = 2;
            // 
            // tableLayoutMain
            // 
            this.tableLayoutMain.ColumnCount = 1;
            this.tableLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutMain.Controls.Add(this.tableLayoutCards, 0, 0);
            this.tableLayoutMain.Controls.Add(this.tableLayoutTables, 0, 1);
            this.tableLayoutMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutMain.Location = new System.Drawing.Point(15, 16);
            this.tableLayoutMain.Name = "tableLayoutMain";
            this.tableLayoutMain.RowCount = 2;
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutMain.Size = new System.Drawing.Size(860, 551);
            this.tableLayoutMain.TabIndex = 0;
            // 
            // tableLayoutCards
            // 
            this.tableLayoutCards.ColumnCount = 4;
            this.tableLayoutCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutCards.Controls.Add(this.cardTotalUsers, 0, 0);
            this.tableLayoutCards.Controls.Add(this.cardTotalCars, 1, 0);
            this.tableLayoutCards.Controls.Add(this.cardTotalOrders, 2, 0);
            this.tableLayoutCards.Controls.Add(this.cardTotalRevenue, 3, 0);
            this.tableLayoutCards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutCards.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutCards.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutCards.Name = "tableLayoutCards";
            this.tableLayoutCards.RowCount = 1;
            this.tableLayoutCards.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutCards.Size = new System.Drawing.Size(860, 120);
            this.tableLayoutCards.TabIndex = 0;
            // 
            // cardTotalUsers
            // 
            this.cardTotalUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cardTotalUsers.BackColor = System.Drawing.Color.Transparent;
            this.cardTotalUsers.BorderRadius = 15;
            this.cardTotalUsers.Controls.Add(this.lblTotalUsersValue);
            this.cardTotalUsers.Controls.Add(this.lblTotalUsersLabel);
            this.cardTotalUsers.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cardTotalUsers.Location = new System.Drawing.Point(5, 5);
            this.cardTotalUsers.Margin = new System.Windows.Forms.Padding(5);
            this.cardTotalUsers.Name = "cardTotalUsers";
            this.cardTotalUsers.ShadowDecoration.BorderRadius = 15;
            this.cardTotalUsers.ShadowDecoration.Depth = 10;
            this.cardTotalUsers.ShadowDecoration.Enabled = true;
            this.cardTotalUsers.Size = new System.Drawing.Size(205, 110);
            this.cardTotalUsers.TabIndex = 0;
            // 
            // lblTotalUsersValue
            // 
            this.lblTotalUsersValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalUsersValue.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblTotalUsersValue.ForeColor = System.Drawing.Color.White;
            this.lblTotalUsersValue.Location = new System.Drawing.Point(0, 0);
            this.lblTotalUsersValue.Name = "lblTotalUsersValue";
            this.lblTotalUsersValue.Size = new System.Drawing.Size(205, 70);
            this.lblTotalUsersValue.TabIndex = 0;
            this.lblTotalUsersValue.Text = "0";
            this.lblTotalUsersValue.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblTotalUsersLabel
            // 
            this.lblTotalUsersLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTotalUsersLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTotalUsersLabel.ForeColor = System.Drawing.Color.White;
            this.lblTotalUsersLabel.Location = new System.Drawing.Point(0, 70);
            this.lblTotalUsersLabel.Name = "lblTotalUsersLabel";
            this.lblTotalUsersLabel.Size = new System.Drawing.Size(205, 40);
            this.lblTotalUsersLabel.TabIndex = 1;
            this.lblTotalUsersLabel.Text = "👥 Tổng người dùng";
            this.lblTotalUsersLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cardTotalCars
            // 
            this.cardTotalCars.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cardTotalCars.BackColor = System.Drawing.Color.Transparent;
            this.cardTotalCars.BorderRadius = 15;
            this.cardTotalCars.Controls.Add(this.lblTotalCarsValue);
            this.cardTotalCars.Controls.Add(this.lblTotalCarsLabel);
            this.cardTotalCars.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.cardTotalCars.Location = new System.Drawing.Point(220, 5);
            this.cardTotalCars.Margin = new System.Windows.Forms.Padding(5);
            this.cardTotalCars.Name = "cardTotalCars";
            this.cardTotalCars.ShadowDecoration.BorderRadius = 15;
            this.cardTotalCars.ShadowDecoration.Depth = 10;
            this.cardTotalCars.ShadowDecoration.Enabled = true;
            this.cardTotalCars.Size = new System.Drawing.Size(205, 110);
            this.cardTotalCars.TabIndex = 1;
            // 
            // lblTotalCarsValue
            // 
            this.lblTotalCarsValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalCarsValue.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblTotalCarsValue.ForeColor = System.Drawing.Color.White;
            this.lblTotalCarsValue.Location = new System.Drawing.Point(0, 0);
            this.lblTotalCarsValue.Name = "lblTotalCarsValue";
            this.lblTotalCarsValue.Size = new System.Drawing.Size(205, 70);
            this.lblTotalCarsValue.TabIndex = 0;
            this.lblTotalCarsValue.Text = "0";
            this.lblTotalCarsValue.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblTotalCarsLabel
            // 
            this.lblTotalCarsLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTotalCarsLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTotalCarsLabel.ForeColor = System.Drawing.Color.White;
            this.lblTotalCarsLabel.Location = new System.Drawing.Point(0, 70);
            this.lblTotalCarsLabel.Name = "lblTotalCarsLabel";
            this.lblTotalCarsLabel.Size = new System.Drawing.Size(205, 40);
            this.lblTotalCarsLabel.TabIndex = 1;
            this.lblTotalCarsLabel.Text = "🚗 Tổng số xe";
            this.lblTotalCarsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cardTotalOrders
            // 
            this.cardTotalOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cardTotalOrders.BackColor = System.Drawing.Color.Transparent;
            this.cardTotalOrders.BorderRadius = 15;
            this.cardTotalOrders.Controls.Add(this.lblTotalOrdersValue);
            this.cardTotalOrders.Controls.Add(this.lblTotalOrdersLabel);
            this.cardTotalOrders.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.cardTotalOrders.Location = new System.Drawing.Point(435, 5);
            this.cardTotalOrders.Margin = new System.Windows.Forms.Padding(5);
            this.cardTotalOrders.Name = "cardTotalOrders";
            this.cardTotalOrders.ShadowDecoration.BorderRadius = 15;
            this.cardTotalOrders.ShadowDecoration.Depth = 10;
            this.cardTotalOrders.ShadowDecoration.Enabled = true;
            this.cardTotalOrders.Size = new System.Drawing.Size(205, 110);
            this.cardTotalOrders.TabIndex = 2;
            // 
            // lblTotalOrdersValue
            // 
            this.lblTotalOrdersValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalOrdersValue.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblTotalOrdersValue.ForeColor = System.Drawing.Color.White;
            this.lblTotalOrdersValue.Location = new System.Drawing.Point(0, 0);
            this.lblTotalOrdersValue.Name = "lblTotalOrdersValue";
            this.lblTotalOrdersValue.Size = new System.Drawing.Size(205, 70);
            this.lblTotalOrdersValue.TabIndex = 0;
            this.lblTotalOrdersValue.Text = "0";
            this.lblTotalOrdersValue.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblTotalOrdersLabel
            // 
            this.lblTotalOrdersLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTotalOrdersLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTotalOrdersLabel.ForeColor = System.Drawing.Color.White;
            this.lblTotalOrdersLabel.Location = new System.Drawing.Point(0, 70);
            this.lblTotalOrdersLabel.Name = "lblTotalOrdersLabel";
            this.lblTotalOrdersLabel.Size = new System.Drawing.Size(205, 40);
            this.lblTotalOrdersLabel.TabIndex = 1;
            this.lblTotalOrdersLabel.Text = "📦 Tổng đơn hàng";
            this.lblTotalOrdersLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cardTotalRevenue
            // 
            this.cardTotalRevenue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cardTotalRevenue.BackColor = System.Drawing.Color.Transparent;
            this.cardTotalRevenue.BorderRadius = 15;
            this.cardTotalRevenue.Controls.Add(this.lblTotalRevenueValue);
            this.cardTotalRevenue.Controls.Add(this.lblTotalRevenueLabel);
            this.cardTotalRevenue.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.cardTotalRevenue.Location = new System.Drawing.Point(650, 5);
            this.cardTotalRevenue.Margin = new System.Windows.Forms.Padding(5);
            this.cardTotalRevenue.Name = "cardTotalRevenue";
            this.cardTotalRevenue.ShadowDecoration.BorderRadius = 15;
            this.cardTotalRevenue.ShadowDecoration.Depth = 10;
            this.cardTotalRevenue.ShadowDecoration.Enabled = true;
            this.cardTotalRevenue.Size = new System.Drawing.Size(205, 110);
            this.cardTotalRevenue.TabIndex = 3;
            // 
            // lblTotalRevenueValue
            // 
            this.lblTotalRevenueValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalRevenueValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTotalRevenueValue.ForeColor = System.Drawing.Color.White;
            this.lblTotalRevenueValue.Location = new System.Drawing.Point(0, 0);
            this.lblTotalRevenueValue.Name = "lblTotalRevenueValue";
            this.lblTotalRevenueValue.Size = new System.Drawing.Size(205, 70);
            this.lblTotalRevenueValue.TabIndex = 0;
            this.lblTotalRevenueValue.Text = "0đ";
            this.lblTotalRevenueValue.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblTotalRevenueLabel
            // 
            this.lblTotalRevenueLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTotalRevenueLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTotalRevenueLabel.ForeColor = System.Drawing.Color.White;
            this.lblTotalRevenueLabel.Location = new System.Drawing.Point(0, 70);
            this.lblTotalRevenueLabel.Name = "lblTotalRevenueLabel";
            this.lblTotalRevenueLabel.Size = new System.Drawing.Size(205, 40);
            this.lblTotalRevenueLabel.TabIndex = 1;
            this.lblTotalRevenueLabel.Text = "💰 Tổng doanh thu";
            this.lblTotalRevenueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutTables
            // 
            this.tableLayoutTables.ColumnCount = 2;
            this.tableLayoutTables.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutTables.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutTables.Controls.Add(this.pnlRecentOrders, 0, 0);
            this.tableLayoutTables.Controls.Add(this.pnlTopCars, 1, 0);
            this.tableLayoutTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutTables.Location = new System.Drawing.Point(0, 125);
            this.tableLayoutTables.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.tableLayoutTables.Name = "tableLayoutTables";
            this.tableLayoutTables.RowCount = 1;
            this.tableLayoutTables.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutTables.Size = new System.Drawing.Size(860, 426);
            this.tableLayoutTables.TabIndex = 1;
            // 
            // pnlRecentOrders
            // 
            this.pnlRecentOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlRecentOrders.BackColor = System.Drawing.Color.Transparent;
            this.pnlRecentOrders.BorderRadius = 15;
            this.pnlRecentOrders.Controls.Add(this.dgvRecentOrders);
            this.pnlRecentOrders.Controls.Add(this.lblRecentOrdersTitle);
            this.pnlRecentOrders.FillColor = System.Drawing.Color.White;
            this.pnlRecentOrders.Location = new System.Drawing.Point(5, 5);
            this.pnlRecentOrders.Margin = new System.Windows.Forms.Padding(5);
            this.pnlRecentOrders.Name = "pnlRecentOrders";
            this.pnlRecentOrders.ShadowDecoration.BorderRadius = 15;
            this.pnlRecentOrders.ShadowDecoration.Depth = 10;
            this.pnlRecentOrders.ShadowDecoration.Enabled = true;
            this.pnlRecentOrders.Size = new System.Drawing.Size(420, 416);
            this.pnlRecentOrders.TabIndex = 4;
            // 
            // dgvRecentOrders
            // 
            this.dgvRecentOrders.AllowUserToAddRows = false;
            this.dgvRecentOrders.AllowUserToDeleteRows = false;
            this.dgvRecentOrders.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvRecentOrders.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRecentOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRecentOrders.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvRecentOrders.ColumnHeadersHeight = 40;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRecentOrders.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvRecentOrders.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvRecentOrders.Location = new System.Drawing.Point(11, 45);
            this.dgvRecentOrders.Name = "dgvRecentOrders";
            this.dgvRecentOrders.ReadOnly = true;
            this.dgvRecentOrders.RowHeadersVisible = false;
            this.dgvRecentOrders.RowTemplate.Height = 35;
            this.dgvRecentOrders.Size = new System.Drawing.Size(398, 360);
            this.dgvRecentOrders.TabIndex = 1;
            this.dgvRecentOrders.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvRecentOrders.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvRecentOrders.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvRecentOrders.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvRecentOrders.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvRecentOrders.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvRecentOrders.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvRecentOrders.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvRecentOrders.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvRecentOrders.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvRecentOrders.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvRecentOrders.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvRecentOrders.ThemeStyle.HeaderStyle.Height = 40;
            this.dgvRecentOrders.ThemeStyle.ReadOnly = true;
            this.dgvRecentOrders.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvRecentOrders.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvRecentOrders.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.dgvRecentOrders.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvRecentOrders.ThemeStyle.RowsStyle.Height = 35;
            this.dgvRecentOrders.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvRecentOrders.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // lblRecentOrdersTitle
            // 
            this.lblRecentOrdersTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRecentOrdersTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblRecentOrdersTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblRecentOrdersTitle.Location = new System.Drawing.Point(0, 0);
            this.lblRecentOrdersTitle.Name = "lblRecentOrdersTitle";
            this.lblRecentOrdersTitle.Size = new System.Drawing.Size(420, 40);
            this.lblRecentOrdersTitle.TabIndex = 0;
            this.lblRecentOrdersTitle.Text = "📦 Đơn hàng gần đây";
            this.lblRecentOrdersTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlTopCars
            // 
            this.pnlTopCars.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTopCars.BackColor = System.Drawing.Color.Transparent;
            this.pnlTopCars.BorderRadius = 15;
            this.pnlTopCars.Controls.Add(this.dgvTopCars);
            this.pnlTopCars.Controls.Add(this.lblTopCarsTitle);
            this.pnlTopCars.FillColor = System.Drawing.Color.White;
            this.pnlTopCars.Location = new System.Drawing.Point(435, 5);
            this.pnlTopCars.Margin = new System.Windows.Forms.Padding(5);
            this.pnlTopCars.Name = "pnlTopCars";
            this.pnlTopCars.ShadowDecoration.BorderRadius = 15;
            this.pnlTopCars.ShadowDecoration.Depth = 10;
            this.pnlTopCars.ShadowDecoration.Enabled = true;
            this.pnlTopCars.Size = new System.Drawing.Size(420, 416);
            this.pnlTopCars.TabIndex = 5;
            // 
            // dgvTopCars
            // 
            this.dgvTopCars.AllowUserToAddRows = false;
            this.dgvTopCars.AllowUserToDeleteRows = false;
            this.dgvTopCars.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            this.dgvTopCars.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvTopCars.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTopCars.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvTopCars.ColumnHeadersHeight = 40;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTopCars.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvTopCars.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvTopCars.Location = new System.Drawing.Point(11, 45);
            this.dgvTopCars.Name = "dgvTopCars";
            this.dgvTopCars.ReadOnly = true;
            this.dgvTopCars.RowHeadersVisible = false;
            this.dgvTopCars.RowTemplate.Height = 35;
            this.dgvTopCars.Size = new System.Drawing.Size(398, 360);
            this.dgvTopCars.TabIndex = 1;
            this.dgvTopCars.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvTopCars.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvTopCars.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvTopCars.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvTopCars.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvTopCars.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvTopCars.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvTopCars.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.dgvTopCars.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvTopCars.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvTopCars.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvTopCars.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvTopCars.ThemeStyle.HeaderStyle.Height = 40;
            this.dgvTopCars.ThemeStyle.ReadOnly = true;
            this.dgvTopCars.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvTopCars.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvTopCars.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.dgvTopCars.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvTopCars.ThemeStyle.RowsStyle.Height = 35;
            this.dgvTopCars.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvTopCars.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // lblTopCarsTitle
            // 
            this.lblTopCarsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTopCarsTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTopCarsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblTopCarsTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTopCarsTitle.Name = "lblTopCarsTitle";
            this.lblTopCarsTitle.Size = new System.Drawing.Size(420, 40);
            this.lblTopCarsTitle.TabIndex = 0;
            this.lblTopCarsTitle.Text = "🏆 Top 5 xe bán chạy nhất";
            this.lblTopCarsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // shadowForm
            // 
            this.shadowForm.BorderRadius = 15;
            this.shadowForm.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.shadowForm.TargetForm = this;
            // 
            // elipseForm
            // 
            this.elipseForm.BorderRadius = 15;
            this.elipseForm.TargetControl = this;
            // 
            // AdminMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 640);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.pnlSidebar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(900, 569);
            this.Name = "AdminMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Admin Dashboard - Car Sales System";
            this.pnlSidebar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picAdmin)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.tableLayoutMain.ResumeLayout(false);
            this.tableLayoutCards.ResumeLayout(false);
            this.cardTotalUsers.ResumeLayout(false);
            this.cardTotalCars.ResumeLayout(false);
            this.cardTotalOrders.ResumeLayout(false);
            this.cardTotalRevenue.ResumeLayout(false);
            this.tableLayoutTables.ResumeLayout(false);
            this.pnlRecentOrders.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentOrders)).EndInit();
            this.pnlTopCars.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopCars)).EndInit();
            this.ResumeLayout(false);

        }
    }
}