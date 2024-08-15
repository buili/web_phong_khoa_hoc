using Microsoft.AspNetCore.Mvc;
using QLPhongKH.Models;
using X.PagedList;

namespace QLPhongKH.Controllers
{
    public class BaiVietController : Controller
    {
        //public IActionResult Index(int maloaibai)
        //{
        //    QlphongKhContext db = new QlphongKhContext();
        //    List<BaiViet> listBaiViet = new List<BaiViet>();
        //    listBaiViet = db.BaiViets.Where(m => m.LoaiBaiViet == maloaibai).OrderByDescending(m => m.MaBaiViet).ToList();


     
        //    return View(listBaiViet);
        //}

        public IActionResult Index(int maloaibai, int? page)
        {
            KhContext db = new KhContext();
            int pageSize = 2;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listBaiViet = db.BaiViets.Where(m => m.LoaiBaiViet == maloaibai).OrderByDescending(m => m.NgayDang).ToList();
            PagedList<BaiViet> lst = new PagedList<BaiViet>(listBaiViet, pageNumber, pageSize);
            ViewBag.maloai = maloaibai;
            return View(lst);
        }

        public ActionResult Chitiet(int id)
        {
            KhContext db = new KhContext();
            var baiviet = db.BaiViets.Find(id);


            //return View("", listBaiViet);
            return View(baiviet);
        }
    }
}
