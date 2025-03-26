using Nhom4DoAn_HeThongQuanLyCuaHangTapHoa.HoaDon;
using Nhom4DoAn_HeThongQuanLyCuaHangTapHoa.QuanLy;
using Nhom4DoAn_HeThongQuanLyCuaHangTapHoa.ThongKe;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nhom4DoAn_HeThongQuanLyCuaHangTapHoa
{
    public partial class frmMain: Form
    {
        private User user;
        public User User { get => user; set => user = value; }
        public frmMain()
        {
            InitializeComponent();
        }
        // xử lí mở form con
        private Form activeform = null;
        private void openChildForm(Form childForm)
        {
            if (activeform != null)
                activeform.Close();
            activeform = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelFormNho.Controls.Add(childForm);
            panelFormNho.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        // Xử lý chuyển màu khi click vào button
        private Guna.UI2.WinForms.Guna2Button currentButton = null;

        private void ActivateButton(object btnSender)
        {
            if (btnSender is Guna.UI2.WinForms.Guna2Button btn)  // Kiểm tra kiểu đúng
            {
                // Đặt lại màu của nút cũ (nếu có)
                if (currentButton != null && currentButton != btn)
                {
                    currentButton.FillColor = Color.FromArgb(100, 149, 237);  // Màu xanh dương nhạt mặc định
                    currentButton.ForeColor = Color.White;                     // Màu chữ mặc định
                }

                // Thiết lập màu cho nút được chọn
                Color color = ColorTranslator.FromHtml("#00EE00");  // Màu xanh đậm khi được chọn
                currentButton = btn;
                currentButton.FillColor = color;
                currentButton.ForeColor = Color.White;
            }
        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            openChildForm(new frmQuanLySanPham());
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            openChildForm(new frmQuanLyHoaDon());
        }

        private void btnDanhMuc_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            openChildForm(new frmQuanLyDanhMuc());
        }
        private void btnNhapKho_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            openChildForm(new frmQuanLyNhapKho());
        }

        private void btnNhaCungCap_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            openChildForm(new frmQuanLyNhaCungCap());
        }

        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            openChildForm(new frmQuanLyTaiKhoan());
        }

        private void btnDashBoard_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            openChildForm(new frmThongKe());

        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có thực sự muốn đăng xuất không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (DialogResult.Yes.Equals(result))
            {
                frmDangNhap frm = new frmDangNhap();
                frm.Closed += (s, args) => this.Close();
                frm.Show();
                this.Hide();
            }
            else
            {
                return;
            }
        }
    }
}
