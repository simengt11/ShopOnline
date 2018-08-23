using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Areas.Admin.Controllers
{
    public class MenuController : BaseController
    {
        // GET: Admin/Menu
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            //SetViewBag();
            var dao = new MenuDAO();
            var model = dao.ListAllPagingMenu(searchString, page, pageSize);
            ViewBag.searchString = searchString;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            //SetViewBag();
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(Menu menu)
        {

            if (ModelState.IsValid)
            {
                var dao = new MenuDAO();
                //var UserSession = (UserSession)Session[CommondConstant.USER_SESSION];
                //menu.CreatedBy = UserSession.Username;

                if (dao.InsertMenu(menu))
                {
                    SetAlert("Add the new menu sucessfully!", "success");
                    return RedirectToAction("Index", "Menu");
                }
                else
                    ModelState.AddModelError("", "An error when add the menu!");
            }
            //SetViewBag();
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            //SetViewBag();
            var dao = new MenuDAO();
            var model = dao.GetMenuByID(id);
            return View(model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(Menu menu)
        {
            //SetViewBag();
            var dao = new MenuDAO();
            //var UserSession = (UserSession)Session[CommondConstant.USER_SESSION];
            //Content.ModifiedBy = UserSession.Username;
            //Content.ModifiedDate = DateTime.Now;
            if (dao.UpdateMenu(menu))
            {
                SetAlert("Edit the user sucessfull!", "success");
                return RedirectToAction("Index", "Menu");
            }

            else
                ModelState.AddModelError("", "An error when edit menu!");
            return View();
        }

        //public void SetViewBag(long? selectedID = null)
        //{
        //    var dao = new ContentCatagoryDAO();
        //    ViewBag.CatagoryID = new SelectList(dao.GetCatagories(), "ID", "Name", selectedID);
        //    ViewBag.CatagoyList = dao.GetCatagories();
        //}

        [HttpPost]
        public JsonResult ChangeMenuStatus(int? menuID)
        {
            var result = new MenuDAO().ChangeMenuStatus(menuID);
            return Json(new
            {
                status = result
            });
        }

        [HttpDelete]

        public ActionResult Delete(int id)
        {
            new MenuDAO().DeleteMenu(id);
            SetAlert("Delete the menu sucessful!", "success");
            return RedirectToAction("Index", "Menu");
        }
    }
}