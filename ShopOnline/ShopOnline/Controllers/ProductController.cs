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

        public ActionResult Catagory(long id, int page=1, int pageSize=1)
        {
            
            var catagory = new ProductCatagoryDAO().GetProductCatagoryByID(id);
            ViewBag.Catagory = catagory;

            int totalRecord = 0;
            var model = new ProductDAO().GetProductByCatagoryID(id, ref totalRecord, page, pageSize);
            ViewBag.TotalRecord = totalRecord;
            ViewBag.Page = page;

            int maxPage = 5;
            int totalPage;
            if (totalRecord < pageSize / 2)
            {
                totalPage = 1;
            }
            else
            {
                totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            }
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            return View(model);
        }

        public ActionResult ProductDetail(long id)
        {
            var productDAO = new ProductDAO();
            var model = productDAO.GetProductByID(id);
            var relatedProduct = productDAO.GetRelatedProductByCatagoryID(model.CatalogyID.Value, id);

            var productCatagoryDAO = new ProductCatagoryDAO();
            var catagoryList = productCatagoryDAO.GetAllCatagories();
            var catagory = productCatagoryDAO.GetProductCatagoryByID(model.CatalogyID.Value);
            
            ViewBag.CatagoryList = catagoryList;
            ViewBag.RelatedProductList = relatedProduct;
            ViewBag.Catagory = catagory;

            return View(model);
        }
    }
}