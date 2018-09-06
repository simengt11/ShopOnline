using BotDetect.Web.Mvc;
using Model.DAO;
using Model.EF;
using ShopOnline.Commond;
using ShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode", "ExampleCaptcha", "Incorrect CAPTCHA code!")]

        public ActionResult Register(RegisterModel model)
        {
            var dao = new UserDAO();
            var newUser = new User();
            if (ModelState.IsValid)
            {
                if (!model.Password.Equals(model.ConfirmPassword))
                    ModelState.AddModelError(null, "Confirm password and password must be the same");
                else if (dao.CheckUserExists(model.Name))
                    ModelState.AddModelError(null, "Username has exist!");
                else
                {

                    newUser.CreatedBy = "user";
                    newUser.CreatedDate = DateTime.Now;
                    newUser.Name = model.Name;
                    newUser.Password = Encryptor.MD5Hash(model.Password);
                    newUser.Address = model.Address;
                    if (dao.InsertUser(newUser) > 0)
                        ViewBag.Success = "Success";
                }
            }
            model = new RegisterModel();
            return View(model);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                var result = dao.UserLogin(login.Username, Encryptor.MD5Hash(login.Password));
                if (result)
                {
                    var user = dao.GetUserByUsername(login.Username);
                    var userSession = new UserSession();
                    userSession.Username = user.Username;
                    userSession.UserID = user.ID;
                    //add username to const
                    Session.Add(CommondConstant.USER_SESSION, userSession);
                    TempData["login"] = "login";
                    return RedirectToAction("Index", "Home");
                   
                }
                else
                {
                    ModelState.AddModelError("", "Username or password is incorrect!");
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session[CommondConstant.USER_SESSION] = null;
            return View();
        }
    }
}