using Model.DAO;
using ShopOnline.Commond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.Mvc;
using Model.EF;
using ShopOnline.Models;


namespace ShopOnline.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        [HttpGet]
        public ActionResult Index()
        {
            var cart = Session[CommondConstant.CART_SESSION];
            var listCartItem = new List<CartSession>();
            double total = 0;
            if (cart != null)
            {
                listCartItem = (List<CartSession>)cart;
                foreach (var item in listCartItem)
                {
                    total = total + (item.Quanlity * Decimal.ToDouble(item.product.Price.Value));
                }
            }
            ViewBag.Total = total;   
            return View(listCartItem);
        }

        [HttpPost]
        public ActionResult Index (long productID, int quanlity)
        {
            return View("Index");
        }
        public ActionResult AddItem(long productID, int quanlity, string url)
        {
            var prod = new ProductDAO().GetProductByID(productID);
            var cart = Session[CommondConstant.CART_SESSION];
            if (cart != null)
            {
                var listCartItem = (List < CartSession >) cart;
                if (listCartItem.Exists(x => x.product.ID == productID))
                {
                    foreach (var item in listCartItem)
                    {
                        if (item.product.ID == productID)
                            item.Quanlity += quanlity;
                    }
                }
                else
                {
                    var item = new CartSession();
                    item.product = prod;
                    item.Quanlity = quanlity;
                    listCartItem.Add(item);

                    //assign listCartItem into session
                    Session[CommondConstant.CART_SESSION] = listCartItem;
                }
            }
            else
            {
                //Create a new cart item
                var item = new CartSession();
                item.product = prod;
                item.Quanlity = quanlity;
                var listCartItem = new List<CartSession>();
                listCartItem.Add(item);

                //assign listCartItem into session
                Session[CommondConstant.CART_SESSION] = listCartItem;
            }
            return RedirectToAction("Index");
        }

        public JsonResult Update(string cartModel)
        {

            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartSession>>(cartModel);
            var sessionCart = (List<CartSession>)(Session[CommondConstant.CART_SESSION]);
            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.product.ID == item.product.ID);
                if (jsonItem != null)
                    item.Quanlity = jsonItem.Quanlity;
            }
            Session[Commond.CommondConstant.CART_SESSION] = sessionCart;
            return Json(new {
                status = true
            });
        }

        public JsonResult Delete(long id)
        {

            var sessionCart = (List<CartSession>)(Session[CommondConstant.CART_SESSION]);
            sessionCart.RemoveAll(x => x.product.ID == id);
            Session[Commond.CommondConstant.CART_SESSION] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        [HttpGet]
        public ActionResult Checkout()
        {
            var cart = Session[CommondConstant.CART_SESSION];
            var listCartItem = new List<CartSession>();
            double total = 0;
            if (cart != null)
            {
                listCartItem = (List<CartSession>)cart;
                foreach (var item in listCartItem)
                {
                    total = total + (item.Quanlity * Decimal.ToDouble(item.product.Price.Value));
                }
            }
            ViewBag.Total = total;
            ViewBag.ListCartItem = listCartItem;
            TempData["Total"] = total;
            
            return View();
        }
        [HttpPost]
        public ActionResult Checkout(CartInforModel model)
        {
            if (ModelState.IsValid)
            {
                var orderDAO = new OrderDAO();
                var order = new Order();
                order.CreateDate = DateTime.Now;
                order.ShipName = model.ShipName;
                order.ShipPhone = model.ShipPhone;
                order.ShipEmail = model.ShipEmail;
                order.ShipAddress = model.ShipAddress;
                order.TotalPrice = (TempData.ContainsKey("Total") ? TempData["Total"].ToString() : "");
                var id = orderDAO.InsertOrder(order);

                var orderDetailDAO = new OrderDetailDAO();
                var orderDetail = new OrderDetail();
                var sessionCart = (List<CartSession>)(Session[CommondConstant.CART_SESSION]);
                if (sessionCart != null)
                {
                    foreach (var item in sessionCart)
                    {
                        orderDetail.ProductID = item.product.ID;
                        orderDetail.OrderID = id;
                        orderDetail.Quanlity = item.Quanlity;
                        orderDetailDAO.InsertOrderDetail(orderDetail);
                    }

                }

                return RedirectToAction("Thanks");
            }
            else
            {
                ModelState.AddModelError("", "Khong duoc bo trong");
                return RedirectToAction("Checkout");
            }
            
        }

        public ActionResult Thanks()
        {
            return View();
        }

       
    }
}
