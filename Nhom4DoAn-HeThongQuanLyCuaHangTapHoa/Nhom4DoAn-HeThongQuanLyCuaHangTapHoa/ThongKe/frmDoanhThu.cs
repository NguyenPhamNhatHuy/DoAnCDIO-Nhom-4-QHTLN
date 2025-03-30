using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nhom4DoAn_HeThongQuanLyCuaHangTapHoa.ThongKe
{
    public partial class frmDoanhThu : Form
    {
        quanlycuahangtaphoaEntities DT = new quanlycuahangtaphoaEntities();

        public frmDoanhThu()
        {
            InitializeComponent();
        }

        private void LoadRevenueData()
        {
            try
            {
                // Truy vấn cơ sở dữ liệu để lấy dữ liệu doanh thu theo tháng
                var revenueData = DT.Orders
                    .Where(o => o.createdAt.HasValue)
                    .GroupBy(o => new
                    {
                        Month = o.createdAt.Value.Month,
                        Year = o.createdAt.Value.Year
                    })
                    .Select(g => new
                    {
                        Month = g.Key.Month,
                        Year = g.Key.Year,
                        Revenue = g.Sum(o => o.totalAmount),
                        QuantitySold = g.Sum(o => o.OrderDetails.Sum(od => od.quantity)),
                        // Giả sử lợi nhuận là 30% doanh thu
                        Profit = g.Sum(o => o.totalAmount) * 0.3m
                    })
                    .OrderByDescending(r => r.Year)
                    .ThenByDescending(r => r.Month)
                    .ToList();

                // Chuyển đổi để thêm trường MonthYear sau khi đã lấy dữ liệu
                var formattedRevenueData = revenueData.Select(r => new
                {
                    MonthYear = $"{r.Month}/{r.Year}",
                    r.Revenue,
                    r.QuantitySold,
                    r.Profit,
                    r.Month,
                    r.Year
                }).ToList();

                // Hiển thị dữ liệu trong DataGridView
                dgvDThu.DataSource = formattedRevenueData;

                // Định dạng DataGridView
                FormatDataGridView();

                // Chọn hàng đầu tiên nếu có dữ liệu
                if (formattedRevenueData.Count > 0 && dgvDThu.Rows.Count > 0)
                {
                    dgvDThu.Rows[0].Selected = true;
                    UpdateSelectedMonthDetails();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu doanh thu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDataGridView()
        {
            // Đặt tiêu đề cho các cột
            dgvDThu.Columns["MonthYear"].HeaderText = "Tháng/Năm";
            dgvDThu.Columns["Revenue"].HeaderText = "Doanh thu";
            dgvDThu.Columns["QuantitySold"].HeaderText = "Số lượng đã bán";
            dgvDThu.Columns["Profit"].HeaderText = "Lợi nhuận";

            // Ẩn cột Month và Year
            dgvDThu.Columns["Month"].Visible = false;
            dgvDThu.Columns["Year"].Visible = false;

            // Định dạng hiển thị các cột tiền tệ trong DataGridView
            // QUAN TRỌNG: Không thay đổi giá trị thực trong cell, chỉ định dạng hiển thị
            dgvDThu.Columns["Revenue"].DefaultCellStyle.Format = "N0";
            dgvDThu.Columns["Revenue"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDThu.Columns["Revenue"].DefaultCellStyle.FormatProvider = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");

            dgvDThu.Columns["Profit"].DefaultCellStyle.Format = "N0";
            dgvDThu.Columns["Profit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDThu.Columns["Profit"].DefaultCellStyle.FormatProvider = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
        }

        private void UpdateSelectedMonthDetails()
        {
            if (dgvDThu.SelectedRows.Count > 0)
            {
                // Lấy hàng đã chọn
                DataGridViewRow row = dgvDThu.SelectedRows[0];

                // Hiển thị thông tin chi tiết trong các text box
                txtThang_DThu.Text = row.Cells["MonthYear"].Value.ToString();

                // Lấy giá trị gốc (không phải giá trị hiển thị đã định dạng)
                decimal revenue = Convert.ToDecimal(row.Cells["Revenue"].Value);
                decimal profit = Convert.ToDecimal(row.Cells["Profit"].Value);
                int quantitySold = Convert.ToInt32(row.Cells["QuantitySold"].Value);

                // Định dạng giá trị tiền tệ để hiển thị
                txtDoanhThu_DThu.Text = string.Format("{0:N0} VND", revenue);
                txtLoiNhuan_DThu.Text = string.Format("{0:N0} VND", profit);
                txtSoLuong_DThu.Text = quantitySold.ToString();
            }
        }

        private void dgvDThu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dgvDThu.Rows[e.RowIndex].Selected = true;
                UpdateSelectedMonthDetails();
            }
        }

        private void dgvDThu_SelectionChanged(object sender, EventArgs e)
        {
            UpdateSelectedMonthDetails();
        }

        private void DoanhThu_Load(object sender, EventArgs e)
        {
            LoadRevenueData();

            dgvDThu.CellClick += new DataGridViewCellEventHandler(dgvDThu_CellContentClick);

            txtDoanhThu_DThu.Text = " ";
            txtLoiNhuan_DThu.Text = " ";
            txtThang_DThu.Text = " ";
            txtSoLuong_DThu.Text = " ";
        }
    }
}
