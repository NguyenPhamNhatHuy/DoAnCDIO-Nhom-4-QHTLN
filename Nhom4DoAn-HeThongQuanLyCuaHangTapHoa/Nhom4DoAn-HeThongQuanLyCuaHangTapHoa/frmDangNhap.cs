using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nhom4DoAn_HeThongQuanLyCuaHangTapHoa.BanHang;
namespace Nhom4DoAn_HeThongQuanLyCuaHangTapHoa
{
    public partial class frmDangNhap: Form
    {
        public static User currentUser;
        // Chuỗi kết nối SQL Server
        string connectionString = "Server= LAPTOPCUAHUY\\MSSQLSERVER01;Database=quanlycuahangtaphoa;Integrated Security=True;";
        public frmDangNhap()
        {
            InitializeComponent();

            // Ẩn mật khẩu bằng dấu *
            txtPassword.PasswordChar = '*'; // Ẩn mật khẩu bằng dấu *

            // Gán sự kiện KeyDown
            txtUsername.KeyDown += TxtUsername_KeyDown;
            txtPassword.KeyDown += TxtPassword_KeyDown;
        }

        private void ckbHienThiPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbHienThiPassword.Checked)
                txtPassword.PasswordChar = '\0'; // Hiện mật khẩu
            else
                txtPassword.PasswordChar = '*'; // Ẩn mật khẩu
        }
        private void TxtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Ngăn Enter gây tiếng "beep"
                txtPassword.Focus();
            }
        }

        // Khi nhấn Enter trong txtpassword => Thực hiện đăng nhập
        private void TxtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Ngăn Enter gây tiếng "beep"
                btnDangNhap.PerformClick(); // Gọi sự kiện click của nút đăng nhập
            }
        }
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập tên tài khoản và mật khẩu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var dbContext = new quanlycuahangtaphoaEntities())
                {
                    // Sử dụng Entity Framework để truy vấn người dùng
                    var user = dbContext.Users
                        .Where(u => u.username == username && u.password == password)
                        .FirstOrDefault();

                    if (user != null)
                    {
                        // Lưu thông tin người dùng hiện tại
                        currentUser = user;

                        // Đăng nhập thành công
                        this.Hide(); // Ẩn form đăng nhập

                        // Kiểm tra vai trò và mở form tương ứng
                        if (user.role == "Admin")
                        {
                            frmMain adminform = new frmMain();
                            adminform.User = user; // Truyền thông tin người dùng sang form
                            adminform.ShowDialog(); // Hiển thị form admin
                        }
                        else if (user.role == "Nhân Viên")
                        {
                            frmNhanVien nhanVienform = new frmNhanVien();
                            nhanVienform.User = user; // Truyền thông tin người dùng sang form
                            nhanVienform.ShowDialog(); // Hiển thị form nhân viên
                        }
                        else
                        {
                            MessageBox.Show("Vai trò người dùng không được hỗ trợ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối đến cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnHuyDangNhap_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
