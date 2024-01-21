using System.ComponentModel.DataAnnotations;

namespace UserManagement_CodeWithSL.Models.ViewModel
{
    public class UserToLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        public bool RememberMe { get; set; }
    }
}
