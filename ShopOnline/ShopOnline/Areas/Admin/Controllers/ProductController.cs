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
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            SetViewBag();
            var dao = new ProductDAO();
            var model = dao.ListAllPagingProduct(searchString, page, pageSize);
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
        public ActionResult Create(Product product)
        {
            SetViewBag();
            if (ModelState.IsValid)
            {
                var dao = new ProductDAO();
                var UserSession = (UserSession)Session[CommondConstant.USER_SESSION];
                product.CreatedBy = UserSession.Username;

                if (dao.InsertProduct(product))
                {
                    SetAlert("Add the new product sucessfully!", "success");
                    return RedirectToAction("Index", "Product");
                }
                else
                    ModelState.AddModelError("", "An error when add the Product!");
            }
            
            return View();
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            SetViewBag();
            var dao = new ProductDAO();
            var product = dao.GetProductByID(id);
            return View(product);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(Product product)
        {
            SetViewBag();
            var dao = new ProductDAO();
            var UserSession = (UserSession)Session[CommondConstant.USER_SESSION];
            product.ModifiedBy = UserSession.Username;
            product.ModifiedDate = DateTime.Now;
            if (dao.UpdateProduct(product))
            {
                SetAlert("Edit the product sucessfull!", "success");
                return RedirectToAction("Index", "Product");
            }

            else
                ModelState.AddModelError("", "An error when edit product!");
            return View();
        }

        public void SetViewBag(long? selectedID = null)
        {
            var dao = new ProductCatagoryDAO();
            ViewBag.CatalogyID = new SelectList(dao.GetCatagories(), "ID", "Name", selectedID);
            ViewBag.CatagoyList = dao.GetCatagories();
        }

        [HttpPost]
        public JsonResult ChangeProductStatus(long? productID)
        {
            var result = new ProductDAO().ChangeProductStatus(productID);
            return Json(new
            {
                status = result
            });
        }

        [HttpPost]
        public JsonResult ChangeProductIncludeVAT(long? productID)
        {
            var result = new ProductDAO().ChangeProductIncludeVAT(productID);
            return Json(new
            {
                status = result
            });
        }

        [HttpDelete]

        public ActionResult Delete(long id)
        {
            new ProductDAO().DeleteProduct(id);
            SetAlert("Delete the Product sucessful!", "success");
            return RedirectToAction("Index", "Product");
        }
    }
}
