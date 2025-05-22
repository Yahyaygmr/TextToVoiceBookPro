using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace SesAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        public HomeController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Library");
            else
                return RedirectToAction("Login", "Account");
        }
    }
} 