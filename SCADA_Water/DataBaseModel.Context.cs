﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ReporterWPF
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ABFAEntities : DbContext
    {
        public ABFAEntities()
            : base("name=ABFAEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<DataSend> DataSends { get; set; }
        public virtual DbSet<Node_Mapping> Node_Mapping { get; set; }
        public virtual DbSet<Pump_Station> Pump_Station { get; set; }
        public virtual DbSet<ReporterConnectUser> ReporterConnectUsers { get; set; }
        public virtual DbSet<StationsABFA> StationsABFAs { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<UsersABFA> UsersABFAs { get; set; }
        public virtual DbSet<Userw> Userws { get; set; }
        public virtual DbSet<VersionApp> VersionApps { get; set; }
        public virtual DbSet<Water_Supply> Water_Supply { get; set; }
        public virtual DbSet<SIMCard> SIMCards { get; set; }
        public virtual DbSet<SimCardCharge> SimCardCharges { get; set; }
        public virtual DbSet<State> States { get; set; }
    }
}