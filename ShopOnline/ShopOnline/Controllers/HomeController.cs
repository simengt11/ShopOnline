﻿using Model.DAO;
using ShopOnline.Commond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            SetViewBag(4);
            return View();
        }

        [ChildActionOnly]
        public ActionResult _TopMenu()
        {
            var model = (Commond.UserSession)Session[CommondConstant.USER_SESSION];
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult _MainMenu()
        {
            var dao = new MenuDAO();
            var model = dao.GetMenuByMenuTypeID(2);
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult _Slide()
        {
            var dao = new SlideDAO();
            var model = dao.GetAllSlide();
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult _ShoppingCart()
        {
            var cart = Session[CommondConstant.CART_SESSION];
            var listCartItem = new List<CartSession>();
            if (cart != null)
                listCartItem = (List<CartSession>)cart;
            return PartialView(listCartItem);
        }

        public void SetViewBag(int top)
        {
            var dao = new ProductDAO();
            ViewBag.ListNewProduct = dao.GetNewProducts(top);
            ViewBag.ListFeatureProduct = dao.GetFeatureProducts(top);
        }

        
    }
}