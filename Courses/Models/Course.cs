using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        public int Id { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public float Grade { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Credit { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        public virtual ICollection<PrerequisitesCourse> Prerequisites { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<Enrollement> Enrollement { get; set; }
    }
}
