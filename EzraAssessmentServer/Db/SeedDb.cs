using EzraAssessmentServer.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzraAssessmentServer.Db
{
    public class SeedDb
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new EzraAssessmentDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<EzraAssessmentDbContext>>()))
            {
                // Cancel if data was already seeded.
                if (context.Employees.Any()) return;

                context.Employees.Add(  new Employee{
                    Name = "John Smith",
                    Email = "john.smith@ezra.com",
                    PhoneNumber = "416-999-9999"
                });

                context.Employees.Add(  new Employee{
                    Name = "Jane Doe",
                    Email = "jane.doe@ezra.com",
                    PhoneNumber = "905-111-1111"
                });

                context.Employees.Add(  new Employee{
                    Name = "John Doe",
                    Email = "john.doe@ezra.com",
                    PhoneNumber = "416-555-5555"
                });

                context.SaveChanges();
            }
        }
    }
}
