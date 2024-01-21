using Microsoft.AspNetCore.Identity;

namespace UserManagement_CodeWithSL.Models.Entities
{
    public class AppUser:  IdentityUser
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public string? PhotoUrl { get; set; } = string.Empty;
    }
}
