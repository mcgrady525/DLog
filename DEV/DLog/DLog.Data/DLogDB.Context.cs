﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using DLog.Entity;


namespace DLog.Data
{
    public partial class DLogDB : DbContext
    {
      public DLogDB()
            : base(string.Format(@"metadata=res://*/DLogDB.csdl|res://*/DLogDB.ssdl|res://*/DLogDB.msl;provider=System.Data.SqlClient;provider connection string='{0};multipleactiveresultsets=True;App=EntityFramework'",System.Configuration.ConfigurationManager.ConnectionStrings["DLogDB"].ConnectionString))
        {
    		this.Configuration.ProxyCreationEnabled = false; //默认关闭代理类
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<ErrorLog> ErrorLog { get; set; }
        public DbSet<PerfLog> PerfLog { get; set; }
        public DbSet<SearchInfo> SearchInfo { get; set; }
        public DbSet<DebugLog> DebugLog { get; set; }
        public DbSet<CommonConfig> CommonConfig { get; set; }
        public DbSet<PerfLogSearchInfo> PerfLogSearchInfo { get; set; }
        public DbSet<Resource> Resource { get; set; }
    }
}
