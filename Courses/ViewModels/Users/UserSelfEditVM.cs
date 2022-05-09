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
        public double? NewGPA { get; set; }

    }
}
