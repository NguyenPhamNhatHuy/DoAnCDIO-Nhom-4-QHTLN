namespace Nhom4DoAn_HeThongQuanLyCuaHangTapHoa.ThongKe
{
    partial class frmThongKe
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageMatHangSapHet = new System.Windows.Forms.TabPage();
            this.tabPageMatHangBanChay = new System.Windows.Forms.TabPage();
            this.tabPageDoanhThu = new System.Windows.Forms.TabPage();
            this.tabPageBieuDo = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageMatHangSapHet);
            this.tabControl1.Controls.Add(this.tabPageMatHangBanChay);
            this.tabControl1.Controls.Add(this.tabPageDoanhThu);
            this.tabControl1.Controls.Add(this.tabPageBieuDo);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1282, 853);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageMatHangSapHet
            // 
            this.tabPageMatHangSapHet.Location = new System.Drawing.Point(4, 25);
            this.tabPageMatHangSapHet.Name = "tabPageMatHangSapHet";
            this.tabPageMatHangSapHet.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMatHangSapHet.Size = new System.Drawing.Size(1274, 824);
            this.tabPageMatHangSapHet.TabIndex = 0;
            this.tabPageMatHangSapHet.Text = "Mặt Hàng Sắp Hết";
            this.tabPageMatHangSapHet.UseVisualStyleBackColor = true;
            // 
            // tabPageMatHangBanChay
            // 
            this.tabPageMatHangBanChay.Location = new System.Drawing.Point(4, 25);
            this.tabPageMatHangBanChay.Name = "tabPageMatHangBanChay";
            this.tabPageMatHangBanChay.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMatHangBanChay.Size = new System.Drawing.Size(1274, 824);
            this.tabPageMatHangBanChay.TabIndex = 1;
            this.tabPageMatHangBanChay.Text = "Mặt Hàng Bán Chạy";
            this.tabPageMatHangBanChay.UseVisualStyleBackColor = true;
            // 
            // tabPageDoanhThu
            // 
            this.tabPageDoanhThu.Location = new System.Drawing.Point(4, 25);
            this.tabPageDoanhThu.Name = "tabPageDoanhThu";
            this.tabPageDoanhThu.Size = new System.Drawing.Size(1274, 824);
            this.tabPageDoanhThu.TabIndex = 2;
            this.tabPageDoanhThu.Text = "Doanh Thu";
            this.tabPageDoanhThu.UseVisualStyleBackColor = true;
            // 
            // tabPageBieuDo
            // 
            this.tabPageBieuDo.Location = new System.Drawing.Point(4, 25);
            this.tabPageBieuDo.Name = "tabPageBieuDo";
            this.tabPageBieuDo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBieuDo.Size = new System.Drawing.Size(1274, 824);
            this.tabPageBieuDo.TabIndex = 3;
            this.tabPageBieuDo.Text = "Biểu Đồ";
            this.tabPageBieuDo.UseVisualStyleBackColor = true;
            // 
            // frmThongKe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1282, 853);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmThongKe";
            this.Text = "ThongKe";
            this.Load += new System.EventHandler(this.ThongKe_Load);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageMatHangSapHet;
        private System.Windows.Forms.TabPage tabPageMatHangBanChay;
        private System.Windows.Forms.TabPage tabPageDoanhThu;
        private System.Windows.Forms.TabPage tabPageBieuDo;
    }
}