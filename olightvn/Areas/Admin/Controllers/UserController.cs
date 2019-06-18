using olightvn.Common;
using olightvn.Models;
using olightvn.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using T.Core.Common;
using T.Core.Filters;

namespace olightvn.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        //
        // GET: /Admin/User/
       private readonly IUserRepository _user;
        private readonly IPermissionRepository _permission;
        private readonly IRoleRepository _role;
        private readonly IEmailRepository _email;
        public UserController(IUserRepository  user, IPermissionRepository permission, IRoleRepository role, IEmailRepository email)
        {
            _user = user;
            _permission = permission;
            _role = role;
            _email = email;
        }
        #region [LogIn]
        public ActionResult Login()
        {
            return View();
        }
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            try
            {
                Session.Clear();
                if (ModelState.IsValid)//kiem tra validate cua du lieu("chi tiet thi vao Models Class:LogOnModel ma xem")
                {

                    string pass = SecureData.EncodeMD5(model.Password);
                    var result = _user.Authenticate(model.UserName, pass);
                    if (result.UserName != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, false);
                        IEnumerable<Permission> permissions = _permission.GetPermissionByUser(result.UserName);

                        foreach (var p in permissions)
                        {
                            SessionManager.UserPermissions += "," + p.Code;
                        }
                        SessionManager.UserPermissions = SessionManager.UserPermissions.Trim(',');
                        if (returnUrl.IsNotNullOrEmpty())
                            return Redirect(returnUrl);
                        return RedirectToAction("Management", "Product");
                    }
                    ModelState.AddModelError("", "Sai tài khoản hoặc mật khẩu.");
                }
            }
            catch
            {
                ModelState.AddModelError("", "Sai tài khoản hoặc mật khẩu.");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        #endregion

        #region [LogOut]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Login");
        }
        #endregion
        #region[Profiles]
        [HttpGet]
        public ActionResult Profiles()
        {
            var user = _user.GetInfo(CurrentUserLogin).CastTo<UserModel>();
            user.Password = null;
            return View(user);
        }

        [HttpPost]
        public ActionResult Profiles(UserModel userModel)
        {
            string message = "Cập nhật thành công.";
            // These field are static
            ModelState.Remove("Email");
            ModelState.Remove("PhoneNumber");
            if (!userModel.ChangePassword)
            {
                ModelState.Remove("Password");
                ModelState.Remove("ConfirmPassword");
                userModel.Password = null;
            }

            if (ModelState.IsValid)
            {
                User user = userModel.CastTo<User>();
                if (userModel.ChangePassword)
                    user.Password = SecureData.EncodeMD5(userModel.Password);
                else
                    user.Password = null;
                var result = _user.Update(user, CurrentUserLogin);
                if (!result)
                    message = "Cập nhật thất bại.";

            }
            else
            {
                message = "Cập nhật thất bại.";
            }
            ModelState.AddModelError("", message);
            return View(userModel);
        }
        #endregion

        #region[Forgot Password]
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(ForgotPassword forgotPassword)
        {
            if (ModelState.IsValid)
            {
                var validation = _user.ValidateUserEmail(forgotPassword.UserName, forgotPassword.Email);
                var message = "Yêu cầu không thành công. Vui lòng liên hệ admin để được giúp đỡ!";

                if (validation)
                {
                    string newPassword = System.Web.Security.Membership.GeneratePassword(7, 0);

                    var html = GeneralFuncs.ReadFile("Templates\\ForgotPasswordTemplate.html");
                    html = html.Replace("[UserName]",forgotPassword.UserName);
                    html = html.Replace("[Email]",forgotPassword.Email);
                    html = html.Replace("[Password]", newPassword);

                    var result = new EmailController(_email).SendEmail(forgotPassword.Email, "Quên mật khẩu", html);
                    if (result)
                    {

                        newPassword = SecureData.EncodeMD5(newPassword);

                        var user = new User() { UserName = forgotPassword.UserName, Password = newPassword };
                        _user.Update(user, CurrentUserLogin);

                        message = "Yêu cầu thành công. Vui lòng kiểm tra email !";
                        ViewBag.Result = true;
                    }
                }
                TempData["message"] = message;
            }
            return View(forgotPassword);
        }
        #endregion

        [AuthorizationRequired(Permissions = "AdminAll")]
        public ActionResult Management()
        {
            return View();
        }
        [HttpPost]
        [AuthorizationRequired(Permissions = "AdminAll")]
        public ActionResult GetAll(Paging paging)
        {
            var data = _user.GetAll(paging);

            paging.GetTotalRow = true;
            int total = _user.GetAll_ToTal(paging);

            var result = new { data = data, total = total };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AuthorizationRequired(Permissions = "AdminAll")]
        public ActionResult Delete(string id)
        {
            var result = _user.Delete(id, CurrentUserLogin);
            return Json(result);
        }

        [HttpGet]
        [AuthorizationRequired(Permissions = "AdminAll")]
        public ActionResult Add(string id)
        {
            var result = string.IsNullOrEmpty(id) ? new User() : _user.GetInfo(id);
            //No change password when edit
            result.Password = null;
            var userModel = result.CastTo<AddUserModel>();

            var role = GetAllRole();
            ViewBag.Roles = new SelectList(role, "Code", "Name", userModel.Role);
            return View(userModel);
        }

        #region[Add new user]
        [HttpPost]
        [AuthorizationRequired(Permissions = "AdminAll")]
        public ActionResult Add(AddUserModel userModel)
        {
            ModelState.Remove("ConfirmPassword");
            if (!userModel.ChangePassword)
            {
                ModelState.Remove("Password");
                userModel.Password = null;
            }

            if (ModelState.IsValid)
            {
                if (userModel.ChangePassword)
                    userModel.Password = SecureData.EncodeMD5(userModel.Password);

                var user = userModel.CastTo<User>();
                var result = _user.Insert(user, CurrentUserLogin);

                if (result)
                    return RedirectToAction("management");

                TempData["UpdateStatus"] = result ? "Cập nhật thành công" : "Cập nhật thất bại";
            }
            var role = GetAllRole();
            ViewBag.Roles = new SelectList(role, "Code", "Name");
            return View(userModel);
        }
        #endregion

        [AuthorizationRequired(Permissions = "AdminAll")]
        private IEnumerable<Role> GetAllRole()
        {
            return _role.GetAll();
        }
    }
}
