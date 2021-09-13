using Model.Dao;
using Model.EF;
using OnlineShop.Common;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Validation;


namespace OnlineShop.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult LoginClient()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginClient(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserClientDao();
                var result = dao.Login(model.userName, Encryptor.MD5Hash(model.Password));
                if (result == 1)
                {
                    var user = dao.GetByUserName(model.userName);
                    var userSession = new UserLogin();
                    userSession.userName = user.TenTK;
                    userSession.name = user.HoTen;
                    userSession.email = user.Email;
                    userSession.adress = user.Dia_Chi;
                    userSession.phoneNumbers = user.SDT;
                    Session.Add(CommonConstants.USERCLIENT_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khóa");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Sai mật khẩu");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập không đúng");
                }
            }
            return View("LoginClient");
        }
        [HttpGet]
        public ActionResult RegisterClient()
        {
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
            return View();
        }
        [HttpPost]
        public ActionResult RegisterClient(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new TaiKhoan();
                user.TenTK = model.userName;
                user.HoTen = model.Name;
                user.SDT = model.PhoneNumbers;
                user.Dia_Chi = model.Adress;
                user.Email = model.Email;
                user.Mat_Khau = Encryptor.MD5Hash(model.Password);
                if(user.TenTK.Length > 0)
                {
                    OnlineShopDbContext db = new OnlineShopDbContext();
                    db.TaiKhoans.Add(user);
                    db.SaveChanges();
                    
                }
                return RedirectToAction("LoginClient", "Login");
            }

            return View("RegisterClient");
        }
        public ActionResult Logout()
        {
            Session.Remove(CommonConstants.USERCLIENT_SESSION);
            return RedirectToAction("Index", "Home");
        }
    }
}