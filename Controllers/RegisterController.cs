using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Storenarm2.Models.Domins;
using Storenarm2.Models.Plugins;

namespace Storenarm2.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register

        DB db = new DB();

        [HttpGet]
        public ActionResult Register()
        {
            if (Session["User"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Profile", "Profile");
            }
        }



        [HttpPost]
        public ActionResult Register(Tbl_User u, string State)
        {

            if (Session["User"] == null)
            {
                Tbl_User ur = new Tbl_User();

                ur.User_Active = false;
                ur.User_Address = u.User_Address;
                ur.User_City = u.User_City;
                ur.User_Date = DateTime.Now;
                ur.User_Email = u.User_Email;
                ur.User_Mobile = u.User_Mobile;
                ur.User_NameFamily = u.User_NameFamily;
                ur.User_NationalCode = u.User_NationalCode;
                ur.User_Password = u.User_Password;
                ur.User_PostalCode = u.User_PostalCode;
                ur.User_Rating = "0";
                ur.User_Roleid = 1;
                ur.User_Stateid = Convert.ToInt32(State);
                ur.User_Tel = u.User_Tel;
                ur.User_Username = u.User_Username;

                db.Tbl_User.Add(ur);

                if (Convert.ToBoolean(db.SaveChanges() > 0))
                {
                    TempData["Message"] = " .ثبت نام شما با موفقیت انجام شد. لطفا به ایمیل خود مراجعه و روی لینک تایید کلیک نمایید";


                    //ایمیل فعالسازی را ارسال کنم
                    Tbl_ConfirmEmail ce = new Tbl_ConfirmEmail();

                    ce.ConfirmEamil_Date = DateTime.Today;
                    ce.ConfirmEamil_Status = false;
                    ce.ConfirmEamil_Userid = db.Tbl_User.OrderByDescending(a => a.User_ID).FirstOrDefault().User_ID;

                    db.Tbl_ConfirmEmail.Add(ce);
                    db.SaveChanges();

                    Email email = new Email();

                    string email_User = db.Tbl_User.OrderByDescending(a => a.User_ID).FirstOrDefault().User_Email;

                    email.SendEmail(db.Tbl_Setting.FirstOrDefault().Smtp, db.Tbl_Setting.FirstOrDefault().Email, db.Tbl_Setting.FirstOrDefault().Passsword, email_User, " فعالسازی اکانت کاربری در سایت " + db.Tbl_Setting.FirstOrDefault().Tilte + "", "کاربر گرامی : " + u.User_NameFamily + " <br /> برای فعالسازی اکانت کاربری خود درسایت " + db.Tbl_Setting.FirstOrDefault().Tilte + " روی لینک زیر کلیک نمایید. <br /> <a href='http://localhost:59195/Register/ConfirmEmails?Code=" + db.Tbl_ConfirmEmail.OrderByDescending(a => a.ConfirmEamil_ID).FirstOrDefault().ConfirmEamil_ID + "'> لینک فعالسازی شما </a>");

                    return RedirectToAction("Massage");

                }
                else
                {
                    TempData["Message"] = "NotAdded";

                    return RedirectToAction("Massage");
                }
            }
            else
            {
                return RedirectToAction("Profile", "Profile");
            }
        }



        public ActionResult Massage()
        {
            ViewBag.Message = TempData["Errors"];
            ViewBag.State = TempData["Info"];
            ViewBag.Message = TempData["Message"];
            return View();
        }

        public ActionResult ForgotPass()
        {
            //ViewBag.Message = TempData["Errors"];
            //ViewBag.State = TempData["Info"];
            //ViewBag.Message = TempData["ForgotPass"];
            return View();
        }

        //public ActionResult ConfirmEmails(int Code = 0)
        //{

        //    if (Code != 0)
        //    {
        //        var qcode = db.Tbl_ConfirmEmail.Where(a => a.ConfirmEamil_ID.Equals(Code)).SingleOrDefault();
        //        if (qcode == null)
        //        {
        //            TempData["Info"] = "Error";
        //            TempData["Errors"] = "خطایی در فعالسازی اکانت شما رخ داد!!";
        //            return RedirectToAction("Massage");
        //        }


        //        if (qcode.ConfirmEamil_Date.AddDays(2) < DateTime.Today)
        //        {
        //            TempData["Errors"] = "زمان انقضای تایید اکانت شما به اتمام رسیده است !!";
        //            return RedirectToAction("Massage");
        //        }

        //        if (qcode.ConfirmEamil_Status == false)
        //        {
        //            // ایمیل دفعه اول است تایید می شود
        //            qcode.ConfirmEamil_Status = true;
        //            db.Tbl_ConfirmEmail.Attach(qcode);
        //            db.Entry(qcode).State = System.Data.Entity.EntityState.Modified;
        //            db.SaveChanges();

        //        }
        //        else
        //        {
        //            //ایمیل یک بار تایید شده
        //            TempData["Errors"] = "اکانت کاربری شما یک بار تایید شده!!";
        //            TempData["Info"] = "Error";
        //            return RedirectToAction("Massage");
        //        }

        //        int userid = qcode.ConfirmEamil_Userid;

        //        var quserid = db.Tbl_User.Where(a => a.User_ID.Equals(userid)).SingleOrDefault();

        //        if (quserid == null)
        //        {
        //            TempData["Info"] = "Error";
        //            return RedirectToAction("Massage");
        //        }
        //        else
        //        {
        //            if (quserid.User_Active == false)
        //            {// اکانت دفعه اول است تایید می شود
        //                quserid.User_Active = true;
        //                db.Tbl_User.Attach(quserid);
        //                db.Entry(quserid).State = System.Data.Entity.EntityState.Modified;
        //                db.SaveChanges();

        //                TempData["Errors"] = "اکنون می توانید وارد سایت شوید";
        //                TempData["Info"] = "Error";
        //                return RedirectToAction("Login");
        //            }
        //            else
        //            {
        //                //اکانت کاربری یک بار تایید شده
        //                TempData["Errors"] = "اکانت کاربری شما یک بار تایید شده!!";
        //                TempData["Info"] = "Error";
        //                return RedirectToAction("Massage");
        //            }
        //        }

        //    }
        //    else
        //    {
        //        TempData["Info"] = "Error";
        //        TempData["Errors"] = "خطایی در فعالسازی اکانت شما رخ داد!!";
        //        return RedirectToAction("Massage");
        //    }


        //}



        [HttpPost]
        public JsonResult UsernameValid(string User_Username)
        {
            try
            {

                var q = db.Tbl_User.Where(a => a.User_Username == User_Username).SingleOrDefault();

                if (q == null)
                {
                    return Json(true, JsonRequestBehavior.DenyGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.DenyGet);
                }

            }
            catch
            {

                return Json(false, JsonRequestBehavior.DenyGet);
            }
        }


        [HttpPost]
        public JsonResult EamilValid(string User_Email)
        {
            try
            {

                var q = db.Tbl_User.Where(a => a.User_Email == User_Email).SingleOrDefault();

                if (q == null)
                {
                    return Json(true, JsonRequestBehavior.DenyGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.DenyGet);
                }

            }
            catch
            {

                return Json(false, JsonRequestBehavior.DenyGet);
            }
        }

        [HttpPost]
        public JsonResult MobileValid(string User_Mobile)
        {
            try
            {

                var q = db.Tbl_User.Where(a => a.User_Mobile == User_Mobile).SingleOrDefault();

                if (q == null)
                {
                    return Json(true, JsonRequestBehavior.DenyGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.DenyGet);
                }

            }
            catch
            {

                return Json(false, JsonRequestBehavior.DenyGet);
            }
        }


        [HttpPost]
        public JsonResult NationalCodeValid(string User_NationalCode)
        {
            try
            {

                var q = db.Tbl_User.Where(a => a.User_NationalCode == User_NationalCode).SingleOrDefault();

                if (q == null)
                {
                    return Json(true, JsonRequestBehavior.DenyGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.DenyGet);
                }

            }
            catch
            {

                return Json(false, JsonRequestBehavior.DenyGet);
            }
        }




        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Login(string emailusername, string passwordlogin)
        {
            try
            {
                if (Session["User"] != null)
                    return RedirectToAction("Profile", "Profile");


                if (emailusername == "" || passwordlogin == "")
                {
                    ViewBag.Message = " شما مجاز به ورود نیستید!!";
                    ViewBag.State = "Error";
                    return View();
                }


                var quser = (from a in db.Tbl_User where (a.User_Username == emailusername || a.User_Email == emailusername) && a.User_Password == passwordlogin select a).SingleOrDefault();
                if (quser != null)
                {

                    if (quser.User_Active != true)
                    {
                        ViewBag.Message = " شما هنوز ایمیل خود را تایید نکرده اید!!";
                        ViewBag.State = "Error";
                        return View();
                    }

                    Session["User"] = quser.User_Username;
                    Session["Role"] = quser.Tbl_Role.Role_Name;

                    return RedirectToAction("Profile", "Profile");



                }
                else
                {
                    //نام کاربری اشتباه است
                    ViewBag.Message = "نام کاربری یا کلمه عبور اشتباه است!!";
                    ViewBag.State = "Error";
                    return View();
                }

            }
            catch
            {
                ViewBag.Message = " شما مجاز به ورود نیستید!!";
                ViewBag.State = "Error";
                return View();

            }
        }



        public ActionResult Logout()
        {
            try
            {
                if (Session["User"] != null)
                {
                    Session["User"] = null;
                    Session["Role"] = null;

                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return RedirectToAction("Profile", "Profile");
            }

        }

    }//پایان
}