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
    public class SaveGoalController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public SaveGoalController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(userEmail);
            var specmonth = new SaveGoalSpecf(user.Id);

            var monthOfExpense = await _unitOfWork.Repository<SaveGoal>().GetAllWithSpecAsync(specmonth);
            await _unitOfWork.CompleteAsync();

            var mappedResults = _mapper.Map<IEnumerable<SaveGoalVM>>(monthOfExpense);

            return View(mappedResults);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Createe(SaveGoalVM model)
        {
            try
            {
                var userEmail = User.FindFirstValue(ClaimTypes.Email);
                var user = await _userManager.FindByEmailAsync(userEmail);
                var saveGoal = new SaveGoal()
                {
                    TargetAmount = model.TargetAmount,
                    TitleGoal = model.TitleGoal,
                    User_Id = user.Id
                };
                await _unitOfWork.Repository<SaveGoal>().AddAsync(saveGoal);
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
            var saveGoal = await _unitOfWork.Repository<SaveGoal>().GetbyIdAsync(id.Value);
            await _unitOfWork.CompleteAsync();
            var mappedResults = _mapper.Map<SaveGoalVM>(saveGoal);
            return View(mappedResults);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveGoalVM model)
        {
            try
            {
                var mappedResults = _mapper.Map<SaveGoal>(model);
                _unitOfWork.Repository<SaveGoal>().Update(mappedResults);
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
            var saveGoal = await _unitOfWork.Repository<SaveGoal>().GetbyIdAsync(id.Value);
            await _unitOfWork.CompleteAsync();
            var mappedResults = _mapper.Map<SaveGoalVM>(saveGoal);
            return View(mappedResults);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(SaveGoalVM model)
        {
            try
            {
                var mappedResults = _mapper.Map<SaveGoal>(model);
                _unitOfWork.Repository<SaveGoal>().DeleteAsync(mappedResults);
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