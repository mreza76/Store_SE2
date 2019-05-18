using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Storenarm2.Models.Domins;
using Storenarm2.Models.Plugins;

namespace Storenarm2.Controllers
{
    public class HomeController : Controller
    {


        DB db = new DB();
        Email email = new Email();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Page404()
        {
            return View();
        }



        public ActionResult Search(string keywordSearch, int Searchid = 0)
        {
            try
            {
                if (Searchid == 0)
                {
                    return RedirectToAction("Index");
                }

                var qid = db.Tbl_Group.Where(a => a.Group_ID == Searchid).FirstOrDefault();

                if (qid == null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    if (keywordSearch == "" || keywordSearch == null)
                    {
                        var q = db.Tbl_Product.Where(a => a.Product_Groupid == Searchid).ToList();

                        if (q == null)
                        {
                            ViewBag.State = "Error";
                            ViewBag.Key = keywordSearch;
                            ViewBag.Groupid = Searchid;
                            return View();
                        }
                        else
                        {

                            ViewBag.State = "Ok";
                            ViewBag.Key = keywordSearch;
                            ViewBag.Groupid = Searchid;
                            return View(q);
                        }
                    }
                    else
                    {
                        var search = (from a in db.Tbl_Product where a.Product_Groupid == Searchid && a.Product_Name.Contains(keywordSearch) select a).ToList();

                        if (search == null)
                        {
                            ViewBag.State = "Error";
                            ViewBag.Key = keywordSearch;
                            ViewBag.Groupid = Searchid;
                            return View();

                        }
                        else
                        {
                            ViewBag.State = "Error";
                            ViewBag.Key = keywordSearch;
                            ViewBag.Groupid = Searchid;
                            return View(search);
                        }
                    }
                }

            }
            catch
            {
                ViewBag.State = "Error";
                ViewBag.Key = keywordSearch;
                ViewBag.Groupid = Searchid;
                return View();
            }
        }

        //20-f2
        public ActionResult ShowProduct(int id = 0)
        {

            if (id == 0)
            {
                return RedirectToAction("Page404");
            }
            else
            {

                var q = db.Tbl_Product.Where(a => a.Product_ID == id && a.Product_Active == true && a.Tbl_User.User_Active == true).SingleOrDefault();


                if (q == null)
                {

                    return RedirectToAction("Page404");
                }
                else
                {
                    if (q.Product_IsDownload == true)
                    {
                        var qd = db.Tbl_Download.Where(a => a.Download_Productid == q.Product_ID).FirstOrDefault();

                        if (qd == null)
                            return RedirectToAction("Page404");

                        q.Product_Visit = q.Product_Visit + 1;
                        db.Tbl_Product.Attach(q);
                        db.Entry(q).State = System.Data.Entity.EntityState.Modified;
                        if (Convert.ToBoolean(db.SaveChanges() > 0))
                        {
                            return View(q);
                        }
                        else
                        {
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        q.Product_Visit = q.Product_Visit + 1;
                        db.Tbl_Product.Attach(q);
                        db.Entry(q).State = System.Data.Entity.EntityState.Modified;
                        if (Convert.ToBoolean(db.SaveChanges() > 0))
                        {
                            return View(q);
                        }
                        else
                        {
                            return RedirectToAction("Index");
                        }
                    }



                }
            }


        }
        //no-f2
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(string username, string email, string text)
        {
            try
            {
                //if (!this.IsCaptchaValid("Error"))
                //{
                //    ViewBag.State = "Error";
                //    ViewBag.Error = "کد امنیتی وارد شده نادرست است";
                //    return View();
                //}

                var qmailadmin = db.Tbl_User.Where(a => a.Tbl_Role.Role_Name == "Admin").FirstOrDefault();

                if (qmailadmin == null)
                {
                    ViewBag.State = "Error";
                    ViewBag.Error = "پیام ارسال نشد!!!";
                    return View();
                }

                Tbl_Message m = new Tbl_Message();
                m.Message_Body = text + " / " + email;
                m.Message_Date = DateTime.Now;
                m.Message_Read = false;
                m.Message_Title = username;
                m.Message_UserGet = qmailadmin.User_ID;

                db.Tbl_Message.Add(m);
                db.SaveChanges();

                Email emails = new Email();
                var qsmtp = db.Tbl_Setting.FirstOrDefault().Smtp;
                var qemail = db.Tbl_Setting.FirstOrDefault().Email;
                var qpassword = db.Tbl_Setting.FirstOrDefault().Passsword;

                emails.SendEmail(qsmtp, qemail, qpassword, qmailadmin.User_Email, username, "شما یک پیام از کاربر " + email + " با محتوای زیر دریافت کردید <br /> " + text + "");

                ViewBag.State = "OK";
                ViewBag.Error = "پیام شما با موفقیت ارسال شد.";
                return View();

            }
            catch
            {

                ViewBag.State = "Error";
                ViewBag.Error = "پیام ارسال نشد!!!";
                return View();

            }
        }









    }

}