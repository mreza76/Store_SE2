using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Storenarm2.Models.Domins;

namespace Storenarm2.Models.Repository
{
    public class Rep_Profile
    {
        DB db = new DB();


        public IEnumerable<Tbl_User> GetUser()
        {
            var q = from a in db.Tbl_User select a;
            return q.AsEnumerable();
        }

        public IEnumerable<Tbl_Role> GetRole()
        {
            var q = db.Tbl_Role.OrderBy(a => a.Role_ID).ToList();
            return q.AsEnumerable();
        }

        public IEnumerable<Tbl_Setting> GetSetting()
        {
            var q = from a in db.Tbl_Setting select a;
            return q.AsEnumerable();
        }
        

        //15-f2
        public IEnumerable<Tbl_Group> GetGroup()
        {
            var q = from a in db.Tbl_Group select a;
            return q.AsEnumerable();
        }

        public IEnumerable<Tbl_Bill> GetBill()
        {
            var q = from a in db.Tbl_Bill select a;
            return q.AsEnumerable();
        }

        //11-12-f2
        public IEnumerable<Tbl_Identity> GetConfirm()
        {
            var q = from a in db.Tbl_Identity select a;
            return q.AsEnumerable();
        }

        //11-f2
        public IEnumerable<Tbl_Product> GetProduct()
        {
            var q = from a in db.Tbl_Product select a;
            return q.AsEnumerable();
        }

        //11-f2
        public IEnumerable<Tbl_Message> GetMsg()
        {
            var q = from a in db.Tbl_Message select a;
            return q.AsEnumerable();
        }


        //18-f2
        public IEnumerable<Tbl_Pic> GetGallery()
        {
            var q = from a in db.Tbl_Pic select a;
            return q.AsEnumerable();
        }


        public IEnumerable<Tbl_Bill> GetSale()
        {
            var q = db.Tbl_Bill.OrderByDescending(a => a.Bill_Date).ToList();
            return q.AsEnumerable();
        }


        public IEnumerable<Tbl_Withdrawal> GetWithdrawal()
        {
            var q = db.Tbl_Withdrawal.OrderBy(a => a.Withdrawal_Userid).ToList();
            return q.AsEnumerable();
        }

        public IEnumerable<Tbl_Tags> GetTag()
        {
            var q = from a in db.Tbl_Tags select a;
            return q.AsEnumerable();
        }

        //22-f2
        public IEnumerable<Tbl_Download> Get_Download()
        {
            var q = from a in db.Tbl_Download select a;
            return q.AsEnumerable();
        }

        public IEnumerable<Tbl_PostStatus> Getpost()
        {
            var q = from a in db.Tbl_PostStatus orderby a.St_ID select a;
            return q.AsEnumerable();
        }

        public IEnumerable<Tbl_NoBank> GetNBank()
        {
            var qs = from a in db.Tbl_NoBank
                     join b in db.Tbl_BankeName on a.Banks_NameID equals b.Bank_ID
                     select a;

            // var q = db.Tbl_NoBank.OrderBy(a => a.Banks_ID).ToList();
            return qs.AsEnumerable();
        }

        public IEnumerable<Tbl_BankeName> GetBankName()
        {
            var q = db.Tbl_BankeName.OrderBy(a => a.Bank_Name).ToList();
            return q.AsEnumerable();
        }


    }
}