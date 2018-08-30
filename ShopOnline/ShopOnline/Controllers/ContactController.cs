using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DAO;
using Model.EF;

namespace ShopOnline.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Feedback feedBack)
        {
            var dao = new FeedbackDAO();
            if (ModelState.IsValid)
            {
                if (dao.InsertFeedback(feedBack))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Have an error when add feedback");
                }
            }
            return View();
        }
    }
}