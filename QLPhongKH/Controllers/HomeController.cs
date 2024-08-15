using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Newtonsoft.Json;
using QLPhongKH.Models;
using System.Diagnostics;

namespace QLPhongKH.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            if (HttpContext.Session.GetString("user") == null)
            {
                return RedirectToAction("DangNhap");
                //return View();
            }
            else
            {
                return View();
            }
        }

        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(string email, string password, string loai)
        {
            if (loai == null)
            {
                TempData["error"] = "Vui lòng chọn loại người dùng";
                TempData["email"] = email;
                TempData["pass"] = password;
                return View();
            }
            else
            {
               // KhContext db = new KhContext();
                //var taiKhoan = db.TaiKhoans.SingleOrDefault(m => m.Email == email && m.PassWord == password);
                string taiKhoan = "SELECT* FROM TaiKhoan WHERE Email = N'"+email+"' AND PassWord = '"+password+"'";
                if (taiKhoan != null)
                {
                    var phanQuyen = db.PhanQuyens.FirstOrDefault(pq => pq.IdTk == taiKhoan.Id);
                    if (phanQuyen != null)
                    {
                        var chucNang = db.ChucNangs.FirstOrDefault(cn => cn.MaChucNang == phanQuyen.MaChucNang);
                        if (chucNang != null)
                        {
                            if (loai == "admin" && chucNang.TenChucNang == "admin")
                            {
                                HttpContext.Session.SetString("admin", taiKhoan.Ten);
                                return RedirectToAction("Index", "HomeAdmin", new { area = "Admin" });
                            }
                            else if (loai == "user" && chucNang.TenChucNang == "user")
                            {
                                HttpContext.Session.SetString("user", taiKhoan.Ten);
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                TempData["email"] = email;
                                TempData["pass"] = password;
                                TempData["error"] = "Email hoặc mật khẩu không chính xác";
                                return View();
                            }
                        }
                    }
                }
                else
                {
                    TempData["email"] = email;
                    TempData["pass"] = password;
                    TempData["error"] = "Email hoặc mật khẩu không chính xác";
                    return View();
                }
            }
            return View();



        }

        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(string ten, string email, string password, string repassword)
        {
            KhContext db = new KhContext();
            var taikhoan = db.TaiKhoans.SingleOrDefault(m => m.Email == email);
            if (taikhoan != null)
            {
                ModelState.AddModelError("Email", "Email đã tồn tại");
                TempData["ten"] = ten;
                TempData["emailDK"] = email;
                TempData["passDK"] = password;
                TempData["repass"] = repassword;
                return View();
            }
            else
            {
                if (password != repassword)
                {
                    ModelState.AddModelError("password", "Password không trùng nhau");
                    TempData["ten"] = ten;
                    TempData["emailDK"] = email;
                    TempData["passDK"] = password;
                    TempData["repass"] = repassword;
                    return View();
                }
                else
                {
                    try
                    {
                        TaiKhoan newAccount = new TaiKhoan();
                        newAccount.Ten = ten;
                        newAccount.Email = email;
                        newAccount.PassWord = password;
                        db.TaiKhoans.Add(newAccount);
                        db.SaveChanges();
                        var addedAccount = db.TaiKhoans.SingleOrDefault(m => m.Email == email);

                        if (addedAccount != null)
                        {
                            PhanQuyen newPQ = new PhanQuyen();
                            newPQ.IdTk = addedAccount.Id;
                            newPQ.MaChucNang = 2;
                            db.PhanQuyens.Add(newPQ);
                            db.SaveChanges();
                            HttpContext.Session.SetString("admin", ten);
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            return View();
                        }
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", ex.Message);
                        return View();
                    }
                }

            }

        }

        public ActionResult DangXuat()
        {
            HttpContext.Session.Remove("admin");
            HttpContext.Session.Remove("user");
            return RedirectToAction("DangNhap");
        }
    }
}
