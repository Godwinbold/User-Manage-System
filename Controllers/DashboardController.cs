using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserManagement_CodeWithSL.Models.Entities;
using UserManagement_CodeWithSL.Models.ViewModel;

namespace UserManagement_CodeWithSL.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DashboardController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ManageUser()
        {
            var users = _userManager.Users;
            var roles = _roleManager.Roles;
            var ListOfUserToReturn = new List<UserToReturnViewModel>();
            

            if(users != null && users.Any() )
            {
                var listOfUserToReturn = users.Select( u =>  new UserToReturnViewModel
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    //Role = r.Name

                }).ToList();
            }
            return View(ListOfUserToReturn);
        }

        [HttpGet]
        //[AllowAnonymous]
        public IActionResult ManageRole()

        {
			var roles = _roleManager.Roles;
			var ListOfRolesToReturn = new List<RolesToReturnViewModel>();


			if (roles != null && roles.Any())
			{
				var listOfRolesToReturn = roles.Select( r => new RolesToReturnViewModel
				{
					Id = r.Id,
					Name = r.Name,
				}).ToList();
			}
			return View(ListOfRolesToReturn);
		}
         
    }
}
