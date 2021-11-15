using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using EntryPoint.Database;
using FirstApp.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntryPoint.Controllers
{
	public class DomainController : Controller
	{
		public DomainController(Context context)
		{
			this.context = context;
			users = context.Users;
			practices = context.Practices;
		}

		public User? TryLogin(string name, string password)
		{
			var user = users.Find(name);
			if (user != null && user.Password == password) return null;
			return user;
		}

		public bool TryRegister(User user)
		{
			var old = users.Find(user.Name);
			if (old != null)
				return false;
			users.Add(user);
			return context.SaveChanges() == 1;
		}

		[HttpPost, Route("AddPractice")]
		public void AddPractice()
		{
			var ms = new MemoryStream();
			Request.Body.CopyToAsync(ms).GetAwaiter().GetResult();
			var s = Encoding.UTF8.GetString(ms.ToArray());
			var practice = JsonSerializer.Deserialize<Practice>(s);
			practices.Add(practice!.ToDb());
			context.SaveChanges();
		}

		[HttpGet, Route("GetPractices")]
		public string GetPractices()
		{
			var result = practices.Select(p => p.ToModel()).ToArray();
			return JsonSerializer.Serialize(result);
		}

		public PracticeDb[] GetPractices(User user, DateTime? exclusiveStart, DateTime? inclusiveEnd)
		{
			return practices.Where(p => p.Users.Contains(user.Name) &&
			                            (exclusiveStart == null || p.Date > exclusiveStart) &&
			                            (inclusiveEnd == null || p.Date <= inclusiveEnd))
				.ToArray();
		}

		private readonly Context context;
		private readonly DbSet<User> users;
		private readonly DbSet<PracticeDb> practices;
	}
}