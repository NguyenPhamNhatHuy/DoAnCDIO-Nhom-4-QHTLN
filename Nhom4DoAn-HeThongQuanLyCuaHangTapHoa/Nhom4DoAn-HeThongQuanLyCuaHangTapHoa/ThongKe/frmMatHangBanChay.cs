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
    public partial class frmMatHangBanChay : Form
    {
        quanlycuahangtaphoaEntities MHBC = new quanlycuahangtaphoaEntities();

        // Tạo lớp để lưu thông tin tháng/năm
        public class MonthYearItem
        {
            public int Month { get; set; }
            public int Year { get; set; }
            public string DisplayText { get; set; }

            public override string ToString()
            {
                return DisplayText;
            }
        }

        // Tạo lớp để lưu thông tin sản phẩm bán chạy
        public class BestSellingProduct
        {
            public int ProductID { get; set; }
            public string CategoryName { get; set; }
            public string ProductName { get; set; }
            public int QuantitySold { get; set; }
        }

        public frmMatHangBanChay()
        {
            InitializeComponent();
            this.dtpThangban_MHBC.ValueChanged += new System.EventHandler(this.dtpThangban_MHBC_ValueChanged);
          
        }

        private void MatHangBanChay_Load(object sender, EventArgs e)
        {
            try
            {
                InitializeDateTimePicker();
                LoadBestSellingProducts(null, null);
                dgvMHBC.CellClick += new DataGridViewCellEventHandler(dgvMHBC_CellContentClick);

                // Đặt giá trị rỗng cho các ô nhập liệu
                txtLoaiHang_MHBC.Text = string.Empty;
                txtTenSanPham_MHBC.Text = string.Empty;
                txtSoLuongBan_MHBC.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi khởi tạo form: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadBestSellingProducts(int? month = null, int? year = null)
        {
            try
            {
                // Tải về tất cả dữ liệu cần thiết (thực thi các truy vấn Entity Framework)
                var Orders = MHBC.Orders.Where(o => o.createdAt.HasValue).ToList();
                var OrderDetails = MHBC.OrderDetails.ToList();
                var Products = MHBC.Products.ToList();
                var Categories = MHBC.Categories.ToList();

                // Bây giờ xử lý dữ liệu bằng LINQ to Objects
                var filteredOrders = Orders;

                // Lọc theo tháng và năm nếu được chỉ định
                if (month.HasValue && year.HasValue)
                {
                    filteredOrders = Orders
                        .Where(o => o.createdAt.Value.Month == month.Value &&
                               o.createdAt.Value.Year == year.Value)
                        .ToList();
                }

                // Lấy danh sách ID đơn hàng đã lọc
                var orderIds = filteredOrders.Select(o => o.orderID).ToList();

                // Lấy chi tiết đơn hàng thuộc các đơn hàng đã lọc
                var relevantOrderDetails = OrderDetails
                    .Where(od => orderIds.Contains(od.orderID))
                    .ToList();

                // Nhóm và tính toán số lượng bán cho mỗi sản phẩm
                var bestSellingProducts = relevantOrderDetails
                    .Join(Products,
                        od => od.productID,
                        p => p.productID,
                        (od, p) => new { OrderDetail = od, Product = p })
                    .Join(Categories,
                        j => j.Product.categoryID,
                        c => c.categoryID,
                        (j, c) => new { j.OrderDetail, j.Product, Category = c })
                    .GroupBy(j => new { ProductID = j.Product.productID, ProductName = j.Product.name, CategoryName = j.Category.name })
                    .Select(g => new BestSellingProduct
                    {
                        ProductID = g.Key.ProductID,
                        ProductName = g.Key.ProductName,
                        CategoryName = g.Key.CategoryName,
                        QuantitySold = g.Sum(j => j.OrderDetail.quantity ?? 0)
                    })
                    .OrderByDescending(p => p.QuantitySold)
                    .ToList();

                    // Cập nhật DataSource và refresh DataGridView
                    dgvMHBC.DataSource = null;
                    dgvMHBC.DataSource = bestSellingProducts;
                    dgvMHBC.Refresh();

                // Định dạng DataGridView
                FormatDataGridView();

                // Chọn hàng đầu tiên nếu có dữ liệu
                if (bestSellingProducts.Count > 0 && dgvMHBC.Rows.Count > 0)
                {
                    dgvMHBC.Rows[0].Selected = true;
                    UpdateSelectedItemDetails();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách mặt hàng bán chạy: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDataGridView()
        {
            // Kiểm tra xem DataGridView có cột và dữ liệu không
            if (dgvMHBC.Columns.Count == 0 || dgvMHBC.Rows.Count == 0)
                return;

            try
            {
                // Đặt tiêu đề cho các cột
                dgvMHBC.Columns["ProductID"].HeaderText = "Mã sản phẩm";
                dgvMHBC.Columns["CategoryName"].HeaderText = "Loại hàng";
                dgvMHBC.Columns["ProductName"].HeaderText = "Tên sản phẩm";
                dgvMHBC.Columns["QuantitySold"].HeaderText = "Số lượng đã bán";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi định dạng DataGridView: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateSelectedItemDetails()
        {
            if (dgvMHBC.SelectedRows.Count > 0)
            {
                try
                {
                    // Lấy hàng đã chọn
                    DataGridViewRow row = dgvMHBC.SelectedRows[0];

                    // Hiển thị thông tin chi tiết trong các text box
                    txtLoaiHang_MHBC.Text = row.Cells["CategoryName"].Value?.ToString() ?? "";
                    txtTenSanPham_MHBC.Text = row.Cells["ProductName"].Value?.ToString() ?? "";
                    txtSoLuongBan_MHBC.Text = row.Cells["QuantitySold"].Value?.ToString() ?? "0";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi hiển thị chi tiết sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void InitializeDateTimePicker()
        {
            dtpThangban_MHBC.Format = DateTimePickerFormat.Custom;
            dtpThangban_MHBC.CustomFormat = "MM/yyyy";
            dtpThangban_MHBC.Value = DateTime.Now;
        }

        private void dtpThangban_MHBC_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                LoadBestSellingProducts(dtpThangban_MHBC.Value.Month, dtpThangban_MHBC.Value.Year);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chọn tháng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            txtLoaiHang_MHBC.Text = "";
            txtTenSanPham_MHBC.Text = "";
            txtSoLuongBan_MHBC.Text = "";
        }

        private void dgvMHBC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dgvMHBC.Rows[e.RowIndex].Selected = true;
                UpdateSelectedItemDetails();
            }
        }

        private void dgvMHBC_SelectionChanged(object sender, EventArgs e)
        {
            UpdateSelectedItemDetails();
        }

      
    }
}