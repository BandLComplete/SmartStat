using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Domain;
using EntryPoint.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntryPoint.Controllers
{
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

        [HttpPost]
        [Route(Api.Login)]
        public async Task<string> Login()
        {
            var user = await ReadBody<User>();
            return JsonSerializer.Serialize(Login(user));
        }

        [HttpPost]
        [Route("LoginWeb")]
        public bool Login(string name, string password)
        {
            return Login(new User { Name = name, Password = password });
        }

        private bool Login(User user)
        {
            var dbUser = users.Find(user.Name);
            return dbUser != null && dbUser.Password == user.Password;
        }

        [HttpPost]
        [Route(Api.Register)]
        public async Task<string> Register()
        {
            var user = await ReadBody<User>();
            return JsonSerializer.Serialize(UpdateUser(user, 0));
        }

        [HttpPost]
        [Route("RegisterWeb")]
        public bool Register(string name, string password)
        {
            return UpdateUser(new User { Name = name, Password = password }, 0);
        }

        [HttpPost]
        [Route(Api.DeleteUser)]
        public async Task<string> DeleteUser()
        {
            var user = await ReadBody<User>();
            return JsonSerializer.Serialize(UpdateUser(user, 1));
        }

        [HttpPost]
        [Route("DeleteUserWeb")]
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

        [HttpPost]
        [Route(Api.AddPractice)]
        public async Task<string> AddPractice()
        {
            var practice = await ReadBody<Practice>();
            practices.Add(practice.ToDb());
            var result = await context.SaveChangesAsync() == 1;
            return JsonSerializer.Serialize(result);
        }

        [HttpPost]
        [Route(Api.DeletePractice)]
        public async Task<string> DeletePractice()
        {
            var practice = await ReadBody<Practice>();
            return JsonSerializer.Serialize(DeletePractice(practice));
        }

        private bool DeletePractice(Practice practice)
        {
            var db = practice.ToDb();
            var existing = practices.Where(p =>
                p.Name == practice.Name && p.Date.Date == practice.Date.Date && p.Users == db.Users);
            practices.RemoveRange(existing);
            return context.SaveChanges() == 1;
        }

        [HttpPost]
        [Route(Api.GetPractices)]
        public async Task<string> GetPractices()
        {
            var practice = await ReadBody<Practice>();

            var result = practices.Where(p => p.Date.Date == practice.Date.Date)
                .AsEnumerable()
                .Select(p => p.ToModel())
                .Where(p => practice.Users == null || practice.Users.All(u => p.Users!.Contains(u)))
                .ToArray();
            return JsonSerializer.Serialize(result);
        }

        [HttpPost]
        [Route(Api.PatchStat)]
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

        [HttpPost]
        [Route(Api.GetStats)]
        public async Task<string> GetStats()
        {
            var stat = await ReadBody<Stat>();

            var result = GetStats(stat).Select(s => s.ToModel()).ToArray();
            return JsonSerializer.Serialize(result);
        }

        private IEnumerable<StatDb> GetStats(Stat stat)
        {
            return stats.Where(s => s.User == stat.User && (stat.Date == default || stat.Date.Date == s.Date.Date) && (stat.Name == null || stat.Name == s.Name));
        }

        private async Task<T> ReadBody<T>()
        {
            return await JsonSerializer.DeserializeAsync<T>(Request.BodyReader.AsStream()) 
                   ?? throw new NullReferenceException();
        }
    }
}