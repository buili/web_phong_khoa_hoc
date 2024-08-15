using Microsoft.AspNetCore.Mvc;
using QLPhongKH.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QLPhongKH.Components
{
    public class dong2 : ViewComponent
    {
        public IViewComponentResult Invoke(int type, int sl)
        {
            KhContext db = new KhContext();
            List<BaiViet> listBaiViet = new List<BaiViet>();
            listBaiViet = db.BaiViets.Where(m => m.LoaiBaiViet == type).OrderByDescending(m => m.MaBaiViet).Take(sl).ToList();

            var tenLoai = db.LoaiBaiViets
                   .Where(m => m.MaLoai == type)
                   .Select(m => m.TenLoai)
                   .SingleOrDefault();
            ViewBag.tenLoai = tenLoai;
            if (type == 4)
            {
                return View("View1", listBaiViet);
            }
            else { return View("Default", listBaiViet); }
            //return View(listBaiViet);
        }
    }
}
