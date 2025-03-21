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
        quanlycuahangtaphoaEntities quanlycuahangtaphoaEntities;

        public frmQuanLyDanhMuc()
        {
            InitializeComponent();
            quanlycuahangtaphoaEntities = new  quanlycuahangtaphoaEntities();
        }

        private void frmQuanLyDanhMuc_Load(object sender, EventArgs e)
        {
            LoadData();
            // Đảm bảo không thể nhập liệu
            txtMaDanhMuc_QLDM.ReadOnly = true;
            SetWidth();
        }
        private void SetWidth()
        {
            dgvDanhMuc_QLDM.Columns[0].HeaderText = "Mã danh mục";
            dgvDanhMuc_QLDM.Columns[1].HeaderText = "Tên danh mục";
            dgvDanhMuc_QLDM.Columns[0].Width = 210;
            dgvDanhMuc_QLDM.Columns[1].Width = 210;
        }

        private void LoadData()
        {
            dgvDanhMuc_QLDM.DataSource = quanlycuahangtaphoaEntities.Categories.Select(DM => new
            {
                DM.categoryID,
                DM.name
            }).ToList();
        }
        private void dgvDanhMuc_QLDM_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            txtMaDanhMuc_QLDM.Text = dgvDanhMuc_QLDM.Rows[row].Cells[0].Value + "";
            txtTenDanhMuc_QLDM.Text = dgvDanhMuc_QLDM.Rows[row].Cells[1].Value + "";
        }

        private void btnThem_QLDM_Click(object sender, EventArgs e)
        {
            string tenDanhMuc = txtTenDanhMuc_QLDM.Text;
            if (string.IsNullOrEmpty(tenDanhMuc))
            {
                MessageBox.Show("Bạn cần điền đầy đủ thông tin",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    Category category = new Category { name = tenDanhMuc };
                    quanlycuahangtaphoaEntities.Categories.Add(category);
                    quanlycuahangtaphoaEntities.SaveChanges();
                    txtMaDanhMuc_QLDM.Text = category.categoryID + "";
                    LoadData();
                    MessageBox.Show("Thêm danh mục thành công", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception exp)
                {
                    MessageBox.Show("Lỗi: " + exp.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCapNhat_QLDM_Click(object sender, EventArgs e)
        {
            try
            {
                int updateID = int.Parse(txtMaDanhMuc_QLDM.Text);
                Category category = quanlycuahangtaphoaEntities.Categories.
                    SingleOrDefault(c => c.categoryID == updateID);
                category.name = txtTenDanhMuc_QLDM.Text;
                if (string.IsNullOrEmpty(category.name))
                {
                    MessageBox.Show("Bạn cần điền đầy đủ thông tin",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    ClearTextBoxes();
                    quanlycuahangtaphoaEntities.SaveChanges();
                    LoadData();
                    MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Lỗi: " + exp.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnXoa_QLDM_Click(object sender, EventArgs e)
        {
            try
            {
                int removeID = int.Parse(txtMaDanhMuc_QLDM.Text);
                Category category = quanlycuahangtaphoaEntities.Categories.
                    SingleOrDefault(c => c.categoryID == removeID);
                quanlycuahangtaphoaEntities.Entry(category).State = System.Data.Entity.EntityState.Deleted;
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Cảnh báo",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult.Equals(DialogResult.Yes))
                {
                    quanlycuahangtaphoaEntities.SaveChanges();
                    ClearTextBoxes();
                    LoadData();
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Lỗi: " + exp.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClearTextBoxes()
        {
            txtMaDanhMuc_QLDM.Text = "";
            txtTenDanhMuc_QLDM.Text = "";
        }

        private void btnClear_QLDM_Click(object sender, EventArgs e)
        {
            ClearTextBoxes();
        }

        private void btnImport_QLDM_Click(object sender, EventArgs e)
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
                        string categoryName = Convert.ToString(importWorksheet.Cells[excelWorkSheetRowIndex, 2].Value);
                        if (string.IsNullOrWhiteSpace(categoryName) || string.IsNullOrEmpty(categoryName))
                        {
                            continue;
                        }
                        Category category = quanlycuahangtaphoaEntities.Categories.Where(c => c.name.Equals(categoryName)).SingleOrDefault();
                        if (category == null)
                        {
                            Category categoryAdd = new Category();
                            categoryAdd.name = categoryName;

                            quanlycuahangtaphoaEntities.Categories.Add(categoryAdd);
                            quanlycuahangtaphoaEntities.SaveChanges();
                            txtMaDanhMuc_QLDM.Text = categoryAdd.categoryID + "";
                            txtTenDanhMuc_QLDM.Text = categoryAdd.name;
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

        private void btnExport_QLDM_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDanhMuc_QLDM.Rows.Count > 0)
                {
                    Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                    excelApp.Application.Workbooks.Add(Type.Missing);
                    for (int i = 1; i < dgvDanhMuc_QLDM.Columns.Count + 1; i++)
                    {
                        excelApp.Cells[1, i] = dgvDanhMuc_QLDM.Columns[i - 1].HeaderText;
                    }
                    for (int i = 0; i < dgvDanhMuc_QLDM.Rows.Count; i++)
                    {
                        for (int j = 0; j <dgvDanhMuc_QLDM.Columns.Count; j++)
                        {

                            excelApp.Cells[i + 2, j + 1] = dgvDanhMuc_QLDM.Rows[i].Cells[j].Value.ToString();
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
    }
}
