using Microsoft.AspNetCore.Mvc;
using QLPhongKH.Models;

namespace QLPhongKH.Components
{
    public class menu2 : ViewComponent
    {
        public IViewComponentResult Invoke(int parentID)
        {
            KhContext db = new KhContext();
            List<LoaiBaiViet> listLoaiBai = new List<LoaiBaiViet>();
            listLoaiBai = db.LoaiBaiViets.Where(m => m.ParentId == parentID).ToList();
            ViewBag.Count = listLoaiBai.Count();
            return View(listLoaiBai);
        }
    }
}
