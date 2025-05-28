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
    public class TinTucController : Controller
    {
        StoreContext context = new StoreContext("Data Source=localhost;Initial Catalog=website_petShop;Integrated Security=True");

        // GET: TinTucController
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
        public ActionResult Index(string? id)
        {
            DataNV();
            int count = 0;
            if (id != null)
            {
                count = context.XoaTT(id);
            }
            
            var ListTT = context.GetTinTucs();
            return View(ListTT);
        }
        public ActionResult ThemTinTuc(TinTuc tt)
        {
            DataNV();
            return View();
        }

        public ActionResult InsertTinTuc(TinTuc tt)
        {
            DataNV();
            int count;

            //StoreContext context = HttpContext.RequestServices.GetService(typeof(firstWeb.Models.StoreContext)) as StoreContext;
            tt.hinhbia =   tt.hinhbia;
            count = context.ThemTT(tt);
            if (count > 0)
                ViewData["thongbao"] = "Insert thành công";
            else
                ViewData["thongbao"] = "Insert không thành công";
            return Redirect("/Admin/TinTuc/Index");
        }
        public ActionResult SuaTinTuc(int id)
        {
            DataNV();
            this.ViewBag.TTuc = context.GetTinTucTheoMa(id);
            return View();
        }
        public ActionResult UpdateTinTuc(TinTuc tt)
        {
            DataNV();
            int count;
            tt.hinhbia =  tt.hinhbia;
            //StoreContext context = HttpContext.RequestServices.GetService(typeof(firstWeb.Models.StoreContext)) as StoreContext;
            count = context.SuaTT(tt);
            if (count > 0)
                ViewData["thongbao"] = "Insert thành công";
            else
                ViewData["thongbao"] = "Insert không thành công";
            return Redirect("/Admin/TinTuc/Index");
        }
        public ActionResult TimKiem(string keyword)
        {
            DataNV();

            // Nếu không có từ khóa, trả về tất cả bài viết
            if (string.IsNullOrEmpty(keyword))
            {
                var allTinTuc = context.GetTinTucs();
                return View("Index", allTinTuc);
            }

            // Tìm kiếm tin tức theo tiêu đề
            var ketQua = context.GetTinTucs().Where(t => t.tieude.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();

            // Truyền dữ liệu tìm kiếm vào ViewBag để hiển thị lại từ khóa tìm kiếm
            ViewBag.Keyword = keyword;

            // Trả về view Index với kết quả tìm kiếm
            return View("Index", ketQua);
        }

    }
}
