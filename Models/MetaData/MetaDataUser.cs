using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Storenarm2.Models.Domins
{
    internal class MetaDataUser
    {
      
    }

    [MetadataType(typeof(MetaDataUser))]
    public partial class Tbl_User
    {

    }

}
