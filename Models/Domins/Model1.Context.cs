﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DB : DbContext
    {
        public DB()
            : base("name=DB")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Tbl_BankeName> Tbl_BankeName { get; set; }
        public virtual DbSet<Tbl_Bill> Tbl_Bill { get; set; }
        public virtual DbSet<Tbl_ConfirmEmail> Tbl_ConfirmEmail { get; set; }
        public virtual DbSet<Tbl_Cooki> Tbl_Cooki { get; set; }
        public virtual DbSet<Tbl_Download> Tbl_Download { get; set; }
        public virtual DbSet<Tbl_Group> Tbl_Group { get; set; }
        public virtual DbSet<Tbl_Identity> Tbl_Identity { get; set; }
        public virtual DbSet<Tbl_InterimBill> Tbl_InterimBill { get; set; }
        public virtual DbSet<Tbl_Message> Tbl_Message { get; set; }
        public virtual DbSet<Tbl_NoBank> Tbl_NoBank { get; set; }
        public virtual DbSet<Tbl_Pic> Tbl_Pic { get; set; }
        public virtual DbSet<Tbl_PostStatus> Tbl_PostStatus { get; set; }
        public virtual DbSet<Tbl_Product> Tbl_Product { get; set; }
        public virtual DbSet<Tbl_Role> Tbl_Role { get; set; }
        public virtual DbSet<Tbl_Setting> Tbl_Setting { get; set; }
        public virtual DbSet<Tbl_Slider> Tbl_Slider { get; set; }
        public virtual DbSet<Tbl_State> Tbl_State { get; set; }
        public virtual DbSet<Tbl_Tags> Tbl_Tags { get; set; }
        public virtual DbSet<Tbl_TempDL> Tbl_TempDL { get; set; }
        public virtual DbSet<Tbl_User> Tbl_User { get; set; }
        public virtual DbSet<Tbl_Visit> Tbl_Visit { get; set; }
        public virtual DbSet<Tbl_Weight> Tbl_Weight { get; set; }
        public virtual DbSet<Tbl_Withdrawal> Tbl_Withdrawal { get; set; }
    }
}
