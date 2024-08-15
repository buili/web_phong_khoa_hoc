using Microsoft.AspNetCore.Mvc;
using QLPhongKH.Models;

namespace QLPhongKH.Components
{
    public class menu : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            KhContext db = new  KhContext();
            List<LoaiBaiViet> listLoaiBai = new List<LoaiBaiViet>();
            listLoaiBai = db.LoaiBaiViets.Where(m=>m.ParentId == null).ToList();


            //return View("", listBaiViet);
            return View(listLoaiBai);
        }
    }
}
