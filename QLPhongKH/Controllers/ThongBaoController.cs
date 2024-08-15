using Microsoft.AspNetCore.Mvc;

namespace QLPhongKH.Controllers
{
    public class ThongBaoController : Controller
    {
        public IActionResult DanhSach()
        {
            return View();
        }
    }
}
