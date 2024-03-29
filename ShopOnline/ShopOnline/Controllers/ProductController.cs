﻿using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

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

        public ActionResult Search(string keyword, int page = 1, int pageSize = 1)
        {
            if (String.IsNullOrEmpty(keyword))
                return RedirectToAction("Index", " Home");
            int totalRecord = 0;
            var model = new ProductDAO().GetProductByName(keyword, ref totalRecord, page, pageSize);
            ViewBag.TotalRecord = totalRecord;
            ViewBag.Page = page;

            int maxPage = 5;
            int totalPage;
            if ((totalRecord < pageSize / 2)&&totalRecord>0)
            {
                totalPage = 1;
            }
            else
            {
                totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            }
            ViewBag.KeyWord = keyword;
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

            XElement xElement = XElement.Parse(model.MoreImages);
            var listStringImage = new List<string>();
            foreach (XElement item in xElement.Elements())
            {
                listStringImage.Add(item.Value);
            }
            ViewBag.ListMoreImage = listStringImage;
            var productCatagoryDAO = new ProductCatagoryDAO();
            var catagoryList = productCatagoryDAO.GetAllCatagories();
            var catagory = productCatagoryDAO.GetProductCatagoryByID(model.CatalogyID.Value);
            
            ViewBag.CatagoryList = catagoryList;
            ViewBag.RelatedProductList = relatedProduct;
            ViewBag.Catagory = catagory;

            return View(model);
        }

        public JsonResult ListName(string term)
        {
            var tempData = new ProductDAO().ListProductProductName(term);
            return Json(new {
            data = tempData,
            status =true
            }, JsonRequestBehavior.AllowGet);
        }
    }
}