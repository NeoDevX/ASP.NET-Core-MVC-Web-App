using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;
using WebApp.ViewModel;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if(ModelState.IsValid == false) 
                return View(loginViewModel);

            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);
            if (user == null)
                return WrongCredentials("User not found");

            bool isPasswordCorrect = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
            if (isPasswordCorrect == false)
                return WrongCredentials();

            var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
            return result.Succeeded ? RedirectToAction("Index", "Race") : WrongCredentials();

            IActionResult WrongCredentials(string message = "Wrong credentials please try again")
            {
                TempData["Error"] = message;
                return View(loginViewModel);
            }
        }

        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid == false)
                return View(registerViewModel);

            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
            if (user != null)
                return SignUpError("This email address is already in use");
            
            var newUser = new AppUser
            {
                Email = registerViewModel.EmailAddress,
                UserName = registerViewModel.EmailAddress
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);
            if (newUserResponse.Succeeded == false)
                return SignUpError("Password is not valid");

            await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            return RedirectToAction("Index", "Club");
            
            IActionResult SignUpError(string message)
            {
                TempData["Error"] = message;
                return View(registerViewModel);
            }
        }
        
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Club");
        }
    }
}