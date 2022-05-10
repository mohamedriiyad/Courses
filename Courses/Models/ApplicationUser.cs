using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Courses.Models
{
    public partial class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Enrollement = new HashSet<Enrollement>();
        }

        [Range(0,4.0,ErrorMessage ="GPA Must be between 0 and 4.")]
        public double? GPA { get; set; }
        public int? UniversityId { get; set; }

        public virtual University University { get; set; }
        public virtual ICollection<Enrollement> Enrollement { get; set; }
    }
}
