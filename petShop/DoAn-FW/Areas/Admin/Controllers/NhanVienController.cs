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
    public class NhanVienController : Controller
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
        public IActionResult Index(string searchTerm, int page = 1)
        {
            DataNV();
            var DSNV = context.GetDSNV(page);

            ViewData["pages"] = DSNV.pages;
            ViewData["page"] = DSNV.page;
            ViewData["searchTerm"] = searchTerm; // Lưu từ khóa tìm kiếm để hiển thị lại trong form

            if (!String.IsNullOrEmpty(searchTerm))
            {
                // Chuyển đổi searchTerm thành chữ thường để tìm kiếm không phân biệt hoa thường
                string searchTermLower = searchTerm.ToLower();

                List<NhanVien> Lkq = DSNV.nv.Where(nv =>
                    nv.TenNV.ToLower().Contains(searchTermLower) ||
                    nv.MaNV != null && nv.MaNV.ToString().ToLower().Contains(searchTermLower) ||
                    (nv.DiaChi != null && nv.DiaChi.ToLower().Contains(searchTermLower)) ||
                    (nv.SDT != null && nv.SDT.Contains(searchTerm))
                ).ToList();

                if (Lkq.Count == 0)
                {
                    ViewData["ThongBao"] = "1"; // Không tìm thấy kết quả
                }
                return View(Lkq);
            }

            ViewData["ThongBao"] = "0";
            return View(DSNV.nv);
        }


        public ActionResult Them(NhanVien nv)
        {
            DataNV();

            // Chỉ kiểm tra các trường bắt buộc
            if (nv.TenNV == null || nv.NgayVL == null || nv.SDT == null
                || nv.DiaChi == null || nv.Email == null)
            {
                ViewData["thongbao"] = "Không được bỏ trống các trường cơ bản";
                return View();
            }

            // Gán giá trị mặc định cho các trường không bắt buộc
            if (nv.LoaiNV == null)
                nv.LoaiNV = "Nhân viên"; // Hoặc giá trị mặc định phù hợp

            if (nv.CMND == null)
                nv.CMND = ""; // Hoặc giá trị mặc định phù hợp

            // Tiếp tục xử lý thêm mới
            int count = context.ThemNhanVien(nv);
            if (count > 0)
                ViewData["thongbao"] = "Thêm thành công";
            else
                ViewData["thongbao"] = "Thêm không thành công";

            return View();
        }
        public ActionResult ThemNV()
        {
            DataNV();
            return View();
        }
        public ActionResult SuaNV(int ID)
        {
            DataNV();

            return View(context.GetNV(ID));
        }

        public ActionResult Xoa(int ID, int page)
        {
            DataNV();
            if (ID != 0)
            {
                int count = context.DeleteNV(ID);
                if (count > 0)
                    ViewData["trangthai"] = "Xóa thành công";
                else
                    ViewData["trangthai"] = "Xóa không thành công";
            }
            var DSNV = context.GetDSNV(page);

            ViewData["pages"] = DSNV.pages;

            ViewData["page"] = DSNV.page;

            return View("Index", DSNV.nv);
        }
        public ActionResult Update(NhanVien nv)
        {
            DataNV();
            if (nv != null)
            {
                if (nv.TenNV == null || nv.NgayVL == null || nv.SDT == null|| nv.LoaiNV == null ||
                    nv.CMND == null || nv.DiaChi == null || nv.Email == null)
                {
                    ViewData["thongbao"] = "Không được bỏ trống";
                }
                else
                {
                    int count = context.UpdateNV(nv);
                    if (count > 0)
                        ViewData["thongbao"] = "Cập nhật thành công";
                    else
                        ViewData["thongbao"] = "Cập nhật không thành công";
                }
            }
            else
            {
                ViewData["thongbao"] = "Không được bỏ trống";
            }
            return View("SuaNV", context.GetNV(nv.MaNV));
        }    
    }
}
