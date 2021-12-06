using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using Domain;
using EntryPoint.Database;
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

		[HttpPost, Route(Api.Login)]
		public string Login()
		{
			var user = ReadBody<User>();
			return JsonSerializer.Serialize(Login(user));
		}

		private bool Login(User user)
		{
			var dbUser = users.Find(user.Name);
			return dbUser != null && dbUser.Password == user.Password;
		}

		[HttpPost, Route(Api.Register)]
		public string Register()
		{
			return JsonSerializer.Serialize(UpdateUser(0));
		}

		[HttpPost, Route(Api.DeleteUser)]
		public string DeleteUser()
		{
			return JsonSerializer.Serialize(UpdateUser(1));
		}

		private bool UpdateUser(int action)
		{
			var user = ReadBody<User>();
			var userDb = users.Find(user.Name);
			switch (action)
			{
				case 0:
					if (userDb != null)
						return false;
					users.Add(user.ToDb());
					break;
				case 1:
					users.Remove(userDb);
					break;
			}

			return context.SaveChanges() == 1;
		}

		[HttpPost, Route("AddPractice")]
		public void AddPractice()
		{
			var practice = ReadBody<Practice>().ToDb();
			practices.Add(practice);
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

		private T ReadBody<T>()
		{
			var ms = new MemoryStream();
			Request.Body.CopyToAsync(ms).GetAwaiter().GetResult();
			var s = Encoding.UTF8.GetString(ms.ToArray());
			return JsonSerializer.Deserialize<T>(s) ?? throw new NullReferenceException($"Failed to deserialized {s}");
		}

		private readonly Context context;
		private readonly DbSet<UserDb> users;
		private readonly DbSet<PracticeDb> practices;
	}
}