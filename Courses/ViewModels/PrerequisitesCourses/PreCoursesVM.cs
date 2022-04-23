using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.ViewModels.PrerequisitesCourses
{
    public class PreCoursesVM
    {
        public int CourseId { get; set; }

        public List<test> tests { get; set; }

    }

    public class test
    {
        public int Id { get; set; }
        public string name { get; set; }
    }
}
