using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Nhom4DoAn_HeThongQuanLyCuaHangTapHoa.QuanLy
{
    public partial class frmQuanLySanPham : Form
    {
        quanlycuahangtaphoaEntities SP = new quanlycuahangtaphoaEntities();
        private bool isAdding = false;
        public frmQuanLySanPham()
        {
            InitializeComponent();
        }
        // reset giá trị cho các mục 
        private void ResetValues_SP()
        {
            cboLoaiHang_QLSP.SelectedIndex = -1;
            txtTenSanPham_QLSP.Text = "";
            txtSoLuong_QLSP.Text = "";
            txtDonGia_QLSP.Text = "";
            txtLinkAnh_QLSP.Text = "";
            picAnh_QLSP.Image = null;

            btnLuu_QLSP.Enabled = false;
            btnHuy_QLSP.Enabled = false;
            btnCapNhat_QLSP.Enabled = false;
            btnXoa_QLSP.Enabled = false;

            btnThem_QLSP.Enabled = true;
            btnImport_QLSP.Enabled = true;
            btnExport_QLSP.Enabled = true;
            btnTimKiem_QLSP.Enabled = true;

            // Xóa thông báo chế độ thêm nếu có
            lblThem_QLSP.Text = "";
        }

        void loadData()
        {
            var result = from c in SP.Products select new {
                ID = c.productID,
                tensp = c.name,
                loai = c.Category.name,
                soluong = c.stockOnHand,
                dongia = c.price,
                hinhanh = c.imagePath
            };

            dgvSanPham_QLSP.DataSource = result.ToList();
            dgvSanPham_QLSP.Columns[0].HeaderText = "ID";
            dgvSanPham_QLSP.Columns[1].HeaderText = "Tên Sản Phẩm";
            dgvSanPham_QLSP.Columns[2].HeaderText = "Loại Hàng";
            dgvSanPham_QLSP.Columns[3].HeaderText = "Số Lượng";
            dgvSanPham_QLSP.Columns[4].HeaderText = "Đơn Giá";
            dgvSanPham_QLSP.Columns[4].DefaultCellStyle.Format = "#,### vnđ";
            dgvSanPham_QLSP.Columns[5].HeaderText = "Hình Ảnh";

            foreach (DataGridViewRow row in dgvSanPham_QLSP.Rows)
            {
                try
                {
                    var cellValue = row.Cells["hinhanh"].Value;
                    string imagePath = cellValue != null ? cellValue.ToString() : string.Empty;
                    if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
                    {
                        row.Cells["HinhAnh"].Value = Image.FromFile(imagePath);
                    }
                    else
                    {
                        row.Cells["HinhAnh"].Value = null;
                    }
                }
                catch
                {
                    row.Cells["HinhAnh"].Value = null;
                }
            }

            txtTenSanPham_QLSP.Clear();
            txtSoLuong_QLSP.Clear();
            txtDonGia_QLSP.Clear();
            txtTenSanPham_QLSP.Focus();

            ResetValues_SP();
        }

        private void btnThem_QLDM_Click(object sender, EventArgs e)
        {
            // Đặt chế độ thêm
            isAdding = true;

            // Set label thông báo chế độ thêm
            lblThem_QLSP.ForeColor = System.Drawing.Color.Red;
            lblThem_QLSP.Text = "*Bạn đang ở chế độ thêm";

            // Vô hiệu hóa các nút khác
            btnThem_QLSP.Enabled = false;
            btnCapNhat_QLSP.Enabled = false;
            btnXoa_QLSP.Enabled = false;
            btnImport_QLSP.Enabled = false;
            btnExport_QLSP.Enabled = false;
            btnTimKiem_QLSP.Enabled = false;

            // Bật nút "Lưu"
            btnLuu_QLSP.Enabled = true;
            // Bật nút "Hủy"
            btnHuy_QLSP.Enabled = true;

            // Xóa trắng các ô nhập liệu
            txtTenSanPham_QLSP.Text = "";
            txtSoLuong_QLSP.Text = "";
            txtDonGia_QLSP.Text = "";
            cboLoaiHang_QLSP.SelectedIndex = -1;

            // Đặt focus vào ô tên sản phẩm
            txtTenSanPham_QLSP.Focus();
        }

        private void btnLuu_QLSP_Click(object sender, EventArgs e)
        {
            int a;
            try
            {
                // Kiểm tra tên sản phẩm
                if (string.IsNullOrWhiteSpace(txtTenSanPham_QLSP.Text))
                {
                    MessageBox.Show("Tên sản phẩm không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra số lượng sản phẩm
                if (string.IsNullOrWhiteSpace(txtSoLuong_QLSP.Text))
                {
                    MessageBox.Show("Số lượng sản phẩm không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!int.TryParse(txtSoLuong_QLSP.Text, out a))
                {
                    MessageBox.Show("Số lượng phải là số", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (a <= 0)
                {
                    MessageBox.Show("Số lượng phải lớn hơn 0", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra đơn giá sản phẩm
                if (string.IsNullOrWhiteSpace(txtDonGia_QLSP.Text))
                {
                    MessageBox.Show("Đơn giá sản phẩm không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!int.TryParse(txtDonGia_QLSP.Text, out a))
                {
                    MessageBox.Show("Đơn giá phải là số", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (a <= 0)
                {
                    MessageBox.Show("Đơn giá phải lớn hơn 0", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tìm sản phẩm theo tên
                Product spThem = SP.Products.SingleOrDefault(q => q.name.Equals(txtTenSanPham_QLSP.Text));
                if (spThem != null)
                {
                    // Cập nhật thông tin sản phẩm nếu đã tồn tại
                    spThem.stockOnHand += Convert.ToInt32(txtSoLuong_QLSP.Text);
                    spThem.price = long.Parse(txtDonGia_QLSP.Text);
                    spThem.categoryID = Convert.ToInt32(cboLoaiHang_QLSP.SelectedValue);
                    spThem.imagePath = txtLinkAnh_QLSP.Text;
                }
                else
                {
                    // Thêm mới sản phẩm nếu chưa tồn tại
                    Product sp = new Product()
                    {
                        name = txtTenSanPham_QLSP.Text,
                        stockOnHand = Convert.ToInt32(txtSoLuong_QLSP.Text),
                        price = long.Parse(txtDonGia_QLSP.Text),
                        categoryID = Convert.ToInt32(cboLoaiHang_QLSP.SelectedValue),
                        imagePath = txtLinkAnh_QLSP.Text
                    };
                    SP.Products.Add(sp);
                }

                // Lưu thay đổi và thông báo thành công
                SP.SaveChanges();
                loadData();
                ResetValues_SP();
                MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi: " + ex.Message, "Cảnh báo");
            }
        }

        private void btnHuy_QLSP_Click(object sender, EventArgs e)
        {
            ResetValues_SP();
            // Reset trạng thái thêm
            isAdding = false;

            // nếu đang ở chế độ thêm
            if (btnThem_QLSP.Enabled == false)
                lblThem_QLSP.Text = "";
            btnThem_QLSP.Enabled = true;
            // Đảm bảo nút Cập Nhật bị tắt
            btnCapNhat_QLSP.Enabled = false;
        }

        private void btnCapNhat_QLDM_Click(object sender, EventArgs e)
        {
            int a;
            try
            {
                // Kiểm tra tên sản phẩm
                if (string.IsNullOrWhiteSpace(txtTenSanPham_QLSP.Text))
                {
                    MessageBox.Show("Tên sản phẩm không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra số lượng sản phẩm
                if (string.IsNullOrWhiteSpace(txtSoLuong_QLSP.Text))
                {
                    MessageBox.Show("Số lượng sản phẩm không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!int.TryParse(txtSoLuong_QLSP.Text, out a))
                {
                    MessageBox.Show("Số lượng phải là số", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (a <= 0)
                {
                    MessageBox.Show("Số lượng phải lớn hơn 0", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra đơn giá sản phẩm
                if (string.IsNullOrWhiteSpace(txtDonGia_QLSP.Text))
                {
                    MessageBox.Show("Đơn giá sản phẩm không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!int.TryParse(txtDonGia_QLSP.Text, out a))
                {
                    MessageBox.Show("Đơn giá phải là số", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (a <= 0)
                {
                    MessageBox.Show("Đơn giá phải lớn hơn 0", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Thực hiện cập nhật sản phẩm
                int idSpSua = Convert.ToInt32(dgvSanPham_QLSP.SelectedCells[0].OwningRow.Cells["ID"].Value.ToString());
                Product spCapNhat = SP.Products.SingleOrDefault(q => q.productID == idSpSua);
                if (spCapNhat != null)
                {
                    spCapNhat.name = txtTenSanPham_QLSP.Text;
                    spCapNhat.stockOnHand = Convert.ToInt32(txtSoLuong_QLSP.Text);
                    spCapNhat.price = long.Parse(txtDonGia_QLSP.Text);
                    spCapNhat.categoryID = Convert.ToInt32(cboLoaiHang_QLSP.SelectedValue);
                    spCapNhat.imagePath = txtLinkAnh_QLSP.Text; // Cập nhật đường dẫn ảnh
                    SP.SaveChanges();
                    MessageBox.Show("Cập nhật sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadData();
                    ResetValues_SP();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sản phẩm cần cập nhật", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi: " + ex.Message, "Cảnh báo");
            }
        }

        private void btnXoa_QLDM_Click(object sender, EventArgs e)
        {
            try
            {
                // Hiển thị hộp thoại xác nhận xóa
                DialogResult re = MessageBox.Show("Bạn có chắc chắn muốn xoá!", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (re == DialogResult.Yes)
                {
                    // Kiểm tra xem có chọn sản phẩm hay không
                    if (dgvSanPham_QLSP.SelectedCells.Count > 0)
                    {
                        int idSpXoa = Convert.ToInt32(dgvSanPham_QLSP.SelectedCells[0].OwningRow.Cells["ID"].Value);

                        // Lấy đối tượng sản phẩm cần xóa
                        Product spXoa = SP.Products.SingleOrDefault(q => q.productID == idSpXoa);
                        if (spXoa != null)
                        {
                            // Lấy danh sách các bản ghi liên quan trong bảng StockIns
                            List<StockIn> stXoa = SP.StockIns.Where(q => q.productID == idSpXoa).ToList();

                            // Xóa các bản ghi trong StockIns nếu tồn tại
                            if (stXoa.Any())
                            {
                                SP.StockIns.RemoveRange(stXoa);
                            }

                            // Xóa sản phẩm trong bảng Products
                            SP.Products.Remove(spXoa);
                            SP.SaveChanges();

                            // Thông báo thành công
                            MessageBox.Show("Xóa sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy sản phẩm để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng chọn sản phẩm để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    // Tải lại dữ liệu sau khi xóa
                    loadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi: " + ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmQuanLySanPham_Load(object sender, EventArgs e)
        {
            loadData();
            ResetValues_SP();

            try
            {
          
                var resultLoai = (from c in SP.Categories select c).ToList();

                Category allCategory = new Category { categoryID = 0, name = "All" };
                resultLoai.Insert(0, allCategory);

                // Đổ dữ liệu vào ComboBox
                cboLoaiHang_QLSP.DataSource = resultLoai;
                cboLoaiHang_QLSP.DisplayMember = "name";
                cboLoaiHang_QLSP.ValueMember = "categoryID";

                // Đặt mặc định chọn "All"
                cboLoaiHang_QLSP.SelectedIndex = 0;

                // Hiển thị tất cả sản phẩm lúc load form
                timkiemsanpham();

                // Đăng ký sự kiện CellClick cho DataGridView
                dgvSanPham_QLSP.CellClick += new DataGridViewCellEventHandler(dgvSanPham_QLSP_CellContentClick);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải loại hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExport_QLDM_Click(object sender, EventArgs e)
        {
            if (dgvSanPham_QLSP.Rows.Count > 0)
            {
                try
                {
                    // Khởi tạo Excel
                    Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                    excelApp.Application.Workbooks.Add(Type.Missing);

                    // Xuất tiêu đề cột
                    for (int i = 1; i <= dgvSanPham_QLSP.Columns.Count; i++)
                    {
                        excelApp.Cells[1, i] = dgvSanPham_QLSP.Columns[i - 1].HeaderText;
                    }

                    // Xuất dữ liệu từ DataGridView
                    for (int i = 0; i < dgvSanPham_QLSP.Rows.Count; i++)
                    {
                        for (int j = 0; j < dgvSanPham_QLSP.Columns.Count; j++)
                        {
                            var cellValue = dgvSanPham_QLSP.Rows[i].Cells[j].Value;
                            excelApp.Cells[i + 2, j + 1] = cellValue != null ? cellValue.ToString() : string.Empty;
                        }
                    }

                    // Tự động căn chỉnh độ rộng cột
                    excelApp.Columns.AutoFit();
                    excelApp.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xuất dữ liệu thất bại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvSanPham_QLSP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int d = e.RowIndex;
            try
            {
                // Kiểm tra nếu đang ở chế độ thêm
                if (isAdding)
                {
                    MessageBox.Show("Bạn không thể chọn khi đang ở chế độ thêm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Lấy dữ liệu từ DataGridView và hiển thị lên các TextBox
                txtTenSanPham_QLSP.Text = dgvSanPham_QLSP.Rows[d].Cells["tensp"].Value?.ToString();
                cboLoaiHang_QLSP.Text = dgvSanPham_QLSP.Rows[d].Cells["loai"].Value?.ToString();
                txtSoLuong_QLSP.Text = dgvSanPham_QLSP.Rows[d].Cells["soluong"].Value?.ToString();
                txtDonGia_QLSP.Text = dgvSanPham_QLSP.Rows[d].Cells["dongia"].Value?.ToString();

                // Lấy đường dẫn ảnh
                string imagePath = dgvSanPham_QLSP.Rows[d].Cells["hinhanh"].Value?.ToString();
                txtLinkAnh_QLSP.Text = imagePath;

                // Hiển thị ảnh từ đường dẫn
                try
                {
                    if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
                    {
                        picAnh_QLSP.Image = Image.FromFile(imagePath);
                    }
                    else
                    {
                        picAnh_QLSP.Image = null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Load ảnh thất bại: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    picAnh_QLSP.Image = null;
                }

                // Lấy ID sản phẩm để sử dụng cho thao tác cập nhật hoặc xóa
                int productID = Convert.ToInt32(dgvSanPham_QLSP.Rows[d].Cells["ID"].Value);

                // Lấy thông tin category từ datagridview
                string categoryName = dgvSanPham_QLSP.Rows[d].Cells["loai"].Value.ToString();

                // Tìm categoryID từ tên category
                var category = SP.Categories.FirstOrDefault(c => c.name == categoryName);
                if (category != null)
                {
                    cboLoaiHang_QLSP.SelectedValue = category.categoryID;
                }

                // Kích hoạt các nút chức năng
                btnCapNhat_QLSP.Enabled = true;
                btnXoa_QLSP.Enabled = true;
                btnHuy_QLSP.Enabled = true; 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi hiển thị dữ liệu: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnChonAnh_QLSP_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                ofd.Title = "Chọn hình minh họa";  // Thêm tiêu đề
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtLinkAnh_QLSP.Text = ofd.FileName;
                    picAnh_QLSP.Image = Image.FromFile(ofd.FileName); // Hiển thị ảnh trong PictureBox
                }
            }
        }

        void timkiemsanpham()
        {
            try
            {
                // Nếu chưa chọn loại hàng, hiển thị toàn bộ sản phẩm
                if (cboLoaiHang_QLSP.SelectedIndex == -1 || cboLoaiHang_QLSP.Text == "All")
                {
                    var allProducts = from c in SP.Products
                                      select new
                                      {
                                          ID = c.productID,
                                          tensp = c.name,
                                          loai = c.Category.name,
                                          soluong = c.stockOnHand,
                                          dongia = c.price,
                                          hinhanh = c.imagePath
                                      };

                    dgvSanPham_QLSP.DataSource = allProducts.ToList();
                }
                else
                {
                    // Nếu đã chọn loại hàng, hiển thị theo loại đã chọn
                    string selectedCategory = cboLoaiHang_QLSP.Text.Trim();

                    var result = from c in SP.Products
                                 where c.Category.name.Equals(selectedCategory)
                                 select new
                                 {
                                     ID = c.productID,
                                     tensp = c.name,
                                     loai = c.Category.name,
                                     soluong = c.stockOnHand,
                                     dongia = c.price,
                                     hinhanh = c.imagePath
                                 };

                    dgvSanPham_QLSP.DataSource = result.ToList();
                }

                // Thiết lập tiêu đề cột
                dgvSanPham_QLSP.Columns[0].HeaderText = "ID";
                dgvSanPham_QLSP.Columns[1].HeaderText = "Tên Hàng";
                dgvSanPham_QLSP.Columns[2].HeaderText = "Loại Hàng";
                dgvSanPham_QLSP.Columns[3].HeaderText = "Số Lượng";
                dgvSanPham_QLSP.Columns[4].HeaderText = "Đơn Giá";
                dgvSanPham_QLSP.Columns[5].HeaderText = "Hình Ảnh";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi trong quá trình tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTimKiem_QLSP_Click(object sender, EventArgs e)
        {
            timkiemsanpham();
        }

        private void cboLoaiHang_QLSP_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnImport_QLSP_Click(object sender, EventArgs e)
        {
            _Application importApp = null;
            _Workbook importWorkbook = null;
            _Worksheet importWorksheet = null;
            Range importRange;
            int countAdded = 0; // Chỉ đếm số sản phẩm được thêm mới

            try
            {
                importApp = new Microsoft.Office.Interop.Excel.Application();
                OpenFileDialog importOpenFileDialog = new OpenFileDialog
                {
                    Title = "Import Excel file to data",
                    Filter = "Choose Excel File | *.xlsx; *.xls; *.xlm"
                };

                if (importOpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    importWorkbook = importApp.Workbooks.Open(importOpenFileDialog.FileName);
                    importWorksheet = importWorkbook.ActiveSheet;
                    importRange = importWorksheet.UsedRange;

                    for (int excelWorkSheetRowIndex = 2; excelWorkSheetRowIndex <= importRange.Rows.Count; excelWorkSheetRowIndex++)
                    {
                        if (importWorksheet.Cells[excelWorkSheetRowIndex, 1].Value == null)
                            break;

                        string tenSP = Convert.ToString(importWorksheet.Cells[excelWorkSheetRowIndex, 1].Value);
                        string loaiSP = Convert.ToString(importWorksheet.Cells[excelWorkSheetRowIndex, 2].Value);
                        Category loaiThem = SP.Categories.SingleOrDefault(q => q.name.Equals(loaiSP));

                        if (loaiThem == null)
                        {
                            MessageBox.Show("Lỗi dữ liệu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }

                        try
                        {
                            // Kiểm tra nếu sản phẩm đã tồn tại thì bỏ qua
                            Product spTonTai = SP.Products.SingleOrDefault(q => q.name.Equals(tenSP));
                            if (spTonTai == null)
                            {
                                // Tạo sản phẩm mới
                                Product sp = new Product
                                {
                                    name = tenSP,
                                    stockOnHand = Convert.ToInt32(importWorksheet.Cells[excelWorkSheetRowIndex, 3].Value),
                                    price = long.Parse(importWorksheet.Cells[excelWorkSheetRowIndex, 4].Value.ToString()),
                                    categoryID = loaiThem.categoryID
                                };
                                SP.Products.Add(sp);
                                countAdded++; // Đếm số sản phẩm mới được thêm
                            }

                            SP.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Có lỗi: " + ex.Message, "Cảnh báo");
                        }
                    }

                    // Hiển thị thông báo khi hoàn tất
                    MessageBox.Show($"Đã thêm {countAdded} sản phẩm mới.",
                                    "Nhập sản phẩm thành công",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Cảnh báo");
            }
            finally
            {
                // Giải phóng tài nguyên
                if (importWorkbook != null) Marshal.ReleaseComObject(importWorkbook);
                if (importApp != null) importApp.Quit();
                if (importApp != null) Marshal.ReleaseComObject(importApp);
            }

            loadData();
        }
    }
}


