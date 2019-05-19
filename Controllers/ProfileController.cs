using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Storenarm2.Models.Plugins;
using Storenarm2.Models.Domins;
using System.IO;


namespace Storenarm2.Controllers
{
    public class ProfileController : Controller
    {

        DB db = new DB();
        
        // GET: Profile
        public ActionResult Profile()
        {
            if (Session["User"] == null)
                return RedirectToAction("Login", "Register");

            ViewBag.State = TempData["State"];
            ViewBag.Error = TempData["Error"];
            return View();
        }


        //public ActionResult Identity(int id = 0)
        //{
        //    if (Session["User"] == null)
        //        return RedirectToAction("Login", "Register");

        //    string username = Session["User"].ToString();

        //    var quser = db.Tbl_User.Where(a => a.User_Username == username).SingleOrDefault();

        //    if (quser != null)
        //    {
        //        var q_id = db.Tbl_User.Where(a => a.User_ID == id).SingleOrDefault();
        //        return View(q_id);
        //    }
        //    else
        //    {
        //        TempData["State"] = "Error";
        //        return RedirectToAction("Profile");
        //    }

        //}


        ////13-f2
        //public ActionResult ConfirmIdentity(string fathername, string year, string month, string day, int iduser, HttpPostedFileBase imagecart)
        //{
        //    if (Session["User"] == null)
        //        return RedirectToAction("Login", "Register");

        //    string username = Session["User"].ToString();

        //    var quser = db.Tbl_User.Where(a => a.User_Username == username).SingleOrDefault();

        //    if (quser != null)
        //    {
        //        Tbl_Identity i = new Tbl_Identity();
        //        i.Identity_Birth = year + "/" + month + "/" + day;
        //        i.Identity_Confirm = false;
        //        i.Identity_FathersName = fathername;
        //        i.Identity_Userid = quser.User_ID;

        //        Random rnd = new Random();
        //        string rndname = imagecart.FileName + "-" + rnd.Next(1, 999).ToString() + ".jpg";
        //        if (imagecart != null)
        //        {
        //            if (imagecart.ContentLength <= 512000)
        //            {
        //                imagecart.SaveAs(Path.Combine(Server.MapPath("~") + "/Content/_images/cart/" + rndname));
        //            }
        //            else
        //            {
        //                ViewBag.Error = " حجم تصویر بیش از 500 کیلو بایت است";
        //                return View();
        //            }


        //        }
        //        else
        //        {
        //            ViewBag.Error = "تصویر باید انتخاب گردد";
        //            return View();
        //        }

        //        i.Identity_Pic = rndname;


        //        db.Tbl_Identity.Add(i);
        //        db.SaveChanges();

        //        TempData["State"] = "OK";

        //        return RedirectToAction("Profile");

        //    }
        //    else
        //    {
        //        return RedirectToAction("Profile");
        //    }
        //}


        //14(2)-f2
        [HttpGet]
        public ActionResult AddProduct()
        {
            if (Session["User"] == null)
                return RedirectToAction("Login", "Register");

            string username = Session["User"].ToString();
            string rolename = Session["Role"].ToString();
            


            if (rolename == "Seller" || rolename == "Admin")
            {

                var quser = db.Tbl_User.Where(a => a.User_Username == username && a.Tbl_Role.Role_Name == rolename).SingleOrDefault();

                if (quser != null)
                {
                    var quser1 = db.Tbl_Identity.Where(a => a.Identity_Userid == quser.User_ID && a.Identity_Confirm == true).SingleOrDefault();

                    if (quser1 != null)
                    {
                        return View();
                    }
                    else
                    {
                        TempData["State"] = "Error";
                        return RedirectToAction("Profile", "Profile");
                    }
                }
                else
                {
                    TempData["State"] = "Error";
                    return RedirectToAction("Profile", "Profile");
                }


            }
            else
            {
                TempData["State"] = "Error";
                return RedirectToAction("Profile", "Profile");
            }


        }

