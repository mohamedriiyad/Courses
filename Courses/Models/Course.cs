using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Courses.Models
{
    public partial class Course
    {
        public Course()
        {
            Enrollement = new HashSet<Enrollement>();
            Prerequisites = new HashSet<PrerequisitesCourse>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public float Grade { get; set; }
        public string Name { get; set; }
        public int? Credit { get; set; }
        public int? DepartmentId { get; set; }

        public virtual ICollection<PrerequisitesCourse> Prerequisites { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<Enrollement> Enrollement { get; set; }
    }
}
