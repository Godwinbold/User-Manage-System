using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UserManagement_CodeWithSL.Models;
using UserManagement_CodeWithSL.Models.ViewModel;

namespace UserManagement_CodeWithSL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var dummyListOfUsers = new List<UserToReturnViewModel>
            {
                new UserToReturnViewModel{Id="1", FirstName="Kenedy", LastName="Tariah", PhotoUrl=""},
                new UserToReturnViewModel{Id="2", FirstName="Murphy", LastName="Ogbeide", PhotoUrl=""},
                new UserToReturnViewModel{Id="3", FirstName="Godwin", LastName="Ozioko", PhotoUrl=""},
                new UserToReturnViewModel{Id="4", FirstName="Chinedu", LastName="Francis", PhotoUrl=""},
                new UserToReturnViewModel{Id="5", FirstName="Wisdom", LastName="Ogwuchie", PhotoUrl=""},
                new UserToReturnViewModel{Id="6", FirstName="Jackson", LastName="Samson", PhotoUrl=""},
            };
            return View(dummyListOfUsers);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}