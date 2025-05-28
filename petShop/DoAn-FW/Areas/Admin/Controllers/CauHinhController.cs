using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAn_FW.Areas.Admin.Controllers
{
    public class CauHinhController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}