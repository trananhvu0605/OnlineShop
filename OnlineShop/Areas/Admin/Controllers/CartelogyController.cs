using Model.Dao;
using Model.EF;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class CartelogyController : BaseController
    {
        OnlineShopDbContext db = new OnlineShopDbContext();
        // GET: Admin/Cartelogy
        public ActionResult Index(string q)
        {
            var Cartelogy = db.HoaDons.Select(x => x);
            var model = q;
            switch (model)
            {
                case "MaHD":
                    Cartelogy = Cartelogy.OrderBy(x => x.MaHD);
                    break;
                case "Ngay_Mua":
                    Cartelogy = Cartelogy.OrderBy(x => x.Ngay_Mua);
                    break;

            }
            var sessionUser = Session["USER_SESSION"] as UserLogin;
            ViewBag.userName = sessionUser.userName;
            ViewBag.email = sessionUser.email;
            return View(Cartelogy.ToList());
        }
        [HttpGet]
        public ActionResult Detail(int MaHD)
        {
            var products = db.ChiTiet_HD.Where(s => s.MaHD.Equals(MaHD)).ToList();
            var hoadon = db.HoaDons.Find(MaHD);
            ViewBag.tenkhach = hoadon.TenKH;
            ViewBag.mahd = hoadon.MaHD;
            ViewBag.diachi = hoadon.Dia_Chi_Nhan;
            ViewBag.tongtien = hoadon.Tong_Tien;
            ViewBag.ngaymua = hoadon.Ngay_Mua;
            ViewBag.sdt = hoadon.Sodienthoai;
            ViewBag.yeucaukhac = hoadon.Yeu_Cau_Khac;
            var sessionUser = Session["USER_SESSION"] as UserLogin;
            ViewBag.userName = sessionUser.userName;
            ViewBag.email = sessionUser.email;
            return View(products);
        }
        [HttpGet]
        public ActionResult Edit(int MaHD)
        {
            var product = db.HoaDons.Find(MaHD);
            var sessionUser = Session["USER_SESSION"] as UserLogin;
            ViewBag.userName = sessionUser.userName;
            ViewBag.email = sessionUser.email;
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(string Tinh_Trang, int MaHD)
        {
            if (ModelState.IsValid)
            {
                var hoadon = db.HoaDons.Find(MaHD);
                try
                {
                    hoadon.Tinh_Trang = Tinh_Trang;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Cartelogy");
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Cập nhật thất bại" + ex.Message;
                    return View(hoadon);
                }
            }
            var sessionUser = Session["USER_SESSION"] as UserLogin;
            ViewBag.userName = sessionUser.userName;
            ViewBag.email = sessionUser.email;
            return View();
        }
    }
}