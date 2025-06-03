using DoAn_FW.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json; // Đảm bảo bạn đã cài đặt package Newtonsoft.Json
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks; // Cần cho các hàm Async nếu bạn gọi API tỉnh thành

namespace DoAn_FW.Controllers
{
    public class HoaDonController : Controller
    {
        // ... (các hàm DataKH, GetDataKH, DataCart của bạn giữ nguyên) ...
        public KhachHang GetDataKH()
        {
            var KH = new KhachHang();
            if (HttpContext.Session.GetString("KH") != null)
            {
                KH = JsonConvert.DeserializeObject<KhachHang>(HttpContext.Session.GetString("KH"));
            }
            this.ViewBag.KH = KH; // Gán vào ViewBag để XacNhanHD.cshtml có thể dùng
            return KH;
        }


        // Action này sẽ được gọi khi submit từ Cart.cshtml
        [HttpPost]
        public IActionResult XacNhanHD(List<int> MaTTSP, List<long> SoLuong_TuForm, int MaKM_TuForm)
        {
            KhachHang KH = GetDataKH();
            if (KH == null || KH.MaKH == 0)
            {
                TempData["ErrorMessage"] = "Vui lòng đăng nhập để tiếp tục.";
                return RedirectToAction("DangNhap", "TaiKhoan"); // Hoặc trang đăng nhập của bạn
            }
            ViewBag.KH = KH; // Cho XacNhanHD.cshtml sử dụng

            // ----- KIỂM TRA DỮ LIỆU ĐẦU VÀO -----
            if (MaTTSP == null || SoLuong_TuForm == null || MaTTSP.Count == 0 || MaTTSP.Count() != SoLuong_TuForm.Count())
            {
                Console.WriteLine($"Lỗi dữ liệu form XacNhanHD không nhất quán hoặc rỗng: MaTTSP.Count() = {MaTTSP?.Count()}, SoLuong_TuForm.Count() = {SoLuong_TuForm?.Count()}");
                TempData["ErrorMessage"] = "Giỏ hàng trống hoặc dữ liệu không hợp lệ. Vui lòng thử lại.";
                return RedirectToAction("Cart", "GioHang");
            }

            KhuyenMai kmDaKiemTra = null;
            if (MaKM_TuForm > 0)
            {
                var kmcontext = new KhuyenMai();
                KhuyenMai kmTam = kmcontext.GetKM(MaKM_TuForm);
                if (kmTam != null && kmTam.MaKM > 0)
                {
                    DateTime ngayHienTai = DateTime.Now.Date;
                    if (ngayHienTai >= kmTam.TuNgay.Date && ngayHienTai <= kmTam.DenNgay.Date)
                    {
                        kmDaKiemTra = kmTam;
                    }
                    else
                    {
                        Console.WriteLine($"Khuyen mai MaKM {MaKM_TuForm} da het han hoac chua bat dau.");
                    }
                }
            }
            this.ViewBag.KM = kmDaKiemTra;

            List<CTHD> listcthdView = new List<CTHD>();
            var spContext = new ThongTinSP();

            for (var i = 0; i < MaTTSP.Count(); i++)
            {
                ThongTinSP sanPhamChiTiet = spContext.GetThongTinSPByMa(MaTTSP[i]);

                if (sanPhamChiTiet != null)
                {
                    long donGiaGocHienThi = sanPhamChiTiet.Gia;

                    if (sanPhamChiTiet.TenSP.ToLower().Contains("bàn chải lông mèo"))
                    {
                        donGiaGocHienThi = 208000;
                    }

                    listcthdView.Add(new CTHD
                    {
                        MaTTSP = MaTTSP[i],
                        SoLuong = SoLuong_TuForm[i],
                        TenSP = sanPhamChiTiet.TenSP,
                        Gia = donGiaGocHienThi,
                    });
                }
                else
                {
                    Console.WriteLine($"Sản phẩm với MaTTSP: {MaTTSP[i]} không tìm thấy trong CSDL khi xác nhận hóa đơn.");
                    TempData["ErrorMessage"] = $"Không tìm thấy thông tin cho một sản phẩm trong giỏ. Vui lòng kiểm tra lại.";
                    return RedirectToAction("Cart", "GioHang");
                }
            }

            this.ViewBag.listcthd = listcthdView;
            // Thay vì `return View(listcthdView);` nếu model của XacNhanHD.cshtml không phải là List<CTHD>
            return View("XacNhanHD");
        }