        [HttpPost]
        public ActionResult AddProduct(Tbl_Product p, HttpPostedFileBase imageindex, HttpPostedFileBase[] picgallery, string TagProduct, string cat1_id)
        {
            if (Session["User"] == null)
                return RedirectToAction("Login", "Register");

            string username = Session["User"].ToString();
            string rolename = Session["Role"].ToString();


            if (rolename == "Seller" || rolename == "Admin")
            {

                var quser = db.Tbl_User.Where(a => a.User_Username == username && a.Tbl_Role.Role_Name == rolename).SingleOrDefault();

                if (quser != null)
                {
                    var quser1 = db.Tbl_Identity.Where(a => a.Identity_Userid == quser.User_ID && a.Identity_Confirm == true).SingleOrDefault();

                    if (quser1 != null)
                    {
                        Random rnd = new Random();

                        if (imageindex == null)
                        {
                            ViewBag.Error = "تصویر باید انتخاب گردد";
                            return View();
                        }

                        //-------------------------------------------
                        //  Check the image mime types
                        //-------------------------------------------
                        if (imageindex.ContentType.ToLower() != "image/jpg" &&
                                    imageindex.ContentType.ToLower() != "image/jpeg" &&
                                    imageindex.ContentType.ToLower() != "image/pjpeg" &&
                                    imageindex.ContentType.ToLower() != "image/gif" &&
                                    imageindex.ContentType.ToLower() != "image/x-png" &&
                                    imageindex.ContentType.ToLower() != "image/png")
                        {
                            ViewBag.Error = " نوع فایل انتخابی غیر مجاز است ";
                            return View();
                        }

                        //-------------------------------------------
                        //  Check the image extension
                        //-------------------------------------------
                        if (Path.GetExtension(imageindex.FileName).ToLower() != ".jpg"
                            && Path.GetExtension(imageindex.FileName).ToLower() != ".png"
                            && Path.GetExtension(imageindex.FileName).ToLower() != ".gif"
                            && Path.GetExtension(imageindex.FileName).ToLower() != ".jpeg")
                        {
                            ViewBag.Error = "  فایل انتخابی غیر مجاز است ";
                            return View();
                        }



                        if (imageindex.ContentLength >= 512000)
                        {

                            ViewBag.Error = " حجم تصویر بیش از 500 کیلو بایت است";
                            return View();
                        }


                        string rndname = rnd.Next(1, 999).ToString() + "-" + imageindex.FileName;
                        imageindex.SaveAs(Path.Combine(Server.MapPath("~") + "/Content/_images/product/" + rndname));
                        Tbl_Product t = new Tbl_Product();
                        t.Product_PicIndex = rndname;
                        t.Product_AllOff = p.Product_AllOff;
                        t.Product_Date = DateTime.Now;
                        t.Product_ExitCount = p.Product_ExitCount;
                        t.Product_Groupid = Convert.ToInt32(cat1_id);
                        t.Product_IsDownload = p.Product_IsDownload;
                        t.Product_Name = p.Product_Name;
                        t.Product_Off = p.Product_Off;
                        t.Product_Price = p.Product_Price;
                        t.Product_Text = p.Product_Text;
                        t.Product_Userid = quser.User_ID;
                        t.Product_Visit = 0;
                        t.Product_Weight = p.Product_Weight;
                        t.Product_Active = false;

                        db.Tbl_Product.Add(t);
                        if (Convert.ToBoolean(db.SaveChanges() > 0))
                        {

                            //بخش اضافه شدن تگ
                            var tagname = TagProduct.Trim().Split('-');

                            foreach (var item1 in tagname)
                            {
                                Tbl_Tags tg = new Tbl_Tags()
                                {
                                    Tag_Proid = db.Tbl_Product.OrderByDescending(a => a.Product_ID).FirstOrDefault().Product_ID,
                                    Tag_Name = item1.Trim()

                                };

                                db.Tbl_Tags.Add(tg);
                                db.SaveChanges();
                            }


                            //تصاویر گالری

                            if (picgallery != null)
                            {
                                List<Tbl_Pic> lstpic = new List<Tbl_Pic>();
                                foreach (var item in picgallery)
                                {
                                    string namepic = rnd.Next().ToString() + "-" + item.FileName;


                                    if (item == null)
                                    {
                                        var qproid = db.Tbl_Product.OrderByDescending(a => a.Product_ID).FirstOrDefault();
                                        var qtagid = db.Tbl_Tags.Where(a => a.Tag_Proid == qproid.Product_ID).ToList();

                                        db.Tbl_Tags.RemoveRange(qtagid);

                                        db.Tbl_Product.Remove(qproid);

                                        db.SaveChanges();

                                        ViewBag.Error = "تصویر گالری باید انتخاب گردد";
                                        return View();
                                    }

                                    //-------------------------------------------
                                    //  Check the image mime types
                                    //-------------------------------------------
                                    if (imageindex.ContentType.ToLower() != "image/jpg" &&
                                                imageindex.ContentType.ToLower() != "image/jpeg" &&
                                                imageindex.ContentType.ToLower() != "image/pjpeg" &&
                                                imageindex.ContentType.ToLower() != "image/gif" &&
                                                imageindex.ContentType.ToLower() != "image/x-png" &&
                                                imageindex.ContentType.ToLower() != "image/png")
                                    {
                                        var qproid = db.Tbl_Product.OrderByDescending(a => a.Product_ID).FirstOrDefault();
                                        var qtagid = db.Tbl_Tags.Where(a => a.Tag_Proid == qproid.Product_ID).ToList();

                                        db.Tbl_Tags.RemoveRange(qtagid);

                                        db.Tbl_Product.Remove(qproid);

                                        db.SaveChanges();
                                        ViewBag.Error = " نوع فایل انتخابی غیر مجاز است ";
                                        return View();
                                    }

                                    //-------------------------------------------
                                    //  Check the image extension
                                    //-------------------------------------------
                                    if (Path.GetExtension(imageindex.FileName).ToLower() != ".jpg"
                                        && Path.GetExtension(imageindex.FileName).ToLower() != ".png"
                                        && Path.GetExtension(imageindex.FileName).ToLower() != ".gif"
                                        && Path.GetExtension(imageindex.FileName).ToLower() != ".jpeg")
                                    {
                                        var qproid = db.Tbl_Product.OrderByDescending(a => a.Product_ID).FirstOrDefault();
                                        var qtagid = db.Tbl_Tags.Where(a => a.Tag_Proid == qproid.Product_ID).ToList();

                                        db.Tbl_Tags.RemoveRange(qtagid);

                                        db.Tbl_Product.Remove(qproid);

                                        db.SaveChanges();

                                        ViewBag.Error = "  فایل انتخابی غیر مجاز است ";
                                        return View();
                                    }


                                    if (item.ContentLength <= 512000)
                                    {
                                        Tbl_Pic pic = new Tbl_Pic();
                                        pic.Pic_Proid = db.Tbl_Product.OrderByDescending(a => a.Product_ID).FirstOrDefault().Product_ID;
                                        item.SaveAs(Path.Combine(Server.MapPath("~") + "/Content/_images/product/Gallery/" + namepic));
                                        pic.Pic_Name = namepic;
                                        lstpic.Add(pic);
                                    }
                                    else
                                    {

                                        var qproid = db.Tbl_Product.OrderByDescending(a => a.Product_ID).FirstOrDefault();
                                        var qtagid = db.Tbl_Tags.Where(a => a.Tag_Proid == qproid.Product_ID).ToList();

                                        db.Tbl_Tags.RemoveRange(qtagid);

                                        db.Tbl_Product.Remove(qproid);

                                        db.SaveChanges();


                                        ViewBag.Error = " حجم تصویر گالری بیش از 500 کیلو بایت است";
                                        return View();
                                    }
                                }

                                db.Tbl_Pic.AddRange(lstpic);
                                db.SaveChanges();

                            }
                            else
                            {
                                var qproid = db.Tbl_Product.OrderByDescending(a => a.Product_ID).FirstOrDefault();
                                var qtagid = db.Tbl_Tags.Where(a => a.Tag_Proid == qproid.Product_ID).ToList();

                                db.Tbl_Tags.RemoveRange(qtagid);

                                db.Tbl_Product.Remove(qproid);

                                db.SaveChanges();

                                ViewBag.Error = "تصویر گالری باید انتخاب گردد";
                                return View();
                            }

                            var qprorat = db.Tbl_Product.OrderByDescending(a => a.Product_ID).FirstOrDefault();

                            var iduser = db.Tbl_User.Where(a => a.User_ID == qprorat.Product_Userid).SingleOrDefault();
                            if (iduser != null)
                            {
                                iduser.User_Rating = iduser.User_Rating + 10;

                                db.Tbl_User.Attach(iduser);
                                db.Entry(iduser).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                            }


                            var qsmtp = db.Tbl_Setting.FirstOrDefault().Smtp;
                            var qemail = db.Tbl_Setting.FirstOrDefault().Email;
                            var qpassword = db.Tbl_Setting.FirstOrDefault().Passsword;
                            // TempData["OK"] ="محصول با موفقیت ثبت شده";
                            TempData["Result"] = "OK";

                            Email email = new Email();
                            email.SendEmail(qsmtp, qemail, qpassword, quser.User_Email, "ثبت محصول جدید در فروشگاه", "کاربر گرامی  آقا / خانم : " + quser.User_NameFamily + " <br /> محصول شما با عنوان " + p.Product_Name + " در سایت ما ثبت گردید . <br /> لطفا جهت نمایش بروی سایت برای فروش تا تایید مدیریت فروشگاه شکیبا باشیدبا سپاس از شما گرامی.");
                            Tbl_Message m = new Tbl_Message();
                            m.Message_Body = "کاربر گرامی  آقا / خانم : " + quser.User_NameFamily + " <br /> محصول شما با عنوان " + p.Product_Name + " در سایت ما ثبت گردید . <br /> لطفا جهت نمایش بروی سایت برای فروش تا تایید مدیریت فروشگاه شکیبا باشیدبا سپاس از شما گرامی.";
                            m.Message_Date = DateTime.Now;
                            m.Message_Read = false;
                            m.Message_Title = "ثبت محصول جدید در فروشگاه";
                            m.Message_UserGet = quser.User_ID;
                            db.Tbl_Message.Add(m);
                            db.SaveChanges();


                            return RedirectToAction("ListProduct");
                        }//end savchange
                        else
                        {
                            TempData["Result"] = "Error";
                            return RedirectToAction("ListProduct");
                        }

                    }//end quser1
                    else
                    {
                        TempData["Result"] = "Error";
                        return RedirectToAction("ListProduct");
                    }
                }//end quser
                else
                {
                    TempData["State"] = "Error";
                    return RedirectToAction("Profile", "Profile");
                }
            }//end role admin or seller
            else
            {
                TempData["State"] = "Error";
                return RedirectToAction("Profile", "Profile");
            }


        }

