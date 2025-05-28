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
    public class SanPhamController : Controller
    {
       
        StoreContext context = new StoreContext("Data Source=localhost;Initial Catalog=website_petShop;Integrated Security=True");

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
        public IActionResult LietKeSanPham(int page = 1)
        {
            DataNV();
            var DSSP = context.GetSanPhams(page);
            foreach (var item in DSSP.sp.Take(3))
            {
                System.Diagnostics.Debug.WriteLine($"MaTTSP: {item.MaTTSP}, TenSP: {item.TenSP}, TenLoaiSP: {item.TenLoaiSP}, TenTH: {item.TenTH}");
            }
            ViewData["pages"] = DSSP.pages;
            ViewData["page"] = DSSP.page;

            return View(DSSP.sp);

        }
        public IActionResult ViewSanPham(string Id)
        {
            DataNV();
            SanPham sp = context.ViewSanPham(Id);
            ViewData.Model = sp;
            List<LoaiSanPham> DSLSP = context.GetLoaiSanPhams();
            ViewBag.DSLSP = DSLSP;
            List<ThuongHieu> DSTH = context.GetThuongHieus();
            ViewBag.DSTH = DSTH;
            return View();
        }
        public IActionResult DeleteSanPham(string Id, int page)
        {
            DataNV();
            int[] count = context.XoaSanPham(Id);
            var DSSP = context.GetSanPhams(page);
            ViewData["pages"] = DSSP.pages;
            ViewData["page"] = DSSP.page;
            return View("LietKeSanPham", DSSP.sp);
        }
        public IActionResult UpdateSanPham(SanPham ttsp, int page=1)
        {
            int count;
            DataNV();
            count = context.UpdateSanPham(ttsp);
            if (count > 0)
                ViewData["thongbao"] = "Update thành công";
            else
                ViewData["thongbao"] = "Update không thành công";
            var DSSP = context.GetSanPhams(page);
            ViewData["pages"] = DSSP.pages;
            ViewData["page"] = DSSP.page;
            return View("LietKeSanPham", DSSP.sp);
        }
        public IActionResult InsertSanPham(SanPham ttsp, int page = 1 )
        {
            int count;
            DataNV();
            count = context.InsertSanPham(ttsp);
            ViewData.Model = ttsp;
            if (count > 0)
                ViewData["thongbao"] = "Thêm sản phẩm thành công";
            else
                ViewData["thongbao"] = "Thêm sản phẩm không thành công";
            var DSSP = context.GetSanPhams(page);
            ViewData["pages"] = DSSP.pages;
            ViewData["page"] = DSSP.page;
            return View("LietKeSanPham", DSSP.sp);
        }
        public IActionResult EnterSanPham()
        {
            DataNV();
            List<LoaiSanPham> DSLSP = context.GetLoaiSanPhams();
            ViewBag.DSLSP = DSLSP;
            List<ThuongHieu> DSTH = context.GetThuongHieus();
            ViewBag.DSTH = DSTH;
            return View();
        }
        public IActionResult FindSanPham(string ten, int page = 1)
        {
            DataNV();

            // Kiểm tra nếu ten là null hoặc rỗng
            if (string.IsNullOrEmpty(ten))
            {
                var DSSP = context.GetSanPhams(page);
                ViewData["pages"] = DSSP.pages;
                ViewData["page"] = DSSP.page;

                // Chuyển hướng về trang danh sách sản phẩm
                return View("LietKeSanPham", DSSP.sp);
            }

            var result = context.FindSanPham(ten, page);

            // Debug để kiểm tra kết quả
            System.Diagnostics.Debug.WriteLine($"Tìm kiếm với từ khóa: {ten}, Trang: {result.page}/{result.pages}");

            ViewData["ten"] = ten;
            ViewData["page"] = result.page;
            ViewData["pages"] = result.pages;

            return View(result.sp);
        }

        public IActionResult QuanLyLoaiSP()
        {
            DataNV();
            List<LoaiSanPham> DSLSP = context.GetLoaiSanPhams();
            ViewBag.DSLSP = DSLSP;
            return View();
        }

        public IActionResult LietKeLoaiSanPham()
        {
            DataNV();
            return View(context.GetLoaiSanPhams());
        }
        public IActionResult InsertLoaiSP(string TENLOAISP)
        {
            int count;
            DataNV();
            count = context.InsertLoaiSanPham(TENLOAISP);

            if (count > 0)
                TempData["thongbao"] = "Thêm loại sản phẩm thành công";
            else
                TempData["thongbao"] = "Thêm loại sản phẩm không thành công";
            List<LoaiSanPham> DSLSP = context.GetLoaiSanPhams();
            ViewBag.DSLSP = DSLSP;
            return View("QuanLyLoaiSP");
        }
        public IActionResult QuanLyThuongHieu()
        {
            DataNV();
            List<ThuongHieu> DSTH = context.GetThuongHieus();
            ViewBag.DSTH = DSTH;
            return View();
        }
        public IActionResult InsertTH(string TENTH)
        {
            int count;
            DataNV();

            ThuongHieu th = new ThuongHieu { TENTH = TENTH };
            count = context.InsertThuongHieu(th);

            if (count > 0)
                TempData["thongbao"] = "Thêm thương hiệu thành công";
            else
                TempData["thongbao"] = "Thêm thương hiệu không thành công";
            List<ThuongHieu> DSTH = context.GetThuongHieus();
            ViewBag.DSTH = DSTH;
            return View("QuanLyThuongHieu");
        }
        public IActionResult DeleteLoaiSP(string Id)
        {
            DataNV();
            int[] count = context.XoaLoaiSanPham(Id);

            // Lấy danh sách loại sản phẩm sau khi xóa
            var ListLoaiSP = context.GetLoaiSanPhams();

            // Sửa tên ViewBag để khớp với tên trong view
            ViewBag.DSLSP = ListLoaiSP;

            // Thêm thông báo (tùy chọn)
            if (count[0] > 0)
                TempData["thongbao"] = "Xóa loại sản phẩm thành công";
            else
                TempData["thongbao"] = "Xóa loại sản phẩm không thành công";

            return View("QuanLyLoaiSP");
        }

        public IActionResult DeleteTH(string Id)
        {
            DataNV();
            int[] count = context.XoaThuongHieu(Id);
            var ListTH = context.GetThuongHieus();
            ViewBag.DSTH = ListTH;
            if (count[0] > 0)
                TempData["thongbao"] = "Xóa thương hiệu thành công";
            else
                TempData["thongbao"] = "Xóa thương hiệu không thành công";
            return View("QuanLyThuongHieu");
        }
        
        public JsonResult GetLoaiSanPhamJson()
        {
            var loaiSanPhams = context.GetLoaiSanPhams();
            return Json(loaiSanPhams);
        }

        
        public JsonResult GetThuongHieuJson()
        {
            var thuongHieus = context.GetThuongHieus();
            return Json(thuongHieus);
        }

    }
}
