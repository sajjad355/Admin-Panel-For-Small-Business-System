using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ATP2_Project.Models.DataViewModel
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "User Name is required")]
        [Display(Name = "Username:")]
        public string username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "password:")]
        public string password { get; set; }
    }
}