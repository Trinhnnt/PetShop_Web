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
            if (g == null || g.MaTTSP <= 0 || g.SoLuong <= 0)
            {
                TempData["ErrorMessage"] = "Vui lòng chọn sản phẩm và số lượng hợp lệ.";
                return RedirectToAction("ChiTietSP", "SanPham", new { t = g?.MaTTSP ?? 0, l = MaLoaiSP });
            }

            var kh = GetKhachHangFromSession();
            if (kh.MaKH <= 0)
            {
                TempData["ErrorMessage"] = "Vui lòng đăng nhập.";
                return RedirectToAction("DangNhap", "TaiKhoan");
            }

            var ghcontext = new GioHang();
            var tonKho = ghcontext.GetProductStock(g.MaTTSP);
            var gioHangHienTai = ghcontext.ListGioHang(kh.MaKH)
                                           .FirstOrDefault(x => x.MaTTSP == g.MaTTSP);

            if (gioHangHienTai != null)
            {
                int tongSL = gioHangHienTai.SoLuong + g.SoLuong;
                if (tongSL > tonKho)
                {
                    TempData["ErrorMessage"] = "Số lượng trong kho không đủ.";
                    return RedirectToAction("ChiTietSP", "SanPham", new { t = g.MaTTSP, l = MaLoaiSP });
                }

                gioHangHienTai.SoLuong = tongSL;
                ghcontext.UpdateGH(gioHangHienTai);
            }
            else
            {
                if (g.SoLuong > tonKho)
                {
                    TempData["ErrorMessage"] = "Sản phẩm không đủ hàng tồn.";
                    return RedirectToAction("ChiTietSP", "SanPham", new { t = g.MaTTSP, l = MaLoaiSP });
                }

                g.MaKH = kh.MaKH;
                ghcontext.InsertGH(g);
            }

            TempData["SuccessMessage"] = "Đã thêm vào giỏ hàng.";
            return RedirectToAction("ChiTietSP", "SanPham", new { t = g.MaTTSP, l = MaLoaiSP });
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
        [HttpPost]
        [HttpPost]
        public IActionResult UpdateSoLuong(int maKH, int maTTSP, int soLuong)
        {
            var ghcontext = new GioHang();

            if (soLuong <= 0)
            {
                return Json(new { success = false, error = "Số lượng phải lớn hơn 0!" });
            }

            int tonKho = ghcontext.GetProductStock(maTTSP);

            if (soLuong > tonKho)
            {
                return Json(new { success = false, error = $"Số lượng vượt quá tồn kho! Chỉ còn {tonKho} sản phẩm." });
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
        [HttpPost]
        [HttpPost]
        [HttpPost]
        public IActionResult XacNhanHD(List<int> MaTTSP, List<string> TenSP, List<long> Gia, List<int> SoLuong)
        {
            var ghcontext = new GioHang();
            var list = new List<CTHD>();

            for (int i = 0; i < MaTTSP.Count; i++)
            {
                int tonKho = ghcontext.GetProductStock(MaTTSP[i]); // Lấy tồn kho

                if (SoLuong[i] > tonKho)
                {
                    TempData["ErrorMessage"] = $"Sản phẩm '{TenSP[i]}' chỉ còn {tonKho} sản phẩm trong kho. Vui lòng chọn lại.";
                    return RedirectToAction("Cart", "GioHang");
                }

                list.Add(new CTHD
                {
                    MaTTSP = MaTTSP[i],
                    TenSP = TenSP[i],
                    Gia = Gia[i],
                    SoLuong = SoLuong[i]
                });
            }

            ViewBag.XacNhanList = list;
            return View("XacNhanHD");
        }



    }
}

