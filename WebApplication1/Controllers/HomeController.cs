using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using ThriftinessCore.Entites;
using ThriftinessCore.Entites.Identity;
using ThriftinessCore.Repos;
using ThriftinessCore.Specfictions;
using WebApplication1.Models;
using WebApplication1.ViewModel;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(UserManager<AppUser> userManager, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(userEmail);

            // User
            var mappedUser = _mapper.Map<UserVM>(user);

            // Save Goals
            var specSaveGoal = new SaveGoalSpecf(user.Id);
            var saveGoals = await _unitOfWork.Repository<SaveGoal>().GetAllWithSpecAsync(specSaveGoal);
            await _unitOfWork.CompleteAsync();
            var mappedSaveGoals = _mapper.Map<IEnumerable<SaveGoalVM>>(saveGoals);

            // Month Of Expenses
            var specMonthOfExpense = new MonthOfExpenseSpecf(user.Id);
            var monthOfExpenses = await _unitOfWork.Repository<MonthOfExpense>().GetAllWithSpecAsync(specMonthOfExpense);

            if (monthOfExpenses.Count == 0 || DateTime.Today.Day == user.DayOfEndMonth)
            {
                await CreateMonthOfExpense();
                user.TotalExpense = user.TotalExpense + user.MonthlySalary;
            }

            var mappedMonthOfExpenses = _mapper.Map<IEnumerable<MonthOfExpenseVM>>(monthOfExpenses);

            // Combine all view models
            var combinedModel = new CombinedHomeScreen
            {
                UserVM = mappedUser,
                SaveGoalVM = mappedSaveGoals,
                MonthOfExpenseVM = mappedMonthOfExpenses
            };

            return View(combinedModel);
        }

        private async Task CreateMonthOfExpense()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(userEmail);
            var checkMonth = new MonthOfExpenseSpecfNum(DateTime.Today.Month, user.Id);
            var checkMonthOfExpense = await _unitOfWork.Repository<MonthOfExpense>().GetAllWithSpecAsync(checkMonth);
            if (checkMonthOfExpense.Count == 0)
            {
                var monthofExpense = new MonthOfExpense()
                {
                    numOfMonth = DateTime.Today.Month,
                    User_Id = user.Id,
                    TotalAmountMoney = 0,
                };

                await _unitOfWork.Repository<MonthOfExpense>().AddAsync(monthofExpense);
                await _unitOfWork.CompleteAsync();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}