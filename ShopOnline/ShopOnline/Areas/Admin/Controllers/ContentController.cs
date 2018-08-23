using Model.EF;
using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopOnline.Commond;

namespace ShopOnline.Areas.Admin.Controllers
{
    public class ContentController : BaseController
    {
        // GET: Admin/Content
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            SetViewBag();
            var dao = new ContentDAO();
            var model = dao.ListAllPagingContent(searchString, page, pageSize);
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
        public ActionResult Create(Content Content)
        {
            
            if (ModelState.IsValid)
            {
                var dao = new ContentDAO();
                var UserSession = (UserSession)Session[CommondConstant.USER_SESSION];
                Content.CreatedBy = UserSession.Username;

                if (dao.InsertContent(Content))
                {
                    SetAlert("Add the new Content sucessfully!", "success");
                    return RedirectToAction("Index", "Content");
                }
                else
                    ModelState.AddModelError("", "An error when add the Content!");
            }
            SetViewBag();
            return View();
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            SetViewBag();
            var dao = new ContentDAO();
            var Content = dao.GetContentByID(id);
            return View(Content);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(Content Content)
        {
            SetViewBag();
            var dao = new ContentDAO();
            var UserSession = (UserSession)Session[CommondConstant.USER_SESSION];
            Content.ModifiedBy = UserSession.Username;
            Content.ModifiedDate = DateTime.Now;
            if (dao.UpdateContent(Content))
            {
                SetAlert("Edit the user sucessfull!", "success");
                return RedirectToAction("Index", "Content");
            }

            else
                ModelState.AddModelError("", "An error when edit Content!");
            return View();
        }

        public void SetViewBag(long? selectedID=null)
        {
            var dao = new ContentCatagoryDAO();
            ViewBag.CatagoryID = new SelectList(dao.GetCatagories(), "ID", "Name", selectedID);
            ViewBag.CatagoyList = dao.GetCatagories();
        }

        [HttpPost]
        public JsonResult ChangeContentStatus(long? ContentID)
        {
            var result = new ContentDAO().ChangeContentStatus(ContentID);
            return Json(new
            {
                status = result
            });
        }

        [HttpDelete]

        public ActionResult Delete(long id)
        {
            new ContentDAO().DeleteContent(id);
            SetAlert("Delete the Content sucessful!", "success");
            return RedirectToAction("Index", "Content");
        }
    }
}