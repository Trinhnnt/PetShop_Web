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

    public class GiaoHangController : Controller
    {
        StoreContext context = new StoreContext(@"Data Source=localhost\HAGIANHU;Initial Catalog=website_PetShop;Integrated Security=True");
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
        public ActionResult Index(string Search, int page = 1)
        {
            DataNV();

            if (!String.IsNullOrEmpty(Search) && int.TryParse(Search, out int maHD))
            {
                // Tìm trong tất cả các trang
                var allData = new List<GiaoHang>();
                for (int i = 1; i <= 100; i++) // Giả sử tối đa 100 trang
                {
                    var pageData = context.GetDSGH(i);
                    if (!pageData.gh.Any()) break;

                    allData.AddRange(pageData.gh);
                }

                // Lọc theo MaHD
                var result = allData.Where(x => x.MaHD == maHD).ToList();

                ViewData["pages"] = 1;
                ViewData["page"] = 1;
                ViewData["Search"] = Search;
                ViewData["ThongBao"] = result.Any() ? "0" : "1";

                return View(result);
            }

            // Không search - bình thường
            var Lgh = context.GetDSGH(page);
            ViewData["pages"] = Lgh.pages;
            ViewData["page"] = Lgh.page;
            ViewData["Search"] = "";
            ViewData["ThongBao"] = "0";

            return View(Lgh.gh);
        }
        public ActionResult HoanThanhGH(int MaHD, int MaNV , int page, int STN)
        {
            DataNV();
            context.HoanThanhGH(MaHD, MaNV, STN);
            var Lgh = context.GetDSGH(page);
            ViewData["pages"] = Lgh.pages;

            ViewData["page"] = Lgh.page;
            ViewData["ThongBao"] = "0";

            return View("Index", Lgh.gh);
        }
        public ActionResult Delete(int MAHD, int MANV, int page)
        {
            DataNV();
            if (MAHD != 0 && MANV != 0)
            {
                int count = context.DeleteGH(MAHD, MANV);
                if (count > 0)
                    ViewData["trangthai"] = "Xóa thành công";
                else
                    ViewData["trangthai"] = "Xóa không thành công";
            }
            var DSNV = context.GetDSGH(page);

            ViewData["pages"] = DSNV.pages;

            ViewData["page"] = DSNV.page;

            return View("Index", DSNV.gh);
        }
        public ActionResult DuyetHD(int MAHD, int page = 1)
        {
            DataNV();
            context.DuyetHD(MAHD);
            var Lgh = context.GetDSGH(page);
            ViewData["pages"] = Lgh.pages;

            ViewData["page"] = Lgh.page;
            if (!String.IsNullOrEmpty(MAHD.ToString()))
            {
                List<GiaoHang> Lkq = new List<GiaoHang>();
                foreach (var gh in Lgh.gh)
                {
                    if (gh.MaHD == MAHD)
                    {
                        Lkq.Add(gh);
                    }
                }
                if (Lkq.Count == 0)
                {
                    ViewData["ThongBao"] = "1";
                }
                return View("Index",Lkq);
            }
            ViewData["ThongBao"] = "0";
            return View("Index",Lgh.gh);

        }
    }
}
