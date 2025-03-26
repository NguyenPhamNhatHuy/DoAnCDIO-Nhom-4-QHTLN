using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nhom4DoAn_HeThongQuanLyCuaHangTapHoa.QuanLy;
using Nhom4DoAn_HeThongQuanLyCuaHangTapHoa.ThongKe;

namespace Nhom4DoAn_HeThongQuanLyCuaHangTapHoa
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmDangNhap());
        }
    }
}
