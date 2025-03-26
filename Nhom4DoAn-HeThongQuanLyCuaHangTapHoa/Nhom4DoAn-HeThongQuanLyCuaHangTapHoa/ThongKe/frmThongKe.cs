using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;

namespace Nhom4DoAn_HeThongQuanLyCuaHangTapHoa.ThongKe
{
    public partial class frmThongKe: Form
    {
        public frmThongKe()
        {
            InitializeComponent();
        }

        private void LoadChildFormToTab(Form childForm, TabPage tabPage)
        {
            childForm.TopLevel = false;  // Không phải form chính
            childForm.FormBorderStyle = FormBorderStyle.None; // Ẩn viền form
            childForm.Dock = DockStyle.Fill; // Hiển thị full trong tab

            tabPage.Controls.Clear(); // Xóa nếu có nội dung cũ
            tabPage.Controls.Add(childForm); // Thêm form vào tab
            childForm.Show(); // Hiển thị form con
        }
        private void ThongKe_Load(object sender, EventArgs e)
        {
            // Khi form ThongKe mở, load form con vào từng tab
            LoadChildFormToTab(new frmMatHangBanChay(), tabPageMatHangBanChay);
            LoadChildFormToTab(new frmMatHangSapHet(), tabPageMatHangSapHet);
            LoadChildFormToTab(new frmDoanhThu(), tabPageDoanhThu);
            LoadChildFormToTab(new frmBieuDo(), tabPageBieuDo);

        }
    }
}
