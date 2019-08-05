using EzraAssessmentServer.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzraAssessmentServer.Db
{
    public class EzraAssessmentDbContext : DbContext
    {
        public EzraAssessmentDbContext(DbContextOptions<EzraAssessmentDbContext> options)
        : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure entity filters - ignore soft-deleted entries.
            modelBuilder.Entity<Employee>().HasQueryFilter(p => !p.IsDeleted);
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
