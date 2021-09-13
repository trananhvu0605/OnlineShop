using Model.Dao;
using Model.EF;
using OnlineShop.Models;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        OnlineShopDbContext db = new OnlineShopDbContext();
        // GET: Home
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 9;
            var products = db.SanPhams.OrderByDescending(x=> x.TenSP).ToPagedList(pageNumber, pageSize);
            try
            {
                var sessionUser = Session["USERCLIENT_SESSION"] as UserLogin;
                ViewBag.name = sessionUser.name;
                ViewBag.userName = sessionUser.userName;
                var ss = ViewBag.name;
                if (ViewBag.name != null )
                {
                    ViewBag.name = sessionUser.name;
                }
                else
                {
                    ViewBag.name = null;
                }
            }
            catch
            {
                ViewBag.name = null;
            }
            return View(products);
        }
        [HttpGet]
        public ActionResult EditAcc(string userName)
        {
            var user = db.TaiKhoans.FirstOrDefault(x => x.TenTK == userName);
            try
            {
                var sessionUser = Session["USERCLIENT_SESSION"] as UserLogin;
                ViewBag.name = sessionUser.name;
                var ss = ViewBag.name;
                if (ViewBag.name != null)
                {
                    ViewBag.name = sessionUser.name;
                }
                else
                {
                    ViewBag.name = null;
                }
            }
            catch
            {
                ViewBag.name = null;
                return RedirectToAction("Index", "Login");
            }
            return View(user);
        }
        [HttpPost]
        public ActionResult EditAcc(TaiKhoan user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserClientDao();
                var result = dao.Update(user);
                if(result)
                {
                    Session.Remove(CommonConstants.USERCLIENT_SESSION);
                    return RedirectToAction("LoginClient", "Login");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật user thành công");
                }
            }
            var sessionUser = Session["USERCLIENT_SESSION"] as UserLogin;
            ViewBag.name = sessionUser.name;
            ViewBag.userName = sessionUser.userName;
            return View();
        }
        [HttpGet]
        public ActionResult EditPassAcc(string userName)
        {
            var user = db.TaiKhoans.FirstOrDefault(x => x.TenTK == userName);
            try
            {
                var sessionUser = Session["USERCLIENT_SESSION"] as UserLogin;
                ViewBag.name = sessionUser.name;
                var ss = ViewBag.name;
                if (ViewBag.name != null)
                {
                    ViewBag.name = sessionUser.name;
                }
                else
                {
                    ViewBag.name = null;
                }
            }
            catch
            {
                ViewBag.name = null;
                return RedirectToAction("Index", "Login");
            }
            return View(user);
        }
        [HttpPost]
        public ActionResult EditPassAcc(string mkcu, TaiKhoan user)
        {
            var taikhoan = db.TaiKhoans.SingleOrDefault(x => x.TenTK == user.TenTK);
            if(!string.IsNullOrEmpty(mkcu))
            {
                var encrypt = Encryptor.MD5Hash(mkcu);
                mkcu = encrypt;
            }                
            if (taikhoan.Mat_Khau == mkcu)
            {
                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrEmpty(user.Mat_Khau))
                    {
                        var encryp = Encryptor.MD5Hash(user.Mat_Khau);
                        user.Mat_Khau = encryp;
                    }
                    taikhoan.Mat_Khau = user.Mat_Khau;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "Mật khẩu cũ không đúng");
            }
            var sessionUser = Session["USERCLIENT_SESSION"] as UserLogin;
            ViewBag.name = sessionUser.name;
            ViewBag.userName = sessionUser.userName;
            return View();
        }
        public ActionResult DetailProc(string maSP)
        {
            var product = db.SanPhams.SingleOrDefault(x => x.MaSP == maSP);
            try
            {
                var sessionUser = Session["USERCLIENT_SESSION"] as UserLogin;
                ViewBag.name = sessionUser.name;
                ViewBag.userName = sessionUser.userName;
                var ss = ViewBag.name;
                if (ViewBag.name != null)
                {
                    ViewBag.name = sessionUser.name;
                }
                else
                {
                    ViewBag.name = null;
                }
            }
            catch
            {
                ViewBag.name = null;
            }
            return View(product);
        }
        [ChildActionOnly]
        public ActionResult ProductCartelogy()
        {
            var model = db.DanhMucs.Select(x => x).ToList();
            return PartialView(model);
        }
        public ActionResult Products(int? page, string maDM)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 9;
            var products = db.SanPhams.Where(x => x.MaDM == maDM).OrderByDescending(x=> x.TenSP).ToPagedList(pageNumber,pageSize);
            ViewBag.tenDM = db.DanhMucs.SingleOrDefault(x => x.MaDM == maDM).TenDM;
            ViewBag.maDM = maDM;
            try
            {
                var sessionUser = Session["USERCLIENT_SESSION"] as UserLogin;
                ViewBag.name = sessionUser.name;
                ViewBag.userName = sessionUser.userName;
                var ss = ViewBag.name;
                if (ViewBag.name != null)
                {
                    ViewBag.name = sessionUser.name;
                }
                else
                {
                    ViewBag.name = null;
                }
            }
            catch
            {
                ViewBag.name = null;
            }
            return View(products);
        }
        public JsonResult ListName(string q)
        {
            var data = db.SanPhams.OrderByDescending(x => x.TenSP.Contains(q)).Select(x => x.TenSP).ToList();
            return Json(new
            {
                data = data,
                status = true
            },JsonRequestBehavior.AllowGet);
        }
        public ActionResult Search(string keyword, int? page)
        {
            int pageNumber = (page ?? 1);
            if(keyword == "")
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.keyword = keyword;
            }      
            int pageSize = 9;
            var model = db.SanPhams.Where(x => x.TenSP.Contains(keyword)).OrderByDescending(x => x.TenSP).ToPagedList(pageNumber, pageSize);
            return View(model);
        }
    }
}