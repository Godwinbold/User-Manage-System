﻿using System.ComponentModel.DataAnnotations;

namespace UserManagement_CodeWithSL.Models.ViewModel
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
