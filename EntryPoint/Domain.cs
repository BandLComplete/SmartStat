using System;
using System.Linq;
using EntryPoint.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EntryPoint
{
	public class Domain
	{
		public Domain(Context context)
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

		public void AddPractice(Practice practice)
		{
			practices.Add(practice);
			context.SaveChanges();
		}

		public Practice[] GetPractices(User user, DateTime? exclusiveStart, DateTime? inclusiveEnd)
		{
			return practices.Where(p => p.Users.Contains(user.Name) &&
			                            (exclusiveStart == null || p.Date > exclusiveStart) &&
			                            (inclusiveEnd == null || p.Date <= inclusiveEnd))
				.ToArray();
		}
		
		private readonly Context context;
		private readonly DbSet<User> users;
		private readonly DbSet<Practice> practices;
	}
}