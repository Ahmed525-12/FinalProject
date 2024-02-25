using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ThriftinessCore.Entites;
using ThriftinessCore.Entites.Identity;
using ThriftinessCore.Repos;
using ThriftinessCore.Specfictions;
using ThriftinessRepository.Repos;
using WebApplication1.ViewModel;

namespace WebApplication1.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public ExpenseController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(userEmail);
            var specmonth = new ExpenseSpecfUser(user.Id);

            var monthOfExpense = await _unitOfWork.Repository<Expense>().GetAllWithSpecAsync(specmonth);
            await _unitOfWork.CompleteAsync();

            var mappedResults = _mapper.Map<IEnumerable<ExpenseVM>>(monthOfExpense);

            return View(mappedResults);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Createe(ExpenseVM model)
        {
            try
            {
                var userEmail = User.FindFirstValue(ClaimTypes.Email);
                var user = await _userManager.FindByEmailAsync(userEmail);
                var checkMonth = new MonthOfExpenseSpecfNum(DateTime.Today.Month, user.Id);
                var checkMonthOfExpense = await _unitOfWork.Repository<MonthOfExpense>().GetAllWithSpecAsync(checkMonth);
                var expense = new Expense()
                {
                    Title = model.Title,
                    AmountMoney = model.AmountMoney,
                    Priority = model.Priority,
                    UserId = user.Id,
                    ExpenseDate = DateTime.Now,
                    MonthOfExpenseId = checkMonthOfExpense.FirstOrDefault().Id
                };
                await _unitOfWork.Repository<Expense>().AddAsync(expense);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(model);
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return BadRequest();
            var monthOfExpense = await _unitOfWork.Repository<Expense>().GetbyIdAsync(id.Value);
            await _unitOfWork.CompleteAsync();
            var mappedResults = _mapper.Map<ExpenseVM>(monthOfExpense);
            return View(mappedResults);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ExpenseVM model)
        {
            try
            {
                var mappedResults = _mapper.Map<Expense>(model);
                _unitOfWork.Repository<Expense>().Update(mappedResults);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest();
            var monthOfExpense = await _unitOfWork.Repository<Expense>().GetbyIdAsync(id.Value);
            await _unitOfWork.CompleteAsync();
            var mappedResults = _mapper.Map<ExpenseVM>(monthOfExpense);
            return View(mappedResults);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ExpenseVM model)
        {
            try
            {
                var mappedResults = _mapper.Map<Expense>(model);
                _unitOfWork.Repository<Expense>().DeleteAsync(mappedResults);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(model);
            }
        }
    }
}