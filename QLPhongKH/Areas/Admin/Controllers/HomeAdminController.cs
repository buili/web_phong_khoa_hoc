using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Newtonsoft.Json;
using QLPhongKH.Models;

namespace QLPhongKH.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeAdminController : Controller
    {
       

        public IActionResult Index()
        {
            // return RedirectToAction("DangNhap", "Home", new { area = "" });
            if (HttpContext.Session.GetString("admin") == null)
            {
                //return RedirectToAction("DangNhap", "Home", new { area = "" });
                return View();
            }
            else
            {
                return View();
            }
        }
        public ActionResult DangXuat()
        {
            HttpContext.Session.Remove("admin");
            return RedirectToAction("DangNhap", "Home", new { area = "" });
        }
    }

    

}
