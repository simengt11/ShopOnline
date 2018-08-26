using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DAO;
using Model.EF;
using ShopOnline.Commond;

namespace ShopOnline.Areas.Admin.Controllers
{
    public class SlideController : BaseController
    {
        // GET: Admin/Silde
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            //SetViewBag();
            var dao = new SlideDAO();
            var model = dao.ListAllPagingSlide(searchString, page, pageSize);
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
        public ActionResult Create(Slide slide)
        {
            //SetViewBag();
            if (ModelState.IsValid)
            {
                var dao = new SlideDAO();
                var UserSession = (UserSession)Session[CommondConstant.USER_SESSION];
                slide.CreatedBy = UserSession.Username;

                if (dao.InsertSlide(slide))
                {
                    SetAlert("Add the new slide sucessfully!", "success");
                    return RedirectToAction("Index", "Slide");
                }
                else
                    ModelState.AddModelError("", "An error when add the Slide!");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            //SetViewBag();
            var dao = new SlideDAO();
            var slide = dao.GetSlideByID(id);
            return View(slide);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(Slide slide)
        {
            //SetViewBag();
            var dao = new SlideDAO();
            var UserSession = (UserSession)Session[CommondConstant.USER_SESSION];
            slide.ModifiedBy = UserSession.Username;
            slide.ModifiedDate = DateTime.Now;
            if (dao.UpdateSlide(slide))
            {
                SetAlert("Edit the slide sucessfull!", "success");
                return RedirectToAction("Index", "Slide");
            }

            else
                ModelState.AddModelError("", "An error when edit slide!");
            return View();
        }

        //public void SetViewBag(long? selectedID = null)
        //{
        //    var dao = new ProductCatagoryDAO();
        //    ViewBag.CatalogyID = new SelectList(dao.GetCatagories(), "ID", "Name", selectedID);
        //    ViewBag.CatagoyList = dao.GetCatagories();
        //}

        [HttpPost]
        public JsonResult ChangeSlideStatus(long? slideID)
        {
            var result = new SlideDAO().ChangeSlideStatus(slideID);
            return Json(new
            {
                status = result
            });
        }


        [HttpDelete]

        public ActionResult Delete(int id)
        {
            new SlideDAO().DeleteSlide(id);
            SetAlert("Delete the slide sucessful!", "success");
            return RedirectToAction("Index", "Product");
        }
    }
}