using System;
using System.Collections.Generic;

namespace Courses.Models
{
    public partial class Enrollement
    {
        public int Id { get; set; }
        public int? StudentId { get; set; }
        public int? CourseId { get; set; }
        public int? Grade { get; set; }

        public virtual Course Course { get; set; }
        public virtual Member Student { get; set; }
    }
}
