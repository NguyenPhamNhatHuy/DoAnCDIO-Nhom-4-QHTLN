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
    public partial class frmCapNhatThongTinKH: Form
    {
        quanlycuahangtaphoaEntities TT = new quanlycuahangtaphoaEntities();
        public string PhoneNumber { get; set; }
        Customer customer;

        public frmCapNhatThongTinKH()
        {
            InitializeComponent();
        }

        private void frmCapNhatThongKH_Load(object sender, EventArgs e)
        {
            customer = TT.Customers.
               SingleOrDefault(c => c.phone == PhoneNumber);

            txtMaKH_CNTTKH.Text = customer.customerID.ToString();
            txtSoDienThoai_CNTTKH.Text = customer.phone;
            txtHoTen_CNTTKH.Text = customer.name;
            txtDiaChi_CNTTKH.Text = customer.address;
        }

        private void btnCapNhat_CNTTKH_Click(object sender, EventArgs e)
        {
            customer.name = txtHoTen_CNTTKH.Text;
            customer.address = txtDiaChi_CNTTKH.Text;


            if (string.IsNullOrEmpty(customer.name) || string.IsNullOrEmpty(customer.name)
                || string.IsNullOrEmpty(customer.address) || string.IsNullOrEmpty(customer.address))
            {
                MessageBox.Show("Bạn cần nhập đầy đủ thông tin", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                TT.SaveChanges();
                MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
        }

        private void btnDong_CNTTKH_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
