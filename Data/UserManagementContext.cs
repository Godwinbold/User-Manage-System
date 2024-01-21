using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserManagement_CodeWithSL.Models.Entities;

namespace UserManagement_CodeWithSL.Data
{
    public class UserManagementContext: IdentityDbContext<AppUser>
    {
        public UserManagementContext(DbContextOptions<UserManagementContext> options) : base(options) { }
        
    }
}
