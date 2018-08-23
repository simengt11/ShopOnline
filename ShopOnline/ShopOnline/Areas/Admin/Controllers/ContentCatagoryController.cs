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
    public class ContentCatagoryController : BaseController
    {
        // GET: Admin/catagoryCatagory
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            SetViewBag();
            var dao = new ContentCatagoryDAO();
            var model = dao.ListAllPagingContentCatagory(searchString, page, pageSize);
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
        public ActionResult Create(Catagory catagory)
        {

            if (ModelState.IsValid)
            {
                var dao = new ContentCatagoryDAO();
                var UserSession = (UserSession)Session[CommondConstant.USER_SESSION];
                catagory.CreatedBy = UserSession.Username;

                if (dao.InsertContentCatagory(catagory))
                {
                    SetAlert("Add the new content catagory sucessfully!", "success");
                    return RedirectToAction("Index", "ContentCatagory");
                }
                else
                    ModelState.AddModelError("", "An error when add the content catagory!");
            }
            SetViewBag();
            return View();
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            SetViewBag();
            var dao = new ContentCatagoryDAO();
            var catagory = dao.GetContentCatagoryByID(id);
            return View(catagory);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(Catagory catagory)
        {
            SetViewBag();
            var dao = new ContentCatagoryDAO();
            var UserSession = (UserSession)Session[CommondConstant.USER_SESSION];
            catagory.ModifiedBy = UserSession.Username;
            catagory.ModifiedDate = DateTime.Now;
            if (dao.UpdateContentCatagory(catagory))
            {
                SetAlert("Edit the Content Catagory sucessfull!", "success");
                return RedirectToAction("Index", "ContentCatagory");
            }

            else
                ModelState.AddModelError("", "An error when edit content catagory!");
            return View();
        }

        public void SetViewBag(long? selectedID = null)
        {
            var dao = new ContentCatagoryDAO();
            var list = dao.GetCatagories();
            list.Add(null);
            ViewBag.ParentID = new SelectList(list, "ID", "Name", selectedID);
            ViewBag.ParentIDList = dao.GetCatagories();
        }

        [HttpPost]
        public JsonResult ChangeContentCatagoryStatus(long? catagoryID)
        {
            var result = new ContentCatagoryDAO().ChangeContentCatagoryStatus(catagoryID);
            return Json(new
            {
                status = result
            });
        }

        [HttpPost]
        public JsonResult ChangeContentCatagoryShowOnHome(long? catagoryID)
        {
            var result = new ContentCatagoryDAO().ChangeContentCatagoryShowOnHome(catagoryID);
            return Json(new
            {
                status = result
            });
        }

        [HttpDelete]

        public ActionResult Delete(long id)
        {
            new ContentCatagoryDAO().DeleteContentCatagory(id);
            SetAlert("Delete the content catagory sucessful!", "success");
            return RedirectToAction("Index", "ContentCatagory");
        }
    }
}