        // Action này sẽ được gọi khi submit từ XacNhanHD.cshtml
        [HttpPost]
        public async Task<IActionResult> InsertHD(List<int> MaTTSP, List<long> SoLuong, int MaKM_DaApDung, string Tinh, string Quan, string DiaChi)
        {
            KhachHang KH = GetDataKH();
            if (KH == null || KH.MaKH == 0)
            {
                TempData["ErrorMessage"] = "Phiên đăng nhập hết hạn hoặc bạn chưa đăng nhập.";
                return RedirectToAction("DangNhap", "TaiKhoan");
            }

            // ----- KIỂM TRA DỮ LIỆU ĐẦU VÀO -----
            if (MaTTSP == null || SoLuong == null || MaTTSP.Count == 0 || MaTTSP.Count() != SoLuong.Count())
            {
                TempData["ErrorMessage"] = "Không có sản phẩm để tạo hóa đơn.";
                // Cần trả về view XacNhanHD với dữ liệu đã có để người dùng không bị mất thông tin
                // Điều này đòi hỏi bạn phải có cách lấy lại/giữ lại thông tin listcthd và KM
                // Cách đơn giản nhất là redirect về bước trước đó, ví dụ trang giỏ hàng
                return RedirectToAction("Cart", "GioHang");
            }
            if (string.IsNullOrEmpty(Tinh) || string.IsNullOrEmpty(Quan) || string.IsNullOrEmpty(DiaChi))
            {
                TempData["ErrorMessage"] = "Vui lòng điền đầy đủ thông tin địa chỉ giao hàng.";
                // Cần trả về view XacNhanHD với dữ liệu đã có
                // Cách xử lý tương tự như trên, hoặc reload view với dữ liệu TempData
                string danhSachSanPhamJson = TempData["DanhSachSanPhamTruocDo"] as string; // Bạn cần lưu danh sách này vào TempData trước khi gọi XacNhanHD view
                int maKMTruocDo = TempData["MaKMTruocDo"] != null ? (int)TempData["MaKMTruocDo"] : 0;

                if (!string.IsNullOrEmpty(danhSachSanPhamJson))
                {
                    // Deserialize và gán lại vào ViewBag để trả về View("XacNhanHD")
                    var danhSachTamThoi = JsonConvert.DeserializeObject<List<dynamic>>(danhSachSanPhamJson);
                    KhuyenMai kmTemp = null;
                    if (maKMTruocDo > 0) kmTemp = new KhuyenMai().GetKM(maKMTruocDo);

                    this.ViewBag.KM = kmTemp;
                    this.ViewBag.KH = KH;
                    List<CTHD> listcthdView = new List<CTHD>();
                    var spContext = new ThongTinSP();
                    foreach (var spTam in danhSachTamThoi)
                    {
                        ThongTinSP spDB = spContext.GetThongTinSPByMa(Convert.ToInt32(spTam.MaTTSP));
                        if (spDB != null)
                        {
                            listcthdView.Add(new CTHD
                            {
                                MaTTSP = spDB.MaTTSP,
                                TenSP = spDB.TenSP,
                                Gia = spDB.Gia,
                                SoLuong = Convert.ToInt64(spTam.SoLuong)
                            });
                        }
                    }
                    this.ViewBag.listcthd = listcthdView;
                    ModelState.AddModelError(string.Empty, "Vui lòng điền đầy đủ thông tin địa chỉ.");
                    return View("XacNhanHD");
                }
                return RedirectToAction("Cart", "GioHang"); // Fallback
            }


            string tenTinh = await GetTenTinhByIdAsync(Tinh);
            string tenQuan = await GetTenQuanByIdAsync(Quan, Tinh);
            string diaChiDayDu = $"{DiaChi}, {tenQuan}, {tenTinh}";


            var hdcontext = new HoaDon();
            int maHoaDonMoi = hdcontext.InsertHD(KH.MaKH, MaKM_DaApDung, diaChiDayDu);

            if (maHoaDonMoi > 0)
            {
                var cthdcontext = new CTHD();
                for (var i = 0; i < MaTTSP.Count(); i++)
                {
                    CTHD ct = new CTHD();
                    ct.MaHD = maHoaDonMoi;
                    ct.MaTTSP = MaTTSP[i];
                    ct.SoLuong = SoLuong[i];
                    // ThanhTien sẽ được tính bởi trigger SQL của bạn
                    cthdcontext.InsertCTHD(ct);
                }
                var gioHangContext = new GioHang();
                gioHangContext.XoaGioHangTheoKH(KH.MaKH);

                TempData["SuccessMessage"] = $"Đã tạo hóa đơn #{maHoaDonMoi} thành công!";
                return RedirectToAction("CTHD", "HoaDon", new { mahd = maHoaDonMoi });
            }
            else
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi tạo hóa đơn. Vui lòng thử lại.";
                // Trả về view XacNhanHD với dữ liệu đã có
                // Tương tự như trường hợp lỗi validation địa chỉ ở trên
                string danhSachSanPhamJson = TempData["DanhSachSanPhamTruocDo"] as string;
                int maKMTruocDo = TempData["MaKMTruocDo"] != null ? (int)TempData["MaKMTruocDo"] : 0;
                // ... chuẩn bị lại ViewBag ...
                this.ViewBag.KH = KH; // Đảm bảo KH được truyền
                // this.ViewBag.listcthd = ... ; // Cần lấy lại listcthd
                // this.ViewBag.KM = ...; // Cần lấy lại KM
                return View("XacNhanHD"); // Cần truyền lại model hoặc ViewBag cho view này
            }
        }

