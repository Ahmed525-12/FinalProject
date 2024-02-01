using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ThriftinessCore.Entites.Identity;
using WebApplication1.ViewModel;

namespace WebApplication1.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var User = new AppUser()
                {
                    UserName = model.Email.Split("@")[0],
                    Email = model.Email,
                    DisplayName = model.DisplayName,
                };
                var result = await _userManager.CreateAsync(User, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("LogIn");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(model);
        }

        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LoginVM logInVM)
        {
            if (ModelState.IsValid)
            {
                var User = await _userManager.FindByEmailAsync(logInVM.Email);
                if (User != null)
                {
                    var flag = await _userManager.CheckPasswordAsync(User, logInVM.Password);

                    if (flag)
                    {
                        var Result = await _signInManager.PasswordSignInAsync(User, logInVM.Password, logInVM.RemeberMe, false);

                        if (Result.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email Not Found");
                }
            }

            return View(logInVM);
        }
    }
}