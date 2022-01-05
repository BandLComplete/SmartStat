using System.Linq;
using Domain;
using EntryPoint.Database;

namespace EntryPoint
{
    public static class DbExtensions
    {
        private const char separator = '\n';

        public static bool CompareIfNotNull<T>(this T? first, T second)
        {
            return first == null || first.Equals(second);
        }

        public static User ToModel(this UserDb userDb)
        {
            return new User
            {
                Name = userDb.Name,
                Password = userDb.Password
            };
        }

        public static UserDb ToDb(this User user)
        {
            return new UserDb
            {
                Name = user.Name,
                Password = user.Password
            };
        }

        public static Practice ToModel(this PracticeDb practiceDb)
        {
            return new Practice
            {
                Name = practiceDb.Name,
                Users = practiceDb.Users.Split(separator),
                Date = practiceDb.Date,
                Length = practiceDb.Length,
                Place = practiceDb.Place,
                Type = practiceDb.Type,
                Description = practiceDb.Description,
                Tag = practiceDb.Tag,
                Notification = practiceDb.Notification
            };
        }

        public static PracticeDb ToDb(this Practice practice)
        {
            return new PracticeDb
            {
                Name = practice.Name,
                Users = string.Join(separator, practice.Users.OrderBy(u => u)),
                Date = practice.Date,
                Length = practice.Length,
                Place = practice.Place,
                Type = practice.Type,
                Description = practice.Description,
                Tag = practice.Tag,
                Notification = practice.Notification
            };
        }

        public static Stat ToModel(this StatDb statDb)
        {
            return new Stat
            {
                User = statDb.User,
                Date = statDb.Date,
                Name = statDb.Name,
                Value = statDb.Value,
                Unit = statDb.Unit
            };
        }

        public static StatDb ToDb(this Stat stat)
        {
            return new StatDb
            {
                UserDateName = string.Join(separator, stat.User, stat.Date.ToShortDateString(), stat.Name),
                User = stat.User!,
                Date = stat.Date,
                Name = stat.Name!,
                Value = stat.Value,
                Unit = stat.Unit
            };
        }
    }
}