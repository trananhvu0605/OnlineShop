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
    public class ProductsController : BaseController
    {
        OnlineShopDbContext db = new OnlineShopDbContext();
        // GET: Admin/Products
        public ActionResult Index(string q)
        {
            var Products = db.SanPhams.Select(x => x);
            var model = q;
            switch (model)
            {
                case "MaSP":
                    Products = Products.OrderBy(x => x.MaSP);
                    break;
                case "TenSP":
                    Products = Products.OrderBy(x => x.TenSP);
                    break;
                case "Gia_HT":
                    Products = Products.OrderBy(x => x.Gia_HT);
                    break;

            }
            var sessionUser = Session["USER_SESSION"] as UserLogin;
            ViewBag.userName = sessionUser.userName;
            ViewBag.email = sessionUser.email;
            return View(Products.ToList());
        }
        [HttpGet]
        public ActionResult Detail(string MaSP)
        {
            var product = db.SanPhams.SingleOrDefault(x => x.MaSP == MaSP);
            var sessionUser = Session["USER_SESSION"] as UserLogin;
            ViewBag.userName = sessionUser.userName;
            ViewBag.email = sessionUser.email;
            return View(product);
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
        public ActionResult Create([Bind(Include = "MaSP,TenSP,Anh,Gia_HT,Xuat_Xu,Diem_Ban,DVT,SoLuongCo,MaDM,Mo_Ta")] SanPham product)
        {
            var f = Request.Files["Anh"];
            if (f != null && f.ContentLength > 0)
            {
                string fileName = System.IO.Path.GetFileName(f.FileName);
                string uploadPath = Server.MapPath("~/Areas/Admin/Assets/images/sanpham/" + fileName);
                string uploadPaths = Server.MapPath("~/wwwroot/img/" + fileName);
                f.SaveAs(uploadPath);
                f.SaveAs(uploadPaths);
                product.Anh = fileName;
            }
            if (ModelState.IsValid)
            {
                try
                {
                    db.SanPhams.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Products");
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Thêm sản phẩm thất bại" + ex.Message;
                    return View("Create",product);
                }
                
            }
            var sessionUser = Session["USER_SESSION"] as UserLogin;
            ViewBag.userName = sessionUser.userName;
            ViewBag.email = sessionUser.email;
            return View();
        }
        [HttpGet]
        public ActionResult Edit(string MaSP)
        {
            var product = db.SanPhams.SingleOrDefault(x => x.MaSP == MaSP);
            var sessionUser = Session["USER_SESSION"] as UserLogin;
            ViewBag.userName = sessionUser.userName;
            ViewBag.email = sessionUser.email;
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(SanPham product)
        {
            var f = Request.Files["Anh"];
            if (f != null && f.ContentLength > 0)
            {
                string fileName = System.IO.Path.GetFileName(f.FileName);
                string uploadPath = Server.MapPath("~/Areas/Admin/Assets/images/sanpham/" + fileName);
                string uploadPaths = Server.MapPath("~/wwwroot/img/" + fileName);
                f.SaveAs(uploadPath);
                f.SaveAs(uploadPaths);
                product.Anh = fileName;
            }
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Products");
                }               
                catch (Exception ex)
                {
                    ViewBag.Error = "Cập nhật thất bại" + ex.Message;
                    return View(product);
                }
            }
            var sessionUser = Session["USER_SESSION"] as UserLogin;
            ViewBag.userName = sessionUser.userName;
            ViewBag.email = sessionUser.email;
            return View();
        }
        [HttpGet]
        public ActionResult Delete(string MaSP)
        {
            var product = db.SanPhams.SingleOrDefault(x => x.MaSP == MaSP);
            var sessionUser = Session["USER_SESSION"] as UserLogin;
            ViewBag.userName = sessionUser.userName;
            ViewBag.email = sessionUser.email;
            return View(product);
        }
        [HttpPost]
        public ActionResult Delete(string MaSP, string TenSP)
        {
            var user = db.SanPhams.SingleOrDefault(x => x.MaSP == MaSP);
            try
            {
                db.SanPhams.Remove(user);
                db.SaveChanges();
                var sessionUser = Session["USER_SESSION"] as UserLogin;
                ViewBag.userName = sessionUser.userName;
                ViewBag.email = sessionUser.email;
                return RedirectToAction("Index", "Products");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Xóa thất bại" + ex.Message;
                return View(user);
            }

        }
    }
}