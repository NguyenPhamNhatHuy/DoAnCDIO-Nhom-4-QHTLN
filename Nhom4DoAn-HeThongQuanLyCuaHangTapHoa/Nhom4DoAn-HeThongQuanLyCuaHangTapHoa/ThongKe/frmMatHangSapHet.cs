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
    public partial class frmMatHangSapHet : Form
    {
        quanlycuahangtaphoaEntities MHSH = new quanlycuahangtaphoaEntities();

        public frmMatHangSapHet()
        {
            InitializeComponent();
        }

        private void MatHangSapHet_Load(object sender, EventArgs e)
        {
            LoadLowStockItems();
            dgvMHSH.CellClick += new DataGridViewCellEventHandler(dgvMHSH_CellContentClick);
            txtLoaiHang_MHSH.Text = "";
            txtSoLuongConLai_MHSH.Text = "";
            txtTenSanPham_MHSH.Text = "";
        }
        private void LoadLowStockItems()
        {
            try
            {
                // Định nghĩa "số lượng hàng tồn kho thấp" có nghĩa là gì (ví dụ: ít hơn 10 mặt hàng)
                const int LOW_STOCK_THRESHOLD = 10;

                // Lấy sản phẩm còn ít hàng
                var lowStockItems = MHSH.Products
                    .Where(p => p.stockOnHand < LOW_STOCK_THRESHOLD)
                    .Select(p => new
                    {
                        ProductID = p.productID,
                        CategoryName = p.Category.name,
                        ProductName = p.name,
                        StockOnHand = p.stockOnHand
                    })
                    .OrderBy(p => p.StockOnHand)
                    .ToList();

                // Liên kết với DataGridView
                dgvMHSH.DataSource = lowStockItems;

                // Định dạng DataGridView
                FormatDataGridView();

                // Chọn hàng đầu tiên nếu có dữ liệu
                if (lowStockItems.Count > 0 && dgvMHSH.Rows.Count > 0)
                {
                    dgvMHSH.Rows[0].Selected = true;
                    UpdateSelectedItemDetails();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách mặt hàng sắp hết: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FormatDataGridView()
        {
            // Đặt tiêu đề cho các cột
            dgvMHSH.Columns["ProductID"].HeaderText = "Mã sản phẩm";
            dgvMHSH.Columns["CategoryName"].HeaderText = "Loại hàng";
            dgvMHSH.Columns["ProductName"].HeaderText = "Tên sản phẩm";
            dgvMHSH.Columns["StockOnHand"].HeaderText = "Số lượng còn lại";

            // Định dạng màu sắc cho số lượng thấp
            foreach (DataGridViewRow row in dgvMHSH.Rows)
            {
                int stockValue = Convert.ToInt32(row.Cells["StockOnHand"].Value);
                if (stockValue <= 5)
                {
                    row.Cells["StockOnHand"].Style.BackColor = Color.LightPink;
                }
            }
        }
        private void UpdateSelectedItemDetails()
        {
            if (dgvMHSH.SelectedRows.Count > 0)
            {
                // Lấy hàng đã chọn
                DataGridViewRow row = dgvMHSH.SelectedRows[0];

                // Hiển thị thông tin chi tiết trong các text box
                txtLoaiHang_MHSH.Text = row.Cells["CategoryName"].Value.ToString();
                txtTenSanPham_MHSH.Text = row.Cells["ProductName"].Value.ToString();
                txtSoLuongConLai_MHSH.Text = row.Cells["StockOnHand"].Value.ToString();
            }
        }

        private void dgvMHSH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dgvMHSH.Rows[e.RowIndex].Selected = true;
                UpdateSelectedItemDetails();
            }

        }
        private void dgvMHSH_SelectionChanged(object sender, EventArgs e)
        {
            UpdateSelectedItemDetails();
        }
    }
}