using System.Diagnostics;
using System.Linq;
using EntryPoint.Database;
using Microsoft.AspNetCore.Mvc;
using EntryPoint.Models;

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
			context.Users.Add(new User {Name = "test"});
			context.SaveChanges();
			var array = context.Users.ToArray();
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