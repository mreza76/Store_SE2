//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Storenarm2.Models.Domins
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tbl_Setting
    {
        public int Id { get; set; }
        public string Smtp { get; set; }
        public string Passsword { get; set; }
        public string Email { get; set; }
        public Nullable<int> Discount { get; set; }
        public string Keyword { get; set; }
        public string Descrption { get; set; }
        public string DescSite { get; set; }
        public string TitleSite { get; set; }
        public string Tilte { get; set; }
        public Nullable<int> PageCount { get; set; }
        public System.DateTime DateDeleteBill { get; set; }
    }
}