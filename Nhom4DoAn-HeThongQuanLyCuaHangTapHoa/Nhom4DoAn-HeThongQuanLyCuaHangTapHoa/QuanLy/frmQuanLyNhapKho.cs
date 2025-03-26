using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nhom4DoAn_HeThongQuanLyCuaHangTapHoa.QuanLy
{
    public partial class frmQuanLyNhapKho : Form
    {
        // Khai báo
        quanlycuahangtaphoaEntities NK = new quanlycuahangtaphoaEntities();
        private bool isAdding = false;

        public frmQuanLyNhapKho()
        {
            InitializeComponent();
        }
        private void ResetValues()
        {
            txtSoLuong_QLNK.Text = "";
            cboNhaCungCap_QLNK.SelectedIndex = -1;
            cboMatHang_QLNK.SelectedIndex = -1;

            btnLuu_QLNK.Enabled = false;
            btnHuy_QLNK.Enabled = false;

            btnThem_QLNK.Enabled = true;
            btnImport_QLNK.Enabled = true;
            btnExport_QLNK.Enabled = true;

            // Xóa thông báo chế độ thêm nếu có
            lblThem_QLNK.Text = "";
        }
        void LoadData()
        {
            var result = from c in NK.StockIns
                         select new
                         {
                             IDNCC = c.supplierID,
                             IDSP = c.productID,
                             soluong = c.quantity,
                             createAt = c.createdAt
                         };
            dgvNhapKho_QLNK.DataSource = result.ToList();
            dgvNhapKho_QLNK.Columns[0].HeaderText = "Mã nhà cung cấp";
            dgvNhapKho_QLNK.Columns[1].HeaderText = "Mã hàng";
            dgvNhapKho_QLNK.Columns[2].HeaderText = "Số lượng";
            dgvNhapKho_QLNK.Columns[3].HeaderText = "Ngày nhập";

            txtSoLuong_QLNK.Clear();

            ResetValues();

            dgvNhapKho_QLNK.CellClick += new DataGridViewCellEventHandler(dgvNhapKho_QLNK_CellContentClick);
        }
        void addStockIn()
        {
            try
            {
                int idSpThem = Convert.ToInt32(cboMatHang_QLNK.SelectedValue);
                int idNCCThem = Convert.ToInt32(cboNhaCungCap_QLNK.SelectedValue);
                StockIn stThem = NK.StockIns.Find(idSpThem, idNCCThem);
                if (stThem == null)
                {
                    StockIn st = new StockIn()
                    {
                        productID = idSpThem,
                        supplierID = Convert.ToInt32(cboNhaCungCap_QLNK.SelectedValue),
                        quantity = Convert.ToInt32(txtSoLuong_QLNK.Text),
                        createdAt = DateTime.Now,
                    };
                    NK.StockIns.Add(st);
                }
                else
                {
                    stThem.quantity += Convert.ToInt32(txtSoLuong_QLNK.Text);
                }
                Product spThem = NK.Products.Find(idSpThem);
                spThem.stockOnHand += Convert.ToInt32(txtSoLuong_QLNK.Text);
                NK.SaveChanges();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi: " + ex.Message, "Cảnh báo");
            }

        }

        private void btnThem_QLNK_Click(object sender, EventArgs e)
        {
            // Đặt chế độ thêm
            isAdding = true;

            // Set label thông báo chế độ thêm
            lblThem_QLNK.ForeColor = System.Drawing.Color.Red;
            lblThem_QLNK.Text = "*Bạn đang ở chế độ thêm";

            // Vô hiệu hóa các nút khác
            btnThem_QLNK.Enabled = false;
            btnImport_QLNK.Enabled = false;
            btnExport_QLNK.Enabled = false;


            // Bật nút "Lưu"
            btnLuu_QLNK.Enabled = true;
            // Bật nút "Hủy"
            btnHuy_QLNK.Enabled = true;

            // Xóa trắng các ô nhập liệu
            txtSoLuong_QLNK.Text = "";
        }

        private void btnLuu_QLNK_Click(object sender, EventArgs e)
        {
            int a;
            if (string.IsNullOrWhiteSpace(txtSoLuong_QLNK.Text))
            {
                MessageBox.Show("Số lượng sản phẩm không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!int.TryParse(txtSoLuong_QLNK.Text, out a))
            {
                MessageBox.Show("Số lương phải là số", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (a <= 0)
            {
                MessageBox.Show("Số lượng phải lớn hơn 0", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                addStockIn();
        }

        private void btnHuy_QLNK_Click(object sender, EventArgs e)
        {
            ResetValues();
            // Reset trạng thái thêm
            isAdding = false;

            // nếu đang ở chế độ thêm
            if (btnThem_QLNK.Enabled == false)
                lblThem_QLNK.Text = "";
            btnThem_QLNK.Enabled = true;
        }

        private void btnHuy_QLNK_Click_1(object sender, EventArgs e)
        {
            ResetValues();
            // Reset trạng thái thêm
            isAdding = false;

            // nếu đang ở chế độ thêm
            if (btnThem_QLNK.Enabled == false)
                lblThem_QLNK.Text = "";
            btnThem_QLNK.Enabled = true;
        }

        private void btnImport_QLNK_Click(object sender, EventArgs e)
        {
            _Application importApp;
            _Workbook importWorkbook;
            _Worksheet importWorksheet;
            Range importRange;

            try
            {
                importApp = new Microsoft.Office.Interop.Excel.Application();
                OpenFileDialog importOpenFileDialog = new OpenFileDialog();
                importOpenFileDialog.Title = "Import Excel file to data";
                importOpenFileDialog.Filter = "Choose Excel File | *.xlsx; *.xls; *.xlm";
                if (importOpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    importWorkbook = importApp.Workbooks.Open(importOpenFileDialog.FileName);
                    importWorksheet = importWorkbook.ActiveSheet;
                    importRange = importWorksheet.UsedRange;

                    //Bắt đầu nhập từ hàng thứ hai. Vì hàng đầu tiên là tiêu đề cột
                    for (int excelWorkSheetRowIndex = 2; excelWorkSheetRowIndex < importRange.Rows.Count + 1; excelWorkSheetRowIndex++)
                    {
                        if (Convert.ToString(importWorksheet.Cells[excelWorkSheetRowIndex, 1].Value) == null)
                            break;
                        int idNCC = Convert.ToInt32(importWorksheet.Cells[excelWorkSheetRowIndex, 1].Value);
                        int idSP = Convert.ToInt32(importWorksheet.Cells[excelWorkSheetRowIndex, 2].Value);
                        int soLuong = Convert.ToInt32(importWorksheet.Cells[excelWorkSheetRowIndex, 3].Value);

                        Product spThem = NK.Products.Where(q => q.productID == idSP).SingleOrDefault();
                        Supplier nccThem = NK.Suppliers.Where(q => q.supplierID == idNCC).SingleOrDefault();
                        if (spThem == null)
                        {
                            MessageBox.Show("Lỗi dữ liệu sản phẩm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                        if (nccThem == null)
                        {
                            MessageBox.Show("Lỗi dữ liệu nhà cung cấp!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }

                        try
                        {
                            StockIn stThem = NK.StockIns.Find(idSP, idNCC);
                            if (stThem == null)
                            {
                                // Nếu chưa có bản ghi, tạo mới
                                StockIn st = new StockIn()
                                {
                                    productID = idSP,
                                    supplierID = idNCC,
                                    quantity = soLuong,
                                    createdAt = DateTime.Now, // Gán ngày nhập là hiện tại
                                };
                                NK.StockIns.Add(st);
                            }
                            else
                            {
                                // Nếu đã tồn tại bản ghi, cộng thêm số lượng
                                stThem.quantity += soLuong;
                                // Cập nhật ngày nhập mới nếu cần
                                stThem.createdAt = DateTime.Now; // Hoặc giữ nguyên nếu không muốn thay đổi
                            }
                            NK.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Có lỗi: " + ex.Message, "Cảnh báo");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Cảnh báo");
            }
            LoadData();
        }

        private void btnExport_QLNK_Click(object sender, EventArgs e)
        {
            if (dgvNhapKho_QLNK.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                excelApp.Application.Workbooks.Add(Type.Missing);
                for (int i = 1; i < dgvNhapKho_QLNK.Columns.Count + 1; i++)
                {
                    excelApp.Cells[1, i] = dgvNhapKho_QLNK.Columns[i - 1].HeaderText;
                }
                for (int i = 0; i < dgvNhapKho_QLNK.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvNhapKho_QLNK.Columns.Count; j++)
                    {

                        excelApp.Cells[i + 2, j + 1] = dgvNhapKho_QLNK.Rows[i].Cells[j].Value.ToString();
                    }

                }
                excelApp.Columns.AutoFit();
                excelApp.Visible = true;
            }
        }

        private void frmQuanLyNhapKho_Load(object sender, EventArgs e)
        {
            cboNhaCungCap_QLNK.SelectedIndex = -1;
            cboMatHang_QLNK.SelectedIndex = -1;

            LoadData();
            ResetValues();

            var resultNCC = from c in NK.Suppliers select c;
            cboNhaCungCap_QLNK.DataSource = resultNCC.ToList();
            cboNhaCungCap_QLNK.DisplayMember = "name";
            cboNhaCungCap_QLNK.ValueMember = "supplierID";
            var resultSP = from c in NK.Products select c;
            cboMatHang_QLNK.DataSource = resultSP.ToList();
            cboMatHang_QLNK.DisplayMember = "name";
            cboMatHang_QLNK.ValueMember = "productID";
        }

        private void dgvNhapKho_QLNK_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu đang ở chế độ thêm
            if (isAdding)
            {
                MessageBox.Show("Bạn không thể chọn danh mục trong khi đang ở chế độ thêm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int d = e.RowIndex;

            // Lấy dữ liệu từ DataGridView và hiển thị lên các TextBox
            cboMatHang_QLNK.SelectedValue = Convert.ToInt32(dgvNhapKho_QLNK.Rows[d].Cells[1].Value.ToString());
            cboNhaCungCap_QLNK.SelectedValue = Convert.ToInt32(dgvNhapKho_QLNK.Rows[d].Cells[0].Value.ToString());
            txtSoLuong_QLNK.Text = dgvNhapKho_QLNK.Rows[d].Cells[2].Value.ToString();
        }
    }
}
