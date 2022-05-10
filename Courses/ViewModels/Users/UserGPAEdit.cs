using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.ViewModels.Users
{
    public class UserGPAEdit
    {
        [Required]
        [Display(Name = "GPA")]
        [Range(0, 4.0, ErrorMessage = "Grade Must be between 0 and 4.")]
        public double? NewGPA { get; set; }
    }
}
