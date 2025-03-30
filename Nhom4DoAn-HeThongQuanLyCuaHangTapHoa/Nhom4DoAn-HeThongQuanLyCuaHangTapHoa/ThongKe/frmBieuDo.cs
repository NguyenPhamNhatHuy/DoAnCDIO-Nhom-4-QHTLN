using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Nhom4DoAn_HeThongQuanLyCuaHangTapHoa.ThongKe
{
    public partial class frmBieuDo : Form
    {
        quanlycuahangtaphoaEntities BD = new quanlycuahangtaphoaEntities();
        public frmBieuDo()
        {
            InitializeComponent();
            LoadNam();
        }
        private void LoadNam()
        {
            // Lấy danh sách năm từ cơ sở dữ liệu
            var years = BD.Orders
                .Where(hd => hd.createdAt.HasValue)
                .Select(hd => hd.createdAt.Value.Year)
                .Distinct()
                .OrderBy(year => year)
                .ToList();

            cboChinhNamDT.Items.Clear();
            foreach (var year in years)
            {
                cboChinhNamDT.Items.Add(year); // Thêm năm vào ComboBox
            }

            if (cboChinhNamDT.Items.Count > 0)
            {
                cboChinhNamDT.SelectedIndex = 0; // Chọn năm đầu tiên mặc định
            }
        }
        private void LoadData()
        {
            if (cboChinhNamDT.SelectedItem == null) return;

            int selectedYear = (int)cboChinhNamDT.SelectedItem; // Lấy năm được chọn từ ComboBox

            // Khởi tạo một mảng doanh số cho 12 tháng
            decimal[] monthlySales = new decimal[12];

            // Truy vấn dữ liệu theo năm được chọn
            var purchaseData = BD.Orders
                .Where(hd => hd.createdAt.HasValue && hd.createdAt.Value.Year == selectedYear)
                .GroupBy(hd => hd.createdAt.Value.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    TotalAmount = g.Sum(hd => hd.totalAmount)
                })
                .ToList();

            // Điền dữ liệu vào mảng
            foreach (var data in purchaseData)
            {
                monthlySales[data.Month - 1] = (decimal)(data.TotalAmount ?? 0); // Tháng - 1 để chuyển đổi giữa 1-12 với chỉ số 0-11
            }

            // Xác định tháng có doanh thu cao nhất
            int maxMonthIndex = Array.IndexOf(monthlySales, monthlySales.Max());

            // Cấu hình biểu đồ
            chartHoaDon.Series.Clear();
            Series series = new Series($"Doanh số năm {selectedYear}")
            {
                ChartType = SeriesChartType.Column, // Đổi sang biểu đồ cột
                Color = System.Drawing.Color.Gray // Màu mặc định
            };

            for (int month = 1; month <= 12; month++)
            {
                series.Points.AddXY(month, monthlySales[month - 1]); // Thêm điểm dữ liệu

                // Đặt màu cho tháng có doanh số cao nhất
                if (month - 1 == maxMonthIndex)
                {
                    series.Points[month - 1].Color = System.Drawing.Color.Red; // Màu cho tháng có doanh thu cao nhất
                }
            }

            chartHoaDon.Series.Add(series); // Thêm Series mới

            // Tùy chỉnh trục X và Y
            chartHoaDon.ChartAreas[0].AxisX.Title = "Tháng";
            chartHoaDon.ChartAreas[0].AxisY.Title = "Doanh số (VNĐ)";
            chartHoaDon.ChartAreas[0].AxisX.Interval = 1; // Hiển thị từng tháng

            // Thiết lập các nhãn cho trục X
            for (int month = 1; month <= 12; month++)
            {
                chartHoaDon.ChartAreas[0].AxisX.CustomLabels.Add(month - 1, month, month.ToString());
            }
        }

        private void frmBieuDo_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
