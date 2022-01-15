namespace EntryPoint.Controllers;

public class DomainController : Controller
{
	private readonly Context context;
	private readonly DbSet<PracticeDb> practices;
	private readonly DbSet<StatDb> stats;
	private readonly DbSet<UserDb> users;

	public DomainController(Context context)
	{
		this.context = context;
		users = context.Users;
		practices = context.Practices;
		stats = context.Stats;
	}
	
	public IActionResult Index(string? name, DateTime date, int length, string place, string type, string description)
	{
		if (name != null)
		{
			AddPractice(new Practice
			{
				Name = name,
				Date = date,
				Description = description,
				Length = TimeSpan.FromMinutes(length),
				Place = place,
				Type = type,
				Users = new[]{"whoyaka"}
			});
		}
		var a = practices.Select(p => p.ToModel()).ToArray();
		return View(a);
	}

	[HttpPost(Api.Login)]
	public async Task<string> Login()
	{
		var user = await ReadBody<User>();
		return JsonSerializer.Serialize(await Login(user));
	}

	[HttpPost("LoginWeb")]
	public async Task<bool> Login(string name, string password)
	{
		return await Login(new User { Name = name, Password = password });
	}

	private async Task<bool> Login(User user)
	{
		var dbUser = await users.FindAsync(user.Name);
		return dbUser != null && dbUser.Password == user.Password;
	}

	[HttpPost(Api.Register)]
	public async Task<string> Register()
	{
		var user = await ReadBody<User>();
		return JsonSerializer.Serialize(UpdateUser(user, 0));
	}

	[HttpPost("RegisterWeb")]
	public bool Register(string name, string password)
	{
		return UpdateUser(new User { Name = name, Password = password }, 0);
	}

	[HttpPost(Api.DeleteUser)]
	public async Task<string> DeleteUser()
	{
		var user = await ReadBody<User>();
		return JsonSerializer.Serialize(UpdateUser(user, 1));
	}

	[HttpPost("DeleteUserWeb")]
	public bool DeleteUser(string name, string password)
	{
		return UpdateUser(new User { Name = name, Password = password }, 1);
	}

	private bool UpdateUser(User user, int action)
	{
		var userDb = users.Find(user.Name);
		switch (action)
		{
			case 0:
				if (userDb != null)
					return false;
				users.Add(user.ToDb());
				break;
			case 1:
				if (userDb == null)
					return true;
				users.Remove(userDb);
				break;
		}

		return context.SaveChanges() == 1;
	}

	[HttpPost(Api.AddPractice)]
	public async Task<string> AddPractice()
	{
		var practice = await ReadBody<Practice>();
		practices.Add(practice.ToDb());
		var result = await context.SaveChangesAsync() == 1;
		return JsonSerializer.Serialize(result);
	}

	private void AddPractice(Practice practice)
	{
		practices.Add(practice.ToDb());
		context.SaveChanges();
	}

	[HttpPost(Api.DeletePractice)]
	public async Task<string> DeletePractice()
	{
		var practice = await ReadBody<Practice>();
		return JsonSerializer.Serialize(DeletePractice(practice));
	}

	private bool DeletePractice(Practice practice)
	{
		var db = practice.ToDb();
		var existing = practices.Where(p => p.Name == practice.Name && p.Date.Date == db.Date.Date && p.Users == db.Users);
		practices.RemoveRange(existing);
		return context.SaveChanges() == 1;
	}

	[HttpPost(Api.GetPractices)]
	public async Task<string> GetPractices()
	{
		var practice = await ReadBody<Practice>();

		var result = practices.Where(p => p.Date.Date == practice.Date.Date)
			.Select(p => p.ToModel())
			.Where(p => practice.Users == null || practice.Users.All(u => p.Users!.Contains(u)))
			.ToArray();
		return JsonSerializer.Serialize(result);
	}

	[HttpPost(Api.PatchStat)]
	public async Task<string> PatchStat(DbAction dbAction)
	{
		var stat = await ReadBody<Stat>();

		switch (dbAction)
		{
			case DbAction.Add:
				stats.Add(stat.ToDb());
				break;
			case DbAction.Update:
				stats.Update(stat.ToDb());
				break;
			case DbAction.Delete:
				var existing = GetStats(stat).ToArray();
				if (!existing.Any())
					return JsonSerializer.Serialize(true);
				stats.RemoveRange(existing);
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(dbAction), dbAction, null);
		}

		var result = await context.SaveChangesAsync();
		return JsonSerializer.Serialize(result == 1);
	}

	[HttpPost(Api.GetStats)]
	public async Task<string> GetStats()
	{
		var stat = await ReadBody<Stat>();

		var result = GetStats(stat).Select(s => s.ToModel()).ToArray();
		return JsonSerializer.Serialize(result);
	}

	private IEnumerable<StatDb> GetStats(Stat stat)
	{
		var date = DateOnly.FromDateTime(stat.Date);
		return stats.Where(s => s.User == stat.User && (stat.Date == default || date == s.Date) && (stat.Name == null || stat.Name == s.Name));
	}

	private async Task<T> ReadBody<T>()
	{
		return await JsonSerializer.DeserializeAsync<T>(Request.BodyReader.AsStream()) 
		       ?? throw new NullReferenceException();
	}
}