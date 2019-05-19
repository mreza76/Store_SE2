using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Storenarm2.Models.Domins;

namespace Storenarm2.Models.Repository
{
    public class Rep_Register
    {
        DB db = new DB();

        public IEnumerable<Tbl_State> Get_State()
        {
            var q = db.Tbl_State.OrderBy(a => a.State_Name).ToList();
            return q.AsEnumerable();
        }


    }
}