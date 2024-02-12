using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
	public class MonthOfExpenseController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
