namespace Nhom4DoAn_HeThongQuanLyCuaHangTapHoa.QuanLy
{
    partial class frmQuanLyDanhMuc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQuanLyDanhMuc));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvDanhMuc_QLDM = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExport_QLDM = new Guna.UI2.WinForms.Guna2Button();
            this.btnImport_QLDM = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoa_QLDM = new Guna.UI2.WinForms.Guna2Button();
            this.btnCapNhat_QLDM = new Guna.UI2.WinForms.Guna2Button();
            this.btnThem_QLDM = new Guna.UI2.WinForms.Guna2Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnClear_QLDM = new Guna.UI2.WinForms.Guna2Button();
            this.btnLoading_QLDM = new Guna.UI2.WinForms.Guna2Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTenDanhMuc_QLDM = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtMaDanhMuc_QLDM = new Guna.UI2.WinForms.Guna2TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhMuc_QLDM)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.dgvDanhMuc_QLDM, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35.07853F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 64.92146F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1282, 853);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dgvDanhMuc_QLDM
            // 
            this.dgvDanhMuc_QLDM.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDanhMuc_QLDM.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDanhMuc_QLDM.Location = new System.Drawing.Point(3, 271);
            this.dgvDanhMuc_QLDM.Name = "dgvDanhMuc_QLDM";
            this.dgvDanhMuc_QLDM.RowHeadersWidth = 51;
            this.dgvDanhMuc_QLDM.RowTemplate.Height = 24;
            this.dgvDanhMuc_QLDM.Size = new System.Drawing.Size(1276, 490);
            this.dgvDanhMuc_QLDM.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnExport_QLDM);
            this.panel1.Controls.Add(this.btnImport_QLDM);
            this.panel1.Controls.Add(this.btnXoa_QLDM);
            this.panel1.Controls.Add(this.btnCapNhat_QLDM);
            this.panel1.Controls.Add(this.btnThem_QLDM);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 767);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1276, 83);
            this.panel1.TabIndex = 0;
            // 
            // btnExport_QLDM
            // 
            this.btnExport_QLDM.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnExport_QLDM.BorderRadius = 15;
            this.btnExport_QLDM.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnExport_QLDM.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnExport_QLDM.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnExport_QLDM.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnExport_QLDM.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport_QLDM.ForeColor = System.Drawing.Color.White;
            this.btnExport_QLDM.Location = new System.Drawing.Point(1025, 19);
            this.btnExport_QLDM.Name = "btnExport_QLDM";
            this.btnExport_QLDM.Size = new System.Drawing.Size(138, 45);
            this.btnExport_QLDM.TabIndex = 23;
            this.btnExport_QLDM.Text = "Export";
            // 
            // btnImport_QLDM
            // 
            this.btnImport_QLDM.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnImport_QLDM.BorderRadius = 15;
            this.btnImport_QLDM.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnImport_QLDM.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnImport_QLDM.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnImport_QLDM.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnImport_QLDM.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImport_QLDM.ForeColor = System.Drawing.Color.White;
            this.btnImport_QLDM.Location = new System.Drawing.Point(814, 19);
            this.btnImport_QLDM.Name = "btnImport_QLDM";
            this.btnImport_QLDM.Size = new System.Drawing.Size(138, 45);
            this.btnImport_QLDM.TabIndex = 22;
            this.btnImport_QLDM.Text = "Import";
            // 
            // btnXoa_QLDM
            // 
            this.btnXoa_QLDM.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnXoa_QLDM.BorderRadius = 15;
            this.btnXoa_QLDM.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnXoa_QLDM.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnXoa_QLDM.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnXoa_QLDM.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnXoa_QLDM.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa_QLDM.ForeColor = System.Drawing.Color.White;
            this.btnXoa_QLDM.Location = new System.Drawing.Point(585, 19);
            this.btnXoa_QLDM.Name = "btnXoa_QLDM";
            this.btnXoa_QLDM.Size = new System.Drawing.Size(138, 45);
            this.btnXoa_QLDM.TabIndex = 21;
            this.btnXoa_QLDM.Text = "Xóa";
            // 
            // btnCapNhat_QLDM
            // 
            this.btnCapNhat_QLDM.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCapNhat_QLDM.BorderRadius = 15;
            this.btnCapNhat_QLDM.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCapNhat_QLDM.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCapNhat_QLDM.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCapNhat_QLDM.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCapNhat_QLDM.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCapNhat_QLDM.ForeColor = System.Drawing.Color.White;
            this.btnCapNhat_QLDM.Location = new System.Drawing.Point(354, 19);
            this.btnCapNhat_QLDM.Name = "btnCapNhat_QLDM";
            this.btnCapNhat_QLDM.Size = new System.Drawing.Size(138, 45);
            this.btnCapNhat_QLDM.TabIndex = 20;
            this.btnCapNhat_QLDM.Text = "Cập Nhật ";
            // 
            // btnThem_QLDM
            // 
            this.btnThem_QLDM.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnThem_QLDM.BorderRadius = 15;
            this.btnThem_QLDM.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnThem_QLDM.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnThem_QLDM.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnThem_QLDM.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnThem_QLDM.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThem_QLDM.ForeColor = System.Drawing.Color.White;
            this.btnThem_QLDM.Location = new System.Drawing.Point(112, 19);
            this.btnThem_QLDM.Name = "btnThem_QLDM";
            this.btnThem_QLDM.Size = new System.Drawing.Size(138, 45);
            this.btnThem_QLDM.TabIndex = 19;
            this.btnThem_QLDM.Text = "Thêm";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnClear_QLDM);
            this.panel2.Controls.Add(this.btnLoading_QLDM);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtTenDanhMuc_QLDM);
            this.panel2.Controls.Add(this.txtMaDanhMuc_QLDM);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1276, 262);
            this.panel2.TabIndex = 1;
            // 
            // btnClear_QLDM
            // 
            this.btnClear_QLDM.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnClear_QLDM.BackColor = System.Drawing.SystemColors.Control;
            this.btnClear_QLDM.BorderRadius = 15;
            this.btnClear_QLDM.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnClear_QLDM.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnClear_QLDM.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnClear_QLDM.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnClear_QLDM.FillColor = System.Drawing.Color.White;
            this.btnClear_QLDM.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClear_QLDM.ForeColor = System.Drawing.Color.White;
            this.btnClear_QLDM.Image = ((System.Drawing.Image)(resources.GetObject("btnClear_QLDM.Image")));
            this.btnClear_QLDM.Location = new System.Drawing.Point(872, 162);
            this.btnClear_QLDM.Name = "btnClear_QLDM";
            this.btnClear_QLDM.Size = new System.Drawing.Size(50, 41);
            this.btnClear_QLDM.TabIndex = 135;
            // 
            // btnLoading_QLDM
            // 
            this.btnLoading_QLDM.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLoading_QLDM.BackColor = System.Drawing.SystemColors.Control;
            this.btnLoading_QLDM.BorderRadius = 15;
            this.btnLoading_QLDM.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLoading_QLDM.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLoading_QLDM.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLoading_QLDM.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLoading_QLDM.FillColor = System.Drawing.Color.White;
            this.btnLoading_QLDM.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnLoading_QLDM.ForeColor = System.Drawing.Color.White;
            this.btnLoading_QLDM.Image = ((System.Drawing.Image)(resources.GetObject("btnLoading_QLDM.Image")));
            this.btnLoading_QLDM.Location = new System.Drawing.Point(872, 93);
            this.btnLoading_QLDM.Name = "btnLoading_QLDM";
            this.btnLoading_QLDM.Size = new System.Drawing.Size(50, 42);
            this.btnLoading_QLDM.TabIndex = 134;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(481, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(297, 37);
            this.label1.TabIndex = 25;
            this.label1.Text = "Quản Lý Danh Mục";
            // 
            // txtTenDanhMuc_QLDM
            // 
            this.txtTenDanhMuc_QLDM.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtTenDanhMuc_QLDM.BorderRadius = 15;
            this.txtTenDanhMuc_QLDM.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenDanhMuc_QLDM.DefaultText = "";
            this.txtTenDanhMuc_QLDM.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTenDanhMuc_QLDM.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTenDanhMuc_QLDM.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTenDanhMuc_QLDM.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTenDanhMuc_QLDM.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTenDanhMuc_QLDM.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenDanhMuc_QLDM.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTenDanhMuc_QLDM.Location = new System.Drawing.Point(573, 162);
            this.txtTenDanhMuc_QLDM.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTenDanhMuc_QLDM.Name = "txtTenDanhMuc_QLDM";
            this.txtTenDanhMuc_QLDM.PlaceholderText = "";
            this.txtTenDanhMuc_QLDM.SelectedText = "";
            this.txtTenDanhMuc_QLDM.Size = new System.Drawing.Size(280, 41);
            this.txtTenDanhMuc_QLDM.TabIndex = 24;
            // 
            // txtMaDanhMuc_QLDM
            // 
            this.txtMaDanhMuc_QLDM.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtMaDanhMuc_QLDM.BorderRadius = 15;
            this.txtMaDanhMuc_QLDM.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaDanhMuc_QLDM.DefaultText = "";
            this.txtMaDanhMuc_QLDM.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtMaDanhMuc_QLDM.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtMaDanhMuc_QLDM.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMaDanhMuc_QLDM.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMaDanhMuc_QLDM.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMaDanhMuc_QLDM.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaDanhMuc_QLDM.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMaDanhMuc_QLDM.Location = new System.Drawing.Point(573, 93);
            this.txtMaDanhMuc_QLDM.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMaDanhMuc_QLDM.Name = "txtMaDanhMuc_QLDM";
            this.txtMaDanhMuc_QLDM.PlaceholderText = "";
            this.txtMaDanhMuc_QLDM.SelectedText = "";
            this.txtMaDanhMuc_QLDM.Size = new System.Drawing.Size(280, 41);
            this.txtMaDanhMuc_QLDM.TabIndex = 23;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(389, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 22);
            this.label3.TabIndex = 22;
            this.label3.Text = "Tên Danh Mục";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(389, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 22);
            this.label2.TabIndex = 21;
            this.label2.Text = "Mã Danh Mục";
            // 
            // frmQuanLyDanhMuc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1282, 853);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmQuanLyDanhMuc";
            this.Text = "frmQuanLyDanhMuc";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhMuc_QLDM)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private Guna.UI2.WinForms.Guna2Button btnExport_QLDM;
        private Guna.UI2.WinForms.Guna2Button btnImport_QLDM;
        private Guna.UI2.WinForms.Guna2Button btnXoa_QLDM;
        private Guna.UI2.WinForms.Guna2Button btnCapNhat_QLDM;
        private Guna.UI2.WinForms.Guna2Button btnThem_QLDM;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2TextBox txtTenDanhMuc_QLDM;
        private Guna.UI2.WinForms.Guna2TextBox txtMaDanhMuc_QLDM;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvDanhMuc_QLDM;
        private Guna.UI2.WinForms.Guna2Button btnClear_QLDM;
        private Guna.UI2.WinForms.Guna2Button btnLoading_QLDM;
    }
}