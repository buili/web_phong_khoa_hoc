using Microsoft.AspNetCore.Mvc;
using QLPhongKH.Models;

namespace QLPhongKH.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QLLoaiBaiVietController : Controller
    {
        public IActionResult Index()
        {
            KhContext db = new KhContext();
            List<LoaiBaiViet> listLoaiBai = db.LoaiBaiViets.Where(m => m.ParentId != null &&
                                            !db.LoaiBaiViets.Where(n => n.ParentId != null).Select(n => n.MaLoai)
                                            .Contains((int)m.ParentId)).ToList();
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
                loai.ParentId = model.ParentId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        public ActionResult Delete(int id)
        {

            KhContext db = new KhContext();
            var updateModel = db.LoaiBaiViets.Find(id);
            db.LoaiBaiViets.Remove(updateModel);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

    }
}
