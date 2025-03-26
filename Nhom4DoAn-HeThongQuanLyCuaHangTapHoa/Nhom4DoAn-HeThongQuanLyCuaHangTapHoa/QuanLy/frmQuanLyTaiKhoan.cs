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
    public partial class frmQuanLyTaiKhoan : Form
    {
        quanlycuahangtaphoaEntities TK = new quanlycuahangtaphoaEntities();
        private bool isAdding = false;

        static List<string> Roles1;
        static List<string> Roles2;

        public frmQuanLyTaiKhoan()
        {
            InitializeComponent();
            Roles1 = new List<string>() { "Admin", "Nhân Viên" };
            Roles2 = new List<string>() { "All", "Admin", "Nhân Viên" };
        }

        private void ResetValues()
        {
            btnLuu_QLTK.Enabled = false;
            btnHuy_QLTK.Enabled = false;
            btnCapNhat_QLTK.Enabled = false;
            btnXoa_QLTK.Enabled = false;

            btnThem_QLTK.Enabled = true;
            cboQuyen2_QLTK.Enabled = true;

            ClearTextBoxes();
            txtTenTaiKhoan_TK.Focus();

            lblThem_QLTK.Text = "";
        }

        private void ClearTextBoxes()
        {
            txtMaTaiKhoan_TK.Text = "";
            txtTenTaiKhoan_TK.Text = "";
            txtMatKhau_TK.Text = "";
            txtHoVaTen_TK.Text = "";
            txtDiaChi_TK.Text = "";
            txtViTri_TK.Text = "";
            txtDienThoai_TK.Text = "";

            cboQuyenTK_TK.SelectedIndex = -1;

        }

        private void LoadData()
        {
            try
            {
                // Tạo DataSource với thứ tự phù hợp với tiêu đề cột
                dgvTaiKhoan_QLTK.DataSource = TK.Users.Select(u => new
                {
                    u.userID,      // 0: Mã TK
                    u.username,    // 1: Tên TK  
                    u.password,    // 2: Mật khẩu
                    u.role,        // 3: Quyền
                    u.fullName,    // 4: Họ tên
                    u.address,     // 5: Địa chỉ 
                    u.position,    // 6: Vị trí
                    u.phone        // 7: Điện thoại
                }).ToList();

                // Nếu không có dữ liệu, thông báo
                if (dgvTaiKhoan_QLTK.Rows.Count <= 1) // Tính cả dòng header
                {
                    MessageBox.Show("Không có dữ liệu tài khoản nào trong database!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Khởi tạo combobox quyền tài khoản
                if (cboQuyenTK_TK.DataSource == null)
                    cboQuyenTK_TK.DataSource = Roles1;

                // Khởi tạo combobox quyền tìm kiếm
                if (cboQuyen2_QLTK.DataSource == null)
                    cboQuyen2_QLTK.DataSource = Roles2;

                // Chọn giá trị mặc định "All" cho combobox địa chỉ và quyền
                cboQuyen2_QLTK.SelectedIndex = 0; // All

                // Đặt tiêu đề cho các cột
                dgvTaiKhoan_QLTK.Columns[0].HeaderText = "Mã TK";
                dgvTaiKhoan_QLTK.Columns[1].HeaderText = "Tên TK";
                dgvTaiKhoan_QLTK.Columns[2].HeaderText = "Mật khẩu";
                dgvTaiKhoan_QLTK.Columns[3].HeaderText = "Quyền";
                dgvTaiKhoan_QLTK.Columns[4].HeaderText = "Họ tên";
                dgvTaiKhoan_QLTK.Columns[5].HeaderText = "Địa chỉ";
                dgvTaiKhoan_QLTK.Columns[6].HeaderText = "Vị trí";
                dgvTaiKhoan_QLTK.Columns[7].HeaderText = "Điện thoại";

                // Không cho người dùng thêm dữ liệu trực tiếp
                dgvTaiKhoan_QLTK.AllowUserToAddRows = false;
                dgvTaiKhoan_QLTK.EditMode = DataGridViewEditMode.EditProgrammatically;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ResetValues();
        }

        private void dgvTaiKhoan_QLTK_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu đang ở chế độ thêm
            if (isAdding)
            {
                MessageBox.Show("Bạn không thể chọn khi đang ở chế độ thêm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int row = e.RowIndex;

            txtMaTaiKhoan_TK.Text = dgvTaiKhoan_QLTK.Rows[row].Cells[0].Value + "";
            txtTenTaiKhoan_TK.Text = dgvTaiKhoan_QLTK.Rows[row].Cells[1].Value + "";
            txtMatKhau_TK.Text = dgvTaiKhoan_QLTK.Rows[row].Cells[2].Value + "";
            cboQuyenTK_TK.Text = dgvTaiKhoan_QLTK.Rows[row].Cells[3].Value + "";
            txtDienThoai_TK.Text = dgvTaiKhoan_QLTK.Rows[row].Cells[4].Value + "";
            txtHoVaTen_TK.Text = dgvTaiKhoan_QLTK.Rows[row].Cells[5].Value + "";
            txtDiaChi_TK.Text = dgvTaiKhoan_QLTK.Rows[row].Cells[6].Value + "";
            txtViTri_TK.Text = dgvTaiKhoan_QLTK.Rows[row].Cells[7].Value + "";


            // Kích hoạt các nút chức năng
            btnCapNhat_QLTK.Enabled = true;
            btnXoa_QLTK.Enabled = true;
            btnHuy_QLTK.Enabled = true;
        }

        private void btnThem_QLTK_Click(object sender, EventArgs e)
        {
            // Đặt chế độ thêm
            isAdding = true;

            // Set label thông báo chế độ thêm
            lblThem_QLTK.ForeColor = System.Drawing.Color.Red;
            lblThem_QLTK.Text = "*Bạn đang ở chế độ thêm";

            // Vô hiệu hóa các nút khác
            btnThem_QLTK.Enabled = false;
            btnCapNhat_QLTK.Enabled = false;
            btnXoa_QLTK.Enabled = false;
            cboQuyen2_QLTK.Enabled = false;

            // Bật nút "Lưu"
            btnLuu_QLTK.Enabled = true;
            // Bật nút "Hủy"
            btnHuy_QLTK.Enabled = true;

            ClearTextBoxes();

            // Đặt focus vào ô tên sản phẩm
            txtTenTaiKhoan_TK.Focus();
        }

        private void btnLuu_QLTK_Click(object sender, EventArgs e)
        {
            User user = new User();
            user.username = txtTenTaiKhoan_TK.Text;
            user.password = txtMatKhau_TK.Text;
            user.role = cboQuyenTK_TK.SelectedValue + "";
            user.phone = txtDienThoai_TK.Text;
            user.fullName = txtHoVaTen_TK.Text;
            user.address = txtDiaChi_TK.Text;
            user.position = txtViTri_TK.Text;


            if (string.IsNullOrEmpty(user.username) || string.IsNullOrEmpty(user.password)
                || string.IsNullOrEmpty(user.role) || string.IsNullOrEmpty(user.phone)
                || string.IsNullOrEmpty(user.fullName) || string.IsNullOrEmpty(user.address)
                || string.IsNullOrEmpty(user.position))
            {
                MessageBox.Show("Bạn cần nhập đầy đủ thông tin", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                User usr = TK.Users.FirstOrDefault(u => u.username.Equals(user.username));
                if (usr == null)
                {
                    User usr1 = TK.Users.FirstOrDefault(u => u.phone.Equals(user.phone));
                    if (usr1 == null)
                    {
                        TK.Users.Add(user);
                        TK.SaveChanges();
                        txtMaTaiKhoan_TK.Text = user.userID + "";
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Số điện thoại đã tồn tại", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Tên tài khoản đã tồn tại", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHuy_QLTK_Click(object sender, EventArgs e)
        {
            ResetValues();
            // Reset trạng thái thêm
            isAdding = false;

            // nếu đang ở chế độ thêm
            if (btnThem_QLTK.Enabled == false)
                lblThem_QLTK.Text = "";
            btnThem_QLTK.Enabled = true;
            // Đảm bảo nút Cập Nhật bị tắt
            btnCapNhat_QLTK.Enabled = false;
            cboQuyenTK_TK.SelectedIndex = -1;
        }

        private void btnCapNhat_QLTK_Click(object sender, EventArgs e)
        {
            int updateID = int.Parse(txtMaTaiKhoan_TK.Text);
            User user = TK.Users.
                SingleOrDefault(u => u.userID == updateID);

            user.username = txtTenTaiKhoan_TK.Text;
            user.password = txtMatKhau_TK.Text;
            user.role = cboQuyenTK_TK.SelectedValue + "";
            user.phone = txtDienThoai_TK.Text;
            user.fullName = txtHoVaTen_TK.Text;
            user.address = txtDiaChi_TK.Text;
            user.position = txtViTri_TK.Text;


            if (string.IsNullOrEmpty(user.username) || string.IsNullOrEmpty(user.password)
                || string.IsNullOrEmpty(user.role) || string.IsNullOrEmpty(user.fullName)
                || string.IsNullOrEmpty(user.address) || string.IsNullOrEmpty(user.position)
                || string.IsNullOrEmpty(user.phone))
            {
                MessageBox.Show("Bạn cần nhập đầy đủ thông tin", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                TK.SaveChanges();
                //ClearTextBoxes();
                LoadData();
                MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnXoa_QLTK_Click(object sender, EventArgs e)
        {
            int removeID = 0;
            try
            {
                removeID = int.Parse(txtMaTaiKhoan_TK.Text);
            }
            catch
            {
                MessageBox.Show("Mã tài khoản không chính xác!", "Cảnh báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            User user = TK.Users.
                SingleOrDefault(u => u.userID == removeID);
            if (user != null)
            {
                if (user != null && frmDangNhap.currentUser != null && user.userID == frmDangNhap.currentUser.userID)
                {
                    MessageBox.Show("Không thể xóa tài khoản đang đăng nhập!", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    TK.Entry(user).State = System.Data.Entity.EntityState.Deleted;

                    DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Cảnh báo",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult.Equals(DialogResult.Yes))
                    {
                        TK.SaveChanges();
                        // ClearTextBoxes();
                        LoadData();
                    }
                }
            }
        }

        private void frmQuanLyTaiKhoan_Load(object sender, EventArgs e)
        {
            // Thiết lập các ComboBox trước khi tải dữ liệu
            if (cboQuyenTK_TK.DataSource == null)
                cboQuyenTK_TK.DataSource = Roles1;

            if (cboQuyen2_QLTK.DataSource == null)
                cboQuyen2_QLTK.DataSource = Roles2;

            // Tải dữ liệu vào DataGridView
            LoadData();

            txtMaTaiKhoan_TK.ReadOnly = true;
            cboQuyenTK_TK.SelectedIndex = -1;

            dgvTaiKhoan_QLTK.CellClick += new DataGridViewCellEventHandler(dgvTaiKhoan_QLTK_CellContentClick);
        }

        private void cboQuyen2_QLTK_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Lấy giá trị lọc từ ComboBox
                string filterWordRole = cboQuyen2_QLTK.Text.ToLower();
                // Lọc dữ liệu theo quyền
                var query = TK.Users.AsQueryable();
                // Chỉ lọc nếu không phải là "all"
                if (filterWordRole != "all")
                {
                    query = query.Where(u => u.role.ToLower() == filterWordRole);
                }
                // Lấy kết quả và gán cho DataGridView
                var results = query.Select(u => new
                {
                    u.userID,
                    u.username,
                    u.password,
                    u.role,
                    u.fullName,
                    u.address,
                    u.position,
                    u.phone,
                }).ToList();

                dgvTaiKhoan_QLTK.DataSource = results;
                // Chỉ hiện thông báo khi không có kết quả nào
                if (results.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy tài khoản nào phù hợp với điều kiện lọc!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lọc dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
