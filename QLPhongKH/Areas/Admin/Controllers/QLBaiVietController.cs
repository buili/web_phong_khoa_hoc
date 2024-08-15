using Microsoft.AspNetCore.Mvc;
using QLPhongKH.Models;
using X.PagedList;

namespace QLPhongKH.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QLBaiVietController : Controller
    {
       
        public IActionResult Index(int? page)
        {
            int pageSize = 4;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            KhContext db = new KhContext();
            var listBaiViet = db.BaiViets.OrderByDescending(m=>m.NgayDang).ToList();
            PagedList<BaiViet> lst = new PagedList<BaiViet>(listBaiViet, pageNumber, pageSize);
            return View(lst);
        }
        //public IActionResult Index(int maloaibai, int? page)
        //{
        //    KhContext db = new KhContext();
        //    int pageSize = 2;
        //    int pageNumber = page == null || page < 0 ? 1 : page.Value;
        //    var listBaiViet = db.BaiViets.Where(m => m.LoaiBaiViet == maloaibai).OrderByDescending(m => m.NgayDang).ToList();
        //    PagedList<BaiViet> lst = new PagedList<BaiViet>(listBaiViet, pageNumber, pageSize);
        //    ViewBag.maloai = maloaibai;
        //    return View(lst);
        //}

        public ActionResult ThemMoi()
        {
            return View(new BaiViet());
        }

        [HttpPost]
        public async Task<IActionResult> ThemMoi(BaiViet model, IFormFile HinhAnh)
        {
            KhContext db = new  KhContext();
            if (HinhAnh != null && HinhAnh.Length > 0)
            {
                // Lưu file
                string rootFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "image");
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(HinhAnh.FileName);
                string filePath = Path.Combine(rootFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await HinhAnh.CopyToAsync(stream);
                }

                // Lưu thuộc tính url hình ảnh
                model.HinhAnh = "/image/" + fileName;
            }
            try
            {
                db.BaiViets.Add(model);
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
            return View(db.BaiViets.Find(id));
        }

        [HttpPost]

        public async Task<IActionResult> Edit(BaiViet model, IFormFile HinhAnh)
        {
            KhContext db = new KhContext();
            var baiViet = db.BaiViets.Find(model.MaBaiViet);
            if (HinhAnh != null && HinhAnh.Length > 0)
            {
                // Lưu file
                string rootFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "image");
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(HinhAnh.FileName);
                string filePath = Path.Combine(rootFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await HinhAnh.CopyToAsync(stream);
                }

                // Lưu thuộc tính url hình ảnh
                model.HinhAnh = "/image/" + fileName;
                baiViet.HinhAnh = model.HinhAnh;
            }
           
            try
            {
                baiViet.TenBaiViet = model.TenBaiViet;
                baiViet.LoaiBaiViet = model.LoaiBaiViet;
                baiViet.TomTat = model.TomTat;
                baiViet.TacGia = model.TacGia;
                baiViet.NgayDang = model.NgayDang;
                baiViet.NoiDung = model.NoiDung;
               
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
