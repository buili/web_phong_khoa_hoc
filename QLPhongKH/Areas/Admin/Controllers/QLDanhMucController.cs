using Microsoft.AspNetCore.Mvc;
using QLPhongKH.Models;

namespace QLPhongKH.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QLDanhMucController : Controller
    {
        public IActionResult Index()
        {
            KhContext db = new KhContext();
            List<LoaiBaiViet> listLoaiBai = db.LoaiBaiViets.Where(m => m.ParentId == null).ToList();
            return View(listLoaiBai);
        }

        public ActionResult ThemMoi()
        {
            return View(new LoaiBaiViet());
        }

        [HttpPost]
        public ActionResult ThemMoi(LoaiBaiViet model)
        {
            KhContext db = new KhContext();
            try
            {
                db.LoaiBaiViets.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }


          public ActionResult Edit(int id)
        {
            KhContext db = new KhContext();
            return View(db.LoaiBaiViets.Find(id));


        }

        [HttpPost]
        public ActionResult Edit(LoaiBaiViet model)
        {
            KhContext db = new KhContext();
            var loai = db.LoaiBaiViets.Find(model.MaLoai);

            try
            {
                loai.TenLoai = model.TenLoai;
                loai.ParentId = null;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }
    }
}
