using Model.Dao;
using Model.EF;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        OnlineShopDbContext db = new OnlineShopDbContext();
        // GET: Admin/User
        public ActionResult Index(string q)
        {
            var users = db.TaiKhoans.Select(x=> x);
            var model = q;
            switch (model)
            {
                case "TenTK":
                    users =  users.OrderBy(x => x.TenTK);
                    break;
                case "Email":
                    users = users.OrderBy(x => x.Email);
                    break;
                default:
                    users = users.OrderBy(x => x.Quyen);
                    break;

            }
            var sessionUser = Session["USER_SESSION"] as UserLogin;
            ViewBag.userName = sessionUser.userName;
            ViewBag.email = sessionUser.email;
            return View(users.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            var sessionUser = Session["USER_SESSION"] as UserLogin;
            ViewBag.userName = sessionUser.userName;
            ViewBag.email = sessionUser.email;
            return View();
        }
        [HttpPost]
        public ActionResult Create(TaiKhoan user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var encrypt = Encryptor.MD5Hash(user.Mat_Khau);
                    user.Mat_Khau = encrypt;
                    db.TaiKhoans.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Index", "User");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Tạo mới tài khoản thất bại" + ex.Message;
                return View(user);
            }        
            var sessionUser = Session["USER_SESSION"] as UserLogin;
            ViewBag.userName = sessionUser.userName;
            ViewBag.email = sessionUser.email;
            return View();
        }
        [HttpGet]
        public ActionResult Edit(string tenTk)
        {
            var user = db.TaiKhoans.SingleOrDefault(x => x.TenTK == tenTk);
            var sessionUser = Session["USER_SESSION"] as UserLogin;
            ViewBag.userName = sessionUser.userName;
            ViewBag.email = sessionUser.email;
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(TaiKhoan user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "User");
            }
            var sessionUser = Session["USER_SESSION"] as UserLogin;
            ViewBag.userName = sessionUser.userName;
            ViewBag.email = sessionUser.email;
            return View();
        }
        [HttpGet]
        public ActionResult EditPass(string tenTk)
        {
            var user = db.TaiKhoans.SingleOrDefault(x => x.TenTK == tenTk);
            var sessionUser = Session["USER_SESSION"] as UserLogin;
            ViewBag.userName = sessionUser.userName;
            ViewBag.email = sessionUser.email;
            return View(user);
        }
        [HttpPost]
        public ActionResult EditPass(string TenTK, string Mat_Khau)
        {
            if (ModelState.IsValid)
            {
                var encrypt = Encryptor.MD5Hash(Mat_Khau);
                Mat_Khau = encrypt;
                var user = db.TaiKhoans.SingleOrDefault(x => x.TenTK == TenTK);
                user.Mat_Khau = Mat_Khau;
                db.SaveChanges();
                return RedirectToAction("Index", "User");
            }
            var sessionUser = Session["USER_SESSION"] as UserLogin;
            ViewBag.userName = sessionUser.userName;
            ViewBag.email = sessionUser.email;
            return View();
        }
        [HttpGet]
        public ActionResult Delete(string tenTk)
        {
            var user = db.TaiKhoans.SingleOrDefault(x => x.TenTK == tenTk);
            var sessionUser = Session["USER_SESSION"] as UserLogin;
            ViewBag.userName = sessionUser.userName;
            ViewBag.email = sessionUser.email;
            return View(user);
        }
        [HttpPost]
        public ActionResult Delete(string tentk,string hoten)
        {
            var user = db.TaiKhoans.SingleOrDefault(x => x.TenTK == tentk);
            try
            {
                db.TaiKhoans.Remove(user);
                db.SaveChanges();
                var sessionUser = Session["USER_SESSION"] as UserLogin;
                ViewBag.userName = sessionUser.userName;
                ViewBag.email = sessionUser.email;
                return RedirectToAction("Index", "User");
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Xóa thất bại" + ex.Message;
                return View(user);
            }
            
        }
        public ActionResult Logdown(string tenTk)
        {
            var user = db.TaiKhoans.SingleOrDefault(x => x.TenTK == tenTk);
            try
            {
                user.Khoa = true;
                db.SaveChanges();
                var sessionUser = Session["USER_SESSION"] as UserLogin;
                ViewBag.userName = sessionUser.userName;
                ViewBag.email = sessionUser.email;
                return RedirectToAction("Index", "User");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Khóa tài khoản thất bại" + ex.Message;
                return View(user);
            }

        }
        public ActionResult Logon(string tenTk)
        {
            var user = db.TaiKhoans.SingleOrDefault(x => x.TenTK == tenTk);
            try
            {
                user.Khoa = false;
                db.SaveChanges();
                var sessionUser = Session["USER_SESSION"] as UserLogin;
                ViewBag.userName = sessionUser.userName;
                ViewBag.email = sessionUser.email;
                return RedirectToAction("Index", "User");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Mở Khóa tài khoản thất bại" + ex.Message;
                return View(user);
            }

        }
    }
}