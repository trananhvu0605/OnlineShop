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
    public class TypeProductsController : BaseController
    {
        OnlineShopDbContext db = new OnlineShopDbContext();
        // GET: Admin/typeProducts
        public ActionResult Index(string q)
        {
            var typeProducts = db.DanhMucs.Select(x => x);
            var model = q;
            switch (model)
            {
                case "MaDM":
                    typeProducts = typeProducts.OrderBy(x => x.MaDM);
                    break;
                case "TenDM":
                    typeProducts = typeProducts.OrderBy(x => x.TenDM);
                    break;

            }
            var sessionUser = Session["USER_SESSION"] as UserLogin;
            ViewBag.userName = sessionUser.userName;
            ViewBag.email = sessionUser.email;
            return View(typeProducts.ToList());
        }
        [HttpGet]
        public ActionResult Detail(string maDM)
        {
            var DM = db.DanhMucs.SingleOrDefault(x => x.MaDM == maDM);
            var sessionUser = Session["USER_SESSION"] as UserLogin;
            ViewBag.userName = sessionUser.userName;
            ViewBag.email = sessionUser.email;
            return View(DM);
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
        public ActionResult Create(DanhMuc danhmuc)
        {
            if (ModelState.IsValid)
            {
                db.DanhMucs.Add(danhmuc);
                db.SaveChanges();
                return RedirectToAction("Index", "TypeProducts");
            }
            var sessionUser = Session["USER_SESSION"] as UserLogin;
            ViewBag.userName = sessionUser.userName;
            ViewBag.email = sessionUser.email;
            return View();
        }
        [HttpGet]
        public ActionResult Edit(string maDM)
        {
            var DM = db.DanhMucs.SingleOrDefault(x => x.MaDM == maDM);
            var sessionUser = Session["USER_SESSION"] as UserLogin;
            ViewBag.userName = sessionUser.userName;
            ViewBag.email = sessionUser.email;
            return View(DM);
        }
        [HttpPost]
        public ActionResult Edit(DanhMuc danhmuc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(danhmuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "TypeProducts");
            }
            var sessionUser = Session["USER_SESSION"] as UserLogin;
            ViewBag.userName = sessionUser.userName;
            ViewBag.email = sessionUser.email;
            return View();
        }
        [HttpGet]
        public ActionResult Delete(string maDM)
        {
            var DM = db.DanhMucs.SingleOrDefault(x => x.MaDM == maDM);
            var sessionUser = Session["USER_SESSION"] as UserLogin;
            ViewBag.userName = sessionUser.userName;
            ViewBag.email = sessionUser.email;
            return View(DM);
        }
        [HttpPost]
        public ActionResult Delete(string maDM, string tenDM)
        {
            var user = db.DanhMucs.SingleOrDefault(x => x.MaDM == maDM);
            try
            {
                db.DanhMucs.Remove(user);
                db.SaveChanges();
                var sessionUser = Session["USER_SESSION"] as UserLogin;
                ViewBag.userName = sessionUser.userName;
                ViewBag.email = sessionUser.email;
                return RedirectToAction("Index", "TypeProducts");
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Xóa thất bại" + ex.Message;
                return View(user);
            }
            
        }
    }
}