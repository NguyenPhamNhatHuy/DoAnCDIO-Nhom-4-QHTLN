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
    public partial class frmNhanVien: Form
    {
        public frmNhanVien()
        {
            InitializeComponent();
        }

        private void btnDangXuat_NV_Click(object sender, EventArgs e)
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