        //18-f2
        public ActionResult ListProduct()
        {
            if (Session["User"] == null)
                return RedirectToAction("Login", "Register");

            string username = Session["User"].ToString();
            string rolename = Session["Role"].ToString();


            if (rolename == "Seller" || rolename == "Admin")
            {
                var quser = db.Tbl_User.Where(a => a.User_Username == username && a.Tbl_Role.Role_Name == rolename).SingleOrDefault();

                if (quser != null)
                {
                    var quser1 = db.Tbl_Identity.Where(a => a.Identity_Userid == quser.User_ID && a.Identity_Confirm == true).SingleOrDefault();

                    if (quser1 != null)
                    {
                        var qpro = db.Tbl_Product.Where(a => a.Product_Userid == quser.User_ID).ToList();

                        if (qpro != null)
                        {
                            ViewBag.Count = qpro.Count();
                            ViewBag.Error = TempData["OK"];
                            ViewBag.State = TempData["Result"];
                            return View(qpro);
                        }
                        else
                        {
                            ViewBag.Error = "شما تاکنون محصولی را برای فروش ثبت نکرده اید!!";
                            return View();
                        }
                    }
                    else
                    {
                        TempData["State"] = "Error";
                        return RedirectToAction("Profile");
                    }

                }
                else
                {
                    TempData["State"] = "Error";
                    return RedirectToAction("Profile");
                }

            }
            else
            {
                TempData["State"] = "Error";
                return RedirectToAction("Profile");
            }


        }



