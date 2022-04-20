using System;
using System.Collections.Generic;

namespace Courses.Models
{
    public partial class Enrollement
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public int? CourseId { get; set; }
        public float Grade { get; set; }

        public virtual Course Course { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
