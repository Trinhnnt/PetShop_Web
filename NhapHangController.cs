using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_projectframeword_admin.Models;

namespace DoAn_FW.Areas.Admin.Controllers
{
    public class NhapHangController : Controller
    {
        // GET: NhapHangController
        StoreContext context = new StoreContext(@"Data Source=QUYNHNHULEE\SQLEXPRESS;Initial Catalog=website_PetShop;Integrated Security=True;Encrypt=False");

        public void DataNV()
        {

            var nv = new NhanVien();
            if (HttpContext.Session.GetString("NV") != null)
            {
                nv = JsonConvert.DeserializeObject<NhanVien>(HttpContext.Session.GetString("NV"));
            }
            this.ViewBag.NV = nv;
            int sohdcd = context.HDChuaDuyet();
            this.ViewBag.SoHDCD = sohdcd;
            int soghcg = context.GHChuaGiao();
            this.ViewBag.SoGHCG = soghcg;
        }
        public IActionResult LietKeNhapHang()
        {
            DataNV();
            return View(context.GetNhapHangs());
        }
        public IActionResult ViewNhapHang(string Id)
        {
            DataNV();
            NhapHang nh = context.ViewNhapHang(Id);

            // Thêm danh sách nhà cung cấp và nhân viên vào ViewBag
            ViewBag.DanhSachNCC = context.GetNhaCungCaps();
            ViewBag.DanhSachNV = context.GetNhanViens();

            return View(nh);
        }

        public IActionResult DeleteNhapHang(string Id)
        {
            DataNV();
            int count = context.XoaNhapHang(Id);
            return View("LietKeNhapHang", context.GetNhapHangs());
        }
        public IActionResult UpdateNhapHang(NhapHang nh)
        {
            int count;
            DataNV();
            count = context.UpdateNhapHang(nh);
            if (count > 0)
                ViewData["thongbao"] = "Update thành công";
            else
                ViewData["thongbao"] = "Update không thành công";
            return View("LietKeNhapHang", context.GetNhapHangs());
        }
        // Action để hiển thị form nhập hàng
        public IActionResult ThemPhieuNhap()
        {
            // Lấy danh sách nhà cung cấp
            ViewBag.DanhSachNCC = context.GetNhaCungCaps();

            // Lấy danh sách nhân viên
            DataNV(); // Giữ nguyên phương thức này nếu nó đã lấy dữ liệu nhân viên
            ViewBag.DanhSachNV = context.GetNhanViens(); // Thêm dòng này để lấy danh sách nhân viên

            return View("InsertNhapHang");
        }

        // Action để xử lý việc thêm phiếu nhập (giữ nguyên tên InsertNhapHang)
        public IActionResult InsertNhapHang(NhapHang nh)
        {
            int count;
            DataNV();

            // Xử lý thêm phiếu nhập
            count = context.InsertNhapHang(nh);
            ViewData.Model = nh;

            // Kiểm tra ngày lập phiếu
            if (nh.NGAYLAPPN < DateTime.Today)
            {
                ModelState.AddModelError("NGAYLAPPN", "Ngày nhập phải lớn hơn hoặc bằng ngày hiện tại");
                ViewBag.DanhSachNCC = context.GetNhaCungCaps();
                ViewBag.DanhSachNV = context.GetNhanViens();
                return View("InsertNhapHang", nh); // hoặc ViewModel nếu dùng
            }

            if (count > 0)
                ViewData["thongbao"] = "Thêm thành công";
            else
                ViewData["thongbao"] = "Thêm không thành công";

            return View("LietKeNhapHang", context.GetNhapHangs());
        }

        public IActionResult EnterNhapHang()
        {
            DataNV();
            ViewBag.DanhSachNCC = context.GetNhaCungCaps();
            ViewBag.DanhSachNV = context.GetNhanViens();
            return View();
        }
        public ActionResult CTPN(int ID)
        {
            DataNV();
            List<SanPham> DSSP = context.GetDSSP();
            ViewBag.DSSP = DSSP;
            ViewData["mapn"] = ID;

            // THÊM DÒNG NÀY để truyền trạng thái thanh toán xuống View
            var nhapHang = context.ViewNhapHang(ID.ToString());
            ViewData["TinhTrangThanhToan"] = nhapHang.TINHTRANGTT; // 1: đã thanh toán, 0: chưa thanh toán
            return View(context.GetDSCTPN(ID));
        }
        public ActionResult InsertCTPN(CTPN ct)
        {

            DataNV();
            List<SanPham> DSSP = context.GetDSSP();
            ViewBag.DSSP = DSSP;
            ViewData["mapn"] = ct.MAPN;

            var sanPham = DSSP.FirstOrDefault(sp => sp.MaTTSP == ct.MATTSP);

            if ( ct.MATTSP == 0 )
            {
                ViewData["thongbao"] = "Vui lòng chọn sản phẩm";
            }
            else if (ct.SOLUONG == 0)
            {
                ViewData["thongbao"] = "Vui lòng kiểm tra số lượng nhập";
            }
            else if (ct.GIANHAP <= 0)
            {
                ViewData["thongbao"] = "Vui lòng kiểm tra giá nhập";
            }

            else
    {
        int count = context.InsertCTPN(ct);
        if (count > 0)
        {
            ViewData["thongbao"] = "Thêm thành công";
        }
        else
        {
            ViewData["thongbao"] = "Thêm thất bại";
        }
    }

            // Lưu các giá trị nhập dở
            ViewBag.GiaNhap = ct.GIANHAP;
            ViewBag.SoLuong = ct.SOLUONG;
            ViewBag.MaSP = ct.MATTSP;

            int ID = ct.MAPN;
            return View("CTPN", context.GetDSCTPN(ID));
        }
        public ActionResult DeleteCTPN(int MAPN, int MASP)
        {
            DataNV();
            List<SanPham> DSSP = context.GetDSSP();
            ViewBag.DSSP = DSSP;
            ViewData["mapn"] = MAPN;
            if (MAPN != 0)
            {
                int count = context.DeleteCTPN(MAPN, MASP);
                if (count > 0)
                    ViewData["trangthai"] = "Xóa thành công";
                else
                    ViewData["trangthai"] = "Xóa không thành công";
            }
            return View("CTPN", context.GetDSCTPN(MAPN));
        }
    }
}
