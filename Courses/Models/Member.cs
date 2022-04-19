using System;
using System.Collections.Generic;

namespace Courses.Models
{
    public partial class Member
    {
        public Member()
        {
            Enrollement = new HashSet<Enrollement>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? Type { get; set; }
        public int? UniversityId { get; set; }

        public virtual University University { get; set; }
        public virtual ICollection<Enrollement> Enrollement { get; set; }
    }
}
