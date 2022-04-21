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
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<PrerequisitesCourse>(pc =>
            {
                pc.HasKey(e => new { e.CourseId, e.PreCourseId });
                pc.HasOne(e => e.Course).WithMany(c => c.Prerequisites);
                pc.HasOne(e => e.PreCourse).WithMany().OnDelete(DeleteBehavior.ClientSetNull);
            });
        }
        public DbSet<University> Universities { get; set; }
        public DbSet<Course> Courses{ get; set; }
        public DbSet<Department> Departments{ get; set; }
        public DbSet<Enrollement> Enrollements{ get; set; }
        public DbSet<ApplicationUser> Users{ get; set; }
        public DbSet<Courses.Models.PrerequisitesCourse> PrerequisitesCourse { get; set; }
    }
}
