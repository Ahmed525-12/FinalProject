using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ThriftinessCore.Entites.Identity;
using WebApplication1.Helper;
using WebApplication1.ViewModel;

namespace WebApplication1.Controllers
{
    public class UserSettingsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public UserSettingsController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddMonthlySalary()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddMonthlySalary(UserSettingsVM model)
        {
            if (ModelState.IsValid)
            {
                var userEmail = User.FindFirstValue(ClaimTypes.Email);
                var user = await _userManager.FindByEmailAsync(userEmail);
                user.MonthlySalary = model.MonthlySalary;
                await _userManager.UpdateAsync(user);
                return RedirectToAction("Index");
            }
            else
            {
                // If model state is not valid, return the view with validation errors
                return View(model);
            }
        }

        public IActionResult AddDayOfEndMonth()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddDayOfEndMonth(UserSettingsVM model)
        {
            if (ModelState.IsValid)
            {
                var userEmail = User.FindFirstValue(ClaimTypes.Email);
                var user = await _userManager.FindByEmailAsync(userEmail);
                user.DayOfEndMonth = model.DayOfEndMonth;
                await _userManager.UpdateAsync(user);
                return RedirectToAction("Index");
            }
            else
            {
                // If model state is not valid, return the view with validation errors
                return View(model);
            }
        }
    }
}