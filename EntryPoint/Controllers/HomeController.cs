using System.Diagnostics;
using System.Linq;
using EntryPoint.Database;
using Microsoft.AspNetCore.Mvc;
using EntryPoint.Models;
using FirstApp.Service;

namespace EntryPoint.Controllers
{
	public class HomeController : Controller
	{
		private readonly Context context;

		public HomeController(Context context)
		{
			this.context = context;
		}

		public IActionResult Index()
		{
			var a = new Client();
			a.AddPractice(new Practice{Id = 1}).GetAwaiter().GetResult();
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
		}
	}
}