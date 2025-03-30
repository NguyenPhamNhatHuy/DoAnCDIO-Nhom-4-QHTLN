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
    public partial class frmQuanLyNhaCungCap: Form
    {
        quanlycuahangtaphoaEntities NCC = new quanlycuahangtaphoaEntities();
        private bool isAdding = false;

        public frmQuanLyNhaCungCap()
        {
            InitializeComponent();
        }

        private void ResetValues_SP()
        {
           ClearTextBoxes();

            btnCapNhat_QLNCC.Enabled = false;
            btnHuy_QLNCC.Enabled = false;
            btnLuu_QLNCC.Enabled = false;
            btnXoa_QLNCC.Enabled = false;

            btnThem_QLNCC.Enabled = true;
            btnImport_QLNCC.Enabled = true;
            btnExport_QLNCC.Enabled = true;
        }
        private void ClearTextBoxes()
        {
            txtMaNhaCungCap_QLNCC.Text = "";
            txtTenNhaCungCap_QLNCC.Text = "";
            txtDiaChi_QLNCC.Text = "";
       
            // Xóa thông báo chế độ thêm nếu có
            lblThem_QLNCC.Text = "";
        }
        void LoadData()
        {
            dgvCungCap_QLNCC.DataSource = NCC.Suppliers
               .Select(s => new
               {
                   s.supplierID,
                   s.name,
                   s.address,
               }).ToList();
            dgvCungCap_QLNCC.Columns[0].HeaderText = "Mã nhà cung cấp";
            dgvCungCap_QLNCC.Columns[1].HeaderText = "Tên nhà cung cấp";
            dgvCungCap_QLNCC.Columns[2].HeaderText = "Địa chỉ";
            dgvCungCap_QLNCC.CellClick += new DataGridViewCellEventHandler(dgvCungCap_QLNCC_CellContentClick);

            ResetValues_SP();
        }

        private void btnThem_QLNCC_Click(object sender, EventArgs e)
        {

            // Đặt chế độ thêm
            isAdding = true;

            // Set label thông báo chế độ thêm
            lblThem_QLNCC.ForeColor = System.Drawing.Color.Red;
            lblThem_QLNCC.Text = "*Bạn đang ở chế độ thêm";

            // Vô hiệu hóa các nút khác
            btnThem_QLNCC.Enabled = false;
            btnCapNhat_QLNCC.Enabled = false;
            btnXoa_QLNCC.Enabled = false;
            btnImport_QLNCC.Enabled = false;
            btnExport_QLNCC.Enabled = false;

            // Bật nút "Lưu"
            btnLuu_QLNCC.Enabled = true;
            // Bật nút "Hủy"
            btnHuy_QLNCC.Enabled = true;

            // Xóa trắng các ô nhập liệu
            txtMaNhaCungCap_QLNCC.Text = "";
            txtTenNhaCungCap_QLNCC.Text = "";
            txtDiaChi_QLNCC.Text = "";

            // Đặt focus vào ô tên sản phẩm
            txtMaNhaCungCap_QLNCC.Focus();
        }

        private void dgvCungCap_QLNCC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            try
            {
                // Kiểm tra nếu đang ở chế độ thêm
                if (isAdding)
                {
                    MessageBox.Show("Bạn không thể chọn danh mục trong khi đang ở chế độ thêm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                txtMaNhaCungCap_QLNCC.Text = dgvCungCap_QLNCC.Rows[row].Cells[0].Value.ToString();
                txtTenNhaCungCap_QLNCC.Text = dgvCungCap_QLNCC.Rows[row].Cells[1].Value.ToString();
                txtDiaChi_QLNCC.Text = dgvCungCap_QLNCC.Rows[row].Cells[2].Value.ToString();

                // Kích hoạt các nút chức năng
                btnCapNhat_QLNCC.Enabled = true;
                btnXoa_QLNCC.Enabled = true;
                btnHuy_QLNCC.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi hiển thị dữ liệu: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_QLNCC_Click(object sender, EventArgs e)
        {
            string tenNCC = txtTenNhaCungCap_QLNCC.Text;
            string diaChi = txtDiaChi_QLNCC.Text;

            if (string.IsNullOrEmpty(tenNCC) || string.IsNullOrEmpty(diaChi))
            {
                MessageBox.Show("Bạn cần điền đầy đủ thông tin",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    Supplier supplier = new Supplier();
                    supplier.name = tenNCC;
                    supplier.address = diaChi;

                    NCC.Suppliers.Add(supplier);
                    NCC.SaveChanges();
                    txtMaNhaCungCap_QLNCC.Text = supplier.supplierID + "";
                    LoadData();
                    MessageBox.Show("Thêm nhà cung cấp thành công", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception exp)
                {
                    MessageBox.Show("Lỗi: " + exp.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHuy_QLNCC_Click(object sender, EventArgs e)
        {
            ResetValues_SP();
            // Reset trạng thái thêm
            isAdding = false;

            // nếu đang ở chế độ thêm
            if (btnThem_QLNCC.Enabled == false)
                lblThem_QLNCC.Text = "";
            btnThem_QLNCC.Enabled = true;
            // Đảm bảo nút Cập Nhật bị tắt
            btnCapNhat_QLNCC.Enabled = false;
        }

        private void btnCapNhat_QLNCC_Click(object sender, EventArgs e)
        {
            try
            {
                int updateID = int.Parse(txtMaNhaCungCap_QLNCC.Text);
                Supplier supplier = NCC.Suppliers.
                    SingleOrDefault(s => s.supplierID == updateID);

                supplier.name = txtTenNhaCungCap_QLNCC.Text;
                supplier.address = txtDiaChi_QLNCC.Text;
                if (string.IsNullOrEmpty(supplier.name) || string.IsNullOrEmpty(supplier.address))
                {
                    MessageBox.Show("Bạn cần điền đầy đủ thông tin",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                   
                    NCC.SaveChanges();
                    LoadData();
                    ClearTextBoxes();
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Lỗi: " + exp.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_QLNCC_Click(object sender, EventArgs e)
        {
            try
            {
                int removeID = int.Parse(txtMaNhaCungCap_QLNCC.Text);
                Supplier supplier = NCC.Suppliers.
                    SingleOrDefault(s => s.supplierID == removeID);
              NCC.Entry(supplier).State = System.Data.Entity.EntityState.Deleted;
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Cảnh báo",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult.Equals(DialogResult.Yes))
                {
                    NCC.SaveChanges();
                    ClearTextBoxes();
                    LoadData();
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Lỗi: " + exp.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnImport_QLNCC_Click(object sender, EventArgs e)
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

                    for (int excelWorkSheetRowIndex = 2; excelWorkSheetRowIndex < importRange.Rows.Count + 1; excelWorkSheetRowIndex++)
                    {
                        string tenNCC = Convert.ToString(importWorksheet.Cells[excelWorkSheetRowIndex, 2].Value);
                        string diaChi = Convert.ToString(importWorksheet.Cells[excelWorkSheetRowIndex, 3].Value);
                        if (string.IsNullOrWhiteSpace(tenNCC) || string.IsNullOrWhiteSpace(diaChi) ||
                            string.IsNullOrEmpty(tenNCC) || string.IsNullOrEmpty(diaChi))
                        {
                            continue;
                        }
                        Supplier supplier = NCC.Suppliers.Where(s => s.name.Equals(tenNCC) && s.address.Equals(diaChi)).SingleOrDefault();
                        if (supplier == null)
                        {
                            Supplier supplierAdd = new Supplier();
                            supplierAdd.name = tenNCC;
                            supplierAdd.address = diaChi;

                            NCC.Suppliers.Add(supplierAdd);
                            NCC.SaveChanges();
                            txtMaNhaCungCap_QLNCC.Text = supplierAdd.supplierID + "";
                            txtTenNhaCungCap_QLNCC.Text = supplierAdd.name;
                            txtDiaChi_QLNCC.Text = supplierAdd.address;
                            LoadData();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Cảnh báo");
            }
        }

        private void btnExport_QLNCC_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCungCap_QLNCC.Rows.Count > 0)
                {
                    Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                    excelApp.Application.Workbooks.Add(Type.Missing);
                    for (int i = 1; i < dgvCungCap_QLNCC.Columns.Count + 1; i++)
                    {
                        excelApp.Cells[1, i] = dgvCungCap_QLNCC.Columns[i - 1].HeaderText;
                    }
                    for (int i = 0; i < dgvCungCap_QLNCC.Rows.Count; i++)
                    {
                        for (int j = 0; j < dgvCungCap_QLNCC.Columns.Count; j++)
                        {

                            excelApp.Cells[i + 2, j + 1] = dgvCungCap_QLNCC.Rows[i].Cells[j].Value.ToString();
                        }

                    }
                    excelApp.Columns.AutoFit();
                    excelApp.Visible = true;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Lỗi: " + exp.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmQuanLyNhaCungCap_Load(object sender, EventArgs e)
        {
            LoadData();
            txtMaNhaCungCap_QLNCC.ReadOnly = true;
        }
    }
}

