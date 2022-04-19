using System;
using System.Collections.Generic;

namespace Courses.Models
{
    public partial class Course
    {
        public Course()
        {
            Enrollement = new HashSet<Enrollement>();
            InverseCoursePreNavigation = new HashSet<Course>();
        }

        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public int? Credit { get; set; }
        public int CoursePre { get; set; }
        public int? DepartmentId { get; set; }

        public virtual Course CoursePreNavigation { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<Enrollement> Enrollement { get; set; }
        public virtual ICollection<Course> InverseCoursePreNavigation { get; set; }
    }
}
