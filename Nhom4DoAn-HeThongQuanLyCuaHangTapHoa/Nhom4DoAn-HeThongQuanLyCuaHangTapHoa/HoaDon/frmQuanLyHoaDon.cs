using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nhom4DoAn_HeThongQuanLyCuaHangTapHoa.HoaDon
{
    public partial class frmQuanLyHoaDon : Form
    {
        int orderIDTemp = -1;
        quanlycuahangtaphoaEntities HD = new quanlycuahangtaphoaEntities();
        public frmQuanLyHoaDon()
        {
            InitializeComponent();
        }
        public void loadData()
        {
            var result = HD.Orders.Select(p => new
            {
                orderID = p.orderID,
                customerID = p.customerID,
                userId = p.userID,
                createAt = p.createdAt
            }).ToList();

            dgvHoaDon_QLHD.DataSource = result.ToList();
            dgvHoaDon_QLHD.Columns[0].HeaderText = "Mã hóa đơn";
            dgvHoaDon_QLHD.Columns[1].HeaderText = "Mã khách hàng";
            dgvHoaDon_QLHD.Columns[2].HeaderText = "Mã nhân viên";
            dgvHoaDon_QLHD.Columns[3].HeaderText = "Ngày lập";
            // Đăng ký sự kiện CellClick cho DataGridView
            dgvHoaDon_QLHD.CellClick += new DataGridViewCellEventHandler(dgvHoaDon_QLHD_CellContentClick);
        }

        private void clearGroupBox()
        {
            lbMaHoaDon_QLHD.Text = "Mã hóa đơn: ";
            lbMaKhachHang_QLHD.Text = "Mã khách hàng: ";
            lbMaNhanVien_QLHD.Text = "Mã nhân viên: ";
            lbNgayLap.Text = "Ngày lập: ";
            grBHoaDon_QLHD.Visible = false;
            btnXemChiTiet_QLHD.Enabled = false;
        }

        private void btnXemChiTiet_QLHD_Click(object sender, EventArgs e)
        {
            if (orderIDTemp != -1)
            {
                frmChiTietHoaDon ChiTiet = new frmChiTietHoaDon();
                ChiTiet.OrderID = orderIDTemp;
                ChiTiet.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn 1 hóa đơn để xem!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void onDateChange(object sender, EventArgs e)
        {
            onDateChangeData();
        }

        private void onDateFromChange(object sender, EventArgs e)
        {
            onDateChangeData();
        }
        public void onDateChangeData()
        {
            var result = HD.Orders.Select(p => new
            {
                orderID = p.orderID,
                customerID = p.customerID,
                userId = p.userID,
                createAt = p.createdAt
            }).Where(p => p.createAt >= dtpTuNgay_QLHD.Value && p.createAt <= dtpDenNgay_QLHD.Value);
            dgvHoaDon_QLHD.DataSource = result.ToList();
            // Ẩn GroupBox nếu không có dữ liệu
            clearGroupBox();
        }

        private void frmQuanLyHoaDon_Load(object sender, EventArgs e)
        {
            loadData();
            grBHoaDon_QLHD.Visible = false;
            btnXemChiTiet_QLHD.Enabled = false;
            this.dtpTuNgay_QLHD.ValueChanged += new System.EventHandler(this.dtpTuNgay_QLHD_ValueChanged);
            this.dtpDenNgay_QLHD.ValueChanged += new System.EventHandler(this.dtpDenNgay_QLHD_ValueChanged);
        }

        private void dgvHoaDon_QLHD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dataGridViewRow = dgvHoaDon_QLHD.CurrentRow;
            String orderID = dataGridViewRow.Cells[0].Value.ToString();
            String cusID = dataGridViewRow.Cells[1].Value.ToString();
            String userID = dataGridViewRow.Cells[2].Value.ToString();
            var dateCreateValue = dataGridViewRow.Cells[3].Value;
            String dateCreate = dateCreateValue != null ? dateCreateValue.ToString() : string.Empty;
            String[] list = dateCreate.Split(' ');
            orderIDTemp = int.Parse(orderID);

            grBHoaDon_QLHD.Visible = true;

            lbMaHoaDon_QLHD.Text = "Mã hóa đơn:" + orderID;
            lbMaKhachHang_QLHD.Text = "Mã khách hàng: " + cusID;
            lbNgayLap.Text = "Ngày lập: " + list[0];
            lbMaNhanVien_QLHD.Text = "Mã nhân viên: " + userID;

            btnXemChiTiet_QLHD.Enabled = true;
        }

        private void dtpTuNgay_QLHD_ValueChanged(object sender, EventArgs e)
        {
            clearGroupBox();
            onDateChangeData();
        }

        private void dtpDenNgay_QLHD_ValueChanged(object sender, EventArgs e)
        {
            clearGroupBox();
            onDateChangeData();
        }
    }
}