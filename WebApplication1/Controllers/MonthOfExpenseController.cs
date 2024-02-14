using AutoMapper;
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
            await _unitOfWork.CompleteAsync();

            var mappedResults = _mapper.Map<IEnumerable<MonthOfExpenseVM>>(monthOfExpense);

            return View(mappedResults);
        }
    }
}