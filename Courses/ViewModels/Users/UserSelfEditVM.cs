using Courses.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Courses.ViewModels.Users
{
    public class UserSelfEditVM : UserEditVM
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [Display(Name = "GPA")]
        [Range(0, 4.0, ErrorMessage = "Grade Must be between 0 and 4.")]
        public double? NewGPA { get; set; }

    }
}