        //1819-f2
        [HttpGet]
        public ActionResult EditProduct(int id = 0)
        {

            if (Session["User"] == null)
                return RedirectToAction("Login", "Register");

            if (id == 0)
            {
                return RedirectToAction("ListProduct");
            }


            string username = Session["User"].ToString();
            string rolename = Session["Role"].ToString();


            if (rolename == "Seller" || rolename == "Admin")
            {
                var quser = db.Tbl_User.Where(a => a.User_Username == username && a.Tbl_Role.Role_Name == rolename).SingleOrDefault();

                if (quser != null)
                {
                    var quser1 = db.Tbl_Identity.Where(a => a.Identity_Userid == quser.User_ID && a.Identity_Confirm == true).SingleOrDefault();

                    if (quser1 != null)
                    {
                        var qpro = db.Tbl_Product.Where(a => a.Product_Userid == quser.User_ID && a.Product_ID == id).SingleOrDefault();

                        if (qpro != null)
                        {
                            ViewBag.State = TempData["State"];
                            ViewBag.Result = TempData["Message"];
                            return View(qpro);
                        }
                        else
                        {
                            TempData["OK"] = "امکان ویرایش نیست";
                            TempData["State"] = "Error";
                            return RedirectToAction("ListProduct");
                        }
                    }
                    else
                    {
                        TempData["State"] = "Error";
                        return RedirectToAction("Profile");
                    }

                }
                else
                {
                    TempData["State"] = "Error";
                    return RedirectToAction("Profile");
                }

            }
            else
            {
                TempData["State"] = "Error";
                return RedirectToAction("Profile");
            }

        }