        // Hàm helper để gọi API lấy tên tỉnh (ví dụ)
        private async Task<string> GetTenTinhByIdAsync(string provinceId)
        {
            if (string.IsNullOrEmpty(provinceId)) return string.Empty;
            try
            {
                // Bạn cần System.Net.Http và Newtonsoft.Json cho đoạn này
                using (var httpClient = new System.Net.Http.HttpClient())
                {
                    var response = await httpClient.GetStringAsync($"https://provinces.open-api.vn/api/p/{provinceId}");
                    dynamic provinceData = JsonConvert.DeserializeObject(response);
                    return provinceData?.name_with_type ?? provinceData?.name ?? $"Tỉnh ID {provinceId}";
                }
            }
            catch { return $"Tỉnh ID {provinceId}"; }
        }

        private async Task<string> GetTenQuanByIdAsync(string districtId, string provinceId)
        {
            if (string.IsNullOrEmpty(districtId) || string.IsNullOrEmpty(provinceId)) return string.Empty;
            try
            {
                using (var httpClient = new System.Net.Http.HttpClient())
                {
                    var response = await httpClient.GetStringAsync($"https://provinces.open-api.vn/api/p/{provinceId}?depth=2");
                    dynamic provinceData = JsonConvert.DeserializeObject(response);
                    if (provinceData?.districts != null)
                    {
                        foreach (var district in provinceData.districts)
                        {
                            if (district.code.ToString() == districtId)
                            {
                                return district?.name_with_type ?? district?.name ?? $"Huyện ID {districtId}";
                            }
                        }
                    }
                    return $"Huyện ID {districtId}";
                }
            }
            catch { return $"Huyện ID {districtId}"; }
        }


        // ... (các action khác như CTHD, LichSuHD giữ nguyên) ...
        //public IActionResult CTHD(int mahd)
        //{
        //   // DataKH(); // Nên đổi thành
        //   this.ViewBag.KH = GetDataKH();
        //    // DataCart(); // Không cần thiết cho trang chi tiết hóa đơn đã đặt
        //    var hdcontext = new HoaDon();
        //    var cthdcontext = new CTHD();
        //    HoaDon hoaDonHienTai = hdcontext.GetHD(mahd);
        //    if (hoaDonHienTai == null)
        //    {
        //        TempData["ErrorMessage"] = "Không tìm thấy hóa đơn.";
        //        return RedirectToAction("LichSuHD");
        //    }
        //    this.ViewBag.HD = hoaDonHienTai;
        //    this.ViewBag.CTHD = cthdcontext.ListCTHD(mahd); // ListCTHD cần lấy cả TenSP và Gia gốc từ ThongTinSP
        //    return View();
        //}
        private KhachHang GetKhachHangFromSession()
        {
            if (HttpContext.Session.GetString("KH") != null)
            {
                try
                {
                    return JsonConvert.DeserializeObject<KhachHang>(HttpContext.Session.GetString("KH"));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi DeserializeObject KhachHang từ Session trong HoaDonController: {ex.Message}");
                    HttpContext.Session.Remove("KH"); // Xóa session không hợp lệ
                }
            }
            return new KhachHang();
        }

