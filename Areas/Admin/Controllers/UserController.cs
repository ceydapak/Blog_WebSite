using AspNetCoreHero.ToastNotification.Abstractions;
using Blog_WebSite.Models;
using Blog_WebSite.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

namespace Blog_WebSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly INotyfService _notification;

        public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, INotyfService notification ) { 
             _notification = notification;
            _userManager = userManager;
            _signInManager = signInManager;
        
        
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            if(!HttpContext.User.Identity.IsAuthenticated) {
                return View(new LoginVM());
            }
            return RedirectToAction("Index", "User", new { area = "Admin" });   
        
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            if(!ModelState.IsValid)
            {
                return View(vm);
            }
            var checkExistingOfUser = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == vm.Username);

            if (checkExistingOfUser == null)
            {
                _notification.Error("Username does not exist!");
                return View(vm);

            }
            var verifyPassword =  await _userManager.CheckPasswordAsync(checkExistingOfUser,vm.Password);


            if (!verifyPassword)
            {
                _notification.Error("Password is not true!");
                return View(vm);

            }
            await _signInManager.PasswordSignInAsync(vm.Username, vm.Password, vm.RememberMe, true);
            _notification.Success("logining is successful.");
            return RedirectToAction("Index", "User", new {area="Admin"});
        }

        [HttpPost]
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            _notification.Success("You logged out.");
            return RedirectToAction("Index", "Home", new {area=""});
        }
    }
}
