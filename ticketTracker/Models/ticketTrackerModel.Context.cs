﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ticketTracker.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ticketTrackerEntities : DbContext
    {
        public ticketTrackerEntities()
            : base("name=ticketTrackerEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblClosedTicket> tblClosedTickets { get; set; }
        public virtual DbSet<tblOpenTicket> tblOpenTickets { get; set; }
        public virtual DbSet<tblUser> tblUsers { get; set; }
    }
}
