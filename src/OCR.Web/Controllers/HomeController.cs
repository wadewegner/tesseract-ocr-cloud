using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OCR.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.IntPtrSize = IntPtr.Size;
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
