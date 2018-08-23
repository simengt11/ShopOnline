using Model.DAO;
using Model.EF;
using ShopOnline.Commond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Areas.Admin.Controllers
{
    public class ProductCatagoryController : BaseController
    {
        // GET: Admin/ProductCatagory
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            SetViewBag();
            var dao = new ProductCatagoryDAO();
            var model = dao.ListAllPagingProductCatagory(searchString, page, pageSize);
            ViewBag.searchString = searchString;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(ProductCatagory product)
        {
            SetViewBag();
            if (ModelState.IsValid)
            {
                var dao = new ProductCatagoryDAO();
                var UserSession = (UserSession)Session[CommondConstant.USER_SESSION];
                product.CreatedBy = UserSession.Username;

                if (dao.InsertProductCatagory(product))
                {
                    SetAlert("Add the new product sucessfully!", "success");
                    return RedirectToAction("Index", "ProductCatagory");
                }
                else
                    ModelState.AddModelError("", "An error when add the ProductCatagory!");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            SetViewBag();
            var dao = new ProductCatagoryDAO();
            var product = dao.GetProductCatagoryByID(id);
            return View(product);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(ProductCatagory product)
        {
            SetViewBag();
            var dao = new ProductCatagoryDAO();
            var UserSession = (UserSession)Session[CommondConstant.USER_SESSION];
            product.ModifiedBy = UserSession.Username;
            product.ModifiedDate = DateTime.Now;
            if (dao.UpdateProductCatagory(product))
            {
                SetAlert("Edit the product catagory sucessfull!", "success");
                return RedirectToAction("Index", "ProductCatagory");
            }

            else
                ModelState.AddModelError("", "An error when edit product catagory!");
            return View();
        }

        public void SetViewBag(long? selectedID = null)
        {
            var dao = new ProductCatagoryDAO();
            ViewBag.ParentID = new SelectList(dao.GetCatagories(), "ID", "Name", selectedID);
            ViewBag.CatagoyList = dao.GetCatagories();
        }

        [HttpPost]
        public JsonResult ChangeProductCatagoryStatus(long? productID)
        {
            var result = new ProductCatagoryDAO().ChangeProductCatagoryStatus(productID);
            return Json(new
            {
                status = result
            });
        }

        [HttpPost]
        public JsonResult ChangeProductCatagoryShowOnHome(long? productID)
        {
            var result = new ProductCatagoryDAO().ChangeProductCatagoryShowOnHome(productID);
            return Json(new
            {
                status = result
            });
        }


        [HttpDelete]

        public ActionResult Delete(long id)
        {
            new ProductCatagoryDAO().DeleteProductCatagory(id);
            SetAlert("Delete the Product Catagory sucessful!", "success");
            return RedirectToAction("Index", "ProductCatagory");
        }
    }
}
