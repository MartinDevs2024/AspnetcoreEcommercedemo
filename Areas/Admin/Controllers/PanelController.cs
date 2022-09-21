using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetcoreEcommercedemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PanelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FrontPage()
        {
            return View();
        }

    }
}
