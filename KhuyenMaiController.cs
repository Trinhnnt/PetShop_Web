using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_projectframeword_admin.Models;

namespace DoAn_FW.Areas.Admin.Controllers
{
    public class KhuyenMaiController : Controller
    {
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

        public IActionResult LietKeKhuyenMai()
        {
            DataNV();
            return View(context.GetKhuyenMais());
        }

        public IActionResult ViewKhuyenMai(string Id)
        {
            DataNV();
            KhuyenMai km = context.ViewKhuyenMai(Id);
            ViewData.Model = km;
            return View();
        }

        public IActionResult DeleteKhuyenMai(string Id)
        {
            DataNV();
            int[] count = context.XoaKhuyenMai(Id);
            if (count != null && count.Length > 0 && count[0] > 0)
                ViewData["thongbao"] = "Xóa thành công";
            else
                ViewData["thongbao"] = "Xóa không thành công";
            return View("LietKeKhuyenMai", context.GetKhuyenMais());
        }

        public IActionResult UpdateKhuyenMai(KhuyenMai km)
        {
            var errors = new List<string>();
            // Validate các trường dữ liệu
            if (km.SoPTKM <= 0) errors.Add("Phải nhập số % khuyến mãi lớn hơn 0.");
            if (km.TuNgay == null) errors.Add("Chưa chọn ngày bắt đầu.");
            if (km.DenNgay == null) errors.Add("Chưa chọn ngày kết thúc.");
            if (km.TuNgay != null && km.DenNgay != null && km.DenNgay < km.TuNgay) errors.Add("Ngày kết thúc không được nhỏ hơn ngày bắt đầu.");
            if (km.TTienToiThieu < 0) errors.Add("Số tiền tối thiểu phải lớn hơn hoặc bằng 0.");
            // TODO: Kiểm tra sản phẩm giảm giá nếu có (tuỳ vào cách truyền model)



            DataNV();
            int count = context.UpdateKhuyenMai(km);
            if (count > 0)
                ViewData["thongbao"] = "Update thành công";
            else
                ViewData["thongbao"] = "Update không thành công";
            return View("LietKeKhuyenMai", context.GetKhuyenMais());
        }

        public IActionResult InsertKhuyenMai(KhuyenMai km)
        {
            var errors = new List<string>();
            // Validate các trường dữ liệu
            if (km.SoPTKM <= 0) errors.Add("Phải nhập số % khuyến mãi lớn hơn 0.");
            if (km.TuNgay == null) errors.Add("Chưa chọn ngày bắt đầu.");
            if (km.DenNgay == null) errors.Add("Chưa chọn ngày kết thúc.");
            if (km.TuNgay != null && km.DenNgay != null && km.DenNgay < km.TuNgay) errors.Add("Ngày kết thúc không được nhỏ hơn ngày bắt đầu.");
            if (km.TTienToiThieu < 0) errors.Add("Số tiền tối thiểu phải lớn hơn hoặc bằng 0.");

            if (errors.Count > 0)
            {
                ViewData["errors"] = string.Join("\n", errors);
                var danhSachSP = context.GetSanPhams();
                ViewBag.DanhSachSanPham = danhSachSP ?? new List<SanPham>();
                DataNV();
                var lastKM = context.GetKhuyenMais().OrderByDescending(k => k.MaKM).FirstOrDefault();
                ViewBag.NextMaKM = (lastKM != null) ? lastKM.MaKM + 1 : 1;
                return View("EnterKhuyenMai", km);
            }

            int count = context.InsertKhuyenMai(km);
            ViewData.Model = km;

            if (errors.Count > 0)
            {
                ViewData["thongbao"] = "Thêm thành công";
            }
            else
                ViewData["thongbao"] = "Thêm không thành công";

            return View("LietKeKhuyenMai", context.GetKhuyenMais());
        }

        public IActionResult EnterKhuyenMai()
        {
            var danhSachSP = context.GetSanPhams();
            ViewBag.DanhSachSanPham = danhSachSP ?? new List<SanPham>();
            DataNV();
            var lastKM = context.GetKhuyenMais().OrderByDescending(k => k.MaKM).FirstOrDefault();
            ViewBag.NextMaKM = (lastKM != null) ? lastKM.MaKM + 1 : 1;
            return View();
        }
    }
}


