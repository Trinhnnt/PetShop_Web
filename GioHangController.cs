using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DoAn_FW.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Web_ProjectFrameWork.Controllers
{
    public class GioHangController : Controller
    {
        // Lấy thông tin khách hàng đang đăng nhập từ session
        public KhachHang DataKH()
        {
            var KH = GetKhachHangFromSession();  // Gọi hàm lấy thông tin KH từ Session
            this.ViewBag.KH = KH;  /// Gửi thông tin KH sang View để hiển thị   
            return KH;
        }
        // Lấy danh sách giỏ hàng của khách hàng và gán vào ViewBag
        public void DataCart()
        {
            var KH = new KhachHang();
            if (HttpContext.Session.GetString("KH") != null)
            // Deserialize dữ liệu KH từ session thành đối tượng KhachHang
            {
                KH = JsonConvert.DeserializeObject<KhachHang>(HttpContext.Session.GetString("KH"));
            }
            var context = new GioHang();
            var cart = context.ListGioHang(KH.MaKH); // Lấy danh sách giỏ hàng theo mã KH
            this.ViewBag.cart = cart; // Gán vào ViewBag để dùng bên View
        }
        public List<GioHang> GetDataCart()
        {
            var KH = new KhachHang();
            if (HttpContext.Session.GetString("KH") != null)
            {
                KH = JsonConvert.DeserializeObject<KhachHang>(HttpContext.Session.GetString("KH"));
            }
            var context = new GioHang();
            var cart = context.ListGioHang(KH.MaKH);
            this.ViewBag.cart = cart;
            return cart;
        }
        public IActionResult Cart()
        {
            KhachHang kh = DataKH();
            DataCart();
            var context = new GioHang();
            var kmcontext = new KhuyenMai();
            List<object> ListSP = new List<object>();
            List<KhuyenMai> ListKM = kmcontext.ListKM();
            if (kh.MaKH != null && kh.MaKH > 0)
            {
                ListSP = context.ListCTSP(kh.MaKH);
            }
            ViewData["ListKM"] = ListKM;
            ViewData["ListSP"] = ListSP;
            return View();
        }
        private KhachHang GetKhachHangFromSession()
        {
            if (HttpContext.Session.GetString("KH") != null)
                return JsonConvert.DeserializeObject<KhachHang>(HttpContext.Session.GetString("KH"));

            return new KhachHang(); // trả về rỗng nếu chưa đăng nhập
        }


        public IActionResult InsertCart(GioHang g, int MaLoaiSP)
        {
            if (g == null || g.MaTTSP == null || g.SoLuong <= 0)
            {
                TempData["ErrorMessage"] = "Vui lòng nhập đầy đủ và hợp lệ thông tin sản phẩm.";
                return RedirectToAction("ChiTietSP", "SanPham", new { t = g?.MaTTSP ?? 0, l = MaLoaiSP });
            }

            var kh = GetKhachHangFromSession();
            if (kh.MaKH == null || kh.MaKH <= 0)
            {
                TempData["ErrorMessage"] = "Vui lòng đăng nhập!";
                return RedirectToAction("DangNhap", "TaiKhoan");
            }

            return XuLyThemGio(kh.MaKH, g.MaTTSP, g.SoLuong, "ChiTietSP", new { t = g.MaTTSP, l = MaLoaiSP });
        }

        public IActionResult DeleteCart(int id, int kh)
        {
            var ghcontext = new GioHang();
            int count = ghcontext.DeleteGH(id, kh);

            if (count > 0)
                TempData["SuccessMessage"] = "Đã xóa sản phẩm khỏi giỏ!";
            else
                TempData["ErrorMessage"] = "Không tìm thấy sản phẩm để xóa!";

            return RedirectToAction("Cart");
        }


        public IActionResult InsertCart2(int id1, int id2, int MaLoaiSP)
        {
            var kh = GetKhachHangFromSession();
            if (kh.MaKH == null || kh.MaKH <= 0)
            {
                TempData["ErrorMessage"] = "Vui lòng đăng nhập!";
                return RedirectToAction("DangNhap", "TaiKhoan");
            }

            return XuLyThemGio(kh.MaKH, id1, 1, "ChiTietSP", new { t = id2, l = MaLoaiSP });
        }

        public IActionResult InsertCart3(int id)
        {
            var kh = GetKhachHangFromSession();
            if (kh.MaKH == null || kh.MaKH <= 0)
            {
                TempData["ErrorMessage"] = "Vui lòng đăng nhập!";
                return RedirectToAction("DangNhap", "TaiKhoan");
            }

            return XuLyThemGio(kh.MaKH, id, 1, "", null, true);
        }
        [HttpPost]
        public IActionResult UpdateSoLuong(int maKH, int maTTSP, int soLuong)
        {
            var ghcontext = new GioHang();

            if (soLuong <= 0)
            {
                return Json(new { success = false, error = "Số lượng phải lớn hơn 0!" });
            }

            var sp = new GioHang()
            {
                MaKH = maKH,
                MaTTSP = maTTSP,
                SoLuong = soLuong
            };

            bool result = ghcontext.UpdateGH(sp) > 0;

            return Json(new { success = result });
        }

        private IActionResult XuLyThemGio(int maKH, int maTTSP, int soLuong, string redirectAction, object redirectParams, bool quayVeTrangChu = false)
        {
            var ghcontext = new GioHang();
            var listgh = ghcontext.ListGioHang(maKH);
            var tonKho = ghcontext.GetProductStock(maTTSP);

            var sp = listgh.FirstOrDefault(x => x.MaKH == maKH && x.MaTTSP == maTTSP);

            if (sp != null)
            {
                if (sp.SoLuong + soLuong > tonKho)
                {
                    TempData["ErrorMessage"] = "Số lượng tồn kho không đủ!";
                    return RedirectToAction(redirectAction, redirectParams);
                }

                sp.SoLuong += soLuong;
                ghcontext.UpdateGH(sp);
                TempData["SuccessMessage"] = "Đã cập nhật số lượng sản phẩm!";
            }
            else
            {
                if (soLuong > tonKho)
                {
                    TempData["ErrorMessage"] = "Sản phẩm không đủ hàng tồn kho!";
                    return RedirectToAction(redirectAction, redirectParams);
                }

                var g = new GioHang() { MaKH = maKH, MaTTSP = maTTSP, SoLuong = soLuong };
                ghcontext.InsertGH(g);
                TempData["SuccessMessage"] = "Đã thêm sản phẩm vào giỏ!";
            }

            if (quayVeTrangChu)
                return RedirectToAction("Index", "Home");

            return RedirectToAction(redirectAction, redirectParams);
        }
        
        // Trong GioHangController.cs
        [HttpPost]
        public IActionResult XacNhanHD(List<int> MaTTSPs, List<string> TenSPs, List<long> Gias, List<int> SoLuongs, int MaKM_TuForm_GioHang) // Thêm MaKM từ giỏ hàng nếu có
        {
            // Kiểm tra dữ liệu đầu vào từ form giỏ hàng (quan trọng!)
            if (MaTTSPs == null || !MaTTSPs.Any())
            {
                TempData["ErrorMessage"] = "Giỏ hàng của bạn đang trống!";
                return RedirectToAction("Cart");
            }

            if (MaTTSPs.Count != SoLuongs.Count || MaTTSPs.Count != TenSPs.Count || MaTTSPs.Count != Gias.Count)
            {
                TempData["ErrorMessage"] = "Dữ liệu giỏ hàng không hợp lệ. Vui lòng thử lại.";
                return RedirectToAction("Cart");
            }

            var danhSachSanPhamDeXacNhan = new List<object>();
            for (int i = 0; i < MaTTSPs.Count; i++)
            {
                danhSachSanPhamDeXacNhan.Add(new
                {
                    MaTTSP = MaTTSPs[i],
                    TenSP = TenSPs[i],      // Tên sản phẩm từ giỏ hàng
                    Gia = Gias[i],          // Giá TẠI GIỎ HÀNG (có thể là giá KM riêng của SP)
                    SoLuong = SoLuongs[i]
                });
            }

            // Lưu các thông tin cần thiết vào TempData để HoaDonController có thể lấy
            TempData["DanhSachSanPhamXacNhan"] = JsonConvert.SerializeObject(danhSachSanPhamDeXacNhan);
            TempData["MaKMGioHang"] = MaKM_TuForm_GioHang; // Truyền mã KM đã chọn ở giỏ hàng

            // Chuyển hướng đến action XacNhanHD của HoaDonController
            return RedirectToAction("XacNhanHD", "HoaDon");
        }

    }
}



