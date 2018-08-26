using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            SetViewBag(4);
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult _ProductCatagories()
        {
            var model = new ProductCatagoryDAO().GetAllCatagories();
            return PartialView(model);
        }

        public void SetViewBag(int top)
        {
            var dao = new ProductDAO();
            ViewBag.ListNewProduct=dao.GetNewProducts(top);
            ViewBag.ListFeatureProduct = dao.GetFeatureProducts(top);
        }
    }
}