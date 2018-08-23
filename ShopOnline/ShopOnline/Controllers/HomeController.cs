using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult _TopMenu()
        {
            var dao = new MenuDAO();
            var model = dao.GetMenuByMenuTypeID(1);
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult _MainMenu()
        {
            var dao = new MenuDAO();
            var model = dao.GetMenuByMenuTypeID(2);
            return PartialView(model);
        }
    }
}