        //1819-f2
        [HttpPost]
        public ActionResult EditProduct(Tbl_Product p, HttpPostedFileBase imageindex, HttpPostedFileBase[] picgallery, string TagProduct, string cat1_id, int Productid)
        {

            if (Session["User"] == null)
                return RedirectToAction("Login", "Register");


            string username = Session["User"].ToString();
            string rolename = Session["Role"].ToString();


            if (rolename == "Seller" || rolename == "Admin")
            {
                var quser = db.Tbl_User.Where(a => a.User_Username == username && a.Tbl_Role.Role_Name == rolename).SingleOrDefault();

                if (quser != null)
                {
                    var quser1 = db.Tbl_Identity.Where(a => a.Identity_Userid == quser.User_ID && a.Identity_Confirm == true).SingleOrDefault();

                    if (quser1 != null)
                    {
                        var qproid = db.Tbl_Product.Where(a => a.Product_Userid == quser.User_ID && a.Product_ID == Productid).SingleOrDefault();

                        if (qproid != null)
                        {

                            //عملیات ویرایش


                            Random rnd = new Random();

                            if (imageindex != null)
                            {
                                string rndname = rnd.Next().ToString() + "-" + imageindex.FileName;

                                if (imageindex.ContentLength <= 512000)
                                {

                                    imageindex.SaveAs(Path.Combine(Server.MapPath("~") + "/Content/_images/product/" + rndname));

                                    //حذف تصویر قبلی
                                    try
                                    {
                                        System.IO.File.Delete(Server.MapPath("/Content/_images/product/") + qproid.Product_PicIndex);

                                    }
                                    catch
                                    {

                                    }

                                    qproid.Product_PicIndex = rndname;
                                    qproid.Product_AllOff = p.Product_AllOff;
                                    qproid.Product_ExitCount = p.Product_ExitCount;
                                    qproid.Product_Groupid = Convert.ToInt32(cat1_id);
                                    qproid.Product_IsDownload = p.Product_IsDownload;
                                    qproid.Product_Name = p.Product_Name;
                                    qproid.Product_Off = p.Product_Off;
                                    qproid.Product_Price = p.Product_Price;
                                    qproid.Product_Text = p.Product_Text;
                                    qproid.Product_Weight = p.Product_Weight;

                                    db.Tbl_Product.Attach(qproid);
                                    db.Entry(qproid).State = System.Data.Entity.EntityState.Modified;

                                    //حذف تگ ها
                                    foreach (var item in db.Tbl_Tags.Where(a => a.Tag_Proid == qproid.Product_ID))
                                    {
                                        db.Tbl_Tags.Remove(item);
                                    }
                                    db.SaveChanges();


                                    var tagname = TagProduct.Trim().Split('-');

                                    foreach (var item1 in tagname)
                                    {
                                        Tbl_Tags tg = new Tbl_Tags()
                                        {
                                            Tag_Proid = qproid.Product_ID,
                                            Tag_Name = item1.Trim()

                                        };

                                        db.Tbl_Tags.Add(tg);
                                        db.SaveChanges();
                                    }


                                    var qg = db.Tbl_Pic.Where(a => a.Pic_Proid == qproid.Product_ID).ToList();

                                    if (qg.Count() == 0)
                                    {
                                        if (picgallery[0] == null)
                                        {
                                            TempData["State"] = "Error";
                                            TempData["Message"] = "حداقل یک تصویر برای بخش گالری باید انتخاب شود";
                                            return RedirectToAction("EditProduct", new { id = qproid.Product_ID });
                                        }
                                        else
                                        {

                                            List<Tbl_Pic> lstpic = new List<Tbl_Pic>();
                                            foreach (var item in picgallery)
                                            {
                                                string namepic = rnd.Next().ToString() + item.FileName;

                                                if (item.ContentLength <= 512000)
                                                {
                                                    Tbl_Pic pic = new Tbl_Pic();
                                                    pic.Pic_Proid = qproid.Product_ID;
                                                    item.SaveAs(Path.Combine(Server.MapPath("~") + "/Content/_images/product/Gallery/" + namepic));
                                                    pic.Pic_Name = namepic;
                                                    lstpic.Add(pic);
                                                }
                                                else
                                                {
                                                    TempData["State"] = "Error";
                                                    TempData["Message"] = "حجم تصاویر انتخاب شده برای گالری بیش از 500 کیلوبایت می باشد";
                                                    return RedirectToAction("EditProduct", new { id = qproid.Product_ID });
                                                }
                                            }

                                            db.Tbl_Pic.AddRange(lstpic);
                                            db.SaveChanges();

                                            TempData["OK"] = "ویرایش با موفقیت انجام شد";
                                            TempData["Result"] = "OK";

                                            return RedirectToAction("ListProduct");
                                        }
                                    }
                                    else
                                    {

                                        if (picgallery[0] != null)
                                        {

                                            List<Tbl_Pic> lstpic = new List<Tbl_Pic>();
                                            foreach (var item in picgallery)
                                            {
                                                string namepic = rnd.Next().ToString() + item.FileName;
                                                if (item.ContentLength <= 512000)
                                                {
                                                    Tbl_Pic pic = new Tbl_Pic();
                                                    pic.Pic_Proid = qproid.Product_ID;
                                                    item.SaveAs(Path.Combine(Server.MapPath("~") + "/Content/_images/product/Gallery/" + namepic));
                                                    pic.Pic_Name = namepic;
                                                    lstpic.Add(pic);
                                                }
                                                else
                                                {
                                                    TempData["State"] = "Error";
                                                    TempData["Message"] = "حجم تصاویر انتخاب شده برای گالری بیش از 500 کیلوبایت می باشد";
                                                    return RedirectToAction("EditProduct", new { id = qproid.Product_ID });
                                                }
                                            }

                                            db.Tbl_Pic.AddRange(lstpic);
                                            db.SaveChanges();

                                            TempData["OK"] = "ویرایش با موفقیت انجام شد";
                                            TempData["Result"] = "OK";

                                            return RedirectToAction("ListProduct");
                                        }

                                        db.SaveChanges();

                                        TempData["OK"] = "ویرایش با موفقیت انجام شد";
                                        TempData["Result"] = "OK";

                                        return RedirectToAction("ListProduct");


                                    }

                                }
                                else
                                {
                                    TempData["State"] = "Error";
                                    TempData["Message"] = "حجم تصاویر انتخاب شده برای گالری بیش از 500 کیلوبایت می باشد";
                                    return RedirectToAction("EditProduct", new { id = qproid.Product_ID });
                                }


                            }
                            else
                            {

                                qproid.Product_AllOff = p.Product_AllOff;
                                qproid.Product_ExitCount = p.Product_ExitCount;
                                qproid.Product_Groupid = Convert.ToInt32(cat1_id);
                                qproid.Product_IsDownload = p.Product_IsDownload;
                                qproid.Product_Name = p.Product_Name;
                                qproid.Product_Off = p.Product_Off;
                                qproid.Product_Price = p.Product_Price;
                                qproid.Product_Text = p.Product_Text;
                                qproid.Product_Weight = p.Product_Weight;


                                db.Tbl_Product.Attach(qproid);
                                db.Entry(qproid).State = System.Data.Entity.EntityState.Modified;

                                foreach (var item in db.Tbl_Tags.Where(a => a.Tag_Proid == qproid.Product_ID))
                                {
                                    db.Tbl_Tags.Remove(item);
                                }
                                db.SaveChanges();


                                var tagname = TagProduct.Trim().Split('-');

                                foreach (var item1 in tagname)
                                {
                                    Tbl_Tags tg = new Tbl_Tags()
                                    {
                                        Tag_Proid = qproid.Product_ID,
                                        Tag_Name = item1.Trim()

                                    };

                                    db.Tbl_Tags.Add(tg);
                                    db.SaveChanges();
                                }

                                var qg = db.Tbl_Pic.Where(a => a.Pic_Proid == qproid.Product_ID).ToList();

                                if (qg.Count() == 0)
                                {
                                    if (picgallery[0] == null)
                                    {
                                        TempData["State"] = "Error";
                                        TempData["Message"] = "حداقل یک تصویر برای بخش گالری باید انتخاب شود";
                                        return RedirectToAction("EditProduct", new { id = qproid.Product_ID });
                                    }
                                    else
                                    {

                                        List<Tbl_Pic> lstpic = new List<Tbl_Pic>();
                                        foreach (var item in picgallery)
                                        {
                                            string namepic = rnd.Next().ToString() + item.FileName;
                                            if (item.ContentLength <= 512000)
                                            {
                                                Tbl_Pic pic = new Tbl_Pic();
                                                pic.Pic_Proid = qproid.Product_ID;
                                                item.SaveAs(Path.Combine(Server.MapPath("~") + "/Content/_images/product/Gallery/" + namepic));
                                                pic.Pic_Name = namepic;
                                                lstpic.Add(pic);
                                            }
                                            else
                                            {
                                                TempData["State"] = "Error";
                                                TempData["Message"] = "حجم تصاویر انتخاب شده برای گالری بیش از 500 کیلوبایت می باشد";
                                                return RedirectToAction("EditProduct", new { id = qproid.Product_ID });
                                            }
                                        }

                                        db.Tbl_Pic.AddRange(lstpic);
                                        db.SaveChanges();

                                        TempData["OK"] = "ویرایش با موفقیت انجام شد";
                                        TempData["Result"] = "OK";

                                        return RedirectToAction("ListProduct");
                                    }
                                }
                                else
                                {

                                    if (picgallery[0] != null)
                                    {

                                        List<Tbl_Pic> lstpic = new List<Tbl_Pic>();
                                        foreach (var item in picgallery)
                                        {
                                            string namepic = rnd.Next().ToString() + item.FileName;
                                            if (item.ContentLength <= 512000)
                                            {
                                                Tbl_Pic pic = new Tbl_Pic();
                                                pic.Pic_Proid = qproid.Product_ID;
                                                item.SaveAs(Path.Combine(Server.MapPath("~") + "/Content/_images/product/Gallery/" + namepic));
                                                pic.Pic_Name = namepic;
                                                lstpic.Add(pic);
                                            }
                                            else
                                            {
                                                TempData["State"] = "Error";
                                                TempData["Message"] = "حجم تصاویر انتخاب شده برای گالری بیش از 500 کیلوبایت می باشد";
                                                return RedirectToAction("EditProduct", new { id = qproid.Product_ID });
                                            }
                                        }

                                        db.Tbl_Pic.AddRange(lstpic);
                                        db.SaveChanges();

                                        TempData["OK"] = "ویرایش با موفقیت انجام شد";
                                        TempData["Result"] = "OK";

                                        return RedirectToAction("ListProduct");
                                    }

                                    db.SaveChanges();

                                    TempData["OK"] = "ویرایش با موفقیت انجام شد";
                                    TempData["Result"] = "OK";

                                    return RedirectToAction("ListProduct");


                                }




                            }


                        }
                        else
                        {
                            TempData["OK"] = "امکان ویرایش نیست";
                            TempData["State"] = "Error";
                            return RedirectToAction("ListProduct");
                        }
                    }
                    else
                    {
                        TempData["State"] = "Error";
                        return RedirectToAction("Profile");
                    }

                }
                else
                {
                    TempData["State"] = "Error";
                    return RedirectToAction("Profile");
                }

            }
            else
            {
                TempData["State"] = "Error";
                return RedirectToAction("Profile");
            }

        }

