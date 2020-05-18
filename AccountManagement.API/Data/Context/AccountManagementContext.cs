using AccountManagement.API.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.API.Data.Context
{
    public class AccountManagementContext : DbContext
    {
        public AccountManagementContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationOrganizations>().HasKey(sc => new { sc.Application, sc.Organization });
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<AccountLog> AccountLogs { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<ApplicationOrganizations> ApplicationOrganizations { get; set; }
        public DbSet<License> Licenses { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
    }
}
