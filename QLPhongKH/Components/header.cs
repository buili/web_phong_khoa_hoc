using Microsoft.AspNetCore.Mvc;
using QLPhongKH.Models;


namespace QLPhongKH.Components
{
    public class header : ViewComponent
    {
        public IViewComponentResult Invoke(int maloai = 1)
        {
            string maloaibai = HttpContext.Request.Query["maloaibai"];
            KhContext db = new KhContext();
           LoaiBaiViet LoaiBaiViet = new LoaiBaiViet();
           LoaiBaiViet = db.LoaiBaiViets.FirstOrDefault(m => m.MaLoai == maloai);
            return View(LoaiBaiViet);
        }
    }
}
