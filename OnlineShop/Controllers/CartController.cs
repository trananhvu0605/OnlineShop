using Model.EF;
using OnlineShop.Common;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace OnlineShop.Controllers
{
    public class CartController : Controller
    {
        private const string Cartsession = "Cartsession";
        OnlineShopDbContext db = new OnlineShopDbContext();
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[Cartsession];
            var list = new List<CartItem>();
            if(cart != null)
            {
                list = (List<CartItem>)cart;
            }
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
            return View(list);
        }
        public ActionResult Additem(string productId, int quantity)
        {
            var product = db.SanPhams.FirstOrDefault(x => x.MaSP == productId);
            var cart = Session[Cartsession];
            if(product.SoLuongCo == 0 || product.SoLuongCo <0)
            {
                return RedirectToAction("ErrorOrder", "Cart");
            }
            else
            {
                if (cart != null)
                {
                    var list = (List<CartItem>)cart;
                    if (list.Exists(x => x.SanPham.MaSP == productId))
                    {
                        foreach (var item in list)
                        {
                            if (item.SanPham.MaSP == productId)
                            {
                                item.Soluong += quantity;
                            }
                        }
                    }
                    else
                    {
                        var item = new CartItem();
                        item.SanPham = product;
                        item.Soluong = quantity;
                        list.Add(item);


                    }
                    Session[Cartsession] = list;
                }
                else
                {
                    var item = new CartItem();
                    item.SanPham = product;
                    item.Soluong = quantity;
                    var list = new List<CartItem>();
                    list.Add(item);

                    Session[Cartsession] = list;

                }
            }
            
            return RedirectToAction("Index", "Cart");
        }
        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize <List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[Cartsession];
            foreach(var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.SanPham.MaSP == item.SanPham.MaSP);
                if(jsonItem != null)
                {
                    item.Soluong = jsonItem.Soluong;
                }
            }
            Session[Cartsession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult Delete(string id)
        {
            var sessionCart = (List<CartItem>)Session[Cartsession];
            sessionCart.RemoveAll(x => x.SanPham.MaSP == id);
            Session[Cartsession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        [HttpGet]
        public ActionResult Order(string sumTotal, string sumNumbers)
        {
            ViewBag.sumTotal = sumTotal;
            ViewBag.sumNumbers = sumNumbers;
            var cart = Session[Cartsession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            try
            {
                var sessionUser = Session["USERCLIENT_SESSION"] as UserLogin;
                ViewBag.name = sessionUser.name;
                ViewBag.userName = sessionUser.userName;
                ViewBag.adress = sessionUser.adress;
                ViewBag.phoneNumbers = sessionUser.phoneNumbers;
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
            return View(list);
        }
        [HttpPost]
        public ActionResult Order(string tenKH,string diaChi, string soDienThoai,string yeuCauKhac,string tinhtrang,string tenTK, string TongTien)
        {
            var order = new HoaDon();
            if (ModelState.IsValid)
            {               
                order.TenKH = tenKH;
                order.Dia_Chi_Nhan = diaChi;
                order.Sodienthoai = soDienThoai;
                order.Tong_Tien = TongTien;
                order.Yeu_Cau_Khac = yeuCauKhac;
                order.Tinh_Trang = tinhtrang;
                order.TenTK = tenTK;
                order.Ngay_Mua = DateTime.Now;
                db.HoaDons.Add(order);
                db.SaveChanges();
            }
            var cart = (List<CartItem>)Session[Cartsession];
            try
            {
                foreach (var item in cart)
                {
                    var OrderDetail = new ChiTiet_HD();
                    OrderDetail.MaHD = order.MaHD;
                    OrderDetail.MaSP = item.SanPham.MaSP;
                    OrderDetail.SoLuongMua = item.Soluong;
                    OrderDetail.Tensanpham = item.SanPham.TenSP;
                    db.ChiTiet_HD.Add(OrderDetail);
                    var product = db.SanPhams.SingleOrDefault(x => x.MaSP == item.SanPham.MaSP);
                    product.SoLuongCo -= item.Soluong;
                    db.SaveChanges();
                }
                Session.Remove(Cartsession);
            }
            catch
            {
                return Redirect("Error");
            }
            var detailproc = db.ChiTiet_HD.Where(s => s.MaHD.Equals(order.MaHD)).ToList();
            var hoadon = db.HoaDons.Find(order.MaHD);
            ViewBag.tenkhach = hoadon.TenKH;
            ViewBag.mahd = hoadon.MaHD;
            ViewBag.diachi = hoadon.Dia_Chi_Nhan;
            ViewBag.tongtien = hoadon.Tong_Tien;
            ViewBag.ngaymua = hoadon.Ngay_Mua;
            ViewBag.yeucaukhac = hoadon.Yeu_Cau_Khac;
            ViewBag.sdt = hoadon.Sodienthoai;
            ViewBag.ngaymua = hoadon.Ngay_Mua;
            return View("DetaiOrder", detailproc);
        }
        public ActionResult DeleteAll()
        {
            Session.Remove(Cartsession);
            return RedirectToAction("Index", "Cart");
        }
        public ActionResult DetaiOrder()
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
        public ActionResult Error()
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
        public ActionResult ErrorOrder()
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
    }
}