        public IActionResult LichSuHD()
        {
            KhachHang kh = GetKhachHangFromSession();

            if (kh == null || kh.MaKH == 0)
            {
                TempData["ErrorMessage"] = "Vui lòng đăng nhập để xem lịch sử đơn hàng.";
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
            ViewBag.KH = kh;

            var hoaDonModel = new HoaDon(); // Sử dụng model HoaDon.cs của bạn
            List<HoaDon> danhSachHoaDon = new List<HoaDon>();
            try
            {
                // Giả sử phương thức ListHD trong model HoaDon.cs của bạn
                // đã được sửa để xử lý DBNull.Value đúng cách (như ví dụ tôi cung cấp ở trên)
                danhSachHoaDon = hoaDonModel.ListHD(kh.MaKH);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi gọi hoaDonModel.ListHD: {ex.Message}");
                ViewBag.ErrorMessageFromController = "Không thể tải lịch sử đơn hàng lúc này. Vui lòng thử lại sau.";
                // danhSachHoaDon sẽ vẫn là list rỗng
            }

            ViewBag.ListHD = danhSachHoaDon;
            return View(); // Trả về Views/HoaDon/LichSuHD.cshtml
        }

        // ... (các action XacNhanHD, InsertHD, CTHD giữ nguyên như các lần sửa trước) ...
        // ... (GetTenTinhByIdAsync, GetTenQuanByIdAsync nếu bạn dùng cho InsertHD) ...

        // Action CTHD và CapNhatHD cũng cần kiểm tra kh != null && kh.MaKH > 0
        public IActionResult CTHD(int mahd)
        {
            KhachHang kh = GetKhachHangFromSession();
            if (kh == null || kh.MaKH == 0)
            {
                TempData["ErrorMessage"] = "Vui lòng đăng nhập.";
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
            ViewBag.KH = kh;

            var hdContext = new HoaDon();
            var cthdContext = new CTHD();

            HoaDon hoaDonHienTai = null;
            List<CTHD> chiTietDonHang = new List<CTHD>();
            try
            {
                hoaDonHienTai = hdContext.GetHD(mahd); // Phương thức này cũng cần xử lý DBNull.Value
                if (hoaDonHienTai != null && hoaDonHienTai.MaKH == kh.MaKH)
                {
                    // Phương thức ListCTHD trong CTHD.cs cũng cần xử lý DBNull và tính ThanhTien từ giá gốc
                    chiTietDonHang = cthdContext.ListCTHD(mahd);
                }
                else
                {
                    hoaDonHienTai = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy chi tiết hóa đơn MaHD {mahd}: {ex.Message}");
                ViewBag.ErrorMessageFromController = "Không thể tải chi tiết đơn hàng lúc này.";
            }

            if (hoaDonHienTai == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy hóa đơn hoặc bạn không có quyền xem hóa đơn này.";
                return RedirectToAction("LichSuHD");
            }

            ViewBag.HD = hoaDonHienTai;
            ViewBag.CTHD = chiTietDonHang;

            return View();
        }
        public IActionResult CapNhatHD(int mahd, int tthd)
        {
            KhachHang kh = GetKhachHangFromSession();
            if (kh == null || kh.MaKH == 0)
            {
                TempData["ErrorMessage"] = "Vui lòng đăng nhập.";
                return RedirectToAction("DangNhap", "TaiKhoan");
            }

            var hdcontext = new HoaDon();
            HoaDon hdToUpdate = hdcontext.GetHD(mahd); // Lấy hóa đơn để kiểm tra MaKH
            if (hdToUpdate == null || hdToUpdate.MaKH != kh.MaKH)
            {
                TempData["ErrorMessage"] = "Bạn không có quyền cập nhật hóa đơn này.";
                return RedirectToAction("LichSuHD");
            }

            // Chỉ cho phép hủy đơn nếu trạng thái hiện tại là "Chờ xử lý" (TinhTrangHD = 0)
            // và trạng thái mới là "Đã hủy" (tthd = -1)
            // Hoặc cho phép "Đặt lại" nếu trạng thái hiện tại là "Đã hủy" (TinhTrangHD = -1)
            // và trạng thái mới là "Chờ xử lý" (tthd = 0)
            bool coPhepCapNhat = (hdToUpdate.TinhTrangHD == 0 && tthd == -1) || (hdToUpdate.TinhTrangHD == -1 && tthd == 0);

            if (coPhepCapNhat)
            {
                int count = hdcontext.CapNhatHD(mahd, tthd);
                if (count > 0) TempData["SuccessMessage"] = "Cập nhật trạng thái đơn hàng thành công.";
                else TempData["ErrorMessage"] = "Cập nhật trạng thái đơn hàng thất bại.";
            }
            else
            {
                TempData["ErrorMessage"] = "Không thể cập nhật trạng thái đơn hàng này.";
            }
            return RedirectToAction("LichSuHD");
        }
    }
}