using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace Nhom4DoAn_HeThongQuanLyCuaHangTapHoa.HoaDon
{
    public partial class frmChiTietHoaDon : Form
    {
        private int orderID;
        quanlycuahangtaphoaEntities HD = new quanlycuahangtaphoaEntities();
        public frmChiTietHoaDon()
        {
            InitializeComponent();
        }
        public int OrderID { get => orderID; set => orderID = value; }

        public void getAllInfor()
        {
            try
            {
                // Verify database connection
                if (HD == null)
                {
                    HD = new quanlycuahangtaphoaEntities();
                }

                // Check if orderID is valid
                if (orderID <= 0)
                {
                    MessageBox.Show("Mã hóa đơn không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Find the order
                Order order = HD.Orders.FirstOrDefault(p => p.orderID == orderID);
                if (order == null)
                {
                    MessageBox.Show("Không tìm thấy hóa đơn với mã " + orderID, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Set order information
                lbMaHoaDon_CTHD.Text = "Mã hóa đơn: " + order.orderID;

                // Format total amount with currency
                if (order.totalAmount != null)
                {
                    lbTongTien_CTHD.Text = "Tổng tiền: " + string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", order.totalAmount) + " vnđ";
                }
                else
                {
                    lbTongTien_CTHD.Text = "Tổng tiền: 0 vnđ";
                }

                // Format date and time
                if (order.createdAt != null)
                {
                    // With this line
                    lbNgayBan_CTHD.Text = "Ngày bán: " + order.createdAt?.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                }
                else
                {
                    lbNgayBan_CTHD.Text = "Ngày bán: N/A";
                }

                // Get user information
                if (order.userID != null)
                {
                    User user = HD.Users.FirstOrDefault(p => p.userID == order.userID);
                    if (user != null)
                    {
                        lbMaNhanVien_CTHD.Text = "Mã nhân viên: " + user.userID;
                        lbTenNhanVien_CTHD.Text = "Tên nhân viên: " + user.fullName;
                    }
                    else
                    {
                        lbMaNhanVien_CTHD.Text = "Mã nhân viên: N/A";
                        lbTenNhanVien_CTHD.Text = "Tên nhân viên: N/A";
                    }
                }
                else
                {
                    lbMaNhanVien_CTHD.Text = "Mã nhân viên: N/A";
                    lbTenNhanVien_CTHD.Text = "Tên nhân viên: N/A";
                }

                // Get customer information
                if (order.customerID != null)
                {
                    Customer customer = HD.Customers.FirstOrDefault(p => p.customerID == order.customerID);
                    if (customer != null)
                    {
                        lbTenKhachHang_CTHD.Text = "Tên khách hàng: " + customer.name;
                        lbDiaChiKhachHang_CTHD.Text = "Địa chỉ: " + customer.address;
                        lbSdtKhachHang_CTHD.Text = "Số điện thoại: " + customer.phone;
                    }
                    else
                    {
                        lbTenKhachHang_CTHD.Text = "Tên khách hàng: N/A";
                        lbDiaChiKhachHang_CTHD.Text = "Địa chỉ: N/A";
                        lbSdtKhachHang_CTHD.Text = "Số điện thoại: N/A";
                    }
                }
                else
                {
                    lbTenKhachHang_CTHD.Text = "Tên khách hàng: N/A";
                    lbDiaChiKhachHang_CTHD.Text = "Địa chỉ: N/A";
                    lbSdtKhachHang_CTHD.Text = "Số điện thoại: N/A";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy thông tin hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void getAllProduct()
        {
            try
            {
                // Verify database connection
                if (HD == null)
                {
                    HD = new quanlycuahangtaphoaEntities();
                }

                // Check if orderID is valid
                if (orderID <= 0)
                {
                    MessageBox.Show("Mã hóa đơn không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Get all product details for the order using a safer approach
                var orderDetails = HD.OrderDetails.Where(a => a.orderID == orderID).ToList();
                if (orderDetails == null || orderDetails.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy chi tiết sản phẩm cho hóa đơn này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Create a result list to hold the joined data
                var result = new List<dynamic>();

                foreach (var detail in orderDetails)
                {
                    var product = HD.Products.FirstOrDefault(p => p.productID == detail.productID);
                    if (product != null)
                    {
                        // Calculate total after discount safely
                        double quantity = 0;
                        double price = 0;
                        double discount = 0;

                        if (detail.quantity != null)
                            quantity = Convert.ToDouble(detail.quantity);

                        if (detail.price != null)
                            price = Convert.ToDouble(detail.price);

                        if (detail.discount != null)
                            discount = Convert.ToDouble(detail.discount);

                        double totalAmount = price * quantity - (price * quantity * discount / 100);

                        // Add to result
                        result.Add(new
                        {
                            maHang = detail.productID,
                            tenHang = product.name,
                            soLuong = detail.quantity,
                            donGia = detail.price,
                            giamGia = detail.discount,
                            thanhTien = totalAmount
                        });
                    }
                }

                // Set DataGridView data source
                dgvDanhSachSp_CTHD.DataSource = result.ToList();

                // Set column headers
                if (dgvDanhSachSp_CTHD.Columns.Count >= 6)
                {
                    dgvDanhSachSp_CTHD.Columns[0].HeaderText = "Mã hàng";
                    dgvDanhSachSp_CTHD.Columns[1].HeaderText = "Tên hàng";
                    dgvDanhSachSp_CTHD.Columns[2].HeaderText = "Số lượng";
                    dgvDanhSachSp_CTHD.Columns[3].HeaderText = "Đơn giá";
                    dgvDanhSachSp_CTHD.Columns[4].HeaderText = "Giảm giá";
                    dgvDanhSachSp_CTHD.Columns[5].HeaderText = "Thành tiền";
                    dgvDanhSachSp_CTHD.Columns[5].DefaultCellStyle.Format = "#,### vnđ";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy danh sách sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnIn_CTHD_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "PDF Files|*.pdf";
                saveDialog.Title = "Lưu hóa đơn";
                saveDialog.FileName = $"HoaDon_{orderID}.pdf";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    iTextSharp.text.Document document = new iTextSharp.text.Document();
                    iTextSharp.text.pdf.PdfWriter.GetInstance(document, new FileStream(saveDialog.FileName, FileMode.Create));
                    document.Open();

                    // Font cho tiếng Việt
                    BaseFont bf = BaseFont.CreateFont("c:\\windows\\fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    iTextSharp.text.Font font = new iTextSharp.text.Font(bf, 12);
                    iTextSharp.text.Font fontBold = new iTextSharp.text.Font(bf, 13, iTextSharp.text.Font.BOLD);
                    iTextSharp.text.Font fontTitle = new iTextSharp.text.Font(bf, 20, iTextSharp.text.Font.BOLD);

                    // Tiêu đề
                    Paragraph title = new Paragraph("HÓA ĐƠN BÁN HÀNG", fontTitle);
                    title.Alignment = Element.ALIGN_CENTER;
                    document.Add(title);
                    document.Add(new Paragraph("\n"));

                    // Thông tin hóa đơn
                    document.Add(new Paragraph(lbMaHoaDon_CTHD.Text, font));
                    document.Add(new Paragraph(lbNgayBan_CTHD.Text, font));
                    document.Add(new Paragraph(lbTenNhanVien_CTHD.Text, font));
                    document.Add(new Paragraph(lbTenKhachHang_CTHD.Text, font));
                    document.Add(new Paragraph(lbDiaChiKhachHang_CTHD.Text, font));
                    document.Add(new Paragraph(lbSdtKhachHang_CTHD.Text, font));
                    document.Add(new Paragraph("\n"));

                    // Tạo bảng sản phẩm
                    PdfPTable table = new PdfPTable(6);
                    table.WidthPercentage = 100;

                    // Header của bảng
                    string[] headers = { "Mã hàng", "Tên hàng", "Số lượng", "Đơn giá", "Giảm giá", "Thành tiền" };
                    foreach (string header in headers)
                    {
                        table.AddCell(new PdfPCell(new Phrase(header, fontBold)));
                    }

                    // Thêm dữ liệu sản phẩm
                    foreach (DataGridViewRow row in dgvDanhSachSp_CTHD.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            for (int i = 0; i < row.Cells.Count; i++)
                            {
                                table.AddCell(new Phrase(row.Cells[i].Value?.ToString() ?? "", font));
                            }
                        }
                    }

                    document.Add(table);
                    document.Add(new Paragraph("\n"));
                    document.Add(new Paragraph(lbTongTien_CTHD.Text, fontBold));

                    document.Close();
                    MessageBox.Show("Xuất hóa đơn PDF thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất PDF: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmChiTietHoaDon_Load(object sender, EventArgs e)
        {
            getAllInfor();
            getAllProduct();
        }
    }
}