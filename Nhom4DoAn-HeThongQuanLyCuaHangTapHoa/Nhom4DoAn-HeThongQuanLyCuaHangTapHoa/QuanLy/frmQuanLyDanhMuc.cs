
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Office.Interop.Excel;

namespace Nhom4DoAn_HeThongQuanLyCuaHangTapHoa.QuanLy
{
    public partial class frmQuanLyDanhMuc : Form
    {
        // Khai báo
        quanlycuahangtaphoaEntities DM =new quanlycuahangtaphoaEntities();
        private bool isAdding = false;
        public frmQuanLyDanhMuc()
        {
            InitializeComponent();
        }

        // reset giá trị cho các mục
        private void ResetValues_SP()
        {
            btnLuu_QLDM.Enabled = false;
            btnHuy_QLDM.Enabled = false;
            btnCapNhat_QLDM.Enabled = false;
            btnXoa_QLDM.Enabled = false;

            btnThem_QLDM.Enabled = true;        
            btnImport_QLDM.Enabled = true;
            btnExport_QLDM.Enabled = true;

            ClearTextBoxes();
        }

        private void ClearTextBoxes()
        {
            txtMaDanhMuc_QLDM.Text = "";
            txtTenDanhMuc_QLDM.Text = "";

            lblThem_QLDM.Text = "";
        }

        private void LoadData()
        {
            dgvDanhMuc_QLDM.DataSource = DM.Categories.Select(c => new
            {
                c.categoryID,
                c.name
            }).ToList();

            dgvDanhMuc_QLDM.Columns[0].HeaderText = "Mã danh mục";
            dgvDanhMuc_QLDM.Columns[1].HeaderText = "Tên danh mục";
            
            ClearTextBoxes();
        }

        private void frmQuanLyDanhMuc_Load(object sender, EventArgs e)
        {
            LoadData();
            ResetValues_SP();
            ClearTextBoxes();
           
            txtMaDanhMuc_QLDM.ReadOnly = true;

            // Đăng ký sự kiện CellClick cho DataGridView
            dgvDanhMuc_QLDM.CellClick += new DataGridViewCellEventHandler(dgvDanhMuc_QLDM_CellContentClick);
        }

