using Microsoft.AspNetCore.Mvc;
using QLPhongKH.Models;

namespace QLPhongKH.Components
{
    public class dong1_1 : ViewComponent
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
            return View(listBaiViet);
        }
    }
}
