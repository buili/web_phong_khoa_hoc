using Microsoft.AspNetCore.Mvc;
using QLPhongKH.Models;

namespace QLPhongKH.Controllers
{
    public class TimKiemController : Controller
    {
        public IActionResult Index(string timkiem)
        {
            KhContext db = new KhContext();
            List<BaiViet> listBaiViet = new List<BaiViet>();
            ViewBag.timkiem = timkiem; 
            listBaiViet = db.BaiViets.Where(m => m.TenBaiViet.Contains(timkiem) == true).OrderByDescending(m => m.NgayDang).ToList();
            int sl = listBaiViet.Count();
            ViewBag.sl = sl;
            return View(listBaiViet);
        }
    }
}
