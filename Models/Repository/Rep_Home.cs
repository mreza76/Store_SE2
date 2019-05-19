using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Storenarm2.Models.Domins;


namespace Storenarm2.Models.Repository
{


    public class Rep_Home
    {

        DB db = new DB();

        public IEnumerable<Tbl_Slider> Get_Slid()
        {
            var q = db.Tbl_Slider.Where(b => b.Enable == true).OrderBy(a => a.Sort).ToList();

            return q.AsEnumerable();
        }
        public IEnumerable<Tbl_Visit> Get_Visit()
        {
            var q = db.Tbl_Visit.OrderBy(a => a.VisitDate).ToList();

            return q.AsEnumerable();
        }


        public IEnumerable<Tbl_Product> GEt_NewPro()
        {
            var q = db.Tbl_Product.Where(a => a.Product_Active == true && a.Tbl_User.User_Active == true).OrderByDescending(a => a.Product_Date).ToList().Take(15);

            if (q == null)
            {
                return null;
            }
            else
            {
                return q.AsEnumerable();
            }
        }



        public IEnumerable<Tbl_Product> Get_AmarPro()
        {
            var q = db.Tbl_Product.Where(a => a.Product_Active == true && a.Tbl_User.User_Active == true).ToList();
            if (q == null)
            {
                return null;
            }
            else
            {
                return q.AsEnumerable();
            }

        }



        public IEnumerable<Tbl_User> Get_User()
        {
            var q = db.Tbl_User.Where(a => a.User_Active == true).ToList();
            return q.AsEnumerable();
        }

        // 20-f2
        public IEnumerable<Tbl_Product> Get_Product()
        {
            var q = db.Tbl_Product.OrderBy(a => a.Product_Date).ToList();
            return q.AsEnumerable();
        }

        //20-f2
        public IEnumerable<Tbl_Setting> Get_Off()
        {
            var q = from a in db.Tbl_Setting select a;
            return q.AsEnumerable();
        }

        //20-f2
        public IEnumerable<Tbl_Pic> Get_pic()
        {
            var q = db.Tbl_Pic.OrderBy(a => a.Pic_Name).ToList();
            return q.AsEnumerable();
        }
        public IEnumerable<Tbl_Tags> Get_tag()
        {
            var q = db.Tbl_Tags.OrderBy(a => a.Tag_Name).ToList();
            return q.AsEnumerable();
        }


        public IEnumerable<Tbl_Group> GetGroup()
        {
            var q = db.Tbl_Group.OrderBy(a => a.Group_Name).ToList();
            return q.AsEnumerable();
        }
    }
}