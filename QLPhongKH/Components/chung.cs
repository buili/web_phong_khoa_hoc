using Microsoft.AspNetCore.Mvc;
using QLPhongKH.Models;

namespace QLPhongKH.Components
{
    public class chung : ViewComponent
    {
        public IViewComponentResult Invoke(int maloaibai, int sl)
        {
            KhContext db = new KhContext();
            List<BaiViet> listBaiViet = new List<BaiViet>();
            listBaiViet = db.BaiViets.Where(m => m.LoaiBaiViet == maloaibai).OrderByDescending(m => m.MaBaiViet).Take(sl).ToList();

            var tenLoai = db.LoaiBaiViets
                   .Where(m => m.MaLoai == maloaibai)
                   .Select(m => m.TenLoai)
                   .SingleOrDefault();
            ViewBag.tenLoai = tenLoai;
            //return View("", listBaiViet);
            return View(listBaiViet);
        }
    }
}
