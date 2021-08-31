using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options){}
        public DbSet<Student> Students { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    id = 1,
                    name = "Kamlesh",
                    department = Dept.CSE,
                    email = "kamlesh@gmail.com"
                },
                new Student
                {
                    id = 2,
                    name = "Arbind",
                    department = Dept.BPharmacy,
                    email = "Arbind@gmail.com"
                }
            );

        }
    }
   
}
