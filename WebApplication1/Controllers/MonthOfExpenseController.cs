using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ThriftinessCore.Entites;
using ThriftinessCore.Entites.Identity;
using ThriftinessCore.Repos;
using ThriftinessCore.Specfictions;
using WebApplication1.ViewModel;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class MonthOfExpenseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public MonthOfExpenseController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(userEmail);
            var specmonth = new MonthOfExpenseSpecf(user.Id);

            var monthOfExpense = await _unitOfWork.Repository<MonthOfExpense>().GetAllWithSpecAsync(specmonth);

            user.TotalExpense = user.MonthlySalary - monthOfExpense.Sum(o => o.TotalAmountMoney);

            if (DateTime.Today.Day == user.DayOfEndMonth)
            {
                await CreateMonthOfExpense();
                user.TotalExpense = user.TotalExpense + user.MonthlySalary;
            }
            await _userManager.UpdateAsync(user);
            await _unitOfWork.CompleteAsync();
            var mappedResults = _mapper.Map<IEnumerable<MonthOfExpenseVM>>(monthOfExpense);

            return View(mappedResults);
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

        public async Task<IActionResult> DetailsExpenss(int id)
        {
            if (id == null)
                return BadRequest();
            var specmonth = new ExpenseSpecfMonth(id);
            var monthOfExpense = await _unitOfWork.Repository<Expense>().GetAllWithSpecAsync(specmonth);

            await _unitOfWork.CompleteAsync();

            var mappedResults = _mapper.Map<IEnumerable<ExpenseVM>>(monthOfExpense);
            return View(mappedResults);
            //___________________________________ end of method ----------------------------------
        }
    }
}