        private void dgvDanhMuc_QLDM_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int d = e.RowIndex;
            // Kiểm tra nếu đang ở chế độ thêm
            if (isAdding)
            {
                MessageBox.Show("Bạn không thể chọn khi đang ở chế độ thêm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy dữ liệu từ DataGridView và hiển thị lên các TextBox
            txtMaDanhMuc_QLDM.Text = dgvDanhMuc_QLDM.Rows[d].Cells[0].Value?.ToString();
            txtTenDanhMuc_QLDM.Text = dgvDanhMuc_QLDM.Rows[d].Cells[1].Value?.ToString();

            // Kích hoạt các nút chức năng
            btnCapNhat_QLDM.Enabled = true;
            btnXoa_QLDM.Enabled = true;
            btnHuy_QLDM.Enabled = true;      
        }

        private void btnThem_QLDM_Click(object sender, EventArgs e)
        {
            // Đặt chế độ thêm
            isAdding = true;
            // Set label thông báo chế độ thêm
            lblThem_QLDM.ForeColor = System.Drawing.Color.Red;
            lblThem_QLDM.Text = "*Bạn đang ở chế độ thêm";

            // Vô hiệu hóa các nút khác
            btnThem_QLDM.Enabled = false;
            btnCapNhat_QLDM.Enabled = false;
            btnXoa_QLDM.Enabled = false;
            btnImport_QLDM.Enabled = false;
            btnExport_QLDM.Enabled = false;


            // Bật nút "Lưu"
            btnLuu_QLDM.Enabled = true;
            // Bật nút "Hủy"
            btnHuy_QLDM.Enabled = true;

            // Xóa trắng các ô nhập liệu
            txtMaDanhMuc_QLDM.Text = "";
            txtTenDanhMuc_QLDM.Text = "";


            // Đặt focus vào ô tên sản phẩm
            txtMaDanhMuc_QLDM.Focus();
        }

        private void btnCapNhat_QLDM_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtMaDanhMuc_QLDM.Text, out int updateID))
            {
                MessageBox.Show("Mã danh mục không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                Category category = DM.Categories.SingleOrDefault(c => c.categoryID == updateID);
                if (category == null)
                {
                    MessageBox.Show("Không tìm thấy danh mục", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string newName = txtTenDanhMuc_QLDM.Text;
                if (string.IsNullOrEmpty(newName))
                {
                    MessageBox.Show("Bạn cần điền đầy đủ thông tin",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                category.name = newName;
                DM.SaveChanges();
                ClearTextBoxes();
                LoadData();
                MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exp)
            {
                MessageBox.Show("Lỗi: " + exp.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_QLDM_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtMaDanhMuc_QLDM.Text, out int removeID))
            {
                MessageBox.Show("Mã danh mục không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Category category = DM.Categories.SingleOrDefault(c => c.categoryID == removeID);
                if (category == null)
                {
                    MessageBox.Show("Không tìm thấy danh mục", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Cảnh báo",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    DM.Categories.Remove(category);
                    DM.SaveChanges();
                    ClearTextBoxes();
                    LoadData();
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Lỗi: " + exp.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImport_QLDM_Click(object sender, EventArgs e)
        {
            _Application importApp = null;
            _Workbook importWorkbook = null;
            _Worksheet importWorksheet = null;
            Range importRange = null;

            try
            {
                importApp = new Microsoft.Office.Interop.Excel.Application();
                OpenFileDialog importOpenFileDialog = new OpenFileDialog
                {
                    Title = "Import Excel file to data",
                    Filter = "Choose Excel File|*.xlsx;*.xls;*.xlm"
                };

                if (importOpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    importWorkbook = importApp.Workbooks.Open(importOpenFileDialog.FileName);
                    importWorksheet = importWorkbook.ActiveSheet;
                    importRange = importWorksheet.UsedRange;

                    for (int row = 2; row <= importRange.Rows.Count; row++)
                    {
                        string categoryName = Convert.ToString(importWorksheet.Cells[row, 2].Value);
                        if (string.IsNullOrWhiteSpace(categoryName))
                            continue;

                        if (!DM.Categories.Any(c => c.name.Equals(categoryName, StringComparison.OrdinalIgnoreCase)))
                        {
                            Category categoryAdd = new Category { name = categoryName };
                            DM.Categories.Add(categoryAdd);
                            DM.SaveChanges();
                        }
                    }

                    LoadData();
                    MessageBox.Show("Import dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (importRange != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(importRange);
                if (importWorksheet != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(importWorksheet);
                if (importWorkbook != null)
                {
                    importWorkbook.Close();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(importWorkbook);
                }
                if (importApp != null)
                {
                    importApp.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(importApp);
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        private void btnExport_QLDM_Click(object sender, EventArgs e)
        {
            if (dgvDanhMuc_QLDM.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _Application excelApp = null;
            _Workbook workbook = null;
            _Worksheet worksheet = null;

            try
            {
                excelApp = new Microsoft.Office.Interop.Excel.Application();
                workbook = excelApp.Workbooks.Add(Type.Missing);
                worksheet = workbook.ActiveSheet;

                // Export headers
                for (int i = 0; i < dgvDanhMuc_QLDM.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1] = dgvDanhMuc_QLDM.Columns[i].HeaderText;
                }

                // Export data
                for (int i = 0; i < dgvDanhMuc_QLDM.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvDanhMuc_QLDM.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dgvDanhMuc_QLDM.Rows[i].Cells[j].Value?.ToString() ?? "";
                    }
                }

                worksheet.Columns.AutoFit();
                excelApp.Visible = true;
                excelApp.UserControl = true;
            }
            catch (Exception exp)
            {
                MessageBox.Show("Lỗi: " + exp.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (DM != null)
            {
                DM.Dispose();
            }
        }

        private void btnLuu_QLDM_Click(object sender, EventArgs e)
        {
            string tenDanhMuc = txtTenDanhMuc_QLDM.Text;
            if (string.IsNullOrEmpty(tenDanhMuc))
            {
                MessageBox.Show("Bạn cần điền đầy đủ thông tin",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Category category = new Category { name = tenDanhMuc };
                DM.Categories.Add(category);
                DM.SaveChanges();
                txtMaDanhMuc_QLDM.Text = category.categoryID.ToString();
                LoadData();
                MessageBox.Show("Thêm danh mục thành công", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exp)
            {
                MessageBox.Show("Lỗi: " + exp.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_QLDM_Click(object sender, EventArgs e)
        {
            ResetValues_SP();

            // Reset trạng thái thêm
            isAdding = false;

            // nếu đang ở chế độ thêm
            if (btnThem_QLDM.Enabled == false)
                lblThem_QLDM.Text = "";
            btnThem_QLDM.Enabled = true;
            // Đảm bảo nút Cập Nhật bị tắt
            btnCapNhat_QLDM.Enabled = false;
        }
    }
}
