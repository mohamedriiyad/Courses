using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Courses.Models
{
    public partial class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Enrollement = new HashSet<Enrollement>();
        }

        public int? UniversityId { get; set; }

        public virtual University University { get; set; }
        public virtual ICollection<Enrollement> Enrollement { get; set; }
    }
}
