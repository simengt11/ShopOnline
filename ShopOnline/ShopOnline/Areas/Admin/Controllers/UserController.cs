using Model.DAO;
using Model.EF;
using ShopOnline.Commond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace ShopOnline.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(string searchString,int page =1, int pageSize=10)
        {
            var dao = new UserDAO();
            var model = dao.ListAllPagingUser(searchString, page, pageSize);
            ViewBag.searchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create (User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                var encryptPasswordToMD5 = Encryptor.MD5Hash(user.Password);
                user.Password = encryptPasswordToMD5;
                var UserSession = (UserSession)Session[CommondConstant.USER_SESSION];
                user.CreatedBy = UserSession.Username;
                if (dao.CheckUserExists(user.Username))
                {
                    ModelState.AddModelError("", "The username has exists!");
                }
                else
                {
                    long id = dao.InsertUser(user);
                    if (id > 0)
                    {
                        SetAlert("Adding the new user sucessful!", "success");
                        return RedirectToAction("Index", "User");
                    }
                        
                    else
                        ModelState.AddModelError("", "An error when add the user!");
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var dao = new UserDAO();
            var user = dao.GetUSerByUserID(id);
            ViewBag.Username = user.Username;
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(User user)
        {
            var dao = new UserDAO();
            if (!string.IsNullOrEmpty(user.Password))
                {
                    var encryptPasswordToMD5 = Encryptor.MD5Hash(user.Password);
                    user.Password = encryptPasswordToMD5;
                }
            var UserSession = (UserSession)Session[CommondConstant.USER_SESSION];
            user.ModifiedBy = UserSession.Username;
            if (dao.UpdateUser(user))
            {
                SetAlert("Editing the user sucessful!", "success");
                return RedirectToAction("Index", "User");
            }
                
            else
                ModelState.AddModelError("", "Có lỗi khi sửa thông tin User!");
            return View();
        }
       [HttpDelete]
       public ActionResult Delete(int id)
        {
            new UserDAO().DeleteUser(id);
            SetAlert("Delete the user sucessful!", "success");
            return RedirectToAction("Index", "User");
        }

        [HttpPost]
        public JsonResult ChangeUserStatus(long? userID)
        {
            var result = new UserDAO().ChangeUserStatus(userID);
            return Json(new
            {
                status = result
            });
        }
    }
}