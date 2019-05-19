using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Storenarm2.Models.Plugins
{
    public class Email
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Smtp">smtp</param>
        /// <param name="From">ایمیل فرستنده</param>
        /// <param name="Password">رمز عبور ایمیل</param>
        /// <param name="To">ایمیل گیرنده</param>
        /// <param name="Subject">موضوع ایمیل</param>
        /// <param name="Body">متن ایمیل</param>
        public  void SendEmail( string Smtp , string From , string Password ,  string To , string Subject , string Body)
        {
            MailMessage MyEmail = new MailMessage();

            MyEmail.From = new MailAddress(From);
            MyEmail.To.Add(To);
            MyEmail.Subject = Subject;
            MyEmail.Body = Body;
            MyEmail.IsBodyHtml = true;
            MyEmail.Priority = MailPriority.High;

            SmtpClient mysmtp = new SmtpClient();
            mysmtp.Host = "smtp.gmail.com";
            mysmtp.UseDefaultCredentials = false;
            mysmtp.EnableSsl = true;
            mysmtp.Port = 587;
            mysmtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            mysmtp.Credentials = new NetworkCredential(From, Password);
            mysmtp.Send(MyEmail);


        }
    }
}