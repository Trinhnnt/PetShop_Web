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
    public class KhachHangController : Controller
    {
        // GET: KhachHangController
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

        public ActionResult Index(string SearchString, int page = 1)
        {
            DataNV();

            // Lưu lại chuỗi tìm kiếm để hiển thị lại trên form
            ViewData["CurrentSearch"] = SearchString;

            // Gọi hàm GetDSKH với tham số tìm kiếm
            var DSKH = context.GetDSKH(page, SearchString);

            ViewData["pages"] = DSKH.pages;
            ViewData["page"] = DSKH.page;

            // Kiểm tra kết quả tìm kiếm
            ViewData["ThongBao"] = (DSKH.kh.Count == 0) ? "1" : "0";

            return View(DSKH.kh);
        }


        public ActionResult Xoa(int ID, int page)
        {
            DataNV();
            if (ID != 0)
            {
                int count = context.DeleteKH(ID);
                if (count > 0)
                    ViewData["trangthai"] = "Xóa thành công";
                else
                    ViewData["trangthai"] = "Xóa không thành công";
            }
            var DSNV = context.GetDSKH(page);

            ViewData["pages"] = DSNV.pages;

            ViewData["page"] = DSNV.page;

            return View("Index", DSNV.kh);
        }
        public ActionResult XemKH(int ID)
        {
            DataNV();

            return View(context.GetKH(ID));
        }
    }
}
