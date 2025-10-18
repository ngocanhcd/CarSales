namespace GUI_CarSales
{
    partial class CarAddEditForm
    {
        private System.ComponentModel.IContainer components = null;

        // Top Panel
        private Guna.UI2.WinForms.Guna2Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private Guna.UI2.WinForms.Guna2Button btnClose;

        // Image Upload
        private Guna.UI2.WinForms.Guna2PictureBox picCarImage;
        private Guna.UI2.WinForms.Guna2Button btnUploadImage;
        private System.Windows.Forms.Label lblImagePath;

        // Input Fields
        private System.Windows.Forms.Label lblCarName;
        private Guna.UI2.WinForms.Guna2TextBox txtCarName;

        private System.Windows.Forms.Label lblPrice;
        private Guna.UI2.WinForms.Guna2TextBox txtPrice;

        private System.Windows.Forms.Label lblCarType;
        private Guna.UI2.WinForms.Guna2ComboBox cboCarType;

        private System.Windows.Forms.Label lblStockQuantity;
        private Guna.UI2.WinForms.Guna2TextBox txtStockQuantity;

        private System.Windows.Forms.Label lblStatus;
        private Guna.UI2.WinForms.Guna2ComboBox cboStatus;

        // Action Buttons
        private Guna.UI2.WinForms.Guna2Button btnSave;
        private Guna.UI2.WinForms.Guna2Button btnCancel;

        // Shadow & Elipse
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
            this.pnlTop = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnClose = new Guna.UI2.WinForms.Guna2Button();
            this.picCarImage = new Guna.UI2.WinForms.Guna2PictureBox();
            this.btnUploadImage = new Guna.UI2.WinForms.Guna2Button();
            this.lblImagePath = new System.Windows.Forms.Label();
            this.lblCarName = new System.Windows.Forms.Label();
            this.txtCarName = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblPrice = new System.Windows.Forms.Label();
            this.txtPrice = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblCarType = new System.Windows.Forms.Label();
            this.cboCarType = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblStockQuantity = new System.Windows.Forms.Label();
            this.txtStockQuantity = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cboStatus = new Guna.UI2.WinForms.Guna2ComboBox();
            this.btnSave = new Guna.UI2.WinForms.Guna2Button();
            this.btnCancel = new Guna.UI2.WinForms.Guna2Button();
            this.shadowForm = new Guna.UI2.WinForms.Guna2ShadowForm(this.components);
            this.elipseForm = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCarImage)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.pnlTop.Controls.Add(this.btnClose);
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(700, 60);
            this.pnlTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 17);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(232, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "THÊM/SỬA XE Ô TÔ";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Animated = true;
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BorderRadius = 5;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FillColor = System.Drawing.Color.Transparent;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnClose.Location = new System.Drawing.Point(657, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(35, 35);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "✕";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // picCarImage
            // 
            this.picCarImage.BorderRadius = 10;
            this.picCarImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picCarImage.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.picCarImage.ImageRotate = 0F;
            this.picCarImage.Location = new System.Drawing.Point(25, 80);
            this.picCarImage.Name = "picCarImage";
            this.picCarImage.Size = new System.Drawing.Size(200, 150);
            this.picCarImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCarImage.TabIndex = 1;
            this.picCarImage.TabStop = false;
            // 
            // btnUploadImage
            // 
            this.btnUploadImage.Animated = true;
            this.btnUploadImage.BorderRadius = 8;
            this.btnUploadImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUploadImage.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnUploadImage.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnUploadImage.ForeColor = System.Drawing.Color.White;
            this.btnUploadImage.Location = new System.Drawing.Point(25, 240);
            this.btnUploadImage.Name = "btnUploadImage";
            this.btnUploadImage.Size = new System.Drawing.Size(200, 40);
            this.btnUploadImage.TabIndex = 2;
            this.btnUploadImage.Text = "📷 Chọn ảnh xe";
            this.btnUploadImage.Click += new System.EventHandler(this.btnUploadImage_Click);
            // 
            // lblImagePath
            // 
            this.lblImagePath.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.lblImagePath.ForeColor = System.Drawing.Color.Gray;
            this.lblImagePath.Location = new System.Drawing.Point(25, 285);
            this.lblImagePath.Name = "lblImagePath";
            this.lblImagePath.Size = new System.Drawing.Size(200, 30);
            this.lblImagePath.TabIndex = 3;
            this.lblImagePath.Text = "Chưa có ảnh";
            this.lblImagePath.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblCarName
            // 
            this.lblCarName.AutoSize = true;
            this.lblCarName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCarName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.lblCarName.Location = new System.Drawing.Point(250, 80);
            this.lblCarName.Name = "lblCarName";
            this.lblCarName.Size = new System.Drawing.Size(68, 19);
            this.lblCarName.TabIndex = 4;
            this.lblCarName.Text = "Tên xe *";
            // 
            // txtCarName
            // 
            this.txtCarName.BorderRadius = 8;
            this.txtCarName.BorderThickness = 2;
            this.txtCarName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCarName.DefaultText = "";
            this.txtCarName.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtCarName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCarName.Location = new System.Drawing.Point(250, 105);
            this.txtCarName.Name = "txtCarName";
            this.txtCarName.PlaceholderText = "Nhập tên xe (vd: Toyota Fortuner)";
            this.txtCarName.SelectedText = "";
            this.txtCarName.Size = new System.Drawing.Size(420, 40);
            this.txtCarName.TabIndex = 5;
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.lblPrice.Location = new System.Drawing.Point(250, 155);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(95, 19);
            this.lblPrice.TabIndex = 6;
            this.lblPrice.Text = "Giá xe (VNĐ) *";
            // 
            // txtPrice
            // 
            this.txtPrice.BorderRadius = 8;
            this.txtPrice.BorderThickness = 2;
            this.txtPrice.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPrice.DefaultText = "";
            this.txtPrice.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtPrice.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPrice.Location = new System.Drawing.Point(250, 180);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.PlaceholderText = "Nhập giá xe (vd: 1200000000)";
            this.txtPrice.SelectedText = "";
            this.txtPrice.Size = new System.Drawing.Size(420, 40);
            this.txtPrice.TabIndex = 7;
            // 
            // lblCarType
            // 
            this.lblCarType.AutoSize = true;
            this.lblCarType.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCarType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.lblCarType.Location = new System.Drawing.Point(250, 230);
            this.lblCarType.Name = "lblCarType";
            this.lblCarType.Size = new System.Drawing.Size(65, 19);
            this.lblCarType.TabIndex = 8;
            this.lblCarType.Text = "Loại xe *";
            // 
            // cboCarType
            // 
            this.cboCarType.BackColor = System.Drawing.Color.Transparent;
            this.cboCarType.BorderRadius = 8;
            this.cboCarType.BorderThickness = 2;
            this.cboCarType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboCarType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCarType.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboCarType.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboCarType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboCarType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboCarType.ItemHeight = 30;
            this.cboCarType.Location = new System.Drawing.Point(250, 255);
            this.cboCarType.Name = "cboCarType";
            this.cboCarType.Size = new System.Drawing.Size(420, 36);
            this.cboCarType.TabIndex = 9;
            // 
            // lblStockQuantity
            // 
            this.lblStockQuantity.AutoSize = true;
            this.lblStockQuantity.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblStockQuantity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.lblStockQuantity.Location = new System.Drawing.Point(250, 305);
            this.lblStockQuantity.Name = "lblStockQuantity";
            this.lblStockQuantity.Size = new System.Drawing.Size(143, 19);
            this.lblStockQuantity.TabIndex = 10;
            this.lblStockQuantity.Text = "Số lượng tồn kho *";
            // 
            // txtStockQuantity
            // 
            this.txtStockQuantity.BorderRadius = 8;
            this.txtStockQuantity.BorderThickness = 2;
            this.txtStockQuantity.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtStockQuantity.DefaultText = "";
            this.txtStockQuantity.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtStockQuantity.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtStockQuantity.Location = new System.Drawing.Point(250, 330);
            this.txtStockQuantity.Name = "txtStockQuantity";
            this.txtStockQuantity.PlaceholderText = "Nhập số lượng (vd: 10)";
            this.txtStockQuantity.SelectedText = "";
            this.txtStockQuantity.Size = new System.Drawing.Size(420, 40);
            this.txtStockQuantity.TabIndex = 11;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.lblStatus.Location = new System.Drawing.Point(250, 380);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(84, 19);
            this.lblStatus.TabIndex = 12;
            this.lblStatus.Text = "Trạng thái *";
            // 
            // cboStatus
            // 
            this.cboStatus.BackColor = System.Drawing.Color.Transparent;
            this.cboStatus.BorderRadius = 8;
            this.cboStatus.BorderThickness = 2;
            this.cboStatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboStatus.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboStatus.ItemHeight = 30;
            this.cboStatus.Items.AddRange(new object[] {
            "Available",
            "Sold",
            "Reserved"});
            this.cboStatus.Location = new System.Drawing.Point(250, 405);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(420, 36);
            this.cboStatus.TabIndex = 13;
            // 
            // btnSave
            // 
            this.btnSave.Animated = true;
            this.btnSave.BorderRadius = 10;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(120)))), ((int)(((byte)(240)))));
            this.btnSave.Location = new System.Drawing.Point(250, 465);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(200, 50);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "💾 LƯU";
            // 
            // btnCancel
            // 
            this.btnCancel.Animated = true;
            this.btnCancel.BorderRadius = 10;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(87)))));
            this.btnCancel.Location = new System.Drawing.Point(470, 465);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(200, 50);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "✕ HỦY";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // shadowForm
            // 
            this.shadowForm.BorderRadius = 15;
            this.shadowForm.TargetForm = this;
            // 
            // elipseForm
            // 
            this.elipseForm.BorderRadius = 15;
            this.elipseForm.TargetControl = this;
            // 
            // CarAddEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(700, 540);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cboStatus);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.txtStockQuantity);
            this.Controls.Add(this.lblStockQuantity);
            this.Controls.Add(this.cboCarType);
            this.Controls.Add(this.lblCarType);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.txtCarName);
            this.Controls.Add(this.lblCarName);
            this.Controls.Add(this.lblImagePath);
            this.Controls.Add(this.btnUploadImage);
            this.Controls.Add(this.picCarImage);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CarAddEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thêm/Sửa Xe";
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCarImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}