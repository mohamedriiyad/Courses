using System;
using System.Collections.Generic;

namespace Courses.Models
{
    public partial class Department
    {
        public Department()
        {
            Courses = new HashSet<Course>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? UniversityId { get; set; }

        public virtual University University { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
