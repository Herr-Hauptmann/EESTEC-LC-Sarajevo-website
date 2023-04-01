﻿using System.ComponentModel.DataAnnotations;

namespace EESTEC.ViewModel
{
    public class LoginViewModel
    {
        [Display(Name = "Email")]
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
