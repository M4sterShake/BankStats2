using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SimpleCrypto;

namespace BankStats.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.UserModel user)
        {
            if (ModelState.IsValid)
            {
                if (this.isValidUser(user.Email, user.Password))
                {
                    FormsAuthentication.SetAuthCookie(user.Email, false);
                    return RedirectToAction("UserHome", "UserHome");
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect.");
                }
            }

            return View(user);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Register(Models.UserModel user)
        {
            if (ModelState.IsValid)
            {
                using (BankStatsDBEntities2 db = new BankStatsDBEntities2())
                {
                    PBKDF2 crypto = new PBKDF2();
                    string salt = crypto.GenerateSalt(1, 100).Substring(0,100);
                    string encryptedPass = crypto.Compute(user.Password, salt);
                    SystemUser sysUser = db.SystemUsers.Create();

                    sysUser.EmailAddress = user.Email;
                    sysUser.Password = encryptedPass;
                    sysUser.Salt = salt;

                    db.SystemUsers.Add(sysUser);
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbEntityValidationException dbEx)
                    {
                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                Logger.Log(String.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage));
                            }
                        }
                    }

                    return RedirectToAction("Index", "Home");
                }
            }
            return View(user);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private bool isValidUser(string email, string password)
        {
            bool isValid = false;
            PBKDF2 crypto = new PBKDF2();

            //Check if the password matches the one in the database
            using (BankStatsDBEntities2 db = new BankStatsDBEntities2())
            {
                SystemUser user = db.SystemUsers.FirstOrDefault(u => u.EmailAddress == email);

                if(user != null)
                {
                    if(user.Password == crypto.Compute(password,user.Salt))
                    {
                        isValid = true;
                    }
                }
            }

            return isValid;
        }
    }
}
