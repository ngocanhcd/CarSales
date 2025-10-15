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
    public partial class EmployeeMainForm : Form
    {
        public EmployeeMainForm()
        {
            InitializeComponent();
            this.Text = "Employee Dashboard";
            this.Size = new System.Drawing.Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lbl = new Label();
            lbl.Text = "Employee Dashboard\n(Đang phát triển)";
            lbl.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            lbl.AutoSize = true;
            lbl.Location = new System.Drawing.Point(400, 300);
            this.Controls.Add(lbl);
        }

        
    }
}
