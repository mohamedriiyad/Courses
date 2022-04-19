using System;
using System.Collections.Generic;

namespace Courses.Models
{
    public partial class University
    {
        public University()
        {
            Departments = new HashSet<Department>();
            Members = new HashSet<Member>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<Member> Members { get; set; }
    }
}
