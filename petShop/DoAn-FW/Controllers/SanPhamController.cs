using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DoAn_FW.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Web_ProjectFrameWork.Controllers
{
    public class SanPhamController : Controller
    {
        //Kiểm tra đăng nhập và lấy thông tin khách hàng gán vào ViewBag.KH
        public void DataKH()
        {
            var KH = new KhachHang();
            if (HttpContext.Session.GetString("KH") != null)
            {
                KH = JsonConvert.DeserializeObject<KhachHang>(HttpContext.Session.GetString("KH"));
            }
            this.ViewBag.KH = KH;
        }
        //Kiểm tra đăng nhập và lấy thông tin giỏ hàng của khách hàng gán vào ViewBag.Cart
        public void DataCart()
        {
            var KH = new KhachHang();
            if (HttpContext.Session.GetString("KH") != null)
            {
                KH = JsonConvert.DeserializeObject<KhachHang>(HttpContext.Session.GetString("KH"));
            }
            var context = new GioHang();
            var cart = context.ListGioHang(KH.MaKH);
            this.ViewBag.cart = cart;
        }

        public IActionResult DanhSachSP(int pg = 1)
        {
            var context = new SanPhamContext();
            List<object> list = context.ListSPMoiNhat();
            int pageSize = 6;
            if (pg < 1) pg = 1;
            int recsCount = list.Count();
            var pager = new Pager(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            var data = list.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;

            ViewData["list"] = data;
            ViewData["ListLoaiSP"] = context.ListLoaiSP();
            return View();
        }

        [HttpGet]
        public IActionResult Products(
        [FromQuery] List<string> MaLoaiSP,
        [FromQuery] List<string> MauSac,     // Đổi từ Ram
        [FromQuery] List<string> DoTuoi,     // Đổi từ Memory
        [FromQuery] List<string> XuatXu,     // Đổi từ ScreenSize
        [FromQuery] List<string> MaTH,
        [FromQuery] string search,
        [FromQuery] string sortOrder,
        int pg = 1)
        {
            DataKH();
            DataCart();
            var context = new SanPhamContext();

            var query = context.BuildFilterQuery(out var parameters, MaLoaiSP, MauSac, DoTuoi, XuatXu, MaTH, search, sortOrder);

            int pageSize = 9;
            List<object> list;

            list = context.FetchFilteredProducts(query, parameters);

            if (pg < 1) pg = 1;
            int recsCount = list.Count();
            var pager = new Pager(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            var data = list.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;

            ViewData["list"] = data;
            ViewData["ListLoaiSP"] = context.ListLoaiSP();
            ViewData["ListHang"] = context.ListHang();
            ViewData["ListMauSac"] = context.ListMauSac();
            ViewData["ListDoTuoi"] = context.ListDoTuoi();
            ViewData["ListXuatXu"] = context.ListXuatXu();
            return View();
        }


        [HttpGet]
        public IActionResult ChiTietSP(int t, int l)
        {
            DataKH();
            DataCart();
            var context = new SanPhamContext();
            ViewData["CTSP"] = context.ChiTietSP(t);
            ViewData["ListDT"] = context.FilterSanPham(l, t);
            ViewData["ListBL"] = context.BinhLuans(t);
            ViewData["ListPK"] = context.FilterSanPham(3); //"Tạo một list phụ kiện có thể tìm thấy được với MaLoaiSP = 3"
            return View();
        }

      
        public IActionResult PhuKien(int pg = 1, int? math = null, string? search = null)
        {
            DataKH();
            DataCart();
            var context = new SanPhamContext();
            int pageSize = 8;
            List<object> list;

            if (math != null)
            {
                list = context.SPLocTheoHangSP(3, math); // Lọc theo hãng
            }
            else if (search != null)
            {
                list = context.SearchPK(search); // Tìm kiếm sản phẩm phụ kiện
                pageSize = 100;
            }
            else
            {
                list = context.FilterSanPham(3); // Lấy tất cả sản phẩm loại phụ kiện
            }

            if (pg < 1) pg = 1;
            int recsCount = list.Count();
            var pager = new Pager(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            var data = list.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;

            ViewData["list"] = data;
            ViewData["math"] = math;
            ViewData["ListLoaiSP"] = context.ListLoaiSP();
            ViewData["ListHang"] = context.ListHang(3);
            return View();
        }
    }
}