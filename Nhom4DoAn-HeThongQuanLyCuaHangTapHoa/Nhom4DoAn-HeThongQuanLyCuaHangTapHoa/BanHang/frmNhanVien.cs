using Nhom4DoAn_HeThongQuanLyCuaHangTapHoa.HoaDon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nhom4DoAn_HeThongQuanLyCuaHangTapHoa
{
    public partial class frmNhanVien : Form
    {
        private User user;

        public User User { get => user; set => user = value; }
        quanlycuahangtaphoaEntities NV = new quanlycuahangtaphoaEntities();

        List<ProductInCart> productInCarts = new List<ProductInCart>();
        ProductInCart productInCart = null;

        class ProductInCart
        {
            public int productID { get; set; }
            public string name { get; set; }
            public long price { get; set; }
            public int quantity { get; set; }
            public int discount { get; set; }
            public long totalAmount { get; set; }

            public void setDiscount(int discount)
            {
                long amount = this.price * this.quantity;
                this.totalAmount = amount - amount * discount / 100;
            }

            public ProductInCart(int productID, string name, long price, int quantity)
            {
                this.productID = productID;
                this.name = name;
                this.price = price;
                this.quantity = quantity;
                this.totalAmount = price * quantity;
            }

            // override object.Equals
            public override bool Equals(object obj)
            {
                ProductInCart product = (ProductInCart)obj;
                return this.name.Equals(product.name);
            }

            public bool CompareID(ProductInCart product)
            {
                return this.productID.Equals(product.productID);
            }
        }

        public frmNhanVien()
        {
            InitializeComponent();
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            grbThongTinKhachHang_NV.Visible = false;
            grbChiTietSanPham_NV.Visible = false;
            dgvGioHang_NV.Visible = false;
            lblDetailID.Visible = false;

            // Thêm sự kiện CellClick cho datagridview sản phẩm
            dgvSanPham_NV.CellClick += new DataGridViewCellEventHandler(this.dgvSanPham_NV_CellContentClick);
            dgvGioHang_NV.CellClick += new DataGridViewCellEventHandler(this.dgvGioHang_NV_CellContentClick);

            setDataDgvProducts();

            var categories = NV.Categories.Select(c => new
            {
                name = c.name
            }).ToList();

            cboDanhMuc_NV.Items.Add("Tất cả");
            foreach (var category in categories)
            {
                cboDanhMuc_NV.Items.Add(category.name);
            }
            cboDanhMuc_NV.SelectedIndex = 0;
        }

        private long sumOfMoney()
        {
            long sum = 0;
            for (int i = 0; i < productInCarts.Count; i++)
            {
                sum += productInCarts[i].totalAmount;
            }

            lbTongTien_NV.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", sum) + " vnđ";

            return sum;
        }

        private long sumOfTotalMoney()
        {
            long sum = 0;
            for (int i = 0; i < productInCarts.Count; i++)
            {
                sum += (productInCarts[i].quantity * productInCarts[i].price);
            }

            return sum;
        }

        private void getAndSetCusomer(string phone)
        {
            Customer customer = NV.Customers.Select(c => c).Where(c => c.phone == phone).SingleOrDefault();

            if (customer == null)
            {
                grbThongTinKhachHang_NV.Visible = true;
                txtTen_NV.ReadOnly = false;
                txtDiaChi_NV.ReadOnly = false;
                txtSDT_NV.ReadOnly = false;
                txtTen_NV.Clear();
                txtDiaChi_NV.Clear();
                txtCheckSdt_NV.Clear();
                btnThem_NV.Visible = true;
                txtSDT_NV.Text = phone;
                return;
            }
            txtCheckSdt_NV.Clear();
            btnSua_NV.Visible = true;
            setCustomerDetail(customer);
            NV = new quanlycuahangtaphoaEntities();
        }
        private void btnSua_NV_Click(object sender, EventArgs e)
        {
            string phone = txtSDT_NV.Text;
            frmCapNhatThongTinKH KH = new frmCapNhatThongTinKH();
            KH.PhoneNumber = phone;
            KH.ShowDialog();
            getAndSetCusomer(phone);
        }

        private void btnKiemTraSdt_NV_Click(object sender, EventArgs e)
        {
            grbThongTinKhachHang_NV.Visible = false;

            string phone = txtCheckSdt_NV.Text;

            // Check if the phone number is empty or not exactly 10 digits
            if (string.IsNullOrWhiteSpace(phone) || phone.Length != 10)
            {
                MessageBox.Show("Số điện thoại phải có 10 chữ số!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if the phone number consists of only digits
            if (!phone.All(char.IsDigit))
            {
                MessageBox.Show("Số điện thoại chỉ được phép chứa các chữ số!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            getAndSetCusomer(phone);
        }

        private void btnThem_NV_Click(object sender, EventArgs e)
        {
            string phone = txtSDT_NV.Text;
            string name = txtTen_NV.Text;
            string address = txtDiaChi_NV.Text;

            if (phone.Length == 0 || name.Length == 0 || address.Length == 0)
            {
                MessageBox.Show("Không để trống các trường!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var curCustomer = NV.Customers.Select(c => c).Where(c => c.phone == phone).SingleOrDefault();

            if (curCustomer != null)
            {
                MessageBox.Show("Khách hàng đã tồn tại trong hệ thống!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Customer customer = new Customer();
            customer.name = name;
            customer.phone = phone;
            customer.address = address;

            NV.Customers.Add(customer);
            NV.SaveChanges();
            btnSua_NV.Visible = true;
            grbThongTinKhachHang_NV.Visible = false;
            setCustomerDetail(customer);

        }
        private void setCustomerDetail(Customer customer)
        {
            grbThongTinKhachHang_NV.Visible = true;
            txtTen_NV.Text = customer.name;
            txtTen_NV.ReadOnly = true;
            txtDiaChi_NV.Text = customer.address;
            txtDiaChi_NV.ReadOnly = true;
            txtSDT_NV.Text = customer.phone;
            txtSDT_NV.ReadOnly = true;
            btnThem_NV.Visible = false;
        }
        private void setTitleDgvProducts()
        {
            dgvSanPham_NV.Columns[0].HeaderText = "Mã";
            dgvSanPham_NV.Columns[1].HeaderText = "Tên sản phẩm";
            dgvSanPham_NV.Columns[2].HeaderText = "Số lượng còn";
            dgvSanPham_NV.Columns[3].HeaderText = "Giá bán (VNĐ)";
            dgvSanPham_NV.Columns[3].DefaultCellStyle.Format = "#,### vnđ";
        }
        private void setDataDgvProducts()
        {
            var products = NV.Products.Select(p => new
            {
                productID = p.productID,
                name = p.name,
                stockOnHand = p.stockOnHand,
                price = p.price
            }).Where(p => p.stockOnHand > 0);

            dgvSanPham_NV.DataSource = null;
            dgvSanPham_NV.DataSource = products.ToList();
            setTitleDgvProducts();
        }

        private void setDataDgvCarts()
        {
            dgvGioHang_NV.DataSource = null;
            dgvGioHang_NV.DataSource = productInCarts;

            dgvGioHang_NV.Columns[0].HeaderText = "Mã";
            dgvGioHang_NV.Columns[1].HeaderText = "Tên sản phẩm";
            dgvGioHang_NV.Columns[2].HeaderText = "Giá bán (VNĐ)";
            dgvGioHang_NV.Columns[3].HeaderText = "Số lượng";
            dgvGioHang_NV.Columns[4].HeaderText = "Giảm giá (%)";
            dgvGioHang_NV.Columns[5].HeaderText = "Thành tiền (VNĐ)";
            dgvGioHang_NV.Columns[2].DefaultCellStyle.Format = "#,### vnđ";
            dgvGioHang_NV.Columns[5].DefaultCellStyle.Format = "#,### vnđ";
        }

        private void filterProductByCategory(string categoryName)
        {
            var products = NV.Products.Join(NV.Categories,
                 p => p.categoryID,
                 c => c.categoryID,
                 (p, c) => new {
                     product = p,
                     category = c
                 })
            .Where(c => c.category.name == categoryName)
            .Select(p => new
            {
                productID = p.product.productID,
                name = p.product.name,
                stockOnHand = p.product.stockOnHand,
                price = p.product.price
            });

            dgvSanPham_NV.DataSource = null;
            dgvSanPham_NV.DataSource = products.ToList();
            setTitleDgvProducts();
        }

        private int findProductInCart(ProductInCart proInCart)
        {
            for (int i = 0; i < productInCarts.Count; i++)
            {
                if (productInCarts[i].Equals(proInCart))
                {
                    return i;
                }
            }
            return -1;
        }

        private void cboDanhMuc_NV_SelectedIndexChanged(object sender, EventArgs e)
        {
            string category = cboDanhMuc_NV.SelectedItem.ToString();
            if (category.Equals("Tất cả"))
            {
                setDataDgvProducts();
                return;
            }

            filterProductByCategory(category);
        }

        private void selectedProduct(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                dgvGioHang_NV.Visible = true;

                string productID = dgvSanPham_NV.Rows[e.RowIndex].Cells[0].Value.ToString();
                string name = dgvSanPham_NV.Rows[e.RowIndex].Cells[1].Value.ToString();
                string price = dgvSanPham_NV.Rows[e.RowIndex].Cells[3].Value.ToString();

                // Loại bỏ định dạng VNĐ từ giá (nếu có)
                if (price.Contains("vnđ"))
                {
                    price = price.Replace("vnđ", "").Trim();
                }
                if (price.Contains(","))
                {
                    price = price.Replace(",", "");
                }

                productInCart = new ProductInCart(int.Parse(productID), name, long.Parse(price), 1);
                int index = findProductInCart(productInCart);

                if (index == -1)
                {
                    productInCarts.Add(productInCart);
                    setDataDgvCarts();
                    sumOfMoney();
                    productInCart = null;
                    return;
                }
                int quantity = productInCarts[index].quantity;
                int curQuantity = quantity + 1;
                if (lblDetailID.Text.Equals(productID))
                {
                    txtSoLuong_NV.Text = curQuantity.ToString();
                }
                productInCarts[index] = new ProductInCart(productInCart.productID, productInCart.name, productInCart.price, curQuantity);
                setDataDgvCarts();
                sumOfMoney();
                productInCart = null;
            }
        }

        private void viewDetailProduct(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string ID = dgvGioHang_NV.Rows[e.RowIndex].Cells[0].Value.ToString();
                string name = dgvGioHang_NV.Rows[e.RowIndex].Cells[1].Value.ToString();
                string price = dgvGioHang_NV.Rows[e.RowIndex].Cells[2].Value.ToString();
                string quantity = dgvGioHang_NV.Rows[e.RowIndex].Cells[3].Value.ToString();
                int productID = int.Parse(ID);

                var product = NV.Products.Select(p => p).Where(p => p.stockOnHand > 0 && p.productID == productID).SingleOrDefault();

                lblDetailID.Text = ID;
                grbChiTietSanPham_NV.Visible = true;
                lblTenSanPham_NV.Text = name;
                lblDonGia_NV.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", long.Parse(price)) + " vnđ";
                lblSoLuong_NV.Text = product.stockOnHand.ToString();
                txtSoLuong_NV.Text = quantity;
            }
        }

        private void updateQuantity(int ID, int quantity)
        {
            ProductInCart prInCart = new ProductInCart(ID, null, 0, 0);
            for (int i = 0; i < productInCarts.Count; i++)
            {
                if (productInCarts[i].CompareID(prInCart))
                {
                    ProductInCart productInCart = productInCarts[i];
                    productInCarts[i] = new ProductInCart(productInCart.productID, productInCart.name, productInCart.price, quantity);
                }
            }
            sumOfMoney();
        }

        private void updateDiscount(int ID, int discount)
        {
            ProductInCart prInCart = new ProductInCart(ID, null, 0, 0);
            for (int i = 0; i < productInCarts.Count; i++)
            {
                if (productInCarts[i].CompareID(prInCart))
                {
                    ProductInCart productInCart = productInCarts[i];
                    productInCarts[i].discount = discount;
                    productInCarts[i].setDiscount(discount);
                }
            }
            setDataDgvCarts();
            sumOfMoney();
        }

        private void btnGiam_NV_Click(object sender, EventArgs e)
        {
            int quantity = int.Parse(txtSoLuong_NV.Text);
            quantity--;
            if (quantity < 1)
            {
                btnXoa_NV_Click(sender, e);
                return;
            }

            updateQuantity(int.Parse(lblDetailID.Text), quantity);
            txtSoLuong_NV.Text = quantity.ToString();

            setDataDgvCarts();
        }

        private void btnTang_NV_Click(object sender, EventArgs e)
        {
            int quantity = int.Parse(txtSoLuong_NV.Text);
            int stockOnHand = int.Parse(lblSoLuong_NV.Text);
            quantity++;

            if (quantity > stockOnHand)
            {
                MessageBox.Show("Quá số lượng trong kho!", "Thông báo!",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }
            updateQuantity(int.Parse(lblDetailID.Text), quantity);
            txtSoLuong_NV.Text = quantity.ToString();

            setDataDgvCarts();
        }

        private void btnXoa_NV_Click(object sender, EventArgs e)
        {
            string name = lblTenSanPham_NV.Text;

            DialogResult dialogResult = MessageBox.Show("Bạn có muốn xoá sản phẩm khỏi giỏ hàng?", "Thông báo!",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            bool status = false;

            if (dialogResult == DialogResult.Yes)
            {
                status = productInCarts.Remove(new ProductInCart(0, name, 0, 0));
            }

            if (!status)
            {
                MessageBox.Show("Xoá thất bại!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            MessageBox.Show("Xoá thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            grbChiTietSanPham_NV.Visible = false;
            setDataDgvCarts();
            sumOfMoney();
        }

        private void txtTimKiem_NV_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtTimKiem_NV.Text;

            if (keyword.Length == 0)
            {
                setDataDgvProducts();
                return;
            }

            var products = NV.Products.Select(p => new
            {
                productID = p.productID,
                name = p.name,
                stockOnHand = p.stockOnHand,
                price = p.price
            }).Where(p => p.stockOnHand > 0 && p.name.ToLower().Contains(keyword.ToLower()));

            dgvSanPham_NV.DataSource = null;
            dgvSanPham_NV.DataSource = products.ToList();
            setTitleDgvProducts();
        }

        private void txtSoLuong_NV_TextChanged(object sender, EventArgs e)
        {
            string text = txtSoLuong_NV.Text;
            if (text.Length == 0)
            {
                text = "0";
            }
            int quantity = int.Parse(text);
            if (quantity == 0)
            {
                btnXoa_NV_Click(sender, e);
            }
            updateQuantity(int.Parse(lblDetailID.Text), quantity);
            setDataDgvCarts();
        }

        private void btnSuDung_NV_Click(object sender, EventArgs e)
        {
            int discount = int.Parse(txtGiamGia_NV.Text);
            txtGiamGia_NV.Clear();
            int ID = int.Parse(lblDetailID.Text);

            for (int i = 0; i < productInCarts.Count; i++)
            {
                if (productInCarts[i].productID == ID)
                {
                    productInCarts[i].setDiscount(discount);
                }
            }

            updateDiscount(ID, discount);
            setDataDgvCarts();
        }

        private void btnThanhToan_NV_Click(object sender, EventArgs e)
        {
            string phone = txtSDT_NV.Text;

            if (productInCarts.Count == 0)
            {
                MessageBox.Show("Chưa có sản phẩm trong giỏ hàng!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!txtSDT_NV.ReadOnly)
            {
                MessageBox.Show("Chưa có thông tin khách hàng!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var customer = NV.Customers.Select(c => c).Where(c => c.phone == phone).SingleOrDefault();

            if (customer == null)
            {
                MessageBox.Show("Chưa có thông tin khách hàng!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (user == null)
            {
                MessageBox.Show("User is not logged in!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Order currentOrder = new Order();
            currentOrder.amount = sumOfTotalMoney();
            currentOrder.totalAmount = sumOfMoney();
            currentOrder.userID = user.userID;
            currentOrder.customerID = customer.customerID;
            currentOrder.createdAt = DateTime.Now;

            NV.Orders.Add(currentOrder);
            NV.SaveChanges();

            int ID = currentOrder.orderID;

            List<OrderDetail> orderDetails = new List<OrderDetail>();

            for (int i = 0; i < productInCarts.Count; i++)
            {
                ProductInCart prInCart = productInCarts[i];

                OrderDetail orderDetail = new OrderDetail();
                orderDetail.discount = prInCart.discount;
                orderDetail.price = prInCart.price;
                orderDetail.productID = prInCart.productID;
                orderDetail.orderID = ID;
                orderDetail.quantity = prInCart.quantity;

                Product product = NV.Products.Find(prInCart.productID);
                product.stockOnHand = product.stockOnHand - prInCart.quantity;

                orderDetails.Add(orderDetail);
            }

            NV.OrderDetails.AddRange(orderDetails);
            NV.SaveChanges();

            frmChiTietHoaDon CTHD = new frmChiTietHoaDon();
            CTHD.OrderID = ID;
 
            CTHD.ShowDialog();
            productInCarts = new List<ProductInCart>();
            this.frmNhanVien_Load(sender, e);

        }
        private void btnDangXuat_NV_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có thực sự muốn đăng xuất không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (DialogResult.Yes.Equals(result))
            {
                frmDangNhap frm = new frmDangNhap();
                frm.Closed += (s, args) => this.Close();
                frm.Show();
                this.Hide();
            }
            else
            {
                return;
            }
        }
        private void dgvSanPham_NV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedProduct(sender, e);
        }

        private void dgvGioHang_NV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            viewDetailProduct(sender, e);
        }

        private void frmNhanVien_Load_1(object sender, EventArgs e)
        {
            grbThongTinKhachHang_NV.Visible = false;
            grbChiTietSanPham_NV.Visible = false;
            dgvGioHang_NV.Visible = false;
            lblDetailID.Visible = false;

            // Thêm sự kiện CellClick cho datagridview sản phẩm
            dgvSanPham_NV.CellClick += new DataGridViewCellEventHandler(this.dgvSanPham_NV_CellContentClick);
            dgvGioHang_NV.CellClick += new DataGridViewCellEventHandler(this.dgvGioHang_NV_CellContentClick);

            setDataDgvProducts();

            var categories = NV.Categories.Select(c => new
            {
                name = c.name
            }).ToList();

            cboDanhMuc_NV.Items.Add("Tất cả");
            foreach (var category in categories)
            {
                cboDanhMuc_NV.Items.Add(category.name);
            }
            cboDanhMuc_NV.SelectedIndex = 0;
        }
    }
}