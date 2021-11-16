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
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}
	}
}