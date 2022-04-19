using Courses.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Courses.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<University> Universities { get; set; }
        public DbSet<Course> Courses{ get; set; }
        public DbSet<Department> Departments{ get; set; }
        public DbSet<Enrollement> Enrollements{ get; set; }
        public DbSet<Member> Members{ get; set; }
    }
}