        //1819-f2
        [HttpPost]
        public string DelPic(int ids, int ProductIDs)
        {
            try
            {
                if (Session["User"] == null)//
                    return "";

                string username = Session["User"].ToString();

                var q = (from a in db.Tbl_Product
                         where a.Tbl_User.User_Username.Equals(username) && a.Product_ID.Equals(ProductIDs)
                         select a).SingleOrDefault();

                if (q != null)
                {
                    var q1 = q.Tbl_Pic.Where(a => a.Pic_ID.Equals(ids)).SingleOrDefault();
                    try
                    {
                        System.IO.File.Delete(Server.MapPath("/Content/_images/product/Gallery/") + q1.Pic_Name);

                    }
                    catch
                    {

                    }
                    db.Tbl_Pic.Remove(q1);
                    db.SaveChanges();


                    string Temp = "<ul class='list'>";

                    foreach (var item in q.Tbl_Pic)
                    {
                        Temp += "<li class='item full'> <span><img src='/Content/_images/product/Gallery/" + item.Pic_Name + "' width='80' height='80' alt='' /></span><span style='color: red; font-size: 20px;margin-right: 10px;'><a data-ajax='true' data-ajax-confirm='میخواهید حذف شود؟ ' data-ajax-method='Post' data-ajax-mode='replace' data-ajax-update='#PicDelet' href='/Profile/DelPic?ids=" + item.Pic_ID + "&ProductIDs=" + item.Pic_Proid + "'>X</a></span></li>";
                    }

                    Temp += "</ul>";

                    return Temp;

                }
                else
                {
                    return "خطایی رخ داد!!!";
                }



            }
            catch
            {
                return "خطایی رخ داد!!!";

            }
        }

    }